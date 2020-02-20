using DonoBasket.Entities;
using DonoBasket.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DonoBasket.Services
{
    public class Basket : IBasket
    {
        private readonly IProductRepository _productRepository;
        private readonly IDiscounter _discounter;

        public Basket(
            IProductRepository productRepository,
            IDiscounter discounter)
        {
            _productRepository = productRepository;
            _discounter = discounter;
        }

        public void PriceBasket(string input)
        {
            IEnumerable<Product> products = input.Split(" ")
                .Skip(1)
                .Select(name => _productRepository.FindByName(name));

            if (products == null || products.Any(p => p == null))
            {
                Console.WriteLine("PriceBasket: Some products weren't found, " +
                    "please ensure you have entered valid product names");
                return;
            }

            PriceBasket(products);
        }

        public void PriceBasket(IEnumerable<Product> products)
        {
            // Print items
            var subtotal = products.Sum(p => p.Price);
            Console.WriteLine($"Subtotal: {subtotal.ToString("£0.00")}");

            // Print discounts
            decimal totalDiscounts = 0;
            var discounts = _discounter.GetDiscounts(products);
            foreach (var discount in discounts)
            {
                Console.WriteLine($"{discount.OfferName}: {discount.Value.ToString("£0.00")}");
                totalDiscounts += discount.Value;
            }
            if (!discounts.Any())
            {
                Console.WriteLine("(no offers available)");
            }

            // Print total
            Console.WriteLine($"Total: {(subtotal + totalDiscounts).ToString("£0.00")}");
        }
    }
}
