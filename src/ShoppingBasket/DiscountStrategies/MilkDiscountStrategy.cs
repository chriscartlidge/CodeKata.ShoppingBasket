using System;
using System.Linq;
using System.Collections.Generic;

namespace ShoppingBasket
{
    public class MilkDiscountStrategy : IDiscountStrategy
    {

        #region IDiscountStrategy implementation

        public decimal GetDiscount(IDictionary<string, decimal> priceStrategy, string items)
        {
            Guard(priceStrategy, items);

            var contents = items.Split(',');

            var milkCount = contents.Count(be => be == "milk");

            // Apply offer is more than 3 milks.
            if (milkCount > 3)
            {
                return Math.Floor(((decimal)milkCount / 3)) * priceStrategy["milk"];
            }

            return 0;
        }

        #endregion

        private static void Guard(IDictionary<string, decimal> priceStrategy, string items)
        {
            if (priceStrategy == null)
            {
                throw new ArgumentNullException("priceStrategy");
            }

            if (string.IsNullOrWhiteSpace(items))
            {
                throw new ArgumentNullException("items");
            }
        }
    }
}

