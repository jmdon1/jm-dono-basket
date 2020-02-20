using DonoBasket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DonoBasket.Repository
{
    public class InMemorySpecialOffersRepository : ISpecialOffersRepository
    {
        public IEnumerable<SpecialOffer> SelectAll()
        {
            return new List<SpecialOffer>
            {
                new SpecialOffer{
                    WhenType = "ProductCombo",
                    SpecialOfferWhen = new SpecialOfferWhen { ProductId = 4, Quantity = 1 },
                    ThenType = "PercentDiscount",
                    // in DB persistence, the current price would be obtained using a join
                    SpecialOfferThen = new SpecialOfferThen{ ProductId = 4, Discount = 0.1M, CurrentPrice = 1M }
                    ,
                    StartDate = DateTime.Now.AddDays(-1),
                    EndDate = DateTime.Now.AddDays(6),
                    Name = "Apples 10% off"
                },
                new SpecialOffer{
                    WhenType = "ProductCombo",
                    SpecialOfferWhen = new SpecialOfferWhen { ProductId = 1, Quantity = 2  },
                    ThenType = "PercentDiscount",
                    SpecialOfferThen =
                        new SpecialOfferThen{ ProductId = 2, Discount = 0.5M, CurrentPrice = 0.80M }
                    ,
                    StartDate = DateTime.Now.AddDays(-1),
                    EndDate = DateTime.Now.AddDays(6),
                    Name = "Buy 2 tins of soup get a loaf of bread half price"
                }
            };
        }

        public IEnumerable<SpecialOffer> GetOffersByBasket(IEnumerable<Product> products)
        {
            // Convert list of products to Id | Total in order to check for matching offers
            var productsWithTotals = products
                .GroupBy(p => p.Id)
                .Select(g => new SpecialOfferWhen
                {
                    ProductId = g.Key,
                    Quantity = products.Count(p => p.Id == g.Key)
                });

            // to add new 'When' types we will use generics, 
            // e.g. the method name for ProductCombo or PercentageDiscount could be passed in 
            return SelectAll().Where(o =>
                WhenProductCombo(o, productsWithTotals)
                && ThenPercentageDiscount(o, productsWithTotals)
                && DateTime.Now >= o.StartDate
                && DateTime.Now <= o.EndDate)
                .Select(o => o);
        }

        private static bool WhenProductCombo(SpecialOffer o, IEnumerable<SpecialOfferWhen> productsWithTotals)
        {
            return productsWithTotals.Where(p => p.ProductId == o.SpecialOfferWhen.ProductId).Sum(p => p.Quantity) >= o.SpecialOfferWhen.Quantity;
        }

        private static bool ThenPercentageDiscount(SpecialOffer o, IEnumerable<SpecialOfferWhen> productsWithTotals)
        {
            return productsWithTotals.Any(p => p.ProductId == o.SpecialOfferThen.ProductId);
        }
    }
}
