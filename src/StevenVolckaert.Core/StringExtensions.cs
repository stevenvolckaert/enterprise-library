namespace StevenVolckaert
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    ///     Provides extension methods for <see cref="string"/> instances.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Indicates whether the specified string is <c>null</c> or empty.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>
        ///     <c>true</c> if the value parameter is <c>null</c> or an empty string (""); otherwise, false.
        /// </returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        ///     Indicates whether the specified string is <c>null</c>, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>
        ///     <c>true</c> if the <paramref name="value"/> parameter is <c>null</c> or <see cref="string.Empty"/>,
        ///     or if <paramref name="value"/> consists exclusively of white-space characters.
        /// </returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
#if NET35
            if (value == null)
                return true;

            for (int i = 0; i < value.Length; i++)
                if (!char.IsWhiteSpace(value[i]))
                    return false;

            return true;
#else
            return string.IsNullOrWhiteSpace(value);
#endif
        }

        /// <summary>
        ///     Returns the string, or a default value if the string is <c>null</c> or empty.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value this extension method affects.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        ///     The string, or the default value if the string is <c>null</c> or empty.
        /// </returns>
        public static string DefaultIfNullOrEmpty(this string value, string defaultValue)
        {
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        /// <summary>
        ///     Returns the string, or a default value if the string is <c>null</c>, empty, or white space.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value this extension method affects.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        ///     The string, or the default value if the string is <c>null</c>, empty, or white space.
        /// </returns>
        public static string DefaultIfNullOrWhiteSpace(this string value, string defaultValue)
        {
            return value.IsNullOrWhiteSpace() ? defaultValue : value;
        }

        /// <summary>
        ///     Returns the first substring in this string that is delimited by a specified separator.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value.</param>
        /// <param name="separators">A collection of strings that delimit the substrings in this string.</param>
        /// <returns>
        ///     The first substring delimited by one of the specified separators, or <c>null</c>
        ///     if no such substring exists.
        /// </returns>
        public static string FirstFromSplit(this string value, params string[] separators)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            var values = value.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
            return values.FirstOrDefault();
        }

        /// <summary>
        ///     Returns a value that indicates whether the string represents a decimal.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value this extension method affects.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="value"/> represents a decimal; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDecimal(this string value)
        {
            try
            {
                Convert.ToDecimal(value, CultureInfo.CurrentCulture);
                return true;
            }
            catch (ArgumentNullException)
            {
                // The string is null.
                return false;
            }
            catch (FormatException)
            {
                // The string is not formatted as a decimal.
                return false;
            }
            catch (OverflowException)
            {
                // The conversion from string to decimal overflowed. The string is too long to represent a decimal.
                return false;
            }
        }

        /// <summary>
        ///     Returns a value that indicates whether the string represents a 32-bit signed integer.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value this extension method affects.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="value"/> represents a 32-bit signed integer; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInt32(this string value)
        {
            // TODO Replace Regex.IsMatch with Convert.ToInt32(string) call wrapped in a try/catch statement.
            return Regex.IsMatch(value, RegexValidationPatterns.Int32);
        }

        /// <summary>
        ///     Returns a list containing the lines present in a specified string.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value this extension method affects.</param>
        /// <returns>
        ///     A list containing all lines present in <paramref name="value"/>.
        /// </returns>
        public static IList<string> Lines(this string value)
        {
            string line;
            var returnValue = new List<string>();

            using (var stringReader = new StringReader(value))
            {
                while ((line = stringReader.ReadLine()) != null)
                    returnValue.Add(line);
            }

            return returnValue;
        }

        /// <summary>
        ///     Converts the string to a collection of objects by splitting the string
        ///     by a specified array of delimitors.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the returned collection.</typeparam>
        /// <param name="value">The <see cref="string"/> value this extension method affects.</param>
        /// <param name="separator">An array of Unicode characters that delimit the substrings in the string.</param>
        /// <returns>
        ///     The collection.
        /// </returns>
        public static IList<T> ToList<T>(this string value, params char[] separator)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return value.Split(separator, StringSplitOptions.RemoveEmptyEntries).Cast<T>().ToList();
        }

        /// <summary>
        ///     Converts the string representation of a number to its 32-bit signed integer equivalent,
        ///     or returns 0 if the conversion fails.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value containing the number to convert.</param>
        /// <returns>
        ///     The 32-bit signed integer equivalent to the number contained in <paramref name="value"/>,
        ///     or 0 if the conversion failed.
        /// </returns>
        public static int ToInt32(this string value)
        {
            return value.TryToInt32(defaultResult: 0);
        }

        /// <summary>
        ///     Converts the string representation of a number to its 32-bit signed integer equivalent,
        ///     or returns a specified default result if the conversion fails.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value containing the number to convert.</param>
        /// <param name="defaultResult">The default result to return if the conversion fails.</param>
        /// <returns>
        ///     The 32-bit signed integer equivalent to the number contained in <paramref name="value"/>,
        ///     or <paramref name="defaultResult"/> if the conversion failed.
        /// </returns>
        public static int TryToInt32(this string value, int defaultResult)
        {
            return value.ToNullableInt32() ?? defaultResult;
        }

        /// <summary>
        ///     Converts the string representation of a number to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value containing the number to convert.</param>
        /// <returns>
        ///     The 32-bit signed integer equivalent to the number contained in <paramref name="value"/>,
        ///     or <c>null</c> if the conversion failed.
        /// </returns>
        public static int? ToNullableInt32(this string value)
        {
            int returnValue;

            return int.TryParse(value, out returnValue)
                ? (int?)returnValue
                : null;
        }

        /// <summary>
        ///     Converts the string representation of a number to its 64-bit signed integer equivalent,
        ///     or returns 0 if the conversion fails.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value containing the number to convert.</param>
        /// <returns>
        ///     The 64-bit signed integer equivalent to the number contained in <paramref name="value"/>,
        ///     or 0 if the conversion failed.
        /// </returns>
        public static long ToInt64(this string value)
        {
            return value.TryToInt64(defaultResult: 0);
        }

        /// <summary>
        ///     Converts the string representation of a number to its 64-bit signed integer equivalent,
        ///     or returns a specified default result if the conversion fails.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value containing the number to convert.</param>
        /// <param name="defaultResult">The default result to return if the conversion fails.</param>
        /// <returns>
        ///     The 64-bit signed integer equivalent to the number contained in <paramref name="value"/>,
        ///     or <paramref name="defaultResult"/> if the conversion failed.
        /// </returns>
        public static long TryToInt64(this string value, long defaultResult)
        {
            return value.ToNullableInt64() ?? defaultResult;
        }

        /// <summary>
        ///     Converts the string representation of a number to its 64-bit signed integer equivalent.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value containing the number to convert.</param>
        /// <returns>
        ///     The 64-bit signed integer equivalent to the number contained in <paramref name="value"/>,
        ///     or <c>null</c> if the conversion failed.
        /// </returns>
        public static long? ToNullableInt64(this string value)
        {
            long returnValue;

            return long.TryParse(value, out returnValue)
                ? (long?)returnValue
                : null;
        }

        /// <summary>
        ///     Converts the string representation of a number to its System.Decimal equivalent.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value containing the number to convert.</param>
        /// <returns>
        ///     The <see cref="decimal"/> equivalent to the number contained in <paramref name="value"/>,
        ///     or <c>null</c> if the conversion failed.
        /// </returns>
        public static decimal? ToNullableDecimal(this string value)
        {
            decimal returnValue;

            return decimal.TryParse(value, out returnValue)
                ? (decimal?)returnValue
                : null;
        }

        /// <summary>
        ///     Attempts to return a copy of this <see cref="string"/> instance converted to uppercase using
        ///     the casing rules of the invariant culture.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value.</param>
        /// <returns>
        ///     The uppercase equivalent of the string, or <c>null</c> if the string was <c>null</c>.
        /// </returns>
        public static string TryToUpperInvariant(this string value)
        {
            return value == null ? null : value.ToUpperInvariant();
        }

        /// <summary>
        ///     Attempts to remove all leading and trailing white-space characters from a specified string.
        /// </summary>
        /// <param name="value">The <see cref="string"/> to trim.</param>
        /// <returns>
        ///     The string that remains after all white-space characters are removed from
        ///     the start and end of the given string instance, or <c>null</c> if the string was <c>null</c>.
        /// </returns>
        public static string TryTrim(this string value)
        {
            return value == null ? null : value.Trim();
        }

        /// <summary>
        ///     Converts the string to a specified enumeration.
        /// </summary>
        /// <typeparam name="TEnum">The type of <see cref="Enum"/> to parse as.</typeparam>
        /// <param name="value">The <see cref="string"/> value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="value"/> does not map to one of the named constants
        ///     defined in <typeparamref name="TEnum"/>.
        /// </exception>
        public static TEnum ParseAs<TEnum>(this string value)
            where TEnum : struct
        {
            return value.ParseAs<TEnum>(ignoreCase: false);
        }

        /// <summary>
        ///     Converts the string to a specified enumeration. A value specifies whether the operation is
        ///     case-insensitive.
        /// </summary>
        /// <typeparam name="TEnum">The type of <see cref="Enum"/> to parse as.</typeparam>
        /// <param name="value">The <see cref="string"/> value.</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case; <c>false</c> to regard case.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="value"/> does not map to one of the named constants
        ///     defined in <typeparamref name="TEnum"/>.
        /// </exception>
        public static TEnum ParseAs<TEnum>(this string value, bool ignoreCase)
            where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
        }

        /// <summary>
        ///     Converts the string to a specified enumeration, or returns a specified default result if
        ///     the conversion fails.
        /// </summary>
        /// <typeparam name="TEnum">The type of <see cref="Enum"/> to parse as.</typeparam>
        /// <param name="value">The <see cref="string"/> value.</param>
        /// <param name="defaultResult">The default result to return if the conversion fails.</param>
        public static TEnum TryParseAs<TEnum>(this string value, TEnum defaultResult)
            where TEnum : struct
        {
            return value.TryParseAs<TEnum>(defaultResult, ignoreCase: false);
        }

        /// <summary>
        ///     Converts the string to a specified enumeration, or returns a specified default result if
        ///     the conversion fails. A value specified whether the operation is case-insensitive.
        /// </summary>
        /// <typeparam name="TEnum">The type of <see cref="Enum"/> to parse as.</typeparam>
        /// <param name="value">The <see cref="string"/> value.</param>
        /// <param name="defaultResult">The default result to return if the conversion fails.</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case; <c>false</c> to regard case.</param>
        public static TEnum TryParseAs<TEnum>(this string value, TEnum defaultResult, bool ignoreCase)
            where TEnum : struct
        {
            try
            {
                return value.ParseAs<TEnum>(ignoreCase);
            }
            catch (ArgumentException)
            {
                return defaultResult;
            }
            catch (OverflowException)
            {
                return defaultResult;
            }
        }
    }
}
