using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDb>());
            InsertData();
            QueryData();
        }

        private static void QueryData()
        {
            var db = new CarDb();
            db.Database.Log = Console.WriteLine;

            var query =
                db.Cars
                .GroupBy(c => c.Manufacturer)
                .Select(g => new
                {
                    Name = g.Key,
                    Cars =
                    g.OrderByDescending(c => c.Combined)
                    .Take(2)
                });

            var query2 =
                from car in db.Cars
                group car by car.Manufacturer into manufacturer
                select new
                {
                    Name = manufacturer.Key,
                    Cars = (from car in manufacturer
                           orderby car.Combined descending
                           select car).Take(2)
                };

            foreach (var group in query)
            {
                Console.WriteLine($"{group.Name}");
                foreach (var car in group.Cars)
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }
        }

        private static void InsertData()
        {
            var cars = ProcessCars("fuel.csv");
            var db = new CarDb();

            db.Database.Log = Console.WriteLine;

            if (!db.Cars.Any())
            {
                foreach (var car in cars)
                {
                    db.Cars.Add(car);
                }
                db.SaveChanges();
            }
        }

        private static void QueryXml(string path)
        {
            var ns = (XNamespace)"http://www.otaru.com/cars/2016";
            var ex = (XNamespace)"http://www.otaru.com/cars/2016/ex";
            var document = XDocument.Load(path);

            var query =
                document
                ?.Element(ns + "Cars")
                ?.Elements(ex + "Car") 
                ?.Where(c => c.Attribute("Manufacturer")?.Value == "BMW")
                ?.Select(c => c.Attribute("Name")?.Value) 
                ?? Enumerable.Empty<string>();

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        private static void CreateXml()
        {
            var records = ProcessCars("fuel.csv");
            var ns = (XNamespace)"http://www.otaru.com/cars/2016";
            var ex = (XNamespace)"http://www.otaru.com/cars/2016/ex";
            var document = new XDocument();

            var cars = new XElement(ns + "Cars",

                        records.Select(c => new XElement(ex + "Car",
                            new XAttribute("Name", c.Name),
                            new XAttribute("Combined", c.Combined),
                            new XAttribute("Manufacturer", c.Manufacturer)))
            );

            cars.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));
            document.Add(cars);
            document.Save("fuel.xml");
        }

        private static List<Car> ProcessCars(string path)
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
