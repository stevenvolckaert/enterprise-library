namespace StevenVolckaert
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Provides extension methods for <see cref="Type"/> instances.
    /// </summary>
    public static class TypeExtensions
    {
#if NET35
        internal static Type GetTypeInfo(this Type type)
        {
            return type;
        }
#endif

        /// <summary>
        /// Returns a value that indicates whether the type is a <see cref="bool"/> or nullable <see cref="bool"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> instance this extension method affects.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="type"/> is a <see cref="bool"/> or nullable <see cref="bool"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static bool IsBoolean(this Type type)
        {
            return type.Is<bool>();
        }

        /// <summary>
        /// Returns a value that indicates whether the type is a <see cref="decimal"/> or
        /// nullable <see cref="decimal"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> instance this extension method affects.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="type"/> is a <see cref="decimal"/> or nullable <see cref="decimal"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static bool IsDecimal(this Type type)
        {
            return type.Is<decimal>();
        }

        /// <summary>
        /// Returns a value that indicates whether the type is a <see cref="int"/> or nullable <see cref="int"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> instance this extension method affects.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="type"/> is a <see cref="int"/> or nullable <see cref="bool"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static bool IsInt32(this Type type)
        {
            return type.Is<int>();
        }

        private static bool Is<T>(this Type type)
            where T : struct
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.Equals(typeof(T)) || type.Equals(typeof(T?));
        }

        /// <summary>
        /// Returns a value that indicates whether the type is a nullable type.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> instance this extension method affects.</param>
        /// <returns><c>true</c> if <paramref name="type"/> is nullable; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static bool IsNullable(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Returns a dictionary containing the name and metadata of all public properties of the specified type,
        /// recursively.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> instance this extension method affects.</param>
        /// <returns>A <see cref="Dictionary{String, PropertyInfo}"/>
        /// containing the name and metadata of all public properties of the specified type and its children.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static Dictionary<string, PropertyInfo> PropertyMetadata(this Type type)
        {
            return type.PropertyMetadata(parent: null);
        }

        /// <summary>
        /// Returns a dictionary containing the name and metadata of all public properties of the specified type,
        /// recursively.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> instance this extension method affects.</param>
        /// <param name="parent">The name of the parent that contains <paramref name="type"/>.</param>
        /// <returns>A <see cref="Dictionary{String, PropertyInfo}"/>
        /// containing the name and metadata of all public properties of the specified type and its children.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static Dictionary<string, PropertyInfo> PropertyMetadata(this Type type, string parent)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return GetPropertyMetadata(type, parent).ToDictionary(x => x.Key, x => x.Value);
        }

        private static IEnumerable<KeyValuePair<string, PropertyInfo>> GetPropertyMetadata(Type type, string parent)
        {
            foreach (var propertyInfo in type.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var name = parent == null
                    ? propertyInfo.Name
                    : parent + "." + propertyInfo.Name;

                var propertyTypeInfo = propertyInfo.PropertyType.GetTypeInfo();

                if ((propertyTypeInfo.IsClass && propertyInfo.PropertyType != typeof(string)) ||
                    propertyTypeInfo.IsInterface
                )
                    foreach (var value in propertyInfo.PropertyType.PropertyMetadata(name))
                        yield return value;
                else
                    yield return
                        new KeyValuePair<string, PropertyInfo>(
                            key: name,
                            value: propertyInfo
                        );
            }
        }

        /// <summary>
        /// Returns the metadata of all public properties of the specified type that have a specified attribute.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> instance this extension method affects.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <returns>A <see cref="IEnumerable{PropertyInfo}"/> containing
        /// the metadata of all public properties of the type that have the specified attribute.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type"/> or <paramref name="attributeType"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="attributeType"/> doesn't derive from <see cref="Attribute"/>.
        /// </exception>
        public static IEnumerable<PropertyInfo> PropertyMetadata(this Type type, Type attributeType)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (attributeType == null)
                throw new ArgumentNullException(nameof(attributeType));

            return type.GetTypeInfo().GetProperties()
                .Where(propertyInfo => CustomAttributeExtensions.IsDefined(propertyInfo, attributeType));
        }

        /// <summary>
        /// Returns the names of all public properties of the specified type that have a specified attribute.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> instance this extension method affects.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <returns>A <see cref="IEnumerable{String}"/> containing
        /// The names of all public properties of the type that have the specified attribute.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="type"/> or <paramref name="attributeType"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="attributeType"/> doesn't derive from <see cref="Attribute"/>.
        /// </exception>
        public static IEnumerable<string> PropertyNames(this Type type, Type attributeType)
        {
            return type.PropertyMetadata(attributeType).Select(propertyInfo => propertyInfo.Name);
        }

    }
#if NET35
    internal static class CustomAttributeExtensions
    {
        public static bool IsDefined(PropertyInfo propertyInfo, Type attributeType)
        {
            return Attribute.IsDefined(propertyInfo, attributeType);
        }
    }
#endif
}
