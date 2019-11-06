namespace Lab
{
    using System;
    using System.Linq;
    using Lab.Data;
    using Lab.Data.Models;
    using Lab.Results;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new CarDbContext())
            {
                db.Database.Migrate();
                var purchases = db.Purchases.
                            Select(p => new PurchaseResultModel
                            {
                                Price = p.Price,
                                Date = p.PurchaseDate,
                                Car = new CarResultModel
                                {
                                    VIN = p.Car.VIN,
                                    Make = p.Car.Model.Make.Name,
                                    Model = p.Car.Model.Name
                                },
                                Customer = new CustomerResultModel
                                {
                                    Name = p.Customer.FirstName + " " + p.Customer.LastName,
                                    Age = p.Customer.Age
                                }
                            })
                            .ToList();

                foreach (var pur in purchases)
                {
                    Console.WriteLine(pur.Customer.Name);
                    Console.WriteLine(pur.Car.Make + " " + pur.Car.Model);
                    Console.WriteLine(pur.Price);
                }

                db.SaveChanges();
            }
        }
    }
}