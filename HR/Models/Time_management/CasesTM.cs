using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static HR.Models.Time_management.Enum_ex;

namespace HR.Models.Time_management
{
    public class CasesTM
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        //[Index(IsUnique = true)]
        public int number { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Description { get; set; }
        public string EXeprission {get;set;}
        public string Return_ex { get; set; }
        public Time_management_conditional_setup Time_management_conditional_setup { get; set; }
        public int? Time_management_conditional_setupID { get; set; }
    }
}