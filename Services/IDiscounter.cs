using DonoBasket.Entities;
using System.Collections.Generic;

namespace DonoBasket.Services
{
    public interface IDiscounter
    {
        /// <summary>
        /// Given a basket of products the method will look up any applicable special offers
        /// and return a list of calculated discounts.
        /// </summary>
        /// <param name="products"></param>
        IEnumerable<Discount> GetDiscounts(IEnumerable<Product> products);
    }
}