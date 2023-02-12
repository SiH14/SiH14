using System;
using System.Collections.Generic;

#nullable disable

namespace Fundraising.Models
{
    public partial class Comment
    {
        public Comment()
        {
            Answers = new HashSet<Answer>();
        }

        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public DateTime Commenttime { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
