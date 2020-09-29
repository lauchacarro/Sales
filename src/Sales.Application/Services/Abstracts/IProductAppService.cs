
using System;
using System.Collections.Generic;

using Abp.Application.Services;

using Microsoft.AspNetCore.Mvc;

using Sales.Application.Dtos.Products;

namespace Sales.Application.Services.Abstracts
{
    public interface IProductAppService : IApplicationService
    {
        [HttpPost]
        ProductDto CreateProductSale(CreateProductSaleInput input);

        [HttpGet]
        IEnumerable<ProductDto> GetProducts();

        [HttpGet]
        IEnumerable<ProductDto> GetProductPlans();

        [HttpGet]
        IEnumerable<ProductDto> GetProductSales();

        [HttpGet]
        ProductDto GetProduct(Guid id);

        [HttpDelete]
        ProductDto DeleteProduct(Guid id);
    }
}
