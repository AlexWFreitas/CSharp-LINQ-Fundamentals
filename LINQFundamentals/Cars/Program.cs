using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            var records = ProcesssCars("fuel.csv");

            var document = new XDocument();
            var cars = new XElement("Cars");

            // Even easier way of creating Car XElements
            var elements =
                records
                .Select(c => new XElement( "Car", 
                                new XAttribute("Name", c.Name),
                                new XAttribute("Combined", c.Combined),
                                new XAttribute("Manufacturer", c.Manufacturer)));



            // Easier way of creating cars
            foreach (var record in records)
            {
                var car = new XElement("Car", 
                            new XAttribute("Name", record.Name), 
                            new XAttribute("Combined", record.Combined), 
                            new XAttribute("Manufacturer", record.Manufacturer));

                cars.Add(car);
            }

            document.Add(elements);
            document.Save("fuel.xml");
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
