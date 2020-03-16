using System;
using System.Collections.Generic;
using System.Text;

namespace StoreLibrary
{
    public class CartProduct : Product
    {
        int quantity;
        public int Quantity { get { return quantity; } }
        public double Total { get { return quantity * price; } }

        public CartProduct(string productName, int productID, int quantity, double price)
        {
            this.productName = productName;
            this.productID = productID;
            this.quantity = quantity;
            this.price = price;
        }

        public override string ToString()
        {
            return "PID: " + ProductID + ", PName: " + productName + ", Quantity: " + Quantity + ", Price for each: $" + Price  + ", Total Cost: $" + Total;
        }
    }
}
