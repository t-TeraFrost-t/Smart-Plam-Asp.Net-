using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MySql.Data.MySqlClient;
using System.Configuration;
namespace testt_6.Controllers
{
    public class TaskController : Controller
    {
        testt_6.Models.Task myTas = new Models.Task();
        // GET: Task
        public ActionResult Select(int id)
        {
            testt_6.Models.Task myTask = new testt_6.Models.Task(id);
            return View("TaskView", myTask);
        }
        public ActionResult UpdateT(Models.Task task)
        {
            //testt_6.Models.Task myTask = new testt_6.Models.Task(id);

            return View("UpdateTaskView",task);
        }
        public ActionResult Update(string nam, string text, string StartDate, string EndDate, string Importance)
        {

            myTas.UpdateTask(
                new Models.Task()
                {
                    name = nam,
                    text_task = text,
                    id_importance = int.Parse(Importance),
                    id_user = int.Parse(Session["UserId"].ToString()),
                    start_date = DateTime.Parse(StartDate),
                    end_date = DateTime.Parse(EndDate),
                }
                );
            return View("Items", new Models.Items().GetItems(int.Parse(Session["UserId"].ToString())));
        }
        public ActionResult Delete(int id)
        {
            testt_6.Models.Task myTask = new testt_6.Models.Task(id);
            myTask.DeleteTask();
            return View("Home" );
        }
        public ActionResult Insert(string nam,string text,string StartDate,string EndDate, string Importance)
        {
            try
            {
                //testt_6.Models.Task myTask = new testt_6.Models.Task(id);
                myTas.InsertTask(
                    new Models.Task()
                    {
                        name = nam,
                        text_task = text,
                        id_importance = int.Parse(Importance),
                        id_user = int.Parse(Session["UserId"].ToString()),
                        start_date = DateTime.Parse(StartDate),
                        end_date = DateTime.Parse(EndDate),
                    }
                    );
            }
            catch (Exception e)
            {
                return View("InsertTaskView");
            }
            return View("Items",new Models.Items().GetItems(int.Parse(Session["UserId"].ToString())));
        }
        public ActionResult InsertT()
        {
            //testt_6.Models.Task myTask = new testt_6.Models.Task(id);
           
            return View("InsertTaskView");
        }

    }
        
}
