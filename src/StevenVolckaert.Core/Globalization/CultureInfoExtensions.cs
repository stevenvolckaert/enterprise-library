namespace StevenVolckaert.Globalization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    ///     Provides extension methods for <see cref="CultureInfo"/> instances.
    /// </summary>
    public static class CultureInfoExtensions
    {
        private static readonly Dictionary<string, string> _cultureNametoMarcLanguageCodeDictionary =
            new Dictionary<string, string>
            {
                { "zh-Hans", "chi"},
                { "zh-CHS", "chi"},
                // REMARK We could return "cht" for zh-Hant (Traditional Chinese),
                // but this code is *not* an official MARC 21 language code.
                { "zh-Hant", "chi"},
                { "zh-CHT", "chi"},
            };

        private static readonly Dictionary<string, string> _twoLetterISOLanguageNameToMarcLanguageCodeDictionary =
            new Dictionary<string, string>
            {
                { "ar", "ara" },
                { "nl", "dut" },
                { "en", "eng" },
                { "fr", "fre" },
                { "de", "ger" },
                { "el", "gre" },
                { "he", "heb" },
                { "hi", "ind" },
                { "it", "ita" },
                { "pl", "pol" },
                { "pt", "por" },
                { "ru", "rus" },
                { "es", "spa" },
                { "tr", "tur" },
                { "uk", "ukr" },
                { "ur", "urd" },
                { "vi", "vie" },
                { "zh", "chi" },
            };

        /// <summary>
        ///     Returns the MARC 21 three-letter code for the language associated with
        ///     the current <see cref="CultureInfo"/> instance.
        /// </summary>
        /// <param name="cultureInfo">
        ///     The <see cref="CultureInfo"/> instance this extension method affects.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureInfo"/> is <c>null</c>.</exception>
        /// <exception cref="NotSupportedException">
        ///     Translating <paramref name="cultureInfo"/> to the associated MARC 21 three-letter language code
        ///     is not supported.
        /// </exception>
        /// <remarks>
        ///     See http://en.wikipedia.org/wiki/MARC_standards#MARC_21 for more information.
        ///     See http://www.loc.gov/marc/languages/language_code.html for the official
        ///     MARC 21 Code List for Languages.
        /// </remarks>
        public static string Marc21LanguageCode(this CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
                throw new ArgumentNullException(nameof(cultureInfo));
            
            string returnValue;

            if (_cultureNametoMarcLanguageCodeDictionary.TryGetValue(cultureInfo.Name, out returnValue))
                return returnValue;

            if (_twoLetterISOLanguageNameToMarcLanguageCodeDictionary.TryGetValue(
                    key: cultureInfo.TwoLetterISOLanguageName,
                    value: out returnValue
                )
            )
                return returnValue;

            throw new NotSupportedException(
                string.Format(CultureInfo.CurrentCulture, Resources.ValueNotSupported, cultureInfo.Name)
            );
        }
    }
}
