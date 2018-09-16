using System.Runtime.Serialization;
using StarlingBank.Contracts.Common;

namespace StarlingBank.Contracts.SavingsGoals
{
    public class TopUpRequest
    {
        public CurrencyAndAmount Amount { get; set; }
    }
}