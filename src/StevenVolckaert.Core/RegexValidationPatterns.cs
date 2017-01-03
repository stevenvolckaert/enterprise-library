namespace StevenVolckaert
{
    /// <summary>
    /// Provides access to regular expression patterns that are used to validate strings.
    /// </summary>
    public static class RegexValidationPatterns
    {
        /// <summary>
        /// A regular expression pattern used to validate strings that represent
        /// a single ISO 3166-1 alpha-2 country code.
        /// </summary>
        public const string CountryCode = @"^[A-Z]{2}$";

        /// <summary>
        /// A regular expression pattern used to validate strings that represent
        /// a single culture name, as used by the <see cref="System.Globalization.CultureInfo"/> class.
        /// </summary>
        public const string CultureName = @"[a-z]{2}(-[A-Za-z]{2,8})?";

        /// <summary>
        /// A regular expression pattern used to validate strings that represent a single email address.
        /// </summary>
        public const string EmailAddress =
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        /// <summary>
        /// A regular expression pattern used to validate strings that represent a 32-bit signed integer.
        /// </summary>
        public const string Int32 = @"^(-)?[0-9]+$";
    }
}
