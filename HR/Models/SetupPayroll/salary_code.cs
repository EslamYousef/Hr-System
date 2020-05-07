using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models.SetupPayroll
{
    [Table("SalaryCodes")]
    public class salary_code
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string SalaryCodeID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public string SalaryCodeDesc { get; set; }
        public string SalaryCodeAltDesc { get; set; }
        public Nullable<int> CodeGroupType { get; set; }
        public Nullable<int> CodeValueType { get; set; }
        public string FormulaCode { get; set; }  //////////////////////////// /// /////////////
        public Nullable<int> SourceEntry { get; set; }
        public Nullable<short> Costcenter_Type { get; set; }
        public string Type_Code { get; set; }
        public Nullable<bool> EnableExtendedFields { get; set; }
        public string ExtendedFields_Code { get; set; }



        public Nullable<bool> PrintableInPayslip { get; set; }
        public Nullable<bool> AffectToNetAmount { get; set; }
        public Nullable<bool> AllowToUseInHRModules { get; set; }
        public string Description { get; set; }
        public Nullable<int> SortingIndex { get; set; }
        public Nullable<int> FrequencyPerPeriod { get; set; }
        public Nullable<bool> LinkedWithAccumulators { get; set; }
        public string Subscription_Code { get; set; }
        public Nullable<bool> ApplayRangeConstrains { get; set; }
        public Nullable<decimal> MinimumAmount { get; set; }
        public Nullable<decimal> MidpointAmount { get; set; }
        public Nullable<decimal> MaximumAmount { get; set; }
        public Nullable<bool> AllowToPostingItemToGL { get; set; }
        public string DebitAccount { get; set; }
        public string CreditAccount { get; set; }
        


        public Nullable<int> EligibilityType { get; set; }
        public string EligibilityMatrixCode { get; set; }
        public string RetroactiveCodeID { get; set; }
        
       
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
       
    
      
        
    }
}