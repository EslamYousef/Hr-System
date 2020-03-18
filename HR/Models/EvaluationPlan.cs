using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class EvaluationPlan:BaseModel
    {
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public int Year { get; set; } = 1900;
        public virtual EvaluationType EvaluationType { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public int EvaluationTypeID { get; set; }
        public int previous_apprisal_to_review { get; set; }
        public virtual List<PlaneSchedule> PlaneSchedule { get; set; }
        public virtual List<EvaluationTransaction> EvaluationTransaction { get; set; }
        public virtual List<per_emp> per_emp { get; set; }

    }
}