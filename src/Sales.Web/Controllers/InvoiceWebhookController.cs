using System;
using System.Threading.Tasks;

using Abp.AspNetCore.Mvc.Controllers;

using Microsoft.AspNetCore.Mvc;

using Sales.Application.Services.Abstracts;
using Sales.Domain.Options;
using Sales.EntityFrameworkCore.PaymentProviders.Mobbex.Models;

namespace Sales.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceWebhookController : AbpController
    {
        private readonly IInvoiceWebhookAppService _invoiceWebhookAppService;
        private readonly IClientOptions _clientOptions;

        public InvoiceWebhookController(IInvoiceWebhookAppService invoiceWebhookAppService, IClientOptions clientOptions)
        {
            _invoiceWebhookAppService = invoiceWebhookAppService ?? throw new ArgumentNullException(nameof(invoiceWebhookAppService));
            _clientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
        }

        [HttpGet]
        public async Task<IActionResult> WebhookReturnPaypal([FromQuery] string token)
        {
            await _invoiceWebhookAppService.WebhookPaypal(token);
            return Redirect(_clientOptions.WebhookReturnUrl);
        }

        [HttpGet]
        public IActionResult WebhookReturnMobbex([FromQuery] Guid invoiceId, [FromQuery] int status, [FromQuery] string transactionId)
        {
            return Redirect(_clientOptions.WebhookReturnUrl);
        }

        [HttpGet]
        public IActionResult WebhookCancelPaypal()
        {
            return Redirect(_clientOptions.WebhookReturnUrl);
        }

        [HttpGet]
        public IActionResult WebhookCancelMobbex([FromQuery] string token)
        {
            return Redirect(_clientOptions.WebhookReturnUrl);
        }

        [HttpPost]
        public IActionResult WebhookNotificationMobbex([FromQuery] Guid invoiceId, [FromForm] MobbexWebhookModel webhook)
        {
            if (webhook.Data?.Payment?.Status?.Code == "200" && webhook.Type == "checkout")
            {
                _invoiceWebhookAppService.WebhookMobbex(invoiceId, int.Parse(webhook.Data.Payment.Status.Code), webhook.Data.Payment.Id);
            }

            return Redirect(_clientOptions.WebhookReturnUrl);
        }
    }
}
