using StarlingBank.Contracts.Common;

namespace StarlingBank.Contracts.SavingsGoals
{
    public class SavingsGoalRequest
    {
        public string Name { get; set; }

        public string Currency { get; set; }

        public CurrencyAndAmount Target { get; set; }

        public string Base64EncodedPhoto { get; set; }
    }
}