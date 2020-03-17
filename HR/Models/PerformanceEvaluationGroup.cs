using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class PerformanceEvaluationGroup:BaseModel
    {
        public virtual List<PerformanceEvaluationGroupEvaluationElements> PerformanceEvaluationGroupEvaluationElements { get; set; }
      
        public virtual List<Questions_Performance> EvaluationQuestionsandanswers { get; set; }
        public virtual List<per_emp> per_emp { get; set; }


    }
}