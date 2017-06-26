using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
namespace MVC.Areas.Admin.Controllers
{
    public class DoiMatKhauController : Controller
    {
        //
        // GET: /Admin/DoiMatKhau/
        TuViPhongThuyContext db = new TuViPhongThuyContext();
        public ActionResult Index()
        {
            if (Session["email"] != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
        public ActionResult submit(string mkc,string mkm,string mkm2)
        {
          
            string email = Session["email"].ToString();
            var Account = from y in db.Admins where y.Email == email select y;


            var existAccount = db.Admins.ToList().Any(p => p.Email == email && p.Password == md5Process.md5(mkc));

            if (existAccount)
            {
              
                if (mkm == mkm2)
                {
                    var v = db.Admins.SingleOrDefault(p => p.Email == email);
                    v.Password = md5Process.md5(mkm).Trim().ToString();
                    db.SaveChanges();
                    Session.Remove("email");
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    return RedirectToAction("Index", "DoiMatKhau");
                }
                //var role = db.Admins.ToList().Single(
                //p => p.Email == email && p.Password == md5Process.md5(password));
            



           


            }
            else
            {

                return RedirectToAction("Index", "DoiMatKhau");
            }
            
        }
    }
}
