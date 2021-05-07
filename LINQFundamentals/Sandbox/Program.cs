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
            // Proof that extension methods are called through instances and are not able to be called through Class even though they are static methods
            string x = "Hello World";
            x.WriteTest();
            StringExtensions.WriteTest(x);
            // String.DefCon(x); // Error - You should call extension methods either by the class name or by the instance of object.


            // Aggregate example with simple code.
            var listInt = new List<int> { 1, 2, 3, 4, 5 };

            var sumTimesTwo = listInt.Aggregate(new int(), (acc, value) => acc + value, acc => acc * 2);


            // Creates a delegate named square that contains the anonymous-method(int x) { return x * x; }
            Func<int, int> square = x => x * x;
            Console.WriteLine(square(2));



            // Tests below are related to testing how lambda expressions interact with methods that accept delegates.
            // Defining some string lists for testing.
            IEnumerable<string> listaString = new string[] { "a", "b", "c" };
            var stringLul = new List<string> { "ABCDEF", "213", "2343" };
            var stringLul2 = new string[] { "ABCZDDEF", "21213", "23434fadsf" };

            // Debug shows how this method passes a value to x to use on the lambda expression.
            List<int> y = stringLul.CloneSelectDifferent(x => StringExtensions.CloneSelector(x)).ToList();
            Console.WriteLine(y[0]);

            // Code to test a fake Select using Generics. ( Previous version )
            stringLul.SelectFake(l => StringExtensions.WriteHello(l));
            stringLul.SelectFake(StringExtensions.WriteHello);

            // Since Select enumerates the number of elements in stringLul.
            // listInt receives 3 elements of value 2.
            List<int> listInt2 = stringLul.Select(x => 2).ToList();

            // Code to prove that the enumeration doesn't happen because of generic method signature.
            stringLul.SelectFakeWithLessGenerics(StringExtensions.WriteHelloWithoutGenerics);
            stringLul.SelectFakeWithLessGenerics(l => StringExtensions.WriteHelloWithoutGenerics(l));
        }
    }
}
