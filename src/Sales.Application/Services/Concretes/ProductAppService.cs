using System;
using System.Collections.Generic;
using System.Linq;

using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Json;
using Abp.ObjectMapping;
using Abp.UI;

using FluentValidation.Results;

using Sales.Application.Dtos.Products;
using Sales.Application.Services.Abstracts;
using Sales.Application.Validators;
using Sales.Domain.Entities.Plans;
using Sales.Domain.Entities.Products;
using Sales.Domain.Repositories;
using Sales.Domain.Services.Abstracts;
using Sales.Domain.ValueObjects;
using Sales.Domain.ValueObjects.Products;

namespace Sales.Application.Services.Concretes
{
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRepository<ProductSale, Guid> _productSaleRepository;
        private readonly IRepository<ProductSalePrice, Guid> _productSalePriceRepository;
        private readonly IRepository<Plan, Guid> _planRepository;
        private readonly IRepository<PlanPrice, Guid> _planPriceRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IProductDomainService _productDomainService;
        private readonly IPlanDomainService _planDomainService;
        private readonly IPlanPriceDomainService _planPriceDomainService;

        public ProductAppService(IProductRepository productRepository,
                                 IRepository<ProductSale, Guid> productSaleRepository,
                                 IRepository<ProductSalePrice, Guid> productSalePriceRepository,
                                 IRepository<Plan, Guid> planRepository,
                                 IRepository<PlanPrice, Guid> planPriceRepository,
                                 IObjectMapper objectMapper,
                                 IProductDomainService productDomainService,
                                 IPlanDomainService planDomainService,
                                 IPlanPriceDomainService planPriceDomainService)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productSaleRepository = productSaleRepository ?? throw new ArgumentNullException(nameof(productSaleRepository));
            _productSalePriceRepository = productSalePriceRepository ?? throw new ArgumentNullException(nameof(productSalePriceRepository));
            _planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
            _planPriceRepository = planPriceRepository ?? throw new ArgumentNullException(nameof(planPriceRepository));
            _objectMapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
            _productDomainService = productDomainService ?? throw new ArgumentNullException(nameof(productDomainService));
            _planDomainService = planDomainService ?? throw new ArgumentNullException(nameof(planDomainService));
            _planPriceDomainService = planPriceDomainService ?? throw new ArgumentNullException(nameof(planPriceDomainService));
        }

        public ProductDto CreateProductSale(CreateProductSaleInput input)
        {
            Logger.Info("Start CreateProductSale");

            CreateProductSaleValidator validator = new CreateProductSaleValidator(_productRepository);

            ValidationResult resultValidator = validator.Validate(input);

            if (!resultValidator.IsValid)
            {
                throw new UserFriendlyException(resultValidator.Errors.ToJsonString());
            }

            var product = _objectMapper.Map<Product>(input);

            _productDomainService.ActiveProduct(product);

            _productRepository.Insert(product);

            var productSale = _productDomainService.CreateProductSale(product);

            _productSaleRepository.Insert(productSale);

            var currency = _objectMapper.Map<Currency>(input);

            var productSalePrice = _productDomainService.AssingPrice(productSale, input.Price, currency);

            _productSalePriceRepository.InsertOrUpdate(productSalePrice);

            Logger.Info("Finish CreateProductSale");

            return _objectMapper.Map<ProductDto>(product);

        }

        public ProductDto CreateProduct(CreateProductInput input)
        {
            Logger.Info("Start CreateProduct");

            var product = _objectMapper.Map<Product>(input);

            _productDomainService.ActiveProduct(product);

            _productRepository.Insert(product);

            Logger.Info("Finish CreateProduct");

            return _objectMapper.Map<ProductDto>(product);

        }

        public ProductDto DeleteProduct(Guid id)
        {
            Logger.Info("Start DeleteProduct");

            var product = _productRepository.Get(id);

            product.Status = new ProductStatus(ProductStatus.ProductStatusValue.Canceled);

            product = _productRepository.Update(product);

            //TODO: Crear evento para cambiar el estado las entidades relacionadas

            Logger.Info("Finish DeleteProduct");

            return _objectMapper.Map<ProductDto>(product);
        }

        public ProductDto GetProduct(Guid id)
        {
            Logger.Info("Start GetProduct");

            var product = _productRepository.Get(id);

            Logger.Info("Finish GetProduct");
            return _objectMapper.Map<ProductDto>(product);
        }

        public IEnumerable<ProductDto> GetProductPlans()
        {
            return _productRepository.GetProductPlans().Select(product => _objectMapper.Map<ProductDto>(product));
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            return _productRepository.GetAllList().Select(product => _objectMapper.Map<ProductDto>(product));
        }

        public IEnumerable<ProductDto> GetProductSales()
        {
            return _productRepository.GetProductSales().Select(product => _objectMapper.Map<ProductDto>(product));
        }
    }
}
