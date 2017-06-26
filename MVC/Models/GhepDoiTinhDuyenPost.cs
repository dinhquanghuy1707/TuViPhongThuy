using System;
using System.Collections.Generic;

namespace MVC.Models
{
    public partial class GhepDoiTinhDuyenPost
    {
        public string IdPost { get; set; }
        public string IdFB { get; set; }
        public string Birthday { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
    }
}
