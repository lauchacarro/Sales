using System;

using Abp.Domain.Services;

using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;

namespace Sales.Domain.Services.Abstracts
{
    public interface IOrderDomainService : IDomainService
    {
        Order CreateOrderForSubscription(PlanPrice planPrice, Guid userId, DateTime creationDate);
        void PaymentPendingOrder(Order order);
        void PayOrder(Order order);
    }
}
