using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models
{
    public class Test:BaseModel
    {
        public Test_type Test_type { get; set; } = Test_type.Test;
        [Display(Name = "Pass mark")]
        public double Pass_mark { get; set; }
        [Display(Name = "Full mark")]
        public double Full_mark { get; set; }
        public virtual List<Questions> Questions { get; set; }
        public List<int> QuestionsID { get; set; }
        [Display(Name = "Job desc")]
        public string job_descId { get; set; }
        public virtual job_title_cards job_title_cards { get; set; }


    }
}