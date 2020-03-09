using System;
using System.Collections.Generic;

namespace StoreApplication
{
    class Customer
    {  
        static List<Customer> allCustomers = new List<Customer>();
        string name;
        string customerID;
        string email;
        List<Product> shoppingCart;

        public Customer(string n, string e)
        {
            name = n;
            email = e;
            Random rand = new Random();
            customerID = "C" + rand.Next(10000, 100000);
            shoppingCart = new List<Product>();
        }
        public void addToCart(Product product, int amount)
        {
            Product cartItem = new Product(product.ProductName, amount, product.Price);
        }
        public void PlaceOrder(Store store)
        {
            if(shoppingCart.Count == 0)
            {
                Console.WriteLine("Your shopping cart is empty.");
            }
            else
            {
                Console.WriteLine("Here is your receipt.");
                foreach (var item in shoppingCart)
                {
                    Console.WriteLine(item.ToStringBought());
                }
                Console.WriteLine("Your total is: ");
            }
        }
    }
}
