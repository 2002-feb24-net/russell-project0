using System;
using System.Collections.Generic;

namespace StoreApp.Data.Model
{
    public partial class OrderHistory
    {
        public OrderHistory()
        {
            OrderReceipt = new HashSet<OrderReceipt>();
        }

        public int Id { get; set; }
        public int StoreId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Total { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<OrderReceipt> OrderReceipt { get; set; }
    }
}
