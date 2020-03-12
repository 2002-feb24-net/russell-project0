using System;
using System.Collections.Generic;

namespace StoreApplication
{
    class Customer
    {  
        static List<Customer> allCustomers = new List<Customer>();
        string name;
        string customerID;
        double balance = 10000;
        List<Product> shoppingCart;

        public Customer(string name)
        {
            this.name = name;
            Random rand = new Random();
            customerID = "C" + rand.Next(10000, 100000);
            shoppingCart = new List<Product>();
        }
        public void addToCart(Product product, int amount)
        {
            if(product.Quantity < amount)
            {
                Console.WriteLine("Error! Amount exceeds stock. Unable to add to cart.");
            }
            else if((TotalInCart() + product.Price * amount) > balance)
            {
                Console.WriteLine("You can't afford that!");
            }
            else
            {
                Product cartItem = new Product(product.ProductName, amount, product.Price);
            }
        }
        public void PlaceOrder(Store store)
        {
            if(shoppingCart.Count == 0)
            {
                Console.WriteLine("Your shopping cart is empty.");
            }
            else
            {
                var placedOrded = new Order(store, this, shoppingCart);
                Console.WriteLine("Here is your receipt.");
                foreach (var item in shoppingCart)
                {
                    Console.WriteLine(item.ToStringReceipt());
                }
                Console.WriteLine("Your total is: ");
            }
        }
        public override String ToString()
        {
            return "Name: " + name + ", Email: " + email + ", ID: " + customerID;
        }
        public double TotalInCart()
        {
            double total = 0;
            foreach (var item in shoppingCart)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }
        public void EmptyCart()
        {
            shoppingCart = new List<Product>();
        }
    }
}
