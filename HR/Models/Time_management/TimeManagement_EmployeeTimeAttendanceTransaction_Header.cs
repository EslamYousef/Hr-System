using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.Time_management
{
    [Table("TimeManagement_EmployeeTimeAttendanceTransaction_Header")]
    public class TimeManagement_EmployeeTimeAttendanceTransaction_Header
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Employee_Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public int EffectiveMonth { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public int EffectiveYear { get; set; }
        public string Location_Code { get; set; }
        public Nullable<short> Working_System { get; set; }
        public string Shift_Code { get; set; }
        public Nullable<short> Transaction_Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Created_Comment { get; set; }
        public string ReportAsReadyBy { get; set; }
        public Nullable<System.DateTime> ReportAsReadyDate { get; set; }
        public string ReportAsReady_Comment { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string Approved_Comment { get; set; }
        public string RejectedBy { get; set; }
        public Nullable<System.DateTime> RejectedDate { get; set; }
        public string Rejected_Comment { get; set; }
        public string CanceledBy { get; set; }
        public Nullable<System.DateTime> CanceledDate { get; set; }
        public string Canceled_Comment { get; set; }
        public string ClosedBy { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public string Closed_Comment { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }

        public virtual status status { get; set; }
        public check_status check_status { get; set; }

        public int statID { get; set; }
        public string name_state { get; set; }
    }
}