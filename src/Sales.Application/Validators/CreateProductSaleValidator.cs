using System;

using Abp.Domain.Repositories;

using FluentValidation;

using Sales.Application.Dtos.Products;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Products;
using Sales.Domain.ValueObjects;

namespace Sales.Application.Validators
{
    public class CreateProductSaleValidator : AbstractValidator<CreateProductSaleInput>
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public CreateProductSaleValidator(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

            RuleFor(x => x.Name).NotEmpty().Length(3, 10);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Currency).IsEnumName(typeof(Currency.CurrencyValue), true);
            RuleFor(x => x.Name).Must(BeUniqueName).WithMessage("Ya existe un Producto con ese nombre");
        }

        private bool BeUniqueName(string name) => _productRepository.FirstOrDefault(x => x.Name == name).IsNull();
    }
}
