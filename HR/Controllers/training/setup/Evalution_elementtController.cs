using HR.Models;
using HR.Models.Infra;
using HR.Models.Training.setup;
using HR.Models.Training.transaction;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.training.setup
{
    [Authorize(Roles = "Admin,talent,talentSetup,Training centers evalution elements")]
    public class Evalution_elementtController : BaseController
    {
        // GET: Evalution_elementt

        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = dbcontext.TrainingCourceEvaluationElement.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new TrainingCourceEvaluationElement();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Talent_Development).Structure_Code;
            var model = dbcontext.TrainingCourceEvaluationElement.ToList();
            if (model.Count() == 0)
            {
                modell.Element_Code = stru + "1";
            }
            else
            {
                modell.Element_Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            var add_model = new VM_evalu { TrainingCourceEvaluationElement = modell, used_for = used_for.center, type_evalution = type_evalution.Grade };
            return View(add_model);
        }
        [HttpPost]
        public ActionResult Create(VM_evalu model)
        {
            try
            {
               
                    model.TrainingCourceEvaluationElement.Created_By = User.Identity.Name;
                    model.TrainingCourceEvaluationElement.Created_Date = DateTime.Now.Date;
                    model.TrainingCourceEvaluationElement.Element_UsedFor =(Int16) model.used_for.GetHashCode();
                    model.TrainingCourceEvaluationElement.Element_Type = (Int16)model.type_evalution.GetHashCode();
                    dbcontext.TrainingCourceEvaluationElement.Add(model.TrainingCourceEvaluationElement);
                    dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("evalution training elements", HR.Models.user.Action.Create, HR.Models.user.type_field.form);
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
                var record = dbcontext.TrainingCourceEvaluationElement.FirstOrDefault(m => m.ID == id);
                if (record != null)
                {
                    var edit_model = new VM_evalu { TrainingCourceEvaluationElement = record, type_evalution = (type_evalution)record.Element_Type, used_for = (used_for)record.Element_UsedFor };
                    return View(edit_model);
                }
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
        public ActionResult Edit(VM_evalu model)
        {
            try
            {
               var record = dbcontext.TrainingCourceEvaluationElement.FirstOrDefault(m => m.ID == model.TrainingCourceEvaluationElement.ID);

                record.Element_Desc = model.TrainingCourceEvaluationElement.Element_Desc;
                record.Element_Alt_Desc = model.TrainingCourceEvaluationElement.Element_Alt_Desc;
                record.Element_UsedFor = (Int16)model.used_for.GetHashCode();
                record.Element_Type = (Int16)model.type_evalution.GetHashCode();
                record.Modified_By = User.Identity.Name;
                record.Modified_Date = DateTime.Now.Date;
                dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("evalution training elements", HR.Models.user.Action.edit, HR.Models.user.type_field.form);
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
                var record = dbcontext.TrainingCourceEvaluationElement.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.TrainingCourceEvaluationElement.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.TrainingCourceEvaluationElement.Remove(record);
                dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("evalution training elements", HR.Models.user.Action.delete, HR.Models.user.type_field.form);
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
    public enum used_for
    {
        course=1,
        center=2,
        trainee=3,
        instructor=4

    }
    public enum type_evalution
    {
        Grade=1,
        Yes_OR_No=2,
        percentage=3

    }
    public class VM_evalu
    {
        public TrainingCourceEvaluationElement TrainingCourceEvaluationElement { get; set; }
        public used_for used_for { get; set; }
        public type_evalution type_evalution { get; set; }
    }
}