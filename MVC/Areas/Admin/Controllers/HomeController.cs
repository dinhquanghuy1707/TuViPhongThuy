using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/

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

    }
}
