using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using HR.Resource;

namespace HR.Models
{
    public class Template
    {
        [Key]
        public int ID { get; set; }
        public string TemplateCode{ get; set; }
        public string TemplateDescription { get; set; }
        public string TemplateAllternativeDescription { get; set; }
        public int Employee_Shift_scheduleID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime From_date { get; set; } = DateTime.Now;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime To_date { get; set; } = DateTime.Now;
    }
}