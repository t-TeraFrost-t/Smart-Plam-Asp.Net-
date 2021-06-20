using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySql.Data.MySqlClient;
using System.Configuration;
namespace testt_6.Models
{
    public class Task : Item
    {

       
        public DateTime end_date { get; set; }
        public List<Note> notes { get; set; }
       

        public Task()
        {

        }



        public List<Note> GetNotes()
        {
            conection.Get_Connection();
            List<Note> it = new List<Note>();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conection.connection;
                cmd.CommandText = string.Format("call select_note_id_task({0})", id);

                MySqlDataReader reader = cmd.ExecuteReader();
                
                try
                {

                    //reader.
                    while (reader.Read()) 
                    {
                        
                        Note t = new Note();
                        t.id = !reader.IsDBNull(0) ? int.Parse(reader.GetString(0)) : 0;
                        t.name = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                        t.text_task = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                        t.start_date = !reader.IsDBNull(3) ? DateTime.Parse(reader.GetString(3)) : DateTime.MinValue;
                        t.id_importance = !reader.IsDBNull(4) ? int.Parse(reader.GetString(4)) : 0;
                        t.id_user = !reader.IsDBNull(5) ? int.Parse(reader.GetString(5)) : 0;
                        


                        it.Add(t);
                    } 


                }
                catch (MySqlException e)
                {
                    string MessageString = "Read error occurred  / entry not found loading the Column details: "
            + e.ErrorCode + " - " + e.Message + "; \n\nPlease Continue";
                    //MessageBox.Show(MessageString, "SQL Read Error");
                    reader.Close();

                    //Address1 = Address2 = null;
                }
            }
            catch (MySqlException e)

            {
                string MessageString = "The following error occurred loading the Column details: "
                    + e.ErrorCode + " - " + e.Message;

                //Address1 = Address2 = null;
            }
            conection.connection.Close();
            it.Sort((el, il) => el.CompareTo(il));
            return it;

        }



        public Task(int arg_id)
        {
            conection.Get_Connection();
            id = arg_id;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conection.connection;
                cmd.CommandText = string.Format("call select_task_id({0})", id);

                MySqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    reader.Read();
                    name = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    text_task = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                    id_importance = !reader.IsDBNull(3) ? int.Parse(reader.GetString(3)) : 0;
                    id_user = !reader.IsDBNull(4) ? int.Parse(reader.GetString(4)) : 0;
                    start_date = !reader.IsDBNull(5) ? DateTime.Parse(reader.GetString(5)) : DateTime.MinValue;
                    end_date = !reader.IsDBNull(6) ? DateTime.Parse(reader.GetString(6)) : DateTime.MinValue;
                   
                    
                    
                }
                catch (MySqlException e)
                {
                    string MessageString = "Read error occurred  / entry not found loading the Column details: "
            + e.ErrorCode + " - " + e.Message + "; \n\nPlease Continue";
                    //MessageBox.Show(MessageString, "SQL Read Error");
                    reader.Close();
                    name = MessageString;
                    //Address1 = Address2 = null;
                }
            }
            catch (MySqlException e)

            {
                string MessageString = "The following error occurred loading the Column details: "
                    + e.ErrorCode + " - " + e.Message;
                name = MessageString;
                //Address1 = Address2 = null;
            }




            conection.connection.Close();
            this.notes = this.GetNotes();

        }

        public void UpdateTask(Task t)
        {
            conection.Get_Connection();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conection.connection;
                cmd.CommandText = string.Format("call update_task(\'{0}\',\'{1}\',{2},{3},{4},\'{5}\',\'{6}\')", t.id, t.name, t.text_task, t.id_importance, t.id_user, t.start_date, t.end_date);

                cmd.ExecuteNonQuery();

                
            }
            catch (MySqlException e)

            {
                string MessageString = "The following error occurred loading the Column details: "
                    + e.ErrorCode + " - " + e.Message;
                name = MessageString;
                //Address1 = Address2 = null;
            }




            conection.connection.Close();
           

        }

        public void InsertTask(Task task)
        {
            string startDate = string.Format("{0,2:D2}-{1,2:D2}-{2,2:D2} {3,2:D2}:{4,2:D2}:{5,2:D2}", task.start_date.Year, task.start_date.Month, task.start_date.Day, task.start_date.Hour, task.start_date.Minute, task.start_date.Second);
            string endDate = string.Format("{0,2:D2}-{1,2:D2}-{2,2:D2} {3,2:D2}:{4,2:D2}:{5,2:D2}", task.end_date.Year, task.end_date.Month, task.end_date.Day, task.end_date.Hour, task.end_date.Minute, task.end_date.Second);
            conection.Get_Connection();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conection.connection;
                cmd.CommandText = string.Format("call insert_task(\'{0}\',\'{1}\',{2},{3},\'{4}\',\'{5}\')", task.name, task.text_task, task.id_importance, task.id_user, startDate, endDate);
                //if(cmd.ExecuteNonQuery()!=1)cmd.RollBack();???????
                //MySqlDataReader reader = cmd.ExecuteReader();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)

            {
                string MessageString = "The following error occurred loading the Column details: "
                    + e.ErrorCode + " - " + e.Message;
                name = MessageString;
                //Address1 = Address2 = null;
            }
            conection.connection.Close();
        }

        public void DeleteTask()
        {
            conection.Get_Connection();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conection.connection;
                cmd.CommandText = string.Format("call delete_task({0}", id);
                //if(cmd.ExecuteNonQuery()!=1)cmd.RollBack();???????
                //MySqlDataReader reader = cmd.ExecuteReader();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)

            {
                string MessageString = "The following error occurred loading the Column details: "
                    + e.ErrorCode + " - " + e.Message;
                name = MessageString;
                //Address1 = Address2 = null;
            }
            conection.connection.Close();
        }

    }
}