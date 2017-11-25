namespace StevenVolckaert.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class UnitConvertTests
    {
        /// <summary>
        ///     A list of tuples that links degrees Celsius to degrees Fahrenheit.
        ///     Source http://www.rapidtables.com/convert/temperature/celsius-to-fahrenheit.htm
        /// </summary>
        private static readonly List<Tuple<int, int>> _temperatureTuples =
            new List<Tuple<int, int>>
            {
                Tuple.Create(-50, -58),
                Tuple.Create(-40, -40),
                Tuple.Create(-30, -22),
                Tuple.Create(-20, -4),
                Tuple.Create(-10, 14),
                Tuple.Create(-9, 16),
                Tuple.Create(-8, 18),
                Tuple.Create(-7, 19),
                Tuple.Create(-6, 21),
                Tuple.Create(-5, 23),
                Tuple.Create(-4, 25),
                Tuple.Create(-3, 27),
                Tuple.Create(-2, 28),
                Tuple.Create(-1, 30),
                Tuple.Create(0, 32),
                Tuple.Create(1, 34),
                Tuple.Create(2, 36),
                Tuple.Create(3, 37),
                Tuple.Create(4, 39),
                Tuple.Create(5, 41),
                Tuple.Create(6, 43),
                Tuple.Create(7, 45),
                Tuple.Create(8, 46),
                Tuple.Create(9, 48),
                Tuple.Create(10, 50),
                Tuple.Create(20, 68),
                Tuple.Create(21, 70),
                Tuple.Create(30, 86),
                Tuple.Create(37, 99),
                Tuple.Create(40, 104),
                Tuple.Create(50, 122),
                Tuple.Create(60, 140),
                Tuple.Create(70, 158),
                Tuple.Create(80, 176),
                Tuple.Create(90, 194),
                Tuple.Create(100, 212),
                Tuple.Create(200, 392),
                Tuple.Create(300, 572),
            };

        [Fact]
        public void FromCelsiusToFahrenheitTest()
        {
            _temperatureTuples.ForEach(
                x => Assert.Equal(x.Item2, UnitConvert.FromCelsiusToFahrenheit(x.Item1))
            );
        }

        [Fact]
        public void FromFahrenheitToCelsiusTest()
        {
            _temperatureTuples.ForEach(
                x => Assert.Equal(x.Item1, UnitConvert.FromFahrenheitToCelsius(x.Item2))
            );
        }
    }
}
