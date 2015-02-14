using System;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Modules
{
    public class Database
    {
        public Dictionary<Type, object> DbSets = new Dictionary<Type, object>();

        public IEnumerable<T> DbSet<T>() where T : class
        {
            return DbSets[typeof(T)] as IEnumerable<T>;
        }
        public IEnumerable<Product> Products { get; set; }

        public Database()
        {
            Seeding();
            DbSets.Add(typeof(Product),Products);
        }

        private void Seeding()
        {
            Products = new List<Product>
            {
                new Product {Id = 1, Name = "Book", IsExempt = true},
                new Product {Id = 2, Name = "Music CD"},
                new Product {Id = 3, Name = "Chocolate Bar", IsExempt = true},
                new Product {Id = 4, Name = "Chocolate", IsExempt = true},
                new Product {Id = 5, Name = "perfume"}
            };
        }
    }
}