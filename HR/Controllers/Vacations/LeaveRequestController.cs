using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models.Infra;
using HR.Models.ViewModel;
using HR.Models;
using System.Data.Entity.Infrastructure;
using HR.Models.Vacations;
using Microsoft.AspNet.Identity;

namespace HR.Controllers.Vacations
{
    [Authorize]
    public class LeaveRequestController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: LeaveRequest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult create()
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
                ViewBag.Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();
                //DateTime Statis = DateTime.Now;
                var new_record = new LeavesRequestMaster { DateFrom = DateTime.Now, DateTo = DateTime.Now, ActualToDate = DateTime.Now, CurrentDate = DateTime.Now, Created_Date = DateTime.Now, Created_By = User.Identity.Name  ,RemainDays = 0 , TotalDays = 0};
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                var model_ = dbcontext.LeavesRequestMaster.ToList();
                if (model_.Count() == 0)
                {
                    new_record.SerialNo = stru + "1";
                }
                else
                {
                    new_record.SerialNo = stru + (model_.LastOrDefault().ID + 1).ToString();
                }

                var model = new Headers { LeavesRequestMaster = new_record, check_status = check_status.Pending };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        //[HttpPost]
        //public ActionResult Create(Headers model, FormCollection form)
        //{
        //    try
        //    {
    //     if (reted <= -1)
    //        {
    //            TempData["Message"] = "Balance Is Not Enough";
    //            return View(model);
    //}

    //        ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
    //        ViewBag.salary_code = dbcontext.salary_code.Where(a => a.SourceEntry == 2).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });
    //        ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });

    //        Employee_Payroll_Transactions new_Record = new Employee_Payroll_Transactions();

    //        new_Record.TransactionNumber = model.Employee_Payroll_Transactions.TransactionNumber;
    //        int EmpID = int.Parse(model.Employee_Payroll_Transactions.Employee_Code);
    //        if (model.Employee_Payroll_Transactions.Employee_Code != null)
    //        {
    //            var empcode = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == EmpID).Code;
    //            new_Record.Employee_Code = empcode;
    //        }
    //        int SalID = int.Parse(model.Employee_Payroll_Transactions.SalaryCodeID);
    //        if (model.Employee_Payroll_Transactions.SalaryCodeID != null)
    //        {
    //            var Salcode = dbcontext.salary_code.FirstOrDefault(a => a.ID == SalID).SalaryCodeID;
    //            new_Record.SalaryCodeID = Salcode;
    //        }
    //        new_Record.JournalName_BatchCode = model.Employee_Payroll_Transactions.JournalName_BatchCode;
    //        new_Record.Contract_Number = model.Employee_Payroll_Transactions.Contract_Number;
    //        new_Record.TransactionDate = model.Employee_Payroll_Transactions.TransactionDate;
    //        string Transaction = model.Employee_Payroll_Transactions.TransactionDate.ToString();
    //        new_Record.TransactionMonth = DateTime.Parse(Transaction).Month;
    //        new_Record.TransactionYear = DateTime.Parse(Transaction).Year;
    //        new_Record.EffectiveDate = model.Employee_Payroll_Transactions.EffectiveDate;
    //        string Effective = model.Employee_Payroll_Transactions.TransactionDate.ToString();
    //        new_Record.EffectiveMonth = DateTime.Parse(Effective).Month;
    //        new_Record.EffectiveYear = DateTime.Parse(Effective).Year;
    //        new_Record.TransactionValue = model.Employee_Payroll_Transactions.TransactionValue;
    //        new_Record.CreatedDate = DateTime.Now.Date;
    //        new_Record.CreatedBy = User.Identity.Name;
    //        new_Record.ReportAsReadyDate = model.Employee_Payroll_Transactions.ReportAsReadyDate;
    //        new_Record.ReportAsReadyBy = model.Employee_Payroll_Transactions.ReportAsReadyBy;
    //        new_Record.ApprovedBy = model.Employee_Payroll_Transactions.ApprovedBy;
    //        new_Record.ApprovedDate = model.Employee_Payroll_Transactions.ApprovedDate;
    //        new_Record.RejectedBy = model.Employee_Payroll_Transactions.RejectedBy;
    //        new_Record.RejectedDate = model.Employee_Payroll_Transactions.RejectedDate;
    //        new_Record.CanceledBy = model.Employee_Payroll_Transactions.CanceledBy;
    //        new_Record.CanceledDate = model.Employee_Payroll_Transactions.CanceledDate;
    //        new_Record.CompletedBy = model.Employee_Payroll_Transactions.CompletedBy;
    //        new_Record.CompletedDate = model.Employee_Payroll_Transactions.CompletedDate;
    //        new_Record.SourceDocumentRefrence = model.Employee_Payroll_Transactions.SourceDocumentRefrence;
    //        new_Record.SourceDocumentDescription = model.Employee_Payroll_Transactions.SourceDocumentDescription;
    //        new_Record.SourceDocumentNotes = model.Employee_Payroll_Transactions.SourceDocumentNotes;
    //        new_Record.TransactionNotes = model.Employee_Payroll_Transactions.TransactionNotes;
    //        new_Record.CostCenterCode = model.Employee_Payroll_Transactions.CostCenterCode;
    //        new_Record.ExtendedFields_Code = model.Employee_Payroll_Transactions.ExtendedFields_Code;
    //        new_Record.Company_ID = model.Employee_Payroll_Transactions.Company_ID;
    //        new_Record.Created_By = User.Identity.Name;
    //        new_Record.Created_Date = DateTime.Now.Date;
    //        new_Record.Modified_By = model.Employee_Payroll_Transactions.Modified_By;
    //        new_Record.Modified_Date = model.Employee_Payroll_Transactions.Modified_Date;
    //        new_Record.SourceDocumentType = Payment_Type_Source_Document.Manual.GetHashCode();
    //        new_Record.TransactionStatus = check_status.Created.GetHashCode();

