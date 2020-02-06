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
    public class EOS_Interview_Questions_GroupsController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: EOS_Interview_Questions_Groups
        public ActionResult Index()
        {
            var model = dbcontext.EOS_Interview_Questions_Groups.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.EOS_Interview_Questions_Groups.ToList();
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

            var modell = new EOS_Interview_Questions_Groups { Questions_Group_Code = stru.Structure_Code + count };
            return View(modell);

        }
        [HttpPost]
        public ActionResult Create(EOS_Interview_Questions_Groups model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EOS_Interview_Questions_Groups record = new EOS_Interview_Questions_Groups();
                    record.Questions_Group_Code = model.Questions_Group_Code;
                    record.Description_Alternative = model.Description_Alternative;
                    record.Description_of = model.Description_of;
                    dbcontext.EOS_Interview_Questions_Groups.Add(record);
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
                var record = dbcontext.EOS_Interview_Questions_Groups.FirstOrDefault(m => m.ID == id);

                if (record != null)
                {
                    return View(record);

                }
                else
                {
                    TempData["Message"] = "There is no EOS Interview Questions Groups";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(EOS_Interview_Questions_Groups model)
        {
            try
            {
                var record = dbcontext.EOS_Interview_Questions_Groups.FirstOrDefault(m => m.ID == model.ID);
                record.Questions_Group_Code = model.Questions_Group_Code;
                record.Description_Alternative = model.Description_Alternative;
                record.Description_of = model.Description_of;
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
                var record = dbcontext.EOS_Interview_Questions_Groups.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no EOS Interview Questions Groups"; ;
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
            var record = dbcontext.EOS_Interview_Questions_Groups.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.EOS_Interview_Questions_Groups.Remove(record);
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