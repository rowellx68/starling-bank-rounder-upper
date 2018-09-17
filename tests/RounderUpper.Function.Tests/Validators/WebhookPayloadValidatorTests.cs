using System;
using RounderUpper.Function.Utilities;
using RounderUpper.Function.Validators;
using Xunit;

namespace RounderUpper.Function.Tests.Validators
{
    public class WebhookPayloadValidatorTests
    {
        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(Guard.EmptyStringException))]
        [InlineData(" ", typeof(Guard.WhitespaceException))]
        public void Validate_NullEmptyWhitespaceSignature_ThrowsException(string val, Type exception)
        {
            Assert.Throws(exception, () => WebhookPayloadValidator.Validate(val, "secret", "{}"));
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(Guard.EmptyStringException))]
        [InlineData(" ", typeof(Guard.WhitespaceException))]
        public void Validate_NullEmptyWhitespaceSecret_ThrowsException(string val, Type exception)
        {
            Assert.Throws(exception, () => WebhookPayloadValidator.Validate("signature", val, "{}"));
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(Guard.EmptyStringException))]
        [InlineData(" ", typeof(Guard.WhitespaceException))]
        public void Validate_NullEmptyWhitespaceJson_ThrowsException(string val, Type exception)
        {
            Assert.Throws(exception, () => WebhookPayloadValidator.Validate("signature", "secret", val));
        }

        [Fact]
        public void Validate_CorrectSecret_ReturnsValid()
        {
            // Arrange
            const string secret = "ded6ca53-64c1-4deb-b684-83eea6f8301d";
            const string signature =
                "9wLF3xAfnzyB4ilHecbxOdbf9l+q+KL+NxRZhRxI/LWUFVWNcgV7gJV1lgjVpAaqqY/zBb+hhfpDeFY7aoE+Qg==";

            // Act
            var (_, valid) = WebhookPayloadValidator.Validate(signature, secret, "{}");

            // Assert
            Assert.True(valid);
        }

        [Fact]
        public void Validate_IncorrectSecret_ReturnsInvalid()
        {
            // Arrange
            const string secret = "wrong-secret";
            const string signature =
                "9wLF3xAfnzyB4ilHecbxOdbf9l+q+KL+NxRZhRxI/LWUFVWNcgV7gJV1lgjVpAaqqY/zBb+hhfpDeFY7aoE+Qg==";

            // Act
            var (_, valid) = WebhookPayloadValidator.Validate(signature, secret, "{}");

            // Assert
            Assert.False(valid);
        }
    }
}