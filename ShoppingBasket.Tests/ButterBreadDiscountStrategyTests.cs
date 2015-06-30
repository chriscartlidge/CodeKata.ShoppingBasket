using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ShoppingBasket.Tests
{
    public class ButterBreadDiscountStrategyTests
    {
        private Dictionary<string, decimal> priceStrategies;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            priceStrategies = new Dictionary<string, decimal>
            {
                { "butter", 0.8m },
                { "milk", 1.15m },
                { "bread", 1.0m }
            };
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Given_Null_Price_Strategy_Then_Throw_Argument_Null_Exception()
        {
            // ACT
            new ButterBreadDiscountStrategy().GetDiscount(null, "butter");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Given_Null_Items_Then_Throw_Argument_Null_Exception()
        {
            // ACT
            new ButterBreadDiscountStrategy().GetDiscount(new Dictionary<string, decimal>(), "");
        }

        [Test]
        public void Given_Three_Butter_No_Bread_Then_Return_No_Discount()
        {
            // ARRANGE
            var items = "butter,butter,butter";

            // ACT
            var result = new ButterBreadDiscountStrategy().GetDiscount(priceStrategies, items);

            // ASSERT
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Given_Three_Butter_One_Bread_Return_0_5_Discount()
        {
            // ARRANGE
            var items = "butter,butter,butter,bread";

            // ACT
            var result = new ButterBreadDiscountStrategy().GetDiscount(priceStrategies, items);

            // ASSERT
            Assert.AreEqual(0.5, result);          
        }
    }
}

