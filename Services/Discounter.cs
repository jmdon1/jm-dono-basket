using DonoBasket.Entities;
using DonoBasket.Repository;
using System.Collections.Generic;
using System.Linq;

namespace DonoBasket.Services
{
    public class Discounter : IDiscounter
    {
        private readonly ISpecialOffersRepository _offersRepo;

        public Discounter(ISpecialOffersRepository offersRepo)
        {
            _offersRepo = offersRepo;
        }

        public IEnumerable<Discount> GetDiscounts(IEnumerable<Product> products)
        {
            var specialOffers = _offersRepo.GetOffersByBasket(products);

            return specialOffers.Select(offer => new Discount
            {
                OfferName = offer.Name,
                // Generics or relection could be used here to call different methods based on different discount types
                Value = PercentageDiscount(products, offer)
            });
        }

        private decimal PercentageDiscount(IEnumerable<Product> products, SpecialOffer offer)
        {
            return offer.SpecialOfferThen.CurrentPrice * offer.SpecialOfferThen.Discount * GetMultiplier(offer, products) * -1;
        }

        /// <summary>
        /// Returns a multiplier based on the number of expected and actual products in the basket, 
        /// in comparison to the special offer.
        /// For example, if the offer is "Buy two of Product A and get one of Product B free" and the basket contains
        /// four of Product A and two of Product B, the method will return 2.
        /// </summary>
        private int GetMultiplier(SpecialOffer offer, IEnumerable<Product> products)
        {
            var whenMultiplier = products.Count(p => p.Id == offer.SpecialOfferWhen.ProductId) / offer.SpecialOfferWhen.Quantity;
            var thenMultiplier = products.Count(p => p.Id == offer.SpecialOfferThen.ProductId);

            return thenMultiplier < whenMultiplier ? thenMultiplier : whenMultiplier;
        }
    }
}
