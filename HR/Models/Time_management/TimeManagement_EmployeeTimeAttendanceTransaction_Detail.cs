using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models.Time_management
{
    [Table("TimeManagement_EmployeeTimeAttendanceTransaction_Detail")]
    public class TimeManagement_EmployeeTimeAttendanceTransaction_Detail
    {
        [Key]
        public int ID { get; set; }
        public string TransactionNumber { get; set; }
        public System.DateTime AttendanceDate { get; set; }
        public string Employee_Code { get; set; }
        public int EffectiveMonth { get; set; }
        public int EffectiveYear { get; set; }
        public Nullable<System.TimeSpan> TimeIn { get; set; }
        public Nullable<System.TimeSpan> TimeOut { get; set; }
        public Nullable<bool> IsCalling { get; set; }
        public Nullable<System.TimeSpan> TimeInAfterCalling { get; set; }
        public Nullable<System.TimeSpan> TimeOutAfterCalling { get; set; }
        public Nullable<short> EmployeeStatus { get; set; }
        public string StatusRefrenceCode { get; set; }
        public string DayStatus_Code { get; set; }
        public string Activity { get; set; }
        public string Location_Code { get; set; }
        public string TM_Location_Code { get; set; }
        public Nullable<short> Working_System { get; set; }
        public string Shift_Code { get; set; }
        public string Comments { get; set; }
        public Nullable<double> ProductivityQtyValue { get; set; }
        public Nullable<double> DamagedQtyValue { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
    }
}