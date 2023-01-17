using System;
using System.Collections.Generic;

#nullable disable

namespace Fundraising.Models
{
    public partial class User
    {
        public User()
        {
            Answers = new HashSet<Answer>();
            Comments = new HashSet<Comment>();
            Followings = new HashSet<Following>();
            MessageFromUsers = new HashSet<Message>();
            MessageToUsers = new HashSet<Message>();
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public DateTime UserBirthday { get; set; }
        public bool? UserGender { get; set; }
        public string UserIntro { get; set; }
        public string UserFblink { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserPhoto { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Following> Followings { get; set; }
        public virtual ICollection<Message> MessageFromUsers { get; set; }
        public virtual ICollection<Message> MessageToUsers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
