namespace StevenVolckaert.Globalization
{
    using System;

    /// <summary>
    ///     Provides data for <see cref="CultureManager.CurrentCultureChanged"/> subscribers.
    /// </summary>
    public class CurrentCultureChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the name of the current culture after the change.
        /// </summary>
        public string NewValue { get; private set; }

        /// <summary>
        ///     Gets the name of the current culture before the change.
        /// </summary>
        public string OldValue { get; private set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CurrentCultureChangedEventArgs"/> class.
        /// </summary>
        /// <param name="oldValue">The name of the current culture before the change.</param>
        /// <param name="newValue">The name of the current culture after the change.</param>
        public CurrentCultureChangedEventArgs(string oldValue, string newValue)
        {
            NewValue = newValue;
            OldValue = oldValue;
        }
    }
}
