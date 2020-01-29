using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Test:BaseModel
    {
        public Test_type Test_type { get; set; }
        public double Pass_mark { get; set; }
        public double Full_mark { get; set; }
        public virtual List<Questions> Questions { get; set; }
        public List<int> QuestionsID { get; set; }

    }
}