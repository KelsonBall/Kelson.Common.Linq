using System;
using System.Collections.Generic;
using System.Linq;

namespace Kelson.Common.Linq
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Filter to items of the desired type
        /// </summary>        
        public static IEnumerable<TResult> WhereIs<TResult>(this IEnumerable<object> source)
        {
            foreach (var item in source)
                if (item is TResult cast)
                    yield return cast;
        }

        /// <summary>
        /// Treats null collections as empty
        /// </summary>        
        public static IEnumerable<T> EmptyIfNull<T>(this T[] source) => source ?? Enumerable.Empty<T>();

        /// <summary>
        /// Treats null collections as empty
        /// </summary>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source) => source ?? Enumerable.Empty<T>();

        /// <summary>
        /// Produce a tuple containing the selector result and the original value
        /// </summary>                
        /// <param name="coalesce">Skip pairs with null results</param        
        public static IEnumerable<(T item, TPair result)> PairedWith<TPair, T>(this IEnumerable<T> source, Func<T, TPair> func, bool coalesce = true)
        {
            foreach (var item in source)
            {
                var result = func(item);
                if (coalesce && result == null)
                    continue;
                yield return (item, result);
            }
        }

        /// <summary>
        /// Concatenate two collections        
        /// </summary>        
        public static IEnumerable<T> Append<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            foreach (var item in first)
                yield return item;
            foreach (var item in second)
                yield return item;
        }

        /// <summary>
        /// Append an item to an enumerable
        /// </summary>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> first, T second)
        {
            foreach (var item in first)
                yield return item;
            yield return second;
        }

        /// <summary>
        /// Prepend an item to an enumerable
        /// </summary>        
        public static IEnumerable<T> PrependTo<T>(this T first, IEnumerable<T> second)
        {            
            yield return first;
            foreach (var item in second)
                yield return item;
        }

        /// <summary>
        /// Take all except the specified index
        /// </summary>        
        public static IEnumerable<T> ExceptIndex<T>(this T[] source, int index)
        {
            for (int i = 0; i < source.Length; i++)
                if (i != index)
                    yield return source[i];
        }

        /// <summary>
        /// Take all except the specified index
        /// </summary>
        public static IEnumerable<T> ExceptIndex<T>(this IList<T> source, int index)
        {
            for (int i = 0; i < source.Count; i++)
                if (i != index)
                    yield return source[i];
        }

        /// <summary>
        /// Take all except the specified index
        /// </summary>
        public static IEnumerable<T> ExceptIndex<T>(this IEnumerable<T> source, int index)
        {
            int i = 0;
            foreach (var item in source)
            {
                if (i != index)
                    yield return item;
                i++;
            }
        }
    }
}
