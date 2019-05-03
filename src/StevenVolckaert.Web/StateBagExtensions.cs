namespace StevenVolckaert.Web
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.UI;

    /// <summary>
    ///     Provides extension methods for <see cref="StateBag"/> instances.
    /// </summary>
    public static class StateBagExtensions
    {
        /// <summary>
        ///     Retrieves a value from the current <see cref="StateBag"/>,
        ///     using the name of this method's caller as the key.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="stateBag">
        ///     The <see cref="StateBag" /> instance this method affects.
        /// </param>
        /// <param name="key">
        ///     The key of value to retrieve, usually the name of this method's caller. This value
        ///     is optional and is provided automatically when invoked from compilers that support
        ///     <see cref="CallerMemberNameAttribute"/>.
        /// </param>
        /// <returns>
        ///     The state bag value with name of this method's caller, or <c>default(T)</c> if
        ///     the key does not exist.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stateBag"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static T Get<T>(this StateBag stateBag, [CallerMemberName] string key = null)
        {
            if (stateBag == null)
                throw new ArgumentNullException(nameof(stateBag));

            return stateBag.Get(defaultValue: default(T), key: key);
        }

        /// <summary>
        ///     Retrieves a value from the current <see cref="StateBag"/>,
        ///     using the name of this method's caller as the key, or a default value if
        ///     the value with specified key doesn't exist.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="stateBag">
        ///     The <see cref="StateBag" /> instance this method affects.
        /// </param>
        /// <param name="defaultValue">
        ///     The value to return if the value with specified key doesn't exist.
        /// </param>
        /// <param name="key">
        ///     The key of value to retrieve, usually the name of this method's caller. This value
        ///     is optional and is provided automatically when invoked from compilers that support
        ///     <see cref="CallerMemberNameAttribute"/>.
        /// </param>
        /// <returns>
        ///     The state bag value with name of this method's caller, or
        ///     <paramref name="defaultValue"/> if a value with that name does not exist.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stateBag"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static T Get<T>(this StateBag stateBag, T defaultValue, [CallerMemberName] string key = null)
        {
            if (stateBag == null)
                throw new ArgumentNullException(nameof(stateBag));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            return (T)(stateBag[key] ?? defaultValue);
        }

        /// <summary>
        ///     Persists a value to the current <see cref="StateBag"/>,
        ///     using the name of this method's caller as the key.
        /// </summary>
        /// <param name="stateBag">
        ///     The <see cref="StateBag" /> instance this method affects.
        /// </param>
        /// <param name="value">
        ///     The value to persist.
        /// </param>
        /// <param name="key">
        ///     The key of value to set, usually the name of this method's caller. This value is optional
        ///     and is provided automatically when invoked from compilers that support
        ///     <see cref="CallerMemberNameAttribute"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="stateBag"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static void Set(this StateBag stateBag, object value, [CallerMemberName] string key = null)
        {
            if (stateBag == null)
                throw new ArgumentNullException(nameof(stateBag));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            stateBag[key] = value;
        }
    }
}
