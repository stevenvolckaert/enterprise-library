namespace StevenVolckaert.Diagnostics
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
#if !NET35
    using System.Runtime.CompilerServices;
#endif

    /// <summary>
    ///     Provides a set of methods and properties that help debug your code. This class cannot be inherited.
    /// </summary>
    public static class Debug
    {
        private const string DateTimeStringFormat = "T";

        private static string CurrentDateTimeString
        {
            get { return DateTime.Now.ToString(DateTimeStringFormat, CultureInfo.CurrentCulture); }
        }

        private static string GetMemberName(MemberInfo memberInfo)
        {
            return memberInfo.DeclaringType.FullName + "." + memberInfo.Name;
        }

        /// <summary>
        ///     Writes a message followed by a line terminator to the trace listeners
        ///     in the System.Diagnostics.Debug.Listeners collection.
        /// </summary>
        /// <param name="message">The message to write to the trace listeners.</param>
        /// <param name="callerMemberName">The name of the member that called this method.</param>
        [Conditional("DEBUG")]
#if NET35
        public static void WriteLine(string message, string callerMemberName)
        {
            System.Diagnostics.Debug.WriteLine(
                string.Format(
                    Resources.DebugMessageFormat,
                    CurrentDateTimeString,
                    callerMemberName,
                    message
                )
            );
        }
#else
        public static void WriteLine(string message, [CallerMemberName]string callerMemberName = "")
        {
            // TODO CA1305: See http://www.thomaslevesque.com/2015/02/24/customizing-string-interpolation-in-c-6/
            // to fix. Steven Volckaert. December 10, 2016.

            System.Diagnostics.Debug.WriteLine(
                Resources.DebugMessageFormat,
                CurrentDateTimeString,
                callerMemberName,
                message
            );
        }
#endif

        /// <summary>
        ///     Writes a message followed by a line terminator to the trace listeners
        ///     in the System.Diagnostics.Debug.Listeners collection.
        /// </summary>
        /// <param name="message">The message to write to the trace listeners.</param>
        /// <param name="callerMemberInfo">Metadata of the member that called this method.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="callerMemberInfo"/> is <c>null</c>.
        /// </exception>
        [Conditional("DEBUG")]
        public static void WriteLine(string message, MemberInfo callerMemberInfo)
        {
            if (callerMemberInfo == null)
                throw new ArgumentNullException(nameof(callerMemberInfo));

            WriteLine(message, callerMemberName: GetMemberName(callerMemberInfo));
        }

        /// <summary>
        ///     Writes an <see cref="Exception"/> instance followed by a line terminator to the trace listeners
        ///     in the System.Diagnostics.Debug.Listeners collection.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> instance to write to the trace listeners.</param>
        /// <param name="callerMemberName">The name of the member that called this method.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="exception"/> is <c>null</c>.
        /// </exception>
        [Conditional("DEBUG")]
#if NET35
        public static void WriteLine(Exception exception, string callerMemberName)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            System.Diagnostics.Debug.WriteLine(
                string.Format(
                    Resources.DebugExceptionMessageFormat,
                    CurrentDateTimeString,
                    callerMemberName,
                    exception.Message
                )
            );
        }
#else
        public static void WriteLine(Exception exception, [CallerMemberName]string callerMemberName = "")
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            System.Diagnostics.Debug.WriteLine(
                Resources.DebugExceptionMessageFormat,
                CurrentDateTimeString,
                callerMemberName,
                exception.Message
            );
        }
#endif

        /// <summary>
        ///     Writes an <see cref="Exception"/> instance followed by a line terminator to the trace listeners
        ///     in the System.Diagnostics.Debug.Listeners collection.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> instance to write to the trace listeners.</param>
        /// <param name="callerMemberInfo">Metadata of the member that called this method.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="exception"/> or <paramref name="callerMemberInfo"/> is <c>null</c>.
        /// </exception>
        [Conditional("DEBUG")]
        public static void WriteLine(Exception exception, MemberInfo callerMemberInfo)
        {
            if (callerMemberInfo == null)
                throw new ArgumentNullException(nameof(callerMemberInfo));

            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            WriteLine(exception, callerMemberName: GetMemberName(callerMemberInfo));
        }
    }
}
