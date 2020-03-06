using System;
using System.Collections.Generic;

namespace StoreApplication
{
    class Customer
    {
        static List<Customer> listOfCustomers = new List<Customer>();
        string name { get; set; }
        public string customerID { get; }
        List<Order> orderHistory;

        public Customer(string n)
        {
            name = n;
            Random rand = new Random();
            customerID = "C" + rand.Next(10000, 100000);
            listOfCustomers.Add(this);
        }

        public void PlaceOrder()
        {

        }
    }
}
