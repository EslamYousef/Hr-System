using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HR.Models
{
    public class EOS_Interview_Questions_Groups
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Questions Group Code")]
        public string Questions_Group_Code { get; set; }
        [Required]
        [Display(Name = "Description of")]
        public string Description_of { get; set; }
        [Display(Name = "Description Alternative")]
        public string Description_Alternative { get; set; }


        public virtual List<Definition_of_EOS_Interview_Questions> questions { get; set; }
    }
}