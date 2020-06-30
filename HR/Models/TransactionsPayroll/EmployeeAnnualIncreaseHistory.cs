using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;


namespace HR.Models.TransactionsPayroll
{
    [Table("EmployeeAnnualIncreaseHistory")]
    public class EmployeeAnnualIncreaseHistory
    {
        [Key]
        public int ID { get; set; }
        public string Employee_Code { get; set; }
        public short Year { get; set; }
        public short Month { get; set; }
        public Nullable<double> AllowncePercentage { get; set; }
        public Nullable<double> AllownceAmount { get; set; }
        public Nullable<double> OldSalary { get; set; }
        public Nullable<double> NewSalary { get; set; }
        public string Notes { get; set; }
        public int IncreaseType { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
    }
}