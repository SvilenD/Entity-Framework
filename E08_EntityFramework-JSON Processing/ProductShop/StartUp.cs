namespace ProductShop
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    using ProductShop.Data;
    using ProductShop.Models;
    using ProductShop.ResultModels;

    public class StartUp
    {
        private const string usersData = "../../../Datasets/users.json";
        private const string productsData = "../../../Datasets/products.json";
        private const string categoriesData = "../../../Datasets/categories.json";
        private const string categoriesProductsData = "../../../Datasets/categories-products.json";

        public static void Main(string[] args)
        {
            var mapperConfig = new ProductShopProfile();
            Mapper.Initialize(c => c.AddProfile(mapperConfig));

            using (var context = new ProductShopContext())
            {
                context.Database.Migrate();

                //ImportData(context);

                Console.WriteLine(GetUsersWithProducts(context));
            }
        }

        //Task 1-4
        public static void ImportData(ProductShopContext context)
        {
            using (var reader = new StreamReader(categoriesProductsData))
            {
                var inputJson = reader.ReadToEnd();

                Console.WriteLine(ImportCategoryProducts(context, inputJson));
            }
        }

        //Task 1
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        //Task 2
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        //Task 3
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<List<Category>>(inputJson);
            categories.RemoveAll(c => c.Name == null);

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }

        //Task 4
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            var categoryProducts = JsonConvert.DeserializeObject<List<CategoryProduct>>(inputJson);

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count}";
        }

        //Task 5 - Get all products in a specified price range:  500 to 1000 (inclusive). Order them by price (from lowest to highest). 
        //Select only the product name, price and the full name of the seller. Export the result to JSON.
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
            .Where(p => p.Price >= 500 && p.Price <= 1000)
            .OrderBy(x => x.Price)
            .ProjectTo<ProductsInRangeDto>()
            .ToList();

            var productsJson = JsonConvert.SerializeObject(products, Formatting.Indented);

            //using (var writer = new StreamWriter("../../../OutputResults/productsPriceRange500-1000.json"))
            //{
            //    writer.Write(productsJson);
            //}

            return productsJson;
        }

        //Task 6 - Get all users who have at least 1 sold item with a buyer. Order them by last name, then by first name. Select the person's first and last name. 
        //For each of the sold products (products with buyers), select the product's name, price and the buyer's first and last name.
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ProjectTo<UserProductsDto>()
                .ToList();

            var usersProductsJson = JsonConvert.SerializeObject(users, Formatting.Indented);

            return usersProductsJson;
        }

        //Task 7 - Get all categories. Order them in descending order by the category’s products count. For each category select its name, 
        //the number of products, the average price of those products (rounded to second digit after the decimal separator) and the total revenue 
        //(total price sum and rounded to second digit after the decimal separator) of those products (regardless if they have a buyer or not).
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .OrderByDescending(c => c.CategoryProducts.Count)
                .ProjectTo<CategoryProductsDto>()
                .ToList();

            var categoriesJson = JsonConvert.SerializeObject(categories, Formatting.Indented);

            return categoriesJson;
        }

        //Task 8 - Get all users who have at least 1 sold product with a buyer. Order them in descending order by the number of sold products 
        //with a buyer. Select only their first and last name, age and for each product - name and price. Ignore all null values.
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var usersProducts = context.Users
                .Where(u => u.ProductsSold.Any(p => p.Buyer != null))
                .ProjectTo<UserWithProductsDto>()
                .OrderByDescending(u => u.SoldProducts.Count)
                .ToArray();

            var users = new UserDto()
            {
                UsersCount = usersProducts.Length,
                Users = usersProducts
            };

            var usersProductsJson = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            //using (var writer = new StreamWriter("../../../OutputResults/users-and-products.json"))
            //{
            //    writer.Write(usersProductsJson);
            //}

            return usersProductsJson;
        }
    }
}