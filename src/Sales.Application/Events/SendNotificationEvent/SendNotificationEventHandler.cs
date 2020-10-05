using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus.Handlers;

using AutoMapper;

using Sales.Application.Dtos.Notifications;
using Sales.Domain.Entities.Notifications;
using Sales.Domain.Options;
using Sales.Domain.Services.Abstracts;

namespace Sales.Application.Events.SendNotificationEvent
{
    public class SendNotificationEventHandler : IAsyncEventHandler<SendNotificationEventData>, ITransientDependency
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Notification, Guid> _noticationRepository;
        private readonly INotificationDomainService _notificationDomainService;
        private readonly IClientOptions _clientOptions;
        private readonly HttpClient _httpClient;

        public SendNotificationEventHandler(IMapper mapper,
                                            IRepository<Notification, Guid> noticationRepository,
                                            INotificationDomainService notificationDomainService,
                                            IClientOptions clientOptions,
                                            IHttpClientFactory httpClientFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _noticationRepository = noticationRepository ?? throw new ArgumentNullException(nameof(noticationRepository));
            _notificationDomainService = notificationDomainService ?? throw new ArgumentNullException(nameof(notificationDomainService));
            _clientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
            _httpClient = httpClientFactory.CreateClient() ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        [UnitOfWork]
        public async Task HandleEventAsync(SendNotificationEventData eventData)
        {
            Notification notification = _noticationRepository.Get(eventData.NotificationId);

            NotificationDto notificationDto = _mapper.Map<NotificationDto>(notification);

            HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync(_clientOptions.NotificactionUrl, notificationDto);

            if (httpResponse.IsSuccessStatusCode)
            {
                _noticationRepository.Delete(notification);
            }
            else
            {
                _notificationDomainService.AddAttempt(notification);

                _noticationRepository.Update(notification);
            }
        }
    }
}
