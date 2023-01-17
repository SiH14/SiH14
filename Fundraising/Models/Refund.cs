using System;
using System.Collections.Generic;

#nullable disable

namespace Fundraising.Models
{
    public partial class Refund
    {
        public int RefundId { get; set; }
        public int OrderId { get; set; }
        public int RefundStateId { get; set; }
        public string RefundResult { get; set; }

        public virtual Order Order { get; set; }
        public virtual RefundState RefundState { get; set; }
    }
}
