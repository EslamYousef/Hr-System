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
        public string Code { get; set; }
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



        /// <summary>
        /// /
        public double H_degree { get; set; } = 0;
        public double M_degree { get; set; } = 0;
        public double average { get; set; } = 0;
        public double final { get; set; } = 0;
        public int num { get; set; } = 0;
        public double def { get; set; } = 0;
        //public virtual EvaluationGrade EvaluationGrade { get; set; }
        //public int EvaluationGradeID { get; set; }

        public string Desc_grade { get; set; }

        public string total { get; set; }

        public virtual PerformanceEvaluationGroup PerformanceEvaluationGroup { get; set; }
        public int PerformanceEvaluationGroupID { get; set; }

        ////////
        public virtual List<Evalu_Element_Tran> Evalu_Element_Tran { get; set; }
        public virtual List<A_Q> A_Q { get; set; }
        public virtual List<obje_eval_tran> obje_eval_tran { get; set; }
        public virtual List<skill_eval> skill_eval { get; set; }

    }


    public class Evalu_Element_Tran
    {
        public int ID { get; set; }

        public double H_degree { get; set; }
        public double M_degree { get; set; }
        public double average { get; set; }
        public double final { get; set; }

        public virtual EvaluationGrade EvaluationGrade { get; set; }
        public int? EvaluationGradeID { get; set; }
        public virtual EvaluationTransaction EvaluationTransaction { get; set; }
        public int EvaluationTransactionID { get; set; }
        public virtual EvaluationElements EvaluationElements { get; set; }
        public int EvaluationElementsID { get; set; }

    }
    public class A_Q
    {
        public int ID { get; set; }
        public string actual_answer { get; set; }
        public virtual EvaluationQuestionsandanswers EvaluationQuestionsandanswers { get; set; }
        public int EvaluationQuestionsandanswersID { get; set; }
        public virtual EvaluationTransaction EvaluationTransaction { get; set; }
        public int EvaluationTransactionID { get; set; }
    }
    public class obje_eval_tran
    {
        public int ID { get; set; }
        public double H_degree { get; set; }
        public double M_degree { get; set; }
        public double average { get; set; }
        public double final { get; set; }
        public DateTime Date { get; set; }
        public virtual EvaluationObjectives EvaluationObjectives { get; set; }
        public int EvaluationObjectivesID { get; set; }


        public virtual EvaluationTransaction EvaluationTransaction { get; set; }
        public int EvaluationTransactionID { get; set; }

    }
    public class skill_eval
    {
        public int id { get; set; }
        public double J_rate { get; set; }
        public double em_rate { get; set; }
        public string GAP { get; set; }
        public virtual Skill Skill { get; set; }
        public int SkillID { get; set; }
        public EvaluationTransaction EvaluationTransaction { get; set; }
        public int EvaluationTransactionID { get; set; }
    }
}