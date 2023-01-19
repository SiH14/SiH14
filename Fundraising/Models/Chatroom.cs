using System;
using System.Collections.Generic;

#nullable disable

namespace Fundraising.Models
{
    public partial class Chatroom
    {
        public Chatroom()
        {
            Messages = new HashSet<Message>();
        }

        public int ChatroomId { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }

        public virtual User UserId1Navigation { get; set; }
        public virtual User UserId2Navigation { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
