using System;

namespace StarlingBank.Contracts.Webhook
{
    /// <summary>
    /// Starling Bank: webhook payload object.
    /// </summary>
    public class TransactionPayload
    {
        public Guid WebhookNotificationUid { get; set; }

        public DateTime TimeStamp { get; set; }

        public TransactionContent Content { get; set; }

        public Guid AccountHolderUid { get; set; }

        /// <summary>
        /// String representation of the webhook event type.
        /// </summary>
        /// <see cref="WebhookTypes"/>
        public string WebhookType { get; set; }

        public Guid CustomerUid { get; set; }

        /// <summary>
        /// Deprecated. Use WebhookNotificationUid instead.
        /// </summary>
        public Guid Uid { get; set; }
    }
}