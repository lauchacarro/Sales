
using System.Threading.Tasks;

using Abp.Application.Services;

using Sales.Application.Dtos.Subscriptions;

namespace Sales.Application.Services.Abstracts
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task<SubscriptionOrderDto> CreateSubscription(CreateSubscriptionInput input);
    }
}
