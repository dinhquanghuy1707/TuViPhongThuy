using Facebook;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Areas.Guest.Controllers
{
    public class fbController : Controller
    {
        public string namsinh;
        //
        // GET: /Guest/fb/
   
        public ActionResult Index()
        {
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
                scope = "email, user_about_me, user_birthday, publish_actions",
            });
             return Redirect(loginUrl.AbsoluteUri);
           
        }
        public ActionResult sharept() //Lấy thông tin người dùng fb và login vào
        {

            string namsinh = Request["namsinh"];
            string gioitinh = Request["gioitinh"];
            string nguhanh = Request["nguhanh"];
            string giainghia = Request["giainghia"];
            string cungmenh = Request["cungmenh"];
            string t1 = Request.Form["t1"];
            string tt1 = Request.Form["tt1"];
            string t2 = Request.Form["t2"];
            string tt2 = Request.Form["tt2"];
            string t3 = Request.Form["t3"];
            string tt3 = Request.Form["tt3"];
            string t4 = Request.Form["t4"];
            string tt4 = Request.Form["tt4"];
            string t5 = Request.Form["t5"];
            string tt5 = Request.Form["tt5"];
            string t6 = Request.Form["t6"];
            string tt6 = Request.Form["tt6"];
            string t7 = Request.Form["t7"];
            string tt7 = Request.Form["tt7"];
            string t8 = Request.Form["t8"];
            string tt8 = Request.Form["tt8"];
            string message = "Bạn sinh :" + namsinh + "\n"
                + "Giới tính: " + gioitinh + "\n" + "Ngũ Hành: " + nguhanh + "- nghĩa là " + giainghia + "\n" + "Cung mệnh: " +
                cungmenh + "\n" + " Các hướng tốt là : \n"+" Hướng: "+ t1 +" ứng với " +tt1 +"\n"
                + "Hướng: " + t2 + " ứng với " + tt2 + "\n"
                + "Hướng: " + t3 + " ứng với " + tt3 + "\n"
                + "Hướng: " + t4 + " ứng với " + tt4 + "\n" +
                "Các hướng xấu là : \n" + "Hướng: " + t5 + "ứng với " + tt5 + "\n"
                + "Hướng: " + t6 + " ứng với " + tt6 + "\n"
                + "Hướng: " + t7 + " ứng với " + tt7 + "\n"
                + "Hướng: " + t8 + " ứng với " + tt8 + "\n"
                ;
            TempData["message"] = message;

            TempData["link"] = "http://tuvigiadao.somee.com/Guest/XemPhongThuy/Index";
            TempData["picture"] = "http://lichvansu.wap.vn/images/xem-tu-vi-tron-doi.jpg";
             TempData["name"] = "Xem Tử Vi Hướng Nhà";
            TempData["flag"] = 0;
            TempData["caption"] = "Click để xem hướng nhà phù hợp với tuổi của bạn..";

            return GetByFB();
        }
        public ActionResult sharetd()
        {
            string namsinh1 = Request.Form["namsinh1"];
            string gioitinh1 = Request.Form["gioitinh1"];
            string menh1 = Request.Form["menh1"];
            string giainghia1 = Request.Form["giainghia1"];
            string cungmenh1 = Request.Form["cungmenh1"];
            string nienmenh1 = Request.Form["nienmenh1"];
            string tutrach1 = Request.Form["tutrach1"];

            string namsinh2 = Request.Form["namsinh2"];
            string gioitinh2 = Request.Form["gioitinh2"];
            string menh2 = Request.Form["menh2"];
            string giainghia2 = Request.Form["giainghia2"];
            string cungmenh2 = Request.Form["cungmenh2"];
            string nienmenh2 = Request.Form["nienmenh2"];
            string tutrach2 = Request.Form["tutrach2"];
            string diemnguhanh = Request.Form["diemnguhanh"];
            string diemtutrach = Request.Form["diemtutrach"];
            string diemtuoi = Request.Form["diemtuoi"];
            string chenhlech = Request.Form["chenhlech"];
            string diem = Request.Form["diem"];
            string message = "Bạn là " + gioitinh1 + " Sinh ngày " + namsinh1 + "\n Mệnh: " + menh1 + " tức là: " + giainghia1
                + "\n Cung: " + cungmenh1 + "\n Niên mệnh: " + nienmenh1 + "\n Tứ trạch: " + tutrach1 + "\n \n Người ấy là "
                + gioitinh2 + " Sinh ngày " + namsinh2 + "\n Mệnh " + menh2 + " tức là: " + giainghia2
                + "\n Cung: " + cungmenh2 + "\n Niên mệnh: " + nienmenh2 + "\n Tứ trạch: " + tutrach2
                + "\n \n Xét Ngũ hành : " + menh1 + " - " + menh2 + ". Đánh giá: " + diemnguhanh + "/5 điểm" +
                "\n Về tứ trạch: " + tutrach1 + " - " + tutrach2 + ". Đánh giá: " + diemtutrach+ "/3 điểm"
                + "\n Bạn và người ấy cách nhau " + chenhlech + " tuổi. Đánh giá: " + diemtuoi+ "/2 điểm"

                + "\n \n Tổng điểm là : " + diem+"/10 điểm";
                ;
            TempData["message"] = message;
            TempData["flag"] = 1;
            TempData["link"] = "http://tuvigiadao.somee.com/Guest/TinhDuyen/Index";
            TempData["picture"] = "http://4.bp.blogspot.com/-qVnDdzTSnNE/UFAvKkXfV2I/AAAAAAAABmc/7o3pRhgTwDQ/s1600/tien+thien+bat+quai.jpg";
            TempData["name"] = "Xem Bói Tình Yêu";

            TempData["caption"] = "Click để Xem Bói Tình yêu..";


            return GetByFB();
        }
        public ActionResult FacebookCallback(string code) //Lấy thông tin người dùng fb và login vào
        {
            var fb = new FacebookClient();
          //  string nameValues = "sss";
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
                dynamic parameters = new ExpandoObject();
                //parameters.message = TempData["message"];
                parameters.link = TempData["link"];// TempData["link"];
            //    parameters.picture = TempData["picture"];
            //    parameters.name = TempData["name"];
     
              //  parameters.caption = TempData["caption"];
                fb.Post("/me/feed", parameters);

            
            }

            if (TempData["flag"].ToString().Equals("0"))
            { return Redirect("~/Guest/XemPhongThuy/Forward"); }
            else
                return Redirect("~/Guest/TinhDuyen/Forward");


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


    }
}
