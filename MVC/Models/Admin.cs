using System;
using System.Collections.Generic;

namespace MVC.Models
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
