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
using static HR.Controllers.Vacations.LeavesApproveController;

namespace HR.Controllers.Vacations
{
    [Authorize(Roles = "Admin,Vacations,VacationsCards,Vacation Balance")]
    public class VacationsBalanceController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: VacationsBalance
        public ActionResult Index()
        {
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
            ViewBag.Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();
            var model = new LeavesApprove();
            return View(model);
        }
        public JsonResult Getone(int Emp, int Vac)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
              
                if (Vac != 0 && Emp != 0)
                {

                    var LeavesRequestMaster = dbcontext.LeavesRequestMaster.FirstOrDefault(a => a.VacCode == Vac && a.EmployeeID == Emp);
                    var LeavesBalance = dbcontext.LeavesBalance.Where(a => a.VacCode == Vac && a.EmployeeID == Emp).ToList();
                    var Vacations_Setup = dbcontext.Vacations_Setup.FirstOrDefault(a => a.ID == Vac);

                    var model = from a in LeavesBalance
                                join b in LeavesBalance on a.ID equals b.ID
                                select new 
                                { VacCode = Vacations_Setup.LeaveTypeCode, LeaveTypeNameEnglish = Vacations_Setup.LeaveTypeNameEnglish, From = a.BalanceStartDate, To = a.BalanceEndDate, Balance = a.Balance };

                    return Json(model, JsonRequestBehavior.AllowGet);
                }

                //else if (Emp != 0)
                //{
                //    var LeavesRequestMaster = dbcontext.LeavesRequestMaster.Where(m => DateTime.Compare(m.DateFrom, from) >= 0 && DateTime.Compare(m.DateTo, to) <= 0).ToList();
                //    var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true && a.ID == Emp).ToList();
                //    var Vacations_Setup = dbcontext.Vacations_Setup.ToList();
                //    var model = from a in LeavesRequestMaster
                //                join b in Employee_Profile on a.EmployeeID equals b.ID
                //                join cc in Vacations_Setup on a.VacCode equals cc.ID
                //                select new VM_LeaveRequest
                //                { EmployeeCode = b.Code, VacationsCode = cc.LeaveTypeCode, EmployeeName = b.Full_Name, Vacations_Setup = cc.LeaveTypeNameEnglish, LeavesRequestMaster = a };

                //    return Json(model, JsonRequestBehavior.AllowGet);
                //}
                //else if (Vac != 0)
                //{
                //    var LeavesRequestMaster = dbcontext.LeavesRequestMaster.Where(m => DateTime.Compare(m.DateFrom, from) >= 0 && DateTime.Compare(m.DateTo, to) <= 0).ToList();
                //    var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();
                //    var Vacations_Setup = dbcontext.Vacations_Setup.Where(a => a.ID == Vac).ToList();
                //    var model = from a in LeavesRequestMaster
                //                join b in Employee_Profile on a.EmployeeID equals b.ID
                //                join cc in Vacations_Setup on a.VacCode equals cc.ID
                //                select new VM_LeaveRequest
                //                { EmployeeCode = b.Code, VacationsCode = cc.LeaveTypeCode, EmployeeName = b.Full_Name, Vacations_Setup = cc.LeaveTypeNameEnglish, LeavesRequestMaster = a };

                //    return Json(model, JsonRequestBehavior.AllowGet);
                //}
                return Json(null);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult GetAll(int Emp, int Vac,DateTime FromDate)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var Sum = new List<double?>();
                var Subtract = new List<double?>();

                var Year = FromDate.Year;
                var LeavesRequestMaster = dbcontext.LeavesRequestMaster.Where(a => a.VacCode == Vac && a.EmployeeID == Emp && a.year == Year).ToList();
            var Last =    LeavesRequestMaster.LastOrDefault();
                var LeavesTransactionBalance = dbcontext.LeavesTransactionBalance.Where(a => a.VacCode == Vac && a.EmployeeID == Emp && a.Year == Year).ToList();
                if (LeavesTransactionBalance != null)
                {
                    for (int i = 0; i < LeavesTransactionBalance.Count; i++)
                    {
                        var Value = LeavesTransactionBalance[i].Value;
                        var Balance = LeavesTransactionBalance[i].Balance;
                        var Remain = LeavesTransactionBalance[i].Remain;
                        var total = Value + Balance;

                        var Day = Sum.LastOrDefault();
                        var Days = Subtract.LastOrDefault();

                        if (Day == null && total == Remain)
                        {
                            Day = Value;
                            Sum.Add(Day);
                        }
                        else if (Day != null && total == Remain)
                        {
                            var All = Day + Value;
                            Sum.Add(All);
                        }
                        else if (Days == null && Remain < total)
                        {
                            Days = Value;
                            Subtract.Add(Days);
                        }
                        else if (Days != null && Remain < total)
                        {
                            var All = Days + Value;
                            Subtract.Add(All);
                        }
                    }

                }


                if (Last != null)
                {
                    var s = Sum.LastOrDefault();
                    if (s == null)
                    {
                        s = 0;
                    }
                    var sub = Subtract.LastOrDefault();
                    if (sub == null)
                    {
                        sub = 0;
                    }
                    var Tot = s - sub;
                    var Rem = Last.RemainDays;
                    var date = "=" + Tot + "=" + Rem;
                    return Json(date);
                }

                return Json(null);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }

    }
}