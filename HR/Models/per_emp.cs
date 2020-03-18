using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class per_emp
    {
        public int ID { get; set; }
        public int? PerformanceEvaluationGroupID { get; set; }
        public int? Employee_ProfileID { get; set; }
        public virtual EvaluationPlan PerformanceEvaluationGroup { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }
    }
}