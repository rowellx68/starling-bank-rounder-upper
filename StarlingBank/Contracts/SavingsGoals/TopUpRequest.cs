using System.Runtime.Serialization;
using StarlingBank.Contracts.Common;

namespace StarlingBank.Contracts.SavingsGoals
{
    public class TopUpRequest
    {
        [DataMember(Name = "amount")]
        public CurrencyAndAmount Amount { get; set; }
    }
}