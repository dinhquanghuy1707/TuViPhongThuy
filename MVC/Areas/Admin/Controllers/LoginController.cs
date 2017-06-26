using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
namespace MVC.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Admin/Login/
        TuViPhongThuyContext db = new TuViPhongThuyContext();
        public ActionResult Index()
        {

            ViewBag.Teacher = db.Admins;
            if (Session["email"] != null )
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult login(string email,string password)
        {

            var Account = from y in db.Admins where y.Email == email select y;
              
         
            var existAccount = db.Admins.ToList().Any(p => p.Email == email && p.Password ==md5Process.md5(password));

            if (existAccount)
            {


                //var role = db.Admins.ToList().Single(
                //p => p.Email == email && p.Password == md5Process.md5(password));
                   Session["email"] = email;

                return RedirectToAction("Index", "Home");


            }
            else
            {
            
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult logout()
        {
            string email = Session["email"] as string;
         
            Session.Remove("email");
  
            return RedirectToAction("Index", "Home");
        }
    }
     
}
