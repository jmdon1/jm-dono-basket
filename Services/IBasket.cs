using DonoBasket.Entities;
using System.Collections.Generic;

namespace DonoBasket.Services
{
    public interface IBasket
    {
        /// <summary>
        /// Prints a basket summary with Subtotal, Discounts and Total.
        /// </summary>
        /// <param name="input"> Accepts a string input in the format "PriceBasket item1 item2 item3"
        /// eg. "PriceBasket Apples Milk Bread".</param>
        void PriceBasket(string input);

        /// <summary>
        /// Prints a basket summary with Subtotal, Discounts and Total.
        /// </summary>
        /// <param name="products">A list of products (the basket).</param>
        void PriceBasket(IEnumerable<Product> products);
    }
}
