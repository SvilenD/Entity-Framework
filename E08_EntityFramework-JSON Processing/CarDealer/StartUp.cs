namespace CarDealer
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using CarDealer.Data;
    using CarDealer.Models;
    using CarDealer.DTO;
    using AutoMapper.QueryableExtensions;

    public class StartUp
    {
        private const string carsData = @"../../../Resources/cars.json";
        private const string customersData = @"../../../Resources/customers.json";
        private const string partsData = @"../../../Resources/parts.json";
        private const string salesData = @"../../../Resources/sales.json";
        private const string suppliersData = @"../../../Resources/suppliers.json";
        public const string importResult = "Successfully imported {0}.";

        public static void Main()
        {
            var mapperConfig = new CarDealerProfile();
            Mapper.Initialize(c => c.AddProfile(mapperConfig));

            using (var context = new CarDealerContext())
            {
                context.Database.Migrate();

                //var data = File.ReadAllText(salesData);

                Console.WriteLine(GetSalesWithAppliedDiscount(context));
            }
        }

        //Task 9
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return String.Format(importResult, suppliers.Length);
        }

        //Task 10
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var validSupplierIds = context
                .Suppliers
                .Select(s => s.Id)
                .ToList();

            var parts = JsonConvert.DeserializeObject<List<Part>>(inputJson)
                .Where(p => validSupplierIds.Contains(p.SupplierId))
                .ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return String.Format(importResult, parts.Count);
        }

        //Task 11 - 50/100 WTF?!
        //public static string ImportCars(CarDealerContext context, string inputJson)
        //{
        //    var carsDto = JsonConvert.DeserializeObject<CarDto[]>(inputJson, new JsonSerializerSettings()
        //    {
        //        ContractResolver = new DefaultContractResolver()
        //        {
        //            NamingStrategy = new CamelCaseNamingStrategy()
        //        }
        //    });

        //    var cars = new List<Car>();
        //    var partsCars = new List<PartCar>();

        //    var validPartIds = context.Parts
        //            .Select(p => p.Id)
        //            .ToList();

        //    foreach (var model in carsDto)
        //    {
        //        var currentCar = Mapper.Map<Car>(model);
        //        cars.Add(currentCar);

        //        foreach (var partId in model.PartsId)
        //        {
        //            if (validPartIds.Contains(partId))
        //            {
        //                var currentPartCar = new PartCar { PartId = partId, CarId = currentCar.Id };

        //                if (currentCar.PartCars.FirstOrDefault(p => p.PartId == partId) == null)
        //                {
        //                    partsCars.Add(currentPartCar);
        //                }
        //            }
        //        }
        //    }

        //    context.Cars.AddRange(cars);
        //    context.PartCars.AddRange(partsCars);

        //    context.SaveChanges();

        //    return String.Format(importResult, cars.Count);
        //}

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carsJson = JsonConvert.DeserializeObject<CarWithPartsId[]>(inputJson);

            var validPartIds = context.Parts
                .Select(p => p.Id)
                .ToList();

            var cars = new List<Car>();
            var partsCars = new List<PartCar>();

            foreach (var carModel in carsJson)
            {
                var carToAdd = Mapper.Map<Car>(carModel);

                cars.Add(carToAdd);

                foreach (var partsCarId in carModel.PartsId)
                {
                    if (validPartIds.Contains(partsCarId))
                    {
                        var carPartToAdd = new PartCar
                        {
                            CarId = carToAdd.Id,
                            PartId = partsCarId
                        };

                        if (carToAdd.PartCars
                            .FirstOrDefault(p => p.PartId == partsCarId && p.CarId == carToAdd.Id) == null)
                        {
                            carToAdd.PartCars.Add(carPartToAdd);
                            partsCars.Add(carPartToAdd);
                        }
                    }
                }
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(partsCars);

            context.SaveChanges();

            return String.Format(importResult, carsJson.Length);
        }

        //Task 12
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<Customer[]>(inputJson, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return String.Format(importResult, customers.Length);
        }

        //Task 13
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<Sale[]>(inputJson, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return String.Format(importResult, sales.Length);
        }

        //Task 14
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .ProjectTo<CustomerDto>()
                .ToArray();

            var customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);

            return customersJson;
        }

        //Task 15 - Get all cars from make Toyota and order them by model alphabetically and by travelled distance descending. 
        //Export the list of cars to JSON in the format provided below.
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TravelledDistance
                })
                .ToArray();

            var carsJson = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return carsJson;
        }

        //Task 16 - Get all suppliers that do not import parts from abroad. Get their id, name and the number of parts they can offer to supply.
        //Export the list of suppliers to JSON in the format provided below.
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count()
                }).ToArray();

            var suppliersJson = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            return suppliersJson;
        }

        //Task17 - Get all cars along with their list of parts. For the car get only make, model and travelled distance 
        //and for the parts get only name and price (formatted to 2nd digit after the decimal point). 
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsWithParts = context.Cars
                .Select(c => new DetailedCarDto
                {
                    Car = new CarDto
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    },
                    Parts = c.PartCars
                        .Select(p => new PartDto
                        {
                            Name = p.Part.Name,
                            Price = $"{p.Part.Price:f2}"
                        })
                        .ToArray()
                })
                .ToArray();

            var carsWithPartsJson = JsonConvert.SerializeObject(carsWithParts, Formatting.Indented);

            return carsWithPartsJson;
        }

        //Task18 - Get all customers that have bought at least 1 car and get their names, bought cars count and total spent money on cars. 
        //Order the result list by total spent money descending then by total bought cars again in descending order. 
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customersSales = context.Customers
                .Where(c => c.Sales.Count >= 1)
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales
                        .Sum(s => s.Car.PartCars.Sum(p => p.Part.Price))
                }).ToArray();

            var customersSalesJson = JsonConvert.SerializeObject(customersSales, Formatting.Indented);
            return customersSalesJson;
        }

        //Task 19 - Get first 10 sales with information about the car, customer and price of the sale with and without discount. 
        //Export the list of sales to JSON in the format provided below.
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var top10Sales = context.Sales
                .Take(10)
                .ProjectTo<SaleDto>()
                .ToArray();

            var salesFormatted = new List<SaleFormattedDto>();
            foreach (var sale in top10Sales)
            {
                var formatedSale = Mapper.Map<SaleFormattedDto>(sale);
                salesFormatted.Add(formatedSale);
            }

            var salesJson = JsonConvert.SerializeObject(salesFormatted, Formatting.Indented);

            return salesJson;
        }
    }
}