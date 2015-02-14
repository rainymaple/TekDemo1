using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Models;
using WebApplication1.Modules;

namespace WebApplication1.Tests.TestClasses
{
    [TestClass]
    public class BusinessTest
    {
        private IBusiness _business;
        private Product _product;

        [TestInitialize]
        public void Init()
        {
            _business = new Business(0.1, 0.05);
            _product = new Product();
        }

        [TestMethod]
        public void CanGetTaxWithExemptionAndNotImported()
        {
            _product = new Product
            {
                Id = 1,
                Name = "Chocolate",
                IsExempt = true,    // Exempt
                Price = 12.23,
                IsImported = false, // Not Imported
                Amount = 1
            };
            var tax = _business.CalTax(_product);
            var finalPrice = Math.Round(_product.Price + tax, 2);
            Assert.AreEqual(_product.Price, finalPrice);
        }
        [TestMethod]
        public void CanGetTaxWithExemptionAndImported()
        {
            _product = new Product
            {
                Id = 1,
                Name = "banana",
                IsExempt = true,    // Exempt
                Price = 11.25,
                IsImported = true,  // Imported
                Amount = 1
            };
            var tax = _business.CalTax(_product);
            var finalPrice = Math.Round(_product.Price + tax, 2);
            Assert.AreEqual(11.85, finalPrice);
        }
        [TestMethod]
        public void CanGetTaxWithNoExemptionAndImported()
        {
            _product = new Product
            {
                Id = 1,
                Name = "Perfume",
                IsExempt = false,   // No Exempt  
                Price = 47.50,
                IsImported = true,  // Imported
                Amount = 1
            };
            var tax = _business.CalTax(_product);
            var finalPrice = Math.Round(_product.Price + tax, 2);
            Assert.AreEqual(54.65, finalPrice);
        }
        [TestMethod]
        public void CanGetTaxWithNoExemptionAndNotImported()
        {
            _product = new Product
            {
                Id = 1,
                Name = "Perfume",
                IsExempt = false,    // Not Exempt
                Price = 18.99,
                IsImported = false,  // Not Imported
                Amount = 1
            };
            var tax = _business.CalTax(_product);
            var finalPrice = Math.Round(_product.Price + tax, 2);
            Assert.AreEqual(20.89, finalPrice);
        }
    }
}
