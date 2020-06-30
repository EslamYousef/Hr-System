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
    [Authorize]
    public class LedgerTransactionController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: LedgerTransaction
        public ActionResult Index()
        {
            var records = dbcontext.LedgerTransaction.ToList();
            return View(records);
        }
        public ActionResult create()
        {
            try
            {
                ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });
                ViewBag.Currency = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                var Statis = DateTime.Now;
                var new_record = new LedgerTransaction {PostedDate = Statis, TransactionDate = Statis };
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                var model_ = dbcontext.LedgerTransaction.ToList();
                if (model_.Count() == 0)
                {
                    new_record.JournalNumber = stru + "1";
                }
                else
                {
                    new_record.JournalNumber = stru + (model_.LastOrDefault().ID + 1).ToString();
                }

                var model = new HeaderLedger { LedgerTransaction = new_record, JournalType = JournalType.AccruedSalaries };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(HeaderLedger model)
        {
            try
            {
                ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });
                ViewBag.Currency = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });

                var new_Record = model.LedgerTransaction;
                new_Record.JournalNumber = model.LedgerTransaction.JournalNumber;
                new_Record.JournalDescription = model.LedgerTransaction.JournalDescription;
                new_Record.JournalType = model.JournalType.GetHashCode();
                new_Record.JournalName = model.LedgerTransaction.JournalName;
                new_Record.Currency_Code = model.LedgerTransaction.Currency_Code;
                new_Record.TransactionDate = model.LedgerTransaction.TransactionDate;
                new_Record.PostedDate = model.LedgerTransaction.PostedDate;
                new_Record.Posted = model.LedgerTransaction.Posted;
                new_Record.ERP_JournalNumber = model.LedgerTransaction.ERP_JournalNumber;

                new_Record.TotalAmountDebit = model.LedgerTransaction.TotalAmountDebit;
                new_Record.TotalAmountCredit = model.LedgerTransaction.TotalAmountCredit;
                new_Record.PaymentJournalNumber = model.LedgerTransaction.PaymentJournalNumber;
                new_Record.ERP_PaymentJournalNumber = model.LedgerTransaction.ERP_PaymentJournalNumber;

                new_Record.Created_By = User.Identity.Name;
                new_Record.Created_Date = DateTime.Now.Date;

               var Header = dbcontext.LedgerTransaction.Add(new_Record);
                dbcontext.SaveChanges();

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
                ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });
                ViewBag.Currency = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.id = id;
                var old_model = dbcontext.LedgerTransaction.FirstOrDefault(m => m.ID == id);
                var new_model = new HeaderLedger { LedgerTransaction = old_model, JournalType = (JournalType)old_model.JournalType};
                return View(new_model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(HeaderLedger model)
        {
            try
            {
                ViewBag.PayrollTransactionJournalSetup = dbcontext.PayrollTransactionJournalSetup.ToList().Select(m => new { Code = m.JournalName_BatchCode + "-[" + m.JournalDesc + ']', ID = m.ID });
                ViewBag.Currency = dbcontext.Currency.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });

                var updated_model = dbcontext.LedgerTransaction.FirstOrDefault(m => m.ID == model.LedgerTransaction.ID);

                updated_model.JournalNumber = model.LedgerTransaction.JournalNumber;
                updated_model.JournalDescription = model.LedgerTransaction.JournalDescription;
                updated_model.JournalType = model.JournalType.GetHashCode();
                updated_model.JournalName = model.LedgerTransaction.JournalName;
                updated_model.Currency_Code = model.LedgerTransaction.Currency_Code;
                updated_model.TransactionDate = model.LedgerTransaction.TransactionDate;
                updated_model.PostedDate = model.LedgerTransaction.PostedDate;
                updated_model.Posted = model.LedgerTransaction.Posted;
                updated_model.ERP_JournalNumber = model.LedgerTransaction.ERP_JournalNumber;

                updated_model.TotalAmountDebit = model.LedgerTransaction.TotalAmountDebit;
                updated_model.TotalAmountCredit = model.LedgerTransaction.TotalAmountCredit;
                updated_model.PaymentJournalNumber = model.LedgerTransaction.PaymentJournalNumber;
                updated_model.ERP_PaymentJournalNumber = model.LedgerTransaction.ERP_PaymentJournalNumber;

                updated_model.Modified_By = User.Identity.Name;
                updated_model.Modified_Date = DateTime.Now.Date;
                dbcontext.SaveChanges();
              
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
                var model = dbcontext.LedgerTransaction.FirstOrDefault(m => m.ID == id);
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
            var model = dbcontext.LedgerTransaction.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.LedgerTransaction.Remove(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public class HeaderLedger
        {
            public LedgerTransaction LedgerTransaction { get; set; }
            public JournalType JournalType { get; set; } = JournalType.AccruedSalaries;
        }
        public enum JournalType
        {
            AccruedSalaries = 1,
            PaymentSalaries = 2,
           }
    }
}