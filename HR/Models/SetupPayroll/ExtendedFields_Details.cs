using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.SetupPayroll
{
    [Table("ExtendedFields_Details")]
    public class ExtendedFields_Details
    {
        [Key]
        public int ID { get; set; }
        public string ExtendedFields_Code { get; set; }
        public string Detail_Code { get; set; }
        public string Detail_Desc { get; set; }
        public string Detail_AltDesc { get; set; }
        public Nullable<short> ValueType { get; set; }
        public Nullable<double> Value { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
    }
}