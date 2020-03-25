using System;
using System.Collections.Generic;

namespace StoreLibrary
{
    public class Customer
    {  
        string firstName;
        string lastName;
        int customerID;
        ShoppingCart myCart;

        public int CustomerID { get { return customerID; } }

        public Customer(string firstName, string lastName, int customerID)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.customerID = customerID;
        }

        public void AddToCart(CartProduct product)
        {
            myCart.AddToCart(product);
        }

        public void PlaceOrder(Store store)
        {
            if(myCart.Count == 0)
            {
                Console.WriteLine("Your shopping cart is empty.");
            }
            else
            {
                var placedOrded = new Order(store, this, myCart);
                Console.WriteLine("Here is your receipt.");
                placedOrded.PrintReceipt();
            }
        }
        public override String ToString()
        {
            return "Name: " + firstName + " " + lastName + ", ID: " + customerID;
        }
        public void EmptyCart(Store store)
        {
            store.PlaceItemsBackOnShelf(myCart);
            myCart = new ShoppingCart();
        }
    }
}
