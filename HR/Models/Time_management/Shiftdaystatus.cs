using HR.Models.Time_management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Shiftdaystatus:BaseModel
    {
        public string Alias { get; set; }
        public string Color { get; set; }
        public bool Disable_Editing { get; set; }
        public virtual Shift_setup Shift_setup { get; set; }
        public int? Shift_setupID { get; set; }
        public virtual List<Day_Status_Linkedto_Location> Day_Status_Linkedto_Location { get; set; }
        public virtual List<Business_Trip> Business_Trip { get; set; }
        public virtual List<Exit_permission_type> Exit_permission_type { get; set; }
        public virtual List<Schedule_Details> Schedule_Details { get; set; }
    }
}