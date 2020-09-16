using HR.Models;
using HR.Models.Infra;
using HR.Models.Training.setup;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.training.setup
{
    [Authorize(Roles = "Admin,talent,talentSetup,Training ploicy")]
    public class TrainingPolicyController : BaseController
    {
        // GET: TrainingPolicy
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = dbcontext.TrainingPolicy.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new TrainingPolicy();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Talent_Development).Structure_Code;
            var model = dbcontext.TrainingPolicy.ToList();
            modell.Year = (short)DateTime.Now.Year;
            ViewBag.training_type = dbcontext.TrainingType_Header.ToList().Select(m => new { Code = m.TrainingType_Code + "-[" + m.TrainingType_Desc + ']', ID = m.ID });
           

            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(TrainingPolicy model,FormCollection form)
        {
            try
            {
                ViewBag.training_type = dbcontext.TrainingType_Header.ToList().Select(m => new { Code = m.TrainingType_Code + "-[" + m.TrainingType_Desc + ']', ID = m.ID });
                var a1 = form["check1"].Split(',');
                var a2 = form["check2"].Split(',');
                var a3 = form["check3"].Split(',');
                var a4 = form["check4"].Split(',');
                var a5 = form["check5"].Split(',');
                var a6 = form["check6"].Split(',');
                var a7 = form["check7"].Split(',');
                var a8 = form["check8"].Split(',');
                var a9 = form["check9"].Split(',');
                if (a1.Length == 1)
                {
                    model.Check_Employee_Resident_City = false;

                }
                else
                {
                    model.Check_Employee_Resident_City = true;

                }
                if (a2.Length == 1)
                {
                    model.Max_No_Attended_Cources = false;
                    model.Attended_Cources = 0;
                }
                else
                {
                    model.Max_No_Attended_Cources = true;


                }
                if (a3.Length == 1)
                {
                    model.Min_Employee_Working_Months = false;
                    model.Employee_Working_Months = 0;
                }
                else
                {
                    model.Min_Employee_Working_Months = true;

                }
                if (a4.Length == 1)
                {
                    model.Min_Appraisal_Grade = false;
                    model.Appraisal_Grade = 0;
                }
                else
                {
                    model.Min_Appraisal_Grade = true;

                }
                if (a5.Length == 1)
                {
                    model.Min_Years_Of_Experience = false;
                    model.Years_Of_Experience = 0;
                }
                else
                {
                    model.Min_Years_Of_Experience = true;

                }
                if (a6.Length == 1)
                {
                    model.Review_HRGM_Required = false;

                }
                else
                {
                    model.Review_HRGM_Required = true;
                }
                if (a7.Length == 1)
                {
                    model.Review_AdminGM_Required = false;
                }
                else
                {
                    model.Review_AdminGM_Required = true;

                }
                if (a8.Length == 1)
                {
                    model.Approve_HRGM_Required = false;
                }
                else
                {
                    model.Approve_HRGM_Required = true;
                }
                if (a9.Length == 1)
                {
                    model.Approve_AdminGM_Required = false;
                }
                else
                {
                    model.Approve_AdminGM_Required = true;

                }
                if (ModelState.IsValid)
                {
                    if(model.TrainingType_Code!=null&& model.training_type!="0")
                    {
                        var ID = int.Parse(model.TrainingType_Code);
                        var traning = dbcontext.TrainingType_Header.FirstOrDefault(m => m.ID == ID);
                        model.training_type= traning.TrainingType_Code+"-"+traning.TrainingType_Desc;
                    }
                    model.Created_By = User.Identity.Name;
                    model.Created_Date = DateTime.Now.Date;
                  
                    dbcontext.TrainingPolicy.Add(model);
                    dbcontext.SaveChanges();
                    //=================================check for alert==================================

                    var get_result_check = HR.Controllers.check.check_alert("training policy", HR.Models.user.Action.Create, HR.Models.user.type_field.form);
                    if (get_result_check != null)
                    {
                        var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.Name, send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
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
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
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
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.training_type = dbcontext.TrainingType_Header.ToList().Select(m => new { Code = m.TrainingType_Code + "-[" + m.TrainingType_Desc + ']', ID = m.ID });
                var record = dbcontext.TrainingPolicy.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = HR.Resource.Basic.thereisnodata;
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(TrainingPolicy model,FormCollection form)
        {
            try
            {
                ViewBag.training_type = dbcontext.TrainingType_Header.ToList().Select(m => new { Code = m.TrainingType_Code + "-[" + m.TrainingType_Desc + ']', ID = m.ID });

                var record = dbcontext.TrainingPolicy.FirstOrDefault(m => m.ID == model.ID);
                if (model.TrainingType_Code != null && model.training_type != "0")
                {
                    var ID = int.Parse(model.TrainingType_Code);
                    var traning = dbcontext.TrainingType_Header.FirstOrDefault(m => m.ID == ID);
                    record.training_type = traning.TrainingType_Code + "-" + traning.TrainingType_Desc;
                }
                else
                {
                    record.training_type = null;
                }
                record.Year = model.Year;
                record.TrainingType_Code = model.TrainingType_Code;
                record.Modified_By = User.Identity.Name;
                record.Modified_Date = DateTime.Now.Date;
                var a1 = form["check1"].Split(',');
                var a2 = form["check2"].Split(',');
                var a3 = form["check3"].Split(',');
                var a4 = form["check4"].Split(',');
                var a5 = form["check5"].Split(',');
                var a6 = form["check6"].Split(',');
                var a7 = form["check7"].Split(',');
                var a8 = form["check8"].Split(',');
                var a9 = form["check9"].Split(',');
                if (a1.Length == 1)
                {
                    record.Check_Employee_Resident_City = false;
                    model.Check_Employee_Resident_City = false;

                }
                else
                {
                    record.Check_Employee_Resident_City = true;
                    model.Check_Employee_Resident_City = true;

                }
                if (a2.Length == 1)
                {
                    record.Max_No_Attended_Cources = false;

                    model.Max_No_Attended_Cources = false;
                    record.Attended_Cources = 0;
                }
                else
                {
                    record.Max_No_Attended_Cources = true;
                    model.Max_No_Attended_Cources =true;
                    record.Attended_Cources = model.Attended_Cources;


                }
                if (a3.Length == 1)
                {
                    record.Min_Employee_Working_Months = false;
                    model.Min_Employee_Working_Months = false;
                    record.Employee_Working_Months = 0;
                }
                else
                {
                    record.Min_Employee_Working_Months = true;
                    model.Min_Employee_Working_Months = true;
                    record.Employee_Working_Months = model.Employee_Working_Months;

                }
                if (a4.Length == 1)
                {
                    record.Min_Appraisal_Grade = false;
                    model.Min_Appraisal_Grade = false;
                    record.Appraisal_Grade = 0;
                }
                else
                {
                    record.Min_Appraisal_Grade = true;
                    model.Min_Appraisal_Grade = true;
                    record.Appraisal_Grade = model.Appraisal_Grade;

                }
                if (a5.Length == 1)
                {
                    record.Min_Years_Of_Experience = false;
                    model.Min_Years_Of_Experience = false;
                    record.Years_Of_Experience = 0;
                }
                else
                {
                    record.Min_Years_Of_Experience = true;
                    model.Min_Years_Of_Experience = true;
                    record.Years_Of_Experience = model.Years_Of_Experience;

                }
                if (a6.Length == 1)
                {
                    record.Review_HRGM_Required = false;
                    model.Review_HRGM_Required = false;

                }
                else
                {
                    record.Review_HRGM_Required = true;
                    model.Review_HRGM_Required = true;
                }
                if (a7.Length == 1)
                {
                    record.Review_AdminGM_Required = false;
                    model.Review_AdminGM_Required = false;
                }
                else
                {
                    record.Review_AdminGM_Required = true;
                    model.Review_AdminGM_Required = true;

                }
                if (a8.Length == 1)
                {
                    record.Approve_HRGM_Required = false;
                    model.Approve_HRGM_Required = false;
                }
                else
                {
                    record.Approve_HRGM_Required = true;
                    model.Approve_HRGM_Required = true;
                }
                if (a9.Length == 1)
                {
                    record.Approve_AdminGM_Required = false;
                    model.Approve_AdminGM_Required = false;
                }
                else
                {
                    record.Approve_AdminGM_Required = true;
                    model.Approve_AdminGM_Required = true;

                }
                dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("training policy", HR.Models.user.Action.edit, HR.Models.user.type_field.form);
                if (get_result_check != null)
                {
                    var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.Name, send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
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
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.TrainingPolicy.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = HR.Resource.Basic.thereisnodata;
                    return View();
                }

            }
            catch (Exception e)
            {
                return View();
            }

        }
        [ActionName("Delete")]
        [HttpPost]
        public ActionResult Deletemethod(int id)
        {
            var record = dbcontext.TrainingPolicy.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.TrainingPolicy.Remove(record);
                dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("training policy", HR.Models.user.Action.delete, HR.Models.user.type_field.form);
                if (get_result_check != null)
                {
                    var inbox = new Models.user.Alert_inbox { send_from_user_id = User.Identity.Name, send_to_user_id = get_result_check.send_to_ID_user, title = get_result_check.Subject, Subject = get_result_check.Message };
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
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.youcannotdeletethisRow;
                return View(record);
            }
            catch (Exception e)
            {
                return View(record);
            }
        }
    }
}