using System;
using System.Collections.Generic;
using System.Linq;

namespace Kelson.Common.Linq
{
    public static class Combinatorics
    {
        /// <summary>
        /// Returns all possible combinations of values in the source enumerable (including none)
        /// Limited to collections of 64 or fewer items
        /// </summary>        
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> source) 
            => source.ToList().Combinations();

        /// <summary>
        /// Returns all possible combinations of values in the source list (including none)
        /// Limited to collections of 64 or fewer items
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IList<T> source)
        {
            if (source.Count > 64)
                throw new InvalidOperationException("To many values in source, can't compute combinations");

            IEnumerable<T> WithMask(ulong m)
            {
                for (int i = 0; i < source.Count; i++)
                    if ((m & (1ul << i)) != 0ul)
                        yield return source[i];
            }

            ulong mask = 0;
            ulong limit = 1ul << source.Count;
            while (mask < limit)            
                yield return WithMask(mask++);
        }

        /// <summary>
        /// Returns all possible combinations of values in the source array (including none)
        /// Limited to collections of 64 or fewer items
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this T[] source)
        {
            if (source.Length > 64)
                throw new InvalidOperationException("To many values in source, can't compute combinations");

            IEnumerable<T> WithMask(ulong m)
            {
                for (int i = 0; i < source.Length; i++)
                    if ((m & (1ul << i)) != 0ul)
                        yield return source[i];
            }

            ulong mask = 0;
            ulong limit = 1ul << source.Length;
            while (mask < limit)
                yield return WithMask(mask++);
        }

        /// <summary>
        /// Returns all possible orderings of the values in the source enumerable
        /// </summary>        
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> source, int count)
        {            
            if (count == 2)
            {
                yield return source;
                yield return source.Reverse();
            }
            else if (count == 1)
            {
                yield return source;
            }
            else
            {
                var enumerator = source.GetEnumerator();
                int pinned = 0;
                while (enumerator.MoveNext())
                {
                    foreach (var permutation in source.ExceptIndex(pinned).Permutations(count - 1))
                        yield return enumerator.Current.PrependTo(permutation);
                    pinned++;
                }
            }
        }

        /// <summary>
        /// Returns all possible orderings of the values in the source list
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IList<T> source)
        {
            if (source.Count == 2)
            {
                yield return source;
                yield return source.Reverse();
            }
            else if (source.Count == 1)
            {
                yield return source;
            }
            else
            {
                var enumerator = source.GetEnumerator();
                int pinned = 0;
                while (enumerator.MoveNext())
                {
                    foreach (var permutation in source.ExceptIndex(pinned).Permutations(source.Count - 1))
                        yield return enumerator.Current.PrependTo(permutation);
                    pinned++;
                }
            }
        }

        /// <summary>
        /// Returns all possible orderings of the values in the source array
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this T[] source)
        {
            if (source.Length == 2)
            {
                yield return source;
                yield return source.Reverse();
            }
            else if (source.Length == 1)
            {
                yield return source;
            }
            else
            {
                for (int pinned = 0; pinned < source.Length; pinned++)
                    foreach (var permutation in source.ExceptIndex(pinned).Permutations(source.Length - 1))
                        yield return source[pinned].PrependTo(permutation);                 
            }
        }
    }
}
