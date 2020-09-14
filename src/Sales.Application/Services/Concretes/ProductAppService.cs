using System;

using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Json;
using Abp.ObjectMapping;
using Abp.UI;

using FluentValidation.Results;

using Sales.Application.Dtos.Plans;
using Sales.Application.Dtos.Products;
using Sales.Application.Services.Abstracts;
using Sales.Application.Validators;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Products;
using Sales.Domain.Services.Abstracts;

namespace Sales.Application.Services.Concretes
{
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<Plan, Guid> _planRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IProductDomainService _productDomainService;
        private readonly IPlanDomainService _planDomainService;

        public ProductAppService(IRepository<Product, Guid> productRepository, IRepository<Plan, Guid> planRepository, IObjectMapper objectMapper, IProductDomainService productDomainService, IPlanDomainService planDomainService)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            _objectMapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
            _productDomainService = productDomainService ?? throw new ArgumentNullException(nameof(productDomainService));
            _planDomainService = planDomainService ?? throw new ArgumentNullException(nameof(planDomainService));
        }

        public object CreateProductPlan(CreateProductPlanInput input)
        {
            Logger.Info("Start CreateProductPlan");

            CreateProductPlanValidator validator = new CreateProductPlanValidator(_productRepository);

            ValidationResult resultValidator = validator.Validate(input);

            if (!resultValidator.IsValid)
            {
                throw new UserFriendlyException(resultValidator.Errors.ToJsonString());
            }

            var product = _objectMapper.Map<Product>(input);

            _productDomainService.ActiveProduct(product);

            _productRepository.Insert(product);

            var plan = _objectMapper.Map<Plan>(input);

            _planDomainService.ActivePlan(plan);
            _planDomainService.LinkPlanWithProduct(plan, product);

            _planRepository.Insert(plan);

            Logger.Info("Finish CreateProductPlan");

            return new { Product = _objectMapper.Map<ProductDto>(product), Plan = _objectMapper.Map<PlanDto>(plan) };
        }
    }
}
