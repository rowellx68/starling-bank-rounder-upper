using System;

namespace RounderUpper.Function.Extensions
{
    internal static class EnvironmentExtensions
    {
        public static string GetEnvString(string key)
        {
            return Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
        }

        public static int GetEnvInt(string key)
        {
            var intString = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
            int.TryParse(intString, out var value);

            return value;
        }
    }
}