using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class EvaluationTransaction
    {
        public int ID { get; set; }
        public string  Code { get; set; }
        public DateTime AppraisalDate { get; set; }
        public string planLine { get; set; }
        public DateTime FromeDate { get; set; }
        public DateTime ToDate { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }
        public int Employee_ProfileID { get; set; }
        public virtual EvaluationPlan EvaluationPlan { get; set; }
        public int EvaluationPlanID { get; set; }
        public status status { get; set; }
        public int statusID { get; set; }
        public check_status check_status { get; set; }

        public virtual List<groupevaluation_evaluation_transaction> groupevaluation_evaluation_transaction { get; set; }
        public virtual List<QuestionsANDAnswers_EvaluationTransaction> QuestionsANDAnswers_EvaluationTransaction { get; set; }


    }
}