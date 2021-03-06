﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HR.Models
{
    public class Employee_Recodes_Group
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Record Group Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Record_Group_Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Record Group Description")]
        public string Record_Group_Description { get; set; }
        [Display(Name = "Record Group Alternative")]
        public string Record_Group_Alternative { get; set; }       
        [Display(Name = "Linked to payroll")]
        public bool Linkedtopayroll { get; set; }  
        [Display(Name = "Linked to basic payment")]
        public bool Linkedtobasicpayment { get; set; }
        [Display(Name = "Linked to another payment")]
        public bool Linkedtoanotherpayment { get; set; }
    }
}