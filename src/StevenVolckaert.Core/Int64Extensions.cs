namespace StevenVolckaert
{
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    ///     Specifies a prefix that is used in combination with a unit of information (e.g. byte).
    /// </summary>
    public enum UnitOfInformationPrefix
    {
        /// <summary>
        /// No prefix, indicating a power of 1.
        /// </summary>
        None = 0,

        /// <summary>
        /// A decimal prefix, indicating a power of 1000.
        /// </summary>
        Decimal = 1000,

        /// <summary>
        /// A binary prefix, indicating a power of 1024.
        /// </summary>
        Binary = 1024
    }

    /// <summary>
    ///     Provides extension methods for <see cref="long"/> and <see cref="ulong"/> values.
    /// </summary>
    public static class Int64Extensions
    {
        private static readonly string[] _binaryUnitSymbols =
            new string[] { "B", "KiB", "MiB", "GiB", "TiB", "PiB" };

        private static readonly string[] _decimalUnitSymbols =
            new string[] { "B", "kB", "MB", "GB", "TB", "PB" };

        /// <summary>
        ///     Returns a string that represents a number of bytes in a human-readable format.
        /// </summary>
        /// <param name="value">The <see cref="ulong"/> value, in bytes.</param>
        /// <param name="prefix">The prefix to be used in the string representation.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="prefix"/> has an illegal value.
        /// </exception>
        public static string BytesToString(this ulong value, UnitOfInformationPrefix prefix)
        {
            var divider = (ulong)prefix;
            string[] unitSymbols;

            switch (prefix)
            {
                case UnitOfInformationPrefix.Binary:
                    unitSymbols = _binaryUnitSymbols;
                    break;
                case UnitOfInformationPrefix.Decimal:
                    unitSymbols = _decimalUnitSymbols;
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
            ulong compareValue = divider;

            for (var i = 0; i < unitSymbols.Length; i++)
            {
                if (value < compareValue)
                    return string.Format(
                        CultureInfo.InvariantCulture,
                        "{0:0.#} {1}",
                        value / divideValue,
                        unitSymbols[i]
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
        /// <param name="value">The <see cref="ulong"/> value, in kibibytes (KiB).</param>
        public static string KibibytesToString(this ulong value)
        {
            return BytesToString(value * 1024, UnitOfInformationPrefix.Binary);
        }

        /// <summary>
        ///     Returns a string that represents a number of kilobytes (kB) in a human-readable format.
        /// </summary>
        /// <param name="value">The <see cref="ulong"/> value, in kilobytes (kB).</param>
        public static string KilobytesToString(this ulong value)
        {
            return BytesToString(value * 1000, UnitOfInformationPrefix.Decimal);
        }

        /// <summary>
        ///     Returns a string that represents a number of mebibytes (MiB) in a human-readable format.
        /// </summary>
        /// <param name="value">The <see cref="ulong"/> value, in mebibytes (MiB).</param>
        public static string MebibytesToString(this ulong value)
        {
            return BytesToString(value * 1024 * 1024, UnitOfInformationPrefix.Binary);
        }

        /// <summary>
        ///     Returns a string that represents a number of megabytes (MB) in a human-readable format.
        /// </summary>
        /// <param name="value">The <see cref="ulong"/> value, in megabytes (MB).</param>
        public static string MegabytesToString(this ulong value)
        {
            return BytesToString(value * 1000 * 1000, UnitOfInformationPrefix.Decimal);
        }
    }
}
