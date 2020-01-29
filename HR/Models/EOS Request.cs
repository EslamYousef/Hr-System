using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class EOS_Request
    {
        public int ID { get; set; }
        [Required]
        public string number { get; set; }
        public DateTime Requset_date { get; set; }
        public virtual Employee_Profile Employee { get; set; }
        public EOS_type EOS_type { get; set; }
        public Notice_period_type Notice_period_type { get; set; }
        public double Notice_period { get; set; }
        public DateTime Date_of_EOS { get; set; }
        public DateTime last_work_day_before_request { get; set; }
        public DateTime last_Date_of_work_after_notice_period { get; set; }
        public bool are_the_employee_has_a_loan_or_advanced { get; set; }
        public bool are_the_settlement_transferred_to_payroll { get; set; } 
      
        public DateTime date_of_eos_interview { get; set; }
        public DateTime date_of_settlement { get; set; }
     
        public string note { get; set; }
        public virtual status status { get; set; }
        public check_status check_status { get; set; }
        public virtual List<Answer_EOS> Answer_EOS_interview { get; set; }
        public virtual EOS_Interview_Questions_Groups EOS_group { get; set; }
        
        /// ///////////////////////////////
        public virtual List<check_list_EOS> Check_List { get; set; }
        public virtual Check_List_Item_Groups Check_List_Item_Groups { get; set; }


        public string sss { get; set; }
        public string req_date { get; set; }
        public string eos_Date { get; set; }
    }
}