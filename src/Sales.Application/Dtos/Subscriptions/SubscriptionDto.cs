using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Subscriptions;

namespace Sales.Application.Dtos.Subscriptions
{
    [AutoMap(typeof(Subscription))]
    public class SubscriptionDto : EntityDto<Guid>
    {
        public Guid PlanId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
