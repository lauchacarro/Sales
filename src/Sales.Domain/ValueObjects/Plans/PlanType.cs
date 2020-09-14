using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects.Plans
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class PlanType : ValueObject
    {
        public PlanType(PlanTypeValue type)
        {
            Type = type;
        }

        public PlanType()
        {
        }

        public enum PlanTypeValue
        {
            Normal
        }

        public PlanTypeValue Type { get; set; }

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
