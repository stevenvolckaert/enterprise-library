namespace StevenVolckaert
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
#if !NET35
    using System.Collections.ObjectModel;
#endif

    /// <summary>
    ///     Provides extension methods for instances that implement the <see cref="IEnumerable{T}"/>
    ///     interface.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        ///     Determines whether a sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> instance to check for emptiness. </param>
        /// <returns>
        ///     <c>true</c> if the source sequence contains no elements; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        public static bool Empty<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return !source.Any();
        }

        /// <summary>
        ///     Determines whether none of the elements of a sequence satisfies a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">
        ///     An <see cref="IEnumerable{T}"/> instance whose elements to apply the predicate to.
        /// </param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///     <c>true</c> if none of the elements in the <paramref name="source"/> sequence pass the test
        ///     in the specified predicate; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="predicate"/> is <c>null</c>.
        /// </exception>
        public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return !source.Any(predicate);
        }

        /// <summary>
        ///     Produces the difference of a sequence and a specified element
        ///     by using the default equality comparer to compare values.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the input sequence.</typeparam>
        /// <param name="source">
        ///     An <see cref="IEnumerable{T}"/> instance whose elements do not
        ///     contain <paramref name="element"/> when this operation returns.
        /// </param>
        /// <param name="element">
        ///     An element that, if contained in <paramref name="source"/>,
        ///     will be removed from the returned sequence.
        /// </param>
        /// <returns>
        ///     A sequence that contains the set difference of <paramref name="source"/> and
        ///     <paramref name="element"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <c>null</c>.
        /// </exception>
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> source, TSource element)
        {
            return source.Except(new List<TSource> { element });
        }

        /// <summary>
        ///     Determines whether a sequence is a subset of another sequence.
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source"/> and <paramref name="other"/>.
        /// </typeparam>
        /// <param name="source">The sequence to check.</param>
        /// <param name="other">The sequence to compare with <paramref name="source"/>.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="source"/> is a subset of <paramref name="other"/>;
        ///     otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="other"/> is <c>null</c>.
        /// </exception>
        public static bool IsSubsetOf<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> other)
        {
            return !source.Except(other).Any();
        }

        /// <summary>
        ///     Sorts the elements of a sequence in ascending ordinal order,
        ///     using the <see cref="object.ToString()"/> method as the key.
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements contained in <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">The sequence to order.</param>
        /// <returns>
        ///     An <see cref="IOrderedEnumerable{TSource}"/> whose elements are sorted according to
        ///     the <see cref="object.ToString()"/> method of every element.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <c>null</c>.
        /// </exception>
        public static IOrderedEnumerable<TSource> OrderByOrdinal<TSource>(this IEnumerable<TSource> source)
        {
            return source.OrderBy(x => x.ToString(), new OrdinalStringComparer(ignoreCase: false));
        }

        /// <summary>
        ///     Sorts the elements of a sequence in ascending ordinal order, according to a specified key.
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements contained in <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">The sequence to order.</param>
        /// <param name="keySelector">A function that extracts a key from an element.</param>
        /// <returns>
        ///     An <see cref="IOrderedEnumerable{TSource}"/> whose elements are sorted according to
        ///     the specified key.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="keySelector"/> is <c>null</c>.
        /// </exception>
        public static IOrderedEnumerable<TSource> OrderByOrdinal<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, string> keySelector)
        {
            return source.OrderBy(keySelector, new OrdinalStringComparer(ignoreCase: false));
        }

        /// <summary>
        ///     Sorts the elements of a sequence in ascending ordinal order, according to a specified key.
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements contained in <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">The sequence to order.</param>
        /// <param name="keySelector">A function that extracts a key from an element.</param>
        /// <param name="ignoreCase">A value that indicates whether to ignore case during comparison.</param>
        /// <returns>
        ///     An <see cref="IOrderedEnumerable{TSource}"/> instance whose elements are sorted according to
        ///     the specified key.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="keySelector"/> is <c>null</c>.
        /// </exception>
        public static IOrderedEnumerable<TSource> OrderByOrdinal<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, string> keySelector,
            bool ignoreCase)
        {
            return source.OrderBy(keySelector, new OrdinalStringComparer(ignoreCase));
        }

        /// <summary>
        ///     Sorts the elements of a sequence in descending ordinal order, according to a specified key.
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements contained in <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">The sequence to order.</param>
        /// <returns>
        ///     An <see cref="IOrderedEnumerable{TSource}"/> instance whose elements are sorted according to
        ///     the specified key.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <c>null</c>.
        /// </exception>
        public static IOrderedEnumerable<TSource> OrderByOrdinalDescending<TSource>(
            this IEnumerable<TSource> source
        )
        {
            return source.OrderByDescending(x => x.ToString(), new OrdinalStringComparer(ignoreCase: false));
        }

        /// <summary>
        ///     Sorts the elements of a sequence in descending ordinal order, according to a specified key.
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements contained in <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">The sequence to order.</param>
        /// <param name="keySelector">A function that extracts a key from an element.</param>
        /// <returns>
        ///     An <see cref="IOrderedEnumerable{TSource}"/> instance whose elements are sorted according to
        ///     the specified key.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="keySelector"/> is <c>null</c>.
        /// </exception>
        public static IOrderedEnumerable<TSource> OrderByOrdinalDescending<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, string> keySelector)
        {
            return source.OrderByDescending(keySelector, new OrdinalStringComparer(ignoreCase: false));
        }

        /// <summary>
        ///     Sorts the elements of a sequence in descending ordinal order, according to a specified key.
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements contained in <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">The sequence to order.</param>
        /// <param name="keySelector">A function that extracts a key from an element.</param>
        /// <param name="ignoreCase">A value that indicates whether to ignore case during comparison.</param>
        /// <returns>
        ///     An <see cref="IOrderedEnumerable{TSource}"/> instance whose elements are sorted according to
        ///     the specified key.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="keySelector"/> is <c>null</c>.
        /// </exception>
        public static IOrderedEnumerable<TSource> OrderByOrdinalDescending<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, string> keySelector,
            bool ignoreCase)
        {
            return source.OrderByDescending(keySelector, new OrdinalStringComparer(ignoreCase));
        }

        /// <summary>
        ///     Removes the last element of a sequence.
        /// </summary>
        /// <param name="source">The sequence to remove an element from.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{T}"/> instance that doesn't contain the element at the end of
        ///     the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <c>null</c>.
        /// </exception>
        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source)
        {
            return source.SkipLast(count: 1);
        }

        /// <summary>
        ///     Removes a specified number of contiguous elements from the end of a sequence.
        /// </summary>
        /// <param name="source">The sequence to remove elements from.</param>
        /// <param name="count">The number of elements to remove.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{T}"/> instance that doesn't contain the specified number of elements
        ///     at the end of the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <c>null</c>.
        /// </exception>
        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (count <= 0)
                yield break;

            var buffer = new Queue<T>(count + 1);

            foreach (var element in source)
            {
                buffer.Enqueue(element);

                if (buffer.Count == count + 1)
                    yield return buffer.Dequeue();
            }
        }

