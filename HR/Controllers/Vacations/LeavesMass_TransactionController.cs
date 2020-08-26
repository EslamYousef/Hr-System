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
using HR.Migrations;
using Microsoft.Ajax.Utilities;

namespace HR.Controllers.Vacations
{
    [Authorize]
    public class LeavesMass_TransactionController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: LeavesMass_Transaction
       
        public ActionResult Create()
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
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
                ViewBag.Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();
                var Statis = DateTime.Now;
                var new_record = new LeavesMass_Transaction { TransactionDate = Statis , Created_Date = Statis};
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Payroll).Structure_Code;
                var model_ = dbcontext.LeavesMass_Transaction.ToList();
                if (model_.Count() == 0)
                {
                    new_record.SerialNo = stru + "1";
                }
                else
                {
                    new_record.SerialNo = stru + (model_.LastOrDefault().ID + 1).ToString();
                }

                return View(new_record);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(LeavesMass_Transaction model, FormCollection form, string Command)
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
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
                ViewBag.Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();

                var model_ = dbcontext.LeavesMass_Transaction.ToList();
                var TransactionCode = "0";
                if (model_.Count() == 0)
                {
                    TransactionCode = "1";
                }
                else
                {
                    TransactionCode = (model_.LastOrDefault().ID + 1).ToString();
                }

                var ID_emp = form["ID_emp"].Split(',');
                var CodeEmp = form["CodeEmp"].Split(',');
                var NameEmp = form["NameEmp"].Split(',');
                var CodeVac = form["CodeVac"].Split(',');
                var NameVac = form["NameVac"].Split(',');
                var BalDay = form["BalDay"].Split(',');
                var TotalDay = form["TotalDay"].Split(',');
                var Reason = form["Reason"].Split(',');
                var FromBalance = form["FromBalance"].Split(',');

                for (var i = 0; i < CodeEmp.Length; i++)
                {
                    if (CodeEmp[i] != "")
                    {
                        LeavesMass_Transaction new_Record = new LeavesMass_Transaction();

                        var LeavesRequestMaster = dbcontext.LeavesRequestMaster.ToList();
                        var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                        var model2 = dbcontext.LeavesMass_Transaction.ToList();
                        if (model2.Count() == 0)
                        {
                            new_Record.SerialNo = stru + "1";
                        }
                        else
                        {
                            new_Record.SerialNo = stru + (model2.LastOrDefault().ID +1 ).ToString();
                        }
                        new_Record.TransactionCode = TransactionCode;
                        new_Record.TransactionDate = DateTime.Now;
                        new_Record.Created_By = User.Identity.Name;
                        new_Record.Created_Date = DateTime.Now.Date;

                        var new_details = new LeavesRequestMaster();
                        if (LeavesRequestMaster.Count() == 0)
                        {
                            new_details.SerialNo = stru + "1";
                        }
                        else
                        {
                            new_details.SerialNo = stru + (LeavesRequestMaster.LastOrDefault().ID + 1).ToString();
                        }
                        var codeva = CodeVac[i];
                        var codeem = CodeEmp[i];

                        var emp = dbcontext.Employee_Profile.FirstOrDefault(a => a.Code == codeem);
                        var Vacations = dbcontext.Vacations_Setup.FirstOrDefault(a => a.LeaveTypeCode == codeva);
                      
                        var Balance = double.Parse(BalDay[i]);
                        var Total = double.Parse(TotalDay[i]);
                        DateTime? year = DateTime.Parse(FromBalance[1]);
                        int FBalance = year.Value.Year;
                        new_details.year = FBalance;
                        DateTime? dateOrNull = model.TransactionDate;
                        if (dateOrNull != null)
                        {
                            new_details.DateFrom = dateOrNull.Value;
                        }
                        DateTime? dateOrNull2 = model.Created_Date;
                        if (dateOrNull2 != null)
                        {
                            new_details.DateTo = dateOrNull2.Value;
                        }
                        new_details.EmployeeID = emp.ID;
                        new_details.VacCode = Vacations.ID;
                        //new_details.DateFrom = model.TransactionDate;
                        //new_details.DateTo = model.Created_Date;
                        new_details.CurrentDate = DateTime.Now.Date;
                        new_details.ActualToDate = model.Created_Date;
                        new_details.Reason = Reason[i];
                        new_details.Approved = check_status.Approved.GetHashCode();
                        new_details.BalanceDays = Balance;
                        new_details.TotalDays = Total;
                        new_details.RemainDays = Balance - Total;
                        new_details.Created_By = User.Identity.Name;
                        new_details.Created_Date = DateTime.Now.Date;
                        new_details.check_status = HR.Models.Infra.check_status.Approved;
                        new_details.name_state = nameof(check_status.Approved);
                        var username = User.Identity.GetUserName();
                        var Date = Convert.ToDateTime("1/1/1900");
                        var s = new status { statu = HR.Models.Infra.check_status.created, created_by = username, Type = Models.Infra.Type.LeavesRequestMaster, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                        var st = dbcontext.status.Add(s);
                        dbcontext.SaveChanges();
                        new_details.statID = s.ID;

                        var LeavesTransactionBalance = dbcontext.LeavesTransactionBalance.Where(a => a.VacCode == Vacations.ID && a.EmployeeID == emp.ID && a.Year == year.Value.Year && a.Check == true).ToList();
                        var LeavesTransaction = LeavesTransactionBalance.LastOrDefault();

                        dbcontext.LeavesMass_Transaction.Add(new_Record);
                        dbcontext.SaveChanges();
                        dbcontext.LeavesRequestMaster.Add(new_details);
                        dbcontext.SaveChanges();
                        if (LeavesTransaction != null && LeavesTransaction.Check == true)
                        {
                            LeavesTransaction.Check = false;
                            dbcontext.SaveChanges();
                        }
                    }
                }
        
                return RedirectToAction("index", "LeaveRequest");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        public JsonResult GetAllEmployeesWithVications(int Vac, DateTime FromBalance, DateTime Start, DateTime End)
        {
            try
            {

                var record = dbcontext.LeavesBalance.Where(a => a.BalanceStartDate == FromBalance && a.VacCode == Vac).FirstOrDefault();
                var BalanceStart = record.BalanceStartDate;
                int BalanceStartDate = Convert.ToDateTime(BalanceStart).Year;

                var LeavesRequestMaster = dbcontext.LeavesRequestMaster.Where(a => a.VacCode == record.VacCode && a.LastOrder == true).FirstOrDefault();
                //var LeavesRequestMasters = dbcontext.LeavesRequestMaster.Where(a => a.VacCode == record.VacCode && a.Approved == 1 && a.year == BalanceStartDate && a.LastOrder == true).ToList();
                var VacationsSetup = dbcontext.Vacations_Setup.FirstOrDefault(a => a.ID == Vac);
                int year = Convert.ToDateTime(Start).Year;

                var emps = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList();
                var LeavesMass = new List<VM_LeavesMass_Transaction>();

                foreach (var item in emps)
                {
                    var ret = new List<DateTime>();
                    var Remain = new List<DateTime>();

                    for (DateTime date = Start; date <= End; date = date.AddDays(1))
                    {
                        ret.Add(date);
                    }
                    var empID = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == item.ID);
                    var Weekendsetup = dbcontext.Weekend_setup.FirstOrDefault(a => a.ID == empID.Weekendcode);
                    if (VacationsSetup.IncludeWeekEnd == true && Weekendsetup != null)
                    {
                        foreach (DateTime o in ret)
                        {
                            var weeks = o.DayOfWeek.ToString();

                            var Saturday = "Saturday";
                            var Sunday = "Sunday";
                            var Monday = "Monday";
                            var Tuesday = "Tuesday";
                            var Wednesday = "Wednesday";
                            var Thursday = "Thursday";
                            var Friday = "Friday";
                            //int EmployeeSta = 1;
                            DateTime Total = Convert.ToDateTime(o);
                            if (Weekendsetup.Saturday == true && Saturday == weeks)
                            {
                                Remain.Add(Total);
                            }
                            else if (Weekendsetup.Sunday == true && Sunday == weeks)
                            {
                                Remain.Add(Total);
                            }
                            else if (Weekendsetup.Monday == true && Monday == weeks)
                            {
                                Remain.Add(Total);
                            }
                            else if (Weekendsetup.Tuesday == true && Tuesday == weeks)
                            {
                                Remain.Add(Total);
                            }
                            else if (Weekendsetup.Wednesday == true && Wednesday == weeks)
                            {
                                Remain.Add(Total);
                            }
                            else if (Weekendsetup.Thursday == true && Thursday == weeks)
                            {
                                Remain.Add(Total);
                            }
                            else if (Weekendsetup.Friday == true && Friday == weeks)
                            {
                                Remain.Add(Total);
                            }
                        }
                    }

                    foreach (var item2 in Remain)
                    {
                        ret.Remove(item2);
                    }
                    var MyHoliday = new List<DateTime>();

                    if (VacationsSetup.IncludeHoliday == true)
                    {
                        var PublicHolidaysDates = dbcontext.Public_Holidays_Dates.FirstOrDefault(a => a.Holidaysyear == year);
                        if (PublicHolidaysDates != null)
                        {
                            var AppendPublicHolidaysDates = dbcontext.Append_Public_Holidays_Dates.Where(a => a.Public_Holidays_DatesId == PublicHolidaysDates.ID).ToList();
                            if (AppendPublicHolidaysDates != null)
                            {
                                for (int i = 0; i < AppendPublicHolidaysDates.Count; i++)
                                {
                                    for (DateTime date = AppendPublicHolidaysDates[i].Fromdate; date <= AppendPublicHolidaysDates[i].Todate; date = date.AddDays(1))
                                    {
                                        MyHoliday.Add(date);
                                    }
                                }
                            }
                            foreach (var item2 in MyHoliday)
                            {
                                var holiday = ret.Contains(item2);
                                if (holiday != false)
                                {
                                    ret.Remove(item2);
                                }
                            }
                        }
                    }
                    var reted = ret.Count();

                    DateTime Hire = empID.Personnel_Information.Hire_Date;

                    var LeavesRequestMasters = dbcontext.LeavesRequestMaster.Where(a => a.VacCode == record.VacCode && a.EmployeeID == item.ID && a.year == FromBalance.Year).ToList();
                    var LeavesRequest = LeavesRequestMasters.LastOrDefault();

                    var LeavesTransactionBalance = dbcontext.LeavesTransactionBalance.Where(a => a.VacCode == record.VacCode && a.EmployeeID == item.ID && a.Year == FromBalance.Year && a.Check == true).ToList();
                    var LeavesTransaction = LeavesTransactionBalance.LastOrDefault();

                    var LeavesRequestMastersfirst = dbcontext.LeavesRequestMaster.Where(a => a.VacCode == record.VacCode && a.EmployeeID == item.ID && a.year == year).FirstOrDefault();
                    var Leavesfirst = dbcontext.LeavesTransactionBalance.Where(a => a.VacCode == record.VacCode && a.EmployeeID == item.ID && a.Year == FromBalance.Year && a.Check == true).FirstOrDefault();
                    var LeavesBalance = dbcontext.LeavesBalance.Where(a => a.VacCode == record.VacCode && a.EmployeeID == item.ID && a.BalanceStartDate.Value.Year == FromBalance.Year).FirstOrDefault();

                    var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == BalanceStart && a.EmployeeID == item.ID && a.VacCode == record.VacCode && a.Stopped == false);

                    if (LeavesTransaction != null && LeavesTransaction.Check == true)
                    {
                        if (Leaves != null)
                        {
                            //var RemainDays = LeavesTransaction.Remain;
                            LeavesMass.Add(new VM_LeavesMass_Transaction
                            {
                                ID = item.ID,
                                CodeEmp = item.Code,
                                NameEmp = item.Name,
                                CodeLeave = VacationsSetup.LeaveTypeCode,
                                NameLeave = VacationsSetup.LeaveTypeNameEnglish,
                                HiringDate = Hire,
                                YearlyBalance = LeavesBalance == null ? 0 : LeavesBalance.Balance,
                                ActualBalance = Leavesfirst.Remain,
                                TotalDays = reted
                            });
                        }
                    }
                    else if (VacationsSetup.TrackMonthlyAccrualBalance == true)
                    {
                        if (Leaves != null)
                        {
                            var Bal = record.Balance / 12;
                            int Sta = Convert.ToDateTime(Start).Month;
                            //int year = Convert.ToDateTime(Start).Year;

                            var RemainDays = Bal * Sta;
                            var LeavesRequestMas = dbcontext.LeavesRequestMaster.Where(a => a.VacCode == record.VacCode && a.EmployeeID == item.ID && a.year == year).ToList();

                            var AllDays = new List<double?>();
                            if (LeavesRequestMas != null)
                            {
                                for (int i = 0; i < LeavesRequestMas.Count; i++)
                                {
                                    var Rem = LeavesRequestMas[i].TotalDays;
                                    var Day = AllDays.LastOrDefault();
                                    if (Day == null)
                                    {
                                        Day = Rem;
                                        AllDays.Add(Day);
                                    }
                                    else
                                    {
                                        var All = Day + Rem;
                                        AllDays.Add(All);
                                    }
                                }
                                var RemainDaysss = RemainDays - AllDays.LastOrDefault();
                                if (RemainDaysss != null)
                                {
                                    LeavesMass.Add(new VM_LeavesMass_Transaction
                                    {
                                        ID = item.ID,
                                        CodeEmp = item.Code,
                                        NameEmp = item.Name,
                                        CodeLeave = VacationsSetup.LeaveTypeCode,
                                        NameLeave = VacationsSetup.LeaveTypeNameEnglish,
                                        HiringDate = Hire,
                                        YearlyBalance = LeavesBalance == null ? 0 : LeavesBalance.Balance,
                                        ActualBalance = Leavesfirst == null ? RemainDaysss : Leavesfirst.Remain,
                                        TotalDays = reted
                                    }); ;
                                }
                            }
                            LeavesMass.Add(new VM_LeavesMass_Transaction
                            {
                                ID = item.ID,
                                CodeEmp = item.Code,
                                NameEmp = item.Name,
                                CodeLeave = VacationsSetup.LeaveTypeCode,
                                NameLeave = VacationsSetup.LeaveTypeNameEnglish,
                                HiringDate = Hire,
                                YearlyBalance = LeavesBalance == null ? 0 : LeavesBalance.Balance,
                                ActualBalance = Leavesfirst == null ? RemainDays : Leavesfirst.Remain,
                                TotalDays = reted
                            });
                        }
                    }
                    if (LeavesRequest != null)
                    {
                        if (Leaves != null)
                        {
                            int years = LeavesRequest.year;
                            if (years == FromBalance.Year)
                            {
                                var RemainDays = LeavesRequest.RemainDays;

                                var holiday = LeavesMass.LastOrDefault(a => a.NameEmp == item.Name);
                                if (holiday != null)
                                {
                                    LeavesMass.Remove(holiday);
                                }

                                LeavesMass.Add(new VM_LeavesMass_Transaction
                                {
                                    ID = item.ID,
                                    CodeEmp = item.Code,
                                    NameEmp = item.Name,
                                    CodeLeave = VacationsSetup.LeaveTypeCode,
                                    NameLeave = VacationsSetup.LeaveTypeNameEnglish,
                                    HiringDate = Hire,
                                    YearlyBalance = LeavesBalance == null ? 0 : LeavesBalance.Balance,
                                    ActualBalance = Leavesfirst == null ? RemainDays : Leavesfirst.Remain,
                                    TotalDays = reted
                                });
                            }
                        }
                    }
                    else if (LeavesRequest == null && Leavesfirst == null)
                    {
                        if (Leaves != null)
                        {
                            var RemainDay = record.Balance;
                            var holiday = LeavesMass.LastOrDefault(a => a.NameEmp == item.Name);
                            if (holiday != null)
                            {
                                LeavesMass.Remove(holiday);
                            }
                            LeavesMass.Add(new VM_LeavesMass_Transaction
                            {
                                ID = item.ID,
                                CodeEmp = item.Code,
                                NameEmp = item.Name,
                                CodeLeave = VacationsSetup.LeaveTypeCode,
                                NameLeave = VacationsSetup.LeaveTypeNameEnglish,
                                HiringDate = Hire,
                                YearlyBalance = LeavesBalance == null ? 0 : LeavesBalance.Balance,
                                ActualBalance = Leavesfirst == null ? RemainDay : Leavesfirst.Remain,
                                TotalDays = reted
                            }); ;
                        }
                    }
                }

                return Json(LeavesMass);
            }
            catch (Exception e)
            {
                return Json(null);
            }
        }

        public class VM_LeavesTransactionBalance
        {
            public LeavesMass_Transaction LeavesTransactionBalance { get; set; }
            public string EmployeeName { get; set; }
            public string Vacations_Setup { get; set; }
        }
    }
}