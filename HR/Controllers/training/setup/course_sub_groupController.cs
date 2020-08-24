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
    [Authorize(Roles = "Admin,talent,talentSetup,Courses_setup")]
    public class course_sub_groupController : BaseController
    {
        // GET: course_sub_group
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = dbcontext.Course_SubGroups.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new Course_SubGroups();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Talent_Development).Structure_Code;
            var model = dbcontext.Course_SubGroups.ToList();
            if (model.Count() == 0)
            {
                modell.SubGroup_Code = stru + "1";
            }
            else
            {
                modell.SubGroup_Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            ViewBag.group = dbcontext.Course_Groups.ToList().Select(m => new { Code = m.Group_Code + "-[" + m.Group_Desc + ']', ID = m.Group_Code });

            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Course_SubGroups model)
        {
            try
            {
                ViewBag.group = dbcontext.Course_Groups.ToList().Select(m => new { Code = m.Group_Code + "-[" + m.Group_Desc + ']', ID = m.Group_Code });
                if (ModelState.IsValid)
                {
                    model.Created_By = User.Identity.Name;
                    model.Created_Date = DateTime.Now.Date;
                    dbcontext.Course_SubGroups.Add(model);
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
                ViewBag.group = dbcontext.Course_Groups.ToList().Select(m => new { Code = m.Group_Code + "-[" + m.Group_Desc + ']', ID = m.Group_Code });
                var record = dbcontext.Course_SubGroups.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Course_SubGroups model)
        {
            try
            {
                ViewBag.group = dbcontext.Course_Groups.ToList().Select(m => new { Code = m.Group_Code + "-[" + m.Group_Desc + ']', ID = m.Group_Code });
                var record = dbcontext.Course_SubGroups.FirstOrDefault(m => m.ID == model.ID);

                record.SubGroup_Desc = model.SubGroup_Desc;
                record.SubGroup_AltDesc = model.SubGroup_AltDesc;
                record.Group_Code = model.Group_Code;
                record.Modified_By = User.Identity.Name;
                record.Modified_Date = DateTime.Now.Date;
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
                var record = dbcontext.Course_SubGroups.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Course_SubGroups.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Course_SubGroups.Remove(record);
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