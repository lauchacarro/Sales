using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Abp.Events.Bus;

using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

using PayPalHttp;

using Sales.Domain.Entities.Invoices;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Products;
using Sales.Domain.Enums;
using Sales.Domain.Options;
using Sales.Domain.PaymentProviders;
using Sales.Domain.ValueObjects;
using Sales.EntityFrameworkCore.PaymentProviders.Paypal.Request;

using PaypalOrder = PayPalCheckoutSdk.Orders.Order;

namespace Sales.EntityFrameworkCore.PaymentProviders
{
    public class PaypalService : IPaypalService
    {
        private readonly IPaypalOptions _paypalOptions;
        private readonly IEventBus _eventBus;

        public PaypalService(IPaypalOptions paypalOptions, IEventBus eventBus)
        {
            _paypalOptions = paypalOptions ?? throw new ArgumentNullException(nameof(paypalOptions));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task CancelInvoice(InvoicePaymentProvider invoice)
        {
            PayPalEnvironment environment = CreateEnvironment();
            var client = new PayPalHttpClient(environment);

            var request = new OrderDeleteRequest(invoice.Transaction);

            try
            {
                await client.Execute(request);
            }
            catch (HttpException httpException)
            {
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
            }
        }

        public async Task<InvoicePaymentProvider> CreateUriForPayment(Invoice invoice, Domain.Entities.Orders.Order order, Plan plan)
        {
            PayPalEnvironment environment = CreateEnvironment();
            var client = new PayPalHttpClient(environment);

            var payment = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>()
                {
                    new PurchaseUnitRequest()
                    {
                        CustomId = invoice.Id.ToString(),
                        Description = plan.Description,
                        AmountWithBreakdown = new AmountWithBreakdown()
                        {
                            CurrencyCode = Currency.CurrencyValue.USD.ToString(),
                            Value = Convert.ToInt32(plan.PlanPrices.Single(x => x.Currency.Code == Currency.CurrencyValue.USD).Price).ToString()
                        }
                    }
                },
                ApplicationContext = CreateApplicationContext()
            };

            //https://developer.paypal.com/docs/api/orders/v2/#orders_create
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(payment);

            try
            {
                var response = await client.Execute(request);
                var result = response.Result<PaypalOrder>();
                var uri = new Uri(result.Links.Single(l => l.Rel == "approve").Href);

                return new InvoicePaymentProvider
                {
                    InvoceId = invoice.Id,
                    Link = uri,
                    Transaction = result.Id,
                    PaymentProvider = new PaymentProvider(PaymentProvider.PaymentProviderValue.Paypal)
                };

            }
            catch (HttpException httpException)
            {
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                throw httpException;
            }
        }

        public async Task<Guid> ConfirmOrder(string token)
        {
            PayPalEnvironment environment = CreateEnvironment();
            var client = new PayPalHttpClient(environment);

            var request = new OrdersCaptureRequest(token);
            request.Prefer("return=representation");
            request.RequestBody(new OrderActionRequest());
            try
            {
                HttpResponse response = await client.Execute(request);
                PaypalOrder order = response.Result<PaypalOrder>();
                Guid invoiceId = Guid.Parse(order.PurchaseUnits.First().CustomId);

                return invoiceId;
                //_eventBus.Trigger(new InvoicePaymentProviderPayedEventData(invoiceId));
            }
            catch (HttpException httpException)
            {
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                throw httpException;
            }
        }

        public Task<InvoicePaymentProvider> CreateUriForPayment(Invoice invoice, Domain.Entities.Orders.Order order, ProductSale productSale)
        {
            throw new NotImplementedException();
        }

        private PayPalEnvironment CreateEnvironment()
        {
            if (_paypalOptions.Environment == PaymentProviderEnvironment.Live)
            {
                return new LiveEnvironment(_paypalOptions.ClientId, _paypalOptions.ClientSecret);
            }
            else
            {
                return new SandboxEnvironment(_paypalOptions.ClientId, _paypalOptions.ClientSecret);
            }
        }

        private ApplicationContext CreateApplicationContext()
        {

            var returnUrl = _paypalOptions.ReturnUrl;

            var cancelUrl = _paypalOptions.CancelUrl;

            return new ApplicationContext
            {
                ReturnUrl = returnUrl,
                CancelUrl = cancelUrl
            };
        }

        public async Task<InvoicePaymentProvider> CreateUriForPayment(Invoice invoice, Domain.Entities.Orders.Order order, string description)
        {
            PayPalEnvironment environment = CreateEnvironment();
            var client = new PayPalHttpClient(environment);

            var payment = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>()
                {
                    new PurchaseUnitRequest()
                    {
                        CustomId = invoice.Id.ToString(),
                        Description = description,
                        AmountWithBreakdown = new AmountWithBreakdown()
                        {
                            CurrencyCode = Currency.CurrencyValue.USD.ToString(),
                            Value = Convert.ToInt32(order.TotalAmount).ToString()
                        }
                    }
                },
                ApplicationContext = CreateApplicationContext()
            };

            //https://developer.paypal.com/docs/api/orders/v2/#orders_create
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(payment);

            try
            {
                var response = await client.Execute(request);
                var result = response.Result<PaypalOrder>();
                var uri = new Uri(result.Links.Single(l => l.Rel == "approve").Href);

                return new InvoicePaymentProvider
                {
                    InvoceId = invoice.Id,
                    Link = uri,
                    Transaction = result.Id,
                    PaymentProvider = new PaymentProvider(PaymentProvider.PaymentProviderValue.Paypal)
                };

            }
            catch (HttpException httpException)
            {
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                throw httpException;
            }
        }

        public async Task<InvoicePaymentProvider> UpdateAmountInvoice(InvoicePaymentProvider invoicePaymentProvider, Domain.Entities.Orders.Order order)
        {
            PayPalEnvironment environment = CreateEnvironment();
            var client = new PayPalHttpClient(environment);

            var getOrderRequest = new OrdersGetRequest(invoicePaymentProvider.Transaction);

            try
            {
                var getOrderResponse = await client.Execute(getOrderRequest);
                var getOrderResult = getOrderResponse.Result<PaypalOrder>();
                var payment = new OrderRequest()
                {
                    CheckoutPaymentIntent = "CAPTURE",
                    PurchaseUnits = new List<PurchaseUnitRequest>()
                    {
                        new PurchaseUnitRequest()
                        {
                            CustomId = getOrderResult.PurchaseUnits[0].CustomId,
                            Description = getOrderResult.PurchaseUnits[0].Description,
                            AmountWithBreakdown = new AmountWithBreakdown()
                            {
                                CurrencyCode = Currency.CurrencyValue.USD.ToString(),
                                Value = Convert.ToInt32(order.TotalAmount).ToString()
                            }
                        }
                    },
                    ApplicationContext = CreateApplicationContext()
                };

                var request = new OrdersCreateRequest();
                request.Prefer("return=representation");
                request.RequestBody(payment);
                var response = await client.Execute(request);
                var result = response.Result<PaypalOrder>();
                var uri = new Uri(result.Links.Single(l => l.Rel == "approve").Href);

                await CancelInvoice(invoicePaymentProvider);

                invoicePaymentProvider.Link = uri;
                invoicePaymentProvider.Transaction = result.Id;

                return invoicePaymentProvider;
            }
            catch (HttpException httpException)
            {
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                throw httpException;
            }
        }
    }
}
