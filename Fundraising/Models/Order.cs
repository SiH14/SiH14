using System;
using System.Collections.Generic;

#nullable disable

namespace Fundraising.Models
{
    public partial class Order
    {
        public Order()
        {
            Refunds = new HashSet<Refund>();
        }

        public int OrderId { get; set; }
        public int PlanId { get; set; }
        public string OrderDateId { get; set; }
        public int UserId { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientAddress { get; set; }
        public string Note { get; set; }
        public string MasterCardId { get; set; }
        public int OrderStateId { get; set; }
        public DateTime PurchaseTime { get; set; }
        public int AddSponsorship { get; set; }
        public string PostCode { get; set; }
        public string RecipientMail { get; set; }

        public virtual OrderState OrderState { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Refund> Refunds { get; set; }
    }
}
