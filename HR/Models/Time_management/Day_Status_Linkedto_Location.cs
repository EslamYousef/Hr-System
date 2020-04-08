using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.Time_management
{
    public class Day_Status_Linkedto_Location
    {
        public int ID { get; set; }
        public virtual Shiftdaystatus Shiftdaystatus { get; set; }
        public int ShiftdaystatusID { get; set; }
        public virtual work_location work_location { get; set; }
        public int work_locationID { get; set; }
    }
}