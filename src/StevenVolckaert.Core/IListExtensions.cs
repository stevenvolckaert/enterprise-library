namespace StevenVolckaert
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    ///     Provides extension methods for instances that implement the <see cref="IList{T}"/> interface.
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        ///     Returns a read-only wrapper for the specified <see cref="IList{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <param name="list">The list to wrap</param>
        /// <exception cref="ArgumentNullException"><paramref name="list"/> is <c>null</c>.</exception>
        public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            return new ReadOnlyCollection<T>(list);
        }
    }
}
