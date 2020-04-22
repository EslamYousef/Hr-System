using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class Applicant_VM
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }
        public string SurNameEn { get; set; }
        public string SurNameAr { get; set; }

        public string Code { get; set; }
        public int ApplicantId { get; set; }
        public Application.Application Application { get; set; }
    }
}