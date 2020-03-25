using System;
using System.Collections.Generic;

namespace StoreApp.Data.Model
{
    public partial class Product
    {
        public Product()
        {
            Inventory = new HashSet<Inventory>();
            OrderReceipt = new HashSet<OrderReceipt>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<OrderReceipt> OrderReceipt { get; set; }
    }
}
