using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class PerformanceEvaluationGroupEvaluationElements
    {
        public int id { get; set; }
        public int PerformanceEvaluationGroupID { get; set; }
        public virtual PerformanceEvaluationGroup PerformanceEvaluationGroup { get;set;}
        public virtual EvaluationElements EvaluationElements { get; set; }
        public int EvaluationElementsID { get; set; }
    }
}