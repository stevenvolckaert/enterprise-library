namespace StevenVolckaert
{
    /// <summary>
    ///     Specifies a prefix that is used in combination with a unit of information (e.g. byte).
    /// </summary>
    public enum UnitOfInformationPrefix
    {
        /// <summary>
        ///     No prefix, indicating a power of 1.
        /// </summary>
        None = 0,
        /// <summary>
        /// A decimal prefix, indicating a power of 1000.
        /// </summary>
        Decimal = 1000,
        /// <summary>
        /// A binary prefix, indicating a power of 1024.
        /// </summary>
        Binary = 1024
    }
}
