using System;
using System.Diagnostics;
using System.Globalization;

namespace RounderUpper.Function.Utilities
{
    internal static class Guard
    {
        /// <summary>
        ///     Guards against a null argument.
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument.</typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="argument" /> is <c>null</c>.</exception>
        /// <remarks>
        ///     <typeparamref name="TArgument" /> is restricted to reference types to avoid boxing of value type objects.
        /// </remarks>
        [DebuggerStepThrough]
        public static void AgainstNullArgument<TArgument>(string parameterName, [ValidatedNotNull] TArgument argument)
            where TArgument : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(
                    parameterName,
                    string.Format(CultureInfo.InvariantCulture, "{0} is null.", parameterName));
            }
        }

        /// <summary>
        ///     Guards against a null or whitespace string argument.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="argument" /> is <c>null</c>.</exception>
        /// <exception cref="WhitespaceException"><paramref name="argument" /> is not empty but only contains <c>whitespace</c>.</exception>
        [DebuggerStepThrough]
        public static void AgainstNullOrWhitespaceArgument(string parameterName, [ValidatedNotNull] string argument)
        {
            AgainstNullOrEmptyArgument(parameterName, argument);

            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new WhitespaceException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "String is not empty but only contains whitespace {0}",
                        parameterName));
            }
        }

        /// <summary>
        ///     Guards against a null of empty string argument.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="argument">The argument.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="argument" /> is <c>null</c>.</exception>
        [DebuggerStepThrough]
        public static void AgainstNullOrEmptyArgument(string parameterName, [ValidatedNotNull] string argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(
                    parameterName,
                    string.Format(CultureInfo.InvariantCulture, "{0} is null.", parameterName));
            }

            if (argument == string.Empty)
            {
                throw new EmptyStringException(
                    string.Format(CultureInfo.InvariantCulture, "String cannot be empty {0}", parameterName));
            }
        }

        [DebuggerStepThrough]
        public static void AgainstDefaultValue<T>(string parameterName, T argument) where T : struct
        {
            var value = default(T);

            if (argument.Equals(value))
            {
                throw new DefaultValueException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Argument {0} is equal to its default value {1}.",
                        parameterName,
                        value
                    )
                );
            }
        }

        #region CustomExceptions

        public class EmptyStringException : Exception
        {
            public EmptyStringException(string message) : base(message)
            {
            }
        }

        public class WhitespaceException : Exception
        {
            public WhitespaceException(string message) : base(message)
            {
            }
        }

        public class DefaultValueException : Exception
        {
            public DefaultValueException(string message) : base(message)
            {
            }
        }

        #endregion

        #region Attributes

        /// <summary>
        ///     When applied to a parameter,
        ///     this attribute provides an indication to code analysis that the argument has been null checked.
        /// </summary>
        private sealed class ValidatedNotNullAttribute : Attribute
        {
        }

        #endregion
    }
}