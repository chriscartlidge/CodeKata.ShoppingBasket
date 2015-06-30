using System;
using System.Collections.Generic;

namespace ShoppingBasket
{
    public interface IDiscountStrategy
	{
        decimal GetDiscount(IDictionary<string, decimal> priceStrategy, string items);
	}
}

