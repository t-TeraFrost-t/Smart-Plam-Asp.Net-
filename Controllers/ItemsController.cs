using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testt_6.Controllers
{
    public class ItemsController : Controller
    {
        // GET: Items
        testt_6.Models.Items items = new testt_6.Models.Items();
        public ActionResult Note(string search = "")
        {
            int user = int.Parse(this.Session["UserId"].ToString());
            items = items.GetNotes(user, search);
            return View("Items", items);
        }
        public ActionResult Task(string search = "")
        {
            int user = int.Parse(this.Session["UserId"].ToString());
            items = items.GetTasks(user, search);
            return View("Items", items);
        }
        public ActionResult Index(string search="")
        {
            int user = int.Parse(this.Session["UserId"].ToString());

            items = items.GetItems(user, search);


            return View("Items", items);
        }
        
    }
}