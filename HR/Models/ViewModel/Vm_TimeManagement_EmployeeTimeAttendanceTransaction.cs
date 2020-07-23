using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class Vm_TimeManagement_EmployeeTimeAttendanceTransaction
    {
        public string AttendDate { get; set; }
        public string week { get; set; }
        public TimeSpan? Start_time { get; set; }
        public TimeSpan? End_time { get; set; }
        public string worklocationcode { get; set; }
        public string worklocationDes { get; set; }
        public string LocationCode { get; set; }
        public string LocationDescription { get; set; }
        public string ShiftCode { get; set; }
        public string ShiftDescription { get; set; }
        public working_system working_system { get; set; } = working_system.Day;
        public EmployeeStatus EmployeeStatus { get; set; } = EmployeeStatus.OnDuty;
        public int EmployeeStat { get; set; }
        public int ID { get; set; }
    }
}