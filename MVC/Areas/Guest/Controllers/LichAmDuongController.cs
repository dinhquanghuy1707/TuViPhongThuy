using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Areas.Guest.Controllers
{
    public class LichAmDuongController : Controller
    {
        //
        // GET: /Guest/LichAmDuong/
        TuViPhongThuyContext db = new TuViPhongThuyContext();
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult DoiLich(string Date)
        {
            string dtt = Date;
            DateTime dt = Convert.ToDateTime(dtt);
            AmLich.LunarDate lnd = new AmLich.LunarDate();
            lnd = AmLich.LunarYearTools.SolarToLunar(dt);
            while (lnd.Year > 2009) lnd.Year = lnd.Year - 60;
            while (lnd.Year < 1950) lnd.Year = lnd.Year + 60;
            int d = lnd.Year;
            var yeartext = from x in db.NguHanhs where x.namDL == d select x;
            string year =yeartext.Single().namAL;
            string data = "Là ngày "+lnd.Day+" tháng "+lnd.Month+" năm "+ year + " âm lịch !";



            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Student(string ngay, string kieu)
        {
            string data = "";
            if (kieu == "DA")
            {
                string dtt = ngay;
                DateTime dt = Convert.ToDateTime(dtt);
                AmLich.LunarDate lnd = new AmLich.LunarDate();
                lnd = AmLich.LunarYearTools.SolarToLunar(dt);
                int nam = lnd.Year;
                while (lnd.Year > 2009) lnd.Year = lnd.Year - 60;
                while (lnd.Year < 1950) lnd.Year = lnd.Year + 60;
                int d = lnd.Year;
                var yeartext = from x in db.NguHanhs where x.namDL == d select x;
                string year = yeartext.Single().namAL;
                data = "Là ngày " + lnd.Day + " tháng " + lnd.Month + " năm " +nam +" (" + year +")";
            }
            else
            {
                bool leap;
                string dtt = ngay;
                DateTime dt = Convert.ToDateTime(dtt);
                if ((dt.Year % 400 == 0) || (dt.Year % 4 == 0 && dt.Year % 100 != 0))
                    leap = true;
                else
                    leap = false;

                AmLich.LunarDate lnd = new AmLich.LunarDate(dt.Day, dt.Month, dt.Year, leap);
                DateTime duonglich = AmLich.LunarYearTools.LunarToSolar(lnd);
               // AmLich.LunarDate lnd = new AmLich.LunarDate();
               // lnd = AmLich.LunarDate.(dt);
               // data = "Là ngày " + lnd.Day + " tháng " + lnd.Month + " năm " + lnd.Year;
                data = "Là ngày " + duonglich.Day + " tháng " + duonglich.Month + " năm " + duonglich.Year +" (Dương Lịch)";
            }
           
            return Content(data);
        }
    }
}
