using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Abp.Application.Services;
using Abp.Domain.Repositories;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using Sales.Application.Dtos.Orders;
using Sales.Application.Services.Abstracts;
using Sales.Domain.Entities.Orders;

namespace Sales.Application.Services.Concretes
{
    public class OrderAppService : ApplicationService, IOrderAppService
    {
        private readonly IRepository<Order, Guid> _repository;
        private readonly IMapper _mapper;

        public OrderAppService(IRepository<Order, Guid> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
