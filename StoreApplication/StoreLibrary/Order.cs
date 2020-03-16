using System;
using System.Collections.Generic;

namespace StoreLibrary
{
    public class Order
    { 
        static List<Order> orderHistory = new List<Order>();
        ShoppingCart itemsBought;
        Customer myCustomer;
        Store myStore;
        int orderID;

        public Order(Store store, Customer customer, ShoppingCart items)
        {
            myStore = store;
            myCustomer = customer;
            itemsBought = items;
            orderHistory.Add(this);
            Random rand = new Random();
            orderID = rand.Next(1000, 10000);
        }

        public void PrintReceipt()
        {
            Console.WriteLine("Order ID: " + orderID);
            Console.WriteLine(myStore.ToString());
            Console.WriteLine(myCustomer.ToString());
            Console.WriteLine(itemsBought.ToString());
            Console.WriteLine("Total: $" + itemsBought.TotalInCart());
        }
    }
}
