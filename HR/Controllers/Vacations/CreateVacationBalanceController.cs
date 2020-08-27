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
    [Authorize(Roles = "Admin,Vacations,VacationsCards,Create Vacation Balance")]
    public class CreateVacationBalanceController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: CreateVacationBalance
        public ActionResult Create()
        {
            ViewBag.Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "------[" + m.Full_Name + ']', ID = m.ID });

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.LeavesBalance.ToList();
            var count = 0;
            if (model.Count() == 0)
            {
                count = 1;
            }
            else
            {
                var te = model.LastOrDefault().ID;
                count = te + 1;
            }
            var modell = new LeavesBalance { Serial_LB = stru.Structure_Code + count };
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(LeavesBalance model, FormCollection form)
        {
            try
            {
                ViewBag.Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "------[" + m.Full_Name + ']', ID = m.ID });

                if (model.VacCode == 0)
                {
                    ModelState.AddModelError("", "Vacations Setup Code must enter");
                    return View(model);
                }
                var Leave = dbcontext.LeavesBalance.ToList();

                var emp = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();
                LeavesBalance record = new LeavesBalance();

                var tranyear = form["RowIndx"].Split(',');
                var Balance = tranyear[0];
                DateTime start = Convert.ToDateTime("1/1/" + Balance);
                DateTime end = Convert.ToDateTime("12/31/" + Balance);

                var Bal = dbcontext.Vacations_Setup.FirstOrDefault(a => a.ID == model.VacCode);
                var empID = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.EmployeeID);
                var CheckFemale = dbcontext.Employee_Profile.Where(a => a.Active == true && a.Gender == Gender.female).ToList();
                var TimesPerLife = Bal.TimesPerLife;
                var RenewBalanceevery = Bal.RenewBalanceevery;

                if (model.EmployeeID == null && Bal.FemaleOnly == true)
                {

                    if (Bal.RenewBalance == true && Bal.RenewBalanceevery == 0)
                    {
                        foreach (var item in CheckFemale)
                        {
                            var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == item.ID && a.VacCode == model.VacCode);
                            if (Leaves == null)
                            {
                                if (item.Gender == Gender.female)
                                {
                                    var model_ = dbcontext.LeavesBalance.ToList();
                                    var stru = dbcontext.StructureModels.FirstOrDefault(a => a.All_Models == ChModels.Personnel).Structure_Code;
                                    if (model_.Count() == 0)
                                    {
                                        record.Serial_LB = stru + "1";
                                    }
                                    else
                                    {
                                        record.Serial_LB = stru + (model_.LastOrDefault().ID + 1).ToString();
                                    }
                                    record.EmployeeID = item.ID;
                                    record.VacCode = model.VacCode;
                                    record.BalanceStartDate = start;
                                    record.BalanceEndDate = end;
                                    record.Balance = Bal.TestFormula;
                                    record.Used = 0;
                                    record.UsedBySys = 0;
                                    record.Created_By = User.Identity.Name;
                                    record.Created_Date = DateTime.Now.Date;
                                    if (Bal.Proportional == true)
                                    {
                                        DateTime DateNow = DateTime.Now;
                                        var Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == item.ID).Personnel_Information;
                                        DateTime Hire = Employee_Profile.Hire_Date; //empID.Personnel_Information.Hire_Date    
                                        var month = 12 - Hire.Month;
                                        var year = DateNow.Year - Hire.Year;
                                        if (year < 1)
                                        {
                                            var testformula = (Bal.TestFormula / 12) * month;
                                            record.Balance = testformula;
                                        }
                                    }
                                    dbcontext.LeavesBalance.Add(record);
                                    dbcontext.SaveChanges();
                                }
                            }
                        }
                    }
                    else if (Bal.RenewBalance == true && Bal.RenewBalanceevery != 0)
                    {
                        var LeavesBalance = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode).ToList();
                        foreach (var item in emp)
                        {
                            if (item.Gender == Gender.female)
                            {
                                var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == item.ID && a.VacCode == model.VacCode);
                                if (Leaves == null)
                                {
                                    var model_ = dbcontext.LeavesBalance.ToList();
                                    var stru = dbcontext.StructureModels.FirstOrDefault(a => a.All_Models == ChModels.Personnel).Structure_Code;
                                    if (model_.Count() == 0)
                                    {
                                        record.Serial_LB = stru + "1";
                                    }
                                    else
                                    {
                                        record.Serial_LB = stru + (model_.LastOrDefault().ID + 1).ToString();
                                    }
                                    //record.Serial_LB = model.Serial_LB;
                                    record.EmployeeID = item.ID;
                                    record.VacCode = model.VacCode;
                                    var LeavesBalances = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode && a.EmployeeID == item.ID).ToList();
                                    var LeavesBal = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode && a.EmployeeID == item.ID).ToList();
                                    var LeavesBalss = LeavesBal.LastOrDefault();
                                    var RenewBalance = 0;
                                    if (LeavesBalss != null)
                                    {
                                        DateTime? StartDate = LeavesBalss.BalanceStartDate;
                                        var Year = StartDate.Value.Year;
                                        RenewBalance = Year + RenewBalanceevery;
                                    }
                                    if (LeavesBalances.Count() == 0)
                                    {
                                        record.BalanceStartDate = start;
                                        record.BalanceEndDate = end;
                                        record.Balance = Bal.TestFormula;
                                        record.Used = 0;
                                        record.UsedBySys = 0;
                                        record.Created_By = User.Identity.Name;
                                        record.Created_Date = DateTime.Now.Date;
                                        if (Bal.Proportional == true)
                                        {
                                            DateTime DateNow = DateTime.Now;
                                            var Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == item.ID).Personnel_Information;
                                            DateTime Hire = Employee_Profile.Hire_Date; //empID.Personnel_Information.Hire_Date
                                            var month = 12 - Hire.Month;
                                            var year = DateNow.Year - Hire.Year;
                                            if (year < 1)
                                            {
                                                var testformula = (Bal.TestFormula / 12) * month;
                                                record.Balance = testformula;
                                            }
                                        }
                                        dbcontext.LeavesBalance.Add(record);
                                        dbcontext.SaveChanges();
                                    }

                                    else if (DateTime.Now.Year >= RenewBalance)
                                    {
                                        record.BalanceStartDate = start;
                                        record.BalanceEndDate = end;
                                        record.Balance = Bal.TestFormula;
                                        record.Used = 0;
                                        record.UsedBySys = 0;
                                        record.Created_By = User.Identity.Name;
                                        record.Created_Date = DateTime.Now.Date;
                                        if (Bal.Proportional == true)
                                        {
                                            DateTime DateNow = DateTime.Now;
                                            var Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == item.ID).Personnel_Information;
                                            DateTime Hire = Employee_Profile.Hire_Date; //empID.Personnel_Information.Hire_Date
                                            var month = 12 - Hire.Month;
                                            var year = DateNow.Year - Hire.Year;
                                            if (year < 1)
                                            {
                                                var testformula = (Bal.TestFormula / 12) * month;
                                                record.Balance = testformula;
                                            }
                                        }

                                        dbcontext.LeavesBalance.Add(record);
                                        dbcontext.SaveChanges();
                                    }
                                }
                            }

                        }
                    }
                    else if (Bal.TimesPerLife == 0)
                    {
                        foreach (var item in emp)
                        {
                            if (item.Gender == Gender.female)
                            {
                                var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == item.ID && a.VacCode == model.VacCode);
                                if (Leaves == null)
                                {
                                    var model_ = dbcontext.LeavesBalance.ToList();
                                    var stru = dbcontext.StructureModels.FirstOrDefault(a => a.All_Models == ChModels.Personnel).Structure_Code;
                                    if (model_.Count() == 0)
                                    {
                                        record.Serial_LB = stru + "1";
                                    }
                                    else
                                    {
                                        record.Serial_LB = stru + (model_.LastOrDefault().ID + 1).ToString();
                                    }
                                    //record.Serial_LB = model.Serial_LB;
                                    record.EmployeeID = item.ID;
                                    record.VacCode = model.VacCode;
                                    record.BalanceStartDate = start;
                                    record.BalanceEndDate = end;
                                    record.Balance = Bal.TestFormula;
                                    record.Used = 0;
                                    record.UsedBySys = 0;
                                    record.Created_By = User.Identity.Name;
                                    record.Created_Date = DateTime.Now.Date;
                                    if (Bal.Proportional == true)
                                    {
                                        DateTime DateNow = DateTime.Now;
                                        var Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == item.ID).Personnel_Information;
                                        DateTime Hire = Employee_Profile.Hire_Date; //empID.Personnel_Information.Hire_Date                                
                                        var month = 12 - Hire.Month;
                                        var year = DateNow.Year - Hire.Year;
                                        if (year < 1)
                                        {
                                            var testformula = (Bal.TestFormula / 12) * month;
                                            record.Balance = testformula;
                                        }
                                    }
                                    dbcontext.LeavesBalance.Add(record);
                                    dbcontext.SaveChanges();
                                }
                            }
                        }
                    }
                    else if (Bal.TimesPerLife != 0)
                    {
                        var LeavesBalance = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode).ToList();
                        foreach (var item in emp)
                        {
                            if (item.Gender == Gender.female)
                            {
                                var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == item.ID && a.VacCode == model.VacCode);
                                if (Leaves == null)
                                {
                                    var model_ = dbcontext.LeavesBalance.ToList();
                                    var stru = dbcontext.StructureModels.FirstOrDefault(a => a.All_Models == ChModels.Personnel).Structure_Code;
                                    if (model_.Count() == 0)
                                    {
                                        record.Serial_LB = stru + "1";
                                    }
                                    else
                                    {
                                        record.Serial_LB = stru + (model_.LastOrDefault().ID + 1).ToString();
                                    }
                                    //record.Serial_LB = model.Serial_LB;
                                    record.EmployeeID = item.ID;
                                    record.VacCode = model.VacCode;

                                    var LeavesBalances = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode && a.EmployeeID == item.ID).ToList();
                                    if (LeavesBalances.Count() == 0)
                                    {
                                        record.BalanceStartDate = start;
                                        record.BalanceEndDate = end;
                                        record.Balance = Bal.TestFormula;
                                        record.Used = 0;
                                        record.UsedBySys = 0;
                                        record.Created_By = User.Identity.Name;
                                        record.Created_Date = DateTime.Now.Date;
                                        if (Bal.Proportional == true)
                                        {
                                            DateTime DateNow = DateTime.Now;
                                            var Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == item.ID).Personnel_Information;
                                            DateTime Hire = Employee_Profile.Hire_Date; //empID.Personnel_Information.Hire_Date
                                            var month = 12 - Hire.Month;
                                            var year = DateNow.Year - Hire.Year;
                                            if (year < 1)
                                            {
                                                var testformula = (Bal.TestFormula / 12) * month;
                                                record.Balance = testformula;
                                            }
                                        }
                                        dbcontext.LeavesBalance.Add(record);
                                        dbcontext.SaveChanges();
                                    }
                                    else if (LeavesBalances.Count() < TimesPerLife)
                                    {
                                        record.BalanceStartDate = start;
                                        record.BalanceEndDate = end;
                                        record.Balance = Bal.TestFormula;
                                        record.Used = 0;
                                        record.UsedBySys = 0;
                                        record.Created_By = User.Identity.Name;
                                        record.Created_Date = DateTime.Now.Date;
                                        if (Bal.Proportional == true)
                                        {
                                            DateTime DateNow = DateTime.Now;
                                            var Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == item.ID).Personnel_Information;
                                            DateTime Hire = Employee_Profile.Hire_Date; //empID.Personnel_Information.Hire_Date
                                            var month = 12 - Hire.Month;
                                            var year = DateNow.Year - Hire.Year;
                                            if (year < 1)
                                            {
                                                var testformula = (Bal.TestFormula / 12) * month;
                                                record.Balance = testformula;
                                            }
                                        }
                                        dbcontext.LeavesBalance.Add(record);
                                        dbcontext.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }

                else if (model.EmployeeID == null)
                {
                    if (Bal.RenewBalance == true && Bal.RenewBalanceevery == 0)
                    {
                        foreach (var item in emp)
                        {
                            var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == item.ID && a.VacCode == model.VacCode);
                            if (Leaves == null)
                            {
                                var model_ = dbcontext.LeavesBalance.ToList();
                                var stru = dbcontext.StructureModels.FirstOrDefault(a => a.All_Models == ChModels.Personnel).Structure_Code;
                                if (model_.Count() == 0)
                                {
                                    record.Serial_LB = stru + "1";
                                }
                                else
                                {
                                    record.Serial_LB = stru + (model_.LastOrDefault().ID + 1).ToString();
                                }
                                //record.Serial_LB = model.Serial_LB;
                                record.EmployeeID = item.ID;
                                record.VacCode = model.VacCode;
                                record.BalanceStartDate = start;
                                record.BalanceEndDate = end;
                                record.Balance = Bal.TestFormula;
                                record.Used = 0;
                                record.UsedBySys = 0;
                                record.Created_By = User.Identity.Name;
                                record.Created_Date = DateTime.Now.Date;
                                if (Bal.Proportional == true)
                                {
                                    DateTime DateNow = DateTime.Now;
                                    var Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == item.ID).Personnel_Information;
                                    DateTime Hire = Employee_Profile.Hire_Date; //empID.Personnel_Information.Hire_Date                              
                                    var month = 12 - Hire.Month;
                                    var year = DateNow.Year - Hire.Year;
                                    if (year < 1)
                                    {
                                        var testformula = (Bal.TestFormula / 12) * month;
                                        record.Balance = testformula;
                                    }
                                }
                                dbcontext.LeavesBalance.Add(record);
                                dbcontext.SaveChanges();
                            }
                        }
                    }
                    else if (Bal.RenewBalance == true && Bal.RenewBalanceevery != 0)
                    {
                        var LeavesBalance = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode).ToList();
                        foreach (var item in emp)
                        {
                            var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == item.ID && a.VacCode == model.VacCode);
                            if (Leaves == null)
                            {
                                var model_ = dbcontext.LeavesBalance.ToList();
                                var stru = dbcontext.StructureModels.FirstOrDefault(a => a.All_Models == ChModels.Personnel).Structure_Code;
                                if (model_.Count() == 0)
                                {
                                    record.Serial_LB = stru + "1";
                                }
                                else
                                {
                                    record.Serial_LB = stru + (model_.LastOrDefault().ID + 1).ToString();
                                }
                                //record.Serial_LB = model.Serial_LB;
                                record.EmployeeID = item.ID;
                                record.VacCode = model.VacCode;
                                var LeavesBalances = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode && a.EmployeeID == item.ID).ToList();
                                var LeavesBal = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode && a.EmployeeID == item.ID).ToList();
                                var LeavesBalss = LeavesBal.LastOrDefault();
                                var RenewBalance = 0;
                                if (LeavesBalss != null)
                                {
                                    DateTime? StartDate = LeavesBalss.BalanceStartDate;
                                    var Year = StartDate.Value.Year;
                                    RenewBalance = Year + RenewBalanceevery;
                                }
                                if (LeavesBalances.Count() == 0)
                                {
                                    record.BalanceStartDate = start;
                                    record.BalanceEndDate = end;
                                    record.Balance = Bal.TestFormula;
                                    record.Used = 0;
                                    record.UsedBySys = 0;
                                    record.Created_By = User.Identity.Name;
                                    record.Created_Date = DateTime.Now.Date;
                                    if (Bal.Proportional == true)
                                    {
                                        DateTime DateNow = DateTime.Now;
                                        var Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == item.ID).Personnel_Information;
                                        DateTime Hire = Employee_Profile.Hire_Date; //empID.Personnel_Information.Hire_Date
                                        var month = 12 - Hire.Month;
                                        var year = DateNow.Year - Hire.Year;
                                        if (year < 1)
                                        {
                                            var testformula = (Bal.TestFormula / 12) * month;
                                            record.Balance = testformula;
                                        }
                                    }
                                    dbcontext.LeavesBalance.Add(record);
                                    dbcontext.SaveChanges();
                                }

                                else
                                {
                                    record.BalanceStartDate = start;
                                    record.BalanceEndDate = end;
                                    record.Balance = Bal.TestFormula;
                                    record.Used = 0;
                                    record.UsedBySys = 0;
                                    record.Created_By = User.Identity.Name;
                                    record.Created_Date = DateTime.Now.Date;
                                    if (Bal.Proportional == true)
                                    {
                                        DateTime DateNow = DateTime.Now;
                                        var Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == item.ID).Personnel_Information;
                                        DateTime Hire = Employee_Profile.Hire_Date; //empID.Personnel_Information.Hire_Date
                                        var month = 12 - Hire.Month;
                                        var year = DateNow.Year - Hire.Year;
                                        if (year < 1)
                                        {
                                            var testformula = (Bal.TestFormula / 12) * month;
                                            record.Balance = testformula;
                                        }
                                    }
                                    dbcontext.LeavesBalance.Add(record);
                                    dbcontext.SaveChanges();
                                }
                            }
                        }
                    }
                    else if (Bal.TimesPerLife == 0)
                    {
                        foreach (var item in emp)
                        {
                            var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == item.ID && a.VacCode == model.VacCode);
                            if (Leaves == null)
                            {
                                var model_ = dbcontext.LeavesBalance.ToList();
                                var stru = dbcontext.StructureModels.FirstOrDefault(a => a.All_Models == ChModels.Personnel).Structure_Code;
                                if (model_.Count() == 0)
                                {
                                    record.Serial_LB = stru + "1";
                                }
                                else
                                {
                                    record.Serial_LB = stru + (model_.LastOrDefault().ID + 1).ToString();
                                }
                                //record.Serial_LB = model.Serial_LB;
                                record.EmployeeID = item.ID;
                                record.VacCode = model.VacCode;
                                record.BalanceStartDate = start;
                                record.BalanceEndDate = end;
                                record.Balance = Bal.TestFormula;
                                record.Used = 0;
                                record.UsedBySys = 0;
                                record.Created_By = User.Identity.Name;
                                record.Created_Date = DateTime.Now.Date;
                                if (Bal.Proportional == true)
                                {
                                    DateTime DateNow = DateTime.Now;
                                    var Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == item.ID).Personnel_Information;
                                    DateTime Hire = Employee_Profile.Hire_Date; //empID.Personnel_Information.Hire_Date                                
                                    var month = 12 - Hire.Month;
                                    var year = DateNow.Year - Hire.Year;
                                    if (year < 1)
                                    {
                                        var testformula = (Bal.TestFormula / 12) * month;
                                        record.Balance = testformula;
                                    }
                                }
                                dbcontext.LeavesBalance.Add(record);
                                dbcontext.SaveChanges();
                            }
                        }
                    }
                    else if (Bal.TimesPerLife != 0)
                    {
                        var LeavesBalance = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode).ToList();
                        foreach (var item in emp)
                        {
                            var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == item.ID && a.VacCode == model.VacCode);
                            if (Leaves == null)
                            {
                                var model_ = dbcontext.LeavesBalance.ToList();
                                var stru = dbcontext.StructureModels.FirstOrDefault(a => a.All_Models == ChModels.Personnel).Structure_Code;
                                if (model_.Count() == 0)
                                {
                                    record.Serial_LB = stru + "1";
                                }
                                else
                                {
                                    record.Serial_LB = stru + (model_.LastOrDefault().ID + 1).ToString();
                                }
                                //record.Serial_LB = model.Serial_LB;
                                record.EmployeeID = item.ID;
                                record.VacCode = model.VacCode;

                                var LeavesBalances = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode && a.EmployeeID == item.ID).ToList();
                                if (LeavesBalances.Count() == 0)
                                {
                                    record.BalanceStartDate = start;
                                    record.BalanceEndDate = end;
                                    record.Balance = Bal.TestFormula;
                                    record.Used = 0;
                                    record.UsedBySys = 0;
                                    record.Created_By = User.Identity.Name;
                                    record.Created_Date = DateTime.Now.Date;
                                    if (Bal.Proportional == true)
                                    {
                                        DateTime DateNow = DateTime.Now;
                                        var Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == item.ID).Personnel_Information;
                                        DateTime Hire = Employee_Profile.Hire_Date; //empID.Personnel_Information.Hire_Date
                                        var month = 12 - Hire.Month;
                                        var year = DateNow.Year - Hire.Year;
                                        if (year < 1)
                                        {
                                            var testformula = (Bal.TestFormula / 12) * month;
                                            record.Balance = testformula;
                                        }
                                    }
                                    dbcontext.LeavesBalance.Add(record);
                                    dbcontext.SaveChanges();
                                }

                                else if (LeavesBalances.Count() < TimesPerLife)
                                {
                                    record.BalanceStartDate = start;
                                    record.BalanceEndDate = end;
                                    record.Balance = Bal.TestFormula;
                                    record.Used = 0;
                                    record.UsedBySys = 0;
                                    record.Created_By = User.Identity.Name;
                                    record.Created_Date = DateTime.Now.Date;
                                    if (Bal.Proportional == true)
                                    {
                                        DateTime DateNow = DateTime.Now;
                                        var Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == item.ID).Personnel_Information;
                                        DateTime Hire = Employee_Profile.Hire_Date; //empID.Personnel_Information.Hire_Date
                                        var month = 12 - Hire.Month;
                                        var year = DateNow.Year - Hire.Year;
                                        if (year < 1)
                                        {
                                            var testformula = (Bal.TestFormula / 12) * month;
                                            record.Balance = testformula;
                                        }
                                    }
                                    dbcontext.LeavesBalance.Add(record);
                                    dbcontext.SaveChanges();
                                }

                            }
                        }
                    }
                }

                else if (Bal.FemaleOnly == true && empID.Gender == Gender.female)
                {
                    if (Bal.RenewBalance == true && Bal.RenewBalanceevery == 0)
                    {
                        var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == empID.ID && a.VacCode == model.VacCode);
                        if (Leaves == null)
                        {
                            record.Serial_LB = model.Serial_LB;
                            record.EmployeeID = model.EmployeeID;
                            record.VacCode = model.VacCode;
                            record.BalanceStartDate = start;
                            record.BalanceEndDate = end;
                            record.Balance = Bal.TestFormula;
                            record.Used = 0;
                            record.UsedBySys = 0;
                            record.Created_By = User.Identity.Name;
                            record.Created_Date = DateTime.Now.Date;
                            if (Bal.Proportional == true)
                            {
                                DateTime DateNow = DateTime.Now;
                                DateTime Hire = empID.Personnel_Information.Hire_Date;
                                var month = 12 - Hire.Month;
                                var year = DateNow.Year - Hire.Year;
                                if (year < 1)
                                {
                                    var testformula = (Bal.TestFormula / 12) * month;
                                    record.Balance = testformula;
                                }
                            }
                            dbcontext.LeavesBalance.Add(record);
                            dbcontext.SaveChanges();
                        }
                    }
                    else if (Bal.RenewBalance == true && Bal.RenewBalanceevery != 0)
                    {
                        var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == empID.ID && a.VacCode == model.VacCode);
                        if (Leaves == null)
                        {
                            record.Serial_LB = model.Serial_LB;
                            record.EmployeeID = model.EmployeeID;
                            record.VacCode = model.VacCode;
                            var LeavesBalances = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode && a.EmployeeID == empID.ID).ToList();
                            var LeavesBal = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode && a.EmployeeID == empID.ID).ToList();
                            var LeavesBalss = LeavesBal.LastOrDefault();
                            var RenewBalance = 0;
                            if (LeavesBalss != null)
                            {
                                DateTime? StartDate = LeavesBalss.BalanceStartDate;
                                var Year = StartDate.Value.Year;
                                RenewBalance = Year + RenewBalanceevery;
                            }
                            if (LeavesBalances.Count() == 0)
                            {
                                record.BalanceStartDate = start;
                                record.BalanceEndDate = end;
                                record.Balance = Bal.TestFormula;
                                record.Used = 0;
                                record.UsedBySys = 0;
                                record.Created_By = User.Identity.Name;
                                record.Created_Date = DateTime.Now.Date;
                                if (Bal.Proportional == true)
                                {
                                    DateTime DateNow = DateTime.Now;
                                    DateTime Hire = empID.Personnel_Information.Hire_Date;
                                    var month = 12 - Hire.Month;
                                    var year = DateNow.Year - Hire.Year;
                                    if (year < 1)
                                    {
                                        var testformula = (Bal.TestFormula / 12) * month;
                                        record.Balance = testformula;
                                    }
                                }
                                dbcontext.LeavesBalance.Add(record);
                                dbcontext.SaveChanges();
                            }

                            else
                            {
                                record.BalanceStartDate = start;
                                record.BalanceEndDate = end;
                                record.Balance = Bal.TestFormula;
                                record.Used = 0;
                                record.UsedBySys = 0;
                                record.Created_By = User.Identity.Name;
                                record.Created_Date = DateTime.Now.Date;
                                if (Bal.Proportional == true)
                                {
                                    DateTime DateNow = DateTime.Now;
                                    DateTime Hire = empID.Personnel_Information.Hire_Date;
                                    var month = 12 - Hire.Month;
                                    var year = DateNow.Year - Hire.Year;
                                    if (year < 1)
                                    {
                                        var testformula = (Bal.TestFormula / 12) * month;
                                        record.Balance = testformula;
                                    }
                                }
                                dbcontext.LeavesBalance.Add(record);
                                dbcontext.SaveChanges();
                            }
                        }
                    }
                    else if (Bal.TimesPerLife == 0)
                    {
                        var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == empID.ID && a.VacCode == model.VacCode);
                        if (Leaves == null)
                        {
                            record.Serial_LB = model.Serial_LB;
                            record.EmployeeID = model.EmployeeID;
                            record.VacCode = model.VacCode;
                            record.BalanceStartDate = start;
                            record.BalanceEndDate = end;
                            record.Balance = Bal.TestFormula;
                            record.Used = 0;
                            record.UsedBySys = 0;
                            record.Created_By = User.Identity.Name;
                            record.Created_Date = DateTime.Now.Date;
                            if (Bal.Proportional == true)
                            {
                                DateTime DateNow = DateTime.Now;
                                DateTime Hire = empID.Personnel_Information.Hire_Date;
                                var month = 12 - Hire.Month;
                                var year = DateNow.Year - Hire.Year;
                                if (year < 1)
                                {
                                    var testformula = (Bal.TestFormula / 12) * month;
                                    record.Balance = testformula;
                                }
                            }
                            dbcontext.LeavesBalance.Add(record);
                            dbcontext.SaveChanges();
                        }
                    }
                    else if (Bal.TimesPerLife != 0)
                    {
                        var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == empID.ID && a.VacCode == model.VacCode);
                        if (Leaves == null)
                        {
                            record.Serial_LB = model.Serial_LB;
                            record.EmployeeID = model.EmployeeID;
                            record.VacCode = model.VacCode;
                            var LeavesBalances = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode && a.EmployeeID == empID.ID).ToList();
                            if (LeavesBalances.Count() == 0)
                            {
                                record.BalanceStartDate = start;
                                record.BalanceEndDate = end;
                                record.Balance = Bal.TestFormula;
                                record.Used = 0;
                                record.UsedBySys = 0;
                                record.Created_By = User.Identity.Name;
                                record.Created_Date = DateTime.Now.Date;
                                if (Bal.Proportional == true)
                                {
                                    DateTime DateNow = DateTime.Now;
                                    DateTime Hire = empID.Personnel_Information.Hire_Date;
                                    var month = 12 - Hire.Month;
                                    var year = DateNow.Year - Hire.Year;
                                    if (year < 1)
                                    {
                                        var testformula = (Bal.TestFormula / 12) * month;
                                        record.Balance = testformula;
                                    }
                                }
                                dbcontext.LeavesBalance.Add(record);
                                dbcontext.SaveChanges();
                            }
                            else if (LeavesBalances.Count() < TimesPerLife)
                            {
                                record.BalanceStartDate = start;
                                record.BalanceEndDate = end;
                                record.Balance = Bal.TestFormula;
                                record.Used = 0;
                                record.UsedBySys = 0;
                                record.Created_By = User.Identity.Name;
                                record.Created_Date = DateTime.Now.Date;
                                if (Bal.Proportional == true)
                                {
                                    DateTime DateNow = DateTime.Now;
                                    DateTime Hire = empID.Personnel_Information.Hire_Date;
                                    var month = 12 - Hire.Month;
                                    var year = DateNow.Year - Hire.Year;
                                    if (year < 1)
                                    {
                                        var testformula = (Bal.TestFormula / 12) * month;
                                        record.Balance = testformula;
                                    }
                                }
                                dbcontext.LeavesBalance.Add(record);
                                dbcontext.SaveChanges();
                            }
                        }
                    }
                }

                else if (Bal.FemaleOnly == false)
                {
                    if (Bal.RenewBalance == true && Bal.RenewBalanceevery == 0)
                    {
                        var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == empID.ID && a.VacCode == model.VacCode);
                        if (Leaves == null)
                        {
                            record.Serial_LB = model.Serial_LB;
                            record.EmployeeID = model.EmployeeID;
                            record.VacCode = model.VacCode;
                            record.BalanceStartDate = start;
                            record.BalanceEndDate = end;
                            record.Balance = Bal.TestFormula;
                            record.Used = 0;
                            record.UsedBySys = 0;
                            record.Created_By = User.Identity.Name;
                            record.Created_Date = DateTime.Now.Date;
                            if (Bal.Proportional == true)
                            {
                                DateTime DateNow = DateTime.Now;
                                DateTime Hire = empID.Personnel_Information.Hire_Date;
                                var month = 12 - Hire.Month;
                                var year = DateNow.Year - Hire.Year;
                                if (year < 1)
                                {
                                    var testformula = (Bal.TestFormula / 12) * month;
                                    record.Balance = testformula;
                                }
                            }
                            dbcontext.LeavesBalance.Add(record);
                            dbcontext.SaveChanges();
                        }
                    }
                    else if (Bal.RenewBalance == true && Bal.RenewBalanceevery != 0)
                    {
                        var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == empID.ID && a.VacCode == model.VacCode);
                        if (Leaves == null)
                        {
                            record.Serial_LB = model.Serial_LB;
                            record.EmployeeID = model.EmployeeID;
                            record.VacCode = model.VacCode;
                            var LeavesBalances = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode && a.EmployeeID == empID.ID).ToList();
                            var LeavesBal = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode && a.EmployeeID == empID.ID).ToList();
                            var LeavesBalss = LeavesBal.LastOrDefault();
                            var RenewBalance = 0;
                            if (LeavesBalss != null)
                            {
                                DateTime? StartDate = LeavesBalss.BalanceStartDate;
                                var Year = StartDate.Value.Year;
                                RenewBalance = Year + RenewBalanceevery;
                            }

                            if (LeavesBalances.Count() == 0)
                            {
                                record.BalanceStartDate = start;
                                record.BalanceEndDate = end;
                                record.Balance = Bal.TestFormula;
                                record.Used = 0;
                                record.UsedBySys = 0;
                                record.Created_By = User.Identity.Name;
                                record.Created_Date = DateTime.Now.Date;
                                if (Bal.Proportional == true)
                                {
                                    DateTime DateNow = DateTime.Now;
                                    DateTime Hire = empID.Personnel_Information.Hire_Date;
                                    var month = 12 - Hire.Month;
                                    var year = DateNow.Year - Hire.Year;
                                    if (year < 1)
                                    {
                                        var testformula = (Bal.TestFormula / 12) * month;
                                        record.Balance = testformula;
                                    }
                                }
                                dbcontext.LeavesBalance.Add(record);
                                dbcontext.SaveChanges();
                            }
                            else 
                            {
                                record.BalanceStartDate = start;
                                record.BalanceEndDate = end;
                                record.Balance = Bal.TestFormula;
                                record.Used = 0;
                                record.UsedBySys = 0;
                                record.Created_By = User.Identity.Name;
                                record.Created_Date = DateTime.Now.Date;
                                if (Bal.Proportional == true)
                                {
                                    DateTime DateNow = DateTime.Now;
                                    DateTime Hire = empID.Personnel_Information.Hire_Date;
                                    var month = 12 - Hire.Month;
                                    var year = DateNow.Year - Hire.Year;
                                    if (year < 1)
                                    {
                                        var testformula = (Bal.TestFormula / 12) * month;
                                        record.Balance = testformula;
                                    }
                                }
                                dbcontext.LeavesBalance.Add(record);
                                dbcontext.SaveChanges();
                            }
                        }
                    }
                    else if (Bal.TimesPerLife == 0)
                    {
                        var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == empID.ID && a.VacCode == model.VacCode);
                        if (Leaves == null)
                        {
                            record.Serial_LB = model.Serial_LB;
                            record.EmployeeID = model.EmployeeID;
                            record.VacCode = model.VacCode;
                            record.BalanceStartDate = start;
                            record.BalanceEndDate = end;
                            record.Balance = Bal.TestFormula;
                            record.Used = 0;
                            record.UsedBySys = 0;
                            record.Created_By = User.Identity.Name;
                            record.Created_Date = DateTime.Now.Date;
                            if (Bal.Proportional == true)
                            {
                                DateTime DateNow = DateTime.Now;
                                DateTime Hire = empID.Personnel_Information.Hire_Date;
                                var month = 12 - Hire.Month;
                                var year = DateNow.Year - Hire.Year;
                                if (year < 1)
                                {
                                    var testformula = (Bal.TestFormula / 12) * month;
                                    record.Balance = testformula;
                                }
                            }
                            dbcontext.LeavesBalance.Add(record);
                            dbcontext.SaveChanges();
                        }
                    }
                    else if (Bal.TimesPerLife != 0)
                    {
                        var Leaves = dbcontext.LeavesBalance.FirstOrDefault(a => a.BalanceStartDate == start && a.EmployeeID == empID.ID && a.VacCode == model.VacCode);
                        if (Leaves == null)
                        {
                            record.Serial_LB = model.Serial_LB;
                            record.EmployeeID = model.EmployeeID;
                            record.VacCode = model.VacCode;
                            var LeavesBalances = dbcontext.LeavesBalance.Where(a => a.VacCode == model.VacCode && a.EmployeeID == empID.ID).ToList();
                            if (LeavesBalances.Count() == 0)
                            {
                                record.BalanceStartDate = start;
                                record.BalanceEndDate = end;
                                record.Balance = Bal.TestFormula;
                                record.Used = 0;
                                record.UsedBySys = 0;
                                record.Created_By = User.Identity.Name;
                                record.Created_Date = DateTime.Now.Date;
                                if (Bal.Proportional == true)
                                {
                                    DateTime DateNow = DateTime.Now;
                                    DateTime Hire = empID.Personnel_Information.Hire_Date;
                                    var month = 12 - Hire.Month;
                                    var year = DateNow.Year - Hire.Year;
                                    if (year < 1)
                                    {
                                        var testformula = (Bal.TestFormula / 12) * month;
                                        record.Balance = testformula;
                                    }
                                }
                                dbcontext.LeavesBalance.Add(record);
                                dbcontext.SaveChanges();
                            }
                            else if (LeavesBalances.Count() < TimesPerLife)
                            {
                                record.BalanceStartDate = start;
                                record.BalanceEndDate = end;
                                record.Balance = Bal.TestFormula;
                                record.Used = 0;
                                record.UsedBySys = 0;
                                record.Created_By = User.Identity.Name;
                                record.Created_Date = DateTime.Now.Date;
                                if (Bal.Proportional == true)
                                {
                                    DateTime DateNow = DateTime.Now;
                                    DateTime Hire = empID.Personnel_Information.Hire_Date;
                                    var month = 12 - Hire.Month;
                                    var year = DateNow.Year - Hire.Year;
                                    if (year < 1)
                                    {
                                        var testformula = (Bal.TestFormula / 12) * month;
                                        record.Balance = testformula;
                                    }
                                }
                                dbcontext.LeavesBalance.Add(record);
                                dbcontext.SaveChanges();
                            }
                        }
                    }
                }

                return RedirectToAction("Create");
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        public JsonResult DifferenceTwoDates(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                var Diff = BusinessLayers.DateTimeSpans.CompareDates(EndDate.Date, StartDate.Date);
                return Json(new { success = true, DateDiff = Diff }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetLeaveRequestFormVacationSetup(DateTime from, DateTime to, int emp, int Day, int Vacation)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var EmployeeProfile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == emp);
            var Weekendsetup = dbcontext.Weekend_setup.FirstOrDefault(a => a.ID == EmployeeProfile.Weekendcode);
            var VacationsSetup = dbcontext.Vacations_Setup.FirstOrDefault(a => a.ID == Vacation);

            var ret = new List<DateTime>();
            var Remain = new List<DateTime>();
            var year = Convert.ToDateTime(from).Year;
            //var month = Convert.ToDateTime(from).Month;
            //int day = Convert.ToDateTime(from).Day;

            for (DateTime date = from; date <= to; date = date.AddDays(1))
            {
                ret.Add(date);
            }
            if (VacationsSetup.IncludeWeekEnd == true)
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
            foreach (var item in Remain)
            {
                ret.Remove(item);
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
                    foreach (var item in MyHoliday)
                    {
                        var holiday = ret.Contains(item);
                        if (holiday != false)
                        {
                            ret.Remove(item);
                        }
                    }
                }
            }
            var reted = ret.Count();

            return Json(reted);
        }

    }
}