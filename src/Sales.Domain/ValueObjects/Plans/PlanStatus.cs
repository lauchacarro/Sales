using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Plans
{
    public class PlanStatus : ValueObject
    {
        public enum PlanStatusValue
        {
            Created,
            Active,
            Canceled
        }

        public PlanStatusValue Status { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Status;
        }
    }
}
