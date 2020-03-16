using System;
using System.Collections.Generic;
using System.Text;

namespace StoreLibrary
{
    public class StoreProduct : Product
    {
        int stock;
        public int Stock { get { return stock; } set { if(value >= 0) Stock = value; } }

        public StoreProduct(string productName, int productID, int stock, double price)
        {
            this.productName = productName;
            this.productID = productID;
            this.stock = stock;
            this.price = price;
        }
        public override string ToString()
        {
            return "PID: " + ProductID + ", PName: " + productName + ", Price: $" + price + ", Stock: " + stock;
        }
    }
}
