using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HR.Models
{
    public class Medical_Medicine_Classfication
    {

        [Key]
        public int ID { get; set; }
        [Required]
        [Index(IsUnique = true)]
        public double Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}