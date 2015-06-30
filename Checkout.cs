using System;
using System.Collections.Generic;

namespace ShoppingBasket
{
    public class Checkout
    {
        private readonly IList<IDiscountStrategy> discountStrategies;
        private readonly IDictionary<string, decimal> priceStrategy;

        private decimal total;

        public Checkout(
            IDictionary<string, decimal> priceStrategy, 
            IList<IDiscountStrategy> discountStrategies)
        {
            if (priceStrategy == null)
            {
                throw new ArgumentNullException("priceStrategy");
            }

            if (discountStrategies == null)
            {
                throw new ArgumentNullException("discountStrategies");
            }
               
            this.priceStrategy = priceStrategy;
            this.discountStrategies = discountStrategies;
        }

        public Checkout(IDictionary<string, decimal> priceStrategy) 
            : this (priceStrategy, new List<IDiscountStrategy>())
        {
        }

        public void Scan(string items)
        {
            if (string.IsNullOrWhiteSpace(items))
            {
                throw new ArgumentNullException("items");
            }

            foreach (var item in items.Split(','))
            {
                if (priceStrategy.ContainsKey(item))
                {
                    total += priceStrategy[item];
                }
            }

            // Now let's apply the discount.
            foreach (var discount in discountStrategies)
            {
                total -= discount.GetDiscount(priceStrategy, items);
            }
        }

        public decimal Total()
        {
            return total;
        }
    }
}

