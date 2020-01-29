using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class check_Request
    {
        public int ID { get; set; }
        [Required]
        public string Request_number { get; set; }
        [Required]
        public DateTime Request_date { get; set; }
        [Required]
        public string check_number { get; set; }
        [Required]
        public int month { get; set; }
        [Required]
        public int year { get; set; }
        [Required]
        public double amount { get; set; }
        [Required]
        public DateTime Check_Due_date { get; set; }
       
        public string Description { get; set; }
        public string Comment { get; set; }

        public virtual check_request_change_status check_request_change_status { get; set; }
        [Required]
        public string check_request_change_statusID { get; set; }
        public virtual Checktype Checktype { get; set; }
        [Required]
        public string ChecktypeID { get; set; }
        public string date { get; set; }

        //public status status { get; set; }
        //public check_status check_status { get; set; }
        //public string s { get; set; }
        //public string date_string { get; set; }
    }
}