using System;
using System.Collections.Generic;

#nullable disable

namespace Fundraising.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int ChatroomId { get; set; }
        public string MessageContent { get; set; }
        public DateTime SentTime { get; set; }
        public bool IsRead { get; set; }

        public virtual Chatroom Chatroom { get; set; }
        public virtual User Receiver { get; set; }
        public virtual User Sender { get; set; }
    }
}
