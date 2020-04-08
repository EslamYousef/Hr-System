using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models.Time_management
{
    public class Employee_Shift_schedule:BaseModel
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime From_date { get; set; } = DateTime.Now;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime To_date { get; set; } = DateTime.Now;
        public bool Use_As_Default { get; set; } = false;

        public virtual Employee_Profile Employee_Profile { get; set; }
        public int? Employee_ProfileID { get; set; }
        public virtual List<Schedule_Details> Schedule_Details { get; set; }
    }
}