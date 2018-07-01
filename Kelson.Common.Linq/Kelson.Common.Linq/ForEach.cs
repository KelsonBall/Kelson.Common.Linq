using System;
using System.Collections.Generic;

namespace Kelson.Common.Linq
{
    public static class ForEach
    {
        public static IEnumerable<T> In<T>(params T[] values) => values; 

        /// <summary>
        /// Execute action for each value in the source and return the value unchanged
        /// </summary>        
        public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var value in source)
            {
                action(value);
                yield return value;
            }
        }
    }
}
