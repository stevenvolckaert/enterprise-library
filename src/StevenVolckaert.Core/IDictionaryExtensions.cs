namespace StevenVolckaert
{
    using System;
    using System.Collections.Generic;
#if !NET35
    using System.Collections.ObjectModel;
#endif

    /// <summary>
    ///     Provides extension methods for instances that implement the <see cref="IDictionary{TKey, TValue}"/>
    ///     interface.
    /// </summary>
    public static class IDictionaryExtensions
    {
#if !NET35
        /// <summary>
        ///     Returns a read-only wrapper for the specified dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to wrap in a read-only wrapper.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="dictionary"/> is <c>null</c>.
        /// </exception>
        public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary
        )
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }
#endif

        /// <summary>
        ///     Gets the value associated with the specified key, or the type's default value if the key
        ///     doesn't exist.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">
        ///     The <see cref="IDictionary{TKey, TValue}"/> instance this extension method affects.
        /// </param>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>
        ///     The value associated with the specified key, or the type's default value if
        ///     <paramref name="key"/> doesn't exist.
        /// </returns>
        public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            return dictionary.TryGetValue(key, defaultValue: default(TValue));
        }

        /// <summary>
        ///     Gets the value associated with the specified key, or a default value if the key doesn't exist.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">
        ///     The <see cref="IDictionary{TKey, TValue}"/> instance this extension method affects.
        /// </param>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        ///     The value associated with the specified key, or <paramref name="defaultValue"/> if
        ///     <paramref name="key"/> doesn't exist.
        /// </returns>
        public static TValue TryGetValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue
        )
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            return dictionary.ContainsKey(key) ? dictionary[key] : defaultValue;
        }
    }
}
