using HR.Models;
using HR.Models.Infra;
using HR.Models.penalities.setup;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.penalites.setup
{
    public class punishmentController : BaseController
    {
        // GET: punishment
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.Discipline_Punishment.ToList();
            return View(model);
        }
        public ActionResult create()
        {
            try
            {

                ViewBag.rest = dbcontext.Discipline_PunishmentRestOption.ToList().Select(m => new { Code = m.RestOption_Code + "-[" + m.RestOption_Desc + ']', ID = m.ID });
                ViewBag.group = dbcontext.Discipline_PunishmentGroup.ToList().Select(m => new { Code = m.PunishmentGroup_Code + "-[" + m.PunishmentGroup_Desc + ']', ID = m.ID });
                ViewBag.penality = dbcontext.Discipline_PenaltyItem_Header.ToList().Select(m => new { Code = m.PenaltyItem_Code + "-[" + m.PenaltyItem_Desc + ']', ID = m.ID });

                var new_model = new Discipline_Punishment();
                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
                var model = dbcontext.Discipline_Punishment.ToList();
                if (model.Count() == 0)
                {
                    new_model.Punishment_Code = stru + "1";
                }
                else
                {
                    new_model.Punishment_Code = stru + (model.LastOrDefault().ID + 1).ToString();
                }
                /////

                return View(new_model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult create(FormCollection form, Discipline_Punishment model)
        {
            try
            {
                ViewBag.rest = dbcontext.Discipline_PunishmentRestOption.ToList().Select(m => new { Code = m.RestOption_Code + "-[" + m.RestOption_Desc + ']', ID = m.ID });
                ViewBag.group = dbcontext.Discipline_PunishmentGroup.ToList().Select(m => new { Code = m.PunishmentGroup_Code + "-[" + m.PunishmentGroup_Desc + ']', ID = m.ID });
                ViewBag.penality = dbcontext.Discipline_PenaltyItem_Header.ToList().Select(m => new { Code = m.PenaltyItem_Code + "-[" + m.PenaltyItem_Desc + ']', ID = m.ID });

                //================================
                model.Created_By = User.Identity.Name;
                model.Created_Date = DateTime.Now;
                var pun=dbcontext.Discipline_Punishment.Add(model);
                dbcontext.SaveChanges();
                //================================

                var center_D = form["center_id"].Split(',');
                for (var i = 0; i < center_D.Length; i++)
                {
                    if (center_D[i] != "")
                    {
                        var ID = int.Parse(center_D[i]);
                        var CENTER = dbcontext.Discipline_PenaltyItem_Header.FirstOrDefault(m => m.ID == ID);
                        var Cource_cen = new Discipline_Punishment_Detail {penal_Des=CENTER.PenaltyItem_Code+"-"+CENTER.PenaltyItem_Desc, Created_By=User.Identity.Name,Created_Date=DateTime.Now.Date,PenaltyItem_Code=CENTER.ID.ToString(),Punishment_Code=pun.ID.ToString(),PenaltyItem_Level=(short)(i)};
                        dbcontext.Discipline_Punishment_Detail.Add(Cource_cen);
                        dbcontext.SaveChanges();
                    }
                }
                //================================

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
        public ActionResult edit(int id)
        {
            try
            {

                ViewBag.rest = dbcontext.Discipline_PunishmentRestOption.ToList().Select(m => new { Code = m.RestOption_Code + "-[" + m.RestOption_Desc + ']', ID = m.ID });
                ViewBag.group = dbcontext.Discipline_PunishmentGroup.ToList().Select(m => new { Code = m.PunishmentGroup_Code + "-[" + m.PunishmentGroup_Desc + ']', ID = m.ID });
                ViewBag.penality = dbcontext.Discipline_PenaltyItem_Header.ToList().Select(m => new { Code = m.PenaltyItem_Code + "-[" + m.PenaltyItem_Desc + ']', ID = m.ID });

                //================
                var cours = dbcontext.Discipline_Punishment.FirstOrDefault(m => m.ID == id);
                return View(cours);
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(Discipline_Punishment model, FormCollection form)
        {
            try
            {
                ViewBag.rest = dbcontext.Discipline_PunishmentRestOption.ToList().Select(m => new { Code = m.RestOption_Code + "-[" + m.RestOption_Desc + ']', ID = m.ID });
                ViewBag.group = dbcontext.Discipline_PunishmentGroup.ToList().Select(m => new { Code = m.PunishmentGroup_Code + "-[" + m.PunishmentGroup_Desc + ']', ID = m.ID });
                ViewBag.penality = dbcontext.Discipline_PenaltyItem_Header.ToList().Select(m => new { Code = m.PenaltyItem_Code + "-[" + m.PenaltyItem_Desc + ']', ID = m.ID });
                //================================
                var cours = dbcontext.Discipline_Punishment.FirstOrDefault(m => m.ID == model.ID);
                cours.Modified_By = User.Identity.Name;
                cours.Modified_Date = DateTime.Now.Date;
                cours.Punishment_Desc = model.Punishment_Desc;
                cours.Punishment_AltDesc = model.Punishment_AltDesc;
                cours.RestOption_Code = model.RestOption_Code;
                cours.PunishmentGroup_Code = model.PunishmentGroup_Code;
                dbcontext.SaveChanges();
                //================================
                var old_course_center = dbcontext.Discipline_Punishment_Detail.Where(m => m.Punishment_Code == cours.ID.ToString()).ToList();
                if (old_course_center != null)
                {
                    dbcontext.Discipline_Punishment_Detail.RemoveRange(old_course_center);
                    dbcontext.SaveChanges();
                }
                //================================
                //================================

                var center_D = form["center_id"].Split(',');
                for (var i = 0; i < center_D.Length; i++)
                {
                    if (center_D[i] != "")
                    {
                        var ID = int.Parse(center_D[i]);
                        var CENTER = dbcontext.Discipline_PenaltyItem_Header.FirstOrDefault(m => m.ID == ID);
                        var Cource_cen = new Discipline_Punishment_Detail { penal_Des = CENTER.PenaltyItem_Code + "-" + CENTER.PenaltyItem_Desc, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, PenaltyItem_Code = CENTER.ID.ToString(), Punishment_Code = cours.ID.ToString(), PenaltyItem_Level = (short)(i) };
                        dbcontext.Discipline_Punishment_Detail.Add(Cource_cen);
                        dbcontext.SaveChanges();
                    }
                }
                //================================

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
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.Discipline_Punishment.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Discipline_Punishment.FirstOrDefault(m => m.ID == id);
            var centers = dbcontext.Discipline_Punishment_Detail.Where(m => m.Punishment_Code == record.ID.ToString()).ToList();
            try
            {
                dbcontext.Discipline_Punishment_Detail.RemoveRange(centers);
                dbcontext.Discipline_Punishment.Remove(record);
                dbcontext.SaveChanges();
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
        //============ajax=============

        public JsonResult getallcontent(string coures_code)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var content = dbcontext.Discipline_Punishment_Detail.Where(m => m.Punishment_Code == coures_code).ToList();
            return Json(content);
        }
    }
}