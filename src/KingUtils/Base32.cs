namespace KingUtils;

/// <summary>
/// Provides Base32 encoding/decoding functionality.
/// </summary>
public static class Base32
{
    /// <summary>
    /// Gets the RFC 4648 Base32 encoder.
    /// </summary>
    public static Base32Encoder Rfc4648 => Rfc4648Lazy.Value;

    /// <summary>
    /// Gets the Crockford Base32 encoder.
    /// </summary>
    public static Base32Encoder Crockford => CrockfordLazy.Value;

    /// <summary>
    /// Gets the word-safe Base32 encoder.
    /// </summary>
    public static Base32Encoder WordSafe => WordSafeLazy.Value;

    /// <summary>
    /// Gets the lazy initializer for <see cref="Rfc4648"/>.
    /// </summary>
    private static Lazy<Base32Encoder> Rfc4648Lazy { get; } = new Lazy<Base32Encoder>(() => new Base32Encoder(Base32Alphabet.Rfc4648));

    /// <summary>
    /// Gets the lazy initializer for <see cref="Crockford"/>.
    /// </summary>
    private static Lazy<Base32Encoder> CrockfordLazy { get; } = new Lazy<Base32Encoder>(() => new Base32Encoder(Base32Alphabet.Crockford));

    /// <summary>
    /// Gets the lazy initializer for <see cref="WordSafe"/>.
    /// </summary>
    private static Lazy<Base32Encoder> WordSafeLazy { get; } = new Lazy<Base32Encoder>(() => new Base32Encoder(Base32Alphabet.WordSafe));
}
