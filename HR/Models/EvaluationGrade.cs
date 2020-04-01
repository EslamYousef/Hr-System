using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class EvaluationGrade:BaseModel
    {
        public double FromScore { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public double ToScore { get; set; }
        [Display(Name = "Decision Type")]
        public Decisiontype Decision_Type { get; set; }


        public virtual List<Evalu_Element_Tran> Evalu_Element_Tran { get; set; }
        //public virtual List<EvaluationTransaction> EvaluationTransaction { get; set; }
    }
}