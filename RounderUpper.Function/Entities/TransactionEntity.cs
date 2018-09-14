using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace RounderUpper.Function.Entities
{
    public class TransactionEntity : TableEntity
    {
        public TransactionEntity(Guid accountHolderUid, Guid transactionUid)
        {
            this.PartitionKey = $"{accountHolderUid}";
            this.RowKey = $"{transactionUid}";
        }

        public TransactionEntity() { }

        public long Amount { get; set; }
    }
}