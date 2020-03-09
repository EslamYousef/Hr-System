using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class PerformanceEvaluationGroup:BaseModel
    {
        public virtual List<EvaluationElements> EvaluationElements { get; set; }
        public virtual List<EvaluationQuestionsandanswers> EvaluationQuestionsandanswers { get; set; }
    }
}