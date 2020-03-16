using System;
using System.Collections.Generic;

namespace StoreLibrary
{
    public class Store
    {
        List<StoreProduct> storeProducts = new List<StoreProduct>();
        string storeName;
        string location;
        int storeID;

        public Store(string storeName, string location, int storeID)
        {
            this.storeName = storeName;
            this.location = location;
            this.storeID = storeID;
        }

        public string[] AllProducts()
        {
            string[] prodsAsString = new string[storeProducts.Count];
            for (int i = 0; i < storeProducts.Count; i++)
            {
                prodsAsString[i] = storeProducts[i].ToString();
            }
            return prodsAsString;
        }
        public override string ToString()
        {
            return "Store Name: " + storeName + ", Location: " + location + ", ID: " + storeID;
        }
        public void SelectProduct(Customer customer, int id, int amount)
        {
            StoreProduct selProd = null;
            foreach (var prod in storeProducts)
            {
                if (id == prod.ProductID)
                    selProd = prod;
            }
            if (selProd == null)
            {
                Console.WriteLine("Product not found.");
            }
            else
            {
                if (amount > selProd.Stock)
                {
                    Console.WriteLine("Not enough " + selProd.ProductName + " in stock.");
                }
                else
                {
                    selProd.Stock -= amount;
                    var outProduct = new CartProduct(selProd.ProductName, selProd.ProductID, amount, selProd.Price);
                    customer.AddToCart(outProduct);
                }
            }
        }
        public void PlaceItemsBackOnShelf(ShoppingCart items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                foreach (var item in storeProducts)
                {
                    if (items[i].ProductID == item.ProductID)
                    {
                        item.Stock += items[i].Quantity;
                        break;
                    }
                }
            }
        }
    }
}
