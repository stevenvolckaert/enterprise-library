namespace StevenVolckaert
{
    using System;

    /// <summary>
    /// Provides extension methods for <see cref="Enum"/> instances.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Converts the value of the specified enumeration to an equivalent type.
        /// </summary>
        /// <typeparam name="TEnum">The type of <see cref="Enum"/> to parse as.</typeparam>
        /// <param name="value">The <see cref="Enum"/> instance to convert.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> doesn't map to one of the named constants
        /// defined in <typeparamref name="TEnum"/>.</exception>
        public static TEnum ParseAs<TEnum>(this Enum value)
            where TEnum : struct
        {
            return value.ParseAs<TEnum>(ignoreCase: false);
        }

        /// <summary>
        /// Converts the value of the specified enumeration to an equivalent type.
        /// A parameter specifies whether the operation is case-insensitive.
        /// </summary>
        /// <typeparam name="TEnum">The type of <see cref="Enum"/> to parse as.</typeparam>
        /// <param name="value">The <see cref="Enum"/> instance to convert.</param>
        /// <param name="ignoreCase"><c>true</c> to ignore case; <c>false</c> to regard case.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> doesn't map to one of the named constants
        /// defined in <typeparamref name="TEnum"/>.</exception>
        public static TEnum ParseAs<TEnum>(this Enum value, bool ignoreCase)
            where TEnum : struct
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return value.ToString().ParseAs<TEnum>(ignoreCase);
        }
    }
}
