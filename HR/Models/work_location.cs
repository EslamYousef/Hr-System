using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class work_location : BaseModel
    {
        [Display(Name = "Location name")]
        public string Location_name { get;set;}
    }
}