#if !NET35
        /// <summary>
        ///     Creates a new <see cref="ObservableCollection{T}"/> from a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The sequence to create an <see cref="ObservableCollection{T}"/> from.</param>
        /// <returns>
        ///     An <see cref="ObservableCollection{T}"/> instance that contains elements from the input
        ///     sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <c>null</c>.
        /// </exception>
        public static ObservableCollection<TSource> ToObservableCollection<TSource>(
            this IEnumerable<TSource> source
        )
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return new ObservableCollection<TSource>(source);
        }

        /// <summary>
        ///     Creates a <see cref="ReadOnlyObservableCollection{T}"/> from a sequence.
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the elements of <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        ///     The sequence to create a <see cref="ReadOnlyObservableCollection{T}"/> instance from.
        /// </param>
        /// <returns>
        ///     A <see cref="ReadOnlyObservableCollection{T}"/> instance that contains elements from
        ///     the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <c>null</c>.
        /// </exception>
        public static ReadOnlyObservableCollection<TSource> ToReadOnlyObservableCollection<TSource>(
            this IEnumerable<TSource> source)
        {
            return new ReadOnlyObservableCollection<TSource>(source.ToObservableCollection());
        }
#endif

        /// <summary>
        ///     Converts a sequence to a string by concatenating the <see cref="object.ToString()"/> value
        ///     of every element, using a specified separator between each member.
        /// </summary>
        /// <param name="source">The sequence to convert.</param>
        /// <param name="separator">The string to use as a separator.</param>
        /// <returns>
        ///     A string that consists of the string representations of the elements
        ///     contained in <paramref name="source"/>, separated with <paramref name="separator"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <c>null</c>.
        /// </exception>
        public static string ToString(this IEnumerable<object> source, string separator)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return string.Join(separator, source.Select(x => x.ToString()).ToArray());
        }

        /// <summary>
        ///     Produces the union of a sequence and a specified element
        ///     by using the default equality comparer to compare values.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the input sequence.</typeparam>
        /// <param name="source">
        ///     An <see cref="IEnumerable{T}"/> instance whose distinct elements
        ///     form the first set for the union.
        /// </param>
        /// <param name="element">
        ///     An element who forms the second set of the union.
        /// </param>
        /// <returns>
        ///     An <see cref="IEnumerable{T}"/> that contains the elements of <paramref name="source"/>
        ///     and <paramref name="element"/>, excluding duplicates.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <c>null</c>.
        /// </exception>
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> source, TSource element)
        {
            return source.Union(new List<TSource> { element });
        }
    }
}
