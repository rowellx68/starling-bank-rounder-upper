using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using RounderUpper.Function.Entities;
using RounderUpper.Function.Utilities;

namespace RounderUpper.Function.Extensions
{
    internal static class CloudTableExtensions
    {
        /// <summary>
        /// Extension method for <see cref="CloudTable"/> to get an existing transaction.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="accountHolderUid"></param>
        /// <param name="transactionUid"></param>
        /// <returns></returns>
        public static async Task<TransactionEntity> GetExistingTransaction(this CloudTable table, Guid accountHolderUid, Guid transactionUid)
        {
            Guard.AgainstNullArgument(nameof(table), table);
            Guard.AgainstDefaultValue(nameof(accountHolderUid), accountHolderUid);
            Guard.AgainstDefaultValue(nameof(transactionUid), transactionUid);

            // TODO: this could get expensive if we keep checking if the table exists
            await table.CreateIfNotExistsAsync();

            var operation = TableOperation.Retrieve<TransactionEntity>($"{accountHolderUid}", $"{transactionUid}");
            var result = await table.ExecuteAsync(operation);

            return result.Result as TransactionEntity;
        }

        /// <summary>
        /// Extension method for <see cref="CloudTable"/> to insert a transaction.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="accountHolderUid"></param>
        /// <param name="transactionUid"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static async Task SaveTransaction(this CloudTable table, Guid accountHolderUid, Guid transactionUid, long amount)
        {
            Guard.AgainstNullArgument(nameof(table), table);

            var entity = new TransactionEntity(accountHolderUid, transactionUid)
            {
                Amount = amount
            };

            var operation = TableOperation.Insert(entity);

            await table.ExecuteAsync(operation);
        }

    }
}