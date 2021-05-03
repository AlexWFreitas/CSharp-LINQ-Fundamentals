using System;
using System.Collections.Generic;
using System.Text;

namespace Queries
{
    public static class MyLinq
    {
        /// <summary>
        /// Deferred Execution - Where Clone
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                    yield return item;
            }   
        }

        /// <summary>
        /// Where Clone that has No Deferred Execution
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> OtherFilter<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            var sequence = new List<T>();
            foreach (var item in source)
            {
                if (predicate(item))
                    sequence.Add(item);
            }
            return sequence;
        }

        public static IEnumerable<double> Random()
        {
            var random = new Random();
            while(true)
            {
                yield return random.NextDouble();
            }
        }
    }
}
