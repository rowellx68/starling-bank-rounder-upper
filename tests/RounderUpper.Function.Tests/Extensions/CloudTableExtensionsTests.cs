using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using RounderUpper.Function.Extensions;
using NSubstitute;
using NSubstitute.Routing.Handlers;
using Xunit;
using RounderUpper.Function.Utilities;

namespace RounderUpper.Function.Tests.Extensions
{
    public class CloudTableExtensionsTests
    {
        [Fact]
        public async Task GetExistingTransaction_NullTable_ThrowsArgumentNullException()
        {
            // Arrange
            var table = null as CloudTable;

            // Act/Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await table.GetExistingTransaction(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public async Task GetExistingTransaction_EmptyAccountUid_ThrowsDefaultValueException()
        {
            // Act/Assert
            await Assert.ThrowsAsync<Guard.DefaultValueException>(async () =>
                await this.Table.GetExistingTransaction(Guid.Empty, Guid.NewGuid()));
        }

        [Fact]
        public async Task GetExistingTransaction_EmptyTransactionUid_ThrowsDefaultValueException()
        {
            // Act/Assert
            await Assert.ThrowsAsync<Guard.DefaultValueException>(async () =>
                await this.Table.GetExistingTransaction(Guid.NewGuid(), Guid.Empty));
        }

        private CloudTable Table { get; } = Substitute.For<CloudTable>(new Uri("https://fake-link.com"));
    }
}