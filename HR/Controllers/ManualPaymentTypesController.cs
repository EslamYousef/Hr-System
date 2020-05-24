using Antlr.Runtime;
using HR.Models;
using HR.Models.Infra;
using HR.Models.SetupPayroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize]
    public class ManualPaymentTypesController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: ManualPaymentTypes
        public ActionResult Index()
        {
            var model = dbcontext.ManualPaymentTypes_Header.ToList();
            return View(model);
        }
        public ActionResult create()
        {
            try
            {
                ViewBag.Debit = dbcontext.GL_AccountSetup.Where(a=>a.AccountType ==1 || a.AccountType == 3).ToList().Select(m => new { Code = m.AccountNumber + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.Credites = dbcontext.GL_AccountSetup.Where(a => a.AccountType == 2 || a.AccountType == 3).ToList().Select(m => new { Code = m.AccountNumber + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });
                ViewBag.Checktype = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.ExtendedFields_Header = dbcontext.ExtendedFields_Header.Where(a => a.Purpose == 3).ToList().Select(m => new { Code = m.ExtendedFields_Code + "-[" + m.ExtendedFields_Desc + ']', ID = m.ID });
                ViewBag.salaryitem = dbcontext.salary_code.Where(a=>a.SourceEntry == 3).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });

                var new_record = new ManualPaymentTypes_Header();
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                var model_ = dbcontext.ManualPaymentTypes_Header.ToList();
                if (model_.Count() == 0)
                {
                    new_record.PaymentTypeCode = stru + "1";
                }
                else
                {
                    new_record.PaymentTypeCode = stru + (model_.LastOrDefault().ID + 1).ToString();
                }
                var model = new Headers { ManualPaymentTypes_Header = new_record, Transaction_Type = new Transaction_Type(), Payment_Type_Source_Document = new Payment_Type_Source_Document(), Frequency_Periodic_type = new Frequency_Periodic_type() };
              
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
                ViewBag.Debit = dbcontext.GL_AccountSetup.Where(a => a.AccountType == 1 || a.AccountType == 3).ToList().Select(m => new { Code = m.AccountNumber + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.Credites = dbcontext.GL_AccountSetup.Where(a => a.AccountType == 2 || a.AccountType == 3).ToList().Select(m => new { Code = m.AccountNumber + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });
                ViewBag.Checktype = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.ExtendedFields_Header = dbcontext.ExtendedFields_Header.Where(a => a.Purpose == 3).ToList().Select(m => new { Code = m.ExtendedFields_Code + "-[" + m.ExtendedFields_Desc + ']', ID = m.ID });
                ViewBag.salaryitem = dbcontext.salary_code.Where(a => a.SourceEntry == 3).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });

                var new_Record = model.ManualPaymentTypes_Header;
                new_Record.ExtendedFields_Code = model.ManualPaymentTypes_Header.ExtendedFields_Code;
                new_Record.Type_Code = model.ManualPaymentTypes_Header.Type_Code;
                new_Record.Transaction_Type = model.Transaction_Type.GetHashCode();
                new_Record.PaymentTypeSourceDocument = model.Payment_Type_Source_Document.GetHashCode();
                new_Record.FrequencyPeriodType = model.Frequency_Periodic_type.GetHashCode();
                new_Record.Created_By = User.Identity.Name;
                new_Record.Created_Date = DateTime.Now.Date;
                var Header = dbcontext.ManualPaymentTypes_Header.Add(new_Record);
                dbcontext.SaveChanges();

                ///////////////////
                var codeid = form["codeid"].Split(',');
                var SalaryDes = form["SalaryDes"].Split(',');
                var TypeE = form["TypeE"].Split(',');
                var ValueType = form["ValueType"].Split(',');
                var DefaultValue = form["DefaultValue"].Split(',');

                for (var i = 0; i < codeid.Length; i++)
                {
                    if (codeid[i] != "")
                    {
                        var new_details = new ManualPaymentTypes_Detail { PaymentTypeCode = Header.ID.ToString(), Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, SalaryCodeID = codeid[i], DefaultValue = int.Parse(DefaultValue[i]) , Salarycodedescription= SalaryDes[i], Type = TypeE[i],ValueType = ValueType[i]};
                        dbcontext.ManualPaymentTypes_Detail.Add(new_details);
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
        public ActionResult edit(int id)
        {
            try
            {
                ViewBag.Debit = dbcontext.GL_AccountSetup.Where(a => a.AccountType == 1 || a.AccountType == 3).ToList().Select(m => new { Code = m.AccountNumber + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.Credites = dbcontext.GL_AccountSetup.Where(a => a.AccountType == 2 || a.AccountType == 3).ToList().Select(m => new { Code = m.AccountNumber + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });
                ViewBag.Checktype = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.ExtendedFields_Header = dbcontext.ExtendedFields_Header.Where(a => a.Purpose == 3).ToList().Select(m => new { Code = m.ExtendedFields_Code + "-[" + m.ExtendedFields_Desc + ']', ID = m.ID });
                ViewBag.salaryitem = dbcontext.salary_code.Where(a => a.SourceEntry == 3).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });

                var old_model = dbcontext.ManualPaymentTypes_Header.FirstOrDefault(m => m.ID == id);
                var header = new Headers { ManualPaymentTypes_Header = old_model , Transaction_Type = (Transaction_Type)old_model.Transaction_Type, Payment_Type_Source_Document = (Payment_Type_Source_Document)old_model.PaymentTypeSourceDocument, Frequency_Periodic_type = (Frequency_Periodic_type)old_model.FrequencyPeriodType };
                var old_details = dbcontext.ManualPaymentTypes_Detail.Where(m => m.PaymentTypeCode == old_model.ID.ToString()).ToList();
                var new_model = new VMs { ManualPaymentTypes_Detail = old_details, Header = header };
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
                ViewBag.Debit = dbcontext.GL_AccountSetup.Where(a => a.AccountType == 1 || a.AccountType == 3).ToList().Select(m => new { Code = m.AccountNumber + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.Credites = dbcontext.GL_AccountSetup.Where(a => a.AccountType == 2 || a.AccountType == 3).ToList().Select(m => new { Code = m.AccountNumber + "-[" + m.AccountName + ']', ID = m.ID });
                ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });
                ViewBag.Checktype = dbcontext.Checktype.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.ExtendedFields_Header = dbcontext.ExtendedFields_Header.Where(a => a.Purpose == 3).ToList().Select(m => new { Code = m.ExtendedFields_Code + "-[" + m.ExtendedFields_Desc + ']', ID = m.ID });
                ViewBag.salaryitem = dbcontext.salary_code.Where(a => a.SourceEntry == 3).ToList().Select(m => new { Code = m.SalaryCodeID + "-[" + m.SalaryCodeDesc + ']', ID = m.ID });
                ///update////
                var updated_model = dbcontext.ManualPaymentTypes_Header.FirstOrDefault(m => m.ID == model.Header.ManualPaymentTypes_Header.ID);
                updated_model.Type_Code = model.Header.ManualPaymentTypes_Header.Type_Code;
                updated_model.ExtendedFields_Code = model.Header.ManualPaymentTypes_Header.ExtendedFields_Code;
                updated_model.Modified_By = User.Identity.Name;
                updated_model.Modified_Date = DateTime.Now.Date;
                updated_model.Transaction_Type = model.Header.Transaction_Type.GetHashCode();
                updated_model.PaymentTypeSourceDocument = model.Header.Payment_Type_Source_Document.GetHashCode();
                updated_model.FrequencyPeriodType = model.Header.Frequency_Periodic_type.GetHashCode();
                dbcontext.SaveChanges();
                ///////////delete//////////
                var update_details = dbcontext.ManualPaymentTypes_Detail.Where(m => m.PaymentTypeCode == updated_model.ID.ToString()).ToList();
                dbcontext.ManualPaymentTypes_Detail.RemoveRange(update_details);
                dbcontext.SaveChanges();
                ///////////////////add///////
                var codeid = form["codeid"].Split(',');
                var SalaryDes = form["SalaryDes"].Split(',');
                var TypeE = form["TypeE"].Split(',');
                var ValueType = form["ValueType"].Split(',');
                var DefaultValue = form["DefaultValue"].Split(',');

                for (var i = 0; i < codeid.Length; i++)
                {
                    if (codeid[i] != "")
                    {
                       
                        var nejjw_details = new ManualPaymentTypes_Detail { PaymentTypeCode = updated_model.ID.ToString(), Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, SalaryCodeID = codeid[i], DefaultValue = int.Parse(DefaultValue[i]), Salarycodedescription = SalaryDes[i], Type = TypeE[i], ValueType = ValueType[i] };
                        dbcontext.ManualPaymentTypes_Detail.Add(nejjw_details);
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
                var model = dbcontext.ManualPaymentTypes_Header.FirstOrDefault(m => m.ID == id);
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
            var model = dbcontext.ManualPaymentTypes_Header.FirstOrDefault(m => m.ID == id);
            try
            {
                var details = dbcontext.ManualPaymentTypes_Detail.Where(m => m.PaymentTypeCode == model.ID.ToString());
                dbcontext.ManualPaymentTypes_Detail.RemoveRange(details);
                dbcontext.SaveChanges();


                dbcontext.ManualPaymentTypes_Header.Remove(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public JsonResult salaryitem(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var item = dbcontext.salary_code.FirstOrDefault(m => m.ID == id);
            return Json(item);
        }
    }
    public class VMs
    {
        public Headers Header { get; set; }
        public List<ManualPaymentTypes_Detail> ManualPaymentTypes_Detail { get; set; }
    }
    public class Headers
    {
        public ManualPaymentTypes_Header ManualPaymentTypes_Header { get; set; }
        public Transaction_Type Transaction_Type { get; set; }
        public Payment_Type_Source_Document Payment_Type_Source_Document { get; set; }
        public Frequency_Periodic_type Frequency_Periodic_type { get; set; }
    }
    public enum Transaction_Type
    {
        Payment = 1,
        Settlement = 2,
    }
    public enum Payment_Type_Source_Document
    {            
        Manual = 1,
        Loan = 2,
        Vacation =3,
        EOS = 4 ,
        Variable_compensation = 5 ,
        Fixed_compensation = 6 ,
        Retro_active= 7,
        Business_trip = 8 ,
        Government_expense =9 ,
        Air_Lines_Booking_expense = 10 ,
        HR_records = 11 ,
        Miscellaneous = 12,
    }
    public enum Frequency_Periodic_type
    {    
        Monthly = 1,
        Yearly = 2,
        Per_life_time = 3,
    }
}