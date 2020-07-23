using Antlr.Runtime;
using HR.Models;
using HR.Models.Infra;
using HR.Models.SetupPayroll;
using HR.Models.TransactionsPayroll;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using System.IO;
using Microsoft.Office.Interop.Excel;
using HR.Models.ViewModel;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace HR.Controllers.TransactionsPayroll
{
    [Authorize]
    public class Employee_Payroll_TransactionsController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_Payroll_Transactions
        public ActionResult Index()
        {
            var Employee_Payroll_Transactions = dbcontext.Employee_Payroll_Transactions.ToList();
            var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();
            var salary_code = dbcontext.salary_code.ToList();
            var model = from a in Employee_Payroll_Transactions
                        join b in Employee_Profile on a.Employee_Code equals b.Code
                        join c in salary_code on a.SalaryCodeID equals c.SalaryCodeID
                        select new VM_Employee_Payroll_Transactions
                        { EmployeeName = b.Full_Name, SalaryCodeDesc = c.SalaryCodeDesc, Employee_Payroll_Transactions = a };
            return View(model);
        }
        public ActionResult ViewExecl()
        {
            var AllData = TempData["list"];
            ViewBag.Data = AllData;
            return View(ViewBag.Data);
        }
        [HttpPost]
        public ActionResult ViewExecl(Employee_Payroll_Transactions model, FormCollection form)
        {
            try
            {
                var AllData = ViewBag.Data;
                var EmpCode = form["EmployeeCode"].Split(',');
                var EmpName = form["EmployeeName"].Split(',');
                var SalCode = form["SalaryCode"].Split(',');
                var SalDesc = form["SalaryDesc"].Split(',');
                var Transaction = form["TransactionDate"].Split(',');
                var Effective = form["EffectiveDate"].Split(',');

                for (var co = 0; co < EmpCode.Length; co++)
                {
                    var Code = EmpCode[co];
                    var ChCode = dbcontext.Employee_Profile.Where(a => a.Active == true).FirstOrDefault(a => a.Code == Code);
                    if (ChCode == null)
                    {
                        TempData["Message"] = "Not found this data " + Code;
                        return RedirectToAction("Index", "Employee_Payroll_Transactions");
                    }
                }
                for (var na = 0; na < EmpName.Length; na++)
                {
                    var Name = EmpName[na];
                    var ChName = dbcontext.Employee_Profile.Where(a => a.Active == true).FirstOrDefault(a => a.Name == Name);
                    if (ChName == null)
                    {
                        TempData["Message"] = "Not found this data " + Name;
                        return RedirectToAction("Index", "Employee_Payroll_Transactions");
                    }
                }
                for (var sa = 0; sa < SalCode.Length; sa++)
                {
                    var SalaryCode = SalCode[sa];
                    var ChSalCode = dbcontext.salary_code.FirstOrDefault(a => a.SalaryCodeID == SalaryCode);
                    if (ChSalCode == null)
                    {
                        TempData["Message"] = "Not found this data " + SalaryCode;
                        return RedirectToAction("Index", "Employee_Payroll_Transactions");
                    }
                }
                for (var SalDe = 0; SalDe < SalDesc.Length; SalDe++)
                {
                    var SalaryDesc = SalDesc[SalDe];
                    var ChSalDesc = dbcontext.salary_code.FirstOrDefault(a => a.SalaryCodeDesc == SalaryDesc);
                    if (ChSalDesc == null)
                    {
                        TempData["Message"] = "Not found this data " + SalaryDesc;
                        return RedirectToAction("Index", "Employee_Payroll_Transactions");
                    }
                }
                for (var c = 0; c < EmpCode.Length; c++)
                {
                    if (EmpCode[c] != "")
                    {
                        var CoEmp = EmpCode[c];
                        var NaEmp = EmpName[c];
                        var ChName = dbcontext.Employee_Profile.Where(a => a.Active == true).FirstOrDefault(a => a.Code == CoEmp && a.Name == NaEmp);
                        if (ChName == null)
                        {
                            TempData["Message"] = "Not found this data " + CoEmp + " " + NaEmp;
                            return RedirectToAction("Index", "Employee_Payroll_Transactions");
                        }
                        var SalaCode = SalCode[c];
                        var SalaDesc = SalDesc[c];
                        var ChSalCode = dbcontext.salary_code.FirstOrDefault(a => a.SalaryCodeID == SalaCode && a.SalaryCodeDesc == SalaDesc);
                        if (ChSalCode == null)
                        {
                            TempData["Message"] = "Not found this data " + SalaCode + " " + SalaDesc;
                            return RedirectToAction("Index", "Employee_Payroll_Transactions");
                        }
                        Employee_Payroll_Transactions newdata = new Employee_Payroll_Transactions();
                        var Employee_Payroll_Transactions = dbcontext.Employee_Payroll_Transactions.ToList();
                        var Stru = dbcontext.StructureModels.FirstOrDefault(a => a.All_Models == ChModels.Payroll).Structure_Code;
                        if (Employee_Payroll_Transactions.Count() == 0)
                        {
                            newdata.TransactionNumber = Stru + "1";
                        }
                        else
                        {
                            newdata.TransactionNumber = Stru + (Employee_Payroll_Transactions.LastOrDefault().ID + 1).ToString();
                        }
                        newdata.Employee_Code = CoEmp;
                        newdata.SalaryCodeID = SalaCode;
                        newdata.TransactionValue = 0;
                        newdata.TransactionDate = DateTime.Parse(Transaction[c]);
                        newdata.TransactionYear = DateTime.Parse(Transaction[c]).Year;
                        newdata.TransactionMonth = DateTime.Parse(Transaction[c]).Month;
                        newdata.EffectiveDate = DateTime.Parse(Effective[c]);
                        newdata.EffectiveYear = DateTime.Parse(Effective[c]).Year;
                        newdata.EffectiveMonth = DateTime.Parse(Effective[c]).Month;
                        newdata.Created_By = User.Identity.Name;
                        newdata.Created_Date = DateTime.Now.Date;
                        newdata.CreatedBy = User.Identity.Name;
                        newdata.CreatedDate = DateTime.Now.Date;
                        DateTime statis2 = Convert.ToDateTime("1/1/1900");
                        newdata.ReportAsReadyDate = statis2;
                        newdata.ApprovedDate = statis2;
                        newdata.RejectedDate = statis2;
                        newdata.CanceledDate = statis2;
                        newdata.CompletedDate = statis2;
                        //newdata.CompletedDate = statis2;
                        newdata.Modified_Date = statis2;
                        newdata.TransactionStatus = check_status.Created.GetHashCode();

                        newdata.check_status = HR.Models.Infra.check_status.created;
                        newdata.name_state = nameof(check_status.Created);
                        var username = User.Identity.GetUserName();
                        var Date = Convert.ToDateTime("1/1/1900");
                        var s = new status { statu = HR.Models.Infra.check_status.created, created_by = username, Type = Models.Infra.Type.Employee_Payroll_Transactions, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                        var st = dbcontext.status.Add(s);
                        dbcontext.SaveChanges();
                        newdata.statID = s.ID;

                        dbcontext.Employee_Payroll_Transactions.Add(newdata);
                        dbcontext.SaveChanges();
                    }
                }
                return RedirectToAction("Index", "Employee_Payroll_Transactions");
            }
            catch (Exception e)
            {
                TempData["Message"] = "Not found this data";
                return RedirectToAction("Index", "Employee_Payroll_Transactions");
            }

        }

        public ActionResult create()
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
                ViewBag.salary_code = dbcontext.salary_code.Where(a => a.SourceEntry == 2).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });
                ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });
                DateTime Statis = Convert.ToDateTime("1/1/1900");
                var new_record = new Employee_Payroll_Transactions { TransactionValue = 0, EffectiveDate = DateTime.Now, TransactionDate = DateTime.Now, Created_Date = DateTime.Now, Created_By = User.Identity.Name, CreatedDate = Statis, ReportAsReadyDate = Statis, ApprovedDate = Statis, RejectedDate = Statis, CanceledDate = Statis, CompletedDate = Statis, Modified_Date = Statis };
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                var model_ = dbcontext.Employee_Payroll_Transactions.ToList();
                if (model_.Count() == 0)
                {
                    new_record.TransactionNumber = stru + "1";
                }
                else
                {
                    new_record.TransactionNumber = stru + (model_.LastOrDefault().ID + 1).ToString();
                }

                var model = new Headers { Employee_Payroll_Transactions = new_record, check_status = check_status.Created, Payment_Type_Source_Document = Payment_Type_Source_Document.Manual, code_value_type = code_value_type.Unkown, code_type = code_type.Earning };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(Headers model, FormCollection form)
        {
            try
            {

                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
                ViewBag.salary_code = dbcontext.salary_code.Where(a => a.SourceEntry == 2).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });
                ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });

                Employee_Payroll_Transactions new_Record = new Employee_Payroll_Transactions();

                new_Record.TransactionNumber = model.Employee_Payroll_Transactions.TransactionNumber;
                int EmpID = int.Parse(model.Employee_Payroll_Transactions.Employee_Code);
                if (model.Employee_Payroll_Transactions.Employee_Code != null)
                {
                    var empcode = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == EmpID).Code;
                    new_Record.Employee_Code = empcode;
                }
                int SalID = int.Parse(model.Employee_Payroll_Transactions.SalaryCodeID);
                if (model.Employee_Payroll_Transactions.SalaryCodeID != null)
                {
                    var Salcode = dbcontext.salary_code.FirstOrDefault(a => a.ID == SalID).SalaryCodeID;
                    new_Record.SalaryCodeID = Salcode;
                }
                new_Record.JournalName_BatchCode = model.Employee_Payroll_Transactions.JournalName_BatchCode;
                new_Record.Contract_Number = model.Employee_Payroll_Transactions.Contract_Number;
                new_Record.TransactionDate = model.Employee_Payroll_Transactions.TransactionDate;
                string Transaction = model.Employee_Payroll_Transactions.TransactionDate.ToString();
                new_Record.TransactionMonth = DateTime.Parse(Transaction).Month;
                new_Record.TransactionYear = DateTime.Parse(Transaction).Year;
                new_Record.EffectiveDate = model.Employee_Payroll_Transactions.EffectiveDate;
                string Effective = model.Employee_Payroll_Transactions.TransactionDate.ToString();
                new_Record.EffectiveMonth = DateTime.Parse(Effective).Month;
                new_Record.EffectiveYear = DateTime.Parse(Effective).Year;
                new_Record.TransactionValue = model.Employee_Payroll_Transactions.TransactionValue;
                new_Record.CreatedDate = DateTime.Now.Date;
                new_Record.CreatedBy = User.Identity.Name;
                new_Record.ReportAsReadyDate = model.Employee_Payroll_Transactions.ReportAsReadyDate;
                new_Record.ReportAsReadyBy = model.Employee_Payroll_Transactions.ReportAsReadyBy;
                new_Record.ApprovedBy = model.Employee_Payroll_Transactions.ApprovedBy;
                new_Record.ApprovedDate = model.Employee_Payroll_Transactions.ApprovedDate;
                new_Record.RejectedBy = model.Employee_Payroll_Transactions.RejectedBy;
                new_Record.RejectedDate = model.Employee_Payroll_Transactions.RejectedDate;
                new_Record.CanceledBy = model.Employee_Payroll_Transactions.CanceledBy;
                new_Record.CanceledDate = model.Employee_Payroll_Transactions.CanceledDate;
                new_Record.CompletedBy = model.Employee_Payroll_Transactions.CompletedBy;
                new_Record.CompletedDate = model.Employee_Payroll_Transactions.CompletedDate;
                new_Record.SourceDocumentRefrence = model.Employee_Payroll_Transactions.SourceDocumentRefrence;
                new_Record.SourceDocumentDescription = model.Employee_Payroll_Transactions.SourceDocumentDescription;
                new_Record.SourceDocumentNotes = model.Employee_Payroll_Transactions.SourceDocumentNotes;
                new_Record.TransactionNotes = model.Employee_Payroll_Transactions.TransactionNotes;
                new_Record.CostCenterCode = model.Employee_Payroll_Transactions.CostCenterCode;
                new_Record.ExtendedFields_Code = model.Employee_Payroll_Transactions.ExtendedFields_Code;
                new_Record.Company_ID = model.Employee_Payroll_Transactions.Company_ID;
                new_Record.Created_By = User.Identity.Name;
                new_Record.Created_Date = DateTime.Now.Date;
                new_Record.Modified_By = model.Employee_Payroll_Transactions.Modified_By;
                new_Record.Modified_Date = model.Employee_Payroll_Transactions.Modified_Date;
                new_Record.SourceDocumentType = Payment_Type_Source_Document.Manual.GetHashCode();
                new_Record.TransactionStatus = check_status.Created.GetHashCode();

                new_Record.check_status = HR.Models.Infra.check_status.created;
                new_Record.name_state = nameof(check_status.Created);
                var username = User.Identity.GetUserName();
                var Date = Convert.ToDateTime("1/1/1900");
                var s = new status { statu = HR.Models.Infra.check_status.created, created_by = username, Type = Models.Infra.Type.Employee_Payroll_Transactions, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                var st = dbcontext.status.Add(s);
                dbcontext.SaveChanges();
                new_Record.statID = s.ID;

                var Header = dbcontext.Employee_Payroll_Transactions.Add(new_Record);
                dbcontext.SaveChanges();

                ///////////////////
                var codeid = form["codeid"].Split(',');
                var SalaryDes = form["SalaryDes"].Split(',');
                var ValueType = form["ValueType"].Split(',');
                var DefaultValue = form["DefaultValue"].Split(',');

                for (var i = 0; i < codeid.Length; i++)
                {
                    if (codeid[i] != "")
                    {
                        var value = double.Parse(DefaultValue[i]);
                        var valuetype = ValueType[i];
                        var new_details = new Employee_Payroll_Transactions_ExtendedFieldsDetail { TransactionNumber = Header.TransactionNumber, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Detail_Code = codeid[i], Detail_Desc = SalaryDes[i], ValueType = valuetype, Value = value };
                        dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.Add(new_details);
                        dbcontext.SaveChanges();
                    }
                }

                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        public ActionResult edit(string id)
        {
            try
            {
                var ID = int.Parse(id);
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
                ViewBag.salary_code = dbcontext.salary_code.Where(a => a.SourceEntry == 2).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.SalaryCodeID });
                ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });
                var x = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(a => a.ID == ID).SalaryCodeID;
                var y = dbcontext.salary_code.FirstOrDefault(a => a.SalaryCodeID == x).ID;
                ViewBag.id = y;
                ViewBag.code = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == ID).ID;
                var old_model = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == ID);
                var Head = new Headers { Employee_Payroll_Transactions = old_model, check_status = (check_status)old_model.TransactionStatus, Payment_Type_Source_Document = (Payment_Type_Source_Document)old_model.SourceDocumentType };
                var old_details = dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.Where(m => m.TransactionNumber == old_model.ID.ToString()).ToList();
                var new_model = new VMs { Employee_Payroll_Transactions_ExtendedFieldsDetail = old_details, Header = Head };
                return View(new_model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(VMs model, FormCollection form)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
                ViewBag.salary_code = dbcontext.salary_code.Where(a => a.SourceEntry == 2).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.SalaryCodeID });
                ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });
                var H_ = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == model.Header.Employee_Payroll_Transactions.ID);
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == H_.statID);
                if (sta.statu == Models.Infra.check_status.Approved || sta.statu == Models.Infra.check_status.Rejected || sta.statu == Models.Infra.check_status.Closed || sta.statu == Models.Infra.check_status.Recervied || sta.statu == Models.Infra.check_status.Canceled)
                {
                    TempData["message"] = HR.Resource.training.status_message;
                    return RedirectToAction("index");
                }
                ///update////
                var new_Record = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == model.Header.Employee_Payroll_Transactions.ID);

                new_Record.TransactionNumber = model.Header.Employee_Payroll_Transactions.TransactionNumber;

                new_Record.Employee_Code = model.Header.Employee_Payroll_Transactions.Employee_Code;

                new_Record.SalaryCodeID = model.Header.Employee_Payroll_Transactions.SalaryCodeID;

                new_Record.JournalName_BatchCode = model.Header.Employee_Payroll_Transactions.JournalName_BatchCode;
                new_Record.Contract_Number = model.Header.Employee_Payroll_Transactions.Contract_Number;
                new_Record.TransactionDate = model.Header.Employee_Payroll_Transactions.TransactionDate;
                string Transaction = model.Header.Employee_Payroll_Transactions.TransactionDate.ToString();
                new_Record.TransactionMonth = DateTime.Parse(Transaction).Month;
                new_Record.TransactionYear = DateTime.Parse(Transaction).Year;
                new_Record.EffectiveDate = model.Header.Employee_Payroll_Transactions.EffectiveDate;
                string Effective = model.Header.Employee_Payroll_Transactions.TransactionDate.ToString();
                new_Record.EffectiveMonth = DateTime.Parse(Effective).Month;
                new_Record.EffectiveYear = DateTime.Parse(Effective).Year;
                new_Record.TransactionValue = model.Header.Employee_Payroll_Transactions.TransactionValue;
                new_Record.CreatedDate = DateTime.Now.Date;
                new_Record.CreatedBy = User.Identity.Name;
                new_Record.ReportAsReadyDate = model.Header.Employee_Payroll_Transactions.ReportAsReadyDate;
                new_Record.ReportAsReadyBy = model.Header.Employee_Payroll_Transactions.ReportAsReadyBy;
                new_Record.ApprovedBy = model.Header.Employee_Payroll_Transactions.ApprovedBy;
                new_Record.ApprovedDate = model.Header.Employee_Payroll_Transactions.ApprovedDate;
                new_Record.RejectedBy = model.Header.Employee_Payroll_Transactions.RejectedBy;
                new_Record.RejectedDate = model.Header.Employee_Payroll_Transactions.RejectedDate;
                new_Record.CanceledBy = model.Header.Employee_Payroll_Transactions.CanceledBy;
                new_Record.CanceledDate = model.Header.Employee_Payroll_Transactions.CanceledDate;
                new_Record.CompletedBy = model.Header.Employee_Payroll_Transactions.CompletedBy;
                new_Record.CompletedDate = model.Header.Employee_Payroll_Transactions.CompletedDate;
                new_Record.SourceDocumentRefrence = model.Header.Employee_Payroll_Transactions.SourceDocumentRefrence;
                new_Record.SourceDocumentDescription = model.Header.Employee_Payroll_Transactions.SourceDocumentDescription;
                new_Record.SourceDocumentNotes = model.Header.Employee_Payroll_Transactions.SourceDocumentNotes;
                new_Record.TransactionNotes = model.Header.Employee_Payroll_Transactions.TransactionNotes;
                new_Record.CostCenterCode = model.Header.Employee_Payroll_Transactions.CostCenterCode;
                new_Record.ExtendedFields_Code = model.Header.Employee_Payroll_Transactions.ExtendedFields_Code;
                new_Record.Company_ID = model.Header.Employee_Payroll_Transactions.Company_ID;
                new_Record.Modified_By = User.Identity.Name;
                new_Record.Modified_Date = DateTime.Now.Date;
                new_Record.Created_By = model.Header.Employee_Payroll_Transactions.Created_By;
                new_Record.Created_Date = model.Header.Employee_Payroll_Transactions.Created_Date;
                new_Record.SourceDocumentType = Payment_Type_Source_Document.Manual.GetHashCode();
                //new_Record.TransactionStatus = check_status.Created.GetHashCode();
                dbcontext.SaveChanges();
                ///////////delete//////////
                var update_details = dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.Where(m => m.TransactionNumber == new_Record.TransactionNumber).ToList();
                dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.RemoveRange(update_details);
                dbcontext.SaveChanges();
                ///////////////////add///////
                var codeid = form["codeid"].Split(',');
                var SalaryDes = form["SalaryDes"].Split(',');
                var ValueType = form["ValueType"].Split(',');
                var DefaultValue = form["DefaultValue"].Split(',');

                for (var i = 0; i < codeid.Length; i++)
                {
                    if (codeid[i] != "")
                    {
                        var value = double.Parse(DefaultValue[i]);
                        var valuetype = ValueType[i];
                        var new_details = new Employee_Payroll_Transactions_ExtendedFieldsDetail { TransactionNumber = new_Record.TransactionNumber, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Detail_Code = codeid[i], Detail_Desc = SalaryDes[i], ValueType = valuetype, Value = value };
                        dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.Add(new_details);
                        dbcontext.SaveChanges();
                    }

                }

                ////////////////
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        public ActionResult delete(int id)
        {
            try
            {
                var model = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult delete_method(int id)
        {
            var header = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == id);
            var H_ = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == header.ID);
            var sta = dbcontext.status.FirstOrDefault(m => m.ID == H_.statID);
            if (sta.statu == Models.Infra.check_status.Approved || sta.statu == Models.Infra.check_status.Rejected || sta.statu == Models.Infra.check_status.Closed || sta.statu == Models.Infra.check_status.Recervied || sta.statu == Models.Infra.check_status.Canceled)
            {
                TempData["message"] = HR.Resource.training.status_message;
                return RedirectToAction("index");
            }
            var details = dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.Where(m => m.TransactionNumber == header.ID.ToString()).ToList();
            var status = dbcontext.status.FirstOrDefault(m => m.ID == header.statID);

            try
            {
                dbcontext.Employee_Payroll_Transactions.Remove(header);
                if (details.Count > 0)
                    dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.RemoveRange(details);

                dbcontext.status.Remove(status);

                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(header);
            }
        }
        public ActionResult status(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == ID);
                var st = dbcontext.status.FirstOrDefault(m => m.ID == model.statID);
                ViewBag.statue = dbcontext.status.ToList().Select(m => new { code = m.approved_by });
                var my_model = new employeestate { status = st, empid = ID };
                if (st.approved_by == null)
                    st.approved_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (st.Rejected_by == null)
                    st.Rejected_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (st.return_to_reviewby == null)
                    st.return_to_reviewdate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (st.cancaled_by == null)
                    st.cancaled_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (st.closed_by == null)
                    st.closed_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                if (st.Recervied_by == null)
                    st.Recervied_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                return View(my_model);
            }
            catch (Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult status(employeestate model)
        {
            var sta = dbcontext.status.FirstOrDefault(m => m.ID == model.status.ID);
            var record = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == model.empid);
            //var committe = dbcontext.Committe_Resolution_Recuirtment.Select(a => a.Committe_Resolution_Status).ToList();
            if (model.check_status == HR.Models.Infra.check_status.Approved)
            {
                sta.approved_by = User.Identity.GetUserName();
                sta.approved_bydate = model.status.approved_bydate;
                sta.statu = Models.Infra.check_status.Approved;
                record.check_status = HR.Models.Infra.check_status.Approved;
                record.name_state = nameof(check_status.Approved);
                record.TransactionStatus = check_status.Approved.GetHashCode();
                record.ApprovedBy = User.Identity.Name;
                record.ApprovedDate = DateTime.Now.Date;

                dbcontext.SaveChanges();
            }
            else if (model.check_status == HR.Models.Infra.check_status.Rejected)
            {
                sta.Rejected_by = User.Identity.GetUserName();
                sta.Rejected_bydate = model.status.Rejected_bydate;
                sta.statu = Models.Infra.check_status.Rejected;
                record.check_status = HR.Models.Infra.check_status.Rejected;
                record.name_state = nameof(check_status.Rejected);
                record.TransactionStatus = check_status.Rejected.GetHashCode();
                record.RejectedBy = User.Identity.Name;
                record.RejectedDate = DateTime.Now.Date;
                dbcontext.SaveChanges();
            }
            else if (model.check_status == HR.Models.Infra.check_status.Return_To_Review)
            {
                sta.return_to_reviewby = User.Identity.GetUserName();
                sta.return_to_reviewdate = model.status.return_to_reviewdate;
                sta.statu = Models.Infra.check_status.Return_To_Review;
                record.check_status = HR.Models.Infra.check_status.Return_To_Review;
                record.name_state = nameof(check_status.Return_To_Review);
                record.TransactionStatus = check_status.Return_To_Review.GetHashCode();
                record.ReportAsReadyBy = User.Identity.Name;
                record.ReportAsReadyDate = DateTime.Now.Date;
                dbcontext.SaveChanges();
            }
            else if (model.check_status == HR.Models.Infra.check_status.Closed)
            {
                sta.closed_by = User.Identity.GetUserName();
                sta.closed_bydate = model.status.closed_bydate;
                sta.statu = HR.Models.Infra.check_status.Closed;
                dbcontext.SaveChanges();
                record.check_status = HR.Models.Infra.check_status.Closed;
                record.name_state = nameof(check_status.Closed);
                record.TransactionStatus = check_status.Closed.GetHashCode();
                dbcontext.SaveChanges();

            }
            else if (model.check_status == HR.Models.Infra.check_status.Canceled)
            {
                sta.cancaled_by = User.Identity.GetUserName();
                sta.cancaled_bydate = model.status.cancaled_bydate;
                sta.statu = Models.Infra.check_status.Canceled;
                dbcontext.SaveChanges();
                record.check_status = HR.Models.Infra.check_status.Canceled;
                record.name_state = nameof(check_status.Canceled);
                record.TransactionStatus = check_status.Canceled.GetHashCode();
                record.CanceledBy = User.Identity.Name;
                record.CanceledDate = DateTime.Now.Date;
                dbcontext.SaveChanges();
            }
            else if (model.check_status == HR.Models.Infra.check_status.Recervied)
            {
                sta.Recervied_by = User.Identity.GetUserName();
                sta.Recervied_bydate = model.status.Recervied_bydate;
                sta.statu = Models.Infra.check_status.Recervied;
                dbcontext.SaveChanges();
                record.check_status = HR.Models.Infra.check_status.Recervied;
                record.name_state = nameof(check_status.Recervied);
                record.TransactionStatus = check_status.Recervied.GetHashCode();
                dbcontext.SaveChanges();
            }

            return RedirectToAction("index");
        }
        public JsonResult Getalll(List<string> c)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var Employee_Payroll_Transactions = dbcontext.Employee_Payroll_Transactions.ToList();
                var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();
                var salary_code = dbcontext.salary_code.ToList();
                var model = from a in Employee_Payroll_Transactions
                            join b in Employee_Profile on a.Employee_Code equals b.Code
                            join s in salary_code on a.SalaryCodeID equals s.SalaryCodeID
                            select new VM_Employee_Payroll_Transactions
                            { EmployeeName = b.Full_Name, SalaryCodeDesc = s.SalaryCodeDesc, Employee_Payroll_Transactions = a };

                return Json(model);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult Getone(int? from, int to, List<string> status)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var nn = new List<HR.Models.Infra.check_status>();
                List<Employee_Payroll_Transactions> re1 = new List<Employee_Payroll_Transactions>();
                foreach (var item in status)
                {
                    if (item != "all")
                    {
                        nn.Add((HR.Models.Infra.check_status)Enum.Parse(typeof(HR.Models.Infra.check_status), item));
                    }
                }
                if (status[0] == "all")
                {
                    var req = dbcontext.Employee_Payroll_Transactions.Where(m => m.EffectiveMonth == from && m.EffectiveYear == to).ToList();
                    var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();
                    var salary_code = dbcontext.salary_code.ToList();
                    var model = from a in req
                                join b in Employee_Profile on a.Employee_Code equals b.Code
                                join c in salary_code on a.SalaryCodeID equals c.SalaryCodeID
                                select new VM_Employee_Payroll_Transactions
                                { EmployeeName = b.Full_Name, SalaryCodeDesc = c.SalaryCodeDesc, Employee_Payroll_Transactions = a };
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else if (status[0] != "all")
                {
                    var req = dbcontext.Employee_Payroll_Transactions.Where(m => m.EffectiveMonth == from && m.EffectiveYear == to).ToList();
                    var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();
                    var salary_code = dbcontext.salary_code.ToList();
                  
                 
                    foreach (var item in nn)
                    {
                        re1.AddRange(req.Where(m => m.check_status == item).ToList());
                    }
                        var modelss = from a in re1
                                      join b in Employee_Profile on a.Employee_Code equals b.Code
                                      join c in salary_code on a.SalaryCodeID equals c.SalaryCodeID
                                      select new VM_Employee_Payroll_Transactions
                                      { EmployeeName = b.Full_Name, SalaryCodeDesc = c.SalaryCodeDesc, Employee_Payroll_Transactions = a };                                    
                    return Json(modelss, JsonRequestBehavior.AllowGet);
                }

                return Json(re1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        [HttpPost]
        public JsonResult getallstatues()
        {
            var list = new List<string>();
            list.Add("created");
            list.Add("Canceled");
            list.Add("Rejected");
            list.Add("Approved");
            list.Add("Return_To_Review");
            return Json(list);
        }

        public ActionResult ImportTransactionEntryFromExcel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ImportTransactionEntryFromExcel(HttpPostedFileBase MyItem)
        {
            try
            {
                if (MyItem == null)
                {
                }
                else if (MyItem.FileName != null)
                {
                    string folderpath = Server.MapPath("~/TransactionEntryFromExcel/");
                    Directory.CreateDirectory(folderpath);
                    string FilePath = folderpath;
                    string filename = MyItem.FileName + Guid.NewGuid() + Path.GetExtension(MyItem.FileName);
                    string p = FilePath + "/" + filename;
                    MyItem.SaveAs(p);
                    var FileCombine = Path.Combine(p);
                    var Listt = ReadExcelFile(FileCombine);
                    TempData["list"] = Listt;
                    return RedirectToAction("ViewExecl", "Employee_Payroll_Transactions");
                }
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public List<EmpVM> ReadExcelFile(string FilePath)
        {
            var xlApp = new Application();
            var xlWorkBook = xlApp.Workbooks.Open(FilePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            var xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            var range = xlWorkSheet.UsedRange;
            var ExcelList = GetExcelList((range.Rows as Range).Value2);
            return ExcelList;
        }
        public object GetExcelList(object[,] TwoDArray)
        {
            var empList = new List<EmpVM>();

            //Getting the no of rows of 2d array 
            int NoOfRows = TwoDArray.GetLength(0);
            //Getting the no of columns of the 2d array
            int NoOfColumns = TwoDArray.GetLength(1);

            for (int R = 2; R <= NoOfRows; R++)
            {
                for (int C = 1; C <= NoOfColumns; C++)
                {
                    var emp = new EmpVM();
                    emp.EmployeeCode = TwoDArray[R, C].ToString();
                    C++;
                    emp.EmployeeName = TwoDArray[R, C].ToString();
                    C++;
                    emp.SalaryCode = TwoDArray[R, C].ToString();
                    C++;
                    emp.SalaryCodeDesc = TwoDArray[R, C].ToString();
                    C++;
                    var tra = TwoDArray[R, C].ToString();
                    double d = double.Parse(tra);
                    DateTime conv = DateTime.FromOADate(d);
                    emp.TransactionDate = conv;
                    C++;
                    var val = TwoDArray[R, C].ToString();
                    double D = double.Parse(val);
                    DateTime Convert = DateTime.FromOADate(D);
                    emp.EffectiveDate = Convert;
                    empList.Add(emp);
                }
            }
            return empList;
        }

        public ActionResult PayrollTransactionsVerticalBatch()
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.SalaryCodeGroup_Header = dbcontext.SalaryCodeGroup_Header.Where(a => a.GroupPurpose == 2).ToList().Select(m => new { Code = m.CodeGroupID + "-[" + m.CodeGroupDesc + ']', ID = m.ID });
                ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });

                DateTime Statis = Convert.ToDateTime("1/1/1900");
                var new_record = new Employee_Payroll_Transactions { TransactionValue = 0, EffectiveDate = DateTime.Now, TransactionDate = DateTime.Now, Created_Date = DateTime.Now, Created_By = User.Identity.Name, CreatedDate = Statis, ReportAsReadyDate = Statis, ApprovedDate = Statis, RejectedDate = Statis, CanceledDate = Statis, CompletedDate = Statis, Modified_Date = Statis };
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                var model_ = dbcontext.Employee_Payroll_Transactions.ToList();
                if (model_.Count() == 0)
                {
                    new_record.TransactionNumber = stru + "1";
                }
                else
                {
                    new_record.TransactionNumber = stru + (model_.LastOrDefault().ID + 1).ToString();
                }

                var model = new Headers { Employee_Payroll_Transactions = new_record, check_status = check_status.Created, Payment_Type_Source_Document = Payment_Type_Source_Document.Manual, code_value_type = code_value_type.Unkown, code_type = code_type.Earning };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult PayrollTransactionsVerticalBatch(Headers model, FormCollection form)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.SalaryCodeGroup_Header = dbcontext.SalaryCodeGroup_Header.Where(a => a.GroupPurpose == 2).ToList().Select(m => new { Code = m.CodeGroupID + "-[" + m.CodeGroupDesc + ']', ID = m.ID });
                ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });

                ///////////////////
                var codeid = form["codeid"].Split(',');
                var DefaultValue = form["DefaultValue"].Split(',');

                for (var i = 0; i < codeid.Length; i++)
                {
                    if (codeid[i] != "")
                    {
                        Employee_Payroll_Transactions new_Record = new Employee_Payroll_Transactions();

                        int EmpID = int.Parse(model.Employee_Payroll_Transactions.Employee_Code);
                        if (model.Employee_Payroll_Transactions.Employee_Code != null)
                        {
                            var empcode = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == EmpID).Code;
                            new_Record.Employee_Code = empcode;
                        }

                        var salcode = codeid[i];
                        new_Record.SalaryCodeID = salcode;
                        var value = double.Parse(DefaultValue[i]);
                        new_Record.TransactionValue = value;
                        var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                        var model_ = dbcontext.Employee_Payroll_Transactions.ToList();
                        if (model_.Count() == 0)
                        {
                            new_Record.TransactionNumber = stru + "1";
                        }
                        else
                        {
                            new_Record.TransactionNumber = stru + (model_.LastOrDefault().ID + 1).ToString();
                        }
                        new_Record.JournalName_BatchCode = model.Employee_Payroll_Transactions.JournalName_BatchCode;
                        new_Record.Contract_Number = model.Employee_Payroll_Transactions.Contract_Number;
                        new_Record.TransactionDate = model.Employee_Payroll_Transactions.TransactionDate;
                        string Transaction = model.Employee_Payroll_Transactions.TransactionDate.ToString();
                        new_Record.TransactionMonth = DateTime.Parse(Transaction).Month;
                        new_Record.TransactionYear = DateTime.Parse(Transaction).Year;
                        new_Record.EffectiveDate = model.Employee_Payroll_Transactions.EffectiveDate;
                        string Effective = model.Employee_Payroll_Transactions.TransactionDate.ToString();
                        new_Record.EffectiveMonth = DateTime.Parse(Effective).Month;
                        new_Record.EffectiveYear = DateTime.Parse(Effective).Year;
                        new_Record.CreatedDate = DateTime.Now.Date;
                        new_Record.CreatedBy = User.Identity.Name;
                        DateTime Statis = Convert.ToDateTime("1/1/1900");
                        new_Record.ReportAsReadyDate = Statis;
                        new_Record.ReportAsReadyBy = model.Employee_Payroll_Transactions.ReportAsReadyBy;
                        new_Record.ApprovedBy = model.Employee_Payroll_Transactions.ApprovedBy;
                        new_Record.ApprovedDate = Statis;
                        new_Record.RejectedBy = model.Employee_Payroll_Transactions.RejectedBy;
                        new_Record.RejectedDate = Statis;
                        new_Record.CanceledBy = model.Employee_Payroll_Transactions.CanceledBy;
                        new_Record.CanceledDate = Statis;
                        new_Record.CompletedBy = model.Employee_Payroll_Transactions.CompletedBy;
                        new_Record.CompletedDate = Statis;
                        new_Record.SourceDocumentRefrence = model.Employee_Payroll_Transactions.SourceDocumentRefrence;
                        new_Record.SourceDocumentDescription = model.Employee_Payroll_Transactions.SourceDocumentDescription;
                        new_Record.SourceDocumentNotes = model.Employee_Payroll_Transactions.SourceDocumentNotes;
                        new_Record.TransactionNotes = model.Employee_Payroll_Transactions.TransactionNotes;
                        new_Record.CostCenterCode = model.Employee_Payroll_Transactions.CostCenterCode;
                        new_Record.ExtendedFields_Code = model.Employee_Payroll_Transactions.ExtendedFields_Code;
                        new_Record.Company_ID = model.Employee_Payroll_Transactions.Company_ID;
                        new_Record.Created_By = User.Identity.Name;
                        new_Record.Created_Date = DateTime.Now.Date;
                        new_Record.Modified_By = model.Employee_Payroll_Transactions.Modified_By;
                        new_Record.Modified_Date = Statis;
                        new_Record.SourceDocumentType = Payment_Type_Source_Document.Manual.GetHashCode();
                        new_Record.TransactionStatus = check_status.Created.GetHashCode();

                        new_Record.check_status = HR.Models.Infra.check_status.created;
                        new_Record.name_state = nameof(check_status.Created);
                        var username = User.Identity.GetUserName();
                        var Date = Convert.ToDateTime("1/1/1900");
                        var s = new status { statu = HR.Models.Infra.check_status.created, created_by = username, Type = Models.Infra.Type.Employee_Payroll_Transactions, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                        var st = dbcontext.status.Add(s);
                        dbcontext.SaveChanges();
                        new_Record.statID = s.ID;

                        var Header = dbcontext.Employee_Payroll_Transactions.Add(new_Record);
                        dbcontext.SaveChanges();
                    }
                }

                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        public ActionResult PayrollTransactionHorizontalBatch()
        {
            try
            {
                List<SelectListItem> items = new List<SelectListItem>();
                items.Insert(0, (new SelectListItem
                {
                    Text = "All employee",
                    Value = "1",
                }));
                items.Insert(1, (new SelectListItem
                {
                    Text = "unit",
                    Value = "2",
                }));
                items.Insert(2, (new SelectListItem
                {
                    Text = "nationality",
                    Value = "3",
                }));
                items.Insert(3, (new SelectListItem
                {
                    Text = "Work location",
                    Value = "4",
                }));
                items.Insert(4, (new SelectListItem
                {
                    Text = "Cost center",
                    Value = "5",

                }));
                items.Insert(5, (new SelectListItem
                {
                    Text = "Cadre level",
                    Value = "6",

                }));
                ViewBag.Object = new SelectList(items, "Value", "Text");

                ViewBag.salary_code = dbcontext.salary_code.Where(a => a.SourceEntry == 2).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });
                ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });
                DateTime Statis = Convert.ToDateTime("1/1/1900");
                var new_record = new Employee_Payroll_Transactions { TransactionValue = 0, EffectiveDate = DateTime.Now, TransactionDate = DateTime.Now, Created_Date = DateTime.Now, Created_By = User.Identity.Name, CreatedDate = Statis, ReportAsReadyDate = Statis, ApprovedDate = Statis, RejectedDate = Statis, CanceledDate = Statis, CompletedDate = Statis, Modified_Date = Statis };
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                var model_ = dbcontext.Employee_Payroll_Transactions.ToList();
                if (model_.Count() == 0)
                {
                    new_record.TransactionNumber = stru + "1";
                }
                else
                {
                    new_record.TransactionNumber = stru + (model_.LastOrDefault().ID + 1).ToString();
                }

                var model = new Headers { Employee_Payroll_Transactions = new_record, check_status = check_status.Created, Payment_Type_Source_Document = Payment_Type_Source_Document.Manual, code_value_type = code_value_type.Unkown, code_type = code_type.Earning };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult PayrollTransactionHorizontalBatch(Headers model, FormCollection form)
        {
            try
            {
                List<SelectListItem> items = new List<SelectListItem>();
                items.Insert(0, (new SelectListItem
                {
                    Text = "All employee",
                    Value = "1",
                }));
                items.Insert(1, (new SelectListItem
                {
                    Text = "unit",
                    Value = "2",
                }));
                items.Insert(2, (new SelectListItem
                {
                    Text = "nationality",
                    Value = "3",
                }));
                items.Insert(3, (new SelectListItem
                {
                    Text = "Work location",
                    Value = "4",
                }));
                items.Insert(4, (new SelectListItem
                {
                    Text = "Cost center",
                    Value = "5",

                }));
                items.Insert(5, (new SelectListItem
                {
                    Text = "Cadre level",
                    Value = "6",

                }));
                ViewBag.Object = new SelectList(items, "Value", "Text");

                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.salary_code = dbcontext.salary_code.Where(a => a.SourceEntry == 2).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });

                var empcode = form["empcode"].Split(',');
                var transactionval = form["transactionval"].Split(',');

                for (var e = 0; e < empcode.Length; e++)
                {
                    if (empcode[e] != "")
                    {
                        Employee_Payroll_Transactions new_Record = new Employee_Payroll_Transactions();

                        var emp = empcode[e];
                        new_Record.Employee_Code = emp;

                        int SalID = int.Parse(model.Employee_Payroll_Transactions.SalaryCodeID);
                        if (model.Employee_Payroll_Transactions.SalaryCodeID != null)
                        {
                            var Salcode = dbcontext.salary_code.FirstOrDefault(a => a.ID == SalID).SalaryCodeID;
                            new_Record.SalaryCodeID = Salcode;
                        }
                        var values = double.Parse(transactionval[e]);
                        new_Record.TransactionValue = values;
                        var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                        var model_ = dbcontext.Employee_Payroll_Transactions.ToList();
                        if (model_.Count() == 0)
                        {
                            new_Record.TransactionNumber = stru + "1";
                        }
                        else
                        {
                            new_Record.TransactionNumber = stru + (model_.LastOrDefault().ID + 1).ToString();
                        }
                        new_Record.JournalName_BatchCode = model.Employee_Payroll_Transactions.JournalName_BatchCode;
                        new_Record.Contract_Number = model.Employee_Payroll_Transactions.Contract_Number;
                        new_Record.TransactionDate = model.Employee_Payroll_Transactions.TransactionDate;
                        string Transaction = model.Employee_Payroll_Transactions.TransactionDate.ToString();
                        new_Record.TransactionMonth = DateTime.Parse(Transaction).Month;
                        new_Record.TransactionYear = DateTime.Parse(Transaction).Year;
                        new_Record.EffectiveDate = model.Employee_Payroll_Transactions.EffectiveDate;
                        string Effective = model.Employee_Payroll_Transactions.TransactionDate.ToString();
                        new_Record.EffectiveMonth = DateTime.Parse(Effective).Month;
                        new_Record.EffectiveYear = DateTime.Parse(Effective).Year;
                        new_Record.CreatedDate = DateTime.Now.Date;
                        new_Record.CreatedBy = User.Identity.Name;
                        DateTime Statis = Convert.ToDateTime("1/1/1900");
                        new_Record.ReportAsReadyDate = Statis;
                        new_Record.ReportAsReadyBy = model.Employee_Payroll_Transactions.ReportAsReadyBy;
                        new_Record.ApprovedBy = model.Employee_Payroll_Transactions.ApprovedBy;
                        new_Record.ApprovedDate = Statis;
                        new_Record.RejectedBy = model.Employee_Payroll_Transactions.RejectedBy;
                        new_Record.RejectedDate = Statis;
                        new_Record.CanceledBy = model.Employee_Payroll_Transactions.CanceledBy;
                        new_Record.CanceledDate = Statis;
                        new_Record.CompletedBy = model.Employee_Payroll_Transactions.CompletedBy;
                        new_Record.CompletedDate = Statis;
                        new_Record.SourceDocumentRefrence = model.Employee_Payroll_Transactions.SourceDocumentRefrence;
                        new_Record.SourceDocumentDescription = model.Employee_Payroll_Transactions.SourceDocumentDescription;
                        new_Record.SourceDocumentNotes = model.Employee_Payroll_Transactions.SourceDocumentNotes;
                        new_Record.TransactionNotes = model.Employee_Payroll_Transactions.TransactionNotes;
                        new_Record.CostCenterCode = model.Employee_Payroll_Transactions.CostCenterCode;
                        new_Record.ExtendedFields_Code = model.Employee_Payroll_Transactions.ExtendedFields_Code;
                        new_Record.Company_ID = model.Employee_Payroll_Transactions.Company_ID;
                        new_Record.Created_By = User.Identity.Name;
                        new_Record.Created_Date = DateTime.Now.Date;
                        new_Record.Modified_By = model.Employee_Payroll_Transactions.Modified_By;
                        new_Record.Modified_Date = Statis;
                        new_Record.SourceDocumentType = Payment_Type_Source_Document.Manual.GetHashCode();
                        new_Record.TransactionStatus = check_status.Created.GetHashCode();

                        new_Record.check_status = HR.Models.Infra.check_status.created;
                        new_Record.name_state = nameof(check_status.Created);
                        var username = User.Identity.GetUserName();
                        var Date = Convert.ToDateTime("1/1/1900");
                        var s = new status { statu = HR.Models.Infra.check_status.created, created_by = username, Type = Models.Infra.Type.Employee_Payroll_Transactions, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                        var st = dbcontext.status.Add(s);
                        dbcontext.SaveChanges();
                        new_Record.statID = s.ID;

                        var Header = dbcontext.Employee_Payroll_Transactions.Add(new_Record);
                        dbcontext.SaveChanges();
                        var codeid = form["codeid"].Split(',');
                        var SalaryDes = form["SalaryDes"].Split(',');
                        var ValueType = form["ValueType"].Split(',');
                        var DefaultValue = form["DefaultValue"].Split(',');

                        for (var i = 0; i < codeid.Length; i++)
                        {
                            if (codeid[i] != "")
                            {
                                var value = double.Parse(DefaultValue[i]);
                                var valuetype = ValueType[i];
                                var new_details = new Employee_Payroll_Transactions_ExtendedFieldsDetail { TransactionNumber = Header.TransactionNumber, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Detail_Code = codeid[i], Detail_Desc = SalaryDes[i], ValueType = valuetype, Value = value };
                                dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.Add(new_details);
                                dbcontext.SaveChanges();
                            }
                        }
                    }
                }

                return RedirectToAction("index");
            }

            catch (Exception e)
            {
                return View(model);
            }
        }

        public class EmpVM
        {
            public string EmployeeCode { get; set; }
            public string EmployeeName { get; set; }
            public string SalaryCode { get; set; }
            public string SalaryCodeDesc { get; set; }
            //public string CostCenterCode { get; set; }
            //public string CostCenterDesc { get; set; }
            public DateTime TransactionDate { get; set; }
            public DateTime EffectiveDate { get; set; }

        }
        public class VMs
        {
            public Headers Header { get; set; }
            public List<Employee_Payroll_Transactions_ExtendedFieldsDetail> Employee_Payroll_Transactions_ExtendedFieldsDetail { get; set; }
        }
        public class Headers
        {
            public Employee_Payroll_Transactions Employee_Payroll_Transactions { get; set; }
            public check_status check_status { get; set; }
            public Payment_Type_Source_Document Payment_Type_Source_Document { get; set; }
            public code_type code_type { get; set; }
            public code_value_type code_value_type { get; set; }

        }
        public enum check_status
        {
            Created = 1,
            Return_To_Review = 2,
            Approved = 3,
            Rejected = 4,
            Canceled = 5,
            Recervied = 6,
            Closed = 7
        }
        public enum Payment_Type_Source_Document
        {
            Manual = 1,
            Loan = 2,
            Vacation = 3,
            EOS = 4,
            Variable_compensation = 5,
            Fixed_compensation = 6,
            Retro_active = 7,
            Business_trip = 8,
            Government_expense = 9,
            Air_Lines_Booking_expense = 10,
            HR_records = 11,
            Miscellaneous = 12,
        }
        public enum code_type
        {
            Earning = 1,
            Deduction = 2
        }
        public enum code_value_type
        {
            Unkown = 1,
            Minites = 2,
            Hours = 3,
            Days = 4,
            Months = 5,
            Years = 6,
            Money = 7,
            Piece = 8,
            [Display(Name = "Calculated Value")]
            Calculated_Value = 9
        }
        public class VM_Employee_Payroll_Transactions
        {
            public Employee_Payroll_Transactions Employee_Payroll_Transactions { get; set; }
            public string EmployeeName { get; set; }
            public string SalaryCodeDesc { get; set; }
        }
    }
}