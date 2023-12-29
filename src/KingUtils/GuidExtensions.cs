namespace KingUtils;

/// <summary>
/// Extension methods for GUIDs.
/// </summary>
public static class GuidExtensions
{
    /// <summary>
    /// Returns a base-32 string representation of the GUID.
    /// Uses Crockford base-32 by default.
    /// </summary>
    /// <param name="guid">The GUID to format.</param>
    /// <returns>A base-32 string representation of the GUID.</returns>
    public static string ToBase32String(this Guid guid)
    {
        return ToBase32String(guid, Base32.Crockford);
    }

    /// <summary>
    /// Returns a base-32 string representation of the GUID.
    /// </summary>
    /// <param name="guid">The GUID to format.</param>
    /// <param name="base32Encoder">The base-32 encoder to use.</param>
    /// <returns>A base-32 string representation of the GUID.</returns>
    public static string ToBase32String(this Guid guid, IBase32 base32Encoder)
    {
#if NET7_0_OR_GREATER
        Span<byte> bytes = stackalloc byte[16];
        guid.TryWriteBytes(bytes);
        return base32Encoder.Encode(bytes);
#else
        return base32Encoder.Encode(guid.ToByteArray());
#endif
    }
}

