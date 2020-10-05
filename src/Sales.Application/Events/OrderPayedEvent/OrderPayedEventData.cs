
using Abp.Events.Bus.Entities;

using Sales.Domain.Entities.Orders;

namespace Sales.Application.Events.OrderPayedEvent
{
    public class OrderPayedEventData : EntityEventData<Order>
    {
        public OrderPayedEventData(Order order) : base(order)
        {

        }
    }
}
