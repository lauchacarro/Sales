using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class Currency : ValueObject
    {
        public Currency(CurrencyValue code)
        {
            Code = code;
        }

        public Currency()
        {
        }

        public enum CurrencyValue
        {
            ARS,
            USD
        }

        public CurrencyValue Code { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Code;
        }

        private string GetDebuggerDisplay()
        {
            return Code.ToString();
        }
    }
}
