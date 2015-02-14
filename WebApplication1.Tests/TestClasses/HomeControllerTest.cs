using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;
using WebApplication1.Models;
using WebApplication1.Modules;

namespace WebApplication1.Tests.TestClasses
{
    [TestClass]
    public class HomeControllerTest
    {
        private Database _db;
        private Repository<Product> _repository;
        private IBusiness _business;
        private HomeController _homeController;
        private IList<Product> _product;

        [TestInitialize]
        public void Init()
        {
            _db = new Database();
            _repository = new Repository<Product>(_db);
            _business = new Business();
            _homeController = new HomeController(_repository, _business);
            _product = new List<Product>
            {
                new Product {Id = 1, Name = "Book", IsExempt = true,Price = 12.23,IsImported = true,Amount = 2},
                new Product {Id = 2, Name = "Music CD",Price = 1.99,IsImported = false,Amount = 1}
            };
        }
        [TestMethod]
        public void Index()
        {
            var result = _homeController.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
        [TestMethod]
        public void CanGetAllProducts()
        {
            var products = _homeController.GetAllProducts() as JsonResult;
            Assert.IsNotNull(products);
        }

        [TestMethod]
        public void CanCreateReceit()
        {
            var jsonResult = _homeController.PrintReceipt(_product) as JsonResult;
            Assert.IsNotNull(jsonResult);
            var receipt = jsonResult.Data as Receipt;
            Assert.IsNotNull(receipt);
            Assert.AreEqual(receipt.Products.Count(), 2);
            Assert.IsTrue(receipt.SalesTaxes > 0);
            Assert.IsTrue(receipt.Total > 0);
        }
    }
}
