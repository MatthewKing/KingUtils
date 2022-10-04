using System.Text;

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

    /// <summary>
    /// Returns a string created by joining the specified items.
    /// For example, the items ["A", "B", "C"] can be joined with ToJoinedString(items, "and", true) to "A, B, and C".
    /// </summary>
    /// <param name="items">The items to join.</param>
    /// <param name="coordinatingConjunction">The coordinating conjunction (generally "and" or "or").</param>
    /// <param name="serialComma">true if a serial comma should be used; otherwise, false.</param>
    /// <returns>A string created by joining the specified items.</returns>
    public static string ToJoinedString(this IEnumerable<string> items, string coordinatingConjunction, bool serialComma)
    {
        return ToJoinedString(items.ToArray(), coordinatingConjunction, serialComma);
    }

    /// <summary>
    /// Returns a string created by joining the specified items.
    /// For example, the items ["A", "B", "C"] can be joined with ToJoinedString(items, "and", true) to "A, B, and C".
    /// </summary>
    /// <param name="items">The items to join.</param>
    /// <param name="coordinatingConjunction">The coordinating conjunction (generally "and" or "or").</param>
    /// <param name="serialComma">true if a serial comma should be used; otherwise, false.</param>
    /// <returns>A string created by joining the specified items.</returns>
    public static string ToJoinedString(this IList<string> items, string coordinatingConjunction, bool serialComma)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        if (coordinatingConjunction == null)
        {
            throw new ArgumentNullException(nameof(coordinatingConjunction));
        }

        if (items.Count == 1)
        {
            return items[0];
        }
        else if (items.Count == 2)
        {
            return $"{items[0]} {coordinatingConjunction} {items[1]}";
        }
        else if (items.Count > 2)
        {
            var sb = new StringBuilder();

            sb.Append(items[0]);

            for (int i = 1; i < items.Count - 1; i++)
            {
                sb.Append(", ");
                sb.Append(items[i]);
            }

            if (serialComma)
            {
                sb.Append(",");
            }

            sb.Append(" ");
            sb.Append(coordinatingConjunction);
            sb.Append(" ");

            sb.Append(items[items.Count - 1]);

            return sb.ToString();
        }
        else
        {
            return string.Empty;
        }
    }
}
