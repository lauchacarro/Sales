using System;

using Abp.Domain.Services;

using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Services.Abstracts;
using Sales.Domain.ValueObjects.Orders;

namespace Sales.Domain.Services.Concretes
{
    public class OrderDomainService : DomainService, IOrderDomainService
    {
        public void PaymentPendingOrder(Order order)
        {
            order.Status.Status = OrderStatus.OrderStatusValue.PaymentPending;
        }

        public Order CreateOrderForSubscription(PlanPrice planPrice, Guid userId, DateTime creationDate)
        {
            return new Order
            {
                CreationTime = creationDate,
                Currency = planPrice.Currency,
                Status = new OrderStatus(OrderStatus.OrderStatusValue.Created),
                TotalAmount = planPrice.Price,
                UserId = userId,
                Type = new OrderType(OrderType.OrderTypeValue.Subscription),
                IsDeleted = false
            };
        }

        public void PayOrder(Order order)
        {
            order.Status.Status = OrderStatus.OrderStatusValue.Payed;
        }
    }
}
