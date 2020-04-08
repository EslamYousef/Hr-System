using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models.Time_management
{
    public class Shift_setup:BaseModel
    {
        public working_system working_system { get; set; }
        public int Total_Hours { get; set; } = 0;
        public TimeSpan Start_time { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:hh\:mm}")]
        public TimeSpan End_time { get; set; } 
        public Weekend_setup Weekend_setup { get; set; }
        public int? Weekend_setupID { get; set; }
        public work_location work_location { get; set; }
        public int? work_locationID { get; set; }
        public virtual List<Schedule_Details> Schedule_Details { get; set; }
    }
}