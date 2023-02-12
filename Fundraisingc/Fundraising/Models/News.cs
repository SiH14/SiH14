using System;
using System.Collections.Generic;

#nullable disable

namespace Fundraising.Models
{
    public partial class News
    {
        public int NewsId { get; set; }
        public int ProductId { get; set; }
        public string NewsTitle { get; set; }
        public string NewsContent { get; set; }
        public DateTime NewsDate { get; set; }

        public virtual Product Product { get; set; }
    }
}
