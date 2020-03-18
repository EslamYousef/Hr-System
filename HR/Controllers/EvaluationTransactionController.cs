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
                ViewBag.employee = Context.Employee_Profile.ToList();
                ViewBag.groupcode = Context.PerformanceEvaluationGroup.ToList();
                ViewBag.plancode = Context.PerformanceEvaluationGroup.ToList();
                var model = new EvaluationTransaction();
                // model.status.statu = Models.Infra.check_status.created;
                model.AppraisalDate = DateTime.Now.Date;
                model.FromeDate = DateTime.Now.Date;
                model.ToDate = DateTime.Now.Date;
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
        public JsonResult getdate(int id)
        {
            var plan = Context.EvaluationPlan.FirstOrDefault(m => m.ID == id);
            var date_now = DateTime.Now.Date;
            var month = date_now.Month;
            var date = new dateVM();
            if (plan.EvaluationType.Periods == Periods.Yearly)
            {
                 date = new dateVM { from_date=plan.PlaneSchedule[0].period_start.ToShortDateString(), to_date=plan.PlaneSchedule[0].period_End.ToShortDateString() };
            }
            else if (plan.EvaluationType.Periods == Periods.Monyhely)
            {
                 date = new dateVM { from_date = plan.PlaneSchedule[month-1].period_start.ToShortDateString(), to_date = plan.PlaneSchedule[month - 1].period_End.ToShortDateString() };

            }
            else if (plan.EvaluationType.Periods == Periods.Quarter)
            {
                if (month <= 3)
                {
                     date = new dateVM { from_date = plan.PlaneSchedule[0].period_start.ToShortDateString(), to_date = plan.PlaneSchedule[0].period_End.ToShortDateString() };
                }
                else if (month <= 6)
                {
                     date = new dateVM { from_date = plan.PlaneSchedule[1].period_start.ToShortDateString(), to_date = plan.PlaneSchedule[1].period_End.ToShortDateString() };

                }
                else if (month <= 9)
                {
                     date = new dateVM { from_date = plan.PlaneSchedule[2].period_start.ToShortDateString(), to_date = plan.PlaneSchedule[2].period_End.ToShortDateString() };

                }
                else
                {
                     date = new dateVM { from_date = plan.PlaneSchedule[3].period_start.ToShortDateString(), to_date = plan.PlaneSchedule[3].period_End.ToShortDateString() };
                }
            }
            else if (plan.EvaluationType.Periods == Periods.Half_year)
            {
                if(month<=6)
                {
                     date = new dateVM { from_date = plan.PlaneSchedule[0].period_start.ToShortDateString(), to_date = plan.PlaneSchedule[0].period_End.ToShortDateString() };
                }
                else
                {
                     date = new dateVM { from_date = plan.PlaneSchedule[1].period_start.ToShortDateString(), to_date = plan.PlaneSchedule[1].period_End.ToShortDateString() };
                }
            }
            return Json(date);
        }
    }

    public class dateVM
    {
        public string from_date { get; set; }
        public string to_date { get; set; }
    }
}