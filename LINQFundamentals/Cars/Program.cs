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

            var querySyntax =
                from manufacturer in manufacturers
                join car in cars
                                on
                                new { Manufacturer = manufacturer.Name, manufacturer.Year }
                                equals
                                new { car.Manufacturer, car.Year }
                                into carGroup
                orderby manufacturer.Name ascending
                select new
                {
                    Manufacturer = manufacturer,
                    Cars = carGroup
                };

            var methodSyntax =
                manufacturers
                .GroupJoin(
                            cars ,
                            m => new { Manufacturer = m.Name, m.Year },
                            c => new { c.Manufacturer, c.Year },
                            (m, c) => new { Manufacturer = m, Cars = c }
                            )
                .OrderBy(g => g.Manufacturer.Name);

            foreach (var aGroup in querySyntax)
            {

                var queryFuelTakeTwo = 
                    aGroup
                    .Cars
                    .OrderByDescending(c => c.Combined).Take(2);

                var querySyntaxFuelTakeTwo =
                    (from car in aGroup.Cars
                     orderby car.Combined descending
                     select car).Take(2);

                Console.WriteLine($"{aGroup.Manufacturer.Name} : {aGroup.Manufacturer.Headquarters}");
                foreach (var car in queryFuelTakeTwo)
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
