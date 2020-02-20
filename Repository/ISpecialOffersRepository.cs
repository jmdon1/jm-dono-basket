using DonoBasket.Entities;
using System.Collections.Generic;

namespace DonoBasket.Repository
{
    public interface ISpecialOffersRepository
    {
        /// <summary>
        /// Returns all Special Offers.
        /// </summary>
        /// <returns></returns>
        IEnumerable<SpecialOffer> SelectAll();

        /// <summary>
        /// Given a list of Product, will return any applicable offers with matching When and Then criteria.
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        IEnumerable<SpecialOffer> GetOffersByBasket(IEnumerable<Product> products);
    }
}
