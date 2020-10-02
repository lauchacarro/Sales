
using System;

using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;

using Sales.Domain.Entities.Notifications;
using Sales.Domain.Services.Abstracts;
using Sales.Domain.ValueObjects.Notifications;

namespace Sales.Application.Events
{

    public class CreatedNotificationEventHandler : IEventHandler<EntityCreatedEventData<Notification>>, ITransientDependency
    {
        private readonly IRepository<Notification, Guid> _noticationRepository;
        private readonly INotificationDomainService _notificationDomainService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CreatedNotificationEventHandler(IRepository<Notification, Guid> noticationRepository,
                                            IUnitOfWorkManager unitOfWorkManager,
                                            INotificationDomainService notificationDomainService)
        {
            _noticationRepository = noticationRepository ?? throw new ArgumentNullException(nameof(noticationRepository));
            _unitOfWorkManager = unitOfWorkManager ?? throw new ArgumentNullException(nameof(unitOfWorkManager));
            _notificationDomainService = notificationDomainService ?? throw new ArgumentNullException(nameof(notificationDomainService));
        }

        public void HandleEvent(EntityCreatedEventData<Notification> eventData)
        {
            Notification notification = _noticationRepository.FirstOrDefault(x => x.Id == eventData.Entity.Id);

            if (notification != null)
            {
                switch (notification.Status.Status)
                {
                    case NotificationStatus.NotificationStatusValue.Created:
                        using (var unitOfWork = _unitOfWorkManager.Begin())
                        {
                            _notificationDomainService.Processing(notification);
                            _noticationRepository.Update(notification);
                            unitOfWork.Complete();
                        }


                        break;

                }
            }
        }
    }
}
