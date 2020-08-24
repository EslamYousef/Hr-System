using HR.Models;
using HR.Models.Infra;
using HR.Models.payroll_trans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.payroll_tran1
{
    public class Loan_transferController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        [Authorize(Roles = "Admin,payroll,payrollTransaction,loan transaction")]
        // GET: Loan_transfer
        public ActionResult Index()
        {
            var model = dbcontext.LoanTransfer.ToList();
            return View(model);
        }
        [Authorize(Roles = "Admin,payroll,payrollTransaction,loan transaction")]
        public ActionResult Loan_transfer_entry(int id)
        {
            try
            {
                var loan = dbcontext.LoanRequest.FirstOrDefault(m => m.ID == id);
                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.loan_type = dbcontext.LoanInAdvanceSetup.ToList().Select(m => new { Code = m.LoanTypeCode + "--[" + m.LoanTypeDesc + ']', ID = m.ID });
                //////////////////////////////////
                var new_trans = new LoanTransfer();
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                var All_requests = dbcontext.LoanRequest.ToList();
                if (All_requests.Count() == 0)
                {
                    new_trans.NewLoanRequestNumber = stru + "1";
                }
                else
                {
                    new_trans.NewLoanRequestNumber = stru + (All_requests.LastOrDefault().ID + 1).ToString();
                }
                new_trans.LoanRequestNumber = loan.LoanRequestNumber;
                new_trans.LoanTypeCode = loan.LoanTypeCode;
                new_trans.StartDate = loan.StartDate;
                new_trans.EndDate = loan.EndDate;
                new_trans.NumberOfDeductedInstallments = loan.NumberOfDeductedInstallments;
                new_trans.TransferTransactionNumber = loan.TransferTransactionNumber;
                new_trans.NumberOfInstallment = loan.NumberOfInstallment;
                new_trans.LoanAmount = loan.LoanAmount;
                var All_trans = dbcontext.LoanTransfer.ToList();
                if (All_trans.Count() == 0)
                {
                    new_trans.TransferTransactionNumber = stru + "1";
                }
                else
                {
                    new_trans.TransferTransactionNumber = stru + (All_trans.LastOrDefault().ID + 1).ToString();
                }
                //////////////////////////////////

                var transfer = new transferVM
                {
                    LoanRequest = loan,
                    LoanTransfer = new_trans

                };
                return View(transfer);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Loan_transfer_entry(transferVM model)
        {
            try
            {
                var loan = dbcontext.LoanRequest.FirstOrDefault(m => m.ID == model.LoanRequest.ID);
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == loan.statusID);
                if (sta.statu == check_status.Approved || sta.statu == check_status.Rejected || sta.statu == check_status.Closed || sta.statu == check_status.Canceled)
                {
                    TempData["message"] = HR.Resource.training.status_message;
                    return RedirectToAction("index");
                }
                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.loan_type = dbcontext.LoanInAdvanceSetup.ToList().Select(m => new { Code = m.LoanTypeCode + "--[" + m.LoanTypeDesc + ']', ID = m.ID });
                var emp_id = int.Parse(model.LoanTransfer.ToEmplpyee_Code);
                var new_emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID ==emp_id );
                var old_emp = loan.EmployeeID + " -- " + loan.emp_name;
                loan.EmployeeID = model.LoanTransfer.ToEmplpyee_Code;
                loan.emp_name = new_emp.Full_Name;
                dbcontext.SaveChanges();

                //====
                var Payment_Type_Source_Document_ = Payment_Type_Source_Document.Loan.GetHashCode();
              
                var transaction = dbcontext.Employee_Payroll_Transactions.Where(m => m.SourceDocumentRefrence == loan.LoanRequestNumber && m.SourceDocumentType == Payment_Type_Source_Document_).ToList();
                foreach(var item in transaction)
                {
                    item.Employee_Code = new_emp.Code;
                    dbcontext.SaveChanges();
                }
                dbcontext.SaveChanges();
                //====
                //////////////
                model.LoanTransfer.CreatedBy = User.Identity.Name;
                model.LoanTransfer.CreatedDate = DateTime.Now.Date;
                model.LoanTransfer.to_emp = loan.EmployeeID+" -- " +loan.emp_name;
                model.LoanTransfer.from_emp = old_emp;
                dbcontext.LoanTransfer.Add(model.LoanTransfer);
                dbcontext.SaveChanges();
                /////////////
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }

    }
    public class transferVM
        {
        public LoanRequest LoanRequest { get; set; }
        public LoanTransfer LoanTransfer { get; set; }
    }

}