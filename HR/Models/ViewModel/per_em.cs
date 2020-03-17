using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class per_em
    {
        public int PER_id {get;set;}
        public List<Employee_Profile> emp { get; set; }
    }
}