using System;

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
            return Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
        }

        /// <summary>
        /// Get a specified int environment variable.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetEnvInt(string key)
        {
            var intString = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
            int.TryParse(intString, out var value);

            return value;
        }
    }
}