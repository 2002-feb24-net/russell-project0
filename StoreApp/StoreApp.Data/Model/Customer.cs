using System;
using System.Collections.Generic;

namespace StoreApp.Data.Model
{
    public partial class Customer
    {
        public Customer()
        {
            OrderHistory = new HashSet<OrderHistory>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CardNumber { get; set; }

        public virtual ICollection<OrderHistory> OrderHistory { get; set; }
    }
}
