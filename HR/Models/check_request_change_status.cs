using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class check_request_change_status
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public virtual check_request_status check_Request { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string check_request_statusID { get; set; }
        public Employee_Profile Sign_by { get; set; }
        public int selected_signby { get; set; }
        public Employee_Profile Directed_to { get; set; }
        public int selected_directedto { get; set; }


    }
}