using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Areas.Guest.Controllers;
using Facebook;
using System.Dynamic;
using System.Threading;
using System.Security.Cryptography;

namespace MVC.Areas.Guest.Controllers
{
    public class XemPhongThuyController : Controller
    {
        TuViPhongThuyContext db = new TuViPhongThuyContext();
     
        //
        // GET: /Guest/XemPhongThuy/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Result()
        {
           
            DateTime dt = new DateTime(1995, 1, 1);
            AmLich.LunarDate lnd = new AmLich.LunarDate();
            lnd= AmLich.LunarYearTools.SolarToLunar(dt);
            int d = lnd.Year;
            ViewBag.day = lnd.Day;
            ViewBag.month = lnd.Month;
            ViewBag.year = lnd.Year;
        
            var yeartext = from x in db.NguHanhs where x.namDL == d select  x;

            foreach(var x in yeartext)
                {
                ViewBag.t = x.namAL;
                ViewBag.BQ = x.CungNam;
                ViewBag.NH = x.NienMenhNam;
                ViewBag.GN = x.GiaiNghia;
              

                 }
                
                return View();
        }
        public ActionResult submit()
        {

            // string gender = Request.Form["gender"];
            string gender = Request.QueryString["gender"];
            //string dtt = Request.Form["date"].ToString();
            string dtt = Request.QueryString["bday"].ToString();
            ViewBag.gender = gender;
            DateTime dt = Convert.ToDateTime(dtt);
            AmLich.LunarDate lnd = new AmLich.LunarDate();
            lnd = AmLich.LunarYearTools.SolarToLunar(dt);
            //
            int namsinh = lnd.Year;
        
         


            //
            while (lnd.Year > 2009) lnd.Year = lnd.Year - 60;
            while (lnd.Year < 1950) lnd.Year = lnd.Year + 60;
            int d = lnd.Year;
            string BQ;
            ViewBag.day = lnd.Day;
            ViewBag.month = lnd.Month;
            ViewBag.year = lnd.Year;
            string nameValues = "bday=" + dtt + "&gender=" + gender;
            ViewBag.url = "http://tuvigiadao.com/Guest/XemPhongThuy/submit?" + nameValues;

            
       
                var yeartext = from x in db.NguHanhs where x.namDL == d select x;
                String s = yeartext.Single().CungNam;
                ViewBag.dtt = dtt;           
                if (gender == "Nam")
                {
                    foreach (var x in yeartext)
                    {
                        ViewBag.t = x.namAL;
                        ViewBag.BQ = x.CungNam;
                        ViewBag.NH = x.TenNguHanh;
                        ViewBag.GN = x.GiaiNghia;
                        ViewBag.NgH = x.NguHanhNamSinh;
                    }
                }
                else
                {
                    foreach (var x in yeartext)
                    {

                        ViewBag.t = x.namAL;
                        ViewBag.BQ = x.CungNu;
                        ViewBag.NH = x.TenNguHanh;
                        ViewBag.GN = x.GiaiNghia;
                        ViewBag.NgH = x.NguHanhNamSinh;
                    }
                }
                BQ = ViewBag.BQ;
                var MBQ = from y in db.BatQuai_TuTrach where y.Cung == BQ select y;
                foreach (var y in MBQ)
                {
                    ViewBag.MBQuai = y.MaBatQuai;
                }
                //
                int MBQ1 = ViewBag.MBQuai;
                var CuuCungTot = from y in db.TinhTuTheoBatQuais where y.MaBatQuai == MBQ1 select y;

                foreach (var y in CuuCungTot)
                {
                    if (y.Huong == "B")
                        y.Huong = "Bắc";
                    if (y.Huong == "D")
                        y.Huong = "Đông";
                    if (y.Huong == "N")
                        y.Huong = "Nam";
                    if (y.Huong == "T")
                        y.Huong = "Tây";
                    if (y.Huong == "DB")
                        y.Huong = "Đông Bắc";
                    if (y.Huong == "DN")
                        y.Huong = "Đông Nam";
                    if (y.Huong == "TB")
                        y.Huong = "Tây Bắc";
                    if (y.Huong == "TN")
                        y.Huong = "Tây Nam";
                    //
                    if (y.MaTinhTu == 1)
                    {
                        ViewBag.T1 = y.Huong;
                    }
                    if (y.MaTinhTu == 2)
                        ViewBag.T2 = y.Huong;
                    if (y.MaTinhTu == 3)
                        ViewBag.T3 = y.Huong;
                    if (y.MaTinhTu == 4)
                        ViewBag.T4 = y.Huong;
                    if (y.MaTinhTu == 5)
                        ViewBag.T5 = y.Huong;
                    if (y.MaTinhTu == 6)
                        ViewBag.T6 = y.Huong;
                    if (y.MaTinhTu == 7)
                        ViewBag.T7 = y.Huong;
                    if (y.MaTinhTu == 8)
                        ViewBag.T8 = y.Huong;

                }
                var TinhTu = from y in db.TinhTus select y;
                foreach (var y in TinhTu)
                {
                    if (y.MaTinhTu == 1)
                    {
                        ViewBag.TTT1 = y.TenTinhTu;
                        ViewBag.TTGT1 = y.GiaiThich;
                    }
                    if (y.MaTinhTu == 2)
                    {
                        ViewBag.TTT2 = y.TenTinhTu;
                        ViewBag.TTGT2 = y.GiaiThich;
                    }
                    if (y.MaTinhTu == 3)
                    {
                        ViewBag.TTT3 = y.TenTinhTu;
                        ViewBag.TTGT3 = y.GiaiThich;
                    }
                    if (y.MaTinhTu == 4)
                    {
                        ViewBag.TTT4 = y.TenTinhTu;
                        ViewBag.TTGT4 = y.GiaiThich;
                    }
                    if (y.MaTinhTu == 5)
                    {
                        ViewBag.TTT5 = y.TenTinhTu;
                        ViewBag.TTGT5 = y.GiaiThich;
                    }
                    if (y.MaTinhTu == 6)
                    {
                        ViewBag.TTT6 = y.TenTinhTu;
                        ViewBag.TTGT6 = y.GiaiThich;
                    }
                    if (y.MaTinhTu == 7)
                    {
                        ViewBag.TTT7 = y.TenTinhTu;
                        ViewBag.TTGT7 = y.GiaiThich;
                    }
                    if (y.MaTinhTu == 8)
                    {
                        ViewBag.TTT8 = y.TenTinhTu;
                        ViewBag.TTGT8 = y.GiaiThich;
                    }
                }
                //


                //    string namsinh = Request["namsinh"];


                string description = "Tôi sinh ngày " + lnd.Day + " tháng " + lnd.Month + " năm " + ViewBag.t + " Âm lịch - Cùng Xem Tử Vi";
                ViewBag.description = description;


                TempData["message"] = description;

                TempData["link"] = "http://tuvigiadao.com/Guest/XemPhongThuy/Index";
                TempData["picture"] = "http://lichvansu.wap.vn/images/xem-tu-vi-tron-doi.jpg";
                TempData["name"] = "Xem Tử Vi Hướng Nhà";
                TempData["flag"] = 0;
                TempData["caption"] = "Click để xem hướng nhà phù hợp với tuổi của bạn..";




                //+ " Các hướng tốt là : \n" + " Hướng: " + t1 + " ứng với " + tt1 + "\n"
                //+ "Hướng: " + t2 + " ứng với " + tt2 + "\n"
                //+ "Hướng: " + t3 + " ứng với " + tt3 + "\n"
                //+ "Hướng: " + t4 + " ứng với " + tt4 + "\n" +
                //"Các hướng xấu là : \n" + "Hướng: " + t5 + "ứng với " + tt5 + "\n"
                //+ "Hướng: " + t6 + " ứng với " + tt6 + "\n"
                //+ "Hướng: " + t7 + " ứng với " + tt7 + "\n"
                //+ "Hướng: " + t8 + " ứng với " + tt8 + "\n"
                //;


                return View();

            
        }

 
        public ActionResult GetByFB()  //Lấy thông tin từ app do mình tạo ra trên fb
        {

            var fb = new FacebookClient();


            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "1567232153295149",
                client_secret = "eab1b4dd9956001f52bd54229aea61c1",    
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email, user_about_me, user_birthday, publish_actions,read_custom_friendlists"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code) //Lấy thông tin người dùng fb và login vào
        {
            string bd = "cc";
            var fb = new FacebookClient();
            string nameValues = "sss";
            dynamic result = fb.Post("oauth/access_token", new
            {

                client_id = "330468244019948",
                client_secret = "2f2a59b2ffe2cd338d81b608bf0ef923",
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
                dynamic me = fb.Get("me?fields=id,name,birthday,gender");
                string name = me.name;
                DateTime dt = new DateTime();
                bd = me.birthday;
                dt = Convert.ToDateTime(bd);

                string date = dt.ToString("yyyy-MM-dd");
               
                string gender = me.gender;
                if (gender.Equals("male")) gender = "Nam";
                else gender = "Nữ";
                nameValues = "";
                nameValues = "bday=" + date + "&gender=" + gender;
            }
      

        //    TempData["url"] = "http://tuvitrondoi.somee.com/Guest/XemPhongThuy/submit?" + nameValues;
///TempData["url2"] = "http://tuvigiadao.somee.com/Guest/XemPhongThuy/submit?" + nameValues;
           
            return Redirect("~/Guest/XemPhongThuy/submit?" + nameValues);
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

        public ActionResult Forward()
        {
            return View();
        }
    }
}
