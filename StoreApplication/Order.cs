using System;
using System.Collections.Generic;

namespace StoreApplication
{
    class Order
    { 
        static List<Order> orderHistory = new List<Order>();
        List<Product> orderedProducts;
        List<int> productQuantities;
        Customer myCustomer;
        Store myStore;
        string orderID;

        public Order(Store sto, Customer cust, List<Product> prods, List<int> quantities)
        {
            myStore = sto;
            myCustomer = cust;
            orderedProducts = prods;
            Random rand = new Random();
            orderHistory.Add(this);
        }

        public double GetTotal()
        {
            double total = 0;
            foreach(var item in orderedProducts)
            {
                total += item.Total;
            }
            return total;
        }
    }
}
