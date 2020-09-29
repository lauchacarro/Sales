
using Abp.Domain.Services;

using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.Services.Abstracts;
using Sales.Domain.ValueObjects.Subscriptions;

namespace Sales.Domain.Services.Concretes
{
    public class SubscriptionDomainService : DomainService, ISubscriptionDomainService
    {
        public void ActiveSubscription(Subscription subscription)
        {
            subscription.Status.Status = SubscriptionStatus.SubscriptionStatusValue.Active;
        }

        public void CancelSubscription(Subscription subscription)
        {
            subscription.Status.Status = SubscriptionStatus.SubscriptionStatusValue.Cancelated;
        }

        public Subscription CreateSubscription(Plan plan)
        {
            return new Subscription
            {
                PlanId = plan.Id,
                Type = new SubscriptionType(SubscriptionType.SubscriptionTypeValue.Normal),
                Status = new SubscriptionStatus(SubscriptionStatus.SubscriptionStatusValue.Created)
            };
        }

        public void FinishSubscription(Subscription subscription)
        {
            subscription.Status.Status = SubscriptionStatus.SubscriptionStatusValue.Finished;
        }

        public void SuspendSubscription(Subscription subscription)
        {
            subscription.Status.Status = SubscriptionStatus.SubscriptionStatusValue.Suspended;
        }
    }
}
