using System;

namespace StoreApplication
{
    class Product
    {
        string productName;
        int quantity;
        double price;
        public double Price { get{ return price; } }
        public string ProductName { get{ return productName; } }
        public int Quantity { get{ return quantity; } }
        
        public Product(string productName, int quantity, double price)
        {
            this.productName = productName;
            this.quantity = quantity;
            this.price = price;
        }
        public string ToStringStore()
        {
            return "Name: " + productName + ", Price: $" + price + ", In stock: " + quantity;
        }
        public string ToStringReceipt()
        {
            return "Name: " + productName + ", Price for each: $" + price + ", Amount: " + quantity + " Cost: $" + (_price * _quantity);
        }
    }
}