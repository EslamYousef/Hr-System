using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models.Time_management
{
    public class workpermissionrequest
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string number { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime date { get; set; }
        public string position_profile_num { get; set; }
        public work_permission_type work_permission_type { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }
        public int Employee_ProfileID { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public DateTime fromD { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public DateTime toD { get; set; }
        public TimeSpan fromT { get; set; }
        public TimeSpan toT { get; set; }
        public int days { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public string reason { get; set; }
        public bool accomplish { get; set; }
        public bool securty { get; set; }
        public string remark { get; set; }
        public bool meal { get; set; }
      public bool lunch { get; set; }
        public bool dinner { get; set; }
        public bool lunch_basket { get; set; }
        public status status { get; set; }
        public int statusID { get; set; }
        public check_status check_status { get; set; }

    }
}