    //        new_Record.check_status = HR.Models.Infra.check_status.created;
    //        new_Record.name_state = nameof(check_status.Created);
    //        var username = User.Identity.GetUserName();
    //        var Date = Convert.ToDateTime("1/1/1900");
    //        var s = new status { statu = HR.Models.Infra.check_status.created, created_by = username, Type = Models.Infra.Type.Employee_Payroll_Transactions, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
    //        var st = dbcontext.status.Add(s);
    //        dbcontext.SaveChanges();
    //        new_Record.statID = s.ID;

    //        var Header = dbcontext.Employee_Payroll_Transactions.Add(new_Record);
    //        dbcontext.SaveChanges();

    //        ///////////////////
    //        var codeid = form["codeid"].Split(',');
    //        var SalaryDes = form["SalaryDes"].Split(',');
    //        var ValueType = form["ValueType"].Split(',');
    //        var DefaultValue = form["DefaultValue"].Split(',');

    //        for (var i = 0; i < codeid.Length; i++)
    //        {
    //            if (codeid[i] != "")
    //            {
    //                var value = double.Parse(DefaultValue[i]);
    //                var valuetype = ValueType[i];
    //                var new_details = new Employee_Payroll_Transactions_ExtendedFieldsDetail { TransactionNumber = Header.TransactionNumber, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Detail_Code = codeid[i], Detail_Desc = SalaryDes[i], ValueType = valuetype, Value = value };
    //                dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.Add(new_details);
    //                dbcontext.SaveChanges();
    //            }
    //        }

