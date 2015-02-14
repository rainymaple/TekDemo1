using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Product : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsExempt { get; set; }
        public bool IsImported { get; set; }
        public double PrintPrice { get; set; }
        public int Amount { get; set; }

    }
}