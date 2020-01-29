using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Models.ViewModel
{
    public class Assign_subqulifications_ViewModel
    {
        //public Sub_educational_body team { get; set; }
        //public int SELECTEDID { get; set; }
        //public int main { get; set; }
        //public bool chacked{get;set; }=false;

        public IList<SelectListItem> AvailableFruits { get; set; }
        public IList<string> SelectedFruits { get; set; }
        public string nameID { get; set; }
             
    }
}