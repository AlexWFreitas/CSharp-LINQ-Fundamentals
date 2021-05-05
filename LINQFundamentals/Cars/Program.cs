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
                        from car in cars
                        join manufacturer in manufacturers 
                            on new { car.Manufacturer, car.Year } 
                                equals 
                                new { Manufacturer = manufacturer.Name, manufacturer.Year }
                        orderby car.Combined descending, car.Name ascending
                        select new
                        {
                            manufacturer.Headquarters,
                            car.Name,
                            car.Combined
                        };

            var methodSyntax =
                    cars
                        .Join(manufacturers,
                                c => new { c.Manufacturer, c.Year },
                                m => new { Manufacturer = m.Name, m.Year },
                                (c, m) => new
                                {
                                    m.Headquarters,
                                    c.Name,
                                    c.Combined
                                })
                        .OrderByDescending(c => c.Combined)
                        .ThenBy(c => c.Name);

            foreach (var car in methodSyntax.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
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
