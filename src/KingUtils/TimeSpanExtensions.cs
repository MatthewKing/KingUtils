namespace KingUtils;

/// <summary>
/// Extension methods for <see cref="TimeSpan"/>.
/// </summary>
public static class TimeSpanExtensions
{
    /// <summary>
    /// Returns a human readable string representation of the timespan.
    /// </summary>
    /// <param name="timespan">The timespan to format.</param>
    /// <param name="maxGranularity">The maximum granularity.</param>
    /// <returns>A human readable string representation of the timespan.</returns>
    public static string ToHumanReadableString(TimeSpan timespan, int maxGranularity)
    {
        const int MINUTES_PER_YEAR = 525600;
        const int MINUTES_PER_MONTH = 43800;
        const int MINUTES_PER_DAY = 1440;
        const int MINUTES_PER_HOUR = 60;

        var items = new List<string>();

        var isInFuture = timespan.TotalMinutes > 0;
        var minutes = Math.Abs((int)timespan.TotalMinutes);
        if (minutes > MINUTES_PER_YEAR)
        {
            var years = minutes / MINUTES_PER_YEAR;
            minutes -= years * MINUTES_PER_YEAR;
            items.Add(years == 1 ? "1 year" : $"{years} years");
        }
        if (minutes > MINUTES_PER_MONTH)
        {
            var months = minutes / MINUTES_PER_MONTH;
            minutes -= months * MINUTES_PER_MONTH;
            items.Add(months == 1 ? "1 month" : $"{months} months");
        }
        if (minutes > MINUTES_PER_DAY)
        {
            var days = minutes / MINUTES_PER_DAY;
            minutes -= days * MINUTES_PER_DAY;
            items.Add(days == 1 ? "1 day" : $"{days} days");
        }
        if (minutes > MINUTES_PER_HOUR)
        {
            var hours = minutes / MINUTES_PER_HOUR;
            minutes -= hours * MINUTES_PER_HOUR;
            items.Add(hours == 1 ? "1 hour" : $"{hours} hours");
        }
        if (minutes > 0)
        {
            items.Add(minutes == 1 ? "1 minute" : $"{minutes} minutes");
        }

        return string.Concat(items.Take(maxGranularity).ToJoinedString("and", true), isInFuture ? " from now" : " ago");
    }
}
