using System;

namespace StoreApplication
{
    class Product
    {
        string _productName;
        int _stock;
        double _price;
        public double Price { get{ return _price; } }
        public string ProductName { get{ return _productName; } }
        
        public Product(string pName, int st, double pr)
        {
            _productName = pName;
            _stock = st;
            _price = pr;
        }
        public override string ToString()
        {
            return "Name: " + _productName + ", Price: " + _price + ", In stock: " + _stock;
        }
    }
}