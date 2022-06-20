using System.ComponentModel;

namespace System.Collections.Generic;

/// <summary>
/// Extension methods for key/value pairs.
/// </summary>
public static class KeyValuePairExtensions
{
#if !NET5_0_OR_GREATER
    /// <summary>
    /// Deconstructs the specified <see cref="KeyValuePair{TKey, TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="keyValuePair">The <see cref="KeyValuePair{TKey, TValue}"/> to deconstruct.</param>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> keyValuePair, out TKey key, out TValue value)
    {
        key = keyValuePair.Key;
        value = keyValuePair.Value;
    }
#endif
}
