using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HR.Models.Infra;
namespace HR.Models.Vacations
{
    [Table("LeavesRequestMaster")]
    public class LeavesRequestMaster
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string SerialNo { get; set; }
        public int EmployeeID { get; set; }
        public int VacCode { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime DateFrom { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime DateTo { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> CurrentDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> ActualToDate { get; set; }
        public int EmpAlternative { get; set; }
        public string Reason { get; set; }
        public int Approved { get; set; }
        public int EmpApproved { get; set; }
        public string RequestTypeCode { get; set; }
        public Nullable<bool> ReturnVac { get; set; }
        public Nullable<bool> Settled { get; set; }
        public bool CasualLeave { get; set; }
        public Nullable<long> PointID { get; set; }
        public Nullable<bool> WithTicket { get; set; }
        public Nullable<bool> WithExitReEntry { get; set; }
        public Nullable<bool> ISWorkFlow { get; set; }
        public string AttachedFile { get; set; }
        public Nullable<double> BalanceDays { get; set; }
        public Nullable<double> TotalDays { get; set; }
        public Nullable<double> RemainDays { get; set; }
        public string Serial_LB { get; set; }
        public Nullable<bool> HalfDay { get; set; }
        public Nullable<bool> QuarterDay { get; set; }
        public string QuarterDayCode { get; set; }
        public Nullable<bool> ShiftFirstHalf { get; set; }
        public string Company_ID { get; set; }
        public int RowIndx { get; set; }
        public Nullable<int> RequestStatus { get; set; }
        public Nullable<short> WebRequestStatus { get; set; }
        public string Created_By { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> Modified_Date { get; set; }
        public string ReportAsReadyBy { get; set; }
        public string ReportAsReadyDate { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public string CanceledBy { get; set; }
        public string CanceledDate { get; set; }
        public string RejectedDate { get; set; }
        public string RejectedBy { get; set; }
        public string CanceledComment { get; set; }
        public string RejectedComment { get; set; }
        public string ManagerID { get; set; }
        public Nullable<bool> AltEmpAgreed { get; set; }
        public string AltEmpDisagreeReason { get; set; }
        public string ManagerNotes { get; set; }
        public string HRNotes { get; set; }
        public string GMNotes { get; set; }
        public virtual status status { get; set; }
        public check_status check_status { get; set; }

        public int statID { get; set; }
        public string name_state { get; set; }

        public int year { get; set; }
        public bool LastOrder { get; set; }
    }
}