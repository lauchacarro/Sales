using System;

using Abp.Domain.Services;

using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.ValueObjects.Plans;

namespace Sales.Domain.Services.Abstracts
{
    public interface ISubscriptionCycleDomainService : IDomainService
    {
        SubscriptionCycle CreateSubscriptionCycle(Subscription subscription, DateTime createdDate);
        void ActiveSubscriptionCycle(SubscriptionCycle subscriptionCycle, DateTime activationDate, PlanCycleDuration planCycleDuration);
        void CancelSubscriptionCycle(SubscriptionCycle subscriptionCycle);
        void PaymentPendingSubscriptionCycle(SubscriptionCycle subscriptionCycle);
        void FinishedSubscriptionCycle(SubscriptionCycle subscriptionCycle);
    }
}
