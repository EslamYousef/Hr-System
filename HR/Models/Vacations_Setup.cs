using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;

namespace HR.Models
{
    public class Vacations_Setup
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Leave Type Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string LeaveTypeCode { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Leave Type Name(English)")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string LeaveTypeNameEnglish { get; set; }
        [Display(Name = "Leave Type Name (Arabic)")]
        public string LeaveTypeNameArabic { get; set; }
        public bool IncludeWeekEnd { get; set; }
        public bool FemaleOnly { get; set; }
        public bool AcceptNegative { get; set; }
        public bool IncludeHoliday { get; set; }
        public bool Show0Balance { get; set; }
        public bool UnlimitedBalance { get; set; }
        public bool Proportional { get; set; }
        public bool AbleToCash { get; set; }
        public bool TrackMonthlyAccrualBalance { get; set; }
        public bool RenewBalance { get; set; }
        public int RenewBalanceevery { get; set; } = 0;
        public int TimesPerLife { get; set; } = 0;
        public int MaxCasualDays { get; set; } = 0;
        public int MaxContinousDays { get; set; } = 0;
        public int TotalDaysPerLife { get; set; } = 0;
        public int MaxDaysToTransfer { get; set; } = 0;
        public int MaxYearsToTransfer { get; set; } = 0;
        public int MaximumDaysContinous { get; set; } = 0;
        public int MaximumDaysPerMonth { get; set; } = 0;
        public int TakenAfterEmploymentDate { get; set; } = 0;
        public LeavesType LeavesType { get; set; } 
        [Display(Name = "TM Day status")]
        [ForeignKey("Shift_day_status_setup")]
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        public int ShiftdaystatussetupId { get; set; }
        public virtual Shift_day_status_setup Shift_day_status_setup { get; set; }

        public int TestFormula { get; set; } = 0;
        public int? LeavesPayItemsCashDays { get; set; }
        public int? PRWorkDayPayCode { get; set; }
        public int? EOSCashmandayAmount { get; set; }
          
    }
}