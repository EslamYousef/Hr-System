using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Models
{
    public class Medical_Service_Classification
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Index(IsUnique = true)]
        [Display(Name = "Classification Code")]
        public double Classification_Code { get; set; }
        [Required]
        public string Description { get; set; }
        public string TDescription { get; set; }

        [Display(Name = "Group Code")]
        [Required]
        public string Group_Medical_Service_GroupId { get; set; }
        public virtual Medical_Service_Group Medical_Service_Group { get; set; }

        public static implicit operator Medical_Service_Classification(Medical_Medicine_Classfication v)
        {
            throw new NotImplementedException();
        }
    }
}