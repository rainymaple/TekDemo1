using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Modules;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repository<Product> _repository;
        private readonly IBusiness _business;

        public HomeController()
        {
            _repository = new Repository<Product>();
            _business = new Business();
        }

        public HomeController(Repository<Product> repository, IBusiness business)
        {
            _repository = repository;
            _business = business;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        public ActionResult GetAllProducts()
        {
            var products = _repository.GetAll();
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PrintReceipt(IList<Product> products)
        {
            double total=0, salesTaxes=0;
            foreach (var product in products.ToList())
            {
                var prod = _repository.GetById(product.Id);
                product.Name = prod.Name;
                product.IsExempt = prod.IsExempt;
                var tax = _business.CalTax(product);
                product.PrintPrice = product.Price + tax;
                salesTaxes += tax;
                total += product.PrintPrice;
            }
            var receipt = new Receipt { Products = products, Total = total, SalesTaxes = Math.Round(salesTaxes,2) };
            return Json(receipt, JsonRequestBehavior.AllowGet);
        }

    }
}
