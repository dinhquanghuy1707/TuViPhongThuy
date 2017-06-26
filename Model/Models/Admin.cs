using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Admin
    {
        public string ID { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
