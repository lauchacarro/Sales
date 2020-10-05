using System;
using System.Collections.Generic;
using System.Linq;

using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Sales.Application.Dtos.Plans;
using Sales.Application.Extensions;
using Sales.Application.Services.Abstracts;
using Sales.Domain.Entities.Orders;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Repositories;
using Sales.Domain.Services.Abstracts;

namespace Sales.Application.Services.Concretes
{
    public class PlanAppService : ApplicationService, IPlanAppService
    {
        private readonly IRepository<Plan, Guid> _planRepository;
        private readonly IPlanDomainService _planDomainService;
        private readonly IPlanPriceRepository _planPriceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IObjectMapper _mapper;

        public PlanAppService(IRepository<Plan, Guid> planRepository,
                              IPlanDomainService planDomainService,
                              IPlanPriceRepository planPriceRepository,
                              IOrderRepository orderRepository,
                              IObjectMapper mapper)
        {
            _planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            _planDomainService = planDomainService ?? throw new ArgumentNullException(nameof(planDomainService));
            _planPriceRepository = planPriceRepository ?? throw new ArgumentNullException(nameof(planPriceRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public PlanDto CreatePlan(CreatePlanInput input)
        {
            Plan plan = _mapper.Map<Plan>(input);
            _planDomainService.ActivePlan(plan);
            _planRepository.Insert(plan);

            return _mapper.Map<PlanDto>(plan);
        }

        [HttpPost]
        public PlanPriceDto AddPrice(CreatePlanPriceInput input)
        {
            PlanPrice planPrice = _mapper.Map<PlanPrice>(input);

            PlanPrice currentPlanPrice = _planPriceRepository.GetByPlan(input.PlanId, planPrice.Currency);
            if (currentPlanPrice.IsNotNull())
            {
                throw new UserFriendlyException("Un plan no puede tener 2 precios de la misma moneda.");
            }

            _planPriceRepository.Insert(planPrice);

            return _mapper.Map<PlanPriceDto>(planPrice);
        }

        public IEnumerable<PlanPriceDto> Get(Guid id)
        {
            Plan plan = _planRepository.GetAllIncluding(x => x.PlanPrices).Single(x => x.Id == id);
            IEnumerable<PlanPrice> planPrices = _planPriceRepository.GetByPlan(plan.Id);

            PlanDto planDto = _mapper.Map<PlanDto>(plan);

            return planPrices.Select(x =>
            {
                return new PlanPriceDto
                {
                    Id = x.Id,
                    Plan = planDto,
                    PlanId = planDto.Id,
                    Currency = x.Currency.Code.ToString(),
                    Price = x.Price
                };
            });
        }

        public PlanPriceDto GetByOrder(Guid orderId)
        {
            Order order = _orderRepository.Get(orderId);

            PlanPrice planprice = _planPriceRepository.GetByOrder(order);

            PlanPriceDto planDto = _mapper.Map<PlanPriceDto>(planprice);

            return planDto;
        }

        public IEnumerable<PlanDto> GetAllPlans()
        {
            return _planRepository.GetAll().AsNoTracking()
                        .Select(x => _mapper.Map<PlanDto>(x))
                        .ToList();
        }
    }
}
