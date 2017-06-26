using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Areas.Guest.Controllers;
using System.Dynamic;
using System.Collections;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace MVC.Areas.Guest.Controllers
{
    public class person
    {
        public int namSinhA;
        public string gioiTinhA;
        public string nguHanhA;
        public string tuTrachA;
        public int namSinhB;
        public string amLichB;
        public string gioiTinhB;
        public string nguHanhB;
        public string tuTrachB;
        public string nguHanhHop;
        public string tuTrachHop;
        public string chenhLechHop;
        public string chenhLech;
        public double diemNguHanh;
        public double diemTuTrach;
        public double diemTuoi;
        public double diemTong;
    }

    public class TinhDuyenController : Controller
    {
        //
        // GET: /Guest/TinhDuyen/
        TuViPhongThuyContext db = new TuViPhongThuyContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult forward()
        {
            return View();
        }
        public ActionResult submit()
        {

            double Point = 0.0;
            string gender1 = Request.QueryString["gender1"];
            string gender2 = Request.QueryString["gender2"];

            string dtt1 = Request.QueryString["bday1"].ToString();
            string dtt2 = Request.QueryString["bday2"].ToString();

            // MD5 encode
            string code = gender1 + gender2 + dtt1 + dtt2;
            string hashCode = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(code);

            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);

            foreach (byte b in mang)
            {
                hashCode += b.ToString("x2");//Nếu là "X2" thì kết quả sẽ tự chuyển sang ký tự in Hoa
            }
            ///



            DateTime date1 = Convert.ToDateTime(dtt1);
            DateTime date2 = Convert.ToDateTime(dtt2);

            AmLich.LunarDate lnd1, lnd2 = new AmLich.LunarDate();

            lnd2 = AmLich.LunarYearTools.SolarToLunar(date2);
            lnd1 = AmLich.LunarYearTools.SolarToLunar(date1);

            ViewBag.day1 = lnd1.Day;
            ViewBag.day2 = lnd2.Day;
            ViewBag.month1 = lnd1.Month;
            ViewBag.month2 = lnd2.Month;
            /// object 1
            ViewBag.yearAL1 = lnd1.Year;
            int namSinhA = lnd1.Year;
            // Đổi âm dương
            while (lnd1.Year > 2009) lnd1.Year = lnd1.Year - 60;
            while (lnd1.Year < 1950) lnd1.Year = lnd1.Year + 60;
            ViewBag.day1 = lnd1.Day;
            ViewBag.month1 = lnd1.Month;
            //
            ViewBag.gender1 = gender1;
            ViewBag.gender2 = gender2;
            // Lấy năm âm , bát quái ngủ hành.
            var yeartext1 = from x in db.NguHanhs
                            where x.namDL == lnd1.Year
                            select x;


            //string nguHanh="";
            //string tuTrach="";


            if (gender1 == "Nam")
            {
                foreach (var x in yeartext1)
                {
                    //namSinhGuest = x.namDL;

                    ViewBag.namAL1 = x.namAL;
                    ViewBag.Menh1 = x.NguHanhNamSinh;
                    ViewBag.GiaiNghia1 = x.GiaiNghia;
                    ViewBag.CungMenh1 = x.CungNam;
                    ViewBag.NienMenh1 = x.NienMenhNam;
                    ViewBag.NguHanh1 = x.TenNguHanh;

                    //nguHanh = x.TenNguHanh;
                }
            }
            else
            {
                foreach (var x in yeartext1)
                {
                    //namSinhGuest = x.namDL;
                    ViewBag.namAL1 = x.namAL;
                    ViewBag.Menh1 = x.NguHanhNamSinh;
                    ViewBag.GiaiNghia1 = x.GiaiNghia;
                    ViewBag.CungMenh1 = x.CungNu;
                    ViewBag.NienMenh1 = x.NienMenhNu;
                    ViewBag.NguHanh1 = x.TenNguHanh;
                    //nguHanh = x.TenNguHanh;
                }
            }
            string cungMenh1 = ViewBag.CungMenh1;
            var MBQ = from y in db.BatQuai_TuTrach where y.Cung == cungMenh1 select y;
            foreach (var y in MBQ)
            {
                ViewBag.TuTrach1 = y.TuTrach;

                //tuTrach = y.TuTrach;
            }
            // object2
            lnd2 = AmLich.LunarYearTools.SolarToLunar(date2);
            ViewBag.yearAL2 = lnd2.Year;
            // Đổi âm dương
            while (lnd2.Year > 2009) lnd2.Year = lnd2.Year - 60;
            while (lnd2.Year < 1950) lnd2.Year = lnd2.Year + 60;
            ViewBag.day2 = lnd2.Day;
            ViewBag.month2 = lnd2.Month;
            // Lấy năm âm , bát quái ngủ hành.
            var yeartext2 = from x in db.NguHanhs where x.namDL == lnd2.Year select x;

            if (gender2 == "Nam")
            {
                foreach (var x in yeartext2)
                {
                    ViewBag.namAL2 = x.namAL;
                    ViewBag.Menh2 = x.NguHanhNamSinh;
                    ViewBag.GiaiNghia2 = x.GiaiNghia;
                    ViewBag.CungMenh2 = x.CungNam;
                    ViewBag.NienMenh2 = x.NienMenhNam;
                    ViewBag.NguHanh2 = x.TenNguHanh;
                }
            }
            else
            {
                foreach (var x in yeartext2)
                {
                    ViewBag.namAL2 = x.namAL;
                    ViewBag.Menh2 = x.NguHanhNamSinh;
                    ViewBag.GiaiNghia2 = x.GiaiNghia;
                    ViewBag.CungMenh2 = x.CungNu;
                    ViewBag.NienMenh2 = x.NienMenhNu;
                    ViewBag.NguHanh2 = x.TenNguHanh;
                }
            }
            string cungMenh2 = ViewBag.CungMenh2;
            var MBQ2 = from y in db.BatQuai_TuTrach where y.Cung == cungMenh2 select y;
            foreach (var y in MBQ2)
            {
                ViewBag.TuTrach2 = y.TuTrach;
            }
            ////////////////////////
            // Điểm ngũ hành
            double diemNguHanh = 0.0;

            if (ViewBag.NguHanh1 == ViewBag.NguHanh2)
            {
                Point += 3;
                diemNguHanh += 3;
            }
            else
            {
                if (ViewBag.NguHanh1 == "Kim")
                {
                    if (ViewBag.NguHanh2 == "Thủy" || ViewBag.NguHanh2 == "Thổ")
                    {
                        Point += 5;
                        diemNguHanh += 5;
                    }
                    else
                    {
                        if (ViewBag.NguHanh2 == "Mộc" || ViewBag.NguHanh2 == "Hỏa")
                        {
                            Point += 0;
                            diemNguHanh += 0;
                        }
                        else
                        {
                            Point += 2;
                            diemNguHanh += 2;
                        }
                    }
                }
                //
                if (ViewBag.NguHanh1 == "Mộc")
                {
                    if (ViewBag.NguHanh2 == "Thủy" || ViewBag.NguHanh2 == "Hỏa")
                    {
                        Point += 5;
                        diemNguHanh += 5;
                    }
                    else
                    {
                        if (ViewBag.NguHanh2 == "Kim" || ViewBag.NguHanh2 == "Thổ")
                        {
                            Point += 0;
                            diemNguHanh = 0;
                        }
                        else
                        {
                            Point += 2;
                            diemNguHanh += 2;
                        }
                    }
                }
                //
                if (ViewBag.NguHanh1 == "Thủy")
                {
                    if (ViewBag.NguHanh2 == "Mộc" || ViewBag.NguHanh2 == "Kim")
                    {
                        Point += 5;
                        diemNguHanh += 5;
                    }
                    else
                    {
                        if (ViewBag.NguHanh2 == "Hỏa" || ViewBag.NguHanh2 == "Thổ")
                        {
                            Point += 0;
                            diemNguHanh += 0;
                        }
                        else
                        {
                            Point += 2;
                            diemNguHanh += 2;
                        }
                    }

                }
                //
                if (ViewBag.NguHanh1 == "Hỏa")
                {
                    if (ViewBag.NguHanh2 == "Mộc" || ViewBag.NguHanh2 == "Thổ")
                    {
                        Point += 5;
                        diemNguHanh += 5;
                    }
                    else
                    {
                        if (ViewBag.NguHanh2 == "Thủy" || ViewBag.NguHanh2 == "Kim")
                        {
                            Point += 0;
                            diemNguHanh += 0;
                        }
                        else
                        {
                            Point += 2;
                            diemNguHanh += 0;
                        }
                    }

                }
                //
                if (ViewBag.NguHanh1 == "Thổ")
                {
                    if (ViewBag.NguHanh2 == "Hỏa" || ViewBag.NguHanh2 == "Kim")
                    {
                        Point += 5;
                        diemNguHanh += 5;
                    }
                    else
                    {
                        if (ViewBag.NguHanh2 == "Thủy" || ViewBag.NguHanh2 == "Mộc")
                        {
                            Point += 0;
                            diemNguHanh += 0;
                        }
                        else
                        {
                            Point += 2;
                            diemNguHanh += 2;
                        }
                    }

                }
            }
            ViewBag.DiemNguHanh = diemNguHanh.ToString();
            if (diemNguHanh == 5) ViewBag.NguHanh = "Hợp";
            else
            {
                if (diemNguHanh == 3 || diemNguHanh == 2) ViewBag.NguHanh = "Bình Thường";
                else ViewBag.NguHanh = "Khắc";
            }
            // ViewBag.Point = Point;
            // Tứ trạch
            if (ViewBag.TuTrach1 == ViewBag.TuTrach2)
            {
                Point += 3;
                ViewBag.TuTrach = "Hợp";
                ViewBag.DiemTuTrach = "3";
            }
            else
            {
                ViewBag.TuTrach = "Khắc";
                ViewBag.DiemTuTrach = "0";
            }
            // Chênh lệch tuổi
            int chenhlech = Math.Abs(ViewBag.yearAL1 - ViewBag.yearAL2);
            double diemTuoi = 0;
            ViewBag.ChenhLech = chenhlech.ToString();

            if (chenhlech % 4 == 0 || chenhlech == 0)
            {
                Point += 2;
                ViewBag.Tuoi = "Hợp";
                diemTuoi += 2;
            }
            else
            {
                if (chenhlech % 3 != 0)
                {
                    Point += 1;
                    ViewBag.Tuoi = "Bình Thường";
                    diemTuoi += 1;
                }
                else
                {
                    ViewBag.Tuoi = "Khắc";
                    diemTuoi += 0;
                }
            }

            if (chenhlech > 20)
            {
                Point -= 0.5;
                diemTuoi -= 0.5;
            }

            ViewBag.Point = Point;
            ViewBag.DiemTuoi = diemTuoi.ToString();
            if (Point >= 9)
                ViewBag.NhanXet = "HAI BẠN RẤT HỢP NHAU";
            else
            {
                if (Point >= 7)
                    ViewBag.NhanXet = "HAI BẠN KHÁ HỢP NHAU";
                else
                {
                    if (Point >= 5)
                        ViewBag.NhanXet = "HAI BẠN TƯƠNG ĐỐI HỢP NHAU";
                    else
                    {
                        if (Point >= 3)
                            ViewBag.NhanXet = "HAI BẠN KHÔNG HỢP NHAU LẮM";
                        else
                            ViewBag.NhanXet = "HAI BẠN RẤT KHẮC NHAU";
                    }
                }
            }
            string nameValues = "bday1="+dtt1+"&gender1="+gender1+"&bday2="+dtt2+"&gender2="+gender2;
            
            string url = "http://tuvigiadao.com/Guest/TinhDuyen/submit?" + nameValues;
            string description = "Tôi và người ấy có số điểm là : " + Point + " / 10 - Cùng xem Tử Vi - Bói Tình Yêu";
            ViewBag.url = url;
            ViewBag.description = description;

            ImageTinhDuyen(Point,hashCode,gender1);
            // Tìm những người hợp tuổi với người chơi, điều kiện kết quả >=6
            ViewBag.Image = hashCode + ".jpg";
            string nguHanhA = ViewBag.NguHanh1;
            string tuTrachA = ViewBag.TuTrach1;
            string gioiTinhB = "";

            List<person> list2 = new List<person>();
            if (gender1 == "Nam")
            {
                gioiTinhB = "Nữ";
                for (int i = namSinhA - 2; i <= namSinhA + 12; i++)
                {
                    person ketQua = ketquahaptuoi(namSinhA, nguHanhA, tuTrachA, i, gioiTinhB);
                    if (ketQua.diemTong >= 6)
                    {
                        list2.Add(ketQua);
                    }
                }
            }
            else
            {
                gioiTinhB = "Nam";
                for (int i = namSinhA - 12; i <= namSinhA + 2; i++)
                {
                    person ketQua = ketquahaptuoi(namSinhA, nguHanhA, tuTrachA, i, gioiTinhB);
                    if (ketQua.diemTong >= 6)
                    {
                        list2.Add(ketQua);
                    }
                }
            }

            list2 = list2.OrderByDescending(n => n.diemTong).ToList();


            



            return View(list2);
        }
        person ketquahaptuoi(int namSinhA, string nguHanhA, string tuTrachA, int namSinhB, string gioiTinhB)
        {
            person perSon1 = new person();
            perSon1.namSinhA = namSinhA;
            perSon1.namSinhB = namSinhB;
            perSon1.nguHanhA = nguHanhA;
            perSon1.tuTrachA = tuTrachA;
            double diem = 0.0;
            string nguHanhB = "";
            string cungMenhB = "";
            string tuTrachB = "";
            int chenhlech = Math.Abs(namSinhA - namSinhB);
            perSon1.chenhLech = "" + chenhlech;
            while (namSinhB > 2009) namSinhB = namSinhB - 60;
            while (namSinhB < 1950) namSinhB = namSinhB + 60;
            var yeartext1 = from x in db.NguHanhs
                            where x.namDL == namSinhB
                            select x;
            if (gioiTinhB == "Nam")
            {
                foreach (var x in yeartext1)
                {

                    nguHanhB = x.TenNguHanh;
                    cungMenhB = x.CungNam;
                    perSon1.amLichB = x.namAL;
                    perSon1.nguHanhB = x.TenNguHanh;
                }
            }
            else
            {
                foreach (var x in yeartext1)
                {
                    nguHanhB = x.TenNguHanh;
                    cungMenhB = x.CungNu;
                    perSon1.amLichB = x.namAL;
                    perSon1.nguHanhB = x.TenNguHanh;
                }
            }
            var MBQ = from y in db.BatQuai_TuTrach where y.Cung == cungMenhB select y;
            foreach (var y in MBQ)
            {
                tuTrachB = y.TuTrach;
                perSon1.tuTrachB = y.TuTrach;
            }
            perSon1.nguHanhB = "" + nguHanhB;
            if (nguHanhA == nguHanhB)
            {
                diem += 3;
                perSon1.diemNguHanh = 3;
            }
            else
            {
                if (nguHanhA == "Kim")
                {
                    if (nguHanhB == "Thủy" || nguHanhB == "Thổ")
                    {
                        diem += 5;
                        perSon1.diemNguHanh = 5;
                    }
                    else
                    {
                        if (nguHanhB == "Mộc" || nguHanhB == "Hỏa")
                        {
                            diem += 0;
                            perSon1.diemNguHanh = 0;
                        }
                        else
                        {
                            diem += 2;
                            perSon1.diemNguHanh = 2;
                        }
                    }
                }
                //
                if (nguHanhA == "Mộc")
                {
                    if (nguHanhB == "Thủy" || nguHanhB == "Hỏa")
                    {
                        diem += 5;
                        perSon1.diemNguHanh = 5;
                    }
                    else
                    {
                        if (nguHanhB == "Kim" || nguHanhB == "Thổ")
                        {
                            diem += 0;
                            perSon1.diemNguHanh = 0;
                        }
                        else
                        {
                            diem += 2;
                            perSon1.diemNguHanh = 2;
                        }
                    }
                }
                //
                if (nguHanhA == "Thủy")
                {
                    if (nguHanhB == "Mộc" || nguHanhB == "Kim")
                    {
                        diem += 5;
                        perSon1.diemNguHanh = 5;
                    }
                    else
                    {
                        if (nguHanhB == "Hỏa" || nguHanhB == "Thổ")
                        {
                            diem += 0;
                            perSon1.diemNguHanh = 0;
                        }
                        else
                        {
                            diem += 2;
                            perSon1.diemNguHanh = 2;
                        }
                    }

                }
                //
                if (nguHanhA == "Hỏa")
                {
                    if (nguHanhB == "Mộc" || nguHanhB == "Thổ")
                    {
                        diem += 5;
                        perSon1.diemNguHanh = 5;
                    }
                    else
                    {
                        if (nguHanhB == "Thủy" || nguHanhB == "Kim")
                        {
                            diem += 0;
                            perSon1.diemNguHanh = 0;
                        }
                        else
                        {
                            diem += 2;
                            perSon1.diemNguHanh = 2;
                        }
                    }

                }
                //
                if (nguHanhA == "Thổ")
                {
                    if (nguHanhB == "Hỏa" || nguHanhB == "Kim")
                    {
                        diem += 5;
                        perSon1.diemNguHanh = 5;
                    }
                    else
                    {
                        if (nguHanhB == "Thủy" || nguHanhB == "Mộc")
                        {
                            diem += 0;
                            perSon1.diemNguHanh = 0;
                        }
                        else
                        {
                            diem += 2;
                            perSon1.diemNguHanh = 2;
                        }
                    }

                }
            }
            if (perSon1.diemNguHanh == 5)
                perSon1.nguHanhHop = "Hợp";
            else
            {
                if (perSon1.diemNguHanh == 0)
                    perSon1.nguHanhHop = "Khắc";
                else
                    perSon1.nguHanhHop = "Bình";
            }

            // Tứ trạch
            if (tuTrachA == tuTrachB)
            {
                diem += 3;
                perSon1.diemTuTrach = 3;
                perSon1.tuTrachHop = "Hợp";
            }
            else
            {
                perSon1.diemTuTrach = 0;
                perSon1.tuTrachHop = "Khắc";
            }


            // Chênh lệch tuổi

            if (chenhlech == 0)
            {
                diem += 1;
                perSon1.diemTuoi = 1;
                perSon1.chenhLechHop = "Bình";
            }
            else
            {
                if (chenhlech % 4 == 0)
                {
                    diem += 2;
                    perSon1.diemTuoi = 2;
                    perSon1.chenhLechHop = "Hợp";
                }
                else
                {
                    if (chenhlech % 3 != 0)
                    {
                        diem += 1;
                        perSon1.diemTuoi = 2;
                        perSon1.chenhLechHop = "Hợp";
                    }
                    else
                    {
                        diem += 0;
                        perSon1.diemTuoi = 0;
                        perSon1.chenhLechHop = "Khắc";
                    }
                }
            }
            if (chenhlech > 20)
            {
                diem -= 0.5;
                perSon1.diemTuoi -= 0.5;
            }
            perSon1.diemTong = diem;
            return perSon1;

        }
        public void ImageTinhDuyen(double Point,string Url,string gender)
        {
            StringFormat stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Center;
         
            //string Str_Point = Convert.ToString(TempData["Point"]);
            string Str_Point = (Point*10)+"%";
            if (Point >= 9)
                ViewBag.NhanXet = "HAI BẠN RẤT HỢP NHAU";
            else
            {
                if (Point >= 7)
                    ViewBag.NhanXet = "HAI BẠN KHÁ HỢP NHAU";
                else
                {
                    if (Point >= 5)
                        ViewBag.NhanXet = "HAI BẠN TƯƠNG ĐỐI HỢP NHAU";
                    else
                    {
                        if (Point >= 3)
                            ViewBag.NhanXet = "HAI BẠN KHÔNG HỢP NHAU LẮM";
                        else
                            ViewBag.NhanXet = "HAI BẠN RẤT KHẮC NHAU";
                    }
                }
            }
            string SourceIMAGE = "TinhDuyen.png";
            if (gender != "Nam") SourceIMAGE = "TinhDuyen2.png";

            string Str_TextOnImage = ViewBag.NhanXet;
            Color StringColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            // Đường dẫn file ảnh. 
            string imageFile = Server.MapPath("~/Assets/Homepage/images/" + SourceIMAGE);
            // Tạo đối tượng Bitmap truyền vào đường dẫn File ảnh
            Bitmap myBitmap = new Bitmap(imageFile);
            // Tạo đối tượng Graphic từ Bitmap
            Graphics myGraphics = Graphics.FromImage(myBitmap);

            //// Vẽ lại hình ảnh, chèn nội dung mới vào.
            myGraphics.DrawString(Str_Point, new Font("arial", 65,
            FontStyle.Bold), new SolidBrush(StringColor), new Point(450, 125), stringformat);
            myGraphics.DrawString(Str_TextOnImage, new Font("arial", 32,
            FontStyle.Bold), new SolidBrush(StringColor), new Point(450, 375), stringformat);
            var bitmap = myBitmap; // The method that returns Bitmap
            var bitmapBytes = BitmapToBytes(bitmap); //Convert bitmap into a byte array

            bitmap.Save(Server.MapPath("~/Assets/Homepage/images/BoiTinhYeu/"+Url+".jpg"), ImageFormat.Jpeg);


           // return File(bitmapBytes, "image/jpeg"); //Return as file result
        }
        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
