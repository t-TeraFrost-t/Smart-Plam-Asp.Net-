using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySql.Data.MySqlClient;
using System.Configuration;

namespace testt_6.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
     
        Conection conection = new Conection();

        public User()
        {
            
        }

        public User(string username, string password)
        {
            conection.Get_Connection();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conection.connection;
                cmd.CommandText = string.Format("call select_user(\'{0}\',\'{1}\')", username, password);

                MySqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    reader.Read();
                    id = !reader.IsDBNull(0) ? int.Parse(reader.GetString(0)) : 0;
                    name = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    
                    
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

        public void InsertUser(string username, string password)
        {
            conection.Get_Connection();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conection.connection;
                cmd.CommandText = string.Format("call insert_user(\'{0}\',\'{1}\')", username, password);
               
               // MySqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    cmd.ExecuteNonQuery();
                    
                }
                catch (MySqlException e)
                {
                    string MessageString = "Read error occurred  / entry not found loading the Column details: "
            + e.ErrorCode + " - " + e.Message + "; \n\nPlease Continue";
                    //MessageBox.Show(MessageString, "SQL Read Error");
                    conection.connection.Close();
                    name = MessageString;
                    return;
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
    }
}