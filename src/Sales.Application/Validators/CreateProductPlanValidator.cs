
using System;

using Abp.Domain.Repositories;

using FluentValidation;

using Sales.Application.Dtos.Products;
using Sales.Application.Extensions;
using Sales.Domain.Entities.Products;
using Sales.Domain.ValueObjects;
using Sales.Domain.ValueObjects.Plans;

namespace Sales.Application.Validators
{
    public class CreateProductPlanValidator : AbstractValidator<CreateProductPlanInput>
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public CreateProductPlanValidator(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PlanType).IsEnumName(typeof(PlanType.PlanTypeValue), true);
            RuleFor(x => x.Duration).IsEnumName(typeof(PlanCycleDuration.PlanCycleDurationValue), true);
            RuleFor(x => x.Currency).IsEnumName(typeof(Currency.CurrencyValue), true);
            RuleFor(x => x.Name).Must(BeUniqueName).WithMessage("Ya existe un Producto con ese nombre");
        }

        private bool BeUniqueName(string name) => _productRepository.FirstOrDefault(x => x.Name == name).IsNull();
    }
}
