using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Modules
{
    public interface IBusiness
    {
        double CalTax(Product product);
    }
}