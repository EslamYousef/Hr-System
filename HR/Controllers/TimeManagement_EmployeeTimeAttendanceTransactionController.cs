using Antlr.Runtime;
using HR.Models;
using HR.Models.Infra;
using HR.Models.SetupPayroll;
using HR.Models.Time_management;
using HR.Models.TransactionsPayroll;
using HR.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize]
    public class TimeManagement_EmployeeTimeAttendanceTransactionController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: TimeManagement_EmployeeTimeAttendanceTransaction
        public ActionResult Index()
        {
            var time = dbcontext.TimeManagement_EmployeeTimeAttendanceTransaction_Header.ToList();
            var Employee = dbcontext.Employee_Profile.Where(a=>a.Active==true).ToList();
            var model = from a in time
                        join b in Employee on a.Employee_Code equals b.ID.ToString()
                        select new VM_TimeManagement
                        { 
                        EmployeeName = b.Full_Name, 
                        EmployeeCode = b.Code,
                        TimeManagement_EmployeeTimeAttendanceTransaction_Header = a
                        };
            return View(model);
        }
        public ActionResult create()
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                var Statis = DateTime.Now;
                //var new_record = new TimeManagement_EmployeeTimeAttendanceTransaction_Header ();
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                var model_ = dbcontext.TimeManagement_EmployeeTimeAttendanceTransaction_Header.ToList();
                //if (model_.Count() == 0)
                //{
                //    new_record.TransactionNumber = stru + "1";
                //}
                //else
                //{
                //    new_record.TransactionNumber = stru + (model_.LastOrDefault().ID + 1).ToString();
                //}
                var statis = DateTime.Now;
                var model = new TimeManagement_EmployeeTimeAttendanceTransaction_Header {EffectiveYear =Convert.ToInt32(statis.Year), EffectiveMonth = Convert.ToInt32(statis.Month) };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(TimeManagement_EmployeeTimeAttendanceTransaction_Header model, FormCollection form)
        {
            try
            {
                var LocationCode = form["Location_Code"].Split(',');
                var WorkingSystem = form["Working_System"].Split(','); 
                var ShiftCode = form["Shift_Code"].Split(',');
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.ToList().Select(m => new { Code = m.PaymentTypeCode + "-[" + m.PaymentTypeDesc + ']', ID = m.ID });
               TimeManagement_EmployeeTimeAttendanceTransaction_Header new_Record = new TimeManagement_EmployeeTimeAttendanceTransaction_Header();
                new_Record.Employee_Code = model.Employee_Code;
                new_Record.EffectiveMonth = model.EffectiveMonth;
                new_Record.EffectiveYear = model.EffectiveYear;
                if (LocationCode[0] != "")
                {
                    var locat = LocationCode[0];
                    var loc = dbcontext.work_location.FirstOrDefault(a => a.Name == locat);
                    new_Record.Location_Code = loc.Code;
                }
                if (WorkingSystem[0] != "")
                {
                    var working = WorkingSystem[0];
                    var Day = "Day";
                    var shift8 = "shift -8 Hours";
                    var shift12 = "shift -12 Hours";
                    if (Day == working)
                    {
                        new_Record.Working_System = 1;
                    }
                    else if (shift8 == working)
                    {
                        new_Record.Working_System = 2;
                    }
                    else if(shift12 == working)
                    {
                        new_Record.Working_System = 3;
                    }
                }
                if (ShiftCode[0] != "")
                {
                    var shift = ShiftCode[0];
                    var sh = dbcontext.Shift_setup.FirstOrDefault(a => a.Name == shift);
                    new_Record.Shift_Code = sh.Code;
                }
                DateTime statis2 = Convert.ToDateTime("1/1/1900");
                new_Record.CreatedDate = DateTime.Now.Date;
                new_Record.CreatedBy = User.Identity.Name;
                new_Record.ReportAsReadyDate = statis2;
                new_Record.ApprovedDate = statis2;
                new_Record.RejectedDate = statis2;
                new_Record.CanceledDate = statis2; 
                new_Record.ClosedDate = statis2;
                new_Record.Modified_Date = statis2;
                new_Record.Created_By = User.Identity.Name;
                new_Record.Created_Date = DateTime.Now.Date;

                new_Record.check_status = HR.Models.Infra.check_status.created;
                new_Record.name_state = nameof(check_status.created);
                var username = User.Identity.GetUserName();
                var Date = Convert.ToDateTime("1/1/1900");
                var s = new status { statu = HR.Models.Infra.check_status.created, created_by = username, Type = Models.Infra.Type.ManualPaymentTransactionEntry, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                var st = dbcontext.status.Add(s);
                dbcontext.SaveChanges();
                new_Record.statID = s.ID;

                var Header = dbcontext.TimeManagement_EmployeeTimeAttendanceTransaction_Header.Add(new_Record);
                dbcontext.SaveChanges();

                ///////////////////
                var ID = form["ID"].Split(',');
                var AttendDate = form["AttendDate"].Split(',');
                var week = form["week"].Split(',');
                var fromtime = form["fromtime"].Split(',');
                var totime = form["totime"].Split(',');
                var worklocationcode = form["worklocationcode"].Split(',');
                var worklocationDes = form["worklocationDes"].Split(',');
                var LocationCodes = form["LocationCode"].Split(',');
                var tmcode = form["tmcode"].Split(',');
                var tmdes = form["tmdes"].Split(',');
                var LocationDescription = form["LocationDescription"].Split(',');
                var ShiftCodes = form["ShiftCode"].Split(',');
                var ShiftDescription = form["ShiftDescription"].Split(',');
                var from_time = form["from_time"].Split(',');
                var to_time = form["to_time"].Split(',');
                var empsta = form["empsta"].Split(',');
                var staref = form["staref"].Split(',');
                var activ = form["activ"].Split(',');
                var worksys = form["worksys"].Split(',');
                var comm = form["comm"].Split(',');
                var proqty = form["proqty"].Split(',');
                var damqty = form["damqty"].Split(',');
                var ches = form["che"].Split(',');//
                var ret = new List<uoi>();
                for (var ck = 0; ck < ID.Length; ck++)
                {
                    if (ID[ck] != "")
                    {
                        bool chbool = false;
                        ret.Add(new uoi { check = chbool });
                    }
                }
                var Cht = new List<uoi>();
                for (var chs = 0; chs < ches.Count(); chs++)
                {
                    if (ches[chs] != "")
                    {
                        ret[int.Parse(ches[chs]) - 1].check = true;
                    }
                }


                for (var i = 0; i < ret.Count(); i++)
                {
                    if (ID[i] != "")
                    {

                        var f_t = Convert.ToDateTime(fromtime[i]).TimeOfDay;
                        var t_t = Convert.ToDateTime(totime[i]).TimeOfDay;
                        var timeinafter = Convert.ToDateTime(from_time[i]).TimeOfDay;
                        var timoutafter = Convert.ToDateTime(to_time[i]).TimeOfDay;
                        var isoffical = Convert.ToBoolean(ret[i].check);
                        var empstatus = short.Parse(empsta[i]);
                        var worksy = short.Parse(worksys[i]);
                        var pro = double.Parse(proqty[i]);
                        var dam = double.Parse(damqty[i]);

                        var new_details = new TimeManagement_EmployeeTimeAttendanceTransaction_Detail
                        { TransactionNumber = Header.ID.ToString(), Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, AttendanceDate = DateTime.Parse(AttendDate[i]),
                            Employee_Code = Header.Employee_Code, EffectiveMonth = Header.EffectiveMonth, EffectiveYear = Header.EffectiveYear,
                            TimeIn = f_t, TimeOut = t_t, IsCalling = isoffical, TimeInAfterCalling = timeinafter, TimeOutAfterCalling = timoutafter ,
                            StatusRefrenceCode = staref[i] ,EmployeeStatus = empstatus ,DayStatus_Code = worklocationcode [i] ,
                            Activity = activ[i] , Location_Code = LocationCodes[i] , TM_Location_Code = tmcode[i] ,
                            Working_System = worksy , Shift_Code = ShiftCodes [i] , Comments = comm[i] ,ProductivityQtyValue = pro,  DamagedQtyValue = dam
                        };
                        dbcontext.TimeManagement_EmployeeTimeAttendanceTransaction_Detail.Add(new_details);
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
        public ActionResult status(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.TimeManagement_EmployeeTimeAttendanceTransaction_Header.FirstOrDefault(m => m.ID == ID);
                var st = dbcontext.status.FirstOrDefault(m => m.ID == model.statID);
                ViewBag.statue = dbcontext.status.ToList().Select(m => new { code = m.approved_by });
                var my_model = new employeestate { status = st, empid = ID };

                //if (my_model.status.approved_by == null)
                //    my_model.status.approved_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                //if (my_model.status.Rejected_by == null)
                //    my_model.status.Rejected_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                //if (my_model.status.return_to_reviewby == null)
                //    my_model.status.return_to_reviewdate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
                //if (my_model.status.cancaled_by == null)
                //    my_model.status.cancaled_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
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
            var record = dbcontext.TimeManagement_EmployeeTimeAttendanceTransaction_Header.FirstOrDefault(m => m.ID == model.empid);
            //var committe = dbcontext.Committe_Resolution_Recuirtment.Select(a => a.Committe_Resolution_Status).ToList();
            if (model.check_status == HR.Models.Infra.check_status.Approved)
            {
                sta.approved_by = User.Identity.GetUserName();
                sta.approved_bydate = model.status.approved_bydate;
                record.check_status = HR.Models.Infra.check_status.Approved;
                record.name_state = nameof(check_status.Approved);
                record.ApprovedBy = User.Identity.Name;
                record.ApprovedDate = DateTime.Now.Date;

                dbcontext.SaveChanges();
            }
            else if (model.check_status == HR.Models.Infra.check_status.Rejected)
            {
                sta.Rejected_by = User.Identity.GetUserName();
                sta.Rejected_bydate = model.status.Rejected_bydate;
                record.check_status = HR.Models.Infra.check_status.Rejected;
                record.name_state = nameof(check_status.Rejected);
                record.RejectedBy = User.Identity.Name;
                record.RejectedDate = DateTime.Now.Date;
                dbcontext.SaveChanges();
            }
            else if (model.check_status == HR.Models.Infra.check_status.Return_To_Review)
            {
                sta.return_to_reviewby = User.Identity.GetUserName();
                sta.return_to_reviewdate = model.status.return_to_reviewdate;
                record.check_status = HR.Models.Infra.check_status.Return_To_Review;
                record.name_state = nameof(check_status.Return_To_Review);
                record.ReportAsReadyBy = User.Identity.Name;
                record.ReportAsReadyDate = DateTime.Now.Date;

                dbcontext.SaveChanges();
            }

            return RedirectToAction("index");
        }
        public JsonResult Getalll(List<string> c)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var Employee_Payroll_Transactions = dbcontext.TimeManagement_EmployeeTimeAttendanceTransaction_Header.ToList();
                var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();
                var model = from a in Employee_Payroll_Transactions
                            join b in Employee_Profile on a.Employee_Code equals b.ID.ToString()
                            select new VM_TimeManagement
                            {
                                EmployeeName = b.Full_Name,
                                EmployeeCode = b.Code,
                                TimeManagement_EmployeeTimeAttendanceTransaction_Header = a
                            };

                return Json(model);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult Getone(int? from, int to, List<string> status)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var nn = new List<HR.Models.Infra.check_status>();
                List<TimeManagement_EmployeeTimeAttendanceTransaction_Header> re1 = new List<TimeManagement_EmployeeTimeAttendanceTransaction_Header>();
                foreach (var item in status)
                {
                    if (item != "all")
                    {
                        nn.Add((HR.Models.Infra.check_status)Enum.Parse(typeof(HR.Models.Infra.check_status), item));
                    }
                }
                if (status[0] == "all")
                {
                    var req = dbcontext.TimeManagement_EmployeeTimeAttendanceTransaction_Header.Where(m => m.EffectiveMonth == from && m.EffectiveYear == to).ToList();
                    var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();
                    var model = from a in req
                                join b in Employee_Profile on a.Employee_Code equals b.ID.ToString()
                                select new VM_TimeManagement
                                {
                                    EmployeeName = b.Full_Name,
                                    EmployeeCode = b.Code,
                                    TimeManagement_EmployeeTimeAttendanceTransaction_Header = a
                                };
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
                else if (status[0] != "all")
                {
                    var req = dbcontext.TimeManagement_EmployeeTimeAttendanceTransaction_Header.Where(m => m.EffectiveMonth == from && m.EffectiveYear == to).ToList();
                    var Employee_Profile = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();


                    foreach (var item in nn)
                    {
                        re1.AddRange(req.Where(m => m.check_status == item).ToList());
                    }

                    var modelss = from a in re1
                                  join b in Employee_Profile on a.Employee_Code equals b.ID.ToString()
                                  select new VM_TimeManagement
                                  {
                                      EmployeeName = b.Full_Name,
                                      EmployeeCode = b.Code,
                                      TimeManagement_EmployeeTimeAttendanceTransaction_Header = a
                                  };
                    return Json(modelss, JsonRequestBehavior.AllowGet);
                }

                return Json(re1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        [HttpPost]
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
        public class VM_TimeManagement
        {
            public TimeManagement_EmployeeTimeAttendanceTransaction_Header TimeManagement_EmployeeTimeAttendanceTransaction_Header { get; set; }
            public string EmployeeCode { get; set; }
            public string EmployeeName { get; set; }
        }
    }
}