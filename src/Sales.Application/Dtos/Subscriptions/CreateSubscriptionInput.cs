using System;

using Sales.Domain.Tools;

namespace Sales.Application.Dtos.Subscriptions
{
    public class CreateSubscriptionInput : IHasUserId<Guid>
    {
        public Guid UserId { get; set; }
        public Guid PlanPriceId { get; set; }
    }
}
