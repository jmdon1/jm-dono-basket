using DonoBasket.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DonoBasket.Repository
{
    public class InMemoryProductRepository : IProductRepository
    {
        public IEnumerable<Product> SelectAll()
        {
            return new List<Product>
            {
                new Product{Id = 1, Name = "Soup", Price = 0.65M },
                new Product{Id = 2, Name = "Bread", Price = 0.80M },
                new Product{Id = 3, Name = "Milk", Price = 1.30M },
                new Product{Id = 4, Name = "Apples", Price = 1M },
            };
        }

        public Product FindByName(string name)
        {
            return SelectAll().FirstOrDefault(p => string.Equals(p.Name, name, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}
