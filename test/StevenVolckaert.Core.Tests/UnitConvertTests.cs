namespace StevenVolckaert
{
    using System.Collections.Generic;
    using Xunit;

    public class UnitConvertTests
    {
        /// <summary>
        ///     Returns a sequence of data that links degrees Celsius to degrees Fahrenheit.
        ///     Source http://www.rapidtables.com/convert/temperature/celsius-to-fahrenheit.htm
        /// </summary>
        public static IEnumerable<object[]> CelsiusToFahrenheitData()
        {
            yield return new object[] { -50, -58 };
            yield return new object[] { -40, -40 };
            yield return new object[] { -30, -22 };
            yield return new object[] { -20, -4 };
            yield return new object[] { -10, 14 };
            yield return new object[] { -9, 16 };
            yield return new object[] { -8, 18 };
            yield return new object[] { -7, 19 };
            yield return new object[] { -6, 21 };
            yield return new object[] { -5, 23 };
            yield return new object[] { -4, 25 };
            yield return new object[] { -3, 27 };
            yield return new object[] { -2, 28 };
            yield return new object[] { -1, 30 };
            yield return new object[] { 0, 32 };
            yield return new object[] { 1, 34 };
            yield return new object[] { 2, 36 };
            yield return new object[] { 3, 37 };
            yield return new object[] { 4, 39 };
            yield return new object[] { 5, 41 };
            yield return new object[] { 6, 43 };
            yield return new object[] { 7, 45 };
            yield return new object[] { 8, 46 };
            yield return new object[] { 9, 48 };
            yield return new object[] { 10, 50 };
            yield return new object[] { 20, 68 };
            yield return new object[] { 21, 70 };
            yield return new object[] { 30, 86 };
            yield return new object[] { 37, 99 };
            yield return new object[] { 40, 104 };
            yield return new object[] { 50, 122 };
            yield return new object[] { 60, 140 };
            yield return new object[] { 70, 158 };
            yield return new object[] { 80, 176 };
            yield return new object[] { 90, 194 };
            yield return new object[] { 100, 212 };
            yield return new object[] { 200, 392 };
            yield return new object[] { 300, 572 };
        }

        [Theory]
        [MemberData(nameof(CelsiusToFahrenheitData))]
        public void FromCelsiustoFahrenheit(int degreesInCelsius, int degreesInFahrenheit)
        {
            Assert.Equal(
                expected: degreesInFahrenheit,
                actual: UnitConvert.FromCelsiusToFahrenheit(degreesInCelsius)
            );
        }

        [Theory]
        [MemberData(nameof(CelsiusToFahrenheitData))]
        public void FromFahrenheitToCelsiusTest(int degreesInCelsius, int degreesInFahrenheit)
        {
            Assert.Equal(
                expected: degreesInCelsius,
                actual: UnitConvert.FromFahrenheitToCelsius(degreesInFahrenheit)
            );
        }
    }
}
