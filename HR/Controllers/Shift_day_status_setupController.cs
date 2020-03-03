using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class Shift_day_status_setupController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Shift_day_status_setup
        public ActionResult Index()
        {
            var model = dbcontext.Shift_day_status_setup.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            //////
            var modell = new Shift_day_status_setup();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var model = dbcontext.Shift_day_status_setup.ToList();
            if (model.Count() == 0)
            {
                modell.Code = stru + "1";
            }
            else
            {
                modell.Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Shift_day_status_setup model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Shift_day_status_setup record = new Shift_day_status_setup();
                    record.Description = model.Description;
                    record.Code = model.Code;
                    dbcontext.Shift_day_status_setup.Add(record);
                    dbcontext.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }
        }
    }
}