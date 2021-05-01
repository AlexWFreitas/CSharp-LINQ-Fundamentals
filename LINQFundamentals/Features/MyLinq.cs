using System;
using System.Collections.Generic;
using System.Text;

namespace Features
{
    public static class MyLinq
    {
        public static int Count<T>(this IEnumerable<T> sequence)
        {
            var count = 0;

            foreach (var item in sequence)
                count++;

            return count;
        }
    }
}
