using HR.Models;
using HR.Models.Infra;
using HR.Models.SetupPayroll;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class LoanInAdvanceSetupController : BaseController
    {
        // GET: LoanInAdvanceSetup
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.LoanInAdvanceSetup.ToList();
            return View(model);
        }
        public ActionResult create ()
        {
            try
            {
                var new_record = new Loan_VM();
                new_record.LoanInAdvanceSetup = new LoanInAdvanceSetup();
                new_record.LoanInAdvanceSetup.MinimumAmount = 0; new_record.LoanInAdvanceSetup.MaximumAmount = 0;
                new_record.LoanInAdvanceSetup.Percentage = 0; new_record.LoanInAdvanceSetup.PeriodLength = 0;
                new_record.LoanInAdvanceSetup.InstallmentAmount = 0;new_record.LoanInAdvanceSetup.NumberOfDeductedInstallments = 0;
                new_record.LoanInAdvanceSetup.IntervalPeriod =0;new_record.interval = 0;
                new_record.installment_period_type =(installment_period_type)installment_period_type.fixed_period;
                new_record.interval_type = new interval_type();
                new_record.loan_amount_type = (loan_amount_type)loan_amount_type.Fixed_amount;
                new_record.loan_eligibility_type = new loan_eligibility_type();
             
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                var model_ = dbcontext.LoanInAdvanceSetup.ToList();
                if (model_.Count() == 0)
                {
                    new_record.LoanInAdvanceSetup.LoanTypeCode = stru + "1";
                }
                else
                {
                    new_record.LoanInAdvanceSetup.LoanTypeCode = stru + (model_.LastOrDefault().ID + 1).ToString();
                }
                ////ViewBag.eligibility_matrix_code
                ////     ViewBag.manual_payment=manual payment settlement loan
                ViewBag.salary_code = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });
                ViewBag.monthly_salarycode = dbcontext.salary_code.Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });


                return View(new_record);
            }
            catch(Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult create(Loan_VM model,FormCollection form)
        {
            try
            {
                ////ViewBag.eligibility_matrix_code
                ////     ViewBag.manual_payment=manual payment settlement loan
                ViewBag.salary_code = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });
                ViewBag.monthly_salarycode = dbcontext.salary_code.Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });

                var add_record = new LoanInAdvanceSetup();
                add_record = model.LoanInAdvanceSetup;
                add_record.LoanEligibilityType = model.loan_eligibility_type.GetHashCode();
                add_record.LoanAmountType = model.loan_amount_type.GetHashCode();
                add_record.InstallmentAmount = model.installment_period_type.GetHashCode();
                add_record.IntervalType = model.interval_type.GetHashCode();
                add_record.InstallmentPeriodType = model.installment_period_type.GetHashCode();
                /////
                var a1 = form["A1"].Split(',');
                var a2 = form["A2"].Split(',');
                var a3 = form["A3"].Split(',');
                var a4 = form["A4"].Split(',');
                var a5 = form["A5"].Split(',');
                ////
                if (a1.Length == 1)
                {
                    add_record.EnableToGenerateManualPayment = false;
                    model.LoanInAdvanceSetup.EnableToGenerateManualPayment = false;
                }
                else
                {
                    add_record.EnableToGenerateManualPayment = true;
                    model.LoanInAdvanceSetup.EnableToGenerateManualPayment = true;
                }
                if (a2.Length == 1)
                {
                    add_record.EnableLoanAmountRestriction = false;
                    model.LoanInAdvanceSetup.EnableLoanAmountRestriction = false;
                }
                else
                {
                    add_record.EnableLoanAmountRestriction = true;
                    model.LoanInAdvanceSetup.EnableLoanAmountRestriction = true;
                }
                if (a3.Length == 1)
                {
                    add_record.EnableFreezing = false;
                    model.LoanInAdvanceSetup.EnableFreezing = false;
                }
                else
                {
                    add_record.EnableFreezing = true;
                    model.LoanInAdvanceSetup.EnableFreezing = true;
                }
                if (a4.Length == 1)
                {
                    add_record.EnableAutomaticPayrollDeduction = false;
                    model.LoanInAdvanceSetup.EnableAutomaticPayrollDeduction = false;
                }
                else
                {
                    add_record.EnableAutomaticPayrollDeduction = true;
                    model.LoanInAdvanceSetup.EnableAutomaticPayrollDeduction = true;

                }
                if (a5.Length == 1)
                {
                    add_record.EnableToRecuiningLoanRequestAutomaticAfterCloseTheRequest = false;
                    model.LoanInAdvanceSetup.EnableToRecuiningLoanRequestAutomaticAfterCloseTheRequest = false;
                }
                else
                {
                    add_record.EnableToRecuiningLoanRequestAutomaticAfterCloseTheRequest = true;
                    model.LoanInAdvanceSetup.EnableToRecuiningLoanRequestAutomaticAfterCloseTheRequest = true;
                }

                if(model.loan_eligibility_type==(loan_eligibility_type)loan_eligibility_type.Specific_employees)
                {
                    if(model.LoanInAdvanceSetup.EligibilityMatrixCode==null)
                    {
                        return View(model);
                    }
                }
                if(a1.Length==2)
                {
                    if (model.LoanInAdvanceSetup.ManualPaymentCode == null)
                    {
                        return View(model);
                    }
                }
                if (model.loan_amount_type == (loan_amount_type)loan_amount_type.percentage_amount_from_salary_code_amount)
                {
                    if (model.LoanInAdvanceSetup.SalaryCodeAmount == null)
                    {
                        return View(model);
                    }
                }
                if (a4.Length == 2)
                {
                    if (model.LoanInAdvanceSetup.SalaryCodeID == null)
                    {
                        return View(model);
                    }
                }
                add_record.Interval =model.interval.ToString();
                add_record.Created_By = User.Identity.Name;
                add_record.Created_Date = DateTime.Now.Date;
                dbcontext.LoanInAdvanceSetup.Add(add_record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");


            }
            catch(Exception e)
            {
                return View(model);
            }
        }
        public ActionResult edit(int id)
        {
            try
            {
                ////ViewBag.eligibility_matrix_code
                ////     ViewBag.manual_payment=manual payment settlement loan
                ViewBag.salary_code = dbcontext.salary_code.ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });
                ViewBag.monthly_salarycode = dbcontext.salary_code.Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });

                var model = dbcontext.LoanInAdvanceSetup.FirstOrDefault(m => m.ID == id);
                var edit_model = new Loan_VM();
                edit_model.LoanInAdvanceSetup = model;
                edit_model.installment_period_type = (installment_period_type)model.InstallmentPeriodType;
                edit_model.interval = int.Parse(model.Interval);
                edit_model.interval_type = (interval_type)model.IntervalType;
                edit_model.loan_amount_type = (loan_amount_type)model.LoanAmountType;
                edit_model.loan_eligibility_type = (loan_eligibility_type)model.LoanEligibilityType;
                return View(edit_model);
                
            }
            catch(Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(Loan_VM model, FormCollection form)
        {
            try
            {
                var add_record = dbcontext.LoanInAdvanceSetup.FirstOrDefault(m => m.ID == model.LoanInAdvanceSetup.ID);

                add_record.LoanEligibilityType = model.loan_eligibility_type.GetHashCode();
                add_record.LoanAmountType = model.loan_amount_type.GetHashCode();
                add_record.InstallmentAmount = model.installment_period_type.GetHashCode();
                add_record.IntervalType = model.interval_type.GetHashCode();

                /////
                var a1 = form["A1"].Split(',');
                var a2 = form["A2"].Split(',');
                var a3 = form["A3"].Split(',');
                var a4 = form["A4"].Split(',');
                var a5 = form["A5"].Split(',');
                ////
                if (a1.Length == 1)
                {
                    add_record.EnableToGenerateManualPayment = false;
                    model.LoanInAdvanceSetup.EnableToGenerateManualPayment = false;
                }
                else
                {
                    add_record.EnableToGenerateManualPayment = true;
                    model.LoanInAdvanceSetup.EnableToGenerateManualPayment = true;
                }
                if (a2.Length == 1)
                {
                    add_record.EnableLoanAmountRestriction = false;
                    model.LoanInAdvanceSetup.EnableLoanAmountRestriction = false;
                }
                else
                {
                    add_record.EnableLoanAmountRestriction = true;
                    model.LoanInAdvanceSetup.EnableLoanAmountRestriction = true;
                }
                if (a3.Length == 1)
                {
                    add_record.EnableFreezing = false;
                    model.LoanInAdvanceSetup.EnableFreezing = false;
                }
                else
                {
                    add_record.EnableFreezing = true;
                    model.LoanInAdvanceSetup.EnableFreezing = true;
                }
                if (a4.Length == 1)
                {
                    add_record.EnableAutomaticPayrollDeduction = false;
                    model.LoanInAdvanceSetup.EnableAutomaticPayrollDeduction = false;
                }
                else
                {
                    add_record.EnableAutomaticPayrollDeduction = true;
                    model.LoanInAdvanceSetup.EnableAutomaticPayrollDeduction = true;

                }
                if (a5.Length == 1)
                {
                    add_record.EnableToRecuiningLoanRequestAutomaticAfterCloseTheRequest = false;
                    model.LoanInAdvanceSetup.EnableToRecuiningLoanRequestAutomaticAfterCloseTheRequest = false;
                }
                else
                {
                    add_record.EnableToRecuiningLoanRequestAutomaticAfterCloseTheRequest = true;
                    model.LoanInAdvanceSetup.EnableToRecuiningLoanRequestAutomaticAfterCloseTheRequest = true;
                }
                if (model.loan_eligibility_type == (loan_eligibility_type)loan_eligibility_type.Specific_employees)
                {
                    if (model.LoanInAdvanceSetup.EligibilityMatrixCode == null)
                    {
                        return View(model);
                    }
                }
                if (a1.Length == 2)
                {
                    if (model.LoanInAdvanceSetup.ManualPaymentCode == null)
                    {
                        return View(model);
                    }
                }
                if (model.loan_amount_type == (loan_amount_type)loan_amount_type.percentage_amount_from_salary_code_amount)
                {
                    if (model.LoanInAdvanceSetup.SalaryCodeAmount == null)
                    {
                        return View(model);
                    }
                }
                if (a4.Length == 2)
                {
                    if (model.LoanInAdvanceSetup.SalaryCodeID == null)
                    {
                        return View(model);
                    }
                }

                add_record.LoanTypeDesc = model.LoanInAdvanceSetup.LoanTypeDesc;
                add_record.LoanTypeAltDesc = model.LoanInAdvanceSetup.LoanTypeAltDesc;
                add_record.EligibilityMatrixCode = model.LoanInAdvanceSetup.EligibilityMatrixCode;
                add_record.ManualPaymentCode = model.LoanInAdvanceSetup.ManualPaymentCode;
                add_record.MinimumAmount = model.LoanInAdvanceSetup.MinimumAmount;
                add_record.MaximumAmount = model.LoanInAdvanceSetup.MaximumAmount;
                add_record.Percentage = model.LoanInAdvanceSetup.Percentage;
                add_record.SalaryCodeAmount = model.LoanInAdvanceSetup.SalaryCodeAmount;
                add_record.PeriodLength = model.LoanInAdvanceSetup.PeriodLength;
                add_record.InstallmentAmount = model.LoanInAdvanceSetup.InstallmentAmount;
                add_record.SalaryCodeID = model.LoanInAdvanceSetup.SalaryCodeID;
                add_record.NumberOfDeductedInstallments = model.LoanInAdvanceSetup.NumberOfDeductedInstallments;
                add_record.Interval = model.interval.ToString();
                
                add_record.IntervalPeriod = model.LoanInAdvanceSetup.IntervalPeriod;
                add_record.EnableToRecuiningLoanRequestAutomaticAfterCloseTheRequest = model.LoanInAdvanceSetup.EnableToRecuiningLoanRequestAutomaticAfterCloseTheRequest;
                add_record.Modified_By = User.Identity.Name;
                add_record.Modified_Date = DateTime.Now.Date;
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch(Exception)
            { return View(model); }
        }

        public ActionResult delete(int id)
        {
            try
            {
                var model = dbcontext.LoanInAdvanceSetup.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult delete_method(int id)
        {
            var model = dbcontext.LoanInAdvanceSetup.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.LoanInAdvanceSetup.Remove(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
    }
    public class Loan_VM
    {
        public LoanInAdvanceSetup LoanInAdvanceSetup { get; set; }
        public loan_amount_type loan_amount_type { get; set; }
        public loan_eligibility_type loan_eligibility_type { get; set; }
        public installment_period_type installment_period_type { get; set; }
        public interval_type interval_type { get; set; }
        public int interval { get; set; }

    }
    public enum loan_eligibility_type
    {
        [Display(Name = "All Employees")]
        All_employees =1,
        [Display(Name = "Specific Amployees")]
        Specific_employees =2
    }
    public enum loan_amount_type
    {
        [Display(Name = "Fixed Amount")]
        Fixed_amount =1,
        [Display(Name ="percentage_amount_from_salary_code_amount")]
        percentage_amount_from_salary_code_amount=2

    }
    public enum installment_period_type
    {
        [Display(Name = "fixed period")]
        fixed_period =1,
        [Display(Name = "fixed Amount")]
        fixed_amount =2,
        calculated=3
    }
    public enum interval_type
    {
        none=1,
        once=2,
        month=3,
        year=4,
        [Display(Name = "Year Of Service")]
        year_of_service =5
    }

}