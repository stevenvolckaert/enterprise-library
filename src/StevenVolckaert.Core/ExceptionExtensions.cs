namespace StevenVolckaert
{
    using System;
    using System.Globalization;
    using System.Text;

#if NETSTANDARD1_6
    using System.Runtime.InteropServices;
#endif

    /// <summary>
    /// Provides extensions methods for <see cref="Exception"/> instances.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Returns a message that describes the current exception in detail.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> instance this extension method affects.</param>
        /// <returns>A message that describes <paramref name="exception"/> in detail,
        /// or an emtpy string if <paramref name="exception"/> is <c>null</c>.</returns>
        public static string DetailedMessage(this Exception exception)
        {
            return DetailedMessage(exception, formatProvider: CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns a message that describes the current exception in detail.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> instance this extension method affects.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <returns>A message that describes <paramref name="exception"/> in detail,
        /// or an emtpy string if <paramref name="exception"/> is <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="formatProvider"/> is <c>null</c>.</exception>
        public static string DetailedMessage(this Exception exception, IFormatProvider formatProvider)
        {
            if (exception == null)
                return string.Empty;

            if (formatProvider == null)
                throw new ArgumentNullException(nameof(formatProvider));

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(
                string.Format(
                    CultureInfo.CurrentCulture,
                    $"[{DateTime.Now.ToString("s", formatProvider)}] {Resources.EncounteredExceptionOfType}",
                    exception
                )
            );

#if NETSTANDARD1_6
            stringBuilder.AppendLine("OS Architecture: " + RuntimeInformation.OSArchitecture);
            stringBuilder.AppendLine("OS Description: " + RuntimeInformation.OSDescription);
            stringBuilder.AppendLine("Process Architecture: " + RuntimeInformation.ProcessArchitecture);
            stringBuilder.AppendLine("FrameworkDescription: " + RuntimeInformation.FrameworkDescription);
#endif
            return stringBuilder.ToString();
        }
    }
}
