using Chat_002.EFContext;
using Chat_002.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Chat_002.Controllers
{
    [AllowAnonymous]//Поскольку обращаться к контроллеру может любой, то для него установим атрибут [AllowAnonymous], который открывает анонимный доступ.
    public class AccountController : Controller
    {
        ChatContext db = new ChatContext();
        //
        // GET: /Account/
        public ViewResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LogViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.Login, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);
                    HttpCookie log = new HttpCookie("Login");
                    log.Value = model.Login;
                    Response.Cookies.Add(log);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("List", "Message");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            HttpCookie log = new HttpCookie("Login");
            log.Value = null;
            Response.Cookies.Add(log);

            return RedirectToAction("List", "Message");
            //return RedirectToAction("Login", "Account");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AdminPanel()
        {
            return View(db.Users);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminPanel(int UserId, string role)
        {
            User user = db.Users.Where(x => x.UserId == UserId).FirstOrDefault();
            user.Role = db.Roles.Where(x => x.Name == role).FirstOrDefault();
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return View(db.Users);
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult ModeratorPanel()
        {
            return View(db.Topics);
        }
        [HttpPost]
        [Authorize(Roles = "Moderator")]
        public ActionResult ModeratorPanelDeleteTopic(int TopicId)
        {
            Topic topic = db.Topics.Where(x => x.TopicId == TopicId).FirstOrDefault();
            db.Topics.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("ModeratorPanel", db.Topics);

           // return View(db.Topics);
        }



        public ActionResult Regictretion()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Regictretion(User user, System.Web.HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    user.ImageMimeType = image.ContentType;
                    user.ImageDate = new byte[image.ContentLength];
                    image.InputStream.Read(user.ImageDate, 0, image.ContentLength);
                }
                db.Users.Add(user);
                user.Role = db.Roles.FirstOrDefault(x => x.Name == "User");
                db.SaveChanges();
                TempData["message"] = string.Format("{0} has been saved", user.UserName);
                return RedirectToAction("Login");
            }
            else
            {
                // there is something wrong with the data values
                return View(user);
            }




            //db.Users.Add(user);
            //db.SaveChanges();

            //return RedirectToAction("Login");
        }

        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;

            using (ChatContext _db = new ChatContext())
            {
                try
                {
                    User user = (from u in _db.Users
                                 where u.Login == login && u.Password == password
                                 select u).FirstOrDefault();

                    if (user != null)
                    {
                        isValid = true;
                    }
                }
                catch
                {
                    isValid = false;
                }
            }
            return isValid;
        }

    }
}
