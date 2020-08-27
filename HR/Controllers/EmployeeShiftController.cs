using HR.Models;
using HR.Models.Infra;
using HR.Models.Time_management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,TM,TMSetup,Employee shift schedule")]
    public class EmployeeShiftController : BaseController
    {
        // GET: EmployeeShift
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            try
            {
                var empl = dbcontext.Employee_Shift_schedule.ToList();
                return View(empl);
            }
            catch (Exception)
            {
                return View();
            }
        }
        public ActionResult Create()
        {
            try
            {
                ViewBag.emp = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.daystate = dbcontext.Shiftdaystatus.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                var model = new Employee_Shift_schedule();
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(FormCollection form, Employee_Shift_schedule model)
        {
            try
            {
                ViewBag.emp = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.daystate = dbcontext.Shiftdaystatus.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                var actual_shift = dbcontext.Employee_Shift_schedule.Add(model);
                dbcontext.SaveChanges();
                if (model.Use_As_Default == true)
                {
                    var all_schedule_for_this_employee = dbcontext.Employee_Shift_schedule.Where(m => m.Use_As_Default == true && m.ID != actual_shift.ID);
                    foreach (var item in all_schedule_for_this_employee)
                    {
                        item.Use_As_Default = false;
                        dbcontext.SaveChanges();
                    }
                }
               

                //////////////////////////////////////////
                var from_D = form["fromdate"].Split(',');
                var to_D = form["todate"].Split(',');
                var from_T = form["fromtime"].Split(',');
                var to_T = form["totime"].Split(',');
                var shift = form["shift_ID"].Split(',');
                var state = form["status_ID"].Split(',');
                var Temco = form["Temco"].Split(',');
                var TemDe = form["TemDe"].Split(',');
                var TemDeAl = form["TemDeAl"].Split(',');
                var temp = new Template();
                if (TemDe[0] != "")
                {
                    temp.TemplateCode = Temco[0];
                    temp.TemplateDescription = TemDe[0];
                    temp.Employee_Shift_scheduleID = actual_shift.ID;
                    if (TemDeAl[0] != "")
                    {
                        temp.TemplateAllternativeDescription = TemDeAl[0];
                    }
                    else
                    {
                        temp.TemplateAllternativeDescription = null;
                    }
                    temp.To_date = actual_shift.To_date;
                    temp.From_date = actual_shift.From_date;
                    dbcontext.Template.Add(temp);
                    dbcontext.SaveChanges();
                }
                for (var i = 0; i < shift.Count(); i++)
                {
                    var Si_id = int.Parse(shift[i]);
                    var St_id = int.Parse(state[i]);
                    var f_D = Convert.ToDateTime(from_D[i]);
                    var t_D = Convert.ToDateTime(to_D[i]);
                    var f_t = Convert.ToDateTime(from_T[i]).TimeOfDay;
                    var t_t = Convert.ToDateTime(to_T[i]).TimeOfDay;
                    var details = new Schedule_Details {/*Template = actual_shift.TemplateCode,*/ Employee_Shift_scheduleID = actual_shift.ID, Shift_setupID = Si_id, ShiftdaystatusID = St_id, From_date = f_D, To_date = t_D, From = f_t, To = t_t };
                    dbcontext.Schedule_Details.Add(details);
                    dbcontext.SaveChanges();
                    if (TemDe[0] != "")
                    {
                        var detailss = new Shiftscheduletemplate { TemplateAllternativeDescription_Shifts = TemDeAl[0], TemplateDescription_Shifts = TemDe[0], TemplateCode_Shifts = Temco[0], Employee_Shift_scheduleID = actual_shift.ID, Shift_setupID = Si_id, ShiftdaystatusID = St_id, From_date = f_D, To_date = t_D, From = f_t, To = t_t };
                        dbcontext.Shiftscheduletemplate.Add(detailss);
                        dbcontext.SaveChanges();
                    }
                }

                /////////////////////////////////////////
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.emp = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.daystate = dbcontext.Shiftdaystatus.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                var model = dbcontext.Employee_Shift_schedule.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Edit(FormCollection form, Employee_Shift_schedule model)
        {
            try
            {
                ViewBag.emp = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.shift = dbcontext.Shift_setup.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                ViewBag.daystate = dbcontext.Shiftdaystatus.ToList().Select(m => new { Code = m.Code + "-[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Employee_Shift_schedule.FirstOrDefault(m => m.ID == model.ID);

                /////update emp////
                record.Code = model.Code;
                record.Description = model.Description;
                record.Employee_ProfileID = model.Employee_ProfileID;
                record.From_date = model.From_date;
                record.Name = model.Name;
                record.To_date = model.To_date;
                record.Use_As_Default = model.Use_As_Default;
                if (model.Use_As_Default == true)
                {
                    var all_schedule_for_this_employee = dbcontext.Employee_Shift_schedule.Where(m => m.Use_As_Default == true && m.ID != record.ID);
                    foreach (var item in all_schedule_for_this_employee)
                    {
                        item.Use_As_Default = false;
                        dbcontext.SaveChanges();
                    }
                }
                ///////////////////remove____schedule_details------//////
                var details = dbcontext.Schedule_Details.Where(m => m.Employee_Shift_scheduleID == record.ID);
                dbcontext.Schedule_Details.RemoveRange(details);
                dbcontext.SaveChanges();
                ////////////////////add new schdule//////////////////////
                //////////////////////////////////////////
                var from_D = form["fromdate"].Split(',');
                var to_D = form["todate"].Split(',');
                var from_T = form["fromtime"].Split(',');
                var to_T = form["totime"].Split(',');
                var shift = form["shift_ID"].Split(',');
                var state = form["status_ID"].Split(',');
                var Temco = form["Temco"].Split(',');
                var TemDe = form["TemDe"].Split(',');
                var TemDeAl = form["TemDeAl"].Split(',');
                var temp = new Template();
                if (TemDe[0] != "")
                {
                    temp.TemplateCode = Temco[0];
                    temp.TemplateDescription = TemDe[0];
                    temp.Employee_Shift_scheduleID = record.ID;
                    if (TemDeAl[0] != "")
                    {
                        temp.TemplateAllternativeDescription = TemDeAl[0];
                    }
                    else
                    {
                        temp.TemplateAllternativeDescription = null;
                    }
                    temp.To_date = record.To_date;
                    temp.From_date = record.From_date;
                    dbcontext.Template.Add(temp);
                    dbcontext.SaveChanges();
                }
                for (var i = 0; i < shift.Count(); i++)
                {
                    var Si_id = int.Parse(shift[i]);
                    var St_id = int.Parse(state[i]);
                    var f_D = Convert.ToDateTime(from_D[i]);
                    var t_D = Convert.ToDateTime(to_D[i]);
                    var f_t = Convert.ToDateTime(from_T[i]).TimeOfDay;
                    var t_t = Convert.ToDateTime(to_T[i]).TimeOfDay;
                    var new_details = new Schedule_Details { Employee_Shift_scheduleID = record.ID, Shift_setupID = Si_id, ShiftdaystatusID = St_id, From_date = f_D, To_date = t_D, From = f_t, To = t_t };
                    dbcontext.Schedule_Details.Add(new_details);
                    dbcontext.SaveChanges();
                }


                for (var i = 0; i < shift.Count(); i++)
                {

                    if (TemDe[0] != "")
                    {
                        var Si_id = int.Parse(shift[i]);
                        var St_id = int.Parse(state[i]);
                        var f_D = Convert.ToDateTime(from_D[i]);
                        var t_D = Convert.ToDateTime(to_D[i]);
                        var f_t = Convert.ToDateTime(from_T[i]).TimeOfDay;
                        var t_t = Convert.ToDateTime(from_T[i]).TimeOfDay;

                        var detailss = new Shiftscheduletemplate { TemplateAllternativeDescription_Shifts = TemDeAl[0], TemplateDescription_Shifts = TemDe[0], TemplateCode_Shifts = Temco[0], Employee_Shift_scheduleID = record.ID, Shift_setupID = Si_id, ShiftdaystatusID = St_id, From_date = f_D, To_date = t_D, From = f_t, To = t_t };
                        dbcontext.Shiftscheduletemplate.Add(detailss);
                        dbcontext.SaveChanges();
                    }
                }
                /////////////////////////////////////////
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var model = dbcontext.Employee_Shift_schedule.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");

            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_method(int id)
        {
            var model = dbcontext.Employee_Shift_schedule.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Employee_Shift_schedule.Remove(model);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        /// <summary>
        /// ///json///////
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public JsonResult getcode(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var num_of_sch = dbcontext.Employee_Shift_schedule.Where(m => m.Employee_ProfileID == id).ToList();
            var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == id);
            var code = "";
            if (num_of_sch.Count == 0)
            {
                code = emp.Code + "_SCH_1";
            }
            else
            {
                code = emp.Code + "_SCH_" + (num_of_sch.Count() + 1);
            }
            return Json(code);
        }
        public JsonResult chech_date(string H_from, string H_to, string from, string to, string empid, string code)
        {
            var Hfrom = Convert.ToDateTime(H_from);
            var Hto = Convert.ToDateTime(H_to);
            var fromm = Convert.ToDateTime(from);
            var too = Convert.ToDateTime(to);
            int ID = int.Parse(empid);
            var All_schedule = dbcontext.Employee_Shift_schedule.Where(m => m.Employee_ProfileID == ID && m.Code != code).ToList();

            foreach (var item in All_schedule)
            {
                var tt = DateTime.Compare(Hfrom, item.From_date);

                if (DateTime.Compare(item.From_date, Hfrom) > 0 && DateTime.Compare(Hto, item.To_date) >= 0)//////حواليين التاريخ
                {
                    return Json(HR.Resource.pers_2.errorempl);
                }
                else if (((DateTime.Compare(Hfrom, item.From_date) >= 0) && (DateTime.Compare(item.To_date, Hto) >= 0)))////بين التاريخ
                {
                    return Json(HR.Resource.pers_2.errorempl);
                }
                else if (((DateTime.Compare(item.From_date, Hfrom) >= 0) && (DateTime.Compare(item.To_date, Hto) >= 0) && (DateTime.Compare(Hto, item.From_date) >= 0)))///من بره والى جوه
                {
                    return Json(HR.Resource.pers_2.errorempl);
                }
                else if (((DateTime.Compare(Hto, item.To_date) >= 0) && (DateTime.Compare(item.To_date, Hfrom) >= 0) && (DateTime.Compare(Hfrom, item.From_date) >= 0)))///الى  بره و من جوه
                {
                    return Json(HR.Resource.pers_2.errorempl);
                }

            }
            /////////////////////////////////////////////////////////////////////

            ///////////////////check if small date between huge date/////////////
            var t = DateTime.Compare(Hfrom, fromm);
            var y = DateTime.Compare(Hto, too);
            if (DateTime.Compare(Hfrom, fromm) <= 0 && DateTime.Compare(Hto, too) >= 0)
            {
                return Json("0");
            }
            else
            {
                return Json(HR.Resource.pers_2.errorempl);
            }
        }
    }
}