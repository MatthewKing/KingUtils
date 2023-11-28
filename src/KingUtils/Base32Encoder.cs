using System.Text;

namespace KingUtils;

/// <summary>
/// Provides Base32 encoding/decoding functionality.
/// </summary>
public sealed class Base32Encoder
{
    private const int Shift = 5;
    private const int Mask = 31;

    private string _alphabet;
    private IDictionary<char, int> _alphabetMap;

    /// <summary>
    /// Initializes a new instance of the <see cref="Base32Encoder"/> class.
    /// </summary>
    /// <param name="alphabet">The Base32 alphabet to use.</param>
    public Base32Encoder(string alphabet)
    {
        if (alphabet is null)
        {
            throw new ArgumentNullException(nameof(alphabet));
        }

        if (alphabet.Length != 32)
        {
            throw new ArgumentException(nameof(alphabet));
        }

        _alphabet = alphabet;

        _alphabetMap = new Dictionary<char, int>(_alphabet.Length);
        for (int i = 0; i < _alphabet.Length; i++)
        {
            _alphabetMap[_alphabet[i]] = i;
        }
    }

#if NET5_0_OR_GREATER
    /// <summary>
    /// Encodes the specified bytes as a Base32 value.
    /// </summary>
    /// <param name="bytes">The bytes to encode.</param>
    /// <returns>A Base32 string representation of the bytes.</returns>
    public string Encode(Span<byte> bytes)
    {
        if (bytes.Length == 0)
        {
            return string.Empty;
        }

        var outputLength = (bytes.Length * 8 + Shift - 1) / Shift;
        var sb = new StringBuilder(outputLength);

        var offset = 0;
        var last = bytes.Length;
        int buffer = bytes[offset++];
        var bitsLeft = 8;
        while (bitsLeft > 0 || offset < last)
        {
            if (bitsLeft < Shift)
            {
                if (offset < last)
                {
                    buffer <<= 8;
                    buffer |= bytes[offset++] & 0xff;
                    bitsLeft += 8;
                }
                else
                {
                    var pad = Shift - bitsLeft;
                    buffer <<= pad;
                    bitsLeft += pad;
                }
            }

            var index = Mask & (buffer >> (bitsLeft - Shift));
            bitsLeft -= Shift;
            sb.Append(_alphabet[index]);
        }

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
        if (bytes == null)
        {
            throw new ArgumentNullException(nameof(bytes));
        }

        if (bytes.Length == 0)
        {
            return string.Empty;
        }

        var outputLength = (bytes.Length * 8 + Shift - 1) / Shift;
        var sb = new StringBuilder(outputLength);

        var offset = 0;
        var last = bytes.Length;
        int buffer = bytes[offset++];
        var bitsLeft = 8;
        while (bitsLeft > 0 || offset < last)
        {
            if (bitsLeft < Shift)
            {
                if (offset < last)
                {
                    buffer <<= 8;
                    buffer |= bytes[offset++] & 0xff;
                    bitsLeft += 8;
                }
                else
                {
                    var pad = Shift - bitsLeft;
                    buffer <<= pad;
                    bitsLeft += pad;
                }
            }

            var index = Mask & (buffer >> (bitsLeft - Shift));
            bitsLeft -= Shift;
            sb.Append(_alphabet[index]);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Decodes the specified Base32 string into a byte array.
    /// </summary>
    /// <param name="base32String">The Base32 string to decode.</param>
    /// <returns>A byte array containing the data in the string.</returns>
    public byte[] Decode(string base32String)
    {
        if (base32String == null)
        {
            throw new ArgumentNullException(nameof(base32String));
        }

        if (base32String.Length == 0)
        {
            return Array.Empty<byte>();
        }

        var byteCount = base32String.Length * Shift / 8;
        var bytes = new byte[byteCount];

        var buffer = 0;
        var bitsLeft = 0;
        var index = 0;

        foreach (var c in base32String)
        {
            if (!_alphabetMap.TryGetValue(c, out var value))
            {
                throw new ArgumentException($"Invalid character '{c}' in input string. Only characters from the alphabet '{_alphabet}' are allowed.", nameof(base32String));
            }

            buffer = (buffer << Shift) | value;
            bitsLeft += Shift;

            if (bitsLeft >= 8)
            {
                bytes[index++] = (byte)(buffer >> (bitsLeft - 8));
                bitsLeft -= 8;
            }
        }

        return bytes;
    }
}
