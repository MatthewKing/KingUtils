using System.Text;

namespace KingUtils.Internal;

/// <summary>
/// An implementation of <see cref="IBase32"/> that uses Crockford's Base32.
/// </summary>
internal sealed class Base32Crockford : IBase32
{

#if NET5_0_OR_GREATER
    /// <summary>
    /// Encodes the specified bytes as a Base32 value.
    /// </summary>
    /// <param name="bytes">The bytes to encode.</param>
    /// <returns>A Base32 string representation of the bytes.</returns>
    public string Encode(Span<byte> bytes)
    {
        var sb = new StringBuilder();
        Crockbase32.Encode(bytes, 0, bytes.Length, sb);
        return sb.ToString();
    }
#endif

    /// <summary>
    /// Encodes the specified byte array as a Base32 value.
    /// </summary>
    /// <param name="bytes">The byte array to encode.</param>
    /// <returns>A Base32 string representation of the byte array.</returns>
    public string Encode(byte[] bytes)
    {
        var sb = new StringBuilder();
        Crockbase32.Encode(bytes, 0, bytes.Length, sb);
        return sb.ToString();
    }

    /// <summary>
    /// Decodes the specified Base32 string into a byte array.
    /// </summary>
    /// <param name="base32String">The Base32 string to decode.</param>
    /// <returns>A byte array containing the data in the string.</returns>
    public byte[] Decode(string base32String)
    {
        return Crockbase32.Decode(base32String);
    }

}
