using System.Globalization;
using System.Text;

namespace KingUtils;

/// <summary>
/// Provides Base16 encoding/decoding functionality.
/// </summary>
public static class Base16
{
    /// <summary>
    /// Encodes the specified byte array as a Base16 value.
    /// </summary>
    /// <param name="bytes">The byte array to encode.</param>
    /// <returns>A Base16 string representation of the byte array.</returns>
    public static string Encode(byte[] bytes)
    {
        if (bytes is null)
        {
            throw new ArgumentNullException(nameof(bytes));
        }

        var sb = new StringBuilder(bytes.Length * 2);
        for (int i = 0; i < bytes.Length; i++)
        {
            sb.AppendFormat("{0:X2}", bytes[i]);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Decodes the specified Base16 string into a byte array.
    /// </summary>
    /// <param name="base16String">The Base16 string to decode.</param>
    /// <returns>A byte array containing the data in the string.</returns>
    public static byte[] Decode(string base16String)
    {
        if (base16String is null)
        {
            throw new ArgumentNullException(nameof(base16String));
        }

        if (base16String.Length % 2 != 0)
        {
            throw new ArgumentException("Value cannot have an odd number of digits", nameof(base16String));
        }

        if (base16String.Length == 0)
        {
            return Array.Empty<byte>();
        }

        var data = new byte[base16String.Length / 2];
        for (var index = 0; index < data.Length; index++)
        {
            var byteValue = base16String.Substring(index * 2, 2);
            data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        }

        return data;
    }
}
