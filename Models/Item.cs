using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace testt_6.Models
{
    public class Item:IComparable<Item>
    {
        public int id { get; set; }
        public string name { get; set; }
        public string text_task { get; set; }
        public int id_importance { get; set; }
        public int id_user { get; set; }
        public DateTime start_date { get; set; }
        protected Conection conection = new Conection();

        public Item()
        {
        }

        public int CompareTo(Item t)
        {
            return start_date.CompareTo(t.start_date);
        }
    }
}
