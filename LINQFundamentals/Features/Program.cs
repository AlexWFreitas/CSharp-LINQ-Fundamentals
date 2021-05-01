using System;
using System.Collections.Generic;
using System.Linq;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            // Func using Expression Lambda
            Func<int, int> square = x => x * x;

            // Func using Statement Lambda
            Func<int, int, int> add = (x, y) =>
            {
                int temp = x + y;
                return temp;
            };

            // Action using Expression Lambda
            Action<int> write = x => Console.WriteLine(x);

            write(square(add(3, 5)));

            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Scott" },
                new Employee { Id = 2, Name = "Chris" }
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Alex" }
            };

            var enumerator = developers.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            }

            Console.WriteLine($"Number of developers: {developers.Count()}");

            Console.WriteLine("\nDevelopers ordered alphabetically only with names with exactly 5 letters:");

            var query = developers.Where(e => e.Name.Length == 5).
                                                OrderBy(e => e.Name);

            foreach (var employee in query)
                Console.WriteLine(employee.Name);
        }
    }
}
    