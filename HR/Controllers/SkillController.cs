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
    [Authorize(Roles = "Admin,Organization,OrganizationSetup,Skills")]
    public class SkillController : BaseController
    {
        // GET: Skillt
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.Skill.ToList();
            return View(model);
        }

        public ActionResult Create(string id)
        {
            var modell = new Skill();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Organization).Structure_Code;
            var modelll = dbcontext.Skill.ToList();
            var Code="";
            if (modelll.Count() == 0)
            {
                Code = stru + "1";
            }
            else
            {
              Code = stru + (modelll.LastOrDefault().ID + 1).ToString();
            }

            ViewBag.Skill_group = dbcontext.Skill_group.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
            if (id != null)
            {
                var ID = int.Parse(id);
                var Skill_group = dbcontext.Skill_group.FirstOrDefault(m => m.ID == ID);
                var model = new Skill {Code=Code, Skill_group=Skill_group,Skill_groupId=Skill_group.ID.ToString()};
                return View(model);
            }
            var mm = new Skill();
            mm.Code = Code;
            return View(mm);
        }
        [HttpPost]
        public ActionResult Create(Skill model)
        {
            try
            {
                ViewBag.Skill_group = dbcontext.Skill_group.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                if (ModelState.IsValid)
                {
                    Skill record = new Skill();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    record.Skill_groupId = model.Skill_groupId;
                    var ID = int.Parse(model.Skill_groupId);
                    record.Skill_group = dbcontext.Skill_group.FirstOrDefault(m => m.ID == ID);
                    dbcontext.Skill.Add(record);
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
                ViewBag.Skill_group = dbcontext.Skill_group.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                var record = dbcontext.Skill.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Skill model)
        {
            try
            {
                ViewBag.Skill_group = dbcontext.Skill_group.ToList().Select(m => new { ID = m.ID, Code = m.Code + "->" + m.Name });
                var record = dbcontext.Skill.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Code = model.Code;
                record.Skill_groupId = model.Skill_groupId;
                var ID = int.Parse(model.Skill_groupId);
                record.Skill_group = dbcontext.Skill_group.FirstOrDefault(m => m.ID == ID);
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
                var record = dbcontext.Skill.FirstOrDefault(m => m.ID == id);
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
            try
            {
                var record = dbcontext.Skill.FirstOrDefault(m => m.ID == id);
                dbcontext.Skill.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}