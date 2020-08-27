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

namespace HR.Controllers.Vacations
{
    [Authorize(Roles = "Admin,Vacations,VacationsTransaction,Vacations Request")]
    public class LeaveRequestController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: LeaveRequest
        public ActionResult Index()
        {
            var LeavesRequestMaster = dbcontext.LeavesRequestMaster.ToList();
            var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();
            var Vacations_Setup = dbcontext.Vacations_Setup.ToList();
            var model = from a in LeavesRequestMaster
                        join b in Employee_Profile on a.EmployeeID equals b.ID
                        join c in Vacations_Setup on a.VacCode equals c.ID
                        select new VM_LeaveRequest
                        { EmployeeName = b.Full_Name, Vacations_Setup = c.LeaveTypeNameEnglish, LeavesRequestMaster = a };
            return View(model);
        }

        public ActionResult create()
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
                ViewBag.Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();
                //DateTime Statis = DateTime.Now;
                var new_record = new LeavesRequestMaster { DateFrom = DateTime.Now, DateTo = DateTime.Now, ActualToDate = DateTime.Now, CurrentDate = DateTime.Now, Created_Date = DateTime.Now, Created_By = User.Identity.Name, RemainDays = 0, TotalDays = 0 };
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
        [HttpPost]
        public ActionResult Create(Headers model, FormCollection form)
        {
            try
            {
                var Bal = dbcontext.Vacations_Setup.FirstOrDefault(a => a.ID == model.LeavesRequestMaster.VacCode);
                var Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.LeavesRequestMaster.EmployeeID).Personnel_Information;
                DateTime Hire = Employee_Profile.Hire_Date;
                var EmploymentDate = Hire.AddMonths(Bal.TakenAfterEmploymentDate);
                if (Bal.TakenAfterEmploymentDate > 0)
                {
                    if (DateTime.Compare(DateTime.Now, EmploymentDate) < 0)
                    {
                        TempData["Message"] = "It must be a Taken After Employment Date equal or bigger from '"+ Bal.TakenAfterEmploymentDate + "'";
                        return RedirectToAction("Create");
                    }
                }
                if (Bal.AcceptNegative == false || Bal.UnlimitedBalance == false)
                {
                    if (model.LeavesRequestMaster.RemainDays <= -1)
                    {
                        TempData["Message"] = "Balance Is Not Enough";
                        return RedirectToAction("Create");
                    }
                }


                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
                ViewBag.Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();

                LeavesRequestMaster new_Record = new LeavesRequestMaster();

                new_Record.SerialNo = model.LeavesRequestMaster.SerialNo;
                new_Record.EmployeeID = model.LeavesRequestMaster.EmployeeID;
                new_Record.VacCode = model.LeavesRequestMaster.VacCode;
                new_Record.DateFrom = model.LeavesRequestMaster.DateFrom;
                new_Record.DateTo = model.LeavesRequestMaster.DateTo;
                new_Record.CurrentDate = model.LeavesRequestMaster.CurrentDate;
                new_Record.ActualToDate = model.LeavesRequestMaster.ActualToDate;
                new_Record.EmpAlternative = model.LeavesRequestMaster.EmpAlternative;
                new_Record.Reason = model.LeavesRequestMaster.Reason;
                new_Record.Approved = check_status.Pending.GetHashCode();
                new_Record.EmpApproved = model.LeavesRequestMaster.EmpApproved;
                new_Record.RequestTypeCode = model.LeavesRequestMaster.RequestTypeCode;
                new_Record.ReturnVac = model.LeavesRequestMaster.ReturnVac;
                new_Record.Settled = model.LeavesRequestMaster.Settled;
                new_Record.CasualLeave = model.LeavesRequestMaster.CasualLeave;
                new_Record.PointID = model.LeavesRequestMaster.PointID;
                new_Record.WithTicket = model.LeavesRequestMaster.WithTicket;
                new_Record.WithExitReEntry = model.LeavesRequestMaster.WithExitReEntry;
                new_Record.AttachedFile = model.LeavesRequestMaster.AttachedFile;

                DateTime? DateFrom = model.LeavesRequestMaster.DateFrom;
                var Year = DateFrom.Value.Year;
                var Month = DateFrom.Value.Month;
                var LeavesRequest = dbcontext.LeavesRequestMaster.Where(a => a.EmployeeID == model.LeavesRequestMaster.EmployeeID && a.VacCode == model.LeavesRequestMaster.VacCode && a.year == Year && a.CasualLeave == true).ToList();
                var LeavesRequestMaster = dbcontext.LeavesRequestMaster.Where(a => a.EmployeeID == model.LeavesRequestMaster.EmployeeID && a.VacCode == model.LeavesRequestMaster.VacCode && a.year == Year).ToList();

                if (model.LeavesRequestMaster.CasualLeave == true)
                {
                    if (Bal.MaxContinousDays < model.LeavesRequestMaster.TotalDays)
                    {
                        TempData["Message"] = "It must be a Max Continous Days equal or less from '" + Bal.MaxContinousDays + "' ";
                        return RedirectToAction("Create");
                    }
                    var AllDays = new List<double?>();
                    if (LeavesRequest != null)
                    {
                        for (int i = 0; i < LeavesRequest.Count; i++)
                        {
                            var Rem = LeavesRequest[i].TotalDays;
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
                        var RemainDay = Bal.MaxCasualDays - model.LeavesRequestMaster.TotalDays - AllDays.LastOrDefault();
                        if (RemainDay < 0)
                        {
                            TempData["Message"] = "It must be a Max Casual Days equal or less from '" + Bal.MaxCasualDays + "' ";
                            return RedirectToAction("Create");
                        }
                    }
                }

                if (Bal.MaximumDaysContinous > 0)
                {
                    if (Bal.MaximumDaysContinous < model.LeavesRequestMaster.TotalDays)
                    {
                        TempData["Message"] = "It must be a Maximum Days Continous equal or less from '" + Bal.MaximumDaysContinous + "' ";
                        return RedirectToAction("Create");
                    }
                }
                if (LeavesRequestMaster != null)
                {
                    var AllDays = new List<double?>();
                    for (int i = 0; i < LeavesRequestMaster.Count; i++)
                    {
                        var ActualToDate = LeavesRequestMaster[i].ActualToDate.Value.Month;
                        if (ActualToDate == Month)
                        {
                            var Rem = LeavesRequestMaster[i].TotalDays;
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
                    }
                    var RemainDay = Bal.MaximumDaysPerMonth - model.LeavesRequestMaster.TotalDays - AllDays.LastOrDefault();
                    if (RemainDay < 0)
                    {
                        TempData["Message"] = "It must be a Maximum Days Per Month equal or less from '" + Bal.MaximumDaysPerMonth + "' ";
                        return RedirectToAction("Create");
                    }
                }


                var CurrentBalance = form["CurrentBalance"].Split(',');
                var FromBalance = form["FromBalance"].Split(',');
                DateTime? year = DateTime.Parse(FromBalance[0]);
                int FBalance = year.Value.Year;
                new_Record.year = FBalance;

                if (LeavesRequestMaster.Count == 0)
                {
                    double? Balance = Convert.ToDouble(CurrentBalance[0]);
                    new_Record.BalanceDays = Balance;
                }
                else
                {
                    var LeavesRequestMasters = dbcontext.LeavesRequestMaster.Where(a => a.EmployeeID == model.LeavesRequestMaster.EmployeeID && a.VacCode == model.LeavesRequestMaster.VacCode && a.year == Year).ToList();
                    var Leaves = LeavesRequestMasters.LastOrDefault();
                    new_Record.BalanceDays = Leaves.RemainDays;
                }
                new_Record.TotalDays = model.LeavesRequestMaster.TotalDays;
                new_Record.RemainDays = model.LeavesRequestMaster.RemainDays;
                new_Record.Serial_LB = model.LeavesRequestMaster.Serial_LB;
                new_Record.HalfDay = model.LeavesRequestMaster.HalfDay;
                new_Record.QuarterDay = model.LeavesRequestMaster.QuarterDay;
                new_Record.QuarterDayCode = model.LeavesRequestMaster.QuarterDayCode;
                new_Record.ShiftFirstHalf = model.LeavesRequestMaster.ShiftFirstHalf;
                new_Record.Company_ID = model.LeavesRequestMaster.Company_ID;
                new_Record.RowIndx = model.LeavesRequestMaster.RowIndx;
                new_Record.Created_By = User.Identity.Name;
                new_Record.Created_Date = DateTime.Now.Date;
                new_Record.Modified_By = model.LeavesRequestMaster.Modified_By;
                new_Record.Modified_Date = model.LeavesRequestMaster.Modified_Date;
                new_Record.ReportAsReadyDate = model.LeavesRequestMaster.ReportAsReadyDate;
                new_Record.ReportAsReadyBy = model.LeavesRequestMaster.ReportAsReadyBy;
                new_Record.ApprovedBy = model.LeavesRequestMaster.ApprovedBy;
                new_Record.ApprovedDate = model.LeavesRequestMaster.ApprovedDate;
                new_Record.RejectedBy = model.LeavesRequestMaster.RejectedBy;
                new_Record.RejectedDate = model.LeavesRequestMaster.RejectedDate;
                new_Record.CanceledBy = model.LeavesRequestMaster.CanceledBy;
                new_Record.CanceledDate = model.LeavesRequestMaster.CanceledDate;
                new_Record.RejectedBy = model.LeavesRequestMaster.RejectedBy;
                new_Record.RejectedDate = model.LeavesRequestMaster.RejectedDate;
                new_Record.ManagerID = model.LeavesRequestMaster.ManagerID;
                new_Record.AltEmpAgreed = model.LeavesRequestMaster.AltEmpAgreed;
                new_Record.AltEmpDisagreeReason = model.LeavesRequestMaster.AltEmpDisagreeReason;
                new_Record.ManagerNotes = model.LeavesRequestMaster.ManagerNotes;
                new_Record.HRNotes = model.LeavesRequestMaster.HRNotes;
                new_Record.GMNotes = model.LeavesRequestMaster.GMNotes;

                new_Record.LastOrder = model.LeavesRequestMaster.LastOrder;

                new_Record.check_status = HR.Models.Infra.check_status.Pending;
                new_Record.name_state = nameof(check_status.Pending);
                var username = User.Identity.GetUserName();
                var Date = Convert.ToDateTime("1/1/1900");
                var s = new status { statu = HR.Models.Infra.check_status.created, created_by = username, Type = Models.Infra.Type.LeavesRequestMaster, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                var st = dbcontext.status.Add(s);
                dbcontext.SaveChanges();
                new_Record.statID = s.ID;

                var LeavesTransactionBalance = dbcontext.LeavesTransactionBalance.Where(a => a.VacCode == model.LeavesRequestMaster.VacCode && a.EmployeeID == model.LeavesRequestMaster.EmployeeID && a.Year == year.Value.Year && a.Check == true).ToList();
                var LeavesTransaction = LeavesTransactionBalance.LastOrDefault();

                var Header = dbcontext.LeavesRequestMaster.Add(new_Record);
                dbcontext.SaveChanges();
                if (LeavesTransaction != null && LeavesTransaction.Check == true)
                {
                    LeavesTransaction.Check = false;
                    dbcontext.SaveChanges();
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
                ViewBag.Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();
               
                var old_model = dbcontext.LeavesRequestMaster.FirstOrDefault(m => m.ID == ID);
                var Head = new Headers { LeavesRequestMaster = old_model, check_status = (check_status)old_model.Approved };
                return View(Head);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(Headers model, FormCollection form)
        {
            try
            {
                var new_Record = dbcontext.LeavesRequestMaster.FirstOrDefault(a => a.ID == model.LeavesRequestMaster.ID);
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == new_Record.statID);
                if (sta.statu == Models.Infra.check_status.Approved || sta.statu == Models.Infra.check_status.Rejected || sta.statu == Models.Infra.check_status.Closed || sta.statu == Models.Infra.check_status.Recervied || sta.statu == Models.Infra.check_status.Canceled)
                {
                    TempData["message"] = HR.Resource.training.status_message;
                    return RedirectToAction("index");
                }

                var Bal = dbcontext.Vacations_Setup.FirstOrDefault(a => a.ID == model.LeavesRequestMaster.VacCode);
                var Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.LeavesRequestMaster.EmployeeID).Personnel_Information;
                DateTime Hire = Employee_Profile.Hire_Date;
                var EmploymentDate = Hire.AddMonths(Bal.TakenAfterEmploymentDate);
                if (Bal.TakenAfterEmploymentDate > 0)
                {
                    if (DateTime.Compare(DateTime.Now, EmploymentDate) < 0)
                    {
                        TempData["Message"] = "It must be a Taken After Employment Date equal or bigger from '" + Bal.TakenAfterEmploymentDate + "'";
                        return RedirectToAction("Create");
                    }
                }
                if (Bal.AcceptNegative == false || Bal.UnlimitedBalance == false)
                {
                    if (model.LeavesRequestMaster.RemainDays <= -1)
                    {
                        TempData["Message"] = "Balance Is Not Enough";
                        return RedirectToAction("Create");
                    }
                }

                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
                ViewBag.Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();
              
                new_Record.SerialNo = model.LeavesRequestMaster.SerialNo;
                new_Record.EmployeeID = model.LeavesRequestMaster.EmployeeID;
                new_Record.VacCode = model.LeavesRequestMaster.VacCode;
                new_Record.DateFrom = model.LeavesRequestMaster.DateFrom;
                new_Record.DateTo = model.LeavesRequestMaster.DateTo;
                new_Record.CurrentDate = model.LeavesRequestMaster.CurrentDate;
                new_Record.ActualToDate = model.LeavesRequestMaster.ActualToDate;
                new_Record.EmpAlternative = model.LeavesRequestMaster.EmpAlternative;
                new_Record.Reason = model.LeavesRequestMaster.Reason;
                new_Record.Approved = check_status.Pending.GetHashCode();
                new_Record.EmpApproved = model.LeavesRequestMaster.EmpApproved;
                new_Record.RequestTypeCode = model.LeavesRequestMaster.RequestTypeCode;
                new_Record.ReturnVac = model.LeavesRequestMaster.ReturnVac;
                new_Record.Settled = model.LeavesRequestMaster.Settled;
                new_Record.CasualLeave = model.LeavesRequestMaster.CasualLeave;
                new_Record.PointID = model.LeavesRequestMaster.PointID;
                new_Record.WithTicket = model.LeavesRequestMaster.WithTicket;
                new_Record.WithExitReEntry = model.LeavesRequestMaster.WithExitReEntry;
                new_Record.AttachedFile = model.LeavesRequestMaster.AttachedFile;

                DateTime? DateFrom = model.LeavesRequestMaster.DateFrom;
                var Year = DateFrom.Value.Year;
                var Month = DateFrom.Value.Month;
                var LeavesRequest = dbcontext.LeavesRequestMaster.Where(a => a.EmployeeID == model.LeavesRequestMaster.EmployeeID && a.VacCode == model.LeavesRequestMaster.VacCode && a.year == Year && a.CasualLeave == true).ToList();
                var LeavesRequestMaster = dbcontext.LeavesRequestMaster.Where(a => a.EmployeeID == model.LeavesRequestMaster.EmployeeID && a.VacCode == model.LeavesRequestMaster.VacCode && a.year == Year).ToList();

                if (model.LeavesRequestMaster.CasualLeave == true)
                {
                    if (Bal.MaxContinousDays < model.LeavesRequestMaster.TotalDays)
                    {
                        TempData["Message"] = "It must be a Max Continous Days equal or less from '" + Bal.MaxContinousDays + "' ";
                        return RedirectToAction("Create");
                    }
                    var AllDays = new List<double?>();
                    if (LeavesRequest != null)
                    {
                        for (int i = 0; i < LeavesRequest.Count; i++)
                        {
                            var Rem = LeavesRequest[i].TotalDays;
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
                        var RemainDay = Bal.MaxCasualDays - model.LeavesRequestMaster.TotalDays - AllDays.LastOrDefault();
                        if (RemainDay < 0)
                        {
                            TempData["Message"] = "It must be a Max Casual Days equal or less from '" + Bal.MaxCasualDays + "' ";
                            return RedirectToAction("Create");
                        }
                    }
                }

                if (Bal.MaximumDaysContinous > 0)
                {
                    if (Bal.MaximumDaysContinous < model.LeavesRequestMaster.TotalDays)
                    {
                        TempData["Message"] = "It must be a Maximum Days Continous equal or less from '" + Bal.MaximumDaysContinous + "' ";
                        return RedirectToAction("Create");
                    }
                }
                if (LeavesRequestMaster != null)
                {
                    var AllDays = new List<double?>();
                    for (int i = 0; i < LeavesRequestMaster.Count; i++)
                    {
                        var ActualToDate = LeavesRequestMaster[i].ActualToDate.Value.Month;
                        if (ActualToDate == Month)
                        {
                            var Rem = LeavesRequestMaster[i].TotalDays;
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
                    }
                    var RemainDay = Bal.MaximumDaysPerMonth - model.LeavesRequestMaster.TotalDays - AllDays.LastOrDefault();
                    if (RemainDay < 0)
                    {
                        TempData["Message"] = "It must be a Maximum Days Per Month equal or less from '" + Bal.MaximumDaysPerMonth + "' ";
                        return RedirectToAction("Create");
                    }
                }


                var CurrentBalance = form["CurrentBalance"].Split(',');
                var FromBalance = form["FromBalance"].Split(',');
                DateTime? year = DateTime.Parse(FromBalance[0]);
                int FBalance = year.Value.Year;
                new_Record.year = FBalance;

                if (LeavesRequestMaster.Count == 0)
                {
                    double? Balance = Convert.ToDouble(CurrentBalance[0]);
                    new_Record.BalanceDays = Balance;
                }
                else
                {
                    var LeavesRequestMasters = dbcontext.LeavesRequestMaster.Where(a => a.EmployeeID == model.LeavesRequestMaster.EmployeeID && a.VacCode == model.LeavesRequestMaster.VacCode && a.year == Year).ToList();
                    var Leaves = LeavesRequestMasters.LastOrDefault();
                    new_Record.BalanceDays = Leaves.RemainDays;
                }
                new_Record.TotalDays = model.LeavesRequestMaster.TotalDays;
                new_Record.RemainDays = model.LeavesRequestMaster.RemainDays;
                new_Record.Serial_LB = model.LeavesRequestMaster.Serial_LB;
                new_Record.HalfDay = model.LeavesRequestMaster.HalfDay;
                new_Record.QuarterDay = model.LeavesRequestMaster.QuarterDay;
                new_Record.QuarterDayCode = model.LeavesRequestMaster.QuarterDayCode;
                new_Record.ShiftFirstHalf = model.LeavesRequestMaster.ShiftFirstHalf;
                new_Record.Company_ID = model.LeavesRequestMaster.Company_ID;
                new_Record.RowIndx = model.LeavesRequestMaster.RowIndx;
                new_Record.Created_By = model.LeavesRequestMaster.Created_By;
                new_Record.Created_Date = model.LeavesRequestMaster.Created_Date;
                new_Record.Modified_By = User.Identity.Name;
                new_Record.Modified_Date = DateTime.Now.Date;
                new_Record.ReportAsReadyDate = model.LeavesRequestMaster.ReportAsReadyDate;
                new_Record.ReportAsReadyBy = model.LeavesRequestMaster.ReportAsReadyBy;
                new_Record.ApprovedBy = model.LeavesRequestMaster.ApprovedBy;
                new_Record.ApprovedDate = model.LeavesRequestMaster.ApprovedDate;
                new_Record.RejectedBy = model.LeavesRequestMaster.RejectedBy;
                new_Record.RejectedDate = model.LeavesRequestMaster.RejectedDate;
                new_Record.CanceledBy = model.LeavesRequestMaster.CanceledBy;
                new_Record.CanceledDate = model.LeavesRequestMaster.CanceledDate;
                new_Record.RejectedBy = model.LeavesRequestMaster.RejectedBy;
                new_Record.RejectedDate = model.LeavesRequestMaster.RejectedDate;
                new_Record.ManagerID = model.LeavesRequestMaster.ManagerID;
                new_Record.AltEmpAgreed = model.LeavesRequestMaster.AltEmpAgreed;
                new_Record.AltEmpDisagreeReason = model.LeavesRequestMaster.AltEmpDisagreeReason;
                new_Record.ManagerNotes = model.LeavesRequestMaster.ManagerNotes;
                new_Record.HRNotes = model.LeavesRequestMaster.HRNotes;
                new_Record.GMNotes = model.LeavesRequestMaster.GMNotes;

                new_Record.LastOrder = model.LeavesRequestMaster.LastOrder;

                var LeavesTransactionBalance = dbcontext.LeavesTransactionBalance.Where(a => a.VacCode == model.LeavesRequestMaster.VacCode && a.EmployeeID == model.LeavesRequestMaster.EmployeeID && a.Year == year.Value.Year && a.Check == true).ToList();
                var LeavesTransaction = LeavesTransactionBalance.LastOrDefault();

                dbcontext.SaveChanges();
                if (LeavesTransaction != null && LeavesTransaction.Check == true)
                {
                    LeavesTransaction.Check = false;
                    dbcontext.SaveChanges();
                }
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        //public ActionResult delete(int id)
        //{
        //    try
        //    {
        //        var model = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == id);
        //        return View(model);
        //    }
        //    catch (Exception)
        //    {
        //        return RedirectToAction("index");
        //    }
        //}
        //[HttpPost]
        //[ActionName("delete")]
        //public ActionResult delete_method(int id)
        //{
        //    var header = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == id);
        //    var H_ = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(m => m.ID == header.ID);
        //    var sta = dbcontext.status.FirstOrDefault(m => m.ID == H_.statID);
        //    if (sta.statu == Models.Infra.check_status.Approved || sta.statu == Models.Infra.check_status.Rejected || sta.statu == Models.Infra.check_status.Closed || sta.statu == Models.Infra.check_status.Recervied || sta.statu == Models.Infra.check_status.Canceled)
        //    {
        //        TempData["message"] = HR.Resource.training.status_message;
        //        return RedirectToAction("index");
        //    }
        //    var details = dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.Where(m => m.TransactionNumber == header.ID.ToString()).ToList();
        //    var status = dbcontext.status.FirstOrDefault(m => m.ID == header.statID);

        //    try
        //    {
        //        dbcontext.Employee_Payroll_Transactions.Remove(header);
        //        if (details.Count > 0)
        //            dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.RemoveRange(details);

        //        dbcontext.status.Remove(status);

        //        dbcontext.SaveChanges();
        //        return RedirectToAction("index");
        //    }
        //    catch (Exception)
        //    {
        //        return View(header);
        //    }
        //}
        public ActionResult status(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.LeavesRequestMaster.FirstOrDefault(m => m.ID == ID);
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
            var record = dbcontext.LeavesRequestMaster.FirstOrDefault(m => m.ID == model.empid);
            //var committe = dbcontext.Committe_Resolution_Recuirtment.Select(a => a.Committe_Resolution_Status).ToList();
            if (model.check_status == HR.Models.Infra.check_status.Approved)
            {
                sta.approved_by = User.Identity.GetUserName();
                sta.approved_bydate = model.status.approved_bydate;
                sta.statu = Models.Infra.check_status.Approved;
                record.check_status = HR.Models.Infra.check_status.Approved;
                record.name_state = nameof(check_status.Approved);
                record.Approved = check_status.Approved.GetHashCode();
                record.ApprovedBy = User.Identity.Name;
                //record.ApprovedDate = DateTime.Now.Date;

                dbcontext.SaveChanges();
            }
            else if (model.check_status == HR.Models.Infra.check_status.Rejected)
            {
                sta.Rejected_by = User.Identity.GetUserName();
                sta.Rejected_bydate = model.status.Rejected_bydate;
                sta.statu = Models.Infra.check_status.Rejected;
                record.check_status = HR.Models.Infra.check_status.Rejected;
                record.name_state = nameof(check_status.Rejected);
                record.Approved = check_status.Rejected.GetHashCode();
                record.RejectedBy = User.Identity.Name;
                //record.RejectedDate = DateTime.Now.Date;
                dbcontext.SaveChanges();
            }
            else if (model.check_status == HR.Models.Infra.check_status.Return_To_Review)
            {
                sta.return_to_reviewby = User.Identity.GetUserName();
                sta.return_to_reviewdate = model.status.return_to_reviewdate;
                sta.statu = Models.Infra.check_status.Return_To_Review;
                record.check_status = HR.Models.Infra.check_status.Return_To_Review;
                record.name_state = nameof(check_status.Return_To_Review);
                record.Approved = check_status.Return_To_Review.GetHashCode();
                record.ReportAsReadyBy = User.Identity.Name;
                //record.ReportAsReadyDate = DateTime.Now.Date;
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
                record.Approved = check_status.Closed.GetHashCode();
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
                record.Approved = check_status.Canceled.GetHashCode();
                record.CanceledBy = User.Identity.Name;
                //record.CanceledDate = DateTime.Now.Date;
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
                record.Approved = check_status.Recervied.GetHashCode();
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
        public class VM_LeaveRequest
        {
            public LeavesRequestMaster LeavesRequestMaster { get; set; }
            public string EmployeeName { get; set; }
            public string Vacations_Setup { get; set; }
        }
    }
}