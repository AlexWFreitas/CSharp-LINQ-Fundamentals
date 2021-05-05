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
            var manufacturers = ProcesssManufacturers("manufacturer.csv");

            var query =
                    cars.Where(car => (car.Manufacturer == "BMW" && car.Year == 2016))
                        .OrderByDescending(car => car.Combined)
                        .ThenBy(car => car.Name)
                        .Select(car => new { car.Manufacturer, car.Name, car.Combined });
        }

        private static List<Car> ProcesssCars(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .ToCar();
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
