using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace HR.Models.ViewModel
{
    public class Report_RecuirtmentVM
    {
        public string name { get; set; }
        [Display(Name = "job setup")]
        public string Full { get; set; }
        public string Full_Name { get; set; }
        public string code { get; set; }
        //public string parent { get; set; }
        public string nationality { get; set; }
        public string Religion { get; set; }
        //public string parmanet { get; set; }
        //public string validity { get; set; }
        //public string gender { get; set; }
        public Gender Gender { get; set; }
        public ID_type ID_type { get; set; }
        public Blood_group Blood_group { get; set; }
        public Marital_Status Marital_Status { get; set; }
        //public int numSlot { get; set; }
        //public int fromAge { get; set; }
        //public int toAge { get; set; }

        public string List_Display { get; set; }

    }
}