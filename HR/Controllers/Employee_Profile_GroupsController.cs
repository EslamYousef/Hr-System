using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;
using HR.Models.Infra;
namespace HR.Controllers
{
    [Authorize]
    public class Employee_Profile_GroupsController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_Profile_Groups
        public ActionResult Index()
        {
            var model = dbcontext.Employee_Profile_Groups.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Employee_Profile_Groups.ToList();
            var count = 0;
            if (model.Count() == 0)
            {
                count = 1;
            }
            else
            {
                var te = model.LastOrDefault().ID;
                count = te + 1;
            }

            var modell = new Employee_Profile_Groups { Group_Code = stru.Structure_Code + count };
            return View(modell);

        }
        [HttpPost]
        public ActionResult Create(Employee_Profile_Groups model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Employee_Profile_Groups record = new Employee_Profile_Groups();
                    record.Group_Code = model.Group_Code;
                    record.Group_Description = model.Group_Description;
                    record.Group_Alternative_Description = model.Group_Alternative_Description;
                    dbcontext.Employee_Profile_Groups.Add(record);
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
        public ActionResult Edit(int id)
        {
            try
            {
                var record = dbcontext.Employee_Profile_Groups.FirstOrDefault(m => m.ID == id);

                if (record != null)
                {
                    return View(record);

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
        public ActionResult Edit(Employee_Profile_Groups model)
        {
            try
            {
                var record = dbcontext.Employee_Profile_Groups.FirstOrDefault(m => m.ID == model.ID);
                record.Group_Code = model.Group_Code;
                record.Group_Description = model.Group_Description;
                record.Group_Alternative_Description = model.Group_Alternative_Description;
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
                var record = dbcontext.Employee_Profile_Groups.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Employee_Profile_Groups.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Employee_Profile_Groups.Remove(record);
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
                return View();
            }
        }

    }
}