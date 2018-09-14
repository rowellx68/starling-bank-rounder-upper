using System.Runtime.Serialization;

namespace StarlingBank.Contracts.Common
{
    public class CurrencyAndAmount
    {
        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "minorUnits")]
        public long MinorUnits { get; set; }
    }
}