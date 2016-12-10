#if (NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NETSTANDARD1_6)
namespace StevenVolckaert
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Provides extension methods for instances that implement the <see cref="IDictionary{TKey, TValue}"/> interface.
    /// </summary>
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Returns a read-only wrapper for the specified dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to wrap in a read-only wrapper.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is <c>null</c>.</exception>
        public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary
        )
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }
    }
}
#endif
