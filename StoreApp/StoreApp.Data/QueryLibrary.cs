using StoreApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.Data
{
    public class QueryLibrary
    {
        // This method gets a Customer when provided with an ID.
        public Customer GetCustomer(int id)
        {
            using (var context = new RGProject0Context())
            {
                Customer customer = null;
                customer = context.Customer.FirstOrDefault(c => c.Id == id);
                return customer;
            }
        }
        // This method gets a Customer when provided with a first and last name.
        public Customer GetCustomer(string fname, string lname)
        {
            using (var context = new RGProject0Context())
            {
                Customer customer = null;
                customer = context.Customer.FirstOrDefault(c => c.FirstName == fname && c.LastName == lname);
                return customer;
            }
        }
        // This method gets a Store when provided with a store id.
        public Store GetStore(int sid)
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
        public void PrintStoreProducts(Store store)
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
                    Console.WriteLine("Product ID: " + prod.pro.Id + ", Name: " + prod.pro.ProductName + ", Price: $" +
                                      prod.pro.Price + ", In Stock: " + prod.inv.Stock);
                }
            }
        }
        // This method creates a shopping cart where the key is the product id and the value is the quantity.
        public Dictionary<int, int> CreateShoppingCart(Store store)
        {
            var getter = new InputGetter();
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
                            int productID = getter.GetInputInt("Input the Product ID you want to add to your cart: ");
                            int quantity = getter.GetInputInt("How many do you want to purchase?: ");
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
                while (getter.GetInputString("Would you like to add another product to your cart? (Y/N): ").ToUpper() == "Y");
                return cart;
            }
        }
        // This method places an order when given a store, customer, and cart of product ids and quantities.
        public void PlaceOrder(Store store, Customer customer, Dictionary<int, int> cart)
        {
            if (cart.Count < 1)
            {
                Console.WriteLine("You don't have anything in your cart...");
                return;
            }

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
        public void AddCustomer(string fname, string lname, int cnum)
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
        public void StoreOrderHistory(Store store)
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
        public void CustomerOrderHistory(Customer customer)
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
        public List<Customer> GetAllCustomers()
        {
            using (var context = new RGProject0Context())
            {
                var results = context.Customer.ToList();
                return results;
            }
        }
        // This method prints all the stores and their locations.
        public List<String> GetAllStoreLocations()
        {
            using (var context = new RGProject0Context())
            {
                var results = from sto in context.Set<Store>()
                              from loc in context.Set<Location>().Where(loc => loc.Id == sto.LocationId)
                              select new { sto, loc };
                var storeAndLocations = new List<String>();
                foreach (var item in results)
                {
                    storeAndLocations.Add("Store ID: " + item.sto.Id + ", Name: " + item.sto.Name + ", Location ID: " +
                                      item.sto.LocationId + ", State: " + item.loc.State + ", City: " + item.loc.City);
                }
                return storeAndLocations;
            }
        }
        // This method prints all the order history items.
        public List<OrderHistory> GetAllOrderHistoryItems()
        {
            using (var context = new RGProject0Context())
            {
                var results = context.OrderHistory.ToList();
                return results;
            }
        }
    }
}
