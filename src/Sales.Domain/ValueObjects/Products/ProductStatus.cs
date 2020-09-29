using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Products
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
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
            Created,
            Active,
            Canceled
        }
        public ProductStatusValue Status { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Status;
        }

        private string GetDebuggerDisplay()
        {
            return Status.ToString();
        }
    }
}
