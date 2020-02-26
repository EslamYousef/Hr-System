using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Evalution_and_competencies
    {
        public int id { get; set; }
        [Display(Name = "Default Degree")]
        public double Default_degree { get; set; } = 0.0;
      


        /// <summary>
        /// ////Relations
        /// </summary>
        public int EvaluationElementCompeteniesID { get; set; }
        public int EvaluationElementsID { get; set; }


        public virtual EvaluationElements EvaluationElements { get; set; }
        public virtual EvaluationElementCompetenies Competition { get; set; }
      

    }
}