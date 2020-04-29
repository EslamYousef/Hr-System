using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models.Time_management
{
    public class Exit_permission_request
    {
        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Request_Number { get; set; }
        public DateTime Date { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }
        public int? Employee_ProfileID { get; set; }
        public virtual Exit_permission_type Exit_permission_type { get; set; }
        public int? Exit_permission_typeID { get; set; }
        public virtual Exit_Permission_Reason Exit_Permission_Reason { get; set; }
        public int? Exit_Permission_ReasonID { get; set; }
        public string Notes { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public virtual status status { get; set; }
        public int statusID { get; set; }
        public check_status check_status { get; set; }
    }
}