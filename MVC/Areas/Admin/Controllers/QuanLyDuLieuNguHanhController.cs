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
    public class QuanLyDuLieuNguHanhController : Controller
    {
        TuViPhongThuyContext db = new TuViPhongThuyContext();
        //
        // GET: /Admin/QuanLyDuLieuNguHanh/

        public ActionResult Index(int? page)
        {
            if (Session["email"] != null)
            {
                //phân trang
                var dulieuNguHanh = db.NguHanhs.ToList();
                int pageSize = 10;
                int pageNumber = (page ?? 1);


                return View(dulieuNguHanh.ToPagedList(pageNumber, pageSize));

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

         
        }
        public ActionResult Insert()
        {
            return View();
        }
        public ActionResult InsertData()
        {
            //xử lý sửa dữ liệu
            try
            {
                int namDL = int.Parse(Request.Form["namDL"]);
                string namAmLich = Request.Form["namAmLich"].ToString();
                string giaiNghia = Request.Form["giaiNghia"].ToString();
                string cungNam = Request.Form["cungNam"].ToString();
                string menhNam = Request.Form["menhNam"].ToString();
                string cungNu = Request.Form["cungNu"].ToString();
                string menhNu = Request.Form["menhNu"].ToString();
                string nguHanhNamSinh = Request.Form["nguHanhNamSinh"].ToString();
                string tenNguHanh = Request.Form["tenNguHanh"].ToString();

                NguHanh nguHanh = new NguHanh();
                nguHanh.namDL = namDL;
                nguHanh.namAL = namAmLich;
                nguHanh.GiaiNghia = giaiNghia;
                nguHanh.CungNam = cungNam;
                nguHanh.NienMenhNam = menhNam;
                nguHanh.CungNu = cungNu;
                nguHanh.NienMenhNu = menhNu;
                nguHanh.NguHanhNamSinh = nguHanhNamSinh;
                nguHanh.TenNguHanh = tenNguHanh;
                db.NguHanhs.Add(nguHanh);
                db.SaveChanges();
                // return RedirectToAction("Index");
            }
            catch (Exception)
            {
            }
            return Redirect("~/Admin/QuanLyDuLieuNguHanh/Index");
            //return View();

        }
        public ActionResult Update()
        {
            return View();
        }
        public ActionResult UpdateData()
        {
            //xử lý sửa dữ liệu
            try
            {
                //String command = "";
                //command = Request.QueryString["Command"];
                //int nam = int.Parse(Request.QueryString["Nam"]);

                int namDL = int.Parse(Request.Form["namDL"]);
                string namAmLich = Request.Form["namAmLich"].ToString();
                string giaiNghia = Request.Form["giaiNghia"].ToString();
                string cungNam = Request.Form["cungNam"].ToString();
                string menhNam = Request.Form["menhNam"].ToString();
                string cungNu = Request.Form["cungNu"].ToString();
                string menhNu = Request.Form["menhNu"].ToString();
                string nguHanhNamSinh = Request.Form["nguHanhNamSinh"].ToString();
                string tenNguHanh = Request.Form["tenNguHanh"].ToString();

                var objs = from table in db.NguHanhs
                           where table.namDL == namDL
                           select table;
                foreach (var item in objs)
                {
                    if (namAmLich == "")
                        namAmLich = item.namAL;
                    if (giaiNghia == "")
                        giaiNghia = item.GiaiNghia;
                    if (cungNam == "")
                        cungNam = item.CungNam;
                    if (menhNam == "")
                        menhNam = item.NienMenhNam;
                    if (cungNu == "")
                        cungNu = item.CungNu;
                    if (menhNu == "")
                        menhNu = item.NienMenhNu;
                    if (nguHanhNamSinh == "")
                        nguHanhNamSinh = item.NguHanhNamSinh;
                    if (tenNguHanh == "")
                        tenNguHanh = item.TenNguHanh;
                }

                foreach (var item in objs)
                {
                    item.namAL = namAmLich;
                    item.GiaiNghia = giaiNghia;
                    item.CungNam = cungNam;
                    item.NienMenhNam = menhNam;
                    item.CungNu = cungNu;
                    item.NienMenhNu = menhNu;
                    item.NguHanhNamSinh = nguHanhNamSinh;
                    item.TenNguHanh = tenNguHanh;
                }
                db.SaveChanges();


            }

            catch (Exception)
            {
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int namDL)
        {
            try
            {
                var nguHanh = db.NguHanhs.Find(namDL);
                db.NguHanhs.Remove(nguHanh);
                db.SaveChanges();

            }
            catch (Exception)
            {

            }
            return RedirectToAction("Index");
        }



    }
}