    //        return RedirectToAction("index");
    //    }
    //    catch (Exception e)
    //    {
    //        return View(model);
    //    }
    //}
    //public ActionResult edit(string id)
    //{
    //    try
    //    {
    //        var ID = int.Parse(id);
    //        ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
    //        ViewBag.salary_code = dbcontext.salary_code.Where(a => a.SourceEntry == 2).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.SalaryCodeID });
    //        ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });
    //        var x = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(a => a.ID == ID).SalaryCodeID;
    //        var y = dbcontext.salary_code.FirstOrDefault(a => a.SalaryCodeID == x).ID;
    //        ViewBag.id = y;
    //        ViewBag.code = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == ID).ID;
    //        var old_model = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == ID);
    //        var Head = new Headers { Employee_Payroll_Transactions = old_model, check_status = (check_status)old_model.TransactionStatus, Payment_Type_Source_Document = (Payment_Type_Source_Document)old_model.SourceDocumentType };
    //        var old_details = dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.Where(m => m.TransactionNumber == old_model.ID.ToString()).ToList();
    //        var new_model = new VMs { Employee_Payroll_Transactions_ExtendedFieldsDetail = old_details, Header = Head };
    //        return View(new_model);
    //    }
    //    catch (Exception)
    //    {
    //        return RedirectToAction("index");
    //    }
    //}
    //[HttpPost]
    //public ActionResult edit(VMs model, FormCollection form)
    //{
    //    try
    //    {
    //        ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
    //        ViewBag.salary_code = dbcontext.salary_code.Where(a => a.SourceEntry == 2).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.SalaryCodeID });
    //        ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });
    //        var H_ = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == model.Header.Employee_Payroll_Transactions.ID);
    //        var sta = dbcontext.status.FirstOrDefault(m => m.ID == H_.statID);
    //        if (sta.statu == Models.Infra.check_status.Approved || sta.statu == Models.Infra.check_status.Rejected || sta.statu == Models.Infra.check_status.Closed || sta.statu == Models.Infra.check_status.Recervied || sta.statu == Models.Infra.check_status.Canceled)
    //        {
    //            TempData["message"] = HR.Resource.training.status_message;
    //            return RedirectToAction("index");
    //        }
    //        ///update////
    //        var new_Record = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == model.Header.Employee_Payroll_Transactions.ID);

    //        new_Record.TransactionNumber = model.Header.Employee_Payroll_Transactions.TransactionNumber;

    //        new_Record.Employee_Code = model.Header.Employee_Payroll_Transactions.Employee_Code;

    //        new_Record.SalaryCodeID = model.Header.Employee_Payroll_Transactions.SalaryCodeID;

