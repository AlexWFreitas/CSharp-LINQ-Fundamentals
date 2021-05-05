using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox
{
    public static class StringExtensions
    {
        // Method to test how you can call Extension Methods on a string
        public static void WriteTest(this string value)
        {
            Console.WriteLine("Test");
        }

        // Method to test how lambda expressions interact with methods that accept delegates.
        public static void SelectFake<T>(this IEnumerable<T> source, Action<T> selector)
        {
            
            foreach (var item in source)
            {
                selector(item);
            }
            
        }

        // Named method that will be used on a method that uses delegates.
        public static void WriteHello<T>(T a)
        {
            Console.WriteLine(a);
        }

        // Same as SelectFake but without using generic methods of T.
        public static void SelectFakeTest3(this string[] source, Action<string> selector)
        {
            foreach (var item in source)
            {
                selector(item);
            }
        }


        // Same as SelectFake3 but using IEnumerable instead of array.
        public static void SelectFakeWithLessGenerics(this IEnumerable<string> source, Action<string> selector)
        {
            foreach (var item in source)
            {
                selector(item);
            }

        }

        // Method to pass on a Action<string> parameter without generic methods.
        public static void WriteHelloWithoutGenerics(string a)
        {
            Console.WriteLine(a);
        }

        // Select clone method that can be used to test how lambda expressions work with deferred execution. 
        // Since this uses yield return, it should be more close to how Select works.
        public static IEnumerable<TResult> CloneSelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            foreach (var element in source)
            {
                yield return selector(element);   
            }
        }

        // Select clone that doesn't use Generic of T.
        public static IEnumerable<int> CloneSelectDifferent(this List<String> source, Func<string, int> selector)
        {
            List<string> secondSource = new List<string>() { "23", "abc", "def" };

            foreach (var element in secondSource)
            { 
                yield return selector(element);
            }
        }

        // Selector to be used by CloneSelect and CloneSelectDifferent
        // Allows you to generate int returns to populate the IEnumerable<int> return.
        public static int CloneSelector(string source)
        {
            Random gen = new Random();
            return gen.Next();
        }
    }
}