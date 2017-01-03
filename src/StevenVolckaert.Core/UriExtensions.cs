namespace StevenVolckaert
{
    using System;

    /// <summary>
    ///     Provides extension methods for <see cref="Uri"/> instances.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        ///     Returns the base URL of the current <see cref="Uri"/> instance,
        ///     consisting of the URI's scheme and host.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="uri"/> is <c>null</c>.</exception>
        public static string ToBaseUrl(this Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            return uri.Scheme + @"://" + uri.Host;
        }

        /// <summary>
        ///     Returns a new <see cref="Uri"/> which is one level of hierarchy above
        ///     the current <see cref="Uri"/>, using the '/' character as delimiter.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="uri"/> is <c>null</c>.</exception>
        public static Uri UpOneLevel(this Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            return new Uri(
                uri.ToString().Remove(uri.ToString().LastIndexOf("/", StringComparison.OrdinalIgnoreCase))
            );
        }
    }
}
