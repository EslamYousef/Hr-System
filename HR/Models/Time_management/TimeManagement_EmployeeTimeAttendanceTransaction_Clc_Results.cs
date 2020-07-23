using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models.Time_management
{
    [Table("TimeManagement_EmployeeTimeAttendanceTransaction_Clc_Results")]
    public class TimeManagement_EmployeeTimeAttendanceTransaction_Clc_Results
    {
        [Key]
        public int ID { get; set; }
        public System.DateTime AttendanceDate { get; set; }
        public string Employee_Code { get; set; }
        public int EffectiveMonth { get; set; }
        public int EffectiveYear { get; set; }
        public string SalaryCodeID { get; set; }
        public string DayStatus_Code { get; set; }
        public Nullable<double> Result { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
    }
}