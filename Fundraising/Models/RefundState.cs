using System;
using System.Collections.Generic;

#nullable disable

namespace Fundraising.Models
{
    public partial class RefundState
    {
        public RefundState()
        {
            Refunds = new HashSet<Refund>();
        }

        public int RefundStateId { get; set; }
        public string State { get; set; }

        public virtual ICollection<Refund> Refunds { get; set; }
    }
}
