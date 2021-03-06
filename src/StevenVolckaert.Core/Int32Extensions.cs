﻿namespace StevenVolckaert
{
    using System;
    using System.Globalization;

    /// <summary>
    ///     Provides extension methods for <see cref="int"/> values.
    /// </summary>
    public static class Int32Extensions
    {
        /// <summary>
        ///     Returns a value that indicates whether the <see cref="int"/> value is even.
        /// </summary>
        /// <param name="value">The <see cref="int"/> value.</param>
        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        ///     Returns a value that indicates whether the <see cref="int"/> value is odd.
        /// </summary>
        /// <param name="value">The <see cref="int"/> value.</param>
        public static bool IsOdd(this int value)
        {
            return !value.IsEven();
        }

        /// <summary>
        ///     Returns a string that represents a number of bytes in a human-readable format,
        ///     using the <see cref="UnitOfInformationPrefix.Binary"/> prefix.
        /// </summary>
        /// <param name="value">The <see cref="int"/> value, in bytes.</param>
        /// <returns>
        ///     A human-readable string representation of <paramref name="value"/>, using powers of 1024.
        /// </returns>
        public static string BytesToString(this int value)
        {
            return ((long)value).BytesToString();
        }

        /// <summary>
        ///     Returns a string that represent the <see cref="int"/> value in a human-readable format. 
        /// </summary>
        /// <param name="value">The <see cref="int"/> value, in bytes.</param>
        /// <param name="prefix">
        ///     The prefix to be used in the string representation.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="prefix"/> has an illegal value.
        /// </exception>
        /// <returns>
        ///     A human-readable string representation of <paramref name="value"/>.
        /// </returns>
        public static string BytesToString(this int value, UnitOfInformationPrefix prefix)
        {
            return ((long)value).BytesToString(prefix);
        }

        /// <summary>
        ///     Returns a string that represents the <see cref="int"/> value in a human-readable format.
        /// </summary>
        /// <param name="value">The <see cref="int"/> value, in kibibytes (KiB).</param>
        public static string KibibytesToString(this int value)
        {
            return ((long)value).KibibytesToString();
        }

        /// <summary>
        ///     Returns a string that represents the <see cref="int"/> value in a human-readable format.
        /// </summary>
        /// <param name="value">The <see cref="int"/> value, in kilobytes (kB).</param>
        public static string KilobytesToString(this int value)
        {
            return ((long)value).KilobytesToString();
        }

        /// <summary>
        ///     Returns a string that represents the <see cref="int"/> value in a human-readable format.
        /// </summary>
        /// <param name="value">The <see cref="int"/> value, in mebibytes (MiB).</param>
        public static string MebibytesToString(this int value)
        {
            return ((long)value).MebibytesToString();
        }

        /// <summary>
        ///     Returns a string that represents the <see cref="int"/> value in a human-readable format.
        /// </summary>
        /// <param name="value">The <see cref="int"/> value, in megabytes (MB).</param>
        public static string MegabytesToString(this int value)
        {
            return ((long)value).MegabytesToString();
        }

        /// <summary>
        ///     Returns a string consisting of the <see cref="int"/> value,
        ///     appended with a suffix based on the value.
        /// </summary>
        /// <param name="value">The value to </param>
        /// <param name="singularSuffix">The suffix to use when <paramref name="value"/> equals 1.</param>
        /// <param name="pluralSuffix">The suffix to use when <paramref name="value"/> doesn't equal 1.</param>
        /// <returns>
        ///     A string consisting of <paramref name="value"/>,
        ///     appended with a suffix based on the value it holds.
        /// </returns>
        public static string ToFormattedString(this int value, string singularSuffix, string pluralSuffix)
        {
            return string.Format(
                CultureInfo.CurrentCulture, "{0} {1}",
                value,
                value == 1 ? singularSuffix : pluralSuffix
            );
        }
    }
}
