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
    public class RestoptionsController : BaseController
    {
        // GET: Restoptions
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = dbcontext.Discipline_PunishmentRestOption.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new Discipline_PunishmentRestOption();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
            var model = dbcontext.Discipline_PunishmentRestOption.ToList();
            if (model.Count() == 0)
            {
                modell.RestOption_Code = stru + "1";
            }
            else
            {
                modell.RestOption_Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            modell.RestOption_Days = 0; 
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Discipline_PunishmentRestOption model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Created_By = User.Identity.Name;
                    model.Created_Date = DateTime.Now.Date;
                    var tra_center = dbcontext.Discipline_PunishmentRestOption.Add(model);
                    dbcontext.SaveChanges();
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
                var record = dbcontext.Discipline_PunishmentRestOption.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Discipline_PunishmentRestOption model)
        {
            try
            {
                var record = dbcontext.Discipline_PunishmentRestOption.FirstOrDefault(m => m.ID == model.ID);

                record.RestOption_Desc = model.RestOption_Desc;
                record.RestOption_AltDesc = model.RestOption_AltDesc;
                record.Modified_By = User.Identity.Name;
                record.Modified_Date = DateTime.Now.Date;
                record.RestOption_Days = model.RestOption_Days;
                dbcontext.SaveChanges();
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
                var record = dbcontext.Discipline_PunishmentRestOption.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Discipline_PunishmentRestOption.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Discipline_PunishmentRestOption.Remove(record);
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
    }
}