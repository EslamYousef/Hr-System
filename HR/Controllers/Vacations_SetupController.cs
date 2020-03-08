using HR.Reposatory.Evalutions.IReposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Reposatory;
using HR.Reposatory.Evalutions.reposatory;
using HR.Models;

namespace HR.Controllers
{
    public class Vacations_SetupController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private readonly IVacations_Setup model;
        private readonly IStructure reposatorystructure;
        public Vacations_SetupController()
            {
            model = new Reposatory.Evalutions.reposatory.Vacations_Setup(new ApplicationDbContext());
            reposatorystructure = new Structure(new ApplicationDbContext());
        }
        // GET: Vacations_Setup
        public ActionResult Index()
        {

            try
            {
                var models = model.GetAll();
            if (models != null) { return View(models); }
            else { TempData["Message"] = HR.Resource.pers_2.Faild; return View(); }
        }
            catch (Exception)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return View();
    }
}
        public ActionResult Add()
        {

            try
            {
                var stru = reposatorystructure.find(Models.Infra.ChModels.Personnel).Structure_Code;
                var list = model.GetAll();
                var modeles = new HR.Models.Vacations_Setup();
                if (list.Count()==0)
                {
                    modeles.LeaveTypeCode = stru+ "1";
                }
                else
                {
                modeles.LeaveTypeCode= stru+ (list.LastOrDefault().ID + 1).ToString();
                }
                ViewBag.Shift_day_status_setup = db.Shift_day_status_setup.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });
                return View(modeles);
            }
            catch (Exception e)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Add(Models.Vacations_Setup Model)
        {

            try
            {
                ViewBag.Shift_day_status_setup = db.Shift_day_status_setup.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });

                if (ModelState.IsValid)
                {
                    if (Model.LeavesType == 0)
                    {
                        ModelState.AddModelError("", "Please Choose from LeavesType");
                        return View(Model);
                    }

                    var flag = model.AddOne(Model);
                    if (flag)
                    {
                        TempData["Message"] = HR.Resource.pers_2.addedSuccessfully;
                        return RedirectToAction("index");
                    }
                    else
                    {
                        TempData["Message"] = HR.Resource.pers_2.Faild;
                        return View(Model);
                    }
                }
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return View(Model);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.Shift_day_status_setup = db.Shift_day_status_setup.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });
                var modeled = model.Find(id);
                if (modeled != null) { return View(modeled); }
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return RedirectToAction("index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Edit(Models.Vacations_Setup Model)
        {
         

            try
            {
                ViewBag.Shift_day_status_setup = db.Shift_day_status_setup.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });

                if (ModelState.IsValid)
                {
               
                      var flag = model.EditOne(Model);
                    if (flag)
                    {
                        TempData["Message"] = HR.Resource.pers_2.addedSuccessfully;
                        return RedirectToAction("index");
                    }
                    else
                    {
                        TempData["Message"] = HR.Resource.pers_2.Faild;
                        return View(model);
                    }
                }
                else
                {

                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return View(model);
                }
            }
            catch (Exception)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return View(model);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {

                var modeldelete = model.Find(id);
                if (modeldelete != null) { return View(modeldelete); }
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return RedirectToAction("index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_method(int id)
        {
            try
            {

                var flag = model.Remove(id);
                if (flag) { TempData["Message"] = HR.Resource.pers_2.removesuccessfully; return RedirectToAction("index"); }
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return View("index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
    }
}