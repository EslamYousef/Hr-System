using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Document_Group
    {
        [Key]
        public int ID { get; set; }
        [Required]
        //[Index(IsUnique = true)]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}