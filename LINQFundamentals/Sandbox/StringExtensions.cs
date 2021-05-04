using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox
{
    public static class StringExtensions
    {
        public static void DefCon(this string value)
        {
            Console.WriteLine("Test");
        }

        // ListaDeString.SelectFake(string => SelectFake(string));
        // ListaDeString.SelectFake(SelectFake);

        public static void SelectFake<T>(this IEnumerable<T> source, Action<T> selector)
        {
            
            foreach (var item in source)
            {
                selector(item);
            }
            
        }

        public static void WriteHello<T>(T a)
        {
            Console.WriteLine(a);
        }

        public static void SelectFakeTest3(this string[] source, Action<string> selector)
        {
            foreach (var item in source)
            {
                selector(item);
            }
        }


        public static void SelectFakeWithLessGenerics(this IEnumerable<string> source, Action<string> selector)
        {
            foreach (var item in source)
            {
                selector(item);
            }

        }

        public static void WriteHelloWithoutGenerics(string a)
        {
            Console.WriteLine(a);
        }

        public static IEnumerable<TResult> CloneSelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            foreach (var element in source)
            {
                yield return selector(element);   
            }
        }

        public static int CloneSelector(string source)
        {
            Random gen = new Random();
            return gen.Next();
        }
    }
}
