using System;
using System.Collections.Generic;

#nullable disable

namespace Fundraising.Models
{
    public partial class Answer
    {
        public int AnswerId { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string AnswerContent { get; set; }
        public DateTime AnswerTime { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual User User { get; set; }
    }
}
