using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            //    Proof that extension methods are called through instances and are not able to be called through Class even though they are static methods
            // string x = "Hello World";
            // x.DefCon();
            // String.DefCon(x); // Error
            // StringExtensions.DefCon(x);

            // Creates a delegate named square that contains the anonymous-method(int x) { return x * x; }
            // Func<int, int> square = x => x * x;

            // WriteHelloWorldForEachElement(23);
            // Console.WriteLine(square(2));

            // IEnumerable<string> listaString = new string[] { "a", "b", "c" };

            var stringLul = new List<string> { "ABCDEF", "213", "2343" };
            var stringLul2 = new string[] { "ABCZDDEF", "21213", "23434fadsf" };

            List<int> y = stringLul.CloneSelect(x => StringExtensions.CloneSelector(x)).ToList();
            Console.WriteLine(y[0]);
            // Code to test a fake Select using Generics.
            //List<int> ListInt = stringLul.Select(x => 2).ToList();
            //stringLul.SelectFake(l => StringExtensions.WriteHello(l));
            //stringLul.SelectFake(StringExtensions.WriteHello);
            //stringLul.SelectFake(l => StringExtensions.WriteHello(l));
            List<int> z = stringLul.CloneSelect(x => StringExtensions.CloneSelector(x)).ToList();

            // stringLul.SelectFake(l => l);

            //stringLul.Select();

            // Code to prove that Generics aren't enumerating by itself.
            // stringLul.SelectFakeWithLessGenerics(StringExtensions.WriteHelloWithoutGenerics);
            // stringLul.SelectFakeWithLessGenerics(l => StringExtensions.WriteHelloWithoutGenerics(l));
        }

        public static void WriteHelloWorldForEachElement<T>(T element)
        {
            Console.WriteLine("hello");
        }
    }
}
