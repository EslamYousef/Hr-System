using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.SetupPayroll
{
    [Table("PayrollPeriodSetup")]
    public class PayrollPeriodSetup
    {
        [Key]
        public int ID { get; set; }
        public string PeriodCode { get; set; }
        public string PeriodDesc { get; set; }
        public string PeriodAltDesc { get; set; }
        public Nullable<int> PeriodType { get; set; }
        public Nullable<int> NumberOfDays { get; set; }
        public Nullable<int> StartPayMonthFromDay { get; set; }
        public Nullable<int> EndPayMonthToDay { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
    }
}