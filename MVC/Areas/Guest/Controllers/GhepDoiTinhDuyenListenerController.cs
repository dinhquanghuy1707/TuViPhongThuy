using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using System.Dynamic;
using MVC.Models;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Globalization;

namespace MVC.Areas.Guest.Controllers
{
    public class GhepDoiTinhDuyenListenerController : Controller
    {
        TuViPhongThuyContext db = new TuViPhongThuyContext();
        //
        // GET: /Guest/GhepDoiTinhDuyenListener/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult submit1(string IdPost)
        {


            //TempData["birthday"] = birthday;
            //TempData["gender"] = gender;
            //TempData["fid"] = id;
            TempData["idpost"] = IdPost;
     

            //
            var fb = new FacebookClient();


            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "1567232153295149",
                client_secret = "eab1b4dd9956001f52bd54229aea61c1",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email, user_about_me, user_birthday,read_custom_friendlists"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult submit2(string IdPost)
        {
            TuViPhongThuyContext db = new TuViPhongThuyContext();
            string gender = "female";

            var PostRow = db.GhepDoiTinhDuyenPosts.First(a => a.IdPost == IdPost);


            // Convert datetime in SQL to Datetime in these
            string dt = PostRow.Birthday.ToString();
            DateTime dtt = Convert.ToDateTime(dt);
            string birthday = dtt.ToString("yyyy-MM-dd");
            //DateTime dt = DateTime.ParseExact(PostRow.Birthday.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            if (PostRow.Gender.Value)
                gender = "male";
            string IdFB = PostRow.IdFB;
            string Image = PostRow.Image;
            string urlButton = "Guest/GhepDoiTinhDuyenListener/submit1?IdPost="+IdPost;        
            ViewBag.Image = Image;
            ViewBag.Gender = gender;
            ViewBag.IdPost = IdPost;
            ViewBag.Birthday = birthday;
            ViewBag.Url = urlButton;
            return View();
        }
        public ActionResult FacebookCallback(string code) //Lấy thông tin người dùng fb và login vào
        {
            //  string bd = "cc";
            string IdPost2 = "";
            var fb = new FacebookClient();
            // string nameValues = "sss";
            dynamic result = fb.Post("oauth/access_token", new
            {

                client_id = "1567232153295149",
                client_secret = "eab1b4dd9956001f52bd54229aea61c1",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                //dynamic parameters = new ExpandoObject();
                //parameters.message = "Check out this funny article";
                //parameters.link = "http://www.natiska.com/article.html";
                //parameters.picture = "http://www.natiska.com/dav.png";
                //parameters.name = "Article Title";
                //parameters.privacy = new { value = "EVERYONE" };
                //parameters.caption = "Caption for the link";
                //fb.Post("/me/feed", parameters


                //);
                //string objID = TempData["fid"].ToString();
                dynamic me = fb.Get("me?fields=id,name,birthday,gender,picture.height(200).width(200)");
                //dynamic obj = fb.Get(objID + "?fields=name");
               // string nameobj = obj.name;
    
                TempData["tentoi"] = me.name;
           //     TempData["tennguoiay"] = nameobj;              
                ///
                //  string myID = me.id;
                //
                //string myPic = "https://graph.facebook.com/" + me.id + "/picture?height=200";
                //string objPic = "https://graph.facebook.com/" + objID + "/picture?height=200";
                TempData["myPic"] = me.id;
              //  TempData["objPic"] = obj.id;
                dynamic idPost = TempData["idpost"];
                string idTEMP = me.id + idPost;
                // MD5 encode
                byte[] mang = System.Text.Encoding.UTF8.GetBytes(idTEMP);
                MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
                mang = my_md5.ComputeHash(mang);

                foreach (byte b in mang)
                {
                    IdPost2 += b.ToString("x2");//Nếu là "X2" thì kết quả sẽ tự chuyển sang ký tự in Hoa
                }
                //


                string IdResult = IdPost2;
                string dob = me.birthday;
                //
                DateTime dt1 = DateTime.ParseExact(dob, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //
                string gender = me.gender;
                string idfb = me.id;
                string imageURL = IdResult;
                string name = me.name;
                dynamic idPostTruoc =  TempData["idpost"];

                DateTime date1 = DateTime.Parse(dob);
                string db1 = dt1.ToString("yyyy-MM-dd");
                var Res = new GhepDoiTinhDuyenResult();
                Res = db.GhepDoiTinhDuyenResults.Where(s => s.IdResult == IdResult).FirstOrDefault();

                if (Res != null)
                {
                    Res.IdResult = IdResult;
                    Res.IdPost = idPostTruoc;
                    Res.IdFb = idfb;
                    Res.Name = name;
                    Res.Birthday = db1;
                    if (gender.Equals("male", StringComparison.Ordinal))
                    {
                        // 1 true là nam
                        Res.Gender = true;
                    }
                    else
                    {
                        // 0 false là nữ
                        Res.Gender = false;
                    }
                    Res.Image = imageURL;
                    // Mark entity as modified
                    db.Entry(Res).State = System.Data.Entity.EntityState.Modified;
                    //  call SaveChanges
                    db.SaveChanges();
                }
                else
                {
                    Res = new GhepDoiTinhDuyenResult();
                    Res.IdResult = IdResult;
                    Res.IdPost = idPostTruoc;
                    Res.IdFb = idfb;
                    Res.Name = name;
                    Res.Birthday = db1;
                    if (gender.Equals("male", StringComparison.Ordinal))
                    {
                        // 1 true là nam
                        Res.Gender = true;
                    }
                    else
                    {
                        // 0 false là nữ
                        Res.Gender = false;
                    }
                    Res.Image = imageURL;
                    Res.Name = name;
                    db.GhepDoiTinhDuyenResults.Add(Res);
                    db.SaveChanges();
                }
            }
            return Redirect("/Guest/GhepDoiTinhDuyenListener/result?IdRes=" + IdPost2);
        }
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public ActionResult result(string IdRes)
        {
            var Res = new GhepDoiTinhDuyenResult();
            var Pos = new GhepDoiTinhDuyenPost();
            //
            Res = db.GhepDoiTinhDuyenResults.Where(s => s.IdResult == IdRes).FirstOrDefault();
            string nameobj1 = Res.Name;
            string dtt1 = Res.Birthday;
            string gender1 = "Nữ";
            if (Res.Gender == true) gender1 = "Nam";
            string idfb1 = Res.IdFb;
            string image1 = Res.Image;
            string IdPost = Res.IdPost;
            //


            Pos = db.GhepDoiTinhDuyenPosts.Where(s => s.IdPost == IdPost).FirstOrDefault();
            string nameobj2 = Pos.Name;
            string dtt2 = Pos.Birthday;
            string gender2 = "Nữ";
            if (Pos.Gender == true) gender2 = "Nam";
            string idfb2 = Pos.IdFB;
            string image2 = Pos.Image;
            double Point = 0.0;
            ViewBag.IdRes = IdRes;
            ViewBag.url= "http://tuvigiadao.com/Guest/GhepDoiTinhDuyenListener/result?IdRes="+IdRes;

            //string objID = TempData["fid"].ToString();
            ViewBag.tentoi = nameobj1;
            ViewBag.tennguoiay = nameobj2;
            //ViewBag.anhtoi = TempData["myPic"];
            //ViewBag.anhnguoiay = TempData["objPic"];
            ///////////////////////////////
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
            ViewBag.decription = nameobj2 + " và " + nameobj1 + "hợp nhau " + Point + "/10 - còn bạn thì sao ? - Xem Bói Tính Duyên";
          


            //



            // Tìm những người hợp tuổi với người chơi, điều kiện kết quả >=6
            string nguHanhA = ViewBag.NguHanh1;
            string tuTrachA = ViewBag.TuTrach1;
            string gioiTinhB = "";

            List<person> list2 = new List<person>();
            if (gender1 == "Nam")
            {
                gioiTinhB = "Nữ";
                for (int i = namSinhA - 12; i <= namSinhA + 2; i++)
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
                for (int i = namSinhA - 2; i <= namSinhA + 12; i++)
                {
                    person ketQua = ketquahaptuoi(namSinhA, nguHanhA, tuTrachA, i, gioiTinhB);
                    if (ketQua.diemTong >= 6)
                    {
                        list2.Add(ketQua);
                    }
                }
            }

            list2 = list2.OrderByDescending(n => n.diemTong).ToList();


          ViewBag.Image=  Image(IdRes, idfb1, idfb2, Point, nameobj1, nameobj2);



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
        public string Image(string PostID,string id1,string id2,double point,string name1,string name2)
        {
            string caption = "";
          //  string Point = point*10+"%";
            string imageURL = PostID;
            // Id fb người đăng
            string fid1 = id1;
            // Id fb người click
            string fid2 = id2;
            //
            if (point >= 9)
                caption = "HAI BẠN RẤT HỢP NHAU";
            else
            {
                if (point >= 7)
                    caption = "HAI BẠN KHÁ HỢP NHAU";
                else
                {
                    if (point >= 5)
                        caption = "HAI BẠN TƯƠNG ĐỐI HỢP NHAU";
                    else
                    {
                        if (point >= 3)
                            caption = "HAI BẠN KHÔNG HỢP NHAU LẮM";
                        else
                            caption = "HAI BẠN RẤT KHẮC NHAU";
                    }
                }
            }
            //
            WebClient wc = new WebClient();
            byte[] originalData = wc.DownloadData("https://graph.facebook.com/" + id1 + "/picture?height=200&type=square");
            MemoryStream stream = new MemoryStream(originalData);
            Bitmap myBitmapLogo = new Bitmap(stream);

            byte[] originalData2 = wc.DownloadData("https://graph.facebook.com/" + id2 + "/picture?height=200&type=square");
            MemoryStream stream2 = new MemoryStream(originalData2);
            Bitmap myBitmapLogo2 = new Bitmap(stream2);

            StringFormat stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Center;
            string Str_TextOnImage = caption;
            //string Str_Point = Convert.ToString(TempData["Point"]);
            string Str_Point = point*10+"%";

            Color StringColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            // Đường dẫn file ảnh. 
            string imageFile = Server.MapPath("~/Assets/Homepage/images/TinhDuyenGhepDoi.png");
            // Tạo đối tượng Bitmap truyền vào đường dẫn File ảnh
            Bitmap myBitmap = new Bitmap(imageFile);
            // Tạo đối tượng Graphic từ Bitmap
            Graphics myGraphics = Graphics.FromImage(myBitmap);

            //// Vẽ lại hình ảnh, chèn nội dung mới vào.
            //Bitmap myBitmapLogo = new Bitmap(logo);

            myGraphics.DrawImage(myBitmapLogo, new Point(90, 95));
            myGraphics.DrawImage(myBitmapLogo2, new Point(610, 95));
            myGraphics.DrawString(Str_Point, new Font("arial", 65,
            FontStyle.Bold), new SolidBrush(StringColor), new Point(450, 125), stringformat);
            myGraphics.DrawString(Str_TextOnImage, new Font("arial", 32,
            FontStyle.Bold), new SolidBrush(StringColor), new Point(450, 375), stringformat);
            var bitmap = myBitmap; // The method that returns Bitmap
            var bitmapBytes = BitmapToBytes(bitmap); //Convert bitmap into a byte array
            bitmap.Save(Server.MapPath("~/Assets/Homepage/images/GhepDoiTinhDuyenPost/" + PostID + ".jpg"), ImageFormat.Jpeg);


           return PostID + ".jpg";
        }
        public ActionResult test()
        {
            string IdResult = "x";
            string dob = "1995-11-01";
            string gender = "male";         
            string idfb = "x";
            string imageURL = "x";
            string name = "x";
            string idPost = "x";
            DateTime dt1 = DateTime.Parse(dob);
            string db1 = dt1.ToString("yyyy-MM-dd");

            var result = new GhepDoiTinhDuyenResult();
            result = db.GhepDoiTinhDuyenResults.Where(s => s.IdResult == IdResult).FirstOrDefault();

            if (result != null)
            {
                result.IdResult = IdResult;
                result.IdPost = idPost;
                result.IdFb = idfb;
                result.Name = name;
                result.Birthday = db1;
                if (gender.Equals("male", StringComparison.Ordinal))
                {
                    // 1 true là nam
                    result.Gender = true;
                }
                else
                {
                    // 0 false là nữ
                    result.Gender = false;
                }
                result.Image = imageURL;
                // Mark entity as modified
                db.Entry(result).State = System.Data.Entity.EntityState.Modified;
                //  call SaveChanges
                db.SaveChanges();
            }
            else
            {
                result = new GhepDoiTinhDuyenResult();
                result.IdResult = IdResult;
                result.IdPost = idPost;
                result.IdFb = idfb;
                result.Name = name;
                result.Birthday = db1;
                if (gender.Equals("male", StringComparison.Ordinal))
                {
                    // 1 true là nam
                    result.Gender = true;
                }
                else
                {
                    // 0 false là nữ
                    result.Gender = false;
                }
                result.Image = imageURL;
                result.Name = name;
                db.GhepDoiTinhDuyenResults.Add(result);
                db.SaveChanges();
            }


            ViewBag.dt1 = db1;
            


            return View();
        }
        // This method is for converting bitmap into a byte array
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