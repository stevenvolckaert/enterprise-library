namespace StevenVolckaert
{
    using System;
    using System.Reflection;

    /// <summary>
    ///     Provides extension methods for <see cref="Assembly"/> instances.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        ///     Gets the simple name of the assembly, i.e. the first part of its display name.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> instance this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <c>null</c>.</exception>
        public static string GetSimpleName(this Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            return assembly.FullName.Split(',')[0];
        }
    }
}
