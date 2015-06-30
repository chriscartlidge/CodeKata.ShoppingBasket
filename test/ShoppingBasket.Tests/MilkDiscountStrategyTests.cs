using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace ShoppingBasket.Tests
{
    [TestFixture]
    public class MilkDiscountStrategyTests
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
            new MilkDiscountStrategy().GetDiscount(null, "milk");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Given_Null_Items_Then_Throw_Argument_Null_Exception()
        {
            // ACT
            new MilkDiscountStrategy().GetDiscount(new Dictionary<string, decimal>(), "");
        }

        [Test]
        public void Given_One_Milk_Then_Return_No_Discount()
        {
            // ARRANGE
            var items = "milk";

            // ACT
            var result = new MilkDiscountStrategy().GetDiscount(priceStrategies, items);

            // ASSERT
            Assert.AreEqual(0, result);
        }

        [TestCase(8, 2.3)]
        [TestCase(4, 1.15)]
        [TestCase(16, 5.75)]
        public void Given_Multiply_Milk_Then_Return_Correct_Discount(int milkAmount, decimal expected)
        {
            // ARRANGE
            var items =  GenerateMilk(milkAmount);

            // ACT
            var result = new MilkDiscountStrategy().GetDiscount(priceStrategies, items);

            // ASSERT
            Assert.AreEqual(expected, result);
        }

        private static string GenerateMilk(int amount){
            var milks = new List<string>();

            for (int i = 0; i < amount; i++)
            {
                milks.Add("milk");
            }

            return  string.Join(",", milks);
        }
    }
}

