using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testt_6.Controllers
{
    public class NoteController : Controller
    {
       testt_6.Models.Note myNot = new Models.Note();
        public ActionResult Select(int id)
        {
            testt_6.Models.Note myNote = new testt_6.Models.Note(id);
            return View("NoteView", myNote);
        }
        public ActionResult Update(int id, testt_6.Models.Note t)
        {
            
                testt_6.Models.Note myNote = new testt_6.Models.Note(id);
                myNote.UpdateNote(t);
            
            
            return View("NoteView", myNote);
        }
        public ActionResult Delete(int id)
        {
            testt_6.Models.Note myNote = new testt_6.Models.Note(id);
            myNote.DeleteNote();
            return View("Home");
        }
        public ActionResult InsertT()
        {
            //testt_6.Models.Task myTask = new testt_6.Models.Task(id);

            return View("InsertNote", new Models.Items().GetTasks(int.Parse(Session["UserId"].ToString())));
        }
        public ActionResult Insert(string nam,string text, string StartDate, string Importance, string Pointer)
        {
            //testt_6.Models.Note myNote = new testt_6.Models.Note(id);
            try
            {
                myNot.InsertNote(
                     new Models.Note()
                     {
                         name = nam,
                         text_task = text,
                         start_date = DateTime.Parse(StartDate),
                         id_importance = int.Parse(Importance),
                         id_user = Pointer == "null" ? int.Parse(Session["UserId"].ToString()) : 0,
                         id_task = Pointer != "null" ? int.Parse(Pointer) : 0
                     }

                    );
            }
            catch (Exception e)
            {
                return View("InsertNote", new Models.Items().GetTasks(int.Parse(Session["UserId"].ToString())));
            }
            return View("Items", new Models.Items().GetItems(int.Parse(Session["UserId"].ToString())));
        }
    }
}