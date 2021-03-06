﻿using HR.Models.Time_management;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class work_location : BaseModel
    {
        [Display(Name = "Location name")]
        public string Location_name { get;set;}
        public int? Public_Holidays_DatesID { get; set; }
        public virtual Public_Holidays_Dates Public_Holidays_Dates { get; set; }
        public virtual List<Shift_setup> Shift_setup { get; set; }

        public virtual List<Day_Status_Linkedto_Location> Day_Status_Linkedto_Location { get; set; }
        public int? Defaultdaystatuscode  { get; set; }
        public int? Absencedaystatus { get; set; }
    }
}