using System;
using System.Collections.Generic;

#nullable disable

namespace Fundraising.Models
{
    public partial class Plan
    {
        public Plan()
        {
            Orders = new HashSet<Order>();
        }

        public int PlanId { get; set; }
        public int ProductId { get; set; }
        public string PlanTitle { get; set; }
        public string PlanContent { get; set; }
        public int PlanPrice { get; set; }
        public string PlanPhoto { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
