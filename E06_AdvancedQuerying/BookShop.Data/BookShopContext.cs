﻿namespace BookShop.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class BookShopContext : DbContext
    {
		public BookShopContext() 
        { 
        }

		public BookShopContext(DbContextOptions options)
			:base(options) 
        { 
        }
		
        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<BookCategory> BooksCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionSettings.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}