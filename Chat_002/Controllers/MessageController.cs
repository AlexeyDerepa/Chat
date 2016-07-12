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
    public class MessageController : Controller
    {
        //
        // GET: /Message/
        ChatContext db = new ChatContext();
       // [Authorize(Roles = "Admin, Moderator, User")]
        public ViewResult List(string category)
        {
            //var us = Request.Cookies["Login"].Value;
            var us = Request.Cookies["Login"];
            ViewBag.user = us == null ? "" : us.Value;
            ViewBag.category = category == null ? false : true;
            MessageListViewModel model = new MessageListViewModel
            {
                Messages = db.Messages
                .Where(p => category == null || p.Topic.TopicName == category)
                .OrderBy(p => p.MessageId),
                Topic = new Topic { TopicName = category }
            };
            return View(model);
        }
        public ViewResult CreateTopic()
        {
            return View();
        }


        [HttpPost]
        //[Authorize(Roles = "Admin, Moderator, User")]
        public ActionResult CreateMessage(Message message, Topic topic, HttpPostedFileBase error)
        {
            var us = FormsAuthentication.FormsCookieName;
            if (Request.Cookies["Login"].Value == null)
            {
                return RedirectToAction("Login", "Account");
            }
            //User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            string log = (string)Request.Cookies["Login"].Value;
            User user = db.Users.Where(m => m.Login == log).FirstOrDefault();
            
            if (user == null) return RedirectToAction("Index", "Home");

            message.User = user;
            message.Time = DateTime.Now;

            message.Topic = db.Topics.Where(m => m.TopicName == topic.TopicName).FirstOrDefault();
            db.Messages.Add(message);
            db.SaveChanges();

            return RedirectToAction("List", "Message");
        }
        // Создание новой заявки
        [HttpPost]
        public ActionResult CreateTopic(Topic topic, HttpPostedFileBase error)
        {
            var us = FormsAuthentication.FormsCookieName;
            if (Request.Cookies["Login"].Value == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // получаем текущего пользователя

            string log = (string)Request.Cookies["Login"].Value;
            User user = db.Users.Where(m => m.Login == log).FirstOrDefault();

            //User user = db.Users.Where(m => m.Role.Name == HttpContext.User.Identity.Name).FirstOrDefault();
            topic.User = user;
            db.Topics.Add(topic);
            db.SaveChanges();
            return RedirectToAction("List", "Message");
        }


        public FileContentResult GetImage(int userId)
        {
            User user = db.Users.FirstOrDefault(p => p.UserId == userId);
            if (user != null)
            {
                return File(user.ImageDate, user.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
