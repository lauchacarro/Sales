using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Plans
{
    public class PlanCycleDuration : ValueObject
    {
        public enum PlanCycleDurationValue
        {
            Daily,
            Weekly,
            TwoWeeks,
            Monthy,
            Anualy
        }

        public PlanCycleDurationValue Duration { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Duration;
        }
    }
}
