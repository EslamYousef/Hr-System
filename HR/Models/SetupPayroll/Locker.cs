﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.SetupPayroll
{
    [Table("Locker")]
    public class Locker
    {
        [Key]
        public int ID { get; set; }
        public string Locker_Code { get; set; }
        public string Locker_Desc { get; set; }
        public string Locker_AltDesc { get; set; }
        public string Company_ID { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public int RowIndx { get; set; }
    }
}