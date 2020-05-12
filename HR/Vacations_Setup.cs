//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HR
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vacations_Setup
    {
        public int ID { get; set; }
        public string LeaveTypeCode { get; set; }
        public string LeaveTypeNameEnglish { get; set; }
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
        public int RenewBalanceevery { get; set; }
        public int TimesPerLife { get; set; }
        public int MaxCasualDays { get; set; }
        public int MaxContinousDays { get; set; }
        public int TotalDaysPerLife { get; set; }
        public int MaxDaysToTransfer { get; set; }
        public int MaxYearsToTransfer { get; set; }
        public int MaximumDaysContinous { get; set; }
        public int MaximumDaysPerMonth { get; set; }
        public int TakenAfterEmploymentDate { get; set; }
        public int LeavesType { get; set; }
        public int ShiftdaystatussetupId { get; set; }
    
        public virtual Shift_day_status_setup Shift_day_status_setup { get; set; }
    }
}