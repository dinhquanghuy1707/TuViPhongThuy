using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace MVC.Areas.Guest.Controllers
{
    
    public class TaiLieuController : Controller
    {
        TuViPhongThuyContext db = new TuViPhongThuyContext();
        //
        // GET: /Guest/TaiLieu/

        public ActionResult Index(int? page)
        {
             var taiLieus = db.TaiLieux.ToList();
            
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(taiLieus.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Submit()
        {
            int maTaiLieu = int.Parse(Request.QueryString["btnSubmit"]);
            var tailieu = from x in db.TaiLieux
                          where maTaiLieu == x.MaTaiLieu
                          select x;
            foreach (var x in tailieu)
            {
                ViewBag.Link = x.LinkAnh;
                ViewBag.TenChuDe = x.ChuDe;
                ViewBag.NoiDung = x.NoiDung;

            }
            return View();

        }
        public ActionResult Search(int? page)
        {
            String str = Request.QueryString["Search"];
            int pageSize = 3;
            int pageNumber = (page ?? 1);
           
            var taiLieus = (from x in db.TaiLieux
                           where x.ChuDe.Contains(str)
                           select x).ToList();


            return View(taiLieus.ToPagedList(pageNumber, pageSize));
        }
    }
}
