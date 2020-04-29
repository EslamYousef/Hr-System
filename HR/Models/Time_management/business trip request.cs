using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models.Time_management
{
    public class business_trip_request
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string number { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime request_date { get; set; }
        public bool carry_important_documents { get; set; }
        public virtual Employee_Profile Employee_Profile { get; set; }
        public int Employee_ProfileID { get; set; }
        public virtual Business_Trip Business_Trip { get; set; }
        public int? Business_TripID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public DateTime go_date { get; set; }
        public virtual TimeSpan go_time { get; set; }
        public virtual TransportationMethod TransportationMethod { get; set; }
        public int TransportationMethodID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public DateTime return_date { get; set; }
        public TimeSpan return_time { get; set; }
        public int tionMethodID_return { get; set; }
        public double Total_rate { get; set; }
        public int days { get; set; }
        public int months { get; set; }
        public int years { get; set; }
        public string location_of_duty { get; set; }

        public string purpose_of_duty { get; set; }

        public string Remarks_of_duty { get; set; }
        public status status { get; set; }
        public int statusID { get; set; }
        public check_status check_status { get; set; }


    }
}