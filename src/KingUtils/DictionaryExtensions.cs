using System;
using System.Collections.Generic;

namespace KingUtils
{
    /// <summary>
    /// Extension methods for dictionaries.
    /// </summary>
    public static class DictionaryExtensions
    {
#if !NET5_0_OR_GREATER
        /// <summary>
        /// Returns the value associated with the specified key, or the default for the value
        /// type if no value was found.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">A dictionary with keys of type TKey and values of type TValue.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>
        /// A TValue instance.
        /// When the method is successful, the returned object is the value associated with the specified key.
        /// When the method fails, it returns the default value for TValue.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return GetValueOrDefault(dictionary, key, default);
        }

        /// <summary>
        /// Returns the value associated with the specified key, or the default for the value
        /// type if no value was found.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">A dictionary with keys of type TKey and values of type TValue.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="defaultValue">The default value to return when the dictionary cannot find a value associated with the specified key.</param>
        /// <returns>
        /// A TValue instance.
        /// When the method is successful, the returned object is the value associated with the specified key.
        /// When the method fails, it returns defaultValue.
        /// </returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }
#endif
    }
}
