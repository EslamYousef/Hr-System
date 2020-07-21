using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    [Table("PayrollGeneralSetup")]
    public class PayrollGeneralSetup
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int SetupKey { get; set; }
        public string DefaultPayrollPeriod { get; set; }
        public string DefaultPayrollPeriod_des { get; set; }
        public string AccountNumberForNetSalary { get; set; }
        public string DefaultAccountNumberForNetPayment { get; set; }
        public string SalaryCodeID_BasicSalary { get; set; }
        public string SalaryCodeID_ExecludedAllwance { get; set; }
        public Nullable<int> GL_DistrbutionBehavior { get; set; }
        public Nullable<int> IntegrationType { get; set; }
        public Nullable<int> Length_Of_Segment { get; set; }
        public Nullable<int> Number_Of_Account_Segments { get; set; }
        public string Type_Code { get; set; }
        public Nullable<short> Rounding_Method { get; set; }
        public Nullable<double> Value { get; set; }
        public Nullable<bool> AllowToRounding { get; set; }
        public string SalaryCodeID_E { get; set; }
        public string SalaryCodeID_D { get; set; }
        public string SalaryCodeID_DeathDate { get; set; }
        public string Subscription_Code_DeathDate { get; set; }
        public Nullable<bool> Rest_On_The_First_Punishment { get; set; }
        public Nullable<bool> Rest_On_The_Last_Punishment { get; set; }







        public string ReportServerURL { get; set; }
        public Nullable<int> Segment_Number_Of_Costcenter { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
    }
}