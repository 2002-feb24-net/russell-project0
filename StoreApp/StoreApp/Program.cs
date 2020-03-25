using StoreApp.Data;
using StoreApp.Data.Model;
using System;

namespace StoreApp.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var getter = new InputGetter();
            var queryLib = new QueryLibrary();
            bool shopping = true;
            do
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Place an order (You need to provide Customer ID and Store ID).");
                Console.WriteLine("2. Add a new customer.");
                Console.WriteLine("3. Search customers by name (first and last).");
                Console.WriteLine("4. Display all order history items of a store location (By store ID).");
                Console.WriteLine("5. Display all order history items of a customer (By customer ID).");
                Console.WriteLine("6. Display all store locations.");
                Console.WriteLine("7. Display all customers.");
                Console.WriteLine("8. Display all order history items.");
                Console.WriteLine("9. Quit");

                int action = getter.GetInputInt("Input a number: ");

                switch (action)
                {
                    case 1:
                        Customer customer = null;
                        while (customer == null)
                        {
                            int cid = getter.GetInputInt("Input the customer's ID: ");
                            customer = queryLib.GetCustomer(cid);
                        }
                        Console.WriteLine("Hello " + customer.FirstName + " " + customer.LastName);
                        Store store = null;
                        while (store == null)
                        {
                            int storeID = getter.GetInputInt("Input the store's ID: ");
                            store = queryLib.GetStore(storeID);
                        }
                        Console.WriteLine("Welcome to " + store.Name);
                        queryLib.PrintStoreProducts(store);
                        var cart = queryLib.CreateShoppingCart(store);
                        queryLib.PlaceOrder(store, customer, cart);
                        break;
                    case 2:
                        string first = getter.GetInputString("Please enter the new customer's first name: ");
                        string last = getter.GetInputString("Please enter the new customer's last name: ");
                        int cnum = getter.GetInputInt("Please enter the new customer's card number: ");
                        queryLib.AddCustomer(first, last, cnum);
                        break;
                    case 3:
                        Customer cFind = null;
                        while (cFind == null)
                        {
                            string fname = getter.GetInputString("Please enter the customer's first name: ");
                            string lname = getter.GetInputString("Please enter the customer's last name: ");
                            cFind = queryLib.GetCustomer(fname, lname);
                        }
                        Console.WriteLine("Here's your customer's information.");
                        Console.WriteLine("ID: " + cFind.Id + ", Name: " + cFind.FirstName + " " + cFind.LastName + ", Card Number: " + cFind.CardNumber);
                        break;
                    case 4:
                        Store location = null;
                        while (location == null)
                        {
                            int sid = getter.GetInputInt("Please enter the store's ID: ");
                            location = queryLib.GetStore(sid);
                        }
                        queryLib.StoreOrderHistory(location);
                        break;
                    case 5:
                        Customer valuedCustomer = null;
                        while (valuedCustomer == null)
                        {
                            int vid = getter.GetInputInt("Please enter the customer's ID: "); 
                            valuedCustomer = queryLib.GetCustomer(vid);
                        }
                        queryLib.CustomerOrderHistory(valuedCustomer);
                        break;
                    case 6:
                        var storeLocs = queryLib.GetAllStoreLocations();
                        foreach (var sl in storeLocs)
                        {
                            Console.WriteLine(sl);
                        }
                        break;
                    case 7:
                        var customers = queryLib.GetAllCustomers();
                        foreach (var item in customers)
                        {
                            Console.WriteLine("Customer ID: " + item.Id + ", First Name: " + item.FirstName + ", Last Name: " +
                                              item.LastName + ", Card Number: " + item.CardNumber);
                        }
                        break;
                    case 8:
                        var orderHistoryItems = queryLib.GetAllOrderHistoryItems();
                        foreach (var item in orderHistoryItems)
                        {
                            Console.WriteLine("Order ID: " + item.Id + ", Store ID: " + item.StoreId + ", Customer ID: " +
                                item.CustomerId + " Order Date: " + item.OrderDate + ", Total: $" + item.Total);
                        }
                        break;
                    default:
                        shopping = false;
                        break;
                }
                Console.WriteLine();
            }
            while (shopping);
        }
    }
}