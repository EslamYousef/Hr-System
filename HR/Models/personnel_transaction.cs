using HR.Models.Infra;
using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class personnel_transaction
    {
        public int ID { get; set; }
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Number { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime transaction_date { get; set; } = Convert.ToDateTime("01/01/2020").Date;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Effective_date { get; set; } = Convert.ToDateTime("01/01/2020").Date;
        public transaction_type Transaction_type { get; set; }
        public virtual Employee_Profile Employee { get; set; }
        public virtual status status { get; set; }
        public check_status check_status { get; set; }
        public string date { get; set; }
        public string ss { get; set; }
        public int statID { get; set; }

        public string name_state { get; set; }
        public string name_type { get; set; }
        /////////////////
        ////////////////
        ///////////////
        [Display(Name = "Position Transaction no")]
        public string Position_Transaction_number { get; set; }
       // [Display(Name = "Position transaction no.")]
        //public string Position_transaction_no { get; set; }
        [Display(Name = "Position transaction")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Position_transaction { get; set; } = Convert.ToDateTime("1/1/2020").Date;
        public Transaction_Type Transaction_Type_ { get; set; } = Transaction_Type.Re_Hire;
        public Fixed_basic_salary_by Fixed_basic_salary_by { get; set; } = Fixed_basic_salary_by.In_House;
        [Display(Name = "Resolution number")]
        public string Resolution_number { get; set; }
        [Display(Name = "Resolution date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Resolution_date { get; set; } = Convert.ToDateTime("1/1/2020").Date;
        [Display(Name = "Activity number")]
        public string Activity_number { get; set; }
        [Display(Name = "Memo number")]
        public string Memo_number { get; set; }
        [Display(Name = "Memo date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Memo_date { get; set; } = Convert.ToDateTime("1/1/2020").Date;
        [Display(Name = "Recommended by")]
        public string Recommended_by { get; set; }
        [Display(Name = "Approved by")]
        public string Approved_by { get; set; }
        [Display(Name = "Approved date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Approved_date { get; set; } = Convert.ToDateTime("1/1/2020").Date;
        ///////////
        //////////

        // [Required]
        // [Display(Name = "Employee No.")]
        // public string Employee_ProfileId { get; set; }
        //[Required]
        // [Display(Name = "Position profile No.")]
        //  public string Code { get; set; }
        //[Display(Name = "Primary Position")]
        //public bool Primary_Position { get; set; }
        //[Display(Name = "Job desc")]
        public string job_descId { get; set; }
        [Display(Name = "Slot desc")]
        public string SlotdescId { get; set; }
        public virtual job_title_cards job_title_cards { get; set; }
        [Display(Name = "Default location desc")]
        public string Default_location_descId { get; set; }
        [Display(Name = "Location desc")]
        public string Location_descId { get; set; }
        public virtual work_location work_location { get; set; }

        [Display(Name = "Job level desc")]
        public string Job_level_gradeId { get; set; }
        public virtual Job_level_grade Job_level_grade { get; set; }
        public virtual job_level_setup job_level_setup { get; set; }

        [Display(Name = "Unit desc")]
        public string Organization_ChartId { get; set; }
        public virtual Organization_Chart Organization_Chart { get; set; }

        [Display(Name = "From date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime From_date { get; set; } = Convert.ToDateTime("1/1/2020").Date;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "To date")]
        public DateTime To_date { get; set; } = Convert.ToDateTime("1/1/2020").Date;
        public int Years { get; set; }
        public int Months { get; set; }
        [Display(Name = "End of service date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]

        public DateTime End_of_service_date { get; set; } = Convert.ToDateTime("1/1/2020").Date;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Last working date")]
        public DateTime Last_working_date { get; set; } = Convert.ToDateTime("1/1/2020").Date;
        public string Commnets { get; set; }
        public working_system working_system { get; set; } = working_system.Day;
        public Position_status Position_status { get; set; } = Position_status.Active;
        public EOS_type EOS_reasons { get; set; } = EOS_type.On_poinsor;

        public int? default_cost_center_id { get; set; }
        public int? cost_center_id { get; set; }
        public int? default_shift_id { get; set; }
        public int? shift_id { get; set; }

    }
}