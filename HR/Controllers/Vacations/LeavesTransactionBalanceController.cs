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
using Microsoft.Ajax.Utilities;

namespace HR.Controllers.Vacations
{
    [Authorize]
    public class LeavesTransactionBalanceController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: LeavesTransactionBalance
        public ActionResult Index()
        {
            var LeavesTransactionBalance = dbcontext.LeavesTransactionBalance.ToList();
            var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();
            var Vacations_Setup = dbcontext.Vacations_Setup.ToList();
            var model = from a in LeavesTransactionBalance
                        join b in Employee_Profile on a.EmployeeID equals b.ID
                        join c in Vacations_Setup on a.VacCode equals c.ID
                        select new VM_LeavesTransactionBalance
                        { EmployeeName = b.Full_Name, Vacations_Setup = c.LeaveTypeNameEnglish, LeavesTransactionBalance = a };
            return View(model);
        }
        public ActionResult create()
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
                ViewBag.Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();
                //DateTime Statis = DateTime.Now;
                var new_record = new LeavesTransactionBalance { TransactionDate = DateTime.Now , Created_Date = DateTime.Now, Created_By = User.Identity.Name, Remain = 0, Balance = 0  ,Value = 0};
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                var model_ = dbcontext.LeavesTransactionBalance.ToList();
                if (model_.Count() == 0)
                {
                    new_record.Serial_LB = stru + "1";
                }
                else
                {
                    new_record.Serial_LB = stru + (model_.LastOrDefault().ID + 1).ToString();
                }

                var model = new Headers { LeavesTransactionBalance = new_record, TransactionType = TransactionType.Increasing };

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
                ViewBag.Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();

                LeavesTransactionBalance new_Record = new LeavesTransactionBalance();

                new_Record.SerialNo = model.LeavesTransactionBalance.SerialNo;
                new_Record.EmployeeID = model.LeavesTransactionBalance.EmployeeID;
                new_Record.VacCode = model.LeavesTransactionBalance.VacCode;
                new_Record.SerialLTB = model.LeavesTransactionBalance.SerialLTB;
                new_Record.Serial_LB = model.LeavesTransactionBalance.Serial_LB;
                var FromBalance = form["FromBalance"].Split(',');
                var CurrentBalance = form["CurrentBalance"].Split(',');
                
                DateTime? Balance = DateTime.Parse(FromBalance[0]);
                new_Record.Year = Balance.Value.Year;
              
                new_Record.TransactionDate = model.LeavesTransactionBalance.TransactionDate;
                if ( model.LeavesTransactionBalance.Balance == 0 || model.LeavesTransactionBalance.Remain == 0)
                {
                    TempData["Message"] = "There should be value in Balance and Remain";
                    return RedirectToAction("Create");
                }
                new_Record.Value = model.LeavesTransactionBalance.Value;
                new_Record.Notes = model.LeavesTransactionBalance.Notes;
                var Current = int.Parse(CurrentBalance[0]);
                new_Record.Balance = Current;
                new_Record.Remain = model.LeavesTransactionBalance.Remain;
                new_Record.Company_ID = model.LeavesTransactionBalance.Company_ID;
                new_Record.RowIndx = model.LeavesTransactionBalance.RowIndx;
                new_Record.Created_By = User.Identity.Name;
                new_Record.Created_Date = DateTime.Now.Date;
                new_Record.Modified_By = model.LeavesTransactionBalance.Modified_By;
                new_Record.Modified_Date = model.LeavesTransactionBalance.Modified_Date;
                var list = dbcontext.LeavesTransactionBalance.Where(a => a.VacCode == model.LeavesTransactionBalance.VacCode && a.EmployeeID == model.LeavesTransactionBalance.EmployeeID && a.Year == Balance.Value.Year).ToList();
                if (list.Count() == 0)
                {
                    new_Record.Check = true;
                }
                else
                {
                    var te = list.Where(m => m.Check == true);
                    foreach (var item in te)
                    {
                        item.Check = false;
                        dbcontext.SaveChanges();
                    }
                    new_Record.Check = true;
                }
                var Header = dbcontext.LeavesTransactionBalance.Add(new_Record);
                dbcontext.SaveChanges();

                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.LeavesTransactionBalance.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no country";
                    return View();
                }

            }
            catch (Exception e)
            {
                return View();
            }

        }
        [ActionName("Delete")]
        [HttpPost]
        public ActionResult Deletemethod(int id)
        {
            var record = dbcontext.LeavesTransactionBalance.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.LeavesTransactionBalance.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "this code Is already exists";
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        public class Headers
        {
            public LeavesTransactionBalance LeavesTransactionBalance { get; set; }
            public TransactionType TransactionType { get; set; }
        }
        public enum TransactionType
        {
            Increasing = 1,
            Decreasing = 2
        }
        public class VM_LeavesTransactionBalance
        {
            public LeavesTransactionBalance LeavesTransactionBalance { get; set; }
            public string EmployeeName { get; set; }
            public string Vacations_Setup { get; set; }
        }
    }
}