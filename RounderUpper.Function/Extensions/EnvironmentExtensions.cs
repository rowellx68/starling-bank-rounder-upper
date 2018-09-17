using System;
using RounderUpper.Function.Utilities;

namespace RounderUpper.Function.Extensions
{
    internal static class EnvironmentExtensions
    {
        /// <summary>
        /// Get a specified string environment variable.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetEnvString(string key)
        {
            Guard.AgainstNullOrWhitespaceArgument(nameof(key), key);

            return Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
        }

        /// <summary>
        /// Get a specified int environment variable.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetEnvInt(string key)
        {
            Guard.AgainstNullOrWhitespaceArgument(nameof(key), key);

            var intString = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
            int.TryParse(intString, out var value);

            return value;
        }
    }
}