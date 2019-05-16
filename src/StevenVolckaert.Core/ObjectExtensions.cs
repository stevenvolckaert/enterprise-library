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
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return source as List<TResult> ?? ((List<object>)source).Cast<TResult>().ToList();
        }

#if NET452 || NETSTANDARD1_6

        /// <summary>
        ///     Creates a deep copy of the current <see cref="object"/>.
        ///     The type must be annotated with the <see cref="SerializableAttribute"/>.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of <paramref name="obj"/>. Ensure it is annotated with the
        ///     <see cref="SerializableAttribute"/>.
        /// </typeparam>
        /// <param name="obj">
        ///     The <see cref="object"/> instance this extension method affects.
        /// </param>
        /// <returns>
        ///     A deep copy op the current <see cref="object"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="obj"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="obj"/> is not serializable. Ensure type <typeparamref name="T"/>
        ///     is annotated with the <see cref="SerializableAttribute"/>.
        /// </exception>
        public static T DeepCopy<T>(this T obj)
        {
            if (ReferenceEquals(obj, null))
                throw new ArgumentNullException(nameof(obj));

            if (typeof(T).IsSerializable == false)
                throw new ArgumentException(
                    message: Resources.ValueNotSerializable,
                    paramName: nameof(obj)
                );

            T returnValue;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                var formatter =
                    new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                formatter.Serialize(memoryStream, obj);
                memoryStream.Seek(offset: 0, loc: System.IO.SeekOrigin.Begin);
                returnValue = (T)formatter.Deserialize(memoryStream);
                memoryStream.Close();
            }

            return returnValue;
        }

#endif
    }
}
