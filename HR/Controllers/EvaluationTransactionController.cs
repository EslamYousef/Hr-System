using HR.Models;
using HR.Models.Infra;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    
    public class EvaluationTransactionController : BaseController
    {
        ApplicationDbContext Context = new ApplicationDbContext();
        // GET: EvaluationTransaction
        [Authorize(Roles = "Admin,Evalution,EvalutionTransaction,EvalutionProcess")]
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
        [Authorize(Roles = "Admin,Evalution,EvalutionTransaction")]

        public ActionResult Create()
        {
            try
            {
                ViewBag.employee = Context.Employee_Profile.Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.groupcode = Context.PerformanceEvaluationGroup.Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.plancode = Context.EvaluationPlan.ToList().Select(m=>new { Code=m.Code+"->"+m.Name,ID=m.ID});
                ViewBag.obj=Context.EvaluationObjectives.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.skk = Context.Skill.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                var model = new EvaluationTransaction();
                // model.status.statu = Models.Infra.check_status.created;
                model.AppraisalDate = Convert.ToDateTime("01/01/1900");/*  DateTime.Now.Date;*/
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
        [HttpPost]
        public ActionResult Create(FormCollection form,EvaluationTransaction model)

        {
            try
            {
                ViewBag.employee = Context.Employee_Profile.Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.groupcode = Context.PerformanceEvaluationGroup.Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.plancode = Context.EvaluationPlan.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.obj = Context.EvaluationObjectives.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.skk = Context.Skill.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                var i1= form["num"].Split(',');
               var i2 = form["D"].Split(',');
              var i3 = form["DH"].Split(',');
              var i4 = form["DM"].Split(',');
                var i5 = form["DAV"].Split(',');
               var i6= form["DF"].Split(',');
               var i7 = form["tot"].Split(',');
                if(i1.Count()>1)
                model.num = int.Parse(form["num"].Split(',')[1]);
                else
                    model.num = 0;
                if (i2.Count() > 1)
                    model.def = double.Parse(form["D"].Split(',')[1]);
                else
                    model.def = 0;
                if (i3.Count() > 1)
                    model.H_degree = double.Parse(form["DH"].Split(',')[1]);
                else
                    model.H_degree = 0;
                if (i4.Count() > 1)
                    model.M_degree = double.Parse(form["DM"].Split(',')[1]);
                else
                    model.M_degree = 0;
                if (i5.Count() > 1)
                    model.average = double.Parse(form["DAV"].Split(',')[1]);
                else
                    model.average = 0;
                if (i6.Count() > 1)
                    model.final = double.Parse(form["DF"].Split(',')[1]);
                else
                    model.final = 0;
                if (i7.Count() > 1)
                    model.total = form["tot"].Split(',')[1];
                else
                    model.total = "";


                ///status////
                // record.check_status = check_status.created;
                //record.sss = record.check_status.GetTypeCode().ToString();
                var Date = Convert.ToDateTime("1/1/1900");
                var s = new status { statu = check_status.created, Type = Models.Infra.Type.Budget, approved_bydate = Date, cancaled_bydate = Date, created_bydate = DateTime.Now.Date, Rejected_bydate = Date, return_to_reviewdate = Date };
                s.created_by = User.Identity.GetUserName();
                var st = Context.status.Add(s);
                Context.SaveChanges();
                model.statusID = st.ID;
                model.check_status = check_status.created;
                ////////////
                var tarnsaction_saved = Context.EvaluationTransaction.Add(model);
                Context.SaveChanges();
                /////elements//////
                var id_element = form["ID_ELEM"].Split(',');
                var H_element = form["degree_head"].Split(',');
                var M_element = form["degree_ector"].Split(',');
                var A_element = form["degree_average"].Split(',');
                var F_element = form["degreeo_final"].Split(',');
                var D_element = form["descID"].Split(',');
                for(var i =0;i< id_element.Count();i++)
                {
                    if (id_element[i] != "")
                    {
                        var ID = int.Parse(id_element[i]);
                       
                        
                        var m = new Evalu_Element_Tran
                        {
                            EvaluationElementsID = ID,
                            EvaluationTransactionID = tarnsaction_saved.ID,
                            H_degree = double.Parse(H_element[i]),
                            M_degree = double.Parse(M_element[i]),
                            average = double.Parse(A_element[i]),
                            final = double.Parse(F_element[i]),
                        };
                        if ((D_element[i]) != "")
                            m.EvaluationGradeID = int.Parse(D_element[i]);
                        Context.Evalu_Element_Tran.Add(m);
                        Context.SaveChanges();
                    }
                }
                //////////Q&A//////////
                var id_Q = form["ID_Q"].Split(',');
                var ans = form["ans"].Split(',');
              
                for (var i = 0; i < id_Q.Count(); i++)
                {
                    if (id_Q[i]!="")
                    {
                        var ID = int.Parse(id_Q[i]);

                        var m = new A_Q
                        {
                            EvaluationQuestionsandanswersID = ID,
                            EvaluationTransactionID = tarnsaction_saved.ID,
                            actual_answer = ans[i]
                        };
                        Context.A_Q.Add(m);
                        Context.SaveChanges();
                    }
                }
                //////////objec////////
                var ID_O = form["ID_O"].Split(',');
                var Date_O = form["Date_O"].Split(',');
                var Head_O = form["Head_O"].Split(',');
                var Mnamger_O = form["Mnamger_O"].Split(',');
                var average_O = form["average_O"].Split(',');
                var final_O = form["final_O"].Split(',');
                for (var i = 0; i < ID_O.Count(); i++)
                {
                    if (ID_O[i] != "")
                    {
                        var ID = int.Parse(ID_O[i]);
                        var m = new obje_eval_tran
                        {
                            EvaluationObjectivesID = ID,
                            EvaluationTransactionID = tarnsaction_saved.ID,
                            H_degree = double.Parse(Head_O[i]),
                            M_degree = double.Parse(Mnamger_O[i]),
                            average = double.Parse(average_O[i]),
                            final = double.Parse(final_O[i]),
                            Date = Convert.ToDateTime(Date_O[i])


                        };
                        Context.obje_eval_tran.Add(m);
                        Context.SaveChanges();
                    }
                }
                ////////skill//////
                var ID_S = form["ID_S"].Split(',');
                var job_rate = form["job_rate"].Split(',');
                var employee_rate = form["employee_rate"].Split(',');
                var GAP = form["GAP"].Split(',');
               
                for (var i = 0; i < ID_S.Count(); i++)
                {
                    if (ID_S[i] != "")
                    {
                        var ID = int.Parse(ID_S[i]);
                        var m = new skill_eval
                        {

                            EvaluationTransactionID = tarnsaction_saved.ID,
                            SkillID = ID,
                            em_rate=double.Parse(employee_rate[i]),
                            J_rate=double.Parse(job_rate[i]),
                            GAP=GAP[i]

                        };
                        Context.skill_eval.Add(m);
                        Context.SaveChanges();
                    }
                }
                //=================================check for alert==================================
                var get_result_check = HR.Controllers.check.check_alert("evaluation transaction", HR.Models.user.Action.Create,HR.Models.user.type_field.form);
                if (get_result_check != null)
                {
                    var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.GetUserId(), send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
                    if (get_result_check.until != null)
                    {
                        if (get_result_check.until.Value.Year != 0001)
                        {
                            inbox.until = get_result_check.until;
                        }
                    }
                    Context.Alert_inbox.Add(inbox);
                    Context.SaveChanges();
                }
                //==================================================================================
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Admin,Evalution,EvalutionTransaction")]

        public ActionResult Edit(int id)
        {
            try
            {
                var model = Context.EvaluationTransaction.FirstOrDefault(m => m.ID == id);
                ViewBag.employee = Context.Employee_Profile.Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.groupcode = Context.PerformanceEvaluationGroup.Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.plancode = Context.EvaluationPlan.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.obj = Context.EvaluationObjectives.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.skk = Context.Skill.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form, EvaluationTransaction model)
        {

            try
            {
                var record = Context.EvaluationTransaction.FirstOrDefault(m => m.ID == model.ID);
                ViewBag.employee = Context.Employee_Profile.Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.groupcode = Context.PerformanceEvaluationGroup.Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.plancode = Context.EvaluationPlan.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.obj = Context.EvaluationObjectives.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ViewBag.skk = Context.Skill.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                ///////////////////////
                record.AppraisalDate = model.AppraisalDate;
                record.Code = model.Code;
                record.Employee_ProfileID = model.Employee_ProfileID;
                record.EvaluationPlanID = model.EvaluationPlanID;
                record.FromeDate = model.FromeDate;
                record.ToDate = model.ToDate;
                //record.def = model.def;
                //record.H_degree = model.H_degree;
                //record.M_degree = model.M_degree;
                record.PerformanceEvaluationGroupID = model.PerformanceEvaluationGroupID;
                //record.average = model.average;
                //record.final = model.final;
                //record.total = model.total;
                record.planLine = model.planLine;
                // record.num = model.num;
                ///////////////////////remove//////
                var elem = record.Evalu_Element_Tran;
                var qu = record.A_Q;
                var ob = record.obje_eval_tran;
                var Skill = record.skill_eval;
                if (elem != null)
                    Context.Evalu_Element_Tran.RemoveRange(elem);
                if (qu != null)
                    Context.A_Q.RemoveRange(qu);
                if (ob != null)
                    Context.obje_eval_tran.RemoveRange(ob);
                if (Skill != null)
                    Context.skill_eval.RemoveRange(Skill);
                Context.SaveChanges();
                ///////////////////////
                var i1 = form["num"].Split(',');
                var i2 = form["D"].Split(',');
                var i3 = form["DH"].Split(',');
                var i4 = form["DM"].Split(',');
                var i5 = form["DAV"].Split(',');
                var i6 = form["DF"].Split(',');
                var i7 = form["tot"].Split(',');
                if (i1.Count() > 1)
                    record.num = int.Parse(form["num"].Split(',')[1]);
                else
                    record.num = 0;
                if (i2.Count() > 1)
                    record.def = double.Parse(form["D"].Split(',')[1]);
                else
                    record.def = 0;
                if (i3.Count() > 1)
                    record.H_degree = double.Parse(form["DH"].Split(',')[1]);
                else
                    record.H_degree = 0;
                if (i4.Count() > 1)
                    record.M_degree = double.Parse(form["DM"].Split(',')[1]);
                else
                    record.M_degree = 0;
                if (i5.Count() > 1)
                    record.average = double.Parse(form["DAV"].Split(',')[1]);
                else
                    record.average = 0;
                if (i6.Count() > 1)
                    record.final = double.Parse(form["DF"].Split(',')[1]);
                else
                    record.final = 0;
                if (i7.Count() > 1)
                    record.total = form["tot"].Split(',')[1];
                else
                    record.total = "";


                Context.SaveChanges();
                /////elements//////
                var id_element = form["ID_ELEM"].Split(',');
                var H_element = form["degree_head"].Split(',');
                var M_element = form["degree_ector"].Split(',');
                var A_element = form["degree_average"].Split(',');
                var F_element = form["degreeo_final"].Split(',');
                var D_element = form["descID"].Split(',');
                for (var i = 0; i < id_element.Count(); i++)
                {
                    if (id_element[i] != "")
                    {
                        var ID = int.Parse(id_element[i]);


                        var m = new Evalu_Element_Tran
                        {
                            EvaluationElementsID = ID,
                            EvaluationTransactionID = record.ID,
                            H_degree = double.Parse(H_element[i]),
                            M_degree = double.Parse(M_element[i]),
                            average = double.Parse(A_element[i]),
                            final = double.Parse(F_element[i]),
                        };
                        if ((D_element[i]) != "")
                            m.EvaluationGradeID = int.Parse(D_element[i]);
                        Context.Evalu_Element_Tran.Add(m);
                        Context.SaveChanges();
                    }
                }
                //////////Q&A//////////
                var id_Q = form["ID_Q"].Split(',');
                var ans = form["ans"].Split(',');

                for (var i = 0; i < id_Q.Count(); i++)
                {
                    if (id_Q[i] != "")
                    {
                        var ID = int.Parse(id_Q[i]);

                        var m = new A_Q
                        {
                            EvaluationQuestionsandanswersID = ID,
                            EvaluationTransactionID = record.ID,
                            actual_answer = ans[i]
                        };
                        Context.A_Q.Add(m);
                        Context.SaveChanges();
                    }
                }
                //////////objec////////
                var ID_O = form["ID_O"].Split(',');
                var Date_O = form["Date_O"].Split(',');
                var Head_O = form["Head_O"].Split(',');
                var Mnamger_O = form["Mnamger_O"].Split(',');
                var average_O = form["average_O"].Split(',');
                var final_O = form["final_O"].Split(',');
                for (var i = 0; i < ID_O.Count(); i++)
                {
                    if (ID_O[i] != "")
                    {
                        var ID = int.Parse(ID_O[i]);
                        var m = new obje_eval_tran
                        {
                            EvaluationObjectivesID = ID,
                            EvaluationTransactionID = record.ID,
                            H_degree = double.Parse(Head_O[i]),
                            M_degree = double.Parse(Mnamger_O[i]),
                            average = double.Parse(average_O[i]),
                            final = double.Parse(final_O[i]),
                            Date = Convert.ToDateTime(Date_O[i])


                        };
                        Context.obje_eval_tran.Add(m);
                        Context.SaveChanges();
                    }

                }
                ////////skill//////
                var ID_S = form["ID_S"].Split(',');
                var job_rate = form["job_rate"].Split(',');
                var employee_rate = form["employee_rate"].Split(',');
                var GAP = form["GAP"].Split(',');

                for (var i = 0; i < ID_S.Count(); i++)
                {
                    if (ID_S[i] != "")
                    {
                        var ID = int.Parse(ID_S[i]);
                        var m = new skill_eval
                        {

                            EvaluationTransactionID = record.ID,
                            SkillID = ID,
                            em_rate = double.Parse(employee_rate[i]),
                            J_rate = double.Parse(job_rate[i]),
                            GAP = GAP[i]

                        };
                        Context.skill_eval.Add(m);
                        Context.SaveChanges();
                    }
                }
                //=================================check for alert==================================
                var get_result_check = HR.Controllers.check.check_alert("evaluation transaction", HR.Models.user.Action.edit, HR.Models.user.type_field.form);
                if (get_result_check != null)
                {
                    var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.GetUserId(), send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
                    if (get_result_check.until != null)
                    {
                        if (get_result_check.until.Value.Year != 0001)
                        {
                            inbox.until = get_result_check.until;
                        }
                    }
                    Context.Alert_inbox.Add(inbox);
                    Context.SaveChanges();
                }
                //==================================================================================

                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Admin,Evalution,EvalutionTransaction")]

        public ActionResult delete(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = Context.EvaluationTransaction.FirstOrDefault(m => m.ID == ID);
                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult method_delete(string id)
        {
            var ID = int.Parse(id);
            var model = Context.EvaluationTransaction.FirstOrDefault(m => m.ID == ID);
          
            try
            {
                var elem = model.Evalu_Element_Tran;
                var qu = model.A_Q;
                var ob = model.obje_eval_tran;
                var st = model.status;
                var Skill = model.skill_eval;
                var st1 = Context.status.FirstOrDefault(m => m.ID == model.statusID);
                if (elem != null)
                    Context.Evalu_Element_Tran.RemoveRange(elem);
                if (qu != null)
                    Context.A_Q.RemoveRange(qu);
                if (ob != null)
                    Context.obje_eval_tran.RemoveRange(ob);
                if (Skill != null)
                    Context.skill_eval.RemoveRange(Skill);
                Context.SaveChanges();
                Context.EvaluationTransaction.Remove(model);
                Context.SaveChanges();
                Context.status.Remove(st1);
                Context.SaveChanges();
                User.Identity.GetUserId();
                //=================================check for alert==================================
                var get_result_check = HR.Controllers.check.check_alert("evaluation transaction", HR.Models.user.Action.delete, HR.Models.user.type_field.form);
                if (get_result_check != null)
                {
                    var inbox = new Models.user.Alert_inbox { send_from_user_id=User.Identity.GetUserId(),send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
                    if (get_result_check.until != null)
                    {
                        if (get_result_check.until.Value.Year != 0001)
                        {
                            inbox.until = get_result_check.until;
                        }
                    }
                    Context.Alert_inbox.Add(inbox);
                    Context.SaveChanges();
                }
                //==================================================================================

                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.organ.youcannotdeletethisRow;
                return View(model);
            }
            catch (Exception e)
            {
                TempData["Message"] = HR.Resource.organ.youcannotdeletethisRow;
                return View(model);
            }
        }

        [Authorize(Roles = "Admin,Evalution,EvalutionProcess")]
        public ActionResult status(int id)
        {
            try
            {
                var model = Context.EvaluationTransaction.FirstOrDefault(m => m.ID == id);
                var st = Context.status.FirstOrDefault(m=>m.ID==model.statusID);
                ViewBag.id = id;
                return View(st);
            }
            catch (Exception e)
            {
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public ActionResult status(status model,int id)
        {
            ViewBag.id = id;
            var record = Context.EvaluationTransaction.FirstOrDefault(m => m.ID == id);
            var sta = Context.status.FirstOrDefault(m => m.ID == record.statusID);
            try
            {
               
                if (model.statu == check_status.Approved)
                {
                    sta.approved_by = User.Identity.GetUserName();
                    sta.approved_bydate = model.approved_bydate;
                    sta.statu = check_status.Approved;
                    record.check_status = check_status.Approved;

                    Context.SaveChanges();

                }

                else if (model.statu == check_status.Rejected)
                {
                    sta.Rejected_by = User.Identity.GetUserName();
                    sta.Rejected_bydate = model.Rejected_bydate;
                    sta.statu = check_status.Rejected;
                    record.check_status = check_status.Rejected;

                    Context.SaveChanges();
                }
                else if (model.statu == check_status.Return_To_Review)
                {
                    sta.return_to_reviewby = User.Identity.GetUserName();
                    sta.return_to_reviewdate = model.return_to_reviewdate;
                    sta.statu = check_status.Return_To_Review;
                    record.check_status = check_status.Return_To_Review;
                    Context.SaveChanges();
                }
                //=================================check for alert==================================
                var get_result_check = HR.Controllers.check.check_alert("evaluation process", HR.Models.user.Action.Create, HR.Models.user.type_field.form);
                if (get_result_check != null)
                {
                    var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.Name, send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject + model.statu, Subject = get_result_check.Message };
                    if (get_result_check.until != null)
                    {
                        if (get_result_check.until.Value.Year != 0001)
                        {
                            inbox.until = get_result_check.until;
                        }
                    }
                    ApplicationDbContext dbcontext = new ApplicationDbContext();
                    dbcontext.Alert_inbox.Add(inbox);
                    dbcontext.SaveChanges();
                }
                //===================================================================================
                return RedirectToAction("index");
            }
            catch(Exception)
            {
                return View(sta);
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
        public JsonResult Getobj(int id)
        {
            try
            {
               
                Context.Configuration.ProxyCreationEnabled = false;
                var record = Context.EvaluationObjectives.FirstOrDefault(m => m.ID == id);
                if (record != null)
                    return Json(record);
                else
                    return Json(null);
            }
            catch(Exception)
            {
                return Json(null);
            }
        }
        public JsonResult Getone(DateTime from, DateTime to, List<string> status)
        {
            try
            {
                Context.Configuration.ProxyCreationEnabled = false;
                //////
                /////
                var nn = new List<check_status>();
                foreach (var item in status)
                {
                    if (item != "all")
                    {
                        nn.Add((check_status)Enum.Parse(typeof(check_status), item));
                    }
                }
                /////
                ////
              
                var req = new List<EvaluationTransaction>();    
                List<EvaluationTransaction> re1 = new List<EvaluationTransaction>();
                var final_model = new List<json_VM>();
                if (status[0] == "all")
                {
                    req = Context.EvaluationTransaction.Where(m => DateTime.Compare(m.AppraisalDate, from) >= 0 && DateTime.Compare(m.AppraisalDate, to) <= 0).ToList();
                    re1.AddRange(req);

                }
                else if (status[0] != "all")
                {
                    req =Context.EvaluationTransaction.Where(m => DateTime.Compare(m.AppraisalDate, from) >= 0 && DateTime.Compare(m.AppraisalDate, to) <= 0).ToList();
                    foreach (var item in nn)
                    {
                        re1.AddRange(req.Where(m => m.check_status == item).ToList());
                    }
                }
                foreach (var item in re1)
                {
                    var t = item.AppraisalDate.ToShortDateString();
                    var g= Context.PerformanceEvaluationGroup.FirstOrDefault(m => m.ID == item.PerformanceEvaluationGroupID).Name;
                   var e= Context.Employee_Profile.FirstOrDefault(m => m.ID == item.Employee_ProfileID).Name;
                    final_model.Add(new json_VM { s=item.check_status.ToString(), ID=item.ID,Code=item.Code,plan=item.planLine,status=item.check_status.GetHashCode(),app_date=t,empID=item.Employee_ProfileID.ToString(),group=g,Emp=e});
               


                }
                return Json(final_model);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult Getalll()
        {
            try
            {
             
                Context.Configuration.ProxyCreationEnabled = false;
                var final_model = new List<json_VM>();
                var model = Context.EvaluationTransaction.ToList();
                foreach (var item in model)
                {
                    var t = item.AppraisalDate.ToShortDateString();
                    var g = Context.PerformanceEvaluationGroup.FirstOrDefault(m => m.ID == item.PerformanceEvaluationGroupID).Name;
                    var e = Context.Employee_Profile.FirstOrDefault(m => m.ID == item.Employee_ProfileID).Name;
                    final_model.Add(new json_VM { s = item.check_status.ToString(), ID = item.ID, Code = item.Code, plan = item.planLine, status = item.check_status.GetHashCode(), app_date = t, empID = item.Employee_ProfileID.ToString(), group = g, Emp = e });



                }
                return Json(final_model);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
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
        public JsonResult Getvalueforedit(int id)
        {
            Context.Configuration.ProxyCreationEnabled = false;
            var value = Context.Evalu_Element_Tran.Where(m => m.EvaluationTransactionID == id).ToList();
            var model = new List<valVM>();
            foreach(var ite  in value)
            {
                var de = Context.EvaluationGrade.FirstOrDefault(m => m.ID == ite.EvaluationGradeID);
             
                model.Add(new Controllers.valVM { dsc_id=de.ID,dsc_n=de.Name,h=ite.H_degree,m=ite.M_degree,a=ite.average,f=ite.final});
            }
            return Json(model);
        }
        public JsonResult Getans(int id)
        {
            Context.Configuration.ProxyCreationEnabled = false;
            var value = Context.A_Q.Where(m => m.EvaluationTransactionID == id);
            return Json(value);
        }
        public JsonResult getobjforedit(int id)
        {
            Context.Configuration.ProxyCreationEnabled = false;
         
            var value = Context.obje_eval_tran.Where(m => m.EvaluationTransactionID == id).ToList();
            var mo = new List<obj_vm>();
            foreach(var item in value)
            {
                var ob = Context.EvaluationObjectives.FirstOrDefault(m => m.ID == item.EvaluationObjectivesID);
                mo.Add(new obj_vm { code=ob.Code,id = ob.ID, name = ob.Name, H = item.H_degree, M = item.M_degree, V = item.average, D = item.Date.ToShortDateString() });

            }
           

            return Json(mo);
        }
        public JsonResult oldeval(int id)
        {
            try
            {
                Context.Configuration.ProxyCreationEnabled = false;
                var eval = Context.EvaluationTransaction.Where(m => m.Employee_ProfileID == id).ToList();
                var list = new List<old_eval>();
                foreach(var item in eval)
                {
                    var plan = Context.EvaluationPlan.FirstOrDefault(m => m.ID == item.EvaluationPlanID);
                    list.Add(new old_eval
                    {
                        apprisaldate = item.AppraisalDate.ToShortDateString(),
                        number = item.Code,
                        plancode = plan.Code,
                        plansed = plan.Description,
                        from = item.FromeDate.ToShortDateString(),
                        to = item.ToDate.ToShortDateString(),
                        degree = item.final,
                        final = item.total
                    });
                }
                return Json(list);
            }
            catch(Exception)
            {
                return Json(null);
            }
        }

        public JsonResult Getskill(int id)
        {
            try
            {
                Context.Configuration.ProxyCreationEnabled = false;
                var model = Context.Skill.FirstOrDefault(m=>m.ID==id);
                return Json(model);
            }
            catch (Exception)
            {
                return Json(null);
            }
        }

        public JsonResult get_skill_edit(int id)
        {
            try
            {
                Context.Configuration.ProxyCreationEnabled = false;
                var skill = Context.skill_eval.Where(m => m.EvaluationTransactionID == id).ToList();
                var list = new List<skillvm>();
                foreach(var item in skill)
                {
                   var sk = Context.Skill.FirstOrDefault(m => m.ID == item.SkillID);
                    list.Add(new skillvm {ID=sk.ID,code=sk.Code,name=sk.Name,E=item.em_rate,GAP=item.GAP,J=item.J_rate });
                }
                return Json(list);
            }
            catch(Exception)
            {
                return Json(null);
            }
        }
        public JsonResult getfooter(int id)
        {
            try
            {
                Context.Configuration.ProxyCreationEnabled = false;
                var eval = Context.EvaluationTransaction.FirstOrDefault(m => m.ID == id);
                return Json(eval);
            }
            catch(Exception)
            {
                return Json(null);
            }
        }
    }
    public class skillvm
    {
        public int ID { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public double J { get; set; }
        public double E { get; set; }
        public string GAP { get; set; }
    }
    public class old_eval
    {
        public string number { get; set; }
        public string apprisaldate { get; set; }
        public string plancode { get; set; }
        public string plansed { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public double degree { get; set; }
        public string final { get; set; }

    }
    public class obj_vm
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public double H { get; set; }
        public double M { get; set; }
        public double V { get; set; }
        public string D { get; set; }
       
    }
    public class json_VM
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string empID { get; set; }
        public string Emp { get; set; }
        public string app_date { get; set; }
        public string plan { get; set; }
        public string group { get; set; }
        public int status { get; set; }  
        public string s { get; set; }
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
    public class valVM
    {
      
      
       
        public double h { get; set; }
        public double m { get; set; }
        public double a { get; set; }
        public double f { get; set; }
        public string dsc_n { get; set; }
        public int dsc_id { get; set; }
    }
}