using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySql.Data.MySqlClient;
using System.Configuration;
namespace testt_6.Models
{
    public class Note : Item
    {

        public int id_task { get; set; }
        public Note()
        {

        }

        public Note(int arg_id)
        {
            conection.Get_Connection();
            id = arg_id;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conection.connection;
                cmd.CommandText = string.Format("call select_note_id({0})", id);

                MySqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    reader.Read();
                    name = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    text_task = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                    id_importance = !reader.IsDBNull(3) ? int.Parse(reader.GetString(3)) : 0;
                    id_user = !reader.IsDBNull(4) ? int.Parse(reader.GetString(4)) : 0;
                    id_task = !reader.IsDBNull(5) ? int.Parse(reader.GetString(5)) : 0;
                    start_date = !reader.IsDBNull(6) ? DateTime.Parse(reader.GetString(6)) : DateTime.MinValue;
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

        }
        public void UpdateNote(Note n)
        {
            conection.Get_Connection();

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conection.connection;
                cmd.CommandText = string.Format("call update_note(\'{0}\',\'{1}\',{2},{3},{4},{5},\'{6}\')", n.id, n.name, n.text_task, n.id_importance, n.id_user,n.id_task, n.start_date);
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

        public void InsertNote(Note task)
        {
            conection.Get_Connection();
            string startDate = string.Format("{0,2:D2}-{1,2:D2}-{2,2:D2} {3,2:D2}:{4,2:D2}:{5,2:D2}", task.start_date.Year, task.start_date.Month, task.start_date.Day, task.start_date.Hour, task.start_date.Minute, task.start_date.Second);
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conection.connection;
                cmd.CommandText = string.Format("call insert_note(\'{0}\',\'{1}\',{2},{3},{4},\'{5}\')", task.name, task.text_task, task.id_importance, task.id_user.ToString()[0]=='0'?"null": task.id_user.ToString(), task.id_task.ToString()[0] == '0' ? "null" : task.id_task.ToString(), startDate);
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

        public void DeleteNote()
        {
            conection.Get_Connection();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conection.connection;
                cmd.CommandText = string.Format("call delete_note({0}", id);
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
