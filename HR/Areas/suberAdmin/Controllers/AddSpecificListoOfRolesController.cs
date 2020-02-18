using HR.Areas.suberAdmin.Models;
using HR.Models;
using HR.Reposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Areas.suberAdmin.Controllers
{
    public class AddSpecificListoOfRolesController : Controller
    {
        // GET: suberAdmin/AddSpecificListoOfRoles

        private readonly IAddSpecificListOfRoles model;
       public AddSpecificListoOfRolesController()
        {
            model = new AddSpecificListOfRoles(new ApplicationDbContext());
        }
        public ActionResult Index()
        {
            var models = model.GetAll();
            return View(model);
        }
        public ActionResult Add()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Add(Roles Model)
        {
            try
            {
                var flag = model.AddOne(Model);
                TempData["Message"] = HR.Resource.pers_2.addedSuccessfully;
                return RedirectToAction("index");
            }
            catch(Exception e)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return RedirectToAction("index");
            }
        }
        public ActionResult Remove(int ID)
        {
            try
            {
                var taregrModel = model.Find(ID);
                return View(taregrModel);
            }
            catch (Exception e)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("Remove")]
        public ActionResult MethodRemove(int ID)
        {
            try
            {
                var taregrModel = model.Find(ID);
                return View(taregrModel);
            }
            catch (Exception e)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return RedirectToAction("index");
            }
        }

    }
}