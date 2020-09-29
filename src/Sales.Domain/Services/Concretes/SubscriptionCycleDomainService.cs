using System;

using Abp.Domain.Services;

using Sales.Domain.Entities.Subscriptions;
using Sales.Domain.Services.Abstracts;
using Sales.Domain.ValueObjects.Plans;
using Sales.Domain.ValueObjects.Subscriptions;

namespace Sales.Domain.Services.Concretes
{
    public class SubscriptionCycleDomainService : DomainService, ISubscriptionCycleDomainService
    {
        public void ActiveSubscriptionCycle(SubscriptionCycle subscriptionCycle, DateTime activationDate, PlanCycleDuration planCycleDuration)
        {
            subscriptionCycle.ActivationDate = activationDate;
            subscriptionCycle.ExpirationDate = planCycleDuration.Duration switch
            {
                PlanCycleDuration.PlanCycleDurationValue.Daily => activationDate.AddDays(1),
                PlanCycleDuration.PlanCycleDurationValue.Weekly => activationDate.AddDays(7),
                PlanCycleDuration.PlanCycleDurationValue.TwoWeeks => activationDate.AddDays(15),
                PlanCycleDuration.PlanCycleDurationValue.Monthly => activationDate.AddMonths(1),
                PlanCycleDuration.PlanCycleDurationValue.Annually => activationDate.AddYears(1),
                _ => throw new NotImplementedException()
            };

            subscriptionCycle.Status.Status = SubscriptionCycleStatus.SubscriptionCycleStatusValue.Active;
        }

        public void CancelSubscriptionCycle(SubscriptionCycle subscriptionCycle)
        {
            subscriptionCycle.Status.Status = SubscriptionCycleStatus.SubscriptionCycleStatusValue.Canceled;
        }

        public SubscriptionCycle CreateSubscriptionCycle(Subscription subscription, DateTime createdDate)
        {
            return new SubscriptionCycle
            {
                CreationDate = createdDate,
                Status = new SubscriptionCycleStatus(SubscriptionCycleStatus.SubscriptionCycleStatusValue.Created),
                SubscriptionId = subscription.Id
            };
        }

        public void FinishedSubscriptionCycle(SubscriptionCycle subscriptionCycle)
        {
            subscriptionCycle.Status.Status = SubscriptionCycleStatus.SubscriptionCycleStatusValue.Finished;
        }

        public void PaymentPendingSubscriptionCycle(SubscriptionCycle subscriptionCycle)
        {
            subscriptionCycle.Status.Status = SubscriptionCycleStatus.SubscriptionCycleStatusValue.PaymentPending;
        }
    }
}
