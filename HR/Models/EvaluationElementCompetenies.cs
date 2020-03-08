using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class EvaluationElementCompetenies:BaseModel
    {
        public virtual List<Evalution_and_competencies> Evalution_and_competencies { get; set; }
    }
}