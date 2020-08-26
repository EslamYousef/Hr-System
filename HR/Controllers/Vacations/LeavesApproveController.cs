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
using System.Globalization;

namespace HR.Controllers.Vacations
{
    [Authorize]
    public class LeavesApproveController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: LeavesApprove
        public ActionResult ShowAllLeaveRequests()
        {
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
            ViewBag.Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();
            var model = new LeavesApprove();
            return View(model);
        }
        [HttpPost]
        public ActionResult ShowAllLeaveRequests(FormCollection form)
        {
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
            ViewBag.Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();

            var CodeEmp = form["CodeEmp"].Split(',');
            var CodeVac = form["CodeVac"].Split(',');
            var FromDate = form["FromDate"].Split(',');
            var ToDate = form["ToDate"].Split(',');
            var Dur = form["Dur"].Split(',');
            var Is_Head = form["Is_Head"].Split(',');

            for (var i = 0; i < CodeEmp.Length; i++)
            {
                if (CodeEmp[i] != "")
                {
                    CultureInfo culture = new CultureInfo("es-ES");
                    String myDate = FromDate[i];
                    DateTime dateFrom = DateTime.Parse(myDate, culture);
                    //CultureInfo culture = new CultureInfo("es-ES");
                    String myDateTo = ToDate[i];
                    DateTime dateTo = DateTime.Parse(myDateTo, culture);
                    //var ff =  date.ToString("MM/dd/yyyy");
                    //  var kf = Convert.ToDateTime(ff);
                    var From = FromDate[i];

                    var CodeEmployee = CodeEmp[i];
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(a => a.Code == CodeEmployee);
                    var CodeVacation = CodeVac[i];
                    var vac = dbcontext.Vacations_Setup.FirstOrDefault(a => a.LeaveTypeCode == CodeVacation);

                    if (Is_Head[i] == "0")
                    {
                        var LeavesRequestMaster = dbcontext.LeavesRequestMaster.FirstOrDefault(a => a.EmployeeID == emp.ID && a.VacCode == vac.ID && a.DateFrom == dateFrom && a.DateTo == dateTo);
                        var Last = dbcontext.LeavesRequestMaster.Where(a => a.EmployeeID == emp.ID && a.VacCode == vac.ID).ToList();
                        var Ch = Last.LastOrDefault();
                        if (LeavesRequestMaster.Approved == 3)
                        {
                            LeavesRequestMaster.Approved = 0;
                            LeavesRequestMaster.Modified_By = User.Identity.Name;
                            LeavesRequestMaster.check_status = HR.Models.Infra.check_status.Pending;
                            LeavesRequestMaster.name_state = nameof(check_status.Pending);
                            dbcontext.SaveChanges();
                        }
                        else if (Ch != null && LeavesRequestMaster.Approved == 4)
                        {
                            LeavesRequestMaster.Approved = 0;
                            var total = LeavesRequestMaster.TotalDays;
                            Ch.RemainDays = Ch.RemainDays - total;
                            LeavesRequestMaster.Modified_By = User.Identity.Name;
                            LeavesRequestMaster.ApprovedBy = User.Identity.Name;
                            LeavesRequestMaster.check_status = HR.Models.Infra.check_status.Pending;
                            LeavesRequestMaster.name_state = nameof(check_status.Pending);
                            dbcontext.SaveChanges();
                        }
                    }
                    else if (Is_Head[i] == "3")
                    {
                        var LeavesRequestMaster = dbcontext.LeavesRequestMaster.FirstOrDefault(a => a.EmployeeID == emp.ID && a.VacCode == vac.ID && a.DateFrom == dateFrom && a.DateTo == dateTo);
                        var Last = dbcontext.LeavesRequestMaster.Where(a => a.EmployeeID == emp.ID && a.VacCode == vac.ID).ToList();
                        var Ch = Last.LastOrDefault();
                        if (LeavesRequestMaster.Approved == 0)
                        {
                            LeavesRequestMaster.Approved = 3;
                            LeavesRequestMaster.Modified_By = User.Identity.Name;
                            LeavesRequestMaster.ApprovedBy = User.Identity.Name;
                            LeavesRequestMaster.check_status = HR.Models.Infra.check_status.Approved;
                            LeavesRequestMaster.name_state = nameof(check_status.Approved);
                            dbcontext.SaveChanges();
                        }
                        else if (Ch != null && LeavesRequestMaster.Approved == 4)
                        {
                            LeavesRequestMaster.Approved = 3;
                            var total = LeavesRequestMaster.TotalDays;
                            Ch.RemainDays = Ch.RemainDays - total;
                            LeavesRequestMaster.Modified_By = User.Identity.Name;
                            LeavesRequestMaster.ApprovedBy = User.Identity.Name;
                            LeavesRequestMaster.check_status = HR.Models.Infra.check_status.Approved;
                            LeavesRequestMaster.name_state = nameof(check_status.Approved);
                            dbcontext.SaveChanges();
                        }
                    }
                    else if (Is_Head[i] == "4")
                    {
                        var LeavesRequestMaster = dbcontext.LeavesRequestMaster.FirstOrDefault(a => a.EmployeeID == emp.ID && a.VacCode == vac.ID && a.DateFrom == dateFrom && a.DateTo == dateTo);
                        var Last = dbcontext.LeavesRequestMaster.Where(a => a.EmployeeID == emp.ID && a.VacCode == vac.ID).ToList();
                        var Ch = Last.LastOrDefault();
                        if (Ch != null && LeavesRequestMaster.Approved == 0 || LeavesRequestMaster.Approved == 3)
                        {
                            LeavesRequestMaster.Approved = 4;
                            var total = LeavesRequestMaster.TotalDays;
                            Ch.RemainDays = Ch.RemainDays + total;
                            LeavesRequestMaster.Modified_By = User.Identity.Name;
                            LeavesRequestMaster.RejectedBy = User.Identity.Name;
                            LeavesRequestMaster.check_status = HR.Models.Infra.check_status.Rejected;
                            LeavesRequestMaster.name_state = nameof(check_status.Rejected);
                            dbcontext.SaveChanges();
                        }
                    }
                    dbcontext.SaveChanges();
                }
            }
            //var model_ = dbcontext.LeavesRequestMaster.ToList();
            var model = new LeavesApprove();
            return View(model);
        }
        [HttpPost]
        public JsonResult getallstatues()
        {
            var list = new List<string>();
            list.Add("Pending");
            list.Add("Approved");
            list.Add("Rejected");
            return Json(list);
        }
        public JsonResult Getalll(List<string> c)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var LeavesRequestMaster = dbcontext.LeavesRequestMaster.ToList();
                var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();
                var Vacations_Setup = dbcontext.Vacations_Setup.ToList();
                var models = from a in LeavesRequestMaster
                             join b in Employee_Profile on a.EmployeeID equals b.ID
                             join cc in Vacations_Setup on a.VacCode equals cc.ID
                             select new VM_LeaveRequest
                             { EmployeeCode = b.Code, VacationsCode = cc.LeaveTypeCode, EmployeeName = b.Full_Name, Vacations_Setup = cc.LeaveTypeNameEnglish, LeavesRequestMaster = a };
                return Json(models);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult Getone(DateTime from, DateTime to, List<string> status, int Emp, int Vac)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var nn = new List<HR.Models.Infra.check_status>();
                List<LeavesRequestMaster> re1 = new List<LeavesRequestMaster>();
                foreach (var item in status)
                {
                    if (item != "all")
                    {
                        nn.Add((HR.Models.Infra.check_status)Enum.Parse(typeof(HR.Models.Infra.check_status), item));
                    }
                }
                if (status[0] == "all" && Vac != 0 && Emp != 0)
                {
                
                    var LeavesRequestMaster = dbcontext.LeavesRequestMaster.Where(m => DateTime.Compare(m.DateFrom, from) >= 0 && DateTime.Compare(m.DateTo, to) <= 0).ToList();
                    var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true && a.ID == Emp).ToList();
                    var Vacations_Setup = dbcontext.Vacations_Setup.Where(a => a.ID == Vac).ToList();
                    var model = from a in LeavesRequestMaster
                                join b in Employee_Profile on a.EmployeeID equals b.ID
                                join cc in Vacations_Setup on a.VacCode equals cc.ID
                                select new VM_LeaveRequest
                                { EmployeeCode = b.Code, VacationsCode = cc.LeaveTypeCode, EmployeeName = b.Full_Name, Vacations_Setup = cc.LeaveTypeNameEnglish, LeavesRequestMaster = a };

