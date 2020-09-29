
using Abp.Domain.Services;

using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Subscriptions;

namespace Sales.Domain.Services.Abstracts
{
    public interface ISubscriptionDomainService : IDomainService
    {
        Subscription CreateSubscription(Plan plan);
        void ActiveSubscription(Subscription subscription);
        void CancelSubscription(Subscription subscription);
        void SuspendSubscription(Subscription subscription);
        void FinishSubscription(Subscription subscription);
    }
}