    //        new_Record.JournalName_BatchCode = model.Header.Employee_Payroll_Transactions.JournalName_BatchCode;
    //        new_Record.Contract_Number = model.Header.Employee_Payroll_Transactions.Contract_Number;
    //        new_Record.TransactionDate = model.Header.Employee_Payroll_Transactions.TransactionDate;
    //        string Transaction = model.Header.Employee_Payroll_Transactions.TransactionDate.ToString();
    //        new_Record.TransactionMonth = DateTime.Parse(Transaction).Month;
    //        new_Record.TransactionYear = DateTime.Parse(Transaction).Year;
    //        new_Record.EffectiveDate = model.Header.Employee_Payroll_Transactions.EffectiveDate;
    //        string Effective = model.Header.Employee_Payroll_Transactions.TransactionDate.ToString();
    //        new_Record.EffectiveMonth = DateTime.Parse(Effective).Month;
    //        new_Record.EffectiveYear = DateTime.Parse(Effective).Year;
    //        new_Record.TransactionValue = model.Header.Employee_Payroll_Transactions.TransactionValue;
    //        new_Record.CreatedDate = DateTime.Now.Date;
    //        new_Record.CreatedBy = User.Identity.Name;
    //        new_Record.ReportAsReadyDate = model.Header.Employee_Payroll_Transactions.ReportAsReadyDate;
    //        new_Record.ReportAsReadyBy = model.Header.Employee_Payroll_Transactions.ReportAsReadyBy;
    //        new_Record.ApprovedBy = model.Header.Employee_Payroll_Transactions.ApprovedBy;
    //        new_Record.ApprovedDate = model.Header.Employee_Payroll_Transactions.ApprovedDate;
    //        new_Record.RejectedBy = model.Header.Employee_Payroll_Transactions.RejectedBy;
    //        new_Record.RejectedDate = model.Header.Employee_Payroll_Transactions.RejectedDate;
    //        new_Record.CanceledBy = model.Header.Employee_Payroll_Transactions.CanceledBy;
    //        new_Record.CanceledDate = model.Header.Employee_Payroll_Transactions.CanceledDate;
    //        new_Record.CompletedBy = model.Header.Employee_Payroll_Transactions.CompletedBy;
    //        new_Record.CompletedDate = model.Header.Employee_Payroll_Transactions.CompletedDate;
    //        new_Record.SourceDocumentRefrence = model.Header.Employee_Payroll_Transactions.SourceDocumentRefrence;
    //        new_Record.SourceDocumentDescription = model.Header.Employee_Payroll_Transactions.SourceDocumentDescription;
    //        new_Record.SourceDocumentNotes = model.Header.Employee_Payroll_Transactions.SourceDocumentNotes;
    //        new_Record.TransactionNotes = model.Header.Employee_Payroll_Transactions.TransactionNotes;
    //        new_Record.CostCenterCode = model.Header.Employee_Payroll_Transactions.CostCenterCode;
    //        new_Record.ExtendedFields_Code = model.Header.Employee_Payroll_Transactions.ExtendedFields_Code;
    //        new_Record.Company_ID = model.Header.Employee_Payroll_Transactions.Company_ID;
    //        new_Record.Modified_By = User.Identity.Name;
    //        new_Record.Modified_Date = DateTime.Now.Date;
    //        new_Record.Created_By = model.Header.Employee_Payroll_Transactions.Created_By;
    //        new_Record.Created_Date = model.Header.Employee_Payroll_Transactions.Created_Date;
    //        new_Record.SourceDocumentType = Payment_Type_Source_Document.Manual.GetHashCode();
    //        //new_Record.TransactionStatus = check_status.Created.GetHashCode();
    //        dbcontext.SaveChanges();
    //        ///////////delete//////////
    //        var update_details = dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.Where(m => m.TransactionNumber == new_Record.TransactionNumber).ToList();
    //        dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.RemoveRange(update_details);
    //        dbcontext.SaveChanges();
    //        ///////////////////add///////
    //        var codeid = form["codeid"].Split(',');
    //        var SalaryDes = form["SalaryDes"].Split(',');
    //        var ValueType = form["ValueType"].Split(',');
    //        var DefaultValue = form["DefaultValue"].Split(',');

    //        for (var i = 0; i < codeid.Length; i++)
    //        {
    //            if (codeid[i] != "")
    //            {
    //                var value = double.Parse(DefaultValue[i]);
    //                var valuetype = ValueType[i];
    //                var new_details = new Employee_Payroll_Transactions_ExtendedFieldsDetail { TransactionNumber = new_Record.TransactionNumber, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Detail_Code = codeid[i], Detail_Desc = SalaryDes[i], ValueType = valuetype, Value = value };
    //                dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.Add(new_details);
    //                dbcontext.SaveChanges();
    //            }

    //        }

    //        ////////////////
    //        return RedirectToAction("index");
    //    }
    //    catch (Exception e)
    //    {
    //        return View(model);
    //    }
    //}
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
      
        public class Headers
        {
            public LeavesRequestMaster LeavesRequestMaster { get; set; }
            public check_status check_status { get; set; }
        }
        public enum check_status
        {
            Pending = 0,
            Created = 1,
            Return_To_Review = 2,
            Approved = 3,
            Rejected = 4,
            Canceled = 5,
            Recervied = 6,
            Closed = 7
        }
    }
}