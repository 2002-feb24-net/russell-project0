using System;

namespace StoreLibrary
{
    public abstract class Product
    {
        protected string productName;
        protected double price;
        protected int productID;
        public double Price { get{ return price; } }
        public string ProductName { get{ return productName; } }
        public int ProductID { get { return productID; } }
        
        public override abstract string ToString();
    }
}