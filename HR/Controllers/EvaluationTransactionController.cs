using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class EvaluationTransactionController : Controller
    {
        ApplicationDbContext Context = new ApplicationDbContext();
        // GET: EvaluationTransaction
        public ActionResult Index()
        {
            try
            {
                var record = Context.EvaluationTransaction.ToList();
                return View(record);
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
                ViewBag.employee = Context.Employee_Profile.Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.groupcode = Context.PerformanceEvaluationGroup.Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.plancode = Context.EvaluationPlan.ToList().Select(m=>new { Code=m.Code+"->"+m.Name,ID=m.ID});
                var model = new EvaluationTransaction();
                // model.status.statu = Models.Infra.check_status.created;
                model.AppraisalDate = DateTime.Now.Date;
                model.FromeDate = DateTime.Now.Date;
                model.ToDate = DateTime.Now.Date;
                model.check_status = check_status.created;
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }

        //////json///////
        public JsonResult getemp(int id)
        {
            Context.Configuration.ProxyCreationEnabled = false;
            var emps = Context.Employee_Profile.ToList();
            var per_emp = Context.per_emp.Where(m => m.Employee_ProfileID == id);
            var olan_evaluation = from e in emps
                                  join d in per_emp on e.ID equals d.Employee_ProfileID
                                  select e;

            var u = olan_evaluation.Select(m => new { code = m.Code + "->" + m.Name, ID = m.ID }).ToList();
            return Json(u);
        }
        public JsonResult getdate(int id,int emp_id,string date2    )
        {
            var date = new dateVM();
            var plan = Context.EvaluationPlan.FirstOrDefault(m => m.ID == id);
            var per_emp = Context.per_emp.Where(m => m.PerformanceEvaluationGroupID == id);
            var emp = Context.Employee_Profile.FirstOrDefault(m => m.ID == emp_id);
            var all_emp = Context.Employee_Profile.ToList();
            var list_emp_tide_to_plan =  from e in all_emp
                                         join d in per_emp on e.ID equals d.Employee_ProfileID
                                                              select e;
            if (!list_emp_tide_to_plan.ToList().Contains(emp))
            {
                date.flag_emp = false;
                return Json(date);
            }
            var input_date = Convert.ToDateTime(date2).Year;
           
            var date_now = DateTime.Now.Date;
            var month = date_now.Month;
           
            if (plan.EvaluationType.Periods == Periods.Yearly)
            {
                 date = new dateVM { flag_emp = true, from_date=plan.PlaneSchedule[0].period_start.ToShortDateString(), to_date=plan.PlaneSchedule[0].period_End.ToShortDateString() };
                date.line = plan.Code + "_1";
                date.code = date.line + "_" + emp.Code;
            }
            else if (plan.EvaluationType.Periods == Periods.Monyhely)
            {
                 date = new dateVM { flag_emp = true, from_date = plan.PlaneSchedule[month-1].period_start.ToShortDateString(), to_date = plan.PlaneSchedule[month - 1].period_End.ToShortDateString() };

                date.line = plan.Code + "_"+month;
                date.code = date.line + "_" + emp.Code;
            }
            else if (plan.EvaluationType.Periods == Periods.Quarter)
            {
                if (month <= 3)
                {
                     date = new dateVM { flag_emp = true, from_date = plan.PlaneSchedule[0].period_start.ToShortDateString(), to_date = plan.PlaneSchedule[0].period_End.ToShortDateString() };
                    date.line = plan.Code + "_1";
                    date.code = date.line + "_" + emp.Code;
                }
                else if (month <= 6)
                {
                     date = new dateVM { flag_emp = true, from_date = plan.PlaneSchedule[1].period_start.ToShortDateString(), to_date = plan.PlaneSchedule[1].period_End.ToShortDateString() };

                    date.line = plan.Code + "_2";
                    date.code = date.line + "_" + emp.Code;
                }
                else if (month <= 9)
                {
                     date = new dateVM { flag_emp = true, from_date = plan.PlaneSchedule[2].period_start.ToShortDateString(), to_date = plan.PlaneSchedule[2].period_End.ToShortDateString() };
                    date.line = plan.Code + "_3";
                    date.code = date.line + "_" + emp.Code;
                }
                else
                {
                     date = new dateVM { flag_emp = true, from_date = plan.PlaneSchedule[3].period_start.ToShortDateString(), to_date = plan.PlaneSchedule[3].period_End.ToShortDateString() };
                    date.line = plan.Code + "_4";
                    date.code = date.line + "_" + emp.Code;
                }
            }
            else if (plan.EvaluationType.Periods == Periods.Half_year)
            {
                if(month<=6)
                {
                     date = new dateVM { flag_emp = true, from_date = plan.PlaneSchedule[0].period_start.ToShortDateString(), to_date = plan.PlaneSchedule[0].period_End.ToShortDateString() };
                    date.line = plan.Code + "_1";
                    date.code = date.line + "_" + emp.Code;
                }
                else
                {
                     date = new dateVM { flag_emp = true, from_date = plan.PlaneSchedule[1].period_start.ToShortDateString(), to_date = plan.PlaneSchedule[1].period_End.ToShortDateString() };
                    date.line = plan.Code + "_2";
                    date.code = date.line + "_" + emp.Code;
                }
            }
            if (input_date !=Convert.ToDateTime(date.from_date).Year)
            {
               date.flag_date = false;
            }
            else
            {
                date.flag_date = true;
            }
                return Json(date);
        }
    }

    public class dateVM
    {
        public string from_date { get; set; }
        public string to_date { get; set; }
        public bool flag_emp { get; set; }
        public bool flag_date { get; set; }
        public string line { get; set; }
        public string code { get; set; }
    }
}