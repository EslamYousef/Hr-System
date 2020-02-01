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
        public Organization_Chart organization { get; set; }
        public status status { get; set; }
        public List<items_man_power> items_man_power { get; set; }


    }
}