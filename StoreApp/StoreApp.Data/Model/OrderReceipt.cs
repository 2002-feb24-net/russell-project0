using System;
using System.Collections.Generic;

namespace StoreApp.Data.Model
{
    public partial class OrderReceipt
    {
        public int OrderHistoryId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual OrderHistory OrderHistory { get; set; }
        public virtual Product Product { get; set; }
    }
}
