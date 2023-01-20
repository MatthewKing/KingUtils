namespace KingUtils;

/// <summary>
/// Extension methods to make dates/times/timespans human readable.
/// </summary>
public static class HumanReadableDateAndTimeExtensions
{
    private const int SECONDS_PER_YEAR = 31_536_000;
    private const int SECONDS_PER_MONTH = 2_628_000;
    private const int SECONDS_PER_DAY = 86_400;
    private const int SECONDS_PER_HOUR = 3_600;
    private const int SECONDS_PER_MINUTE = 60;

    /// <summary>
    /// Gets or sets the default precision to use, if no other is specified.
    /// </summary>
    public static int PrecisionDefault { get; set; } = 2;

    /// <summary>
    /// Gets or sets the minimum unit of time to use, if no other is specified.
    /// </summary>
    public static TimeUnit MinUnitDefault { get; set; } = TimeUnit.Second;

    /// <summary>
    /// Gets or sets the minimum unit of time to use, if no other is specified.
    /// </summary>
    public static TimeUnit MaxUnitDefault { get; set; } = TimeUnit.Year;

    /// <summary>
    /// Returns a human-readable representation of the value, relative to now.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>A human-readable representation of the value, relative to now.</returns>
    public static string ToRelativeHumanReadableString(this DateTimeOffset value)
    {
        return ToRelativeHumanReadableString(value, PrecisionDefault, MinUnitDefault, MaxUnitDefault);
    }

    /// <summary>
    /// Returns a human-readable representation of the value, relative to now.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <param name="precision">The precision of the output.</param>
    /// <returns>A human-readable representation of the value, relative to now.</returns>
    /// <remarks>
    /// Precision 1: 26 days ago
    /// /
    /// Precision 2: 26 days and 1 hour ago
    /// /
    /// Precision 3: 26 days, 1 hour, and 10 minutes ago
    /// </remarks>
    public static string ToRelativeHumanReadableString(this DateTimeOffset value, int precision)
    {
        return ToRelativeHumanReadableString(value, precision, MinUnitDefault, MaxUnitDefault);
    }

    /// <summary>
    /// Returns a human-readable representation of the value, relative to now.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <param name="precision">The precision of the output.</param>
    /// <param name="minUnit">The minimum time unit to include.</param>
    /// <param name="maxUnit">The maximum time unit to include.</param>
    /// <returns>A human-readable representation of the value, relative to now.</returns>
    /// <remarks>
    /// Precision 1: 26 days ago
    /// /
    /// Precision 2: 26 days and 1 hour ago
    /// /
    /// Precision 3: 26 days, 1 hour, and 10 minutes ago
    /// </remarks>
    public static string ToRelativeHumanReadableString(this DateTimeOffset value, int precision, TimeUnit minUnit, TimeUnit maxUnit)
    {
        var timespan = (value - DateTimeOffset.UtcNow);
        var timespanHumanReadable = timespan.ToHumanReadableString(precision, minUnit, maxUnit);

        if (timespanHumanReadable is null)
        {
            return null;
        }

        return timespan.TotalSeconds switch
        {
            < 0 => timespanHumanReadable + " ago",
            0 => "now", // Very unlikely to ever hit this case
            > 0 => timespanHumanReadable + " from now",
            _ => null, // Also very unlikely: NaN, etc.
        };
    }

    /// <summary>
    /// Returns a human-readable representation of the value.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>A human-readable representation of the value.</returns>
    public static string ToHumanReadableString(this TimeSpan value)
    {
        return ToHumanReadableString(value, PrecisionDefault, MinUnitDefault, MaxUnitDefault);
    }

    /// <summary>
    /// Returns a human-readable representation of the value.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <param name="precision">The precision of the output.</param>
    /// <returns>A human-readable representation of the value.</returns>
    /// <remarks>
    /// Precision 1: 26 days
    /// /
    /// Precision 2: 26 days and 1 hour
    /// /
    /// Precision 3: 26 days, 1 hour, and 10 minutes
    /// </remarks>
    public static string ToHumanReadableString(this TimeSpan value, int precision)
    {
        return ToHumanReadableString(value, precision, MinUnitDefault, MaxUnitDefault);
    }

    /// <summary>
    /// Returns a human-readable representation of the value.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <param name="precision">The precision of the output.</param>
    /// <param name="minUnit">The minimum time unit to include.</param>
    /// <param name="maxUnit">The maximum time unit to include.</param>
    /// <returns>A human-readable representation of the value.</returns>
    /// <remarks>
    /// Precision 1: 26 days
    /// /
    /// Precision 2: 26 days and 1 hour
    /// /
    /// Precision 3: 26 days, 1 hour, and 10 minutes
    /// </remarks>
    public static string ToHumanReadableString(this TimeSpan value, int precision, TimeUnit minUnit, TimeUnit maxUnit)
    {
        if (precision == 0)
        {
            return null;
        }

        if (maxUnit < minUnit)
        {
            return null;
        }

        var items = new List<string>();

        var seconds = Math.Abs((int)value.TotalSeconds);

        if (IsInRange(TimeUnit.Year, minUnit, maxUnit) && seconds > SECONDS_PER_YEAR)
        {
            var years = seconds / SECONDS_PER_YEAR;
            seconds -= years * SECONDS_PER_YEAR;
            items.Add(years == 1 ? "1 year" : $"{years} years");
        }

        if (IsInRange(TimeUnit.Month, minUnit, maxUnit) && seconds > SECONDS_PER_MONTH)
        {
            var months = seconds / SECONDS_PER_MONTH;
            seconds -= months * SECONDS_PER_MONTH;
            items.Add(months == 1 ? "1 month" : $"{months} months");
        }

        if (IsInRange(TimeUnit.Day, minUnit, maxUnit) && seconds > SECONDS_PER_DAY)
        {
            var days = seconds / SECONDS_PER_DAY;
            seconds -= days * SECONDS_PER_DAY;
            items.Add(days == 1 ? "1 day" : $"{days} days");
        }

        if (IsInRange(TimeUnit.Hour, minUnit, maxUnit) && seconds > SECONDS_PER_HOUR)
        {
            var hours = seconds / SECONDS_PER_HOUR;
            seconds -= hours * SECONDS_PER_HOUR;
            items.Add(hours == 1 ? "1 hour" : $"{hours} hours");
        }

        if (IsInRange(TimeUnit.Minute, minUnit, maxUnit) && seconds > SECONDS_PER_MINUTE)
        {
            var minutes = seconds / SECONDS_PER_MINUTE;
            seconds -= minutes * SECONDS_PER_MINUTE;
            items.Add(minutes == 1 ? "1 minute" : $"{minutes} minutes");
        }

        if (IsInRange(TimeUnit.Second, minUnit, maxUnit) && seconds > 0)
        {
            items.Add(seconds == 1 ? "1 second" : $"{seconds} seconds");
        }

        return items.Any() ? items.Take(precision).ToJoinedString("and", true) : null;
    }

    /// <summary>
    /// Returns a value indicating whether the time unit is within the specified range.
    /// </summary>
    /// <param name="unit">The unit to check.</param>
    /// <param name="min">The minimum value in the range.</param>
    /// <param name="max">The maximum value in the range.</param>
    /// <returns>true if the unit is within the range; otherwise, false.</returns>
    private static bool IsInRange(TimeUnit unit, TimeUnit min, TimeUnit max)
    {
        return (min <= unit) && (unit <= max);
    }
}
