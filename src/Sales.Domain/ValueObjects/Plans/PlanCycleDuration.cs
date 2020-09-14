using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Plans
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class PlanCycleDuration : ValueObject
    {
        public PlanCycleDuration(PlanCycleDurationValue duration)
        {
            Duration = duration;
        }

        public PlanCycleDuration()
        {
        }

        public enum PlanCycleDurationValue
        {
            Daily,
            Weekly,
            TwoWeeks,
            Monthly,
            Annually
        }

        public PlanCycleDurationValue Duration { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Duration;
        }

        private string GetDebuggerDisplay()
        {
            return Duration.ToString();
        }
    }
}
