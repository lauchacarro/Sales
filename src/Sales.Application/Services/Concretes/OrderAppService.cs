using System;
using System.Collections.Generic;
using System.Linq;

using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.UI;

using AutoMapper;

using Sales.Application.Dtos.Orders;
using Sales.Application.Events.OrderPayedEvent;
using Sales.Application.Services.Abstracts;
using Sales.Domain.Entities.Orders;
using Sales.Domain.ValueObjects.Orders;

namespace Sales.Application.Services.Concretes
{
    public class OrderAppService : ApplicationService, IOrderAppService
    {
        private readonly IRepository<Order, Guid> _repository;
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;

        public OrderAppService(IRepository<Order, Guid> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _eventBus = EventBus.Default;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public OrderDto UpdateOrder(UpdateOrderInput input)
        {
            Order order = _repository.Single(x => x.Id == input.Id);

            if (order.Status.Status == OrderStatus.OrderStatusValue.Payed)
            {
                throw new UserFriendlyException("No se puede editar una orden que ya fue Pagada.");
            }

            if (order.Status.Status == OrderStatus.OrderStatusValue.Canceled)
            {
                throw new UserFriendlyException("No se puede editar una orden que ya fue Camcelada.");
            }

            order = _mapper.Map<UpdateOrderInput, Order>(input, order);

            _repository.Update(order);

            if (order.Status.Status == OrderStatus.OrderStatusValue.Payed)
            {
                _eventBus.Trigger(new OrderPayedEventData(order));
            }

            return _mapper.Map<OrderDto>(order);
        }

        public OrderDto GetOrder(Guid id)
        {
            return _mapper.Map<OrderDto>(_repository.Single(x => x.Id == id));
        }

        public IEnumerable<OrderDto> GetOrders()
        {
            return _repository.GetAll().Select(x => _mapper.Map<OrderDto>(x)).ToList();
        }
    }
}
