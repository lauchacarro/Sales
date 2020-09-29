using System.Threading.Tasks;

using Abp.Application.Services;

namespace Sales.Application.Services.Abstracts
{
    public interface IInvoiceWebhookAppService : IApplicationService
    {
        Task WebhookPaypal(string token);
    }
}
