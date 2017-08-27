namespace StevenVolckaert.Globalization
{
using System;
using System.Collections.Generic;
using System.Globalization;

    /// <summary>
    ///     Manages cultures of an application.
    /// </summary>
    public interface ICultureManager
    {
        /// <summary>
        ///     Gets the manager's current culture.
        /// </summary>
        CultureInfo CurrentCulture { get; }
        /// <summary>
        ///     Gets or sets the name of the current culture in the format
        ///     "&lt;languagecode2&gt;-&lt;country/regioncode2&gt;".
        /// </summary>
        /// <returns>
        ///     The current culture name in the format "&lt;languagecode2&gt;-&lt;country/regioncode2&gt;",
        ///     where "&lt;languagecode2&gt;" is a lowercase two-letter code derived from ISO 639-1
        ///     and "&lt;country/regioncode2&gt;" is an uppercase two-letter code derived from ISO 3166.
        ///</returns>
        string CurrentCultureName { get; set; }
        /// <summary>
        ///     Gets the name of the manager's default culture.
        /// </summary>
        string DefaultCultureName { get; }
        /// <summary>
        ///     Gets a dictionary of cultures that are supported by this culture manager.
        /// </summary>
        Dictionary<string, CultureInfo> SupportedCultures { get; }
        /// <summary>
        ///     Occurs when the manager's culture has changed.
        /// </summary>
        event EventHandler<CurrentCultureChangedEventArgs> CurrentCultureChanged;
        /// <summary>
        ///     Returns an instance of the <see cref="CultureInfo"/> class based on the culture specified by
        ///     name, or <c>null</c> if the culture is not supported by the current operating system.
        /// </summary>
        /// <param name="cultureName">The name of a culture.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="cultureName"/> is <c>null</c>.
        /// </exception>
        CultureInfo GetCultureInfo(string cultureName);
        /// <summary>
        ///     Convert a given culture name to the name of it's associated neutral culture.
        /// </summary>
        /// <param name="cultureName">
        ///     The name of the culture, representing a specific or neutral culture.
        /// </param>
        /// <returns>
        ///     The name of the neutral culture, or <c>null</c> if the culture is not supported.
        /// </returns>
        string GetNeutralCultureName(string cultureName);
        /// <summary>
        ///     Returns a value that indicates whether a given culture, or it's associated neutral culture,
        ///     is currently selected.
        /// </summary>
        /// <param name="cultureName">The name of the culture.</param>
        bool IsCultureSelected(string cultureName);
        /// <summary>
        ///     Returns a value that indicates whether a given culture, or its associated neutral culture,
        ///     is supported by this culture manager.
        /// </summary>
        /// <param name="cultureName">
        ///     The name of the culture, representing a specific or neutral culture.
        /// </param>
        bool IsCultureSupported(string cultureName);
        /// <summary>
        ///     Returns a value that indicates whether a given specific culture is supported
        ///     by this culture manager.
        /// </summary>
        /// <param name="cultureName">The name of the culture.</param>
        /// <returns><c>true</c> if the culture is supported, <c>false</c> otherwise.</returns>
        bool IsSpecificCultureSupported(string cultureName);
        /// <summary>
        ///     Sets the application's current culture, given an array of culture names.
        ///     The manager selects the first culture that is supported, or the default culture
        ///     if none of the specified cultures are supported.
        /// </summary>
        /// <param name="cultureNames">An array that contains zero or more culture names.</param>
        /// <returns>The name of the manager's culture when the operation finishes.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="cultureNames"/> is <c>null</c>.
        /// </exception>
        string SetCulture(params string[] cultureNames);
        /// <summary>
        ///     Sets the application's current culture.
        ///     If the given culture is not supported, the manager's default culture is selected.
        ///     <para>
        ///         If a given specific culture is not supported, its associated neutral culture is selected
        ///         (if it exists).
        ///     </para>
        /// </summary>
        /// <param name="cultureName">
        ///     The name of the culture, representing a specific or neutral culture.
        /// </param>
        /// <returns>The name of the manager's culture when the operation finishes.</returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="cultureName"/> is <c>null</c>, empty, or white space.
        /// </exception>
        string SetCulture(string cultureName);
        /// <summary>
        ///     Sets the application's current culture to the manager's default culture.
        /// </summary>
        /// <returns>The name of the manager's culture when the operation finishes.</returns>
        string SetDefaultCulture();
        /// <summary>
        ///     Sets the application's current culture to a specific culture.
        ///     If the given culture is not supported, the manager's default culture is selected.
        /// </summary>
        /// <param name="cultureName">The name of the culture.</param>
        /// <returns>The name of the manager's culture when the operation finishes.</returns>
        string SetSpecificCulture(string cultureName);
    }
}
