using System;
using System.Net.Http;
using System.Net.Http.Json;

using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;

using AutoMapper;

using Sales.Application.Dtos.Notifications;
using Sales.Domain.Entities.Notifications;
using Sales.Domain.Options;
using Sales.Domain.Services.Abstracts;
using Sales.Domain.ValueObjects.Notifications;

namespace Sales.Application.Events
{
    public class UpdatedNotificationEventHandler : IEventHandler<EntityUpdatedEventData<Notification>>, ITransientDependency
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Notification, Guid> _noticationRepository;
        private readonly INotificationDomainService _notificationDomainService;
        private readonly IClientOptions _clientOptions;
        private readonly HttpClient _httpClient;

        public UpdatedNotificationEventHandler(IMapper mapper,
                                            IRepository<Notification, Guid> noticationRepository,
                                            INotificationDomainService notificationDomainService,
                                            IClientOptions clientOptions,
                                            IUnitOfWorkManager unitOfWorkManager,
                                            IHttpClientFactory httpClientFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _noticationRepository = noticationRepository ?? throw new ArgumentNullException(nameof(noticationRepository));
            _notificationDomainService = notificationDomainService ?? throw new ArgumentNullException(nameof(notificationDomainService));
            _unitOfWorkManager = unitOfWorkManager ?? throw new ArgumentNullException(nameof(unitOfWorkManager));
            _clientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
            _httpClient = httpClientFactory.CreateClient() ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public void HandleEvent(EntityUpdatedEventData<Notification> eventData)
        {
            if (eventData?.Entity != null)
            {
                switch (eventData.Entity.Status.Status)
                {
                    case NotificationStatus.NotificationStatusValue.Processing:
                        {
                            using var unitOfWork = _unitOfWorkManager.Begin();

                            NotificationDto notificationDto = _mapper.Map<NotificationDto>(eventData.Entity);

                            HttpResponseMessage httpResponse = _httpClient.PostAsJsonAsync(_clientOptions.NotificactionUrl, notificationDto).GetAwaiter().GetResult();

                            if (httpResponse.IsSuccessStatusCode)
                            {
                                _noticationRepository.Delete(eventData.Entity);
                            }
                            else
                            {
                                _notificationDomainService.AddAttempt(eventData.Entity);

                                _noticationRepository.Update(eventData.Entity);
                            }

                            unitOfWork.Complete();

                            break;
                        }
                }
            }
        }

    }
}
