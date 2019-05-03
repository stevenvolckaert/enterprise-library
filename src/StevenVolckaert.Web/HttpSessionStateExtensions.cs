namespace StevenVolckaert.Web
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.SessionState;

    /// <summary>
    ///     Provides extension methods for <see cref="HttpSessionState"/> instances.
    /// </summary>
    public static class HttpSessionStateExtensions
    {
        /// <summary>
        ///     Retrieves a value from the current <see cref="HttpSessionState"/>,
        ///     using the name of this method's caller as the key.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="httpSessionState">
        ///     The <see cref="HttpSessionState"/> instance this method affects.
        /// </param>
        /// <param name="key">
        ///     The key of value to retrieve, usually the name of this method's caller. This value is optional
        ///     and is provided automatically when invoked from compilers that support
        ///     <see cref="CallerMemberNameAttribute"/>.
        /// </param>
        /// <returns>
        ///     The session-state value with name of this method's caller, or <c>null</c> if the key does not exist.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="httpSessionState"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static T Get<T>(this HttpSessionState httpSessionState, [CallerMemberName] string key = null)
        {
            if (httpSessionState == null)
                throw new ArgumentNullException(nameof(httpSessionState));

            return httpSessionState.Get(defaultValue: default(T), key: key);
        }

        /// <summary>
        ///     Retrieves a value from the current <see cref="HttpSessionState"/>,
        ///     using the name of this method's caller as the key, or a default value if
        ///     the value with specified key doesn't exist.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="httpSessionState">
        ///     The <see cref="HttpSessionState"/> instance this method affects.
        /// </param>
        /// <param name="defaultValue">
        ///     The value to return if the value with specified key doesn't exist.
        /// </param>
        /// <param name="key">
        ///     The key of value to retrieve, usually the name of this method's caller. This value is optional
        ///     and is provided automatically when invoked from compilers that support
        ///     <see cref="CallerMemberNameAttribute"/>.
        /// </param>
        /// <returns>
        ///     The session-state value with name of this method's caller, or
        ///     <paramref name="defaultValue"/> if a value with that name does not exist.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="httpSessionState"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static T Get<T>(this HttpSessionState httpSessionState, T defaultValue, [CallerMemberName] string key = null)
        {
            if (httpSessionState == null)
                throw new ArgumentNullException(nameof(httpSessionState));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            return (T)(httpSessionState[key] ?? defaultValue);
        }

        /// <summary>
        ///     Persists a value to the current <see cref="HttpSessionState"/>,
        ///     using the name of this method's caller as the key.
        /// </summary>
        /// <param name="httpSessionState">
        ///     The <see cref="HttpSessionState"/> instance this method affects.
        /// </param>
        /// <param name="value">
        ///     The value to persist.
        /// </param>
        /// <param name="key">
        ///     The key of value to set, usually the name of this method's caller. This value is optional
        ///     and is provided automatically when invoked from compilers that support
        ///     <see cref="CallerMemberNameAttribute"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="httpSessionState"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static void Set(this HttpSessionState httpSessionState, object value, [CallerMemberName] string key = null)
        {
            if (httpSessionState == null)
                throw new ArgumentNullException(nameof(httpSessionState));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            httpSessionState[key] = value;
        }

        /// <summary>
        ///     Persists a value to the current <see cref="HttpSessionState"/>, or removes the key-value pair
        ///     if the value is <c>null</c>, using the name of this method's caller as the key.
        /// </summary>
        /// <param name="httpSessionState">
        ///     The <see cref="HttpSessionState"/> instance this method affects.
        /// </param>
        /// <param name="value">
        ///     The value to persist or remove.
        /// </param>
        /// <param name="key">
        ///     The key of value to set or remove, usually the name of this method's caller. This value is optional
        ///     and is provided automatically when invoked from compilers that support
        ///     <see cref="CallerMemberNameAttribute"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="httpSessionState"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static void SetOrRemove(this HttpSessionState httpSessionState, object value, [CallerMemberName] string key = null)
        {
            if (httpSessionState == null)
                throw new ArgumentNullException(nameof(HttpSessionState));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (value == null)
                httpSessionState.Remove(key);
            else
                httpSessionState[key] = value;
        }
    }
}
