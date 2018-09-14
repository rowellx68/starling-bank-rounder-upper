using System;

namespace StarlingBank.Contracts.Webhook
{
    /// <summary>
    /// Starling Bank: payload content.
    /// </summary>
    public class TransactionContent
    {
        public string Class { get; set; }

        public Guid TransactionUid { get; set; }

        public decimal Amount { get; set; }

        public string SourceCurrency { get; set; }

        public decimal SourceAmount { get; set; }

        public Guid MerchantUid { get; set; }

        public Guid MerchantLocationUid { get; set; }

        public Guid EventUid { get; set; }

        public Guid CategoryUid { get; set; }

        public string Type { get; set; }

        public string ForCustomer { get; set; }
    }
}