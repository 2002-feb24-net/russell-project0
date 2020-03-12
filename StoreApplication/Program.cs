using System;
using System.Collections.Generic;

namespace StoreApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // The "Game Store" has four different locations.
            var myStores = new List<Store>();
            myStores.Add(new Store("Game Store 0", "Texas"));
            myStores.Add(new Store("Game Store 1", "California"));
            myStores.Add(new Store("Game Store 2", "New York"));
            myStores.Add(new Store("Game Store 3", "Florida"));

            // Stock each of the stores with the same products.
            foreach(var store in myStores)
            {
                // Each "Game Store" sells three different products.
                store.addStock(new Product("TCG Booster Box", 500, 99.99));
                store.addStock(new Product("TCG Booster Pack", 5000, 4.19));
                store.addStock(new Product("Board Game", 200, 49.99));
            }
            bool marketIsBooming = true;

            while(marketIsBooming)
            {
                string name = getInputString("What's your name?: ");
                var currentCust = new Customer(name);
                Console.WriteLine("Here are out available locations.");
                for (int i = 0; i < myStores.Count; i++)
                {
                    Console.WriteLine(myStores[i].ToString());
                }
                int whichStore = getInputInt("What store would you like to visit? Please use the number corresponding to the store, or " + myStores.Count + " to quit.");
                if(whichStore >= 0 && whichStore < myStores.Count)
                {
                    Store myStore = myStores[whichStore];
                    Console.WriteLine("Welcome " + name + " to " + myStore.ToString() + "!");
                    bool checkout = false;
                    bool quit = false;

                    while(!checkout && !quit)
                    {
                        string[] storeProducts = myStore.AllProducts();
                        Console.WriteLine("Here's a list of the available products.");
                        for (int i = 0; i < storeProducts.Length; i++)
                        {
                            Console.WriteLine(i + ". " + storeProducts[i]);
                        }
                        int doItem = getInputInt("Input the product number you want to add to your cart, " +
                            storeProducts.Length + " to go to checkout, or " + (storeProducts.Length+1) + " to quit.");
                        if(doItem >= 0 && doItem < storeProducts.Length)
                        {
                            int amount = getInputInt("How many would you like to purchase?: ");
                            currentCust.addToCart(myStore.SelectProduct(doItem), amount);
                        }
                        else if(doItem == storeProducts.Length)
                        {

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
            while(!inputValid)
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
    }
}
