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
        public double price { get; set; }
        public string Indication{ get; set; }
        [Display(Name = "Usual Dosage")]
    public string Usual_Dosage { get; set; }
public string Contraindication { get; set; }
        [Display(Name = "Precaution Warnings")]
        public string Precaution_Warnings{ get; set; }  
    public string Note { get; set; }
        [Display(Name = "Medical Medicine Classfication")]
        public string  Medical_Medicine_ClassficationId { get; set; }
        public virtual Medical_Medicine_Classfication Medical_Medicine_Classfication { get; set; }

    }
}