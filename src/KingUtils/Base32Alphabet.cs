namespace KingUtils;

/// <summary>
/// Provides a number of standard Base32 alphabets.
/// </summary>
public static class Base32Alphabet
{
    /// <summary>
    /// Gets the RFC 4648 Base32 alphabet.
    /// </summary>
    public static string Rfc4648 { get; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

    /// <summary>
    /// Gets the Crockford Base32 alphabet.
    /// </summary>
    public static string Crockford { get; } = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";

    /// <summary>
    /// Gets the word-safe Base32 alphabet.
    /// </summary>
    public static string WordSafe { get; } = "23456789CFGHJMPQRVWXcfghjmpqrvwx";
}
