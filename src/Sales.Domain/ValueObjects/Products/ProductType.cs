using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Products
{
    public class ProductType : ValueObject
    {
        public enum ProductTypeValue
        {
            Sale,
            Plan
        }
        public ProductTypeValue Type { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
        }
    }
}
