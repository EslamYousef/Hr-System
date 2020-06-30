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
        // GET: Loan_transfer
        public ActionResult Index()
        {
            var model = dbcontext.LoanTransfer.ToList();
            return View(model);
        }
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
                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.loan_type = dbcontext.LoanInAdvanceSetup.ToList().Select(m => new { Code = m.LoanTypeCode + "--[" + m.LoanTypeDesc + ']', ID = m.ID });
                var emp_id = int.Parse(model.LoanTransfer.ToEmplpyee_Code);
                var new_emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID ==emp_id );
                var old_emp = loan.EmployeeID + " -- " + loan.emp_name;
                loan.EmployeeID = model.LoanTransfer.ToEmplpyee_Code;
                loan.emp_name = new_emp.Full_Name;
                dbcontext.SaveChanges();
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