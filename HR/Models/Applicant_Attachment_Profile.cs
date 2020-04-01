using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
using System.Web.Mvc;

namespace HR.Models
{
    public class Applicant_Attachment_Profile
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Applicant No.")]
        public int Employee_ProfileId { get; set; }
        public virtual Applicant_Profile Applicant_Profile { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Attachment profile No.")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        [Display(Name = "Is copy")]
        public bool Is_copy { get; set; }
        [Display(Name = "Is required")]
        public bool Is_required { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Document code")]
        public int DocumentsId { get; set; }
        public virtual Documents Documents { get; set; }
        [Display(Name = "Document group")]
        public string DocumentGroup { get; set; }
        [Display(Name = "Issued place")]
        public string Issued_place { get; set; }
        [Display(Name = "Issued date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Issue_date { get; set; }
        [Display(Name = "Expiry date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Expiry_date { get; set; }
        [Display(Name = "Document description")]
        public string Document_description { get; set; }
        [Display(Name = "Reference number")]
        public string Reference_number { get; set; }
        [Display(Name = "Document number")]
        public string Document_number { get; set; }
        public string Comments { get; set; }
        public string Attachmentfile { get; set; }
        public Related_to Related_to { get; set; } = Related_to.Employee;
        public Document_status Document_status { get; set; } = Document_status.Not_delivered;
    }
}