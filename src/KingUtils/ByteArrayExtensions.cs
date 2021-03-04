using System;
using System.Text;

namespace KingUtils
{
    /// <summary>
    /// Extension methods for byte arrays.
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Returns a hex string representation of the specified byte array.
        /// </summary>
        /// <param name="bytes">The byte array to convert.</param>
        /// <returns>A hex string representation of the specified byte array.</returns>
        public static string ToHexString(this byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            var sb = new StringBuilder();

            if (bytes != null)
            {
                foreach (var b in bytes)
                {
                    sb.AppendFormat("{0:x2}", b);
                }
            }

            return sb.ToString();
        }
    }
}
