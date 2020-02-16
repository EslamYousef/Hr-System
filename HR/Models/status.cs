using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class status
    {
        public int ID { get; set; }
        public check_status statu { get; set; }
        public string created_by { get; set; }
        public string report_as_ready_by { get; set; }
        public string approved_by { get; set; }
        public string Rejected_by { get; set; }
        public string cancaled_by { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime created_bydate { get; set; } = Convert.ToDateTime("1/1/2020").Date;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime report_as_ready_bydate { get; set; } = Convert.ToDateTime("1/1/2020").Date;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime approved_bydate { get; set; } = Convert.ToDateTime("1/1/2020").Date;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Rejected_bydate { get; set; } = Convert.ToDateTime("1/1/2020").Date;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime cancaled_bydate { get; set; } = Convert.ToDateTime("1/1/2020").Date;
        public Infra.Type Type {get;set;}
    }
}