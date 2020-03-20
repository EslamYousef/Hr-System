using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class exper_jobdetails
    {
        public int ID { get; set; }
        public virtual Job_Details Job_Details { get; set; }
        public int Job_DetailsID { get; set; }
        public Experience_group Experience_group { get; set; }
        public int Experience_groupID { get; set; }
    }
}