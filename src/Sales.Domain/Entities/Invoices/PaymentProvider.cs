using System.Collections.Generic;
using System.Diagnostics;

using Abp.Domain.Values;

namespace Sales.Domain.Entities.Invoices
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class PaymentProvider : ValueObject
    {
        public PaymentProvider()
        {
        }

        public PaymentProvider(PaymentProviderValue provider)
        {
            Provider = provider;
        }

        public enum PaymentProviderValue
        {
            Paypal,
            Mobbex
        }

        public PaymentProviderValue Provider { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Provider;
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
