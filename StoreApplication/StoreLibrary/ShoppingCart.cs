using System;
using System.Collections.Generic;
using System.Text;

namespace StoreLibrary
{
    public class ShoppingCart
    {
        List<CartProduct> inCart = new List<CartProduct>();
        public int Count { get { return inCart.Count; } }

        public void AddToCart(CartProduct product)
        {
            inCart.Add(product);
        }
        public double TotalInCart()
        {
            double total = 0;
            foreach (var item in inCart)
            {
                total += item.Total;
            }
            return total;
        }
        public override string ToString()
        {
            string allItems = "";
            foreach (var item in inCart)
            {
                allItems += item.ToString() + "\n";
            }
            return allItems;
        }
        public CartProduct this[int x] { get { return inCart[x]; } }
    }
}
