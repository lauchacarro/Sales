using System;

using Abp.Application.Services.Dto;
using Abp.AutoMapper;

using Sales.Domain.Entities.Subscriptions;

namespace Sales.Application.Dtos.Subscriptions
{
    [AutoMap(typeof(SubscriptionCycle))]
    public class SubscriptionCycleDto : EntityDto<Guid>
    {
        public Guid SubscriptionId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public string Status { get; set; }
    }
}
