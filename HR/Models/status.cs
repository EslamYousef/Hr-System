using HR.Models.Infra;
using System;
using System.Collections.Generic;
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
        public DateTime created_bydate { get; set; }
        public DateTime report_as_ready_bydate { get; set; }
        public DateTime approved_bydate { get; set; }
        public DateTime Rejected_bydate { get; set; }
        public DateTime cancaled_bydate { get; set; }
        public Infra.Type Type {get;set;}
    }
}