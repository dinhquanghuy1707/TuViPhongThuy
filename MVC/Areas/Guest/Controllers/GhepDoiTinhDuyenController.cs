using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using System.Dynamic;
using MVC.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
namespace MVC.Areas.Guest.Controllers
{
    public class GhepDoiTinhDuyenController : Controller
    {
        //
        // GET: /Guest/GhepDoiTinhDuyen/
        TuViPhongThuyContext db = new TuViPhongThuyContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Forward()
        {
            @ViewBag.myPic = Request.QueryString["myPic"];
            @ViewBag.objPic = Request.QueryString["objPic"];
            
            return View();
        }

        public ActionResult submit()  //Lấy thông tin từ app do mình tạo ra trên fb
        {

            var fb = new FacebookClient();


            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "1567232153295149",
                client_secret = "eab1b4dd9956001f52bd54229aea61c1",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email, user_about_me, user_birthday"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code) //Lấy thông tin người dùng fb và login vào
        {
            //  string bd = "cc";

            string IdPost = "";
            string id="";
            string date="";
            string gender="";
            string imageURL="";
            var fb = new FacebookClient();
            // string nameValues = "sss";
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
                //dynamic parameters = new ExpandoObject();
                //parameters.message = "Check out this funny article";
                //parameters.link = "http://www.natiska.com/article.html";
                //parameters.picture = "http://www.natiska.com/dav.png";
                //parameters.name = "Article Title";
                //parameters.privacy = new { value = "EVERYONE" };
                //parameters.caption = "Caption for the link";
                //fb.Post("/me/feed", parameters


                //);
                dynamic me = fb.Get("me?fields=id,name,birthday,gender");
                 id = me.id;
                dynamic parameters = new ExpandoObject();
                //DateTime dt = Convert.ToDateTime(me.birthday);
                string datestr = me.birthday;
                DateTime dt1 = DateTime.ParseExact(datestr, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                date = dt1.ToString("yyyy-MM-dd");
                 gender = me.gender;

                //parameters.message = "Xem chúng ta được bao nhiêu điểm nào <3 ! ";



                //parameters.link = "http://tuvigiadao.somee.com/Guest/GhepDoiTinhDuyenListener/submit2/?id=" + id + "&birthday=" + date + "&gender=" + me.gender;
                //parameters.picture = "http://tuvisomenh.com/Media/Default/BlogPost/tu-vi-2017-tuoi-than.jpg";
                //parameters.name = id;

                //parameters.caption = "Click để xem chúng ta hợp nhau tới đâu nhé !";
                //fb.Post("/me/feed", parameters);

              

                // MD5 encode
                byte[] mang = System.Text.Encoding.UTF8.GetBytes(id);

                MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
                mang = my_md5.ComputeHash(mang);

                foreach (byte b in mang)
                {
                    IdPost += b.ToString("x2");//Nếu là "X2" thì kết quả sẽ tự chuyển sang ký tự in Hoa
                }
                //
                // Draw an image

                imageURL = Image(id, IdPost);        
                var post = new GhepDoiTinhDuyenPost();
                // Get post from DB
                post = db.GhepDoiTinhDuyenPosts.Where(s => s.IdPost == IdPost).FirstOrDefault();
                if (post != null)
                {
                    post.IdPost = IdPost;
                    post.IdFB = id;
                    post.Birthday = date;
                    post.Name = me.name;
                    if (gender.Equals("male", StringComparison.Ordinal))
                    {
                        // 1 true là nam
                        post.Gender = true;
                    }
                    else
                    {
                        // 0 false là nữ
                        post.Gender = false;
                    }
                    post.Image = imageURL;
                    // Mark entity as modified
                    db.Entry(post).State = System.Data.Entity.EntityState.Modified;
                    //  call SaveChanges
                    db.SaveChanges();
                }
                else
                {
                    post = new GhepDoiTinhDuyenPost();
                    post.IdPost = IdPost;
                    post.IdFB = id;
                    post.Birthday = date;
                    if (gender.Equals("male", StringComparison.Ordinal))
                    {
                        // 1 true là nam
                        post.Gender = true;
                    }
                    else
                    {
                        // 0 false là nữ
                        post.Gender = false;
                    }
                    post.Image = imageURL;
                    post.Name = me.name;
                    db.GhepDoiTinhDuyenPosts.Add(post);
                    db.SaveChanges();
                }

            }

            //TempData["url"] = "http://localhost:3104/Guest/XemPhongThuy/submit?" + nameValues;
            TempData["url"] = "Guest/GhepDoiTinhDuyenListener/submit2?IDPost=" + IdPost;

            return Redirect("/Guest/GhepDoiTinhDuyen/submit2?IdPost="+IdPost);    
          //  return Redirect("~/Guest/GhepDoiTinhDuyenListener/submit2?"+nameValues);  ???????
        }

        public ActionResult submit2(string IdPost)
        {
            TuViPhongThuyContext db = new TuViPhongThuyContext();
            string gender="female";

            var PostRow = db.GhepDoiTinhDuyenPosts.FirstOrDefault(a => a.IdPost == IdPost);


            // Convert datetime in SQL to Datetime in these
            string dt = PostRow.Birthday.ToString();
            DateTime dtt = Convert.ToDateTime(dt);
            string birthday = dtt.ToString("yyyy-MM-dd");
            //DateTime dt = DateTime.ParseExact(PostRow.Birthday.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            

           
         
            if (PostRow.Gender.Value)
                gender = "male";
            string IdFB=PostRow.IdFB ;
            string Image = PostRow.Image;

           string url = "Guest/GhepDoiTinhDuyenListener/submit2?IdPost=" +IdPost;
           // string url = "http://localhost:3104/Guest/GhepDoiTinhDuyenListener/submit2/?id=" + IdFB + "&birthday=" + birthday + "&gender=" + gender;
            ViewBag.IdFB = IdFB;
            ViewBag.Image = Image;
            ViewBag.Gender = gender;
            ViewBag.IdPost = IdPost;
            ViewBag.Birthday = birthday;
            ViewBag.Url = url;
            return View();
        }

        public string Image(string FbId,string PostID)
        {
            string imageURL=PostID;
            WebClient wc = new WebClient();
            byte[] originalData = wc.DownloadData("https://graph.facebook.com/" + FbId + "/picture?height=200&type=square");
            MemoryStream stream = new MemoryStream(originalData);
            Bitmap myBitmapLogo = new Bitmap(stream);

            //byte[] originalData2 = wc.DownloadData("https://graph.facebook.com/1508511446100036/picture?height=200&type=square");
            //MemoryStream stream2 = new MemoryStream(originalData2);
            //Bitmap myBitmapLogo2 = new Bitmap(stream2);

            StringFormat stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Center;
            string Str_TextOnImage = "Xem chúng ta hợp nhau bao nhiêu nào ?";
            //string Str_Point = Convert.ToString(TempData["Point"]);
            string Str_Point = "???";

            Color StringColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            // Đường dẫn file ảnh. 
            string imageFile = Server.MapPath("~/Assets/Homepage/images/TinhDuyenGhepDoi.png");
            // Tạo đối tượng Bitmap truyền vào đường dẫn File ảnh
            Bitmap myBitmap = new Bitmap(imageFile);
            // Tạo đối tượng Graphic từ Bitmap
            Graphics myGraphics = Graphics.FromImage(myBitmap);

            //// Vẽ lại hình ảnh, chèn nội dung mới vào.
            //Bitmap myBitmapLogo = new Bitmap(logo);

            myGraphics.DrawImage(myBitmapLogo, new Point(90, 95));
            //myGraphics.DrawImage(myBitmapLogo2, new Point(610, 95));
            myGraphics.DrawString(Str_Point, new Font("arial", 65,
            FontStyle.Bold), new SolidBrush(StringColor), new Point(450, 125), stringformat);
            myGraphics.DrawString(Str_TextOnImage, new Font("arial", 32,
            FontStyle.Bold), new SolidBrush(StringColor), new Point(450, 375), stringformat);
            var bitmap = myBitmap; // The method that returns Bitmap
            var bitmapBytes = BitmapToBytes(bitmap); //Convert bitmap into a byte array
            bitmap.Save(Server.MapPath("~/Assets/Homepage/images/GhepDoiTinhDuyenPost/"+PostID+".jpg"), ImageFormat.Jpeg);


            return PostID+".jpg";
        }
        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
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