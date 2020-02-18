using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models
{
    public class Check_List_Item_Groups
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Group Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Group_Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Description Group")]
        public string Description_Group { get; set; }
        [Display(Name = "Description Alternative")]
        public string Description_Alternative { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "The Purpose")]
        public The_Purpose The_Purpose { get; set; }
        public virtual List<Check_Lists_Items> check_items { get; set; }
    }
}