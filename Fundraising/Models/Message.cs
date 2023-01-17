using System;
using System.Collections.Generic;

#nullable disable

namespace Fundraising.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string Contact { get; set; }
        public DateTime SentTime { get; set; }

        public virtual User FromUser { get; set; }
        public virtual User ToUser { get; set; }
    }
}
