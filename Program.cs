using DonoBasket.Repository;
using DonoBasket.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DonoBasket
{
    internal static class Program
    {
        private static void Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IProductRepository, InMemoryProductRepository>()
                .AddTransient<ISpecialOffersRepository, InMemorySpecialOffersRepository>()
                .AddTransient<IBasket, Basket>()
                .AddTransient<IDiscounter, Discounter>()
                .BuildServiceProvider();

            while (true)
            {
                string input = Console.ReadLine();
                if (string.Equals(input.Split(" ")[0], "PriceBasket", StringComparison.OrdinalIgnoreCase))
                {
                    serviceProvider.GetService<IBasket>().PriceBasket(input);
                }
                else
                {
                    Console.WriteLine("Please enter a valid input");
                }
            }
        }
    }
}
