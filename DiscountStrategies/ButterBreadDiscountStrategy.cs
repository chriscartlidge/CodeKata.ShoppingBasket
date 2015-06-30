using System;
using System.Linq;
using System.Collections.Generic;

namespace ShoppingBasket
{
    public class ButterBreadDiscountStrategy : IDiscountStrategy
    {
        #region IDiscountStrategy implementation

        public decimal GetDiscount(IDictionary<string, decimal> priceStrategy, string items)
        {
            Guard(priceStrategy, items);

            var contents = items.Split(',');

            var breadCount = contents.Count(be => be == "bread");
            var butterCount = contents.Count(b => b == "butter");
          
            if (butterCount >= 2)
            {
                return Math.Ceiling(((decimal)breadCount) / 2) * (priceStrategy["bread"] / 2);
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

