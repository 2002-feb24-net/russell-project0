using StoreApp.Data;
using StoreApp.Data.Model;
using System;
using Xunit;

namespace XUnitTestStoreApp
{
    public class UnitTest1
    {
        QueryLibrary que = new QueryLibrary();

        [Fact]
        public void TestContext()
        {
            using (var context = new RGProject0Context())
            {
                Assert.NotNull(context.Inventory);
                Assert.NotNull(context.Store);
                Assert.NotNull(context.Customer);
                Assert.NotNull(context.Location);
                Assert.NotNull(context.Product);
                Assert.NotNull(context.OrderHistory);
                Assert.NotNull(context.OrderReceipt);
            }
        }
        [Fact]
        public void TestGetCustomerByID()
        {
            int id = 1;
            Customer cust = que.GetCustomer(id);
            Assert.Equal(id, cust.Id);
        }
        [Fact]
        public void TestGetNullCustomerByID()
        {
            int id = -1;
            Customer cust = que.GetCustomer(id);
            Assert.Null(cust);
        }
        [Fact]
        public void TestGetCustomerByName()
        {
            string fname = "Russell";
            string lname = "Gehan";
            Customer cust = que.GetCustomer(fname, lname);
            Assert.Equal(fname, cust.FirstName);
            Assert.Equal(lname, cust.LastName);
        }
        [Fact]
        public void TestGetNullCustomerByName()
        {
            string fname = "Whatzitooya";
            string lname = "Punk";
            Customer cust = que.GetCustomer(fname, lname);
            Assert.Null(cust);
        }
        [Fact]
        public void TestGetStoreByID()
        {
            int id = 1;
            Store store = que.GetStore(id);
            Assert.True(id == store.Id);
        }
        [Fact]
        public void TestGetNullStoreByID()
        {
            int id = -1;
            Store store = que.GetStore(id);
            Assert.Null(store);
        }
        [Fact]
        public void TestGetAllStoreLocations()
        {
            var sl = que.GetAllStoreLocations();
            Assert.NotNull(sl);
        }
        [Fact]
        public void TestGetAllCustomers()
        {
            var cust = que.GetAllCustomers();
            Assert.NotNull(cust);
        }
        [Fact]
        public void TestGetAllOrders()
        {
            var orders = que.GetAllOrderHistoryItems();
            Assert.NotNull(orders);
        }
    }
}
