using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Employee_records : BaseModel
    {
        public virtual Employee_Profile Employee_Profile { get; set; }
        public virtual Employee_Recodes_Group Employee_Recodes_Group { get; set; }
        [Display(Name = "Record date")]
        public DateTime Record_date { get; set; }
        [Display(Name = " Effictive date")]
        public DateTime Effictive_date { get; set; }
        [Display(Name = "Record expire date")]
        public DateTime Record_expire_date { get; set; }
        [Display(Name = "Record value")]
        public double record_value { get; set; }
        [Display(Name = "Record amount")]
        public double record_amount { get; set; }
        public virtual status status { get;set;}
        public check_status check_status { get; set; }

        public string sss { get; set; }
    }
}