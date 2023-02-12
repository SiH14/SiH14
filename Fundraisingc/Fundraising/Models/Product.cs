using System;
using System.Collections.Generic;

#nullable disable

namespace Fundraising.Models
{
    public partial class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            Followings = new HashSet<Following>();
            News = new HashSet<News>();
            Plans = new HashSet<Plan>();
            Questions = new HashSet<Question>();
        }

        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductContent { get; set; }
        public int TargetAmount { get; set; }
        public DateTime Startime { get; set; }
        public DateTime Endtime { get; set; }
        public string ProductVedio { get; set; }
        public int ProductStateId { get; set; }
        public int UserId { get; set; }
        public string PrincipalId { get; set; }
        public string PrincipalName { get; set; }
        public string PrincipalPhone { get; set; }
        public string PrincipalEmail { get; set; }
        public string PrincipalBankAccount { get; set; }
        public bool? Featured { get; set; }
        public string Coverphoto { get; set; }

        public virtual ProductState ProductState { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Following> Followings { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
