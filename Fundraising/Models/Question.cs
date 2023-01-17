using System;
using System.Collections.Generic;

#nullable disable

namespace Fundraising.Models
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public int ProductId { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionContent { get; set; }

        public virtual Product Product { get; set; }
    }
}
