﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Qualification_Major
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Name of educational qualification")]
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public int Name_of_educational_qualificationid { get; set; }
        [Display(Name = "Educate Title")]
        public int Educate_Titleid { get; set; }
        public virtual Name_of_educational_qualification Name_of_educational_qualification { get; set; }
    }
}