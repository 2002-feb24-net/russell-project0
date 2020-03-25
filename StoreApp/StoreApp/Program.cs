using StoreApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreApp.App
{
    class Program
    {
        static void Main(string[] args)
        {
            bool shopping = true;
            do
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Place an order.");
                Console.WriteLine("2. Add a new customer.");
                Console.WriteLine("3. Search customers by name.");
                Console.WriteLine("4. Display all order history items of a store location.");
                Console.WriteLine("5. Display all order history items of a customer.");
                Console.WriteLine("6. Display all store locations.");
                Console.WriteLine("7. Display all customers.");
                Console.WriteLine("8. Display all order history items.");
                Console.WriteLine("9. Quit");

                int action = GetInputInt("Input a number: ");

                switch (action)
                {
                    case 1:
                        Customer customer = null;
                        while (customer == null)
                        {
                            int cid = GetInputInt("Input the customer's ID: ");
                            customer = GetCustomer(cid);
                        }
                        Console.WriteLine("Hello " + customer.FirstName + " " + customer.LastName);
                        Store store = null;
                        while (store == null)
                        {
                            int storeID = GetInputInt("Input the store's ID: ");
                            store = GetStore(storeID);
                        }
                        Console.WriteLine("Welcome to " + store.Name);
                        PrintStoreProducts(store);
                        var cart = CreateShoppingCart(store);
                        PlaceOrder(store, customer, cart);
                        break;
                    case 2:
                        string first = GetInputString("Please enter the new customer's first name: ");
                        string last = GetInputString("Please enter the new customer's last name: ");
                        int cnum = GetInputInt("Please enter the new customer's card number: ");
                        AddCustomer(first, last, cnum);
                        break;
                    case 3:
                        Customer cFind = null;
                        while (cFind == null)
                        {
                            string fname = GetInputString("Please enter the customer's first name: ");
                            string lname = GetInputString("Please enter the customer's last name: ");
                            cFind = GetCustomer(fname, lname);
                        }
                        Console.WriteLine("Here's your customer's information.");
                        Console.WriteLine("ID: " + cFind.Id + ", Name: " + cFind.FirstName + " " + cFind.LastName + ", Card Number: " + cFind.CardNumber);
                        break;
                    case 4:
                        Store location = null;
                        while (location == null)
                        {
                            int sid = GetInputInt("Please enter the store's ID: ");
                            location = GetStore(sid);
                        }
                        StoreOrderHistory(location);
                        break;
                    case 5:
                        Customer valuedCustomer = null;
                        while (valuedCustomer == null)
                        {
                            int vid = GetInputInt("Please enter the customer's ID: "); 
                            valuedCustomer = GetCustomer(vid);
                        }
                        CustomerOrderHistory(valuedCustomer);
                        break;
                    case 6:
                        ShowAllStoreLocations();
                        break;
                    case 7:
                        ShowAllCustomers();
                        break;
                    case 8:
                        ShowAllOrderHistoryItems();
                        break;
                    default:
                        shopping = false;
                        break;
                }
                Console.WriteLine();
            }
            while (shopping);
        }

        // This method gets a non-empty input string from the user.
        static string GetInputString(string msg)
        {
            string input = null;
            do
            {
                Console.Write(msg);
                input = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(input));
            return input;
        }

        // This method gets an integer from the user.
        static int GetInputInt(string msg)
        {
            bool inputValid = false;
            int inputInt = 0;
            while (!inputValid)
            {
                string strInput = GetInputString(msg);
                try
                {
                    inputInt = int.Parse(strInput);
                    inputValid = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error! Please input a number.");
                }
            }
            return inputInt;
        }

        // This method gets a Customer when provided with an ID.
        static Customer GetCustomer(int id)
        {
            using (var context = new RGProject0Context())
            {
                Customer customer = null;
                try
                {
                    customer = context.Customer.First(c => c.Id == id);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Error! Customer not found.");
                }
                return customer;
            }
        }
        // This method gets a Customer when provided with a first and last name.
        static Customer GetCustomer(string fname, string lname)
        {
            using (var context = new RGProject0Context())
            {
                Customer customer = null;
                try
                {
                    customer = context.Customer.First(c => c.FirstName == fname && c.LastName == lname);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Error! Customer not found.");
                }
                return customer;
            }
        }
        // This method gets a Store when provided with a store id.
        static Store GetStore(int sid)
        {
            using (var context = new RGProject0Context())
            {
                Store store = null;
                try
                { 
                    store = context.Store.First(s => s.Id == sid);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Error! Store not found.");
                }
                return store;
            }
        }
        // This method prints all products in a given store.
        static void PrintStoreProducts(Store store)
        {
            using (var context = new RGProject0Context())
            {
                Console.WriteLine("Here's a list of this store's products:");
                var productsInStore = from sto in context.Set<Store>().Where(sto => sto.Id == store.Id)
                                      from inv in context.Set<Inventory>().Where(inv => inv.StoreId == sto.Id)
                                      from pro in context.Set<Product>().Where(pro => pro.Id == inv.ProductId)
                                      select new { sto, inv, pro };
                foreach (var prod in productsInStore.ToList())
                {
                    Console.WriteLine("Product ID: " + prod.pro.Id + ", Name: " + prod.pro.ProductName + ", In Stock: " + prod.inv.Stock);
                }
            }
        }
        // This method creates a shopping cart where the key is the product id and the value is the quantity.
        static Dictionary<int, int> CreateShoppingCart(Store store)
        {
            using (var context = new RGProject0Context())
            {

                var cart = new Dictionary<int, int>();
                do
                {
                    Inventory inventory = null;
                    do
                    {
                        try
                        {
                            int productID = GetInputInt("Input the Product ID you want to add to your cart: ");
                            int quantity = GetInputInt("How many do you want to purchase?: ");
                            inventory = context.Inventory.First(i => i.ProductId == productID && i.StoreId == store.Id);
                            if (inventory.Stock < quantity || (cart.ContainsKey(productID) && inventory.Stock < (cart[productID] + quantity)))
                            {
                                Console.WriteLine("Error! Not enough of these products in stock.");
                            }
                            else
                            {
                                if (cart.ContainsKey(productID))
                                {
                                    cart[productID] += quantity;
                                }
                                else
                                {
                                    cart.Add(productID, quantity);
                                    Console.WriteLine("Added " + quantity + " of Product " + productID + " to your cart.");
                                }
                            }
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine("Error! Product not found.");
                        }
                    }
                    while (inventory == null);
                }
                while (GetInputString("Would you like to add another product to your cart? (Y/N): ").ToUpper() == "Y");
                return cart;
            }
        }
        // This method places an order when given a store, customer, and cart of product ids and quantities.
        static void PlaceOrder(Store store, Customer customer, Dictionary<int, int> cart)
        {
            using (var context = new RGProject0Context())
            {
                var order = new OrderHistory() { StoreId = store.Id, CustomerId = customer.Id };
                decimal total = 0;
                foreach (var item in cart)
                {
                    order.OrderReceipt.Add(new OrderReceipt() { OrderHistoryId = order.Id, ProductId = item.Key, Quantity = item.Value });
                    var inventory = context.Inventory.First(i => i.StoreId == store.Id && i.ProductId == item.Key);
                    inventory.Stock -= item.Value;
                    total += context.Product.First(p => p.Id == item.Key).Price * item.Value;
                }
                order.Total = total;
                context.OrderHistory.Add(order);
                context.SaveChanges();
                Console.WriteLine("Your order has been placed. Your total is: $" + total);
            }
        }
        // This method adds a customer to the DB when given a first name, last name, and card number.
        static void AddCustomer(string fname, string lname, int cnum)
        {
            using (var context = new RGProject0Context())
            {
                Customer newCust = new Customer() { FirstName = fname, LastName = lname, CardNumber = cnum };
                context.Customer.Add(newCust);
                context.SaveChanges();
                Console.WriteLine("Added customer " + fname + " " + lname);
            }
        }
        // This method prints all the order history items of a given store.
        static void StoreOrderHistory(Store store)
        {
            using (var context = new RGProject0Context())
            {
                var orderItems = from sto in context.Set<Store>().Where(sto => sto.Id == store.Id)
                                 from orh in context.Set<OrderHistory>().Where(orh => orh.StoreId == sto.Id)
                                 from orr in context.Set<OrderReceipt>().Where(orr => orr.OrderHistoryId == orh.Id)
                                 from pro in context.Set<Product>().Where(pro => pro.Id == orr.ProductId)
                                 select new { sto, orh, orr, pro };
                Console.WriteLine("Here are all the order history items from Store ID: " + store.Id + ", Store Name: " + store.Name);
                foreach (var oItem in orderItems)
                {
                    Console.WriteLine("Order ID: " + oItem.orh.Id + ", Customer ID: " + oItem.orh.CustomerId + ", Total: $" + oItem.orh.Total +
                                      ", Product ID: " + oItem.pro.Id + ", Product Name: " + oItem.pro.ProductName + ", Price: $" +
                                      oItem.pro.Price + ", Quantity: " + oItem.orr.Quantity);
                }
            }
        }
        // This method prints all the order history items of a given customer.
        static void CustomerOrderHistory(Customer customer)
        {
            using (var context = new RGProject0Context())
            {
                var orderItems = from cus in context.Set<Customer>().Where(cus => cus.Id == customer.Id)
                                 from orh in context.Set<OrderHistory>().Where(orh => orh.CustomerId == cus.Id)
                                 from orr in context.Set<OrderReceipt>().Where(orr => orr.OrderHistoryId == orh.Id)
                                 from pro in context.Set<Product>().Where(pro => pro.Id == orr.ProductId)
                                 select new { cus, orh, orr, pro };
                Console.WriteLine("Here are all the order history items from Customer ID: " + customer.Id +
                                  ", Name: " + customer.FirstName + " " + customer.LastName);
                foreach (var oItem in orderItems)
                {
                    Console.WriteLine("Order ID: " + oItem.orh.Id + ", Store ID: " + oItem.orh.StoreId + ", Total: $" + oItem.orh.Total +
                                      ", Product ID: " + oItem.pro.Id + ", Product Name: " + oItem.pro.ProductName + ", Price: $" +
                                      oItem.pro.Price + ", Quantity: " + oItem.orr.Quantity);
                }
            }
        }
        // This method prints all the customers and their values.
        static void ShowAllCustomers()
        {
            using (var context = new RGProject0Context())
            {
                var results = context.Customer;
                foreach (var item in results)
                {
                    Console.WriteLine("Customer ID: " + item.Id + ", First Name: " + item.FirstName + ", Last Name: " +
                                      item.LastName + ", Card Number: " + item.CardNumber);
                }
            }
        }
        // This method prints all the stores and their locations.
        static void ShowAllStoreLocations()
        {
            using (var context = new RGProject0Context())
            {
                var results = from sto in context.Set<Store>()
                              from loc in context.Set<Location>().Where(loc => loc.Id == sto.LocationId)
                              select new { sto, loc };
                foreach (var item in results)
                {
                    Console.WriteLine("Store ID: " + item.sto.Id + ", Name: " + item.sto.Name + ", Location ID: " +
                                      item.sto.LocationId + ", State: " + item.loc.State + ", City: " + item.loc.City);
                }
            }
        }
        // This method prints all the order history items.
        static void ShowAllOrderHistoryItems()
        {
            using (var context = new RGProject0Context())
            {
                var results = context.OrderHistory;
                foreach (var item in results)
                {
                    Console.WriteLine("Order ID: " + item.Id + ", Store ID: " + item.StoreId + ", Customer ID: " +
                        item.CustomerId + " Order Date: " + item.OrderDate + ", Total: $" + item.Total);
                }
            }
        }
    }
}