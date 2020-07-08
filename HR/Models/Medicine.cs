using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Medicine:BaseModel
    {
        [Display(Name = "Is Foreign")]
        public bool Is_Foreign { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public double price { get; set; }
        public string Indication{ get; set; }
        [Display(Name = "Usual Dosage")]
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string Usual_Dosage { get; set; }
public string Contraindication { get; set; }
        [Display(Name = "Precaution Warnings")]
        public string Precaution_Warnings{ get; set; }  
    public string Note { get; set; }
        [Display(Name = "Medical Medicine Classfication")]
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public string  Medical_Medicine_ClassficationId { get; set; }
        public virtual Medical_Medicine_Classfication Medical_Medicine_Classfication { get; set; }

    }
}