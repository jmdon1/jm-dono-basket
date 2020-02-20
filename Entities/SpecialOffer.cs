using System;

namespace DonoBasket.Entities
{
    public class SpecialOffer
    {
        public string Name { get; set; }
        public SpecialOfferWhen SpecialOfferWhen { get; set; }
        public string WhenType { get; set; }
        public SpecialOfferThen SpecialOfferThen { get; set; }
        public string ThenType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
