using System;
using System.Collections.Generic;

namespace KingUtils;

/// <summary>
/// Extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class EnumerableExtensions
{
#if !NET6_0_OR_GREATER

    /// <summary>
    /// Split the elements of a sequence into chunks of size at most <paramref name="size"/>.
    /// </summary>
    /// <remarks>
    /// Every chunk except the last will be of size <paramref name="size"/>.
    /// The last chunk will contain the remaining elements and may be of a smaller size.
    /// </remarks>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> whose elements to chunk.</param>
    /// <param name="size">Maximum size of each chunk.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> that contains the elements the input sequence
    /// split into chunks of size <paramref name="size"/>.
    /// </returns>
    public static IEnumerable<T[]> Chunk<T>(this IEnumerable<T> source, int size)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (size < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(size));
        }

        return ChunkIterator(source, size);
    }

    /// <summary>
    /// The iterator that performs the work for the <see cref="Chunk{T}(IEnumerable{T}, int)"/> method.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> whose elements to chunk.</param>
    /// <param name="size">Maximum size of each chunk.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> that contains the elements the input sequence
    /// split into chunks of size <paramref name="size"/>.
    /// </returns>
    private static IEnumerable<T[]> ChunkIterator<T>(IEnumerable<T> source, int size)
    {
        using var enumerator = source.GetEnumerator();

        if (enumerator.MoveNext())
        {
            var chunk = new List<T>();
            while (true)
            {
                do
                {
                    chunk.Add(enumerator.Current);
                }
                while (chunk.Count < size && enumerator.MoveNext());

                yield return chunk.ToArray();

                if (chunk.Count < size || !enumerator.MoveNext())
                {
                    yield break;
                }

                chunk.Clear();
            }
        }
    }
#endif
}
