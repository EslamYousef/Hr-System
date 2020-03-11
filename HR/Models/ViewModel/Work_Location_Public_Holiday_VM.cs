using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Models.ViewModel
{
    public class Work_Location_Public_Holiday_VM
    {
        public IList<SelectListItem> AvailableFruits { get; set; }
        public IList<string> SelectedFruits { get; set; }
        public int nameID { get; set; }
    }
}