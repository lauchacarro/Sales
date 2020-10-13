using System;
using System.Collections.Generic;
using System.Text;

using Abp.Events.Bus.Entities;

using Sales.Domain.Entities.Orders;

namespace Sales.Application.Events.Orders.OrderExtraPayedEvent
{
    public class OrderExtraPayedEventData : EntityEventData<Order>
    {
        public OrderExtraPayedEventData(Order order) : base(order)
        {

        }
    }
}
