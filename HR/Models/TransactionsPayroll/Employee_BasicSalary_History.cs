using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;


namespace HR.Models.TransactionsPayroll
{
    [Table("Employee_BasicSalary_History")]
    public class Employee_BasicSalary_History
    {
        [Key]
        public int ID { get; set; }
        public string Employee_Code { get; set; }
        public short History_Year { get; set; }
        public short History_Month { get; set; }
        public Nullable<double> BasicSalary_A { get; set; }
        public Nullable<double> Allowances_Percentage { get; set; }
        public Nullable<double> Allowances_Amount { get; set; }
        public Nullable<double> BasicSalary { get; set; }
        public Nullable<double> Total_Included_Amount { get; set; }
        public Nullable<double> Total_excluded_Amount { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
    }
}