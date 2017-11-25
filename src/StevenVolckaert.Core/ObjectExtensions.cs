namespace StevenVolckaert
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    ///     Provides extension methods for <see cref="object"/> instances.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Casts the object as a <see cref="List{T}"/> of the specified type.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The type to cast the elements of <paramref name="source"/> to.
        /// </typeparam>
        /// <param name="source">
        ///     The <see cref="object"/> that needs to be casted to a <see cref="List{T}"/> of the specified
        ///     type.
        /// </param>
        /// <returns>
        ///     A <see cref="List{T}"/> that contains each element of the <paramref name="source"/>
        ///     sequence cast to the specified type.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidCastException">
        ///     An element in the sequence cannot be cast to the type <typeparamref name="TResult"/>.
        /// </exception>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1002:DoNotExposeGenericLists",
            Justification = "Method is provided for convenience."
        )]
        public static List<TResult> AsList<TResult>(this object source)
        {
            var result = source as List<TResult>;

            return result == null
                ? ((List<object>)source).Cast<TResult>().ToList()
                : result;
        }

        /// <summary>
        ///     Returns a value that indicates whether the object is <c>null</c>.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="object"/> instance this extension method affects.
        /// </param>
        /// <returns>
        ///     <c>true</c> if <paramref name="value"/> is <c>null</c>; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNull(this object value)
        {
            return null == value;
        }

        /// <summary>
        ///     Returns a value that indicates whether the object is not <c>null</c>.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="object"/> instance this extension method affects.
        /// </param>
        /// <returns>
        ///     <c>true</c> if <paramref name="value"/> is not <c>null</c>; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNotNull(this object value)
        {
            return null != value;
        }
    }
}