                    return Json(model, JsonRequestBehavior.AllowGet);
                }

                else if (status[0] == "all" && Emp != 0)
                {
                    var LeavesRequestMaster = dbcontext.LeavesRequestMaster.Where(m => DateTime.Compare(m.DateFrom, from) >= 0 && DateTime.Compare(m.DateTo, to) <= 0).ToList();
                    var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true && a.ID == Emp).ToList();
                    var Vacations_Setup = dbcontext.Vacations_Setup.ToList();
                    var model = from a in LeavesRequestMaster
                                join b in Employee_Profile on a.EmployeeID equals b.ID
                                join cc in Vacations_Setup on a.VacCode equals cc.ID
                                select new VM_LeaveRequest
                                { EmployeeCode = b.Code, VacationsCode = cc.LeaveTypeCode, EmployeeName = b.Full_Name, Vacations_Setup = cc.LeaveTypeNameEnglish, LeavesRequestMaster = a };

                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else if (status[0] == "all" && Vac != 0)
                {
                    var LeavesRequestMaster = dbcontext.LeavesRequestMaster.Where(m => DateTime.Compare(m.DateFrom, from) >= 0 && DateTime.Compare(m.DateTo, to) <= 0).ToList();
                    var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();
                    var Vacations_Setup = dbcontext.Vacations_Setup.Where(a => a.ID == Vac).ToList();
                    var model = from a in LeavesRequestMaster
                                join b in Employee_Profile on a.EmployeeID equals b.ID
                                join cc in Vacations_Setup on a.VacCode equals cc.ID
                                select new VM_LeaveRequest
                                { EmployeeCode = b.Code, VacationsCode = cc.LeaveTypeCode, EmployeeName = b.Full_Name, Vacations_Setup = cc.LeaveTypeNameEnglish, LeavesRequestMaster = a };

                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else if (status[0] != "all" && Vac != 0 && Emp != 0)
                {
                    var req = dbcontext.LeavesRequestMaster.Where(m => DateTime.Compare(m.DateFrom, from) >= 0 && DateTime.Compare(m.DateTo, to) <= 0).ToList();
                    var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true && a.ID == Emp).ToList();
                    var Vacations_Setup = dbcontext.Vacations_Setup.Where(a => a.ID == Vac).ToList();
                    foreach (var item in nn)
                    {
                        re1.AddRange(req.Where(m => m.check_status == item).ToList());
                    }
                    var model = from a in re1
                                join b in Employee_Profile on a.EmployeeID equals b.ID
                                join cc in Vacations_Setup on a.VacCode equals cc.ID
                                select new VM_LeaveRequest
                                { EmployeeCode = b.Code, VacationsCode = cc.LeaveTypeCode, EmployeeName = b.Full_Name, Vacations_Setup = cc.LeaveTypeNameEnglish, LeavesRequestMaster = a };

                    return Json(model, JsonRequestBehavior.AllowGet);
                }

                else if (status[0] != "all" && Emp != 0)
                {
                    var req = dbcontext.LeavesRequestMaster.Where(m => DateTime.Compare(m.DateFrom, from) >= 0 && DateTime.Compare(m.DateTo, to) <= 0).ToList();
                    var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true && a.ID == Emp).ToList();
                    var Vacations_Setup = dbcontext.Vacations_Setup.ToList();
                    foreach (var item in nn)
                    {
                        re1.AddRange(req.Where(m => m.check_status == item).ToList());
                    }
                    var model = from a in re1
                                join b in Employee_Profile on a.EmployeeID equals b.ID
                                join cc in Vacations_Setup on a.VacCode equals cc.ID
                                select new VM_LeaveRequest
                                { EmployeeCode = b.Code, VacationsCode = cc.LeaveTypeCode, EmployeeName = b.Full_Name, Vacations_Setup = cc.LeaveTypeNameEnglish, LeavesRequestMaster = a };

                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else if (status[0] != "all" && Vac != 0)
                {
                    var req = dbcontext.LeavesRequestMaster.Where(m => DateTime.Compare(m.DateFrom, from) >= 0 && DateTime.Compare(m.DateTo, to) <= 0).ToList();
                    var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();
                    var Vacations_Setup = dbcontext.Vacations_Setup.Where(a => a.ID == Vac).ToList();
                    foreach (var item in nn)
                    {
                        re1.AddRange(req.Where(m => m.check_status == item).ToList());
                    }
                    var model = from a in re1
                                join b in Employee_Profile on a.EmployeeID equals b.ID
                                join cc in Vacations_Setup on a.VacCode equals cc.ID
                                select new VM_LeaveRequest
                                { EmployeeCode = b.Code, VacationsCode = cc.LeaveTypeCode, EmployeeName = b.Full_Name, Vacations_Setup = cc.LeaveTypeNameEnglish, LeavesRequestMaster = a };

                    return Json(model, JsonRequestBehavior.AllowGet);
                }


                return Json(re1);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public class LeavesApprove
        {
            public string EmployeeName { get; set; }
            public string Vacations_Setup { get; set; }
        }
        public class VM_LeaveRequest
        {
            public LeavesRequestMaster LeavesRequestMaster { get; set; }
            public string EmployeeName { get; set; }
            public string EmployeeCode { get; set; }
            public string VacationsCode { get; set; }
            public string Vacations_Setup { get; set; }
        }
    }
}