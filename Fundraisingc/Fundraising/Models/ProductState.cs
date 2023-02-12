using System;
using System.Collections.Generic;

#nullable disable

namespace Fundraising.Models
{
    public partial class ProductState
    {
        public ProductState()
        {
            Products = new HashSet<Product>();
        }

        public int ProductStateId { get; set; }
        public string State { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
