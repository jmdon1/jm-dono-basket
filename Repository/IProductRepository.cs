using DonoBasket.Entities;
using System.Collections.Generic;

namespace DonoBasket.Repository
{
    public interface IProductRepository
    {
        /// <summary>
        /// Returns all Products.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Product> SelectAll();

        /// <summary>
        /// Returns a product by name.
        /// </summary>
        /// <param name="name">The product name, case insensitive.</param>
        /// <returns></returns>
        Product FindByName(string name);
    }
}
