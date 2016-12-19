namespace StevenVolckaert
{
    using System;

    /// <summary>
    /// Provides extension methods for <see cref="DateTime"/> instances.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns the Unix time, also known as POSIX time or Epoch time, defined as the number of seconds elapsed
        /// since 00:00:00 UTC January 1, 1970, not counting leap seconds.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> instance this extension method affects.</param>
        /// <returns>The Unix time, i.e. the number of seconds elapsed since 1970-01-01T00:00:00,
        /// not counting leap seconds.</returns>
        /// <remarks>See https://en.wikipedia.org/wiki/Unix_time for more information.</remarks>
        public static int ToUnixTime(this DateTime dateTime)
        {
            return (int)(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
