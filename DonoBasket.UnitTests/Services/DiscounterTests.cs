using DonoBasket.Entities;
using DonoBasket.Repository;
using DonoBasket.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DonoBasket.UnitTests.Services
{
    [TestClass]
    [TestCategory("Services.Discounter")]
    public class DiscounterTests
    {
        private readonly InMemorySpecialOffersRepository offersRepo = new InMemorySpecialOffersRepository();

        [TestMethod]
        public void GetDiscounts_WithoutOffers_ReturnsEmptyList()
        {
            var products = new List<Product> {
                new Product{ Id = 2, Name = "Bread", Price = 0.80M }
            };
            var discounter = new Discounter(offersRepo);

            var result = discounter.GetDiscounts(products);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetDiscounts_With2Soup1Bread_Returns40pDiscount()
        {
            var products = new List<Product> {
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 2, Name = "Bread", Price = 0.80M }
            };
            var discounter = new Discounter(offersRepo);

            var result = discounter.GetDiscounts(products);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(-0.40M, result.Sum(r => r.Value));
        }

        [TestMethod]
        public void GetDiscounts_With2Soup2Bread_Returns40pDiscount()
        {
            var products = new List<Product> {
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 2, Name = "Bread", Price = 0.80M },
                new Product{ Id = 2, Name = "Bread", Price = 0.80M }
            };
            var discounter = new Discounter(offersRepo);

            var result = discounter.GetDiscounts(products);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(-0.40M, result.Sum(r => r.Value));
        }

        [TestMethod]
        public void GetDiscounts_With3Soup1Bread_Returns40pDiscount()
        {
            var products = new List<Product> {
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 2, Name = "Bread", Price = 0.80M }
            };
            var discounter = new Discounter(offersRepo);

            var result = discounter.GetDiscounts(products);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(-0.40M, result.Sum(r => r.Value));
        }

        [TestMethod]
        public void GetDiscounts_With4Soup1Bread_Returns40pDiscount()
        {
            var products = new List<Product> {
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 2, Name = "Bread", Price = 0.80M }
            };
            var discounter = new Discounter(offersRepo);

            var result = discounter.GetDiscounts(products);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(-0.40M, result.Sum(r => r.Value));
        }

        [TestMethod]
        public void GetDiscounts_With4Soup2Bread_Returns80pDiscount()
        {
            var products = new List<Product> {
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 2, Name = "Bread", Price = 0.80M },
                new Product{ Id = 2, Name = "Bread", Price = 0.80M }
            };
            var discounter = new Discounter(offersRepo);

            var result = discounter.GetDiscounts(products);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(-0.80M, result.Sum(r => r.Value));
        }

        [TestMethod]
        public void GetDiscounts_With1Apples_Returns10pDiscount()
        {
            var products = new List<Product> {
                new Product{ Id = 4, Name = "Apples", Price = 1M }
            };
            var discounter = new Discounter(offersRepo);

            var result = discounter.GetDiscounts(products);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(-0.10M, result.Sum(r => r.Value));
        }

        [TestMethod]
        public void GetDiscounts_With5Apples_Returns50pDiscount()
        {
            var products = new List<Product> {
                new Product{ Id = 4, Name = "Apples", Price = 1M },
                new Product{ Id = 4, Name = "Apples", Price = 1M },
                new Product{ Id = 4, Name = "Apples", Price = 1M },
                new Product{ Id = 4, Name = "Apples", Price = 1M },
                new Product{ Id = 4, Name = "Apples", Price = 1M }
            };
            var discounter = new Discounter(offersRepo);

            var result = discounter.GetDiscounts(products);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(-0.50M, result.Sum(r => r.Value));
        }

        [TestMethod]
        public void GetDiscounts_WithApplesMilkBread_Returns10pDiscount()
        {
            var products = new List<Product> {
                new Product{ Id = 4, Name = "Apples", Price = 1M },
                new Product{ Id = 3, Name = "Milk", Price = 1.30M },
                new Product{ Id = 2, Name = "Bread", Price = 0.80M }
            };
            var discounter = new Discounter(offersRepo);

            var result = discounter.GetDiscounts(products);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(-0.10M, result.Sum(r => r.Value));
        }

        [TestMethod]
        public void GetDiscounts_WithMultipleOffers_Returns50pDiscount()
        {
            var products = new List<Product> {
                new Product{ Id = 4, Name = "Apples", Price = 1M },
                new Product{ Id = 3, Name = "Milk", Price = 1.30M },
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 1, Name = "Soup", Price = 0.65M },
                new Product{ Id = 2, Name = "Bread", Price = 0.80M }
            };
            var discounter = new Discounter(offersRepo);

            var result = discounter.GetDiscounts(products);

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(-0.50M, result.Sum(r => r.Value));
        }
    }
}
