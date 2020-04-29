using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models.Application
{
    public class Hiring_Check_List
    {

        [Key]
        public int ID { get; set; }
        public bool Select { get; set; }       
        [Display(Name = "Item Code")]
        public string Item_Code { get; set; }
        [Display(Name = "Item Desc")]
        public string Item_Desc { get; set; }
        public virtual List<Check_List_Items> Check_List_Items { get; set; }

        [Display(Name = "Applicant Id")]
        public int ApplicantId { get; set; }
        public virtual Application Application { get; set; }
    }
}