namespace StevenVolckaert
{
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    ///     Provides extension methods for <see cref="long"/> and <see cref="ulong"/> values.
    /// </summary>
    public static class Int64Extensions
    {
        private static readonly string[] BinaryUnitSymbols = { "B", "KiB", "MiB", "GiB", "TiB", "PiB" };
        private static readonly string[] DecimalUnitSymbols = { "B", "kB", "MB", "GB", "TB", "PB" };

        /// <summary>
        ///     Returns a string that represents a number of bytes in a human-readable format,
        ///     using the <see cref="UnitOfInformationPrefix.Binary"/> prefix.
        /// </summary>
        /// <param name="value">The <see cref="long"/> value, in bytes.</param>
        /// <returns>
        ///     A human-readable string representation of <paramref name="value"/>, using powers of 1024.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="value"/> is negative.
        /// </exception>
        public static string BytesToString(this long value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(value), message: Resources.ValueNegative);

            return value.BytesToString(UnitOfInformationPrefix.Binary);
        }

        /// <summary>
        ///     Returns a string that represents a number of bytes in a human-readable format.
        /// </summary>
        /// <param name="value">The <see cref="long"/> value, in bytes.</param>
        /// <param name="prefix">The prefix to be used in the string representation.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="value"/> is negative or <paramref name="prefix"/> has an illegal value.
        /// </exception>
        public static string BytesToString(this long value, UnitOfInformationPrefix prefix)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(value), message: Resources.ValueNegative);

            var divider = (long)prefix;
            string[] unitSymbols;

            switch (prefix)
            {
                case UnitOfInformationPrefix.Binary:
                    unitSymbols = BinaryUnitSymbols;
                    break;
                case UnitOfInformationPrefix.Decimal:
                    unitSymbols = DecimalUnitSymbols;
                    break;
                case UnitOfInformationPrefix.None:
                    return value + " B";
                default:
                    throw new ArgumentOutOfRangeException(
                        paramName: nameof(prefix),
                        message: string.Format(CultureInfo.InvariantCulture, Resources.IllegalEnumValue, prefix)
                    );
            }

            double divideValue = 1;
            long compareValue = divider;

            foreach (var unitSymbol in unitSymbols)
            {
                if (value < compareValue)
                    return string.Format(
                        CultureInfo.InvariantCulture,
                        "{0:0.#} {1}",
                        value / divideValue,
                        unitSymbol
                    );

                compareValue = compareValue * divider;
                divideValue = divideValue * divider;
            }

            return string.Format(
                CultureInfo.InvariantCulture, "{0:0.#} {1}", value / divideValue, unitSymbols.Last()
            );
        }

        /// <summary>
        ///     Returns a string that represents a number of kibiytes (KiB) in a human-readable format.
        /// </summary>
        /// <param name="value">The <see cref="long"/> value, in kibibytes (KiB).</param>
        public static string KibibytesToString(this long value)
        {
            return BytesToString(value * 1024, UnitOfInformationPrefix.Binary);
        }

        /// <summary>
        ///     Returns a string that represents a number of kilobytes (kB) in a human-readable format.
        /// </summary>
        /// <param name="value">The <see cref="long"/> value, in kilobytes (kB).</param>
        public static string KilobytesToString(this long value)
        {
            return BytesToString(value * 1000, UnitOfInformationPrefix.Decimal);
        }

        /// <summary>
        ///     Returns a string that represents a number of mebibytes (MiB) in a human-readable format.
        /// </summary>
        /// <param name="value">The <see cref="long"/> value, in mebibytes (MiB).</param>
        public static string MebibytesToString(this long value)
        {
            return BytesToString(value * 1024 * 1024, UnitOfInformationPrefix.Binary);
        }

        /// <summary>
        ///     Returns a string that represents a number of megabytes (MB) in a human-readable format.
        /// </summary>
        /// <param name="value">The <see cref="long"/> value, in megabytes (MB).</param>
        public static string MegabytesToString(this long value)
        {
            return BytesToString(value * 1000 * 1000, UnitOfInformationPrefix.Decimal);
        }
    }
}
