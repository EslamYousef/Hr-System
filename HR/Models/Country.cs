using HR.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Models
{
    public class Country///البلد
    {
        //[Display(Name = "FullName", ResourceType = typeof(Resource1))]
        //public string NAME { get; set; }
        //[Display(Name = "Age", ResourceType = typeof(Resource1))]
        //public int Age { get; set; }
        //[Display(Name = "Address", ResourceType = typeof(Resource1))]
        //public string Address { get; set; }
        //[Display(Name = "code", ResourceType = typeof(Resource1))]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public  int  ID { get; set; }
        [Required]
        [Display(Name = "Code")]
       // [Index(IsUnique = true)]
        public string Code { get; set; }
        //[Display(Name = "Name", ResourceType = typeof(Resource1))]
        [Required]
        [Display(Name = "Name")]
        public virtual string Name { get; set; }
        //[Display(Name = "Description", ResourceType = typeof(Resource1))] 
        [Display(Name = "Description")]
        public string Description { get; set; }

        //public virtual List<Area> Area { get; set; }
    }
}