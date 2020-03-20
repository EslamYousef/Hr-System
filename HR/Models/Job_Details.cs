
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Job_Details
    {
        public int ID { get; set; }
        //////////Qulifications/////////
        public virtual List<qulification_job> qulification_job { get; set; }
        public  List<string> qulification_jobID { get; set; }
        ///////////////////////////////
        /////////Skills///////////////
        public virtual List<skills_job> skill { get; set; }
        public List<string> skillID { get; set; }
        public virtual List<Risks> Risks { get; set; }
        public List<string> Risksid { get; set; }
        public virtual List<Responsibilities> Responsibilities { get; set; }
        public List<string> responsibilitiesID { get; set; }

        public virtual List<Requirments> Requirments { get; set; }
        public List<string> requirmentID { get; set; }
        public virtual List<exper_jobdetails> exper_jobdetails { get; set; }
        public virtual List<mental> mental { get; set; }
        public virtual List<Required_Licenses> Required_Licenses { get; set; }


    }
    public class skills_job
    {
        public int ID { get; set; }
        public virtual Skill skill { get; set; }
        public string skillid { get; set; }
        public bool required { get; set; }
    }
    public class qulification_job
    {
        public int ID { get; set; }
        public string Name_of_educational_qualificationID { get; set; }
        public virtual Name_of_educational_qualification Name_of_educational_qualification { get; set; }
        public string GradeEducateID { get; set; }
        public virtual GradeEducate GradeEducate { get; set; }
        public string Qualification_MajorID { get; set; }
        public virtual Qualification_Major Qualification_Major { get; set; }
        public bool required { get; set; }

    }
    public class mental
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Alt_Description { get; set; }
        public virtual Job_Details Job_Details { get; set; }
        public int Job_DetailsID { get; set; }
    }
    public class Required_Licenses
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Alt_Description { get; set; }
        public virtual Job_Details Job_Details { get; set; }
        public int Job_DetailsID { get; set; }
    }

}