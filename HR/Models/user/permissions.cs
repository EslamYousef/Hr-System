using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.user
{
    public class permissions
    {
        public int ID { get; set; }
        public string Permission_Name { get; set; }
        public type_permission type_permission { get; set; }
        public int module { get; set; }
        public int sub_module { get; set; }
     }
    public enum type_permission
    {
        module =1,
        sub_module =2,
        forms =3,
        contents_of_form =4

    }
}