using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.Time_management
{
    public class Exit_permission_type:BaseModel
    {
        public virtual Shiftdaystatus Shiftdaystatus { get; set; }
        public int? ShiftdaystatusID { get; set; }
        public TimeSpan from { get; set; }
        public TimeSpan to { get; set; }
        public bool integrate_with_payroll { get; set; }
        public bool integrate_with_leaves { get; set; }
    }
}