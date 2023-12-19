namespace KingUtils;

/// <summary>
/// Provides Base32 encoding and decoding functionality.
/// </summary>
public interface IBase32
{

#if NET5_0_OR_GREATER
    /// <summary>
    /// Encodes the specified bytes as a Base32 value.
    /// </summary>
    /// <param name="bytes">The bytes to encode.</param>
    /// <returns>A Base32 string representation of the bytes.</returns>
    string Encode(Span<byte> bytes);
#endif

    /// <summary>
    /// Encodes the specified bytes as a Base32 value.
    /// </summary>
    /// <param name="bytes">The bytes to encode.</param>
    /// <returns>A Base32 string representation of the bytes.</returns>
    string Encode(byte[] bytes);

    /// <summary>
    /// Decodes the specified Base32 string into a byte array.
    /// </summary>
    /// <param name="base32String">The Base32 string to decode.</param>
    /// <returns>A byte array containing the data in the string.</returns>
    byte[] Decode(string base32String);

}
