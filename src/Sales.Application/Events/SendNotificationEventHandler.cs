
using System;
using System.Net.Http;
using System.Net.Http.Json;

using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;

using AutoMapper;

using Sales.Application.Dtos.Notifications;
using Sales.Domain.Entities.Notifications;
using Sales.Domain.Options;
using Sales.Domain.Services.Abstracts;

namespace Sales.Application.Events
{
    public class SendNotificationEventHandler : IEventHandler<EntityChangedEventData<Notification>>, ITransientDependency
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Notification, Guid> _noticationRepository;
        private readonly INotificationDomainService _notificationDomainService;
        private readonly INotificationOptions _notificationOptions;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly HttpClient _httpClient;

        public SendNotificationEventHandler(IMapper mapper,
                                            IRepository<Notification, Guid> noticationRepository,
                                            INotificationDomainService notificationDomainService,
                                            INotificationOptions notificationOptions,
                                            IBackgroundJobManager backgroundJobManager,
                                            IHttpClientFactory httpClientFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _noticationRepository = noticationRepository ?? throw new ArgumentNullException(nameof(noticationRepository));
            _notificationDomainService = notificationDomainService ?? throw new ArgumentNullException(nameof(notificationDomainService));
            _backgroundJobManager = backgroundJobManager ?? throw new ArgumentNullException(nameof(backgroundJobManager));
            _notificationOptions = notificationOptions ?? throw new ArgumentNullException(nameof(notificationOptions));
            _httpClient = httpClientFactory.CreateClient() ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public void HandleEvent(EntityChangedEventData<Notification> eventData)
        {
            Notification notification = _noticationRepository.FirstOrDefault(x => x.Id == eventData.Entity.Id);

            if (notification != null)
            {
                NotificationDto notificationDto = _mapper.Map<NotificationDto>(notification);

                HttpResponseMessage httpResponse = _httpClient.PostAsJsonAsync(_notificationOptions.Url, notificationDto).GetAwaiter().GetResult();

                if (httpResponse.IsSuccessStatusCode)
                {
                    _noticationRepository.Delete(notification);
                }
                else
                {
                    _notificationDomainService.AddAttempt(notification);
                    if (notification.Attempts > 3)
                    {
                        _noticationRepository.Delete(notification);

                    }
                    else
                    {
                        _noticationRepository.Update(notification);
                    }
                }
            }
        }
    }
}
