using DonoBasket.Entities;
using DonoBasket.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DonoBasket.UnitTests.Repository
{
    [TestClass]
    [TestCategory("Repository.InMemorySpecialOffersRepository")]
    public class InMemorySpecialOffersRepositoryTests
    {
        [TestMethod]
        public void GetOffersByBasket_With2Soups_ReturnsEmptyList()
        {
            // Arrange
            var sut = new InMemorySpecialOffersRepository();
            var soupProducts = new List<Product>
            {
                new Product{Id = 1, Name = "Soup", Price = 0.65M},
                new Product{Id = 1, Name = "Soup", Price = 0.65M}
            };

            // Act
            var result = sut.GetOffersByBasket(soupProducts);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetOffersByBasket_With2Soup1Bread_ReturnsSoupSpecialOffer()
        {
            // Arrange
            var sut = new InMemorySpecialOffersRepository();
            List<Product> soupBreadProducts = new List<Product>
            {
                new Product{Id = 1, Name = "Soup", Price = 0.65M},
                new Product{Id = 1, Name = "Soup", Price = 0.65M},
                new Product{Id = 2, Name = "Bread", Price = 0.80M}
            };

            // Act
            var result = sut.GetOffersByBasket(soupBreadProducts);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Buy 2 tins of soup get a loaf of bread half price", result.FirstOrDefault()?.Name);
        }

        [TestMethod]
        public void GetOffersByBasket_With1Apple_ReturnsApple10PercentDiscount()
        {
            // Arrange
            var sut = new InMemorySpecialOffersRepository();
            var applesProducts = new List<Product>
            {
                new Product{Id = 4, Name = "Apples", Price = 1M}
            };

            // Act
            var result = sut.GetOffersByBasket(applesProducts);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Apples 10% off", result.FirstOrDefault()?.Name);
        }

        [TestMethod]
        public void GetOffersByBasket_WithoutMatchingOffers_ReturnsEmptyList()
        {
            // Arrange        
            var breadProducts = new List<Product>
            {
                new Product { Id = 2, Name = "Bread", Price = 0.80M }
            };
            var sut = new InMemorySpecialOffersRepository();

            // Act
            var result = sut.GetOffersByBasket(breadProducts);

            // Assert
            Assert.AreEqual(0, result.Count());
        }
    }
}
