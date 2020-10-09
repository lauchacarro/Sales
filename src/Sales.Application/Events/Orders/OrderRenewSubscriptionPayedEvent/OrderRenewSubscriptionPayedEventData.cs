using System;
using System.Collections.Generic;
using System.Text;

using Abp.Events.Bus.Entities;

using Sales.Domain.Entities.Orders;

namespace Sales.Application.Events.Orders.OrderRenewSubscriptionPayedEvent
{
    public class OrderRenewSubscriptionPayedEventData : EntityEventData<Order>
    {
        public OrderRenewSubscriptionPayedEventData(Order order) : base(order)
        {

        }
    }
}
