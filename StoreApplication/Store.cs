using System;
using System.Collections.Generic;

namespace StoreApplication
{
    class Store
    {
        List<Product> storeProducts;
        string storeName;
        string location;
        string storeID;


        public Store(string storeName, string location)
        {
            this.storeName = storeName;
            this.location = location;
            Random rand = new Random();
            storeID = "S" + rand.Next(10000, 100000);
            storeProducts = new List<Product>();
        }
        public void addStock(Product newProduct)
        {
            storeProducts.Add(newProduct);
        }

        public void addStock(List<Product> newProducts)
        {
            foreach(var prod in newProducts)
            {
                storeProducts.Add(prod);
            }
        }
        public string[] AllProducts()
        {
            string[] prods = new string[storeProducts.Count];
            for (int i = 0; i < storeProducts.Count; i++)
            {
                prods[i] = storeProducts[i].ToStringStore();
            }
            return prods;
        }
        public override string ToString()
        {
            return "Store Name: " + storeName + ", Location: " + location + ", ID: " + storeID;
        }
        public Product SelectProduct(int i)
        {
            return storeProducts[i];
        }
    }
}
