using StoreLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StoreApplication
{
    class Program
    {
        static async void Main(string[] args)
        {
            string customersPath = "../../../customers.json";
            string storesPath = "../../../stores.json";
            string ordersPath = "../../../orders.json";

            if (!File.Exists(customersPath) && !File.Exists(storesPath) && !File.Exists(ordersPath))
            {
                await FirstTimeSetup(customersPath, storesPath, ordersPath);
                Console.WriteLine("First time setup was a success!");
            }
            else
            {
                string customerJson = await ReadFromFileAsync(customersPath);
                string storesJson = await ReadFromFileAsync(storesPath);
                string ordersJson = await ReadFromFileAsync(ordersPath);

                var myStores = JsonSerializer.Deserialize<List<Store>>(storesJson);
                var myCustomers = JsonSerializer.Deserialize<List<Customer>>(customerJson);
                var myOrders = JsonSerializer.Deserialize<List<Order>>(ordersJson);
                // Get the customer's name, requiring a first and a last.
                string[] name;
                do
                {
                    name = getInputString("What's your full name? (Include: 'First Last'): ").Split(' ');
                } 
                while (name.Length < 2);

                var currentCust = new Customer(name[0], name[1], myCustomers[myCustomers.Count - 1].CustomerID + 1);

                Console.WriteLine("Here are our available locations.");
                for (int i = 0; i < myStores.Count; i++)
                {
                    Console.WriteLine(myStores[i].ToString());
                }

                int whichStore = getInputInt("What store would you like to visit? (Please input the store ID or " + myStores.Count + " to quit.)");
                if (whichStore != myStores.Count)
                {
                    Store myStore = myStores.Find(sid => sid.StoreID == whichStore);
                    Console.WriteLine("Welcome " + name + " to " + myStore.ToString() + "!");
                    bool checkout = false;
                    bool quit = false;

                    while (!checkout && !quit)
                    {
                        string[] storeProducts = myStore.AllProducts();
                        Console.WriteLine("Here's a list of the available products.");
                        for (int i = 0; i < storeProducts.Length; i++)
                        {
                            Console.WriteLine(storeProducts[i]);
                        }
                        int itemID = getInputInt("Input the product ID number you want to add to your cart, " +
                            storeProducts.Length + " to go to checkout, or -1 to quit.");
                        if (itemID >= 0)
                        {
                            int amount = getInputInt("How many would you like to purchase?: ");
                            myStore.SelectProduct(currentCust, itemID, amount);
                        }
                        else if (itemID == storeProducts.Length)
                        {
                            currentCust.PlaceOrder();
                        }
                        else
                        {
                            quit = true;
                        }
                    }
                }
            }
        }
        static string getInputString(string msg)
        {
            Console.WriteLine(msg);
            string input = Console.ReadLine();
            return input;
        }

        static int getInputInt(string msg)
        {
            bool inputValid = false;
            int inputInt = 0;
            while (!inputValid)
            {
                string strInput = getInputString(msg);
                try
                {
                    inputInt = int.Parse(strInput);
                    inputValid = true;
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Error! Please input a number.");
                }
            }
            return inputInt;
        }
        private async static Task<string> ReadFromFileAsync(string filePath)
        {
            using var sr = new StreamReader(filePath);
            Task<string> textTask = sr.ReadToEndAsync();
            string text = await textTask;
            return text;
        }
        private async static Task WriteToFileAsync(string text, string path)
        {
            FileStream file = null;
            try
            {
                file = new FileStream(path, FileMode.Create);
                byte[] data = Encoding.UTF8.GetBytes(text);

                await file.WriteAsync(data);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access to file {path} is not allowed by the OS:");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (file != null)
                {
                    file.Dispose();
                }
            }
        }
        private static async Task FirstTimeSetup(string cPath, string sPath, string oPath)
        {
            // The "Game Store" has four different locations.
            var myStores = new List<Store>();
            myStores.Add(new Store("Game Store 0", "Texas", 1000));
            myStores.Add(new Store("Game Store 1", "California", 1001));
            myStores.Add(new Store("Game Store 2", "New York", 1002));
            myStores.Add(new Store("Game Store 3", "Florida", 1003));

            // Stock each of the stores with the same products.
            foreach (var store in myStores)
            {
                // Each "Game Store" sells three different products.
                store.AddStock(new StoreProduct("TCG Booster Box", 2000, 500, 99.99));
                store.AddStock(new StoreProduct("TCG Booster Pack", 2001, 5000, 4.19));
                store.AddStock(new StoreProduct("Board Game", 2002, 200, 49.99));
            }

            var myCustomers = new List<Customer>();
            myCustomers.Add(new Customer("Nick", "Escalona", 3000));
            myCustomers.Add(new Customer("Mark", "Moore", 3001));
            myCustomers.Add(new Customer("Russell", "Gehan", 3002));

            var nicksCart = new ShoppingCart();
            nicksCart.AddToCart(new CartProduct("TCGBoosterBox", 2000, 2, 99.99));
            var marksCart = new ShoppingCart();
            marksCart.AddToCart(new CartProduct("TCG Booster Pack", 2001, 20, 4.19));
            var russellsCart = new ShoppingCart();
            russellsCart.AddToCart(new CartProduct("Board Game", 2002, 5, 49.99));
            russellsCart.AddToCart(new CartProduct("TCG Booster Box", 2000, 1, 99.99));
            russellsCart.AddToCart(new CartProduct("TCG Booster Pack", 2001, 10, 4.19));

            var myOrders = new List<Order>();
            myOrders.Add(new Order(myStores.Find(sid => sid.StoreID == 1000), myCustomers.Find(cid => cid.CustomerID == 3000), nicksCart));
            myOrders.Add(new Order(myStores.Find(sid => sid.StoreID == 1001), myCustomers.Find(cid => cid.CustomerID == 3001), marksCart));
            myOrders.Add(new Order(myStores.Find(sid => sid.StoreID == 1002), myCustomers.Find(cid => cid.CustomerID == 3002), russellsCart));

            string storeData = JsonSerializer.Serialize(myStores);
            string customerData = JsonSerializer.Serialize(myCustomers);
            string orderData = JsonSerializer.Serialize(myOrders);

            try
            {
                await WriteToFileAsync(storeData, sPath);
                await WriteToFileAsync(customerData, cPath);
                await WriteToFileAsync(orderData, oPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal error");
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
