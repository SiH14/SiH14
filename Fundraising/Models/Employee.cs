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
    }
}
