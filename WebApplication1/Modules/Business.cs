using System;
using WebApplication1.Models;

namespace WebApplication1.Modules
{
    public class Business : IBusiness
    {
        private readonly double _rate;
        private readonly double _dutyRate;

        public Business(double rate, double dutyRate)
        {
            _rate = rate;
            _dutyRate = dutyRate;
        }
        public Business()
        {
            _rate = 0.1;
            _dutyRate = 0.05;
        }

        public double CalTax(Product product)
        {
            var rate = (product.IsExempt ? 0 : _rate) + (product.IsImported ? _dutyRate : 0);

            var tax = product.Price * product.Amount * rate;
            if (tax > 0)
            {
                tax = Math.Ceiling(20 * tax) * 0.05;
            }

            return tax;

        }

    }
}