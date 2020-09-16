using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models.user
{
    public class notifications
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public type_field type_field { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Form { get; set; }
        public string Field { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public Action Action { get; set; }
        public DateTime? until { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string send_to_ID_user { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Subject { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Message { get; set; }

        public int? number { get; set; } = 0;
    }
    public enum Action
    {
        Create = 1,
        edit = 2,
        delete = 3,
        before_due_Date = 4,
        after_due_Date = 5
    }
 
    public enum type_field
    {

        form = 1,
        date_field =2,
        normal_field=3,
        
    }
}