using System;

using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;

using Sales.Application.Dtos.Products;
using Sales.Application.Services.Abstracts;
using Sales.Domain.Entities.Products;
using Sales.Domain.Services.Abstracts;

namespace Sales.Application.Services.Concretes
{
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IProductDomainService _productDomainService;

        public ProductAppService(IRepository<Product, Guid> productRepository, IObjectMapper objectMapper, IProductDomainService productDomainService)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _objectMapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
            _productDomainService = productDomainService ?? throw new ArgumentNullException(nameof(productDomainService));
        }

        public ProductDto CreateProduct(CreateProductInput input)
        {
            Logger.Info("Start CreateProduct");

            var productEntity = _objectMapper.Map<Product>(input);

            _productDomainService.ActiveProduct(productEntity);

            var product = _productRepository.Insert(productEntity);

            Logger.Info("Finish CreateProduct");

            return _objectMapper.Map<ProductDto>(product);
        }
    }
}
