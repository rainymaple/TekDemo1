using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Models;
using WebApplication1.Modules;

namespace WebApplication1.Tests.TestClasses
{
    [TestClass]
    public class RepositoryTest
    {
        private Database _db;
        private Repository<Product> _repository;

        [TestInitialize]
        public void Init()
        {
            _db = new Database();
            _repository = new Repository<Product>(_db);
        }

        [TestMethod]
        public void CanGetAll()
        {
            var products = _repository.GetAll();
            Assert.IsTrue(products.Any());
        }
        [TestMethod]
        public void CanGetById()
        {
            var product = _repository.GetById(1);
            Assert.IsNotNull(product);
        }
    }
}
