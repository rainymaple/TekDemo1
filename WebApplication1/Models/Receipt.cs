using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Receipt
    {
        public IEnumerable<Product> Products { get; set; }
        public double SalesTaxes { get; set; }
        public double Total { get; set; }
    }
}