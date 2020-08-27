using Antlr.Runtime;
using HR.Models;
using HR.Models.Infra;
using HR.Models.SetupPayroll;
using HR.Models.TransactionsPayroll;
using HR.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.TransactionsPayroll
{
   
    public class ManualPaymentTransactionEntryController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: ManualPaymentTransactionEntry
        [Authorize(Roles = "Admin,payroll,payrollTransaction,Payment Settlement Transaction Entry,payrollProcess,Manual Payment Settlement Action Process")]
        public ActionResult Index()
        {
            var Employee_Profile = dbcontext.Employee_Profile.ToList();
            var ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList();
            var ManualPaymentTransactionEntry = dbcontext.ManualPaymentTransactionEntry.ToList();
            var model = from a in Employee_Profile
                        join b in ManualPaymentTransactionEntry on a.ID equals int.Parse(b.Employee_Code)
                        join c in ManualPaymentTypes_Header on b.ManualPaymentType equals c.ID.ToString()
                        orderby b.ID
                        select new ManualPaymentTransactionEntryEmployee_VM
                        {
                            fullname = a.Full_Name,
                            Paymenttypedescription = c.PaymentTypeDesc,
                            EmployeeId = a.ID,
                            ManualPaymentTransactionEntry = b
                        };
            //var model = dbcontext.ManualPaymentTransactionEntry.ToList();
            return View(model);
        }
        [Authorize(Roles = "Admin,payroll,payrollTransaction,Payment Settlement Transaction Entry")]
        public ActionResult create()
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList().Select(m => new { Code = m.PaymentTypeCode + "-[" + m.PaymentTypeDesc + ']', ID = m.ID });
                var Statis = DateTime.Now;
                var new_record = new ManualPaymentTransactionEntry { EffectiveDate = Statis, TransactionDate = Statis, FromDate = Statis, ToDate = Statis, PaidDate = Statis };
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                var model_ = dbcontext.ManualPaymentTransactionEntry.ToList();
                if (model_.Count() == 0)
                {
                    new_record.TransactionNumber = stru + "1";
                }
                else
                {
                    new_record.TransactionNumber = stru + (model_.LastOrDefault().ID + 1).ToString();
                }

                var model = new HeaderManual { ManualPaymentTransactionEntry = new_record, Payment_Type_Source_Document = Payment_Type_Source_Document.Manual, check_status = check_status.Created };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(HeaderManual model, FormCollection form, string Command)
        {
            try
            {

                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList().Select(m => new { Code = m.PaymentTypeCode + "-[" + m.PaymentTypeDesc + ']', ID = m.ID });

                var new_Record = model.ManualPaymentTransactionEntry;
                new_Record.ExtendedFields_Code = model.ManualPaymentTransactionEntry.ExtendedFields_Code;
                //var ExtendedFields_Code = int.Parse(model.ManualPaymentTransactionEntry.ExtendedFields_Code);
                //var EmpObj = dbcontext.ExtendedFields_Header.FirstOrDefault(a => a.ID == ExtendedFields_Code);
                new_Record.TransactionNumber = model.ManualPaymentTransactionEntry.TransactionNumber;
                new_Record.Employee_Code = model.ManualPaymentTransactionEntry.Employee_Code;
                //new_Record.ManualPaymentType = model.ManualPaymentTransactionEntry.ManualPaymentType == null ? model.ManualPaymentTransactionEntry.ManualPaymentType = EmpObj.ID.ToString() : model.ManualPaymentTransactionEntry.ManualPaymentType;
                new_Record.ManualPaymentType = model.ManualPaymentTransactionEntry.ManualPaymentType;
                new_Record.TransactionDate = model.ManualPaymentTransactionEntry.TransactionDate;
                new_Record.EffectiveDate = model.ManualPaymentTransactionEntry.EffectiveDate;
                new_Record.CurrentYear = model.ManualPaymentTransactionEntry.CurrentYear;
                new_Record.PreviousYear = model.ManualPaymentTransactionEntry.PreviousYear;
                new_Record.FromDate = model.ManualPaymentTransactionEntry.FromDate;
                new_Record.ToDate = model.ManualPaymentTransactionEntry.ToDate;

                new_Record.TransactionNumber = model.ManualPaymentTransactionEntry.TransactionNumber;
                new_Record.PaidDate = model.ManualPaymentTransactionEntry.PaidDate;
                new_Record.PaidReferenceNumber = model.ManualPaymentTransactionEntry.PaidReferenceNumber;
                new_Record.ReferenceNumber = model.ManualPaymentTransactionEntry.ReferenceNumber;
                new_Record.ReferenceDescription = model.ManualPaymentTransactionEntry.ReferenceDescription;
                new_Record.ReferenceNote = model.ManualPaymentTransactionEntry.ReferenceNote;
                new_Record.TransactionNote = model.ManualPaymentTransactionEntry.TransactionNote;
                new_Record.TransactionStatus = check_status.Created.GetHashCode();
                new_Record.DocumentType = model.Payment_Type_Source_Document.GetHashCode();

                DateTime statis2 = Convert.ToDateTime("1/1/1900");
                new_Record.CreatedDate = DateTime.Now.Date;
                new_Record.CreatedBy = User.Identity.Name;
                new_Record.ReportAsReadyDate = statis2;
                new_Record.ApprovedDate = statis2;
                new_Record.RejectedDate = statis2;
                new_Record.CanceledDate = statis2;
                new_Record.CompletedDate = statis2;

                new_Record.Created_By = User.Identity.Name;
                new_Record.Created_Date = DateTime.Now.Date;

                new_Record.check_status = HR.Models.Infra.check_status.created;
                new_Record.name_state = nameof(check_status.Created);
                var username = User.Identity.GetUserName();
                var Date = Convert.ToDateTime("1/1/1900");
                var s = new status { statu = HR.Models.Infra.check_status.created, created_by = username, Type = Models.Infra.Type.ManualPaymentTransactionEntry, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                var st = dbcontext.status.Add(s);
                dbcontext.SaveChanges();
                new_Record.statID = s.ID;

                var Header = dbcontext.ManualPaymentTransactionEntry.Add(new_Record);
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
                        var new_details = new ManualPaymentTransactionEntry_ExtendedFieldsDetail { TransactionNumber = Header.ID.ToString(), Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Detail_Code = codeid[i], Detail_Desc = SalaryDes[i], ValueType = valuetype, Value = value };
                        dbcontext.ManualPaymentTransactionEntry_ExtendedFieldsDetail.Add(new_details);
                        dbcontext.SaveChanges();
                    }
                }
                if (Command == "Submit")
                {
                    return RedirectToAction("Details", "ManualPaymentTransactionEntry", new { id = Header.ID, Trans = Header.TransactionNumber, Emp = Header.Employee_Code, Man = Header.ManualPaymentType, Source = Header.DocumentType });//int.Parse(record.Employee_ProfileId)
                }
                ////////////////
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Admin,payroll,payrollTransaction,Payment Settlement Transaction Entry")]
        public ActionResult edit(int id)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList().Select(m => new { Code = m.PaymentTypeCode + "-[" + m.PaymentTypeDesc + ']', ID = m.ID });
                ViewBag.id = id;
                var old_model = dbcontext.ManualPaymentTransactionEntry.FirstOrDefault(m => m.ID == id);
                var header = new HeaderManual { ManualPaymentTransactionEntry = old_model, check_status = (check_status)old_model.TransactionStatus, Payment_Type_Source_Document = (Payment_Type_Source_Document)old_model.DocumentType };
                var old_details = dbcontext.ManualPaymentTransactionEntry_ExtendedFieldsDetail.Where(m => m.TransactionNumber == old_model.ID.ToString()).ToList();
                var new_model = new VMs { ManualPaymentTransactionEntry_ExtendedFieldsDetail = old_details, Header = header };
                return View(new_model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(VMs model, FormCollection form, string Command)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList().Select(m => new { Code = m.PaymentTypeCode + "-[" + m.PaymentTypeDesc + ']', ID = m.ID });
                var H_ = dbcontext.ManualPaymentTransactionEntry.FirstOrDefault(m => m.ID == model.Header.ManualPaymentTransactionEntry.ID);
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == H_.statID);
                if (sta.statu == Models.Infra.check_status.Approved || sta.statu == Models.Infra.check_status.Rejected || sta.statu == Models.Infra.check_status.Closed || sta.statu == Models.Infra.check_status.Recervied || sta.statu == Models.Infra.check_status.Canceled)
                {
                    TempData["message"] = HR.Resource.training.status_message;
                    return RedirectToAction("index");
                }
                ///update////
                var updated_model = dbcontext.ManualPaymentTransactionEntry.FirstOrDefault(m => m.ID == model.Header.ManualPaymentTransactionEntry.ID);

                updated_model.ExtendedFields_Code = model.Header.ManualPaymentTransactionEntry.ExtendedFields_Code;
                updated_model.TransactionNumber = model.Header.ManualPaymentTransactionEntry.TransactionNumber;
                updated_model.Employee_Code = model.Header.ManualPaymentTransactionEntry.Employee_Code;
                updated_model.ManualPaymentType = model.Header.ManualPaymentTransactionEntry.ManualPaymentType;
                updated_model.TransactionDate = model.Header.ManualPaymentTransactionEntry.TransactionDate;
                updated_model.EffectiveDate = model.Header.ManualPaymentTransactionEntry.EffectiveDate;
                updated_model.CurrentYear = model.Header.ManualPaymentTransactionEntry.CurrentYear;
                updated_model.PreviousYear = model.Header.ManualPaymentTransactionEntry.PreviousYear;
                updated_model.FromDate = model.Header.ManualPaymentTransactionEntry.FromDate;
                updated_model.ToDate = model.Header.ManualPaymentTransactionEntry.ToDate;
                updated_model.TransactionNumber = model.Header.ManualPaymentTransactionEntry.TransactionNumber;
                updated_model.PaidDate = model.Header.ManualPaymentTransactionEntry.PaidDate;
                updated_model.PaidReferenceNumber = model.Header.ManualPaymentTransactionEntry.PaidReferenceNumber;
                updated_model.ReferenceNumber = model.Header.ManualPaymentTransactionEntry.ReferenceNumber;
                updated_model.ReferenceDescription = model.Header.ManualPaymentTransactionEntry.ReferenceDescription;
                updated_model.ReferenceNote = model.Header.ManualPaymentTransactionEntry.ReferenceNote;
                updated_model.TransactionNote = model.Header.ManualPaymentTransactionEntry.TransactionNote;
                updated_model.TransactionStatus = model.Header.check_status.GetHashCode();
                updated_model.DocumentType = model.Header.Payment_Type_Source_Document.GetHashCode();
                dbcontext.SaveChanges();
                ///////////delete//////////
                var update_details = dbcontext.ManualPaymentTransactionEntry_ExtendedFieldsDetail.Where(m => m.TransactionNumber == updated_model.ID.ToString()).ToList();
                dbcontext.ManualPaymentTransactionEntry_ExtendedFieldsDetail.RemoveRange(update_details);
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
                        var new_details = new ManualPaymentTransactionEntry_ExtendedFieldsDetail { TransactionNumber = updated_model.ID.ToString(), Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Detail_Code = codeid[i], Detail_Desc = SalaryDes[i], ValueType = valuetype, Value = value };
                        dbcontext.ManualPaymentTransactionEntry_ExtendedFieldsDetail.Add(new_details);
                        dbcontext.SaveChanges();
                    }
                }
                if (Command == "Submit")
                {
                    return RedirectToAction("Details", "ManualPaymentTransactionEntry", new { id = updated_model.ID, Trans = updated_model.TransactionNumber, Emp = updated_model.Employee_Code, Man = updated_model.ManualPaymentType, Source = updated_model.DocumentType });//int.Parse(record.Employee_ProfileId)
                }
                ////////////////
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Admin,payroll,payrollTransaction,Payment Settlement Transaction Entry")]
        public ActionResult delete(int id)
        {
            try
            {
                var model = dbcontext.ManualPaymentTransactionEntry.FirstOrDefault(m => m.ID == id);
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
            //var model = dbcontext.ManualPaymentTransactionEntry.FirstOrDefault(m => m.ID == id);
            //var status = model.status;

            //try
            //{
            //    dbcontext.status.Remove(status);
            //    dbcontext.SaveChanges();
            //    var details = dbcontext.ManualPaymentTransactionEntry_ExtendedFieldsDetail.Where(m => m.TransactionNumber == model.ID.ToString());
            //    dbcontext.ManualPaymentTransactionEntry_ExtendedFieldsDetail.RemoveRange(details);
            //    dbcontext.SaveChanges();

            //    dbcontext.ManualPaymentTransactionEntry.Remove(model);
            //    dbcontext.SaveChanges();
            //    return RedirectToAction("index");
            //}
             var header = dbcontext.ManualPaymentTransactionEntry.FirstOrDefault(m => m.ID == id);
            var H_ = dbcontext.ManualPaymentTransactionEntry.FirstOrDefault(m => m.ID == header.ID);
            var sta = dbcontext.status.FirstOrDefault(m => m.ID == H_.statID);
            if (sta.statu == Models.Infra.check_status.Approved || sta.statu == Models.Infra.check_status.Rejected || sta.statu == Models.Infra.check_status.Closed || sta.statu == Models.Infra.check_status.Recervied || sta.statu == Models.Infra.check_status.Canceled)
            {
                TempData["message"] = HR.Resource.training.status_message;
                return RedirectToAction("index");
            }
            var details = dbcontext.ManualPaymentTransactionEntry_ExtendedFieldsDetail.Where(m => m.TransactionNumber == header.ID.ToString()).ToList();
            var status = dbcontext.status.FirstOrDefault(m => m.ID == header.statID);

            try
            {
                dbcontext.ManualPaymentTransactionEntry.Remove(header);
                if (details.Count > 0)
                    dbcontext.ManualPaymentTransactionEntry_ExtendedFieldsDetail.RemoveRange(details);
               
                dbcontext.status.Remove(status);

                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(header);
            }
        }
        [Authorize(Roles = "Admin,payroll,payrollTransaction,Payment Settlement Transaction Entry")]
        public ActionResult Details(int id, string Trans, string Emp, string Man, string Source)
        {
            try
            {
                var Emps = int.Parse(Emp);
                ViewBag.Trans = Trans;
                ViewBag.Emp = dbcontext.Employee_Profile.Where(a => a.ID == Emps).FirstOrDefault().Full_Name;
                ViewBag.Empcode = dbcontext.Employee_Profile.Where(a => a.ID == Emps).FirstOrDefault().Code;
                var Mans = int.Parse(Man);
                ViewBag.Man = dbcontext.ManualPaymentTypes_Header.Where(a => a.ID == Mans).FirstOrDefault().PaymentTypeDesc;
                ViewBag.Mans = Man;
                ViewBag.Source = Source;
                ViewBag.Mans = Man;
                ViewBag.IDD = id;
                var old_model = dbcontext.ManualPaymentTransactionEntry.FirstOrDefault(m => m.ID == id);
                var header = new HeaderManual { ManualPaymentTransactionEntry = old_model, check_status = (check_status)old_model.TransactionStatus, Payment_Type_Source_Document = (Payment_Type_Source_Document)old_model.DocumentType };

                return View(header);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Details(ManualPaymentTransactionEntry_Detail model, FormCollection form, string did, string id)
        {
            try
            {
                ViewBag.Trans = did;
                ViewBag.IDD = id;
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList().Select(m => new { Code = m.PaymentTypeCode + "-[" + m.PaymentTypeDesc + ']', ID = m.ID });
                ///////////delete//////////
                var update_details = dbcontext.ManualPaymentTransactionEntry_Detail.Where(m => m.TransactionNumber == id).ToList();
                dbcontext.ManualPaymentTransactionEntry_Detail.RemoveRange(update_details);
                dbcontext.SaveChanges();
                ///////////////////add///////
                var codeid = form["codeid"].Split(',');
                var SalaryDes = form["SalaryDes"].Split(',');
                var Type = form["Type"].Split(',');
                var ValueType = form["ValueType"].Split(',');
                var DefaultValue = form["DefaultValue"].Split(',');

                for (var i = 0; i < codeid.Length; i++)
                {
                    if (codeid[i] != "")
                    {
                        var value = double.Parse(DefaultValue[i]);
                        var new_details = new ManualPaymentTransactionEntry_Detail { TransactionNumber = ViewBag.IDD, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, SalaryCodeID = SalaryDes[i], Value = value };
                        dbcontext.ManualPaymentTransactionEntry_Detail.Add(new_details);
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
        [Authorize(Roles = "Admin,payroll,payrollTransaction,Payment Settlement Mass Transaction")]
        public ActionResult PaymentSettlementMassTransaction()
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
                ViewBag.ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList().Select(m => new { Code = m.PaymentTypeCode + "-[" + m.PaymentTypeDesc + ']', ID = m.ID });
                var Statis = DateTime.Now;
                var new_record = new ManualPaymentTransactionEntry { EffectiveDate = Statis, TransactionDate = Statis, FromDate = Statis, ToDate = Statis, PaidDate = Statis };
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                var model_ = dbcontext.ManualPaymentTransactionEntry.ToList();
                if (model_.Count() == 0)
                {
                    new_record.TransactionNumber = stru + "1";
                }
                else
                {
                    new_record.TransactionNumber = stru + (model_.LastOrDefault().ID + 1).ToString();
                }

                var model = new HeaderManual { ManualPaymentTransactionEntry = new_record, Payment_Type_Source_Document = new Payment_Type_Source_Document(), check_status = check_status.Created };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult PaymentSettlementMassTransaction(HeaderManual model, FormCollection form, string Command)
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
                ViewBag.ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList().Select(m => new { Code = m.PaymentTypeCode + "-[" + m.PaymentTypeDesc + ']', ID = m.ID });

                var list_emp = new List<Employee_Profile>();
                var ID_emp = form["ID_emp"].Split(',');
                foreach (var item in ID_emp)
                {
                    if (item != "")
                    {
                        var ID = int.Parse(item);
                        var emp = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == ID);
                        list_emp.Add(emp);
                    }
                }
                foreach (var item2 in list_emp)
                {
                    ManualPaymentTransactionEntry new_Record = new ManualPaymentTransactionEntry();

                    var model_ = dbcontext.ManualPaymentTransactionEntry.ToList();
                    var stru = dbcontext.StructureModels.FirstOrDefault(a => a.All_Models == ChModels.Payroll).Structure_Code;
                    if (model_.Count() == 0)
                    {
                        new_Record.TransactionNumber = stru + "1";
                    }
                    else
                    {
                        new_Record.TransactionNumber = stru + (model_.LastOrDefault().ID + 1).ToString();
                    }

                    new_Record.Employee_Code = item2.ID.ToString();
                    new_Record.ManualPaymentType = model.ManualPaymentTransactionEntry.ManualPaymentType;
                    new_Record.TransactionDate = model.ManualPaymentTransactionEntry.TransactionDate;
                    new_Record.EffectiveDate = model.ManualPaymentTransactionEntry.EffectiveDate;
                    new_Record.CurrentYear = model.ManualPaymentTransactionEntry.CurrentYear;
                    new_Record.PreviousYear = model.ManualPaymentTransactionEntry.PreviousYear;
                    new_Record.FromDate = model.ManualPaymentTransactionEntry.FromDate;
                    new_Record.ToDate = model.ManualPaymentTransactionEntry.ToDate;
                    new_Record.TransactionNote = model.ManualPaymentTransactionEntry.TransactionNote;
                    new_Record.TransactionStatus = check_status.Created.GetHashCode();
                    new_Record.DocumentType = model.Payment_Type_Source_Document.GetHashCode();
                    new_Record.Created_By = User.Identity.Name;
                    new_Record.Created_Date = DateTime.Now.Date;

                    new_Record.check_status = HR.Models.Infra.check_status.created;
                    new_Record.name_state = nameof(check_status.Created);
                    var username = User.Identity.GetUserName();
                    var Date = Convert.ToDateTime("1/1/1900");
                    var s = new status { statu = HR.Models.Infra.check_status.created, created_by = username, Type = Models.Infra.Type.ManualPaymentTransactionEntry, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                    var st = dbcontext.status.Add(s);
                    dbcontext.SaveChanges();
                    new_Record.statID = s.ID;

                    var Header = dbcontext.ManualPaymentTransactionEntry.Add(new_Record);
                    dbcontext.SaveChanges();
                }




                ////////////////
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Admin,payroll,payrollProcess,Manual Payment Settlement Action Process")]
        public ActionResult status(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.ManualPaymentTransactionEntry.FirstOrDefault(m => m.ID == ID);
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
            var record = dbcontext.ManualPaymentTransactionEntry.FirstOrDefault(m => m.ID == model.empid);
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
                var Employee_Profile = dbcontext.Employee_Profile.ToList();
                var ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList();
                var ManualPaymentTransactionEntry = dbcontext.ManualPaymentTransactionEntry.ToList();
                var model = from a in Employee_Profile
                            join b in ManualPaymentTransactionEntry on a.ID equals int.Parse(b.Employee_Code)
                            join s in ManualPaymentTypes_Header on b.ManualPaymentType equals s.ID.ToString()
                            orderby b.ID
                            select new ManualPaymentTransactionEntryEmployee_VM
                            {
                                fullname = a.Full_Name,
                                Paymenttypedescription = s.PaymentTypeDesc,
                                EmployeeId = a.ID,
                                ManualPaymentTransactionEntry = b
                            };
                return Json(model);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult Getone(DateTime from, DateTime to, List<string> status, string types)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var nn = new List<HR.Models.Infra.check_status>();
                List<ManualPaymentTransactionEntry> re1 = new List<ManualPaymentTransactionEntry>();
                foreach (var item in status)
                {
                    if (item != "all")
                    {
                        nn.Add((HR.Models.Infra.check_status)Enum.Parse(typeof(HR.Models.Infra.check_status), item));
                    }
                }
                if (status[0] == "all" && types == "1")
                {
                    var Employee_Profile = dbcontext.Employee_Profile.ToList();
                    var ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList();
                    var ManualPaymentTransactionEntry = dbcontext.ManualPaymentTransactionEntry.Where(m => DateTime.Compare(m.EffectiveDate, from) >= 0 && DateTime.Compare(m.EffectiveDate, to) <= 0).ToList();
                    var model = from a in Employee_Profile
                                join b in ManualPaymentTransactionEntry on a.ID equals int.Parse(b.Employee_Code)
                                join s in ManualPaymentTypes_Header on b.ManualPaymentType equals s.ID.ToString()
                                orderby b.ID
                                select new ManualPaymentTransactionEntryEmployee_VM
                                {
                                    fullname = a.Full_Name,
                                    Paymenttypedescription = s.PaymentTypeDesc,
                                    EmployeeId = a.ID,
                                    ManualPaymentTransactionEntry = b
                                };
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else if (status[0] == "all" && types == "2")
                {
                    var Employee_Profile = dbcontext.Employee_Profile.ToList();
                    var ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList();
                    var ManualPaymentTransactionEntry = dbcontext.ManualPaymentTransactionEntry.Where(m => DateTime.Compare(m.TransactionDate, from) >= 0 && DateTime.Compare(m.TransactionDate, to) <= 0).ToList();
                    var model = from a in Employee_Profile
                                join b in ManualPaymentTransactionEntry on a.ID equals int.Parse(b.Employee_Code)
                                join s in ManualPaymentTypes_Header on b.ManualPaymentType equals s.ID.ToString()
                                orderby b.ID
                                select new ManualPaymentTransactionEntryEmployee_VM
                                {
                                    fullname = a.Full_Name,
                                    Paymenttypedescription = s.PaymentTypeDesc,
                                    EmployeeId = a.ID,
                                    ManualPaymentTransactionEntry = b
                                };
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else if (status[0] != "all" && types == "1")
                {
                    var req = dbcontext.ManualPaymentTransactionEntry.Where(m => DateTime.Compare(m.EffectiveDate, from) >= 0 && DateTime.Compare(m.EffectiveDate, to) <= 0).ToList();
                    var Employee_Profile = dbcontext.Employee_Profile.ToList();
                    var ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList();
                    foreach (var item in nn)
                    {
                        re1.AddRange(req.Where(m => m.check_status == item).ToList());
                    }
                    var model = from a in Employee_Profile
                                join b in re1 on a.ID equals int.Parse(b.Employee_Code)
                                join s in ManualPaymentTypes_Header on b.ManualPaymentType equals s.ID.ToString()
                                orderby b.ID
                                select new ManualPaymentTransactionEntryEmployee_VM
                                {
                                    fullname = a.Full_Name,
                                    Paymenttypedescription = s.PaymentTypeDesc,
                                    EmployeeId = a.ID,
                                    ManualPaymentTransactionEntry = b
                                };
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else if (status[0] != "all" && types == "2")
                {
                    var req = dbcontext.ManualPaymentTransactionEntry.Where(m => DateTime.Compare(m.TransactionDate, from) >= 0 && DateTime.Compare(m.TransactionDate, to) <= 0).ToList();
                    var Employee_Profile = dbcontext.Employee_Profile.ToList();
                    var ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList();
                    foreach (var item in nn)
                    {
                        re1.AddRange(req.Where(m => m.check_status == item).ToList());
                    }

                    var model = from a in Employee_Profile
                                join b in re1 on a.ID equals int.Parse(b.Employee_Code)
                                join s in ManualPaymentTypes_Header on b.ManualPaymentType equals s.ID.ToString()
                                orderby b.ID
                                select new ManualPaymentTransactionEntryEmployee_VM
                                {
                                    fullname = a.Full_Name,
                                    Paymenttypedescription = s.PaymentTypeDesc,
                                    EmployeeId = a.ID,
                                    ManualPaymentTransactionEntry = b
                                };
                    return Json(model, JsonRequestBehavior.AllowGet);
                }

                return Json(re1);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
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

    }
    public class VMs
    {
        public HeaderManual Header { get; set; }
        public List<ManualPaymentTransactionEntry_ExtendedFieldsDetail> ManualPaymentTransactionEntry_ExtendedFieldsDetail { get; set; }
    }
    public class HeaderManual
    {
        public ManualPaymentTransactionEntry ManualPaymentTransactionEntry { get; set; }
        public check_status check_status { get; set; }
        public Payment_Type_Source_Document Payment_Type_Source_Document { get; set; }
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
    public class ManualPaymentTransactionEntryEmployee_VM
    {
        public string fullname { get; set; }
        public string Paymenttypedescription { get; set; }
        public int EmployeeId { get; set; }
        public ManualPaymentTransactionEntry ManualPaymentTransactionEntry { get; set; }

    }
}