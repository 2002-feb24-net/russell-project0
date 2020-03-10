using System;
using System.Collections.Generic;

namespace StoreApplication
{
    class Order
    { 
        static List<Order> orderHistory = new List<Order>();
        List<Product> orderedProducts;
        Customer myCustomer;
        Store myStore;
        string orderID;

        public Order(Store store, Customer customer, List<Product> products)
        {
            myStore = store;
            myCustomer = customer;
            orderedProducts = products;
            Random rand = new Random();
            orderHistory.Add(this);
        }

        public void PrintReceipt()
        {
            Console.WriteLine("Order ID: " + orderID);
            Console.WriteLine(myStore.ToString());
            Console.WriteLine(myCustomer.ToString());
            double total = 0;
            foreach(var item in orderedProducts)
            {
                Console.WriteLine(item.ToStringReceipt());
                total += item.Price * item.Quantity;
            }
        }
    }
}
