using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Plans
{
    public class PlanType : ValueObject
    {
        public enum PlanTypeValue
        {
            Normal
        }

        public PlanTypeValue Type { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
        }
    }
}
