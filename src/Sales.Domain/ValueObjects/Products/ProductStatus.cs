using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Products
{
    public class ProductStatus : ValueObject
    {
        public ProductStatus(ProductStatusValue status)
        {
            Status = status;
        }

        public ProductStatus()
        {
        }

        public enum ProductStatusValue
        {
            Active,
            Canceled
        }
        public ProductStatusValue Status { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Status;
        }
    }
}
