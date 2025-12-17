namespace KingUtils;

/// <summary>
/// Extension methods for GUIDs.
/// </summary>
public static class GuidExtensions
{
    extension(Guid)
    {
        /// <summary>
        /// Converts the specified base-32 string into a GUID.
        /// </summary>
        /// <param name="base32String">The base-32 string to parse.</param>
        /// <param name="base32Encoder">The base-32 encoded/decoder to use.</param>
        /// <param name="guid">
        /// When this method returns, contains the parsed value. If the method returns true,
        /// result contains a valid System.Guid. If the method returns false, result equals
        /// System.Guid.Empty.
        /// </param>
        /// <returns>true if the parse operation was successful; otherwise, false.</returns>
        public static bool TryParseFromBase32(string base32String, IBase32 base32Encoder, out Guid guid)
        {
            if (string.IsNullOrEmpty(base32String) || base32String.Length != 26 || base32Encoder is null)
            {
                guid = Guid.Empty;
                return false;
            }

            try
            {
                var bytes = base32Encoder.Decode(base32String);
                guid = new Guid(bytes);
                return true;
            }
            catch (Exception)
            {
                guid = Guid.Empty;
                return false;
            }
        }
    }

    extension(Guid guid)
    {
        /// <summary>
        /// Returns a base-32 string representation of the GUID.
        /// Uses Crockford base-32 by default.
        /// </summary>
        /// <returns>A base-32 string representation of the GUID.</returns>
        public string ToBase32String()
        {
            return ToBase32String(guid, Base32.Crockford);
        }

        /// <summary>
        /// Returns a base-32 string representation of the GUID.
        /// </summary>
        /// <param name="base32Encoder">The base-32 encoder to use.</param>
        /// <returns>A base-32 string representation of the GUID.</returns>
        public string ToBase32String(IBase32 base32Encoder)
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
}
