﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.CardPayroll
{
    [Table("FinancialContract_ExtendedFieldsDetails")]
    public class FinancialContract_ExtendedFieldsDetails
    {
        [Key]
        public int ID { get; set; }
        public string Contract_Number { get; set; }
        public string SalaryCodeID { get; set; }
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
        public virtual Employee_Financial_Contract_Header Employee_Financial_Contract_Header { get; set; }

    }
}