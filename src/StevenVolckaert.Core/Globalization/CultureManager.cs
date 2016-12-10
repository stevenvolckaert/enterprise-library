namespace StevenVolckaert.Globalization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Diagnostics;

    /// <summary>
    /// Manages cultures of an application.
    /// </summary>
    public class CultureManager
    {
        private string _currentCultureName;
        /// <summary>
        /// Gets or sets the name of the current culture in the format
        /// "&lt;languagecode2&gt;-&lt;country/regioncode2&gt;".
        /// </summary>
        /// <returns>
        /// The current culture name in the format "&lt;languagecode2&gt;-&lt;country/regioncode2&gt;",
        /// where "&lt;languagecode2&gt;" is a lowercase two-letter code derived from ISO 639-1
        /// and "&lt;country/regioncode2&gt;" is an uppercase two-letter code derived from ISO 3166.
        ///</returns>
        public string CurrentCultureName
        {
            get { return _currentCultureName; }
            set
            {
#if NET35
                if (value == _currentCultureName || string.IsNullOrEmpty(value))
#else
                if (value == _currentCultureName || string.IsNullOrWhiteSpace(value))
#endif
                    return;

                SetCulture(value);
            }
        }

        /// <summary>
        /// Gets the manager's current culture.
        /// </summary>
        public CultureInfo CurrentCulture
        {
            get { return SupportedCultures[CurrentCultureName]; }
        }

        private readonly string _defaultCultureName;
        /// <summary>
        /// Gets the name of the manager's default culture.
        /// </summary>
        public string DefaultCultureName
        {
            get { return _defaultCultureName; }
        }

        private readonly Dictionary<string, CultureInfo> _supportedCultures;
        /// <summary>
        /// Gets a dictionary of cultures that are supported by this culture manager.
        /// </summary>
        public Dictionary<string, CultureInfo> SupportedCultures
        {
            get { return _supportedCultures; }
        }

        /// <summary>
        /// Occurs when the manager's culture has changed.
        /// </summary>
        public event EventHandler<CurrentCultureChangedEventArgs> CurrentCultureChanged;

        /// <summary>
        /// Raises the <see cref="CurrentCultureChanged"/> event.
        /// </summary>
        /// <param name="oldValue">The name of the current culture before the change.</param>
        /// <param name="newValue">The name of the current culture after the change.</param>
        private void OnCurrentCultureChanged(string oldValue, string newValue)
        {
            CurrentCultureChanged?.Invoke(this, new CurrentCultureChangedEventArgs(oldValue, newValue));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CultureManager"/> class.
        /// </summary>
        /// <param name="supportedCultureNames">An array that contains one or more culture names
        /// that need to be supported by the manager. Invalid culture names are ignored.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="supportedCultureNames"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="supportedCultureNames"/> contains no elements, or contains no supported culture names.
        /// </exception>
        /// <remarks>
        /// The manager's default culture is set to the first item of
        /// the <paramref name="supportedCultureNames"/> parameter.
        /// </remarks>
        public CultureManager(params string[] supportedCultureNames) :
            this(defaultCultureName: null, supportedCultureNames: supportedCultureNames)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CultureManager"/> class.
        /// </summary>
        /// <param name="defaultCultureName">The name of the manager's default culture.</param>
        /// <param name="supportedCultureNames">An array that contains one or more culture names
        /// that need to be supported by the manager. Invalid culture names are ignored.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="supportedCultureNames"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="supportedCultureNames"/> contains no elements, or contains no supported culture names.
        /// </exception>
        /// <remarks>
        /// If the <paramref name="defaultCultureName"/> parameter is not supported, the manager's default culture
        /// is set to the first item of the <paramref name="supportedCultureNames"/> parameter.
        /// </remarks>
        public CultureManager(string defaultCultureName, params string[] supportedCultureNames)
        {
            if (supportedCultureNames == null)
                throw new ArgumentNullException(nameof(supportedCultureNames));

            if (supportedCultureNames.Count() == 0)
                throw new ArgumentException(Resources.ValueContainsNoElements, nameof(supportedCultureNames));

            _supportedCultures = new Dictionary<string, CultureInfo>();

            foreach (var cultureName in supportedCultureNames)
            {
                var cultureInfo = GetCultureInfo(cultureName);

                if (cultureInfo == null)
                    continue;

                _supportedCultures.Add(cultureName, cultureInfo);
            }

            if (_supportedCultures.Count() == 0)
                throw new ArgumentException(Resources.ValueContainsNoSupportedElements, nameof(supportedCultureNames));

            _defaultCultureName = IsSpecificCultureSupported(defaultCultureName)
                ? defaultCultureName
                : _supportedCultures.First().Key;

            _currentCultureName = SetDefaultCulture();
            _supportedCultures = _supportedCultures.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Returns an instance of the <see cref="System.Globalization.CultureInfo"/> class
        /// based on the culture specified by name, or <c>null</c> if the culture is not
        /// supported by the current operating system.
        /// </summary>
        /// <param name="cultureName">The name of a culture.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureName"/> is <c>null</c>.</exception>
        public static CultureInfo GetCultureInfo(string cultureName)
        {
            try
            {
                return new CultureInfo(cultureName);
            }
            catch (ArgumentNullException)
            {
                Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, Resources.ValueNull, nameof(cultureName)));
                return null;
            }
#if NET35
            catch (ArgumentException)
#else
            catch (CultureNotFoundException)
#endif
            {
                Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, Resources.IllegalCultureName, cultureName));
                return null;
            }
        }

        /// <summary>
        /// Convert a given culture name to the name of it's associated neutral culture.
        /// </summary>
        /// <param name="cultureName">The name of the culture, representing a specific or neutral culture.</param>
        /// <returns>The name of the neutral culture, or <c>null</c> if the culture is not supported.</returns>
        public static string GetNeutralCultureName(string cultureName)
        {
#if NET35
            if (string.IsNullOrEmpty(cultureName))
#else
            if (string.IsNullOrWhiteSpace(cultureName))
#endif
                return null;

            var cultureInfo = GetCultureInfo(cultureName);

            if (cultureInfo == null)
                return null;

            if (!cultureInfo.IsNeutralCulture)
                cultureInfo = cultureInfo.Parent;

            return cultureInfo.Name;
        }

        /// <summary>
        /// Returns a value that indicates whether a given culture, or its associated neutral culture,
        /// is supported by this culture manager.
        /// </summary>
        /// <param name="cultureName">The name of the culture, representing a specific or neutral culture.</param>
        public bool IsCultureSupported(string cultureName)
        {
            if (IsSpecificCultureSupported(cultureName))
                return true;

            cultureName = GetNeutralCultureName(cultureName);

            if (cultureName == null)
                return false;

            return IsSpecificCultureSupported(cultureName);
        }

        /// <summary>
        /// Returns a value that indicates whether a given specific culture is supported by this culture manager.
        /// </summary>
        /// <param name="cultureName">The name of the culture.</param>
        /// <returns><c>true</c> if the culture is supported, <c>false</c> otherwise.</returns>
        public bool IsSpecificCultureSupported(string cultureName)
        {
            return _supportedCultures.ContainsKey(cultureName);
        }

        /// <summary>
        /// Returns a value that indicates whether a given culture, or it's associated neutral culture,
        /// is currently selected.
        /// </summary>
        /// <param name="cultureName">The name of the culture.</param>
        public bool IsCultureSelected(string cultureName)
        {
            return CurrentCultureName.StartsWith(cultureName, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Sets the application's current culture.
        /// If the given culture is not supported, the manager's default culture is selected.
        /// <para>
        /// If a given specific culture is not supported, its associated neutral culture is selected (if it exists).
        /// </para>
        /// </summary>
        /// <param name="cultureName">The name of the culture, representing a specific or neutral culture.</param>
        /// <returns>The name of the manager's culture when the operation finishes.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="cultureName"/> is <c>null</c>, empty, or white space.
        /// </exception>
        public string SetCulture(string cultureName)
        {
#if NET35
            if (string.IsNullOrEmpty(cultureName))
#else
            if (string.IsNullOrWhiteSpace(cultureName))
#endif
                throw new ArgumentException(Resources.ValueNullEmptyOrWhiteSpace, nameof(cultureName));

            if (!IsSpecificCultureSupported(cultureName))
                cultureName = GetNeutralCultureName(cultureName);

            return SetSpecificCulture(cultureName);
        }

        /// <summary>
        /// Sets the application's current culture, given an array of culture names.
        /// The manager selects the first culture that is supported, or the default culture
        /// if none of the specified cultures are supported.
        /// </summary>
        /// <param name="cultureNames">An array that contains zero or more culture names.</param>
        /// <returns>The name of the manager's culture when the operation finishes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cultureNames"/> is <c>null</c>.</exception>
        public string SetCulture(params string[] cultureNames)
        {
            if (cultureNames == null)
                throw new ArgumentNullException(nameof(cultureNames));

            var cultureName = cultureNames.FirstOrDefault(x => IsCultureSupported(x)) ?? DefaultCultureName;
            return SetCulture(cultureName);
        }

        /// <summary>
        /// Sets the application's current culture to the manager's default culture.
        /// </summary>
        /// <returns>The name of the manager's culture when the operation finishes.</returns>
        public string SetDefaultCulture()
        {
            return SetSpecificCulture(DefaultCultureName);
        }

        /// <summary>
        /// Sets the application's current culture to a specific culture.
        /// If the given culture is not supported, the manager's default culture is selected.
        /// </summary>
        /// <param name="cultureName">The name of the culture.</param>
        /// <returns>The name of the manager's culture when the operation finishes.</returns>
        public string SetSpecificCulture(string cultureName)
        {
            var newCultureName = IsSpecificCultureSupported(cultureName)
                ? cultureName
                : DefaultCultureName;

            var oldCultureName = CurrentCultureName;
            var newCulture = _supportedCultures[newCultureName];

#if NETSTANDARD1_6
            CultureInfo.CurrentCulture = newCulture;
            CultureInfo.CurrentUICulture = newCulture;
#else
            System.Threading.Thread.CurrentThread.CurrentCulture = newCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = newCulture;
#endif
            _currentCultureName = newCultureName;

            OnCurrentCultureChanged(oldCultureName, newCultureName);
            return newCultureName;
        }
    }
}
