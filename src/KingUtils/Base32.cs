using KingUtils.Internal;

namespace KingUtils;

/// <summary>
/// Provides Base32 encoding/decoding functionality.
/// </summary>
public static class Base32
{
    /// <summary>
    /// Gets the RFC 4648 Base32 encoder.
    /// </summary>
    public static IBase32 Rfc4648 => Rfc4648Lazy.Value;

    /// <summary>
    /// Gets the Crockford Base32 encoder.
    /// </summary>
    public static IBase32 Crockford => CrockfordLazy.Value;

    /// <summary>
    /// Gets the word-safe Base32 encoder.
    /// </summary>
    public static IBase32 WordSafe => WordSafeLazy.Value;

    /// <summary>
    /// Gets the lazy initializer for <see cref="Rfc4648"/>.
    /// </summary>
    private static Lazy<IBase32> Rfc4648Lazy { get; } = new Lazy<IBase32>(() => new Base32Default(Base32Alphabet.Rfc4648));

    /// <summary>
    /// Gets the lazy initializer for <see cref="Crockford"/>.
    /// </summary>
    private static Lazy<IBase32> CrockfordLazy { get; } = new Lazy<IBase32>(() => new Base32Crockford());

    /// <summary>
    /// Gets the lazy initializer for <see cref="WordSafe"/>.
    /// </summary>
    private static Lazy<IBase32> WordSafeLazy { get; } = new Lazy<IBase32>(() => new Base32Default(Base32Alphabet.WordSafe));
}
