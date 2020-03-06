using System;
using System.Collections.Generic;

namespace StoreApplication
{
    class Order
    {  
        static List<Order> orderHistory;
        Dictionary<Product, int> orderedProducts;
        Customer myCustomer;
        Store myStore;
        string orderID;

        public Order(Store sto, Customer cust, Dictionary<Product, int> prods)
        {
            myStore = sto;
            myCustomer = cust;
            orderedProducts = prods;
            Random rand = new Random();
            orderHistory.Add(this);
        }

        public double GetTotal()
        {
            double total = 0;
            foreach(var item in orderedProducts)
            {
                total += item.Key.price * item.Value;
            }
            return total;
        }

        public List<Order> GetHistory(Store store)
        {
            var history = new List<Order>();
            foreach (var item in orderHistory)
            {
                if(store.storeID == item.myStore.storeID)
                {
                    history.Add(item);
                }
            }
            return history;
        }

        public List<Order> GetHistory(Customer customer)
        {
            var history = new List<Order>();
            foreach (var item in orderHistory)
            {
                if(customer.customerID == item.myCustomer.customerID)
                {
                    history.Add(item);
                }
            }
            return history;
        }
    }
}
