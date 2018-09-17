namespace StarlingBank.Contracts.Webhook
{
    /// <summary>
    /// Starling Bank: Identifier of the webhook type that triggered the call.
    /// </summary>
    public class WebhookTypes
    {
        public const string InterestCharge = "INTEREST_CHARGE";

        public const string ScheduledPaymentCancelled = "SCHEDULED_PAYMENT_CANCELLED";

        public const string ScheduledPaymentInsufficientFunds = "SCHEDULED_PAYMENT_INSUFFICIENT_FUNDS";

        public const string TransactionCard = "TRANSACTION_CARD";

        public const string TransactionCashWithdrawal = "TRANSACTION_CASH_WITHDRAWAL";

        public const string TransactionMobileWallet = "TRANSACTION_MOBILE_WALLET";

        public const string TransactionDirectCredit = "TRANSACTION_DIRECT_CREDIT";

        public const string TransactionDirectDebit = "TRANSACTION_DIRECT_DEBIT";

        public const string TransactionDirectDebitInsufficientFunds = "TRANSACTION_DIRECT_DEBIT_INSUFFICIENT_FUNDS";

        public const string TransactionDirectDebitDispute = "TRANSACTION_DIRECT_DEBIT_DISPUTE";

        public const string TransactionFasterPaymentIn = "TRANSACTION_FASTER_PAYMENT_IN";

        public const string TransactionFasterPaymentOut = "TRANSACTION_FASTER_PAYMENT_OUT";

        public const string TransactionFasterPaymentReversal = "TRANSACTION_FASTER_PAYMENT_REVERSAL";

        public const string TransactionInterestPayment = "TRANSACTION_INTEREST_PAYMENT";

        public const string TransactionInternalTransfer = "TRANSACTION_INTERNAL_TRANSFER";

        public const string TransactionNostroDeposit = "TRANSACTION_NOSTRO_DEPOSIT";

        public const string TransactionInterestWaivedDeposit = "TRANSACTION_INTEREST_WAIVED_DEPOSIT";

        public const string TransactionStripeFunding = "TRANSACTION_STRIPE_FUNDING";

        public const string TransactionDeclinedInsufficientFunds = "TRANSACTION_DECLINED_INSUFFICIENT_FUNDS";

        public const string TransactionAuthDeclined = "TRANSACTION_AUTH_DECLINED";

        public const string TransactionAuthPartialReversal = "TRANSACTION_AUTH_PARTIAL_REVERSAL";

        public const string TransactionAuthFullReversal = "TRANSACTION_AUTH_FULL_REVERSAL";
    }
}