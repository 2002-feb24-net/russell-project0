using System;
using System.Collections.Generic;

namespace StoreApp.Data.Model
{
    public partial class Location
    {
        public Location()
        {
            Store = new HashSet<Store>();
        }

        public int Id { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public virtual ICollection<Store> Store { get; set; }
    }
}
