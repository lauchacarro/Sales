
using Abp.Events.Bus.Entities;

using Sales.Domain.Entities.Orders;

namespace Sales.Application.Events.Orders.OrderSubscriptionPayedEvent
{
    public class OrderSubscriptionPayedEventData : EntityEventData<Order>
    {
        public OrderSubscriptionPayedEventData(Order order) : base(order)
        {

        }
    }
}
