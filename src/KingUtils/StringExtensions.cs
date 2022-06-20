namespace KingUtils;

/// <summary>
/// Extension methods for strings.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Truncates the string at the specified length.
    /// </summary>
    /// <param name="value">The string to truncate.</param>
    /// <param name="length">The length of the truncated string.</param>
    /// <returns>The truncated string.</returns>
    public static string Truncate(this string value, int length)
    {
        return Truncate(value, length, null);
    }

    /// <summary>
    /// Truncates the string at the specified length.
    /// If a suffix is provided, the suffix will replace the end of the string.
    /// </summary>
    /// <param name="value">The string to truncate.</param>
    /// <param name="length">The length of the truncated string.</param>
    /// <param name="suffix">The suffix to append to the string.</param>
    /// <returns>The truncated string.</returns>
    public static string Truncate(this string value, int length, string suffix)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        if (suffix != null && suffix.Length >= length)
        {
            throw new ArgumentOutOfRangeException(nameof(suffix), "Suffix length must be shorter than truncation length");
        }

        if (value.Length > length)
        {
            if (suffix == null)
            {
                return value.Substring(0, length);
            }
            else
            {
                return value.Substring(0, length - suffix.Length) + suffix;
            }
        }
        else
        {
            return value;
        }
    }
}
