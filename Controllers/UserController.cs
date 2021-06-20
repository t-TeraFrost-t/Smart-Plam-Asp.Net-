using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testt_6.Controllers
{
    public class UserController : Controller
    {
        
        public ActionResult LogIn(string email, string pwd)
        {
            
            testt_6.Models.User u = new testt_6.Models.User(email, pwd);
            if (u.id==0) return View("LogIn", new Models.Item() { id = 1 });
            this.Session["UserId"] = u.id;
            this.Session["UserName"] = u.name;
            this.Session["ElementType"] = 0;
            
            var redy = new testt_6.Models.Items().GetItems(int.Parse(this.Session["UserId"].ToString()));
            return View("Items",redy);
        }
        public ActionResult Log()
        {
            
            return View("LogIn", new Models.Item() { id = 0 });
        }
        public ActionResult Sign()
        {

            
            return View("Register", new Models.Item() { id = 0 });
        }
        public ActionResult SignIn(string email, string pwd1, string pwd2)
        {
            if (pwd1 != pwd2) return View("Register", new Models.Item() { id = 1 });
            new testt_6.Models.User().InsertUser(email, pwd1);
            testt_6.Models.User u = new testt_6.Models.User(email, pwd1);
            this.Session["UserId"] = u.id;
            this.Session["UserName"] = u.name;
            this.Session["ElementType"] = "0";
            return View("Items", new testt_6.Models.Items().GetItems(int.Parse(this.Session["UserId"].ToString())));
        }
        public ActionResult Exit()
        {
            Session.Remove("UserId");
            Session.Remove("UserName");
            Session.Remove("ElementType");

            return View("Index");
        }


    }
}
    
