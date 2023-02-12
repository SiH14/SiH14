using System;
using System.Collections.Generic;

#nullable disable

namespace Fundraising.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Status { get; set; }
        public int Position { get; set; }
        public bool? Sexy { get; set; }
        public string Employeephoto { get; set; }

        public virtual EmployeePosition PositionNavigation { get; set; }
        public virtual EmployeeStatus StatusNavigation { get; set; }
    }
}
