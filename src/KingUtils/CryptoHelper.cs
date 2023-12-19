using System.Security.Cryptography;
using System.Text;

namespace KingUtils;

/// <summary>
/// Provides some simple cryptography helper methods.
/// </summary>
public static class CryptoHelper
{
    /// <summary>
    /// The delimiter to use to separate the data, salt, and iterations in the encrypted string.
    /// Has to be something that won't otherwise occur in the string.
    /// Since we're using Base32 Crockford, we can use any of the omitted characters.
    /// </summary>
    private const char DELIMITER = 'L';

    /// <summary>
    /// Encrypts the specified data using the specified password, salt length, and number of iterations.
    /// </summary>
    /// <param name="data">The data to encrypt.</param>
    /// <param name="password">The password to use.</param>
    /// <param name="saltLength">The length of the salt.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <param name="hashAlgorithmName">The hash algorithm to use with PBKDF2.</param>
    /// <returns>An string containing the encrypted data, the salt, and the number of iterations.</returns>
    public static string EncryptBytes(byte[] data, string password, int saltLength, int iterations, HashAlgorithmName hashAlgorithmName)
    {
        var salt = GetRandomSalt(saltLength);
        var (key, iv) = GetKeyAndIV(password, salt, iterations, hashAlgorithmName);
        var encryptedBytes = EncryptUsingAes(key, iv, data);
        var iterationsBytes = ConvertIntToByteArrayEndianAgnostic(iterations);
        var output = string.Join(DELIMITER.ToString(),
            Base32.Crockford.Encode(encryptedBytes),
            Base32.Crockford.Encode(salt),
            Base32.Crockford.Encode(iterationsBytes));

        return output;
    }

    /// <summary>
    /// Encrypts the specified data using the specified password, salt length, and number of iterations.
    /// </summary>
    /// <param name="data">The data to encrypt.</param>
    /// <param name="password">The password to use.</param>
    /// <param name="saltLength">The length of the salt.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <param name="hashAlgorithmName">The hash algorithm to use with PBKDF2.</param>
    /// <returns>An string containing the encrypted data, the salt, and the number of iterations.</returns>
    public static string EncryptString(string data, string password, int saltLength, int iterations, HashAlgorithmName hashAlgorithmName)
    {
        return EncryptBytes(Encoding.UTF8.GetBytes(data), password, saltLength, iterations, hashAlgorithmName);
    }

    /// <summary>
    /// Decrypt the specified value, using the specified password. Return the data as a byte array.
    /// </summary>
    /// <param name="value">The value to decrypt.</param>
    /// <param name="password">The password to use.</param>
    /// <param name="hashAlgorithmName">The hash algorithm to use with PBKDF2.</param>
    /// <returns>The decrypted data.</returns>
    public static byte[] DecryptBytes(string value, string password, HashAlgorithmName hashAlgorithmName)
    {
        var segments = value.Split(DELIMITER);
        var data = Base32.Crockford.Decode(segments[0]);
        var salt = Base32.Crockford.Decode(segments[1]);
        var iterationsBytes = Base32.Crockford.Decode(segments[2]);
        var iterations = ConvertByteArrayToIntEndianAgnostic(iterationsBytes);
        var (key, iv) = GetKeyAndIV(password, salt, iterations, hashAlgorithmName);

        var decryptedBytes = DecryptUsingAes(key, iv, data);

        return decryptedBytes;
    }

    /// <summary>
    /// Decrypt the specified value, using the specified password. Return the data as a string.
    /// </summary>
    /// <param name="value">The value to decrypt.</param>
    /// <param name="password">The password to use.</param>
    /// <param name="hashAlgorithmName">The hash algorithm to use with PBKDF2.</param>
    /// <returns>The decrypted data.</returns>
    public static string DecryptString(string value, string password, HashAlgorithmName hashAlgorithmName)
    {
        return Encoding.UTF8.GetString(DecryptBytes(value, password, hashAlgorithmName));
    }

    /// <summary>
    /// Returns a random salt of the specified length.
    /// </summary>
    /// <param name="length">The salt length.</param>
    /// <returns>A random salt of the specified length.</returns>
    public static byte[] GetRandomSalt(int length)
    {
#if NET6_0_OR_GREATER
        return RandomNumberGenerator.GetBytes(length);
#else
        var salt = new byte[length];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
#endif
    }

    private static (byte[], byte[]) GetKeyAndIV(string password, byte[] salt, int iterations, HashAlgorithmName hashAlgorithmName)
    {
#if NET6_0_OR_GREATER
        var deriveBytes = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithmName, 48);
        var key = deriveBytes[0..32];
        var iv = deriveBytes[32..48];
        return (key, iv);
#else
#if NETSTANDARD2_0
        if (hashAlgorithmName != HashAlgorithmName.SHA1)
        {
            throw new NotSupportedException("Only SHA1 is supported on NETSTANDARD 2.0.");
        }

        var passwordBytes = Encoding.UTF8.GetBytes(password);
        using var deriveBytes = new Rfc2898DeriveBytes(passwordBytes, salt, iterations);
#else
        using var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations, hashAlgorithmName);
#endif
        var key = deriveBytes.GetBytes(32);
        var iv = deriveBytes.GetBytes(16);
        return (key, iv);
#endif
    }

    private static byte[] EncryptUsingAes(byte[] key, byte[] iv, byte[] data)
    {
#if NET6_0_OR_GREATER
        using var aes = Aes.Create();
        aes.Key = key;
        return aes.EncryptCbc(data, iv);
#else
        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        using var transform = aes.CreateEncryptor();
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
        cryptoStream.Write(data, 0, data.Length);
        cryptoStream.FlushFinalBlock();

        return memoryStream.ToArray();
#endif
    }

    private static byte[] DecryptUsingAes(byte[] key, byte[] iv, byte[] data)
    {
#if NET6_0_OR_GREATER
        using var aes = Aes.Create();
        aes.Key = key;

        return aes.DecryptCbc(data, iv);
#else
        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        using var transform = aes.CreateDecryptor();
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
        cryptoStream.Write(data, 0, data.Length);
        cryptoStream.FlushFinalBlock();

        return memoryStream.ToArray();
#endif
    }

    private static int ConvertByteArrayToIntEndianAgnostic(byte[] bytes)
    {
        var workingBytes = new byte[4];
        Array.Copy(bytes, workingBytes, bytes.Length);

        if (!BitConverter.IsLittleEndian)
        {
            Array.Reverse(workingBytes);
        }

        return BitConverter.ToInt32(workingBytes, 0);
    }

    private static byte[] ConvertIntToByteArrayEndianAgnostic(int value)
    {
        var bytes = BitConverter.GetBytes(value);
        if (!BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }

        var length = Array.FindLastIndex(bytes, b => b != 0) + 1;
        if (length <= 0)
        {
            return bytes;
        }
        else
        {
            var bytesWithoutTrailingZeros = new byte[length];
            Array.Copy(bytes, bytesWithoutTrailingZeros, length);
            return bytesWithoutTrailingZeros;
        }
    }
}
