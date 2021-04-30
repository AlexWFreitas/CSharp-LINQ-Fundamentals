using System;
using System.Collections.Generic;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            //    Proof that extension methods are called through instances and are not able to be called through Class even though they are static methods
            string x = "Hello World";
            x.DefCon();
            // String.DefCon(x); // Error
            StringExtensions.DefCon(x);

            // Creates a delegate named square that contains the anonymous-method(int x) { return x * x; }
            Func<int, int> square = x => x * x;

            WriteHelloWorldForEachElement(23);
            Console.WriteLine(square(2));
        }

        public static void WriteHelloWorldForEachElement<T>(T element)
        {
            Console.WriteLine("hello");
        }

        

    }
}
