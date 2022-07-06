using Database.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class AppDbConext : DbContext
    {
        public AppDbConext(DbContextOptions<AppDbConext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }    
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category { Id=1,Name = "Category 1" }); 
            modelBuilder.Entity<Product>().HasData(new Product { Id=1, Name="Product 1", CategoryId=1}); 
        }
    }
}
