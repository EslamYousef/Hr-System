using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.SetupPayroll
{
    [Table("GL_AccountSetup")]
    public class GL_AccountSetup
    {
        [Key]
        public int ID { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public Nullable<int> AccountType { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
        public string Account { get; set; }
    }
}