using System;
using NSubstitute;
using RounderUpper.Function.Extensions;
using RounderUpper.Function.Utilities;
using Xunit;

namespace RounderUpper.Function.Tests.Extensions
{
    public class EnvironmentExtensionsTests
    {
        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(Guard.EmptyStringException))]
        [InlineData(" ", typeof(Guard.WhitespaceException))]
        public void GetEnvString_KeyNullEmptyWhitespace_ThrowsException(string val, Type exception)
        {
            Assert.Throws(exception, () => EnvironmentExtensions.GetEnvString(val));
        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(Guard.EmptyStringException))]
        [InlineData(" ", typeof(Guard.WhitespaceException))]
        public void GetEnvInt_KeyNullEmptyWhitespace_ThrowsException(string val, Type exception)
        {
            Assert.Throws(exception, () => EnvironmentExtensions.GetEnvInt(val));
        }
    }
}