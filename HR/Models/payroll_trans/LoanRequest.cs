using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models.payroll_trans
{
    [Table("LoanRequest")]
    public class LoanRequest
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string LoanRequestNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public string EmployeeID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public string LoanTypeCode { get; set; }
        public Nullable<int> RequestStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public string ReportAsReadyBy { get; set; }
        public Nullable<System.DateTime> ReportAsReadyDate { get; set; }

        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }

        public string CanceledBy { get; set; }
        public Nullable<System.DateTime> CanceledDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]

        public Nullable<double> LoanAmount { get; set; }
        public Nullable<double> LoanInstallmentAmount { get; set; }
        public Nullable<double> NumberOfInstallment { get; set; }
        public Nullable<double> TotalPaidAmount { get; set; }
        public Nullable<double> TotalRemainingAmount { get; set; }
        public Nullable<int> NumberOfDeductedInstallments { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }

        public string TransferredTo { get; set; }
        public string TransferTransactionNumber { get; set; }
        public string TransferNotes { get; set; }

        public string Guarantor1 { get; set; }
        public string Guarantor2 { get; set; }


        public string LoanRequestNote { get; set; }

        public string Company_ID { get; set; }


        public string Created_By { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }


        public int RowIndx { get; set; }
        public string CreatedComment { get; set; }
        public string ReportAsReadyComment { get; set; }
        public string ApprovedComment { get; set; }
        public string CanceledComment { get; set; }
        public string RejectedBy { get; set; }
        public Nullable<System.DateTime> RejectedDate { get; set; }
        public string Rejected_Comment { get; set; }
        public string ClosedBy { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public string ClosedComment { get; set; }


        public Nullable<short> WebRequestStatus { get; set; }
        public string ManagerID { get; set; }
        public string ManagerNotes { get; set; }
        public string HRNotes { get; set; }
        public string GMNotes { get; set; }

        ///tarek
        public string check_status { get; set; }
        public string emp_name { get; set; }

        public virtual status status { get; set; }
        public int statusID { get;set;}
    }
}