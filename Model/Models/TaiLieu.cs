using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class TaiLieu
    {
        public int MaTaiLieu { get; set; }
        public string ChuDe { get; set; }
        public string NoiDung { get; set; }
        public Nullable<System.DateTime> NgayDang { get; set; }
        public string TenNguoiDang { get; set; }
    }
}
