﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class EvaluationElements:BaseModel
    {
        [Required]
        [Display(Name = "Default Degree")]
        public double defaultDegree { get; set; }

        public virtual List<Evalution_and_competencies> Evalution_and_competencies { get; set; }
      


    }
}