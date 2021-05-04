using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcesssFile("fuel.csv");

            var query =
                    cars.Where(car => (car.Manufacturer == "BMW" && car.Year == 2016))
                        .OrderByDescending(car => car.Combined)
                        .ThenBy(car => car.Name)
                        .Select(c => c);

            var query2 = 
                from car in cars
                where car.Manufacturer == "BMW" && car.Year == 2016
                orderby car.Combined descending, car.Name ascending
                select car;

            var top =
                    cars.OrderByDescending(car => car.Combined)
                        .ThenBy(car => car.Name)
                        .Select(car => car)
                        .FirstOrDefault(car => (car.Manufacturer == "BMW" && car.Year == 2016));

            var result = cars.Any(c => c.Manufacturer == "Ford");

            Console.WriteLine(result);

            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Manufacturer} {car.Name} : {car.Combined}");
            }
        }

        private static List<Car> ProcesssFile2(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .Select(Car.ParseFromCsv)
                    .ToList();
            return query.ToList();
        }

        private static List<Car> ProcesssFile(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .ToCar()
                    .ToList();
            return query.ToList();
        }
    }
}
