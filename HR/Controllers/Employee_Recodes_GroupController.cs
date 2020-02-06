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
    public class Employee_Recodes_GroupController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_Recodes_Group
        public ActionResult Index()
        {
            var model = dbcontext.Employee_Recodes_Group.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Employee_Recodes_Group.ToList();
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

            var modell = new Employee_Recodes_Group { Record_Group_Code = stru.Structure_Code + count };
            return View(modell);

        }
        [HttpPost]
        public ActionResult Create(Employee_Recodes_Group model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Employee_Recodes_Group record = new Employee_Recodes_Group();
                    record.Record_Group_Code = model.Record_Group_Code;
                    record.Record_Group_Description = model.Record_Group_Description;
                    record.Record_Group_Alternative = model.Record_Group_Alternative;
                    dbcontext.Employee_Recodes_Group.Add(record);
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
                TempData["Message"] = "this code Is already exists";
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
                var record = dbcontext.Employee_Recodes_Group.FirstOrDefault(m => m.ID == id);

                if (record != null)
                {
                    return View(record);

                }
                else
                {
                    TempData["Message"] = "There is no Employee Recodes Group";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Employee_Recodes_Group model)
        {
            try
            {
                var record = dbcontext.Employee_Recodes_Group.FirstOrDefault(m => m.ID == model.ID);
                record.Record_Group_Code = model.Record_Group_Code;
                record.Record_Group_Description = model.Record_Group_Description;
                record.Record_Group_Alternative = model.Record_Group_Alternative;
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "this code Is already exists";
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.Employee_Recodes_Group.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Employee Recodes Group";
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
            var record = dbcontext.Employee_Recodes_Group.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Employee_Recodes_Group.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "You can't delete beacause it have child";
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }

    }
}