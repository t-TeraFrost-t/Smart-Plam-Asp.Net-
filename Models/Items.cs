using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySql.Data.MySqlClient;
using System.Configuration;
namespace testt_6.Models
{
    public class Items
    {
        public List<Item> items { get; set; }
        private Conection conection = new Conection();

        private string select_note_substring = "call select_note_user_substring({0},\'{1}\')";
        private string select_note = "call select_note_user({0})";

        private string select_task_substring = "call select_task_user_substring({0},\'{1}\')";
        private string select_task = "call select_task_user({0})";
        public Items()
        {
           items = new List<Item>();
        }
        public Items GetNotes(int id_user,string substring="")
        {
            conection.Get_Connection();
            Items it = new Items();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conection.connection;

                cmd.CommandText = string.Format(substring.Length == 0 ? select_note: select_note_substring, id_user,substring);

                MySqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        Note t = new Note();

                        t.id = !reader.IsDBNull(0) ? int.Parse(reader.GetString(0)) : 0;
                        t.name = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                        t.text_task = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                        t.id_importance = !reader.IsDBNull(4) ? int.Parse(reader.GetString(4)) : 0;
                        t.id_user = !reader.IsDBNull(5) ? int.Parse(reader.GetString(5)) : 0;
                        t.start_date = !reader.IsDBNull(3) ? DateTime.Parse(reader.GetString(3)) : DateTime.MinValue;
                        

                        try
                        {
                            it.items.Add(t);
                        }
                        catch (Exception e) { }
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
            try
            {
                it.items.OrderBy(el => el.start_date);
            }
            catch (ArgumentNullException e)
            { }
            return it;

        }
        
        public Items GetTasks(int id_user,string substring="")
        {
            conection.Get_Connection();
            Items it = new Items();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conection.connection;
                
                cmd.CommandText = string.Format(substring.Length == 0 ? select_task : select_task_substring, id_user, substring);

                MySqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        Task t = new Task
                        {
                            id = !reader.IsDBNull(4) ? int.Parse(reader.GetString(4)) : 0,
                            name = !reader.IsDBNull(1) ? reader.GetString(1) : null,
                            text_task = !reader.IsDBNull(2) ? reader.GetString(2) : null,
                            id_importance = !reader.IsDBNull(3) ? int.Parse(reader.GetString(3)) : 0,
                            id_user = !reader.IsDBNull(4) ? int.Parse(reader.GetString(4)) : 0,
                            start_date = !reader.IsDBNull(5) ? DateTime.Parse(reader.GetString(5)) : DateTime.MinValue,
                            end_date = !reader.IsDBNull(6) ? DateTime.Parse(reader.GetString(6)) : DateTime.MinValue
                        };

                        it.items.Add(t);
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
            try
            {
                it.items.OrderBy(el => el.start_date);
            }
            catch (ArgumentNullException e)
            { }
            return it;

        }

       

        public Items GetItems(int id_user,string substring="")
        {


            Items it = new Items();
            

            
                var a = it.GetTasks(id_user, substring).items;
                var b = it.GetNotes(id_user, substring).items;
                it.items.AddRange(a);
                it.items.AddRange(b);
                it.items.OrderBy(el => el.start_date);
            
            return it;
        }

        
  }

}


