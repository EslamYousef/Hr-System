using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.CardPayroll
{
    [Table("Employee_Financial_Contract_Detail")]
    public class Employee_Financial_Contract_Detail
    {
        [Key]
        public int ID { get; set; }
        public string Contract_Number { get; set; }
        public string SalaryCodeID { get; set; }
        public Nullable<double> SalaryCodeValue { get; set; }
        public string ExtendedFields_Code { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
        public string Salarycodedescription { get; set; }
        public string Type { get; set; }
        public string ValueType { get; set; }
        public string ExtendedFields_Desc { get; set; }

    }
}