﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Job_title_sub_class
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "JOB TYPE ALLWANCE PERCENTAGE")]
        public float JOB_TYPE_ALLWANCE_PERCENTAGE { get; set; }
        [Display(Name = "DEDICATED ALLWANCE VALUE")]
        public float Dedicated_ALLWANCE_VALUE { get; set; }
        [Display(Name = "Exchanging ALLWANCE VALUE")]
        public float Exchanging_ALLWANCE_VALUE { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Job title class")]
        public string Job_title_classId { get; set; }
        public virtual Job_title_class Job_title_class { get; set; }
        public virtual List<items_man_power> items_man_power { get; set; }

    }
}