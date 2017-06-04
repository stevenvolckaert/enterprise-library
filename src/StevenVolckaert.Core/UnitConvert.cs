namespace StevenVolckaert
{
    using System;

    /// <summary>
    ///     Converts a unit of measurement to an equivalent unit of measurement.
    /// </summary>
    public static class UnitConvert
    {
        private const double MillimetersPerInch = 25.4;
        private const double MillimetersPerFoot = MillimetersPerInch * 12;
        private const double MillimetersPerYard = MillimetersPerFoot * 3;

        /// <summary>
        ///     The millimeters per DTP point. The DTP is defined as 1⁄72 of an international inch
        ///     (exactly 25.4 mm).
        /// </summary>
        /// <remarks>
        ///     See https://en.wikipedia.org/wiki/Point_(typography) for more information.
        /// </remarks>
        private const double MillimetersPerPoint = MillimetersPerInch / 72;

        private const double MetersPerFoot = MillimetersPerFoot / 1000;
        private const double MetersPerYard = MillimetersPerYard / 1000;

        /// <summary>
        ///     Converts a value in degrees Celsius (°C) to its equivalent in degrees Fahrenheit (°F).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in degrees Fahrenheit (°F).</returns>
        public static int FromCelsiusToFahrenheit(int value)
        {
            return FromCelsiusToFahrenheit((double)value);
        }

        /// <summary>
        ///     Converts a value in degrees Celsius (°C) to its equivalent in degrees Fahrenheit (°F).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in degrees Fahrenheit (°F).</returns>
        public static int FromCelsiusToFahrenheit(double value)
        {
            var calculatedValue = 9 / (decimal)5 * (decimal)value + 32;
            return Convert.ToInt32(Math.Round(calculatedValue));
        }

        /// <summary>
        ///     Converts a value in degrees Fahrenheit (°F) to its equivalent in degrees Celsius (°C).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in degrees Celsius (°C)</returns>
        public static int FromFahrenheitToCelsius(int value)
        {
            return FromFahrenheitToCelsius((double)value);
        }

        /// <summary>
        ///     Converts a value in degrees Fahrenheit (°F) to its equivalent in degrees Celsius (°C).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in degrees Celsius (°C)</returns>
        public static int FromFahrenheitToCelsius(double value)
        {
            var calculatedValue = 5 / (decimal)9 * ((decimal)value - 32);
            return Convert.ToInt32(Math.Round(calculatedValue));
        }

        /// <summary>
        ///     Converts a value in feet to its equivalent in meters.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in meters.</returns>
        public static double FromFeetToMeters(int value)
        {
            return FromFeetToMeters((double)value);
        }

        /// <summary>
        ///     Converts a value in feet to its equivalent in meters.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in meters.</returns>
        public static double FromFeetToMeters(double value)
        {
            return value * MetersPerFoot;
        }

        /// <summary>
        ///     Converts a value in international inches to its equivalent in millimeters.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in millimeters.</returns>
        public static double FromInchesToMillimeters(double value)
        {
            return value * MillimetersPerInch;
        }

        /// <summary>
        ///     Converts a value in meters to its equivalent in feet.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in feet.</returns>
        public static double FromMetersToFeet(int value)
        {
            return FromMetersToFeet((double)value);
        }

        /// <summary>
        ///     Converts a value in meters to its equivalent in feet.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in feet.</returns>
        public static double FromMetersToFeet(double value)
        {
            return value / MetersPerFoot;
        }

        /// <summary>
        ///     Converts a value in millimeters to its equivalent in international inches.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in international inches.</returns>
        public static double FromMillimetersToInches(double value)
        {
            return value / MillimetersPerInch;
        }

        /// <summary>
        ///     Converts a value in millimeters to its equivalent in DTPs (Desktop Publishing Points).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in DTPs (Desktop Publishing Points).</returns>
        public static double FromMillimetersToPoints(double value)
        {
            return value / MillimetersPerPoint;
        }

        /// <summary>
        ///     Converts a value in DTPs (Desktop Publishing Points) to its equivalent in millimeters.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value in millimeters.</returns>
        public static double FromPointsToMillimeters(double value)
        {
            return value * MillimetersPerPoint;
        }
    }
}
