using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.user
{
    public class group_with_permission
    {
        public int ID { get; set; }
        //public  virtual User_Group_Info User_Group_Info { get; set; }
        public int User_Group_InfoID { get; set; }
        public string Rolle_name { get; set; }

    }
}