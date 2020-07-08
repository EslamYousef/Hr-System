using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public int SalaryCode { get; set; }

        public virtual List<Exit_permission_request> Exit_permission_request { get; set; }
    }
}