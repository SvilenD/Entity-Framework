namespace ProductShop
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using AutoMapper;

    using ProductShop.Data;
    using ProductShop.Models;
    using ProductShop.Dtos.Import;
    using ProductShop.Dtos.Export;
    using System.Xml;
    using System.Text;
    using AutoMapper.QueryableExtensions;

    public class StartUp
    {
        public const string outputResult = "Successfully imported {0}";
        public const string usersXml = "Datasets/users.xml";
        public const string productsXml = "Datasets/products.xml";
        public const string categoriesXml = "Datasets/categories.xml";
        public const string categorieProductsXml = "Datasets/categories-products.xml";

        public static void Main()
        {
            var mapperConfig = new ProductShopProfile();
            Mapper.Initialize(c => c.AddProfile(mapperConfig));

            //var data = File.ReadAllText(categorieProductsXml);

            using (var context = new ProductShopContext())
            {
                //context.Database.Migrate();

                Console.WriteLine(GetUsersWithProducts(context));
            }
        }

        //Task 1
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var attr = new XmlRootAttribute("Users");
            var serializer = new XmlSerializer(typeof(UserImportDto[]), attr);

            var userDtos = (UserImportDto[])serializer.Deserialize(new StringReader(inputXml));

            var users = new List<User>();

            foreach (var userDto in userDtos)
            {
                var user = Mapper.Map<User>(userDto);
                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return String.Format(outputResult, users.Count);
        }

        //Task 2
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var attr = new XmlRootAttribute("Products");

            var serializer = new XmlSerializer(typeof(ProductImportDto[]), attr);

            var productDtos = (ProductImportDto[])serializer.Deserialize(new StringReader(inputXml));

            var products = new List<Product>();

            foreach (var productDto in productDtos)
            {
                var product = Mapper.Map<Product>(productDto);
                products.Add(product);
            }

            context.Products.AddRange(products);
            context.SaveChanges();

            return String.Format(outputResult, products.Count);
        }

        //Task 3
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var attr = new XmlRootAttribute("Categories");

            var serializer = new XmlSerializer(typeof(CategoryImportDto[]), attr);
            var categoryDtos = (CategoryImportDto[])serializer.Deserialize(new StringReader(inputXml));

            var categories = new List<Category>();

            foreach (var categoryDto in categoryDtos)
            {
                var category = Mapper.Map<Category>(categoryDto);
                categories.Add(category);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return String.Format(outputResult, categories.Count);
        }

        //Task 4
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var attr = new XmlRootAttribute("CategoryProducts");

            var serializer = new XmlSerializer(typeof(CategoryProductImportDto[]), attr);
            var catProductsDtos = (CategoryProductImportDto[])serializer.Deserialize(new StringReader(inputXml));

            var categoryIds = context.Categories.Select(c => c.Id);
            var productIds = context.Products.Select(p => p.Id);
            var categoryProducts = new List<CategoryProduct>();

            foreach (var catPro in catProductsDtos)
            {
                if (categoryIds.Contains(catPro.CategoryId) && productIds.Contains(catPro.ProductId))
                {
                    var categoryProduct = Mapper.Map<CategoryProduct>(catPro);
                    categoryProducts.Add(categoryProduct);
                }
            }

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return String.Format(outputResult, categoryProducts.Count);
        }

        //Task 5 - Get all products in a specified price range between 500 and 1000 (inclusive). Order them by price (from lowest to highest).
        //Select only the product name, price and the full name of the buyer. Take top 10 records.
        public static string GetProductsInRange(ProductShopContext context)
        {
            var selectedProducts = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ProductExportDto
                {
                    Name = p.Name,
                    Price = p.Price
                })
                .OrderBy(p => p.Price)
                .Take(10)
                .ToList();

            var serializer = new XmlSerializer(selectedProducts.GetType(), new XmlRootAttribute("Products"));

            var result = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(result), selectedProducts, namespaces);

            return result.ToString().TrimEnd();
        }

        //Task 6 -Get all users who have at least 1 sold item. Order them by last name, then by first name.
        //Select the person's first and last name. For each of the sold products, select the product's name and price. Take top 5 records. 
        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .Select(u => new UserProductsExportDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = new SoldProductsExportDto
                    {
                        Products = u.ProductsSold.Where(p => p.Buyer != null)
                        .Select(p => new ProductExportDto
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .ToArray()
                    }
                }).ToArray();

            var serializer = new XmlSerializer(users.GetType(), new XmlRootAttribute("Users"));
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var result = new StringBuilder();

            serializer.Serialize(new StringWriter(result), users, namespaces);

            return result.ToString().TrimEnd();
        }

        //Task 7 -Get all categories. For each category select its nameq, the number of products, the average price of those products 
        //and the total revenue (total price sum) of those products (regardless if they have a buyer or not). 
        //Order them by the number of products (descending) then by total revenue.
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .ProjectTo<CategoryExportDto>()        //        .Select(c=> new CategoryExportDto
                                                       //        {
                                                       //            Name = c.Name,
                                                       //            ProductsCount = c.CategoryProducts.Count,
                                                       //            AveragePrice = c.CategoryProducts.Average(p=>p.Product.Price),
                                                       //            TotalRevenue = c.CategoryProducts.Sum(p=>p.Product.Price)
                                                       //        })
                .OrderByDescending(c => c.ProductsCount)
                .ThenBy(c => c.TotalRevenue)
                .ToArray();

            var result = new StringBuilder();

            var serializer = new XmlSerializer(categories.GetType(), new XmlRootAttribute("Categories"));
            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(result), categories, xmlNamespaces);

            return result.ToString().TrimEnd();
        }

        //Task 8 -Select users who have at least 1 sold product. Order them by the number of sold products (from highest to lowest). 
        //Select only their first and last name, age, count of sold products and for each product 
        //- name and price sorted by price (descending). Take top 10 records.
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var usersProducts = context.Users
                .Where(u => u.ProductsSold.Any())
                .OrderByDescending(u => u.ProductsSold.Count)
                .Take(10)
                .Select(u => new UserAgeProductsExportDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new SoldProductsCountExportDto
                    {
                        Count = u.ProductsSold.Count,
                        Products = new SoldProductsExportDto
                        {
                            Products = u.ProductsSold
                            .Select(p => new ProductExportDto
                            {
                                Name = p.Name,
                                Price = p.Price
                            })
                            .OrderByDescending(p => p.Price)
                            .ToArray()
                        }
                    }
                })
                .ToArray();

            var users = new UsersExportDto
            {
                Count = context.Users.Count(p => p.ProductsSold.Any()),
                Users = usersProducts
            };

            var attr = new XmlRootAttribute("Users");
            var serializer = new XmlSerializer(users.GetType(), attr);
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var result = new StringBuilder();
            serializer.Serialize(new StringWriter(result), users, namespaces);
            //serializer.Serialize(new StreamWriter("../../../usersProducts.xml"), users, namespaces);

            return result.ToString().TrimEnd();
        }
    }
}