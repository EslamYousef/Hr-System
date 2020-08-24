using HR.Models;
using HR.Models.Infra;
using HR.Models.ViewModel;
using HR.Reposatory.Evalutions.IReposatory;
using HR.Reposatory.Evalutions.reposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,Evalution,EvalutionCards")]
    public class EvaluationPlanController : BaseController
    {
        // GET: EvaluationPlan

        private readonly IEvalutonplan reposatoryEvaluationplan;
        private readonly IStructure reposatorystructure;
        private readonly IEvalutionType reposatorytype;
        private readonly IEvaluationperformanceGroup reposatoryEvaluationPerformance;
        public EvaluationPlanController()
        {
            reposatoryEvaluationPerformance = new EvaluationperformanceGroup(new ApplicationDbContext());
            reposatoryEvaluationplan = new Evalutonplan(new Models.ApplicationDbContext());
            reposatorystructure = new Structure(new Models.ApplicationDbContext());
            reposatorytype = new EvalutionType(new Models.ApplicationDbContext());
        }
        public ActionResult Index()
        {
            try
            {
                var model = reposatoryEvaluationplan.GetAll();
                if (model != null) { return View(model); }
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return View();
                }
            }
            catch (Exception)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var obj = reposatoryEvaluationplan.Find(id);
                return View(obj);
            }
            catch (Exception) { TempData["Message"] = HR.Resource.pers_2.Faild; return RedirectToAction("index"); }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult method_Delete(int id)
        {
            try
            {
                var obj = reposatoryEvaluationplan.Remove(id);
                if (obj) { TempData["Message"] = HR.Resource.organ.delete; return RedirectToAction("index"); }
                else { TempData["Message"] = HR.Resource.pers_2.Faild; return View(); }
            }
            catch (Exception) { TempData["Message"] = HR.Resource.pers_2.Faild; return RedirectToAction("index"); }

        }

        public ActionResult Create()
        {
            try
            {
                var ALLList = reposatoryEvaluationplan.GetAll();
             //   var stru = reposatorystructure.find(ChModels.Personnel).Structure_Code;
                ViewBag.type = reposatorytype.GetAll().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                var model = new HR.Models.EvaluationPlan();
                if (ALLList.Count() == 0)
                {
                    model.Code =  "1";
                }
                else
                {
                    model.Code =  (ALLList.LastOrDefault().ID + 1).ToString();
                }
                return View(model);
            }
            catch (Exception)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection form,HR.Models.EvaluationPlan model, string Command)
        {
            try
            {
                ViewBag.type = reposatorytype.GetAll().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
             
                if(ModelState.IsValid)
                {

                    var obj= reposatoryEvaluationplan.AddOne(model);

                    if (obj!=null)

                    {
                        var ID_sch = form["ID_sch"].Split(',');
                        var period_start = form["period_start"].Split(',');
                        var period_End = form["period_End"].Split(',');
                        var Process_Start = form["Process_Start"].Split(',');
                        var process_end = form["process_end"].Split(',');
                        for (var i = 0; i < ID_sch.Count(); i++)
                        {
                            if (ID_sch[i] != "")
                            {
                               
                                var elementAndComp = new PlaneSchedule
                                {
                                   Code=ID_sch[i],period_start=Convert.ToDateTime(period_start[i]),
                                    period_End = Convert.ToDateTime(period_End[i]),
                                    Process_Start = Convert.ToDateTime(Process_Start[i]),
                                    process_end = Convert.ToDateTime(process_end[i]),
                                    EvaluationPlanID=obj.ID
                                };
                                var eva_comp = reposatoryEvaluationplan.AddOneschedule(elementAndComp);
                            }
                        }
                        if (Command == "tide")
                        {
                            return RedirectToAction("Tide_Emp_With_performance", "EvaluationPlan", new { performance_ID = obj.ID });
                        }
                        TempData["Message"] = HR.Resource.pers_2.addedSuccessfully; return RedirectToAction("index");

                    }
                else

                    {
                    TempData["Message"] = HR.Resource.pers_2.Faild; return View(model);

                    }

                }
                else
                {
                    {
                        TempData["Message"] = HR.Resource.pers_2.Faild; return View(model);

                    }

                }
            }
            catch (Exception)
            {
                return View(model);
            }
        }



        public ActionResult Edit(int id)
        {
            try
            {
                var obj = reposatoryEvaluationplan.Find(id);
                ViewBag.type = reposatorytype.GetAll().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });

                return View(obj);
            }
            catch(Exception)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Edit(FormCollection form, HR.Models.EvaluationPlan model, string Command)
        {
            try
            {
                ViewBag.type = reposatorytype.GetAll().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                if (ModelState.IsValid)
                {

                    var obj = reposatoryEvaluationplan.Editone(model);

                    if (obj!=null)

                    {
                        var flag= reposatoryEvaluationplan.reomveplanescedule(model.ID);
                        if (flag)
                        {
                            var ID_sch = form["ID_sch"].Split(',');
                            var period_start = form["period_start"].Split(',');
                            var period_End = form["period_End"].Split(',');
                            var Process_Start = form["Process_Start"].Split(',');
                            var process_end = form["process_end"].Split(',');
                            for (var i = 0; i < ID_sch.Count(); i++)
                            {
                                if (ID_sch[i] != "")
                                {

                                    var elementAndComp = new PlaneSchedule
                                    {
                                        Code = ID_sch[i],
                                        period_start = Convert.ToDateTime(period_start[i]),
                                        period_End = Convert.ToDateTime(period_End[i]),
                                        Process_Start = Convert.ToDateTime(Process_Start[i]),
                                        process_end = Convert.ToDateTime(process_end[i]),
                                        EvaluationPlanID = model.ID
                                    };
                                    var eva_comp = reposatoryEvaluationplan.AddOneschedule(elementAndComp);
                                }
                            }
                            if (Command == "tide")
                            {
                                return RedirectToAction("editTide_Emp_With_performance", "EvaluationPlan", new { ID = obj.ID });
                            }
                            TempData["Message"] = HR.Resource.pers_2.addedSuccessfully; return RedirectToAction("index");
                        }
                        else
                        {
                            TempData["Message"] = HR.Resource.pers_2.Faild; return View(model);
                        }
                    }
                    else

                    {
                        TempData["Message"] = HR.Resource.pers_2.Faild; return View(model);

                    }
                }

                else

                {
                    TempData["Message"] = HR.Resource.pers_2.Faild; return View(model);

                }


            }
            catch (Exception)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return RedirectToAction("index");
            }
        }

        public ActionResult FindType(int id,int year,int code)
        {
            var type = reposatorytype.Find2(id);
            var plan_schedule = new List<evaluationplanVM>();
           
            if (type.Periods==Periods.Monyhely)
            {
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_1", period_start=("01/01/"+(year).ToString()),period_End= ("01/31/"+(year).ToString())});
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_2", period_start = ("02/01/" + (year).ToString()), period_End = ("02/28/" + (year).ToString()) });
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_3", period_start = ("03/01/" + (year).ToString()), period_End = ("03/31/" + (year).ToString()) });
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_4", period_start = ("04/01/" + (year).ToString()), period_End = ("04/30/" + (year).ToString()) });
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_5", period_start = ("05/01/" + (year).ToString()), period_End = ("05/31/" + (year).ToString()) });
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_6", period_start = ("06/01/" + (year).ToString()), period_End = ("06/30/" + (year).ToString()) });
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_7", period_start = ("07/01/" + (year).ToString()), period_End = ("07/31/" + (year).ToString()) });
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_8", period_start = ("08/01/" + (year).ToString()), period_End = ("08/31/" + (year).ToString()) });
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_9", period_start = ("09/01/" + (year).ToString()), period_End = ("09/30/" + (year).ToString()) });
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_10", period_start = ("10/01/" + (year).ToString()), period_End = ("10/31/" + (year).ToString()) });
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_11", period_start = ("11/01/" + (year).ToString()), period_End = ("11/30/" + (year).ToString()) });
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_12", period_start = ("12/01/" + (year).ToString()), period_End = ("12/31/" + (year).ToString()) });


            }
            else if (type.Periods == Periods.Quarter)
            {
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_1", period_start = ("01/01/" + (year).ToString()), period_End = ("03/31/" + (year).ToString()) });
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_2", period_start = ("04/01/" + (year).ToString()), period_End = ("06/30/" + (year).ToString()) });
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_3", period_start = ("07/01/" + (year).ToString()), period_End = ("09/30/" + (year).ToString()) });
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_4", period_start = ("10/01/" + (year).ToString()), period_End = ("12/31/" + (year).ToString()) });


            }
            else if (type.Periods == Periods.Half_year)
            {
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_1", period_start = ("01/01/" + (year).ToString()), period_End = ("6/30/" + (year).ToString()) });
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_2", period_start = ("07/01/" + (year).ToString()), period_End = ("12/31/" + (year).ToString()) });

            }
            else
            {
                plan_schedule.Add(new HR.Models.evaluationplanVM { code = code + "_1", period_start = ("01/01/" + (year).ToString()), period_End = ("12/31/" + (year).ToString()) });

            }
            return Json(plan_schedule);
        }
        public ActionResult FindType2(int id)
        {
            var type = reposatoryEvaluationplan.findplanescedule(id);
            var plan_schedule = new List<evaluationplanVM>();
            foreach (var item in type)
            {
                var startperiod = item.period_start.ToShortDateString();
                var endperiod = item.period_End.ToShortDateString();
                plan_schedule.Add(new evaluationplanVM { code = item.Code, period_start = startperiod, period_End = item.period_End.ToShortDateString(),process_start=item.Process_Start.ToShortDateString(),process_end=item.process_end.ToShortDateString() });
            }

            return Json(plan_schedule);
        }
        public ActionResult Tide_Emp_With_performance(int performance_ID)
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
                ViewBag.Object = new SelectList(items, "Value", "Text");
                ViewBag.performance_ID = performance_ID;
                var pe = new per_em();
                pe.PER_id = performance_ID;
                return View(pe);
            }
            catch (Exception)
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult Tide_Emp_With_performance(FormCollection form, per_em model)
        {
            try
            {
                List<SelectListItem> items = new List<SelectListItem>();
                items.Insert(0, (new SelectListItem
                {
                    Text = "employee",
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
                ViewBag.Object = new SelectList(items, "Value", "Text");

                var context = new ApplicationDbContext();
                var ID_emp = form["ID_emp"].Split(',');
                var per = reposatoryEvaluationplan.Find(model.PER_id);

                foreach (var item in ID_emp)
                {
                    if (item != "")
                    {
                        var ID = int.Parse(item);
                        var per_em = new per_emp();
                        var emp = context.Employee_Profile.FirstOrDefault(m => m.ID == ID);
                        per_em.PerformanceEvaluationGroupID = per.ID;
                        per_em.Employee_ProfileID = emp.ID;
                        var record = context.per_emp.Add(per_em);
                        context.SaveChanges();
                    }

                }
                return RedirectToAction("Edit", new { id = per.ID });
            }
            catch (Exception)
            {
                return View();
            }
        }




        public ActionResult editTide_Emp_With_performance(int ID)
        {
            try
            {
                var context = new ApplicationDbContext();
                List<SelectListItem> items = new List<SelectListItem>();
                items.Insert(0, (new SelectListItem
                {
                    Text = "employee",
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
                ViewBag.Object = new SelectList(items, "Value", "Text");

                var model = context.per_emp.Where(m => m.PerformanceEvaluationGroupID == ID).ToList();

                var pe = new per_em();
                pe.PER_id = ID;
                var em = new List<Employee_Profile>();
                foreach (var item in model)
                {
                    var emp = context.Employee_Profile.FirstOrDefault(m => m.ID == item.Employee_ProfileID);
                    em.Add(emp);
                }
                pe.emp = em;
                return View(pe);
            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult editTide_Emp_With_performance(FormCollection form, per_em model)
        {
            try
            {
                var context = new ApplicationDbContext();
                List<SelectListItem> items = new List<SelectListItem>();
                items.Insert(0, (new SelectListItem
                {
                    Text = "employee",
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
                ViewBag.Object = new SelectList(items, "Value", "Text");

                var per = reposatoryEvaluationplan.Find(model.PER_id);
                var per_em1 = context.per_emp.Where(m => m.PerformanceEvaluationGroupID == model.PER_id).ToList();
                context.per_emp.RemoveRange(per_em1);
                context.SaveChanges();

                //var model = context.per_emp.Where(m => m.PerformanceEvaluationGroupID == ID).ToList();


                var ID_emp = form["ID_emp"].Split(',');
                var per2 = reposatoryEvaluationPerformance.Find(model.PER_id);
                foreach (var item in ID_emp)
                {
                    if (item != "")
                    {
                        var ID = int.Parse(item);
                        var per_em = new per_emp();
                        var emp = context.Employee_Profile.FirstOrDefault(m => m.ID == ID);
                        per_em.PerformanceEvaluationGroupID = per.ID;
                        per_em.Employee_ProfileID = emp.ID;
                        var record = context.per_emp.Add(per_em);
                        context.SaveChanges();

                    }
                }

                return RedirectToAction("Edit", new { id = per.ID });
            }
            catch (Exception)
            {
                return View();
            }
        }
        public ActionResult showtide(int ID)
        {
            var context = new ApplicationDbContext();
            var model = context.per_emp.Where(m => m.PerformanceEvaluationGroupID == ID).ToList();
            var pe = new per_em();
            pe.PER_id = ID;
            var em = new List<Employee_Profile>();
            foreach (var item in model)
            {
                var emp = context.Employee_Profile.FirstOrDefault(m => m.ID == item.Employee_ProfileID);
                em.Add(emp);
            }
            pe.emp = em;
            return View(pe);
        }
    }
}