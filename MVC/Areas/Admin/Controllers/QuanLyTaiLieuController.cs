using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MVC.Models;
using System.Data.Linq;
namespace MVC.Areas.Admin.Controllers
{
    public class QuanLyTaiLieuController : Controller
    {
        
        TuViPhongThuyContext db = new TuViPhongThuyContext();
        //
        // GET: /Admin/QuanLyTaiLieu/

        public ActionResult Index(int? page)
        {
            if (Session["email"] != null)
            {
                //phân trang
                var taiLieus = db.TaiLieux.ToList();
                int pageSize = 10;
                int pageNumber = (page ?? 1);

                return View(taiLieus.ToPagedList(pageNumber, pageSize));

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
      
        }
        public ActionResult InsertData()
        {
            try
            {
                //int maTaiLieu = int.Parse(Request.QueryString["maTaiLieu"]);
                int maTaiLieu = int.Parse(Request.Form["maTaiLieu"].ToString());
                string chuDe = Request.Form["chuDe"].ToString();
                string tieuDe = Request.Form["tieuDe"].ToString();
                string noiDung = Request.Form["noiDung"].ToString();
                string linkAnh = Request.Form["linkAnh"].ToString();
                DateTime ngayDang = DateTime.Now;

                TaiLieu taiLieu = new TaiLieu();
                taiLieu.MaTaiLieu = maTaiLieu;
                taiLieu.ChuDe = chuDe;
                taiLieu.TieuDe = tieuDe;
                taiLieu.NoiDung = noiDung;
                taiLieu.LinkAnh = linkAnh;
                taiLieu.NgayDang = ngayDang;
                taiLieu.TenNguoiDang = "HuyAdmin";
                db.TaiLieux.Add(taiLieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
            }
           return RedirectToAction("Index");
        }
        public ActionResult Insert()
        {
            return View();
        }
        public ActionResult Delete(int maTaiLieu)
        {
            try
            {
                var taiLieu = db.TaiLieux.Find(maTaiLieu);
                db.TaiLieux.Remove(taiLieu);
                db.SaveChanges();

            }
            catch (Exception)
            {

            }
            return RedirectToAction("Index");
        }
    }
}
