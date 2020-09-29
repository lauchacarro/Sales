using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Products
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class ProductType : ValueObject
    {
        public ProductType(ProductTypeValue type)
        {
            Type = type;
        }

        public ProductType()
        {
        }

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

        private string GetDebuggerDisplay()
        {
            return Type.ToString();
        }
    }
}
