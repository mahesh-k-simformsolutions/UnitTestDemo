using Database;
using Database.Tables;
using Demo.Web.Controllers;
using Demo.Web.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Demo.nUnit
{
    [TestFixture]
    public class CategoriesTests
    {
        private AppDbConext _appDbConext;
        private IProductRepository _repo;
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UnitTestDemoDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        [SetUp]
        public void Setup()
        {
            _appDbConext = new AppDbConext(new DbContextOptionsBuilder<AppDbConext>()
                .UseSqlServer(connectionString)
                .Options);
            _repo = new ProductRepository(_appDbConext);
        }

        [Test]
        public void GetCategories_ShouldReturnAllCategories()
        {
            _appDbConext.Categories.Add(new Category { Id = 1, Name = "Category 1"});
            _appDbConext.Categories.Add(new Category { Id = 2, Name = "Category 2" });
            _appDbConext.Categories.Add(new Category { Id = 3, Name = "Category 3" });


            Assert.AreEqual(3, _appDbConext.Categories.Local.Count);
        }

        [Test]
        public void DeleteCategory_ShouldReturnOK()
        {
            var item = new Category { Name = "CategoryTest" };
            _appDbConext.Categories.Add(item);
            var result1 = _appDbConext.SaveChangesAsync().Result;

            int id = _appDbConext.Categories.FirstOrDefaultAsync(x => x.Name == item.Name).Result.Id;
           
            var controller = new CategoriesController(_appDbConext);
            var result = controller.DeleteCategory(id).Result is NoContentResult;

            Assert.IsTrue(result);
        }
        [Test]
        public void PutCategory_ShouldReturnBadRequest()
        {
            var item = new Category { Id =11, Name = "CategoryTest" };
            int id = 10;

            var controller = new CategoriesController(_appDbConext);
            var result = controller.PutCategory(id, item).Result;

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [TearDown]
        public void TearDown()
        {
            _appDbConext.Dispose();
        }
    }
}