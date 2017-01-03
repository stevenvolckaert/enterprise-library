namespace StevenVolckaert
{
    using System;
    using System.Text;

    /// <summary>
    ///     Provides extension methods for <see cref="TimeSpan"/> instances.
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        ///     Returns a formatted string that represents the current <see cref="TimeSpan"/> in the language
        ///     of the application's current culture.
        /// </summary>
        /// <param name="timeSpan">The <see cref="TimeSpan"/> instance that this extension method affects.</param>
        public static string ToFormattedString(this TimeSpan timeSpan)
        {
            if (timeSpan.Seconds < 60 && timeSpan.Minutes == 0 && timeSpan.Hours == 0 && timeSpan.Days == 0)
                return Resources.LessThanOneMinute;

            if (timeSpan.Minutes < 60 && timeSpan.Hours == 0 && timeSpan.Days == 0)
                return timeSpan.Minutes.ToFormattedString(
                    singularSuffix: Resources.Minute,
                    pluralSuffix: Resources.Minutes
                );

            var stringBuilder = new StringBuilder();

            if (timeSpan.Hours < 24 && timeSpan.Days == 0)
            {
                stringBuilder.Append(
                    timeSpan.Hours.ToFormattedString(
                        singularSuffix: Resources.Hour,
                        pluralSuffix: Resources.Hours
                    )
                );

                if (timeSpan.Minutes > 0)
                {
                    stringBuilder.Append(", ");
                    stringBuilder.Append(
                        timeSpan.Minutes.ToFormattedString(
                            singularSuffix: Resources.Minute,
                            pluralSuffix: Resources.Minutes
                        )
                    );
                }

                return stringBuilder.ToString();
            }

            stringBuilder.Append(
                timeSpan.Days.ToFormattedString(singularSuffix: Resources.Day, pluralSuffix: Resources.Days)
            );

            if (timeSpan.Hours > 0)
            {
                stringBuilder.Append(", ");
                stringBuilder.Append(
                    timeSpan.Hours.ToFormattedString(
                        singularSuffix: Resources.Hour,
                        pluralSuffix: Resources.Hours
                    )
                );

                return stringBuilder.ToString();
            }

            if (timeSpan.Minutes > 0)
            {
                stringBuilder.Append(", ");
                stringBuilder.Append(
                    timeSpan.Minutes.ToFormattedString(
                        singularSuffix: Resources.Minute, pluralSuffix: Resources.Minutes
                    )
                );

                return stringBuilder.ToString();
            }

            return stringBuilder.ToString();
        }
    }
}
