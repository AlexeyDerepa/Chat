using Chat_002.EFContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chat_002.Controllers
{
    public class NavController : Controller
    {
        //
        // GET: /Nav/

        ChatContext db = new ChatContext();
        public ActionResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = db.Topics
                .Select(x => x.TopicName)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}
