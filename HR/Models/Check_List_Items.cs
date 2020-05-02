using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
using HR.Models.Application;

namespace HR.Models
{
    public class Check_List_Items:BaseModel
    {
        public bool Required_on_application { get; set; }
        public virtual Application.Application Name_of_educational_qualification { get; set; }

        public string Name_of_educational_qualification_ns{ get; set; }
        //public bool Select { get; set; }
        //[Display(Name = "Applicant Id")]
        //public int ApplicantId { get; set; }
        //public virtual Application.Application Application { get; set; }
    }
}