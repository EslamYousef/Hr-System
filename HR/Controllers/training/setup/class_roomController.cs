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
    [Authorize(Roles = "Admin,talent,talentSetup,Class rooms")]
    public class class_roomController : BaseController
    {
        // GET: class_room
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = dbcontext.ClassRoom.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new ClassRoom();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Talent_Development).Structure_Code;
            var model = dbcontext.ClassRoom.ToList();
            if (model.Count() == 0)
            {
                modell.ClassRoom_Code = stru + "1";
            }
            else
            {
                modell.ClassRoom_Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }

            ViewBag.branches = dbcontext.TrainingCenters_Branch_Contact.ToList().Select(m => new { Code = m.Branch_Code + "-[" + m.Contact_Method_Detail + ']', ID = m.ID });
            ViewBag.centers = dbcontext.TrainingCenter.ToList().Select(m => new { Code = m.TrainingCenters_Code + "-[" + m.TrainingCenters_Desc + ']', ID = m.ID });
            ViewBag.class_group = dbcontext.ClassRoom_Group.ToList().Select(m => new { Code = m.ClassRoom_Group_Code + "-[" + m.ClassRoom_Group_Desc + ']', ID = m.ID });
            modell.Maximum_Capacity = 0;modell.Maximum_Occupancy = 0;
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(ClassRoom model)
        {
            try
            {
                ViewBag.branches = dbcontext.TrainingCenters_Branch_Contact.ToList().Select(m => new { Code = m.Branch_Code + "-[" + m.Contact_Method_Detail + ']', ID = m.ID });
                ViewBag.centers = dbcontext.TrainingCenter.ToList().Select(m => new { Code = m.TrainingCenters_Code + "-[" + m.TrainingCenters_Desc + ']', ID = m.ID });
                ViewBag.class_group = dbcontext.ClassRoom_Group.ToList().Select(m => new { Code = m.ClassRoom_Group_Code + "-[" + m.ClassRoom_Group_Desc + ']', ID = m.ID });

                if (ModelState.IsValid)
                {
                    model.Created_By = User.Identity.Name;
                    model.Created_Date = DateTime.Now.Date;
                    dbcontext.ClassRoom.Add(model);
                    dbcontext.SaveChanges();
                    //=================================check for alert==================================

                    var get_result_check = HR.Controllers.check.check_alert("class room", HR.Models.user.Action.Create, HR.Models.user.type_field.form);
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
                var record = dbcontext.ClassRoom.FirstOrDefault(m => m.ID == id);
                ViewBag.branches = dbcontext.TrainingCenters_Branch_Contact.ToList().Select(m => new { Code = m.Branch_Code + "-[" + m.Contact_Method_Detail + ']', ID = m.ID });
                ViewBag.centers = dbcontext.TrainingCenter.ToList().Select(m => new { Code = m.TrainingCenters_Code + "-[" + m.TrainingCenters_Desc + ']', ID = m.ID });
                ViewBag.class_group = dbcontext.ClassRoom_Group.ToList().Select(m => new { Code = m.ClassRoom_Group_Code + "-[" + m.ClassRoom_Group_Desc + ']', ID = m.ID });

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
        public ActionResult Edit(ClassRoom model)
        {
            try
            {
                ViewBag.branches = dbcontext.TrainingCenters_Branch_Contact.ToList().Select(m => new { Code = m.Branch_Code + "-[" + m.Contact_Method_Detail + ']', ID = m.ID });
                ViewBag.centers = dbcontext.TrainingCenter.ToList().Select(m => new { Code = m.TrainingCenters_Code + "-[" + m.TrainingCenters_Desc + ']', ID = m.ID });
                ViewBag.class_group = dbcontext.ClassRoom_Group.ToList().Select(m => new { Code = m.ClassRoom_Group_Code + "-[" + m.ClassRoom_Group_Desc + ']', ID = m.ID });

                var record = dbcontext.ClassRoom.FirstOrDefault(m => m.ID == model.ID);

                record.ClassRoom_Desc = model.ClassRoom_Desc;
                record.ClassRoom_AltDesc = model.ClassRoom_AltDesc;
                record.Maximum_Occupancy = model.Maximum_Occupancy;
                record.Maximum_Capacity = model.Maximum_Capacity;
                record.TrainingCenters_Code = model.TrainingCenters_Code;
                record.Branch_Code = model.Branch_Code;
                record.ClassRoom_Group_Code = model.ClassRoom_Group_Code;
                record.Note = model.Note;
                record.Modified_By = User.Identity.Name;
                record.Modified_Date = DateTime.Now.Date;
                dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("class room", HR.Models.user.Action.edit, HR.Models.user.type_field.form);
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
                var record = dbcontext.ClassRoom.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.ClassRoom.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.ClassRoom.Remove(record);
                dbcontext.SaveChanges();
                //=================================check for alert==================================

                var get_result_check = HR.Controllers.check.check_alert("class room", HR.Models.user.Action.delete, HR.Models.user.type_field.form);
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