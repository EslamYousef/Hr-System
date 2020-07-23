using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
using HR.Models.Time_management;

namespace HR.Models
{
    public class Shiftscheduletemplate
    {
        [Key]
        public int ID { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public Shift_setup Shift_setup { get; set; }
        public int Shift_setupID { get; set; }
        public Shiftdaystatus Shiftdaystatus { get; set; }
        public int ShiftdaystatusID { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime From_date { get; set; } = DateTime.Now;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime To_date { get; set; } = DateTime.Now;

        //public virtual Employee_Shift_schedule Employee_Shift_schedule { get; set; }
        public int Employee_Shift_scheduleID { get; set; }
        //[StringLength(50)]
        //[Index(IsUnique = true)]
        public string TemplateCode_Shifts { get; set; }
        public string TemplateDescription_Shifts { get; set; }
        public string TemplateAllternativeDescription_Shifts { get; set; }

    }
}