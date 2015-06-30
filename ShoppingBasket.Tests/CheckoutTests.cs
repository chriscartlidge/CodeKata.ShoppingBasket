using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace ShoppingBasket.Tests
{
    [TestFixture]
    public class CheckoutTests
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
        public void Given_Scan_Called_With_Null_Throw_Argument_Null_Exception()
        {
            // ACT
            new Checkout(priceStrategies).Scan(null);
        }

        [Test]
        public void Given_No_Items_In_Cart_Total_Should_Return_Zero()
        {
            // ACT
            var result = new Checkout(priceStrategies).Total();

            // ASSERT
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Given_One_Milk_Scanned_Total_Should_Return_1_15()
        {
            // ARRANGE
            var items = "milk";
            var checkout = new Checkout(priceStrategies);

            // ACT
            checkout.Scan(items);
            var result = checkout.Total();

            // ASSERT
            Assert.AreEqual(1.15, result);
        }

        [Test]
        public void Given_One_Bread_Butter_Milk_Total_Should_Return_2_95()
        {
            // ARRANGE
            var items = "bread,milk,butter"; 
            var checkout = new Checkout(priceStrategies);

            // ACT
            checkout.Scan(items);
            var result = checkout.Total();

            // ASSERT
            Assert.AreEqual(2.95, result);
        }

        [Test]
        public void Given_Two_Butter_Two_Bread_Total_Should_Return_3_10()
        {
            // ARRANGE
            var items = "bread,bread,butter,butter"; 

            var discounts = new List<IDiscountStrategy>
            {
                new ButterBreadDiscountStrategy()
            };

            var checkout = new Checkout(priceStrategies, discounts);

            // ACT
            checkout.Scan(items);
            var result = checkout.Total();

            // ASSERT
            Assert.AreEqual(3.10, result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Given_Discount_Strategies_Null_Throw_Argument_Null_Exception()
        {
            // ACT
            new Checkout(priceStrategies, null);
        }

        [Test]
        public void Given_Four_Milk_Total_Should_Return_3_45()
        {
            // ARRANGE
            var items = "milk,milk,milk,milk"; 

            var discounts = new List<IDiscountStrategy>
                {
                    new MilkDiscountStrategy()
                };

            var checkout = new Checkout(priceStrategies, discounts);

            // ACT
            checkout.Scan(items);
            var result = checkout.Total();

            // ASSERT
            Assert.AreEqual(3.45, result);
        }

        [Test]
        public void Given_Two_Butter_One_Bread_Eight_Milk_Total_Should_Return_9_00()
        {
            // ARRANGE
            var items = "butter,butter,bread,milk,milk,milk,milk,milk,milk,milk,milk"; 

            var discounts = new List<IDiscountStrategy>
                {
                    new MilkDiscountStrategy(),
                    new ButterBreadDiscountStrategy()
                };

            var checkout = new Checkout(priceStrategies, discounts);

            // ACT
            checkout.Scan(items);
            var result = checkout.Total();

            // ASSERT
            Assert.AreEqual(9.0, result);
        }

        [Test]
        public void Given_Four_Butter_One_Bread_Total_Should_Return_3_70()
        {
            // ARRANGE
            var items = "butter,butter,butter,butter,bread"; 

            var discounts = new List<IDiscountStrategy>
                {
                    new MilkDiscountStrategy(),
                    new ButterBreadDiscountStrategy()
                };

            var checkout = new Checkout(priceStrategies, discounts);

            // ACT
            checkout.Scan(items);
            var result = checkout.Total();

            // ASSERT
            Assert.AreEqual(3.7, result);
        }

        [Test]
        public void Given_Two_Butter_One_Bread_Four_Milk_No_Discount_Total_Should_Return_7_20()
        {
            // ARRANGE
            var items = "butter,butter,bread,milk,milk,milk,milk"; 
           
            var checkout = new Checkout(priceStrategies);

            // ACT
            checkout.Scan(items);
            var result = checkout.Total();

            // ASSERT
            Assert.AreEqual(7.2, result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Given_Price_Strategies_Is_Null_Throw_Argument_Null_Exception()
        {
            // ARRANGE
            var discounts = new List<IDiscountStrategy>
                {
                    new MilkDiscountStrategy(),
                    new ButterBreadDiscountStrategy()
                };

            // ACT
            new Checkout(null, discounts);
        }
    }
}

