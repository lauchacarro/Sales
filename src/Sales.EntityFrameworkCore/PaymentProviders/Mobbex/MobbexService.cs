using System.Linq;
using System.Threading.Tasks;

using PayPalHttp;

using Sales.Domain.Entities.Invoices;
using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Products;
using Sales.Domain.Options;
using Sales.Domain.PaymentProviders;
using Sales.Domain.ValueObjects;
using Sales.EntityFrameworkCore.PaymentProviders.Mobbex.Models;

namespace Sales.EntityFrameworkCore.PaymentProviders.Mobbex
{
    public class MobbexService : IMobbexService
    {

        private const string ENDPOINT = "/p/checkout";
        private readonly IApiGateway _apiGateway;
        private readonly IMobbexOptions _mobbexOptions;
        public MobbexService(IApiGateway apiGateway, IMobbexOptions mobbexOptions)
        {
            _apiGateway = apiGateway;
            _mobbexOptions = mobbexOptions;
        }

        public Task CancelInvoice(InvoicePaymentProvider invoice)
        {
            throw new System.NotImplementedException();
        }

        public async Task<InvoicePaymentProvider> CreateUriForPayment(Invoice invoice, Order order, Plan plan)
        {
            PlanPrice price = plan.PlanPrices.Single(x => x.Currency.Code == Currency.CurrencyValue.ARS);
            var contract = new CheckoutCreateContract
            {
                Total = price.Price,
                Currency = Currency.CurrencyValue.ARS.ToString(),
                Reference = invoice.Id.ToString(),
                Description = plan.Description,
                ReturnUrl = _mobbexOptions.ReturnUrl + "?invoiceId=" + invoice.Id,
                Webhook = _mobbexOptions.WebhookUrl + "?invoiceId=" + invoice.Id,
                Test = _mobbexOptions.Environment == Domain.Enums.PaymentProviderEnvironment.Sandbox,
                Options = new SubscriptionOptionsDto()
            };

            var response = await _apiGateway.PostAsync<CheckoutContract, CheckoutCreateContract>(ENDPOINT, contract);

            return new InvoicePaymentProvider
            {
                Link = new System.Uri(response.Data.Url),
                Transaction = response.Data.Id,
                InvoceId = invoice.Id,
                PaymentProvider = new PaymentProvider(PaymentProvider.PaymentProviderValue.Mobbex)
            };
        }


        public Task<InvoicePaymentProvider> CreateUriForPayment(Invoice invoice, Order order, ProductSale productSale)
        {
            throw new System.NotImplementedException();
        }
    }
}