using System;

using Abp.Application.Services.Dto;

namespace Sales.Application.Dtos.Notifications
{
    public class NotificationDto : EntityDto<Guid>
    {
        public Guid OrderId { get; set; }
        public int Attempts { get; set; }
        public string Type { get; set; }
    }
}
