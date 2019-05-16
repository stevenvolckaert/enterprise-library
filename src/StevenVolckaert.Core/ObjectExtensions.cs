﻿namespace StevenVolckaert
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
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return source as List<TResult> ?? ((List<object>)source).Cast<TResult>().ToList();
        }
    }
}
