using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Subscriptions;

namespace Sales.Application.Dtos.Subscriptions
{
    [AutoMap(typeof(SubscriptionCycleOrder))]
    public class SubscriptionCycleOrderDto : EntityDto<Guid>
    {
        public Guid SubscriptionCycleId { get; set; }
        public Guid OrderId { get; set; }
    }
}
