using System;
using StarlingBank.Contracts.Common;

namespace StarlingBank.Contracts.SavingsGoals
{
    public class SavingsGoal
    {
        public Guid Uid { get; set; }

        public string  Name { get; set; }

        public CurrencyAndAmount TotalSaved { get; set; }
    }
}