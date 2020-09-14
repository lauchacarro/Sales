using System.Collections.Generic;

using Abp.Domain.Values;

namespace Sales.Domain.ValueObjects
{
    public class Currency : ValueObject
    {
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
    }
}
