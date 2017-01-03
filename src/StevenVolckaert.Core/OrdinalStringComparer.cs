namespace StevenVolckaert
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    ///     Defines a method for comparing strings according to their ordinal value.
    ///     <para>This class cannot be inherited.</para>
    /// </summary>
    /// <remarks>
    ///     See https://code.msdn.microsoft.com/windowsdesktop/Ordinal-String-Sorting-1cbac582 for more
    ///     information.
    /// </remarks>
    internal sealed class OrdinalStringComparer : IComparer<string>
    {
        private bool _ignoreCase;

        /// <summary>
        ///     Initializes a new instance of the <see cref="OrdinalStringComparer"/> class.
        /// </summary>
        /// <param name="ignoreCase">A value that indicates whether to ignore case during comparison.</param>
        public OrdinalStringComparer(bool ignoreCase)
        {
            _ignoreCase = ignoreCase;
        }

        /// <summary>
        ///     Compares two string values and returns a value that indicates whether one is less than, equal to,
        ///     or greater than the other.
        /// </summary>
        /// <param name="x">The first string value to compare.</param>
        /// <param name="y">The second string value to compare.</param>
        /// <returns>
        ///     A signed integer that indicates the relative values of x and y, as in the Compare method
        ///     in the <see cref="IComparer{T}"/> interface.
        /// </returns>
        /// <remarks>
        ///     Implementing this method with string.Compare(String, String, StringComparison)
        ///     does not produce the desired results.
        /// </remarks>
        public int Compare(string x, string y)
        {
            // Check for null values first: A null reference is considered to be less than
            // any reference that is not null.
            if (x == null && y == null)
                return 0;

            if (x == null)
                return -1;

            if (y == null)
                return 1;

            var comparisonMode = _ignoreCase
                ? StringComparison.CurrentCultureIgnoreCase
                : StringComparison.CurrentCulture;

            string[] splitX = Regex.Split(x.Replace(" ", ""), "([0-9]+)");
            string[] splitY = Regex.Split(y.Replace(" ", ""), "([0-9]+)");

            int comparer = 0;

            for (int i = 0; comparer == 0; i++)
            {
                if (i >= splitX.Length && i >= splitY.Length)
                {
                    // x = y
                    comparer = 0;
                    break;
                }
                else if (i >= splitX.Length)
                    // x < y
                    comparer = -1;
                else if (i >= splitY.Length)
                    // x > y
                    comparer = 1;
                else
                {
                    var numericX = -1;
                    var numericY = -1;

                    if (int.TryParse(splitX[i], out numericX))
                        comparer = int.TryParse(splitY[i], out numericY)
                            ? numericX - numericY
                            // x > y
                            : 1;
                    else
                        comparer = string.Compare(splitX[i], splitY[i], comparisonMode);
                }
            }

            return comparer;
        }
    }
}
