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
            var cars = ProcesssCars("fuel.csv");
            var manufacturers = ProcesssManufacturers("manufacturers.csv");

            var challengeQuerySyntax =
                from manufacturer in manufacturers
                join car in cars
                    on manufacturer.Name equals car.Manufacturer
                    into carGroup
                select 
                    new
                    {
                        Manufacturer = manufacturer,
                        Cars = carGroup
                    } 
                    into result
                group result by result.Manufacturer.Headquarters;

            var challengeQuery =
                manufacturers
                .GroupJoin(cars, m => m.Name, c => c.Manufacturer, (m, c) => new
                {
                    Manufacturer = m,
                    Cars = c
                });

            var challengeQuery2 =
                challengeQuery
                .GroupBy(m => m.Manufacturer.Headquarters)
                .OrderBy(o => o.Key);


            foreach (var groupOfCarsByCountry in challengeQuery2)
            {
                Console.WriteLine($"{groupOfCarsByCountry.Key}");

                var queryCars =
                    groupOfCarsByCountry
                    .SelectMany(g => g.Cars) // Flattening the Manufacturers
                    .OrderByDescending(c => c.Combined)
                    .Take(3);

                foreach (var car in queryCars)
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }

        }

        private static List<Car> ProcesssCars(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .Select(line =>
                    {
                        var columns = line.Split(',');
                        return new Car
                        {
                            Year = int.Parse(columns[0]),
                            Manufacturer = columns[1],
                            Name = columns[2],
                            Displacement = double.Parse(columns[3]),
                            Cylinders = int.Parse(columns[4]),
                            City = int.Parse(columns[5]),
                            Highway = int.Parse(columns[6]),
                            Combined = int.Parse(columns[7])
                        };

                    });
            return query.ToList();
        }

        private static List<Manufacturer> ProcesssManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Where(line => line.Length > 1)
                    .Select(line =>
                    {
                        var columns = line.Split(',');
                        return new Manufacturer()
                        {
                            Name = columns[0],
                            Headquarters = columns[1],
                            Year = int.Parse(columns[2])
                        };
                    });
            return query.ToList();
        }
    }
}
