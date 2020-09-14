using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Plans
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class PlanStatus : ValueObject
    {
        public PlanStatus(PlanStatusValue status)
        {
            Status = status;
        }

        public PlanStatus()
        {
        }

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

        private string GetDebuggerDisplay()
        {
            return Status.ToString();
        }
    }
}
