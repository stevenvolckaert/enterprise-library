namespace StevenVolckaert.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnitConvertTests
    {
        /// <summary>
        ///     A list of tuples that links degrees Celsius to degrees Fahrenheit.
        ///     Source http://www.rapidtables.com/convert/temperature/celsius-to-fahrenheit.htm
        /// </summary>
        private static readonly List<Tuple<int, int>> _temperatureTuples =
            new List<Tuple<int, int>>
            {
                new Tuple<int, int>(-50, -58),
                new Tuple<int, int>(-40, -40),
                new Tuple<int, int>(-30, -22),
                new Tuple<int, int>(-20, -4),
                new Tuple<int, int>(-10, 14),
                new Tuple<int, int>(-9, 16),
                new Tuple<int, int>(-8, 18),
                new Tuple<int, int>(-7, 19),
                new Tuple<int, int>(-6, 21),
                new Tuple<int, int>(-5, 23),
                new Tuple<int, int>(-4, 25),
                new Tuple<int, int>(-3, 27),
                new Tuple<int, int>(-2, 28),
                new Tuple<int, int>(-1, 30),
                new Tuple<int, int>(0, 32),
                new Tuple<int, int>(1, 34),
                new Tuple<int, int>(2, 36),
                new Tuple<int, int>(3, 37),
                new Tuple<int, int>(4, 39),
                new Tuple<int, int>(5, 41),
                new Tuple<int, int>(6, 43),
                new Tuple<int, int>(7, 45),
                new Tuple<int, int>(8, 46),
                new Tuple<int, int>(9, 48),
                new Tuple<int, int>(10, 50),
                new Tuple<int, int>(20, 68),
                new Tuple<int, int>(21, 70),
                new Tuple<int, int>(30, 86),
                new Tuple<int, int>(37, 99),
                new Tuple<int, int>(40, 104),
                new Tuple<int, int>(50, 122),
                new Tuple<int, int>(60, 140),
                new Tuple<int, int>(70, 158),
                new Tuple<int, int>(80, 176),
                new Tuple<int, int>(90, 194),
                new Tuple<int, int>(100, 212),
                new Tuple<int, int>(200, 392),
                new Tuple<int, int>(300, 572),
            };

        [TestMethod]
        public void FromCelsiusToFahrenheitTest()
        {
            _temperatureTuples.ForEach(
                x => Assert.AreEqual(x.Item2, UnitConvert.FromCelsiusToFahrenheit(x.Item1))
            );
        }

        [TestMethod]
        public void FromFahrenheitToCelsiusTest()
        {
            _temperatureTuples.ForEach(
                x => Assert.AreEqual(x.Item1, UnitConvert.FromFahrenheitToCelsius(x.Item2))
            );
        }
    }
}
