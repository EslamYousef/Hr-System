using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class man_power
    {
        [Key]
        public int ID { get; set; }
        public int from_year { get; set; }
        public int to_year { get; set; }
        public virtual Organization_Chart organization { get; set; } 
        public virtual status status { get; set; }
        public virtual List<items_man_power> items_man_power { get; set; }


    }
}