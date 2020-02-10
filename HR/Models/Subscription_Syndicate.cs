using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Subscription_Syndicate
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Server Legatees ")]
        public bool Server_Legatees { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Subscription Code")]
        public string Subscription_Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Subscription Description")]
        public string Subscription_Description { get; set; }
        [Display(Name = "Subscription Alternative Description")]
        public string Subscription_Alternative_Description { get; set; }
        [Display(Name = "Contact Detail")]
        public string Contact_Detail { get; set; }
        [Display(Name = "Default Subscription Fees")]
        public int Default_Subscription_Fees { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Deduction Period")]
        public Deduction_Period Deduction_Period { get; set; }
        [Required]
        public Infra.Type Type { get; set; }
        [Display(Name = "Phone 1")]
        public string Phone_1 { get; set; }
        [Display(Name = "Phone 2")]
        public string Phone_2 { get; set; }
        public string Fax { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Contact Methods Code")]
        public string Contact_methodsId { get; set; }
        public virtual Contact_methods Contact_methods { get; set; }
    }
}