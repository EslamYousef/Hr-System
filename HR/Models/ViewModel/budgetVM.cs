using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class budgetVM
    {
        public Budget Budget { get; set; }
        public status status { get; set; }
        public justification justification { get;set;}
    }
}