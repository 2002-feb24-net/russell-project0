using System;
using System.Collections.Generic;

namespace StoreApplication
{
    class Store
    {
        static List<Store> listOfStores = new List<Store>();
        string storeName;
        string location;
        public string storeID { get; }
        List<Order> orderHistory;

        public Store(string sName, string loc)
        {
            storeName = sName;
            location = loc;
            Random rand = new Random();
            storeID = "S" + rand.Next(10000, 100000);
            listOfStores.Add(this);
        }
    }
}
