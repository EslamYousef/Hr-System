using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.user
{
    public class Screen
    {
        public int ID { get; set; }
        public string name { get; set; }
        public type type { get; set; }
        public int? parent_screen { get; set; } ///if it is field only 
        public type_filed? type_filed { get; set; }  ///if it is field only
    }
    public enum type
    {
        screen=1,
        filed=2
    }
    public enum type_filed
    {
        normal=1,
        date=2
    }
}