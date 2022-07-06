using Database;
using Database.Tables;
using Demo.Web.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Demo.xUnit
{
    public class ProductTests
    {
        private AppDbConext _appDbConext;
        private IProductRepository _repo;
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UnitTestDemoDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public ProductTests()
        {
            _appDbConext = new AppDbConext(new DbContextOptionsBuilder<AppDbConext>()
                .UseSqlServer(connectionString)
                .Options);
            _repo = new ProductRepository(_appDbConext);    
        }

        [Fact]
        public async void GetProduct_Test_Ok()
        {
            //Arrange
            var id = 1;

            //Act
            var data = await _repo.Get(id); 

            //Assert
            Assert.Equal(id, data.Id);
        }

        [Fact]
        public async void GetProduct_Test_NotFound()
        {
            //Arrange
            var id = 0;

            //Act
            var data = await _repo.Get(id);

            //Assert
            Assert.Equal(data, null);
        }

        [Fact]
        public async void PostProduct_Test_Ok()
        {
            //Arrange
            var data = new Product() { Name = "Product2", CategoryId = 1 };

            //Act
            var result = await _repo.Add(data);

            //Assert
            Assert.Equal(1, result);
        }

        [Theory]
        [InlineData(400,50,200)]
        [InlineData(368,33,246.56)]
        public void Calculate_Discount_Test(decimal price, float discount, decimal expected)
        {
            //Arrange
            var data = new Product() { Name = "Product3", Price = price, DiscountInPercent = discount };
            //Act
            var result = data.DiscountedPrice;

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
