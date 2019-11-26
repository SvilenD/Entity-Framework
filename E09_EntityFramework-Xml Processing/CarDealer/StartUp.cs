namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using CarDealer.Data;
    using CarDealer.Dtos.Export;
    using CarDealer.Dtos.Import;
    using CarDealer.Models;

    public class StartUp
    {
        public const string outputResult = "Successfully imported {0}";
        public const string carsInputXmlData = "Datasets/cars.xml";
        public const string customersInputXmlData = "Datasets/customers.xml";
        public const string partsInputXmlData = "Datasets/parts.xml";
        public const string salesInputXmlData = "Datasets/sales.xml";
        public const string suppliersInputXmlData = "Datasets/suppliers.xml";

        public static void Main()
        {
            var mapperConfig = new CarDealerProfile();
            Mapper.Initialize(c => c.AddProfile(mapperConfig));

            using (var context = new CarDealerContext())
            {
                //context.Database.Migrate();

                //var data = File.ReadAllText(carsInputXmlData);
                Console.WriteLine(GetSalesWithAppliedDiscount(context));
            }
        }

        //Task 9
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var attr = new XmlRootAttribute("Suppliers");
            var serializer = new XmlSerializer(typeof(SupplierImportDto[]), attr);

            var supplierDtos = (SupplierImportDto[])serializer.Deserialize(new StringReader(inputXml));

            var suppliers = Mapper.Map<Supplier[]>(supplierDtos);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return String.Format(outputResult, suppliers.Length);
        }

        //Task 10
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var attr = new XmlRootAttribute("Parts");
            var serializer = new XmlSerializer(typeof(PartImportDto[]), attr);

            var partsDto = (PartImportDto[])serializer.Deserialize(new StringReader(inputXml));

            var supplierIds = context.Suppliers
                .Select(p => p.Id)
                .ToArray();
            var parts = new List<Part>();

            foreach (var dto in partsDto)
            {
                if (supplierIds.Contains(dto.SupplierId))
                {
                    var part = Mapper.Map<Part>(dto);
                    parts.Add(part);
                }
            }

            context.AddRange(parts);
            context.SaveChanges();

            return String.Format(outputResult, parts.Count);
        }

        //Task 11 
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var attr = new XmlRootAttribute("Cars");
            var serializer = new XmlSerializer(typeof(CarImportDto[]), attr);

            var carsDto = (CarImportDto[])serializer.Deserialize(new StringReader(inputXml));

            var partIds = context.Parts
                .Select(p => p.Id)
                .ToArray();

            var carParts = new HashSet<PartCar>();
            var cars = new HashSet<Car>();

            foreach (var dto in carsDto)
            {
                var carToAdd = Mapper.Map<Car>(dto);
                cars.Add(carToAdd);

                var carPartsIds = dto.Parts
                    .Select(p => p.Id)
                    .Distinct();

                foreach (var carPartId in carPartsIds)
                {
                    if (partIds.Contains(carPartId))
                    {
                        var part = new PartCar
                        {
                            CarId = carToAdd.Id,
                            PartId = carPartId
                        };

                        carParts.Add(part);
                    }
                }
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(carParts);
            context.SaveChanges();

            return String.Format(outputResult, cars.Count);
        }

        //public static string ImportCars(CarDealerContext context, string inputXml)
        //{
        //    var carsParsed = XDocument.Parse(inputXml)
        //        .Root
        //        .Elements()
        //        .ToList();

        //    var cars = new List<Car>();

        //    var partsID = context.Parts
        //        .Select(x => x.Id)
        //        .ToArray();

        //    foreach (var car in carsParsed)
        //    {
        //        var currentCar = new Car()
        //        {
        //            Make = car.Element("make").Value,
        //            Model = car.Element("model").Value,
        //            TravelledDistance = long.Parse(car.Element("TraveledDistance").Value)
        //        };

        //        var partIds = new HashSet<int>();

        //        foreach (var partid in car.Element("parts").Elements())
        //        {
        //            var pid = int.Parse(partid.Attribute("id").Value);
        //            partIds.Add(pid);
        //        }

        //        foreach (var pid in partIds)
        //        {
        //            if (partsID.Contains(pid))
        //            {
        //                var currentPair = new PartCar()
        //                {
        //                    Car = currentCar,
        //                    PartId = pid
        //                };

        //                currentCar.PartCars.Add(currentPair);
        //            }
        //        }
        //        cars.Add(currentCar);
        //    }

        //    context.Cars.AddRange(cars);

        //    context.SaveChanges();

        //    return $"Successfully imported {cars.Count}";
        //}

        //Task 12
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var customersParsed = XDocument.Parse(inputXml)
                .Root
                .Elements()
                .ToArray();

            var customers = new List<Customer>();
            foreach (var customer in customersParsed)
            {
                var currentCustomer = new Customer
                {
                    Name = customer.Element("name").Value,
                    BirthDate = DateTime.Parse(customer.Element("birthDate").Value),
                    IsYoungDriver = Convert.ToBoolean(customer.Element("isYoungDriver").Value)
                };

                customers.Add(currentCustomer);
            }

            context.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
        }

        //Task 13
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var attr = new XmlRootAttribute("Sales");
            var serializer = new XmlSerializer(typeof(SaleImportDto[]), attr);

            var sales = new List<Sale>();

            var carsIds = context.Cars
                .Select(s => s.Id)
                .ToArray();

            var saleDtos = (SaleImportDto[])serializer.Deserialize(new StringReader(inputXml));
            foreach (var saleDto in saleDtos)
            {
                if (carsIds.Contains(saleDto.CarId))
                {
                    var sale = Mapper.Map<Sale>(saleDto);
                    sales.Add(sale);
                }
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}";
        }

        //Task 14 - Get all cars with distance more than 2,000,000. Order them by make, then by model alphabetically. Take top 10 records.
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.TravelledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ProjectTo<CarExportDto>()
                .ToArray();

            var result = new StringBuilder();
            var xmlSerializer = new XmlSerializer(cars.GetType(), new XmlRootAttribute("cars"));

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            xmlSerializer.Serialize(new StringWriter(result), cars, namespaces);
            //xmlSerializer.Serialize(new StreamWriter("../../../carsOver2mlnKM.xml"), cars, namespaces);

            return result.ToString().TrimEnd();
        }

        //Task 15 - Get all cars from make BMW and order them by model alphabetically and by travelled distance descending.
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var bmwCars = context.Cars
                .Where(c => c.Make.ToLower() == "bmw")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ProjectTo<CarBmwExportDto>()
                .ToArray();

            var attr = new XmlRootAttribute("cars");
            var xmlSerializer = new XmlSerializer(bmwCars.GetType(), attr);

            var result = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(result), bmwCars, namespaces);
            //xmlSerializer.Serialize(new StreamWriter("../../../bmwCars.xml"), bmwCars, namespaces);

            return result.ToString().TrimEnd();
        }

        //Task 16 - Get all suppliers that do not import parts from abroad.Get their id, name and the number of parts they can offer to supply. 
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .ProjectTo<SuppliersExportDto>()
                .ToArray();

            var attr = new XmlRootAttribute("suppliers");
            var xmlSerializer = new XmlSerializer(suppliers.GetType(), attr);
            var result = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(result), suppliers, namespaces);
            //xmlSerializer.Serialize(new StreamWriter("../../../localSuppliers.xml"), suppliers, namespaces);

            return result.ToString().TrimEnd();
        }

        //Task 17 - Get all cars along with their list of parts. For the car get only make, model and travelled distance 
        //and for the parts get only name and price and sort all parts by price (descending). 
        //Sort all cars by travelled distance (descending) then by model (ascending). Select top 5 records.

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ProjectTo<CarWithPartsExportDto>()
                                                    //.Select(c => new CarWithPartsExportDto
                                                    //{
                                                    //    Make = c.Make,
                                                    //    Model = c.Model,
                                                    //    TravelledDistance = c.TravelledDistance,
                                                    //    Parts = c.PartCars.Select(p => new PartExportDto
                                                    //        {
                                                    //            Name = p.Part.Name,
                                                    //            Price = p.Part.Price
                                                    //        })
                                                    //        .OrderByDescending(p => p.Price)
                                                    //        .ToArray()
                                                    //})
                .ToArray();

            var attr = new XmlRootAttribute("cars");
            var xmlSerializer = new XmlSerializer(cars.GetType(), attr);

            var result = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            xmlSerializer.Serialize(new StringWriter(result), cars, namespaces);
            //xmlSerializer.Serialize(new StreamWriter("../../../carsWithParts.xml"), cars, namespaces);

            return result.ToString().TrimEnd();
        }

        //Task 18 - Get all customers that have bought at least 1 car and get their names, bought cars count and total spent money on cars. 
        //Order the result list by total spent money descending.
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Any())
                .ProjectTo<CustomerExportDto>()
                .OrderByDescending(p => p.SpentMoney)
                .ToArray();

            var serializer = new XmlSerializer(customers.GetType(), new XmlRootAttribute("customers"));
            var result = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(result), customers, namespaces);
            //serializer.Serialize(new StreamWriter("../../../customersBought.xml"), customers, namespaces);

            return result.ToString().TrimEnd();
        }

        //Task 19 - Get all sales with information about the car, customer and price of the sale with and without discount.
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                  .Select(s => new SaleExportDto
                  {
                      Car = new CarAttrExportDto
                      {
                          Make = s.Car.Make,
                          Model = s.Car.Model,
                          TravelledDistance = s.Car.TravelledDistance
                      },
                      CustomerName = s.Customer.Name,
                      Discount = s.Discount,
                      Price = s.Car.PartCars.Sum(pc => pc.Part.Price),
                      PriceWithDiscount = s.Car.PartCars.Sum(pc => pc.Part.Price)
                          - s.Car.PartCars.Sum(pc => pc.Part.Price) * s.Discount / 100m
                  })
                  .ToArray();

            var rootAttr = new XmlRootAttribute("sales");
            var serializer = new XmlSerializer(sales.GetType(), rootAttr);

            var result = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(result), sales, namespaces);
            //serializer.Serialize(new StreamWriter("../../../sales.xml"), sales, namespaces);

            return result.ToString().TrimEnd();
        }
    }
}