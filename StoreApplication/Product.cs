using System;

namespace StoreApplication
{
    class Product
    {
        string productName;
        int stock;
        public double price { get; set; }

        public Product(string pname, int q, double p)
        {
            productName = pname;
            stock = q;
            price = p;
        }
    }
}