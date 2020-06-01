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
                ViewBag.manual_payment = dbcontext.ManualPaymentTypes_Header.Where(m => m.PaymentTypeSourceDocument == 2).Select(m => new { Code = m.PaymentTypeCode + "-[" + m.PaymentTypeDesc + "-]", ID = m.ID }).ToList();///loan only
                ViewBag.salary_code = dbcontext.salary_code.Where(m=>m.CodeGroupType==1&&m.CodeValueType==7&&m.SourceEntry==1).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList(); //earning and money and contract
                ViewBag.monthly_salarycode = dbcontext.salary_code.Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();



                return View(new_record);
            }
            catch(Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult create(Loan_VM model,FormCollection form,string Command)
        {
            try
            {
                ////ViewBag.eligibility_matrix_code
                ViewBag.manual_payment = dbcontext.ManualPaymentTypes_Header.Where(m => m.PaymentTypeSourceDocument == 2).Select(m => new { Code = m.PaymentTypeCode + "-[" + m.PaymentTypeDesc + "-]", ID = m.ID }).ToList();///loan only
                ViewBag.salary_code = dbcontext.salary_code.Where(m => m.CodeGroupType == 1 && m.CodeValueType == 7 && m.SourceEntry == 1).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList(); //earning and money and contract
                ViewBag.monthly_salarycode = dbcontext.salary_code.Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();

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
             var loan=   dbcontext.LoanInAdvanceSetup.Add(add_record);
                dbcontext.SaveChanges();
                if(Command== "link")
                {
                    return RedirectToAction("link", new { loan_ID = loan.ID, type = 0 });
                }
                return RedirectToAction("index");


            }
            catch(Exception e)
            {
                return View(model);
            }
        }
        public ActionResult link(int loan_ID,int type)
        {
            try
            {
                var model_link = new LinkLoanDeductionsWithOtherManualPayment { Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date };
                ViewBag.header = dbcontext.ManualPaymentTypes_Header.ToList().Select(m => new { Code = m.PaymentTypeCode + "------[" + m.PaymentTypeDesc + ']', ID = m.ID });
                if(type==0)
                {
                    model_link.LoanInAdvanceSetupID = loan_ID;
                    var loan = dbcontext.LoanInAdvanceSetup.FirstOrDefault(m => m.ID == loan_ID);
                    model_link.LoanInAdvanceSetup = loan;
                    model_link.NumberOfInstallments = 0;
                    ViewBag.type = 0;
                    return View(model_link);
                }
                else if(type==1)
                {
                   
                    var link = dbcontext.LinkLoanDeductionsWithOtherManualPayment.FirstOrDefault(m => m.LoanInAdvanceSetupID == loan_ID);
                    if (link != null) { ViewBag.type = 1; return View(link); }
                    else
                    {
                       
                        model_link.LoanInAdvanceSetupID = loan_ID;
                        var loan = dbcontext.LoanInAdvanceSetup.FirstOrDefault(m => m.ID == loan_ID);
                        model_link.LoanInAdvanceSetup = loan;
                        model_link.NumberOfInstallments = 0;
                        ViewBag.type = 0;
                        return View(model_link);
                    }
                }
                else
                {
                    return RedirectToAction("edit", new { id = loan_ID });
                }
            }
            catch(Exception)
            {
                return RedirectToAction("edit", new { id = loan_ID });
            }
        }
        [HttpPost]
        public ActionResult link(LinkLoanDeductionsWithOtherManualPayment model,int type)
        {
            try
            {
                ViewBag.header = dbcontext.ManualPaymentTypes_Header.ToList().Select(m => new { Code = m.PaymentTypeCode + "------[" + m.PaymentTypeDesc + ']', ID = m.ID });
                ViewBag.type = type;
                if(type==0)
                {
                    model.Created_By = User.Identity.Name;model.Created_Date = DateTime.Now.Date;
                }
                if (type==1)
                {
                    var link = dbcontext.LinkLoanDeductionsWithOtherManualPayment.FirstOrDefault(m => m.LoanInAdvanceSetupID == model.LoanInAdvanceSetupID);
                    if (link != null) { dbcontext.LinkLoanDeductionsWithOtherManualPayment.Remove(link);dbcontext.SaveChanges(); model.Modified_By = User.Identity.Name; model.Modified_Date = DateTime.Now.Date; }
                }
              
                if (model.ManualPaymentTypes_DetailID==0 ||model.ManualPaymentTypes_HeaderID==0||model.LoanInAdvanceSetupID==0)
                {
                    return View(model);
                }
                model.LoanTypeCode = dbcontext.LoanInAdvanceSetup.FirstOrDefault(m => m.ID == model.LoanInAdvanceSetupID).LoanTypeCode;
                model.LoanInAdvanceSetup = null;
                model.PaymentTypeCode = dbcontext.ManualPaymentTypes_Header.FirstOrDefault(m=>m.ID==model.ManualPaymentTypes_HeaderID).Type_Code;
                model.SalaryCodeID = dbcontext.ManualPaymentTypes_Detail.FirstOrDefault(m => m.ID == model.ManualPaymentTypes_DetailID).SalaryCodeID;
                dbcontext.LinkLoanDeductionsWithOtherManualPayment.Add(model);
                dbcontext.SaveChanges();
                    return RedirectToAction("edit", new { id = model.LoanInAdvanceSetupID });
                
            }
            catch(Exception e)
            {
                return View(model);
            }
        }
        public JsonResult details(int id_header)
        {
            try
            {
         //       var header = dbcontext.ManualPaymentTypes_Header.FirstOrDefault(m => m.ID == id_header);
                var details = dbcontext.ManualPaymentTypes_Detail.Where(m => m.PaymentTypeCode == id_header.ToString()).ToList().Select(m => new { Code = m.SalaryCodeID + "------" +m.Salarycodedescription, ID = m.ID }); 
                return Json(details);
            }
            catch(Exception)
            {
                return Json(0);
            }
        }
        public ActionResult edit(int id)
        {
            try
            {
                ////ViewBag.eligibility_matrix_code
                ViewBag.manual_payment = dbcontext.ManualPaymentTypes_Header.Where(m => m.PaymentTypeSourceDocument == 2).Select(m => new { Code = m.PaymentTypeCode + "-[" + m.PaymentTypeDesc + "-]", ID = m.ID }).ToList();///loan only
                ViewBag.salary_code = dbcontext.salary_code.Where(m => m.CodeGroupType == 1 && m.CodeValueType == 7 && m.SourceEntry == 1).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList(); //earning and money and contract
                ViewBag.monthly_salarycode = dbcontext.salary_code.Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();

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
        public ActionResult edit(Loan_VM model, FormCollection form,string Command)
        {
            try
            {
                ////ViewBag.eligibility_matrix_code
                ViewBag.manual_payment = dbcontext.ManualPaymentTypes_Header.Where(m => m.PaymentTypeSourceDocument == 2).Select(m => new { Code = m.PaymentTypeCode + "-[" + m.PaymentTypeDesc + "-]", ID = m.ID }).ToList();///loan only
                ViewBag.salary_code = dbcontext.salary_code.Where(m => m.CodeGroupType == 1 && m.CodeValueType == 7 && m.SourceEntry == 1).Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList(); //earning and money and contract
                ViewBag.monthly_salarycode = dbcontext.salary_code.Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + "-]", ID = m.ID }).ToList();

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
                    var link = dbcontext.LinkLoanDeductionsWithOtherManualPayment.FirstOrDefault(m => m.LoanInAdvanceSetupID == add_record.ID);
                    if (link != null) { dbcontext.LinkLoanDeductionsWithOtherManualPayment.Remove(link); dbcontext.SaveChanges(); }

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
                if (Command == "link")
                {
                  
                        return RedirectToAction("link", new { loan_ID = add_record.ID, type = 1 });
                    
                }
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