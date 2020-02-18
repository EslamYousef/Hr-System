using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HR.Models
{
    public class Check_Lists_Items
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Check Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Check_Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Description { get; set; }
        [Display(Name = "Description Alternative")]
        public string Description_Alternative { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Is Mandatory")]
        public bool Is_Mandatory { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Check List Item Groups")]
        public string Check_List_Item_GroupsId { get; set; }
        public virtual Check_List_Item_Groups Check_List_Item_Groups { get; set; }

    }
}