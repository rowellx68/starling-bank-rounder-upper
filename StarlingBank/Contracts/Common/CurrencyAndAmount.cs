using System.Runtime.Serialization;

namespace StarlingBank.Contracts.Common
{
    public class CurrencyAndAmount
    {
        public string Currency { get; set; }

        public long MinorUnits { get; set; }
    }
}