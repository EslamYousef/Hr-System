using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Questions_Performance
    {
        public int id { get; set; }
        public int PerformanceEvaluationGroupID { get; set; }
        public int EvaluationQuestionsandanswersID { get; set; }
        public PerformanceEvaluationGroup PerformanceEvaluationGroup { get; set; }
        public EvaluationQuestionsandanswers EvaluationQuestionsandanswers { get; set; }
    }
}