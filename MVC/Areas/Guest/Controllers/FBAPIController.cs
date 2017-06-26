using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Areas.Guest.Controllers
{
    public class FBAPIController : Controller
    {
        //public long InsertForFacebook(User user) //Hàm này dùng lưu dữ liệu người dùng fb xuống database
        //{

        //    var user2 = db.Users.SingleOrDefault(x => x.username == user.username);
        //    if (user2 == null)
        //    {
        //        user.soluotchoi = 5;
        //        db.Users.Add(user);
        //        db.SaveChanges();
        //        return user.ID;
        //        //return Redirect("~/User");
        //    }
        //    else
        //    {
        //        return user.ID;
        //    }
        //}



        public ActionResult LoginFacebook()  //Lấy thông tin từ app do mình tạo ra trên fb
        {

            var fb = new FacebookClient();

           
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "218742458552684",
                client_secret = "5d96599075e989c462f7aa497fdd2fee",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });
            return Redirect(loginUrl.AbsoluteUri);
        }



        public ActionResult FacebookCallback(string code) //Lấy thông tin người dùng fb và login vào
        {
            var fb = new FacebookClient();
         
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "218742458552684",
                client_secret = "5d96599075e989c462f7aa497fdd2fee",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                string bodymessage = "This is test message description new";
            
                fb.AccessToken = accessToken;
                fb.Post("/me/feed", new { message = bodymessage, title = "dsadsa" });
                dynamic me = fb.Get("me?fields=first_name, middle_name, last_name, email");
                string email = me.email;
                string firstname = me.first_name;
                string midname = me.middle_name;
                string lastname = me.last_name;

                //var user = new User();
                //user.email = email;
                //user.username = email;
                //user.nickname = firstname + " " + midname + " " + lastname;

               // var resultInsert = new UserController().InsertForFacebook(user);
                //if (resultInsert > 0)
                //{
                //    Session["userName"] = user.nickname;
                //    Session["IDs"] = user.ID;
                //    Session["eMail"] = user.email;
                //    Session["pHone"] = user.phone;
                //    Session["soLuotChoi"] = user.soluotchoi.ToString();
                //}
            }
            return Redirect("~/User/userProfile");
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
