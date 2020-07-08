using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;


namespace HR.Models
{
    public class ShiftdaystatusDetials
    {
        [Key]
        public int ID { get; set; }
        public string ShiftdaystatusId { get; set; }
        public string PayrollItemCode { get; set; }
        public string PayrollItemDescription{ get; set; }
        public Nullable<int> DefaultValue { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
        public virtual Shiftdaystatus Shiftdaystatus { get; set; }
    }
}