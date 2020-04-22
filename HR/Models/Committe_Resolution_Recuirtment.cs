using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
using HR.Models.All_Table_Commitee_Resolution;

namespace HR.Models
{
    public class Committe_Resolution_Recuirtment
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Committe Resolution No.")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        [Display(Name = "Committe Usage")]
        public Committe_Usage Committe_Usage { get; set; } = Committe_Usage.Personnel;
        [Display(Name = "Committe Location")]
        public string Committe_Location { get; set; }
        [Display(Name = "Description Alternative")]
        public string Description_Alternative { get; set; }
        [Display(Name = "Committe Resolution Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Committe_Resolution_Date { get; set; }
        [Display(Name = "Committe Year")]
        public int Committe_Year { get; set; }
        [Display(Name = "Committe Resolution Status")]
        public Committe_Resolution_Status Committe_Resolution_Status { get; set; } = Committe_Resolution_Status.Created;
        [Display(Name = "Committe Type")]
        public Committe_Type Committe_Type { get; set; } = Committe_Type.Official;
        [Display(Name = "Committe Conclusion")]
        public string Committe_Conclusion { get; set; }
        public virtual List<Append_Committe_Member> Append_Committe_Member { get; set; }
        public virtual status status { get; set; }
        public check_status check_status { get; set; }

        public int statID { get; set; }
        public string name_state { get; set; }
        public virtual List<Commitee_Agenda> Commitee_Agenda { get; set; }
        public virtual List<Out_Organization> Out_Organization { get; set; }
        public virtual List<In_Organization> In_Organization { get; set; }
        public virtual List<Committe_Activities> Committe_Activities { get; set; }
        public virtual List<Linked_to_Testing> Linked_to_Testing { get; set; }

    }
}