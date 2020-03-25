using System;
using System.Collections.Generic;

namespace StoreLibrary
{
    public class Order
    { 
        ShoppingCart itemsBought;
        Customer myCustomer;
        Store myStore;
        DateTime date;
        int orderID;

        public Order(Store store, Customer customer, ShoppingCart items)
        {
            myStore = store;
            myCustomer = customer;
            itemsBought = items;
            date = DateTime.Today;
            Random rand = new Random();
            orderID = rand.Next(1000, 10000);
        }

        public void PrintReceipt()
        {
            Console.WriteLine("Order ID: " + orderID);
            Console.WriteLine(myStore.ToString());
            Console.WriteLine(myCustomer.ToString());
            Console.WriteLine("Date: " + date);
            Console.WriteLine(itemsBought.ToString());
            Console.WriteLine("Total: $" + itemsBought.TotalInCart());
        }
    }
}
