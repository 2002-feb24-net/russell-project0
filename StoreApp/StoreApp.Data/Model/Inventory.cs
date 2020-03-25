using System;
using System.Collections.Generic;

namespace StoreApp.Data.Model
{
    public partial class Inventory
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
