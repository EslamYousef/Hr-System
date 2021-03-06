﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class EvaluationQuestionsandanswers
    {

        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Question")]
        public string Question { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Model Answer")]
        public string model_answer {get;set;}
        public virtual  List<Questions_Performance> Questions_Performance { get; set; }

        //public virtual List<QuestionsANDAnswers_EvaluationTransaction> QuestionsANDAnswers_EvaluationTransaction { get; set; }
        public virtual List<A_Q> A_Q { get; set; }

    }
}