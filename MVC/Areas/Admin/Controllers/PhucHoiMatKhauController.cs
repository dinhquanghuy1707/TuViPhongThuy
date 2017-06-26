using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using System.Text;

namespace MVC.Areas.Admin.Controllers
{
    public class PhucHoiMatKhauController : Controller
    {
        //
        // GET: /Admin/PhucHoiMatKhau/
        TuViPhongThuyContext db = new TuViPhongThuyContext();
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult submit(string email,string sdt)
        {
            String reponse = "x";
            var Account = from y in db.Admins where y.Email == email select y;
            var existAccount = db.Admins.ToList().Any(p => p.Email == email && p.Phone == sdt);
            if (existAccount)
            {
                Session["email2"] = email;
                StringBuilder sb = new StringBuilder();
                char c;
                string c1;
                Random rd = new Random();
                for (int i =0; i<4;i++)
                {
                    c = Convert.ToChar(Convert.ToInt32(rd.Next(65, 77)));
                    sb.Append(c);
                }
                c1 = sb.ToString();
   
                Session["maxacnhan"] = c1;
                SpeedSMSAPI sms = new SpeedSMSAPI();
                String userinfo = sms.getUserInfo();
                reponse = sms.sendSMS(sdt, "Ma xac thuc phuc hoi mat khau cua ban la: " + c1, 2, " ");
            }
         
            return View();
        }
        public ActionResult submitPHMK(string maxacnhan,string mkm,string mkm2)
        {

            string email = Session["email2"].ToString();
            string maxacnhandagui = Session["maxacnhan"].ToString();
            if (maxacnhan == maxacnhandagui)
            {
                if (mkm == mkm2)
                {
                    
                    var v = db.Admins.SingleOrDefault(p => p.Email == email );
                    string mkec= md5Process.md5(mkm);
                    v.Password = mkec;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                  return RedirectToAction("Index", "PhucHoiMatKhau");

                }
            }
            return RedirectToAction("Index2", "PhucHoiMatKhau");
        }
    }
}
