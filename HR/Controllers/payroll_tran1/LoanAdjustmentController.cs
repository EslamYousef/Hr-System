using HR.Models;
using HR.Models.Infra;
using HR.Models.payroll_trans;
using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.payroll_tran1
{
    public class LoanAdjustmentController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        [Authorize(Roles = "Admin,payroll,payrollTransaction,loan transaction")]
        public ActionResult Index()
        {
            var model = dbcontext.LoanAdjustment.ToList();
            return View(model);
        }
        [Authorize(Roles = "Admin,payroll,payrollTransaction,loan transaction")]
        public ActionResult create(int id)
        {
            try
            {
                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.loan_type = dbcontext.LoanInAdvanceSetup.ToList().Select(m => new { Code = m.LoanTypeCode + "--[" + m.LoanTypeDesc + ']', ID = m.ID });

                var loan = dbcontext.LoanRequest.FirstOrDefault(m => m.ID == id);
                var installment = dbcontext.LoanInstallment.Where(m => m.LoanRequestNumber == loan.LoanRequestNumber).ToList();
                var loan_adjustment = new LoanAdjustment();
                //////////////////////////////////
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                var All_requests = dbcontext.LoanAdjustment.ToList();
                if (All_requests.Count() == 0)
                {
                    loan_adjustment.TransactionNumber = stru + "1";
                }
                else
                {
                    loan_adjustment.TransactionNumber = stru + (All_requests.LastOrDefault().ID + 1).ToString();
                }
                //////////////////////////////////

                    loan_adjustment.TransactionDate = DateTime.Now.Date;
                    loan_adjustment.LoanRequestNumber = loan.LoanRequestNumber;
                    loan_adjustment.PaidAmount = 0;
                    loan_adjustment.InstallmentAmount = loan.LoanInstallmentAmount;
                    loan_adjustment.InstallmentNumber = loan.NumberOfInstallment.ToString();
                    loan_adjustment.TotalPaidAmount = loan.TotalPaidAmount;
                    loan_adjustment.TotalUnpaidAmount = loan.TotalRemainingAmount;
              
                /////////////////////////////////
                var installmentVM = new List<loan_installmentVM>();
                foreach(var item in installment)
                {
                    installmentVM.Add(new loan_installmentVM { freez=false,LoanInstallment=item} );
                }
                var Our_model = new adjustmentVM {type=Type.Monthly, loan = loan, installment = installmentVM, adjustment = loan_adjustment };
                return View(Our_model);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult create(adjustmentVM model,FormCollection Form)
        {
            try
            {
                ViewBag.emp = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID });
                ViewBag.loan_type = dbcontext.LoanInAdvanceSetup.ToList().Select(m => new { Code = m.LoanTypeCode + "--[" + m.LoanTypeDesc + ']', ID = m.ID });
                var loan = dbcontext.LoanRequest.FirstOrDefault(m => m.ID == model.loan.ID);
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == loan.statusID);
                if (sta.statu == check_status.Approved || sta.statu == check_status.Rejected || sta.statu == check_status.Closed || sta.statu == check_status.Canceled)
                {
                    TempData["message"] = HR.Resource.training.status_message;
                    return RedirectToAction("index");
                }
                var installment = dbcontext.LoanInstallment.Where(m => m.IsPaid==false&&m.IsActive==true&&m.IsFreeze==false&&m.LoanRequestNumber == loan.LoanRequestNumber).ToList();
                var installment2 = dbcontext.LoanInstallment.Where(m => m.LoanRequestNumber == loan.LoanRequestNumber).ToList();
                model.installment = new List<loan_installmentVM>();
                foreach (var item in installment2)
                {
                    model.installment.Add(new loan_installmentVM { freez = false, LoanInstallment = item });
                }
                if (model.type == Type.Money)
                {
                    var loan_request_number2 = "";
                    var paid = 0;
                    var flag2 = false;
                    foreach (var item in installment)
                    {
                        flag2 = true;
                        if (model.adjustment.PaidAmount == 0)
                        {
                            break;
                        }
                        if (model.adjustment.PaidAmount<item.UnpaidAmount)////القيمه المدفوعة اقل من قيمة القسط الحالى
                        {
                            item.PaidAmount = item.UnpaidAmount-model.adjustment.PaidAmount;
                            item.UnpaidAmount = item.UnpaidAmount-item.PaidAmount;
                            model.adjustment.PaidAmount = model.adjustment.PaidAmount-item.PaidAmount;
                            loan_request_number2 = item.LoanRequestNumber;
                            paid += (int)item.PaidAmount;
                            //====
                            var Payment_Type_Source_Document_ = Payment_Type_Source_Document.Loan.GetHashCode();
                            var transaction = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.SourceDocumentDescription == item.ID.ToString() && m.SourceDocumentType == Payment_Type_Source_Document_);
                            transaction.TransactionValue = item.UnpaidAmount;
                            dbcontext.SaveChanges();
                            //====

                        }
                        else if(model.adjustment.PaidAmount>item.UnpaidAmount&& model.adjustment.PaidAmount<=loan.TotalRemainingAmount)///القيمة المدفوعة اكبر من قيمة القسط الحالى
                        {
                            item.InstallmenNotes = "this installment payed by adjustment with number";
                            item.IsPaid = true;
                            model.adjustment.PaidAmount = model.adjustment.PaidAmount - item.UnpaidAmount;
                            item.PaidAmount = item.UnpaidAmount;
                            item.UnpaidAmount = 0;
                            loan_request_number2 = item.LoanRequestNumber;
                            paid += (int)item.PaidAmount;
                            //====
                            var Payment_Type_Source_Document_ = Payment_Type_Source_Document.Loan.GetHashCode();
                            var transaction = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.SourceDocumentDescription == item.ID.ToString() && m.SourceDocumentType == Payment_Type_Source_Document_);
                            transaction.TransactionValue = item.UnpaidAmount;
                            dbcontext.SaveChanges();
                            //====

                        }
                        else if (model.adjustment.PaidAmount == item.UnpaidAmount)//القيمة المدفوعة بتساوى قيمة القسط الحالى
                        {
                            item.InstallmenNotes = "this installment payed by adjustment with number";
                            item.IsPaid = true;
                            model.adjustment.PaidAmount = model.adjustment.PaidAmount - item.UnpaidAmount;
                            item.PaidAmount = item.UnpaidAmount;
                            item.UnpaidAmount = 0;
                            loan_request_number2 = item.LoanRequestNumber;
                            paid += (int)item.PaidAmount;
                            //====
                            var Payment_Type_Source_Document_ = Payment_Type_Source_Document.Loan.GetHashCode();
                            var transaction = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.SourceDocumentDescription == item.ID.ToString() && m.SourceDocumentType == Payment_Type_Source_Document_);
                            transaction.TransactionValue = item.UnpaidAmount;
                            dbcontext.SaveChanges();
                            //====
                        }
                        else
                        {
                            TempData["Message"] = "you paid more than Remaining amount";
                            return View(model);
                        }

                        dbcontext.SaveChanges();
                        //===
                        var loan_request = dbcontext.LoanRequest.FirstOrDefault(m => m.LoanRequestNumber == loan_request_number2);
                        loan_request.TotalPaidAmount = loan_request.TotalPaidAmount + item.PaidAmount;
                        loan_request.TotalRemainingAmount = loan_request.TotalRemainingAmount - item.PaidAmount;
                        dbcontext.SaveChanges();
                        //===
                        
                    }
                    if(flag2)
                    {
                        model.adjustment.CreatedBy = User.Identity.Name;
                        model.adjustment.CreatedDate = DateTime.Now.Date;
                        model.adjustment.LoanRequestNumber = loan.LoanRequestNumber;
                        model.adjustment.PaidAmount = paid;
                        model.adjustment.TotalPaidAmount = loan.TotalPaidAmount;
                        model.adjustment.TotalUnpaidAmount = loan.TotalRemainingAmount;
                        dbcontext.LoanAdjustment.Add(model.adjustment);
                        dbcontext.SaveChanges();
                    }
                }
                else
                {
                    ////////////////////////////////////
                    double? paid = 0.0;
                    bool flag = false;
                    var loan_request_number = "";
                    var selected = Form["frezz"].Split(',');
                    foreach (var item in selected)
                    {
                        if (item != " ")
                        {
                            var ID = int.Parse(item);
                            var spacial_installment = dbcontext.LoanInstallment.FirstOrDefault(m => m.ID == ID);
                            if (spacial_installment.IsPaid == false&& spacial_installment.IsActive==true)
                            {
                                paid = paid + spacial_installment.UnpaidAmount;
                                spacial_installment.PaidAmount = spacial_installment.UnpaidAmount;
                                spacial_installment.UnpaidAmount = 0;
                                spacial_installment.IsPaid = true;
                                dbcontext.SaveChanges();
                                flag = true;
                                loan_request_number = spacial_installment.LoanRequestNumber;
                                //====
                                var Payment_Type_Source_Document_ = Payment_Type_Source_Document.Loan.GetHashCode();

                                var transaction = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.SourceDocumentDescription == spacial_installment.ID.ToString() && m.SourceDocumentType == Payment_Type_Source_Document_);
                                transaction.TransactionValue = 0;
                                dbcontext.SaveChanges();
                                //====
                            }

                        }
                    }
                    if (flag)
                    {
                        var loan_request = dbcontext.LoanRequest.FirstOrDefault(m => m.LoanRequestNumber == loan_request_number);
                        loan_request.TotalPaidAmount = loan_request.TotalPaidAmount + paid;
                        loan_request.TotalRemainingAmount = loan_request.TotalRemainingAmount - paid;
                        dbcontext.SaveChanges();
                        ///////////
                        //////////
                        model.adjustment.CreatedBy = User.Identity.Name;
                        model.adjustment.CreatedDate = DateTime.Now.Date;
                        model.adjustment.LoanRequestNumber = loan.LoanRequestNumber;
                        model.adjustment.PaidAmount = paid;
                        model.adjustment.TotalPaidAmount = loan.TotalPaidAmount;
                        model.adjustment.TotalUnpaidAmount = loan.TotalRemainingAmount;
                        dbcontext.LoanAdjustment.Add(model.adjustment);
                        dbcontext.SaveChanges();
                    }
                }
                //=================================check for alert==================================
                var get_result_check = HR.Controllers.check.check_alert("loan adjustmetn transaction", HR.Models.user.Action.Create, HR.Models.user.type_field.form);
                if (get_result_check != null)
                {
                    var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.Name, send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
                    if (get_result_check.until != null)
                    {
                        if (get_result_check.until.Value.Year != 0001)
                        {
                            inbox.until = get_result_check.until;
                        }
                    }
                    ApplicationDbContext dbcontext = new ApplicationDbContext();
                    dbcontext.Alert_inbox.Add(inbox);
                    dbcontext.SaveChanges();
                }
                //===================================================================================
                ////////////////////////////////
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
    }
    public class adjustmentVM
    {
        public LoanRequest loan { get; set; }
        public List<loan_installmentVM> installment { get; set; }
        public LoanAdjustment adjustment { get; set; }
        public Type type { get; set; }
    }
    public enum Type
    {
        Money=1,
        Monthly=2
    }
}