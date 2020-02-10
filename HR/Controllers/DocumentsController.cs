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
    [Authorize]
    public class DocumentsController : BaseController
    {
        // GET: Documents
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = dbcontext.Documents.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new Documents();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var model = dbcontext.Documents.ToList();
            if (model.Count() == 0)
            {
                modell.Code = stru + "1";
            }
            else
            {
                modell.Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            ViewBag.Document_Group = dbcontext.Document_Group.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Documents model)
        {
            try
            {
                ViewBag.Document_Group = dbcontext.Document_Group.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                if (ModelState.IsValid)
                {
                    Documents record = new Documents();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Document_Groupid = model.Document_Groupid;
                    record.Code = model.Code;
                    var id = int.Parse(model.Document_Groupid);
                    record.Document_Group = dbcontext.Document_Group.FirstOrDefault(m => m.ID == id);
                    dbcontext.Documents.Add(record);
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
                ViewBag.Document_Group = dbcontext.Document_Group.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Documents.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Documents model)
        {
            try
            {
                ViewBag.Document_Group = dbcontext.Document_Group.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var record = dbcontext.Documents.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Document_Groupid = model.Document_Groupid;
                record.Code = model.Code;
                var id = int.Parse(model.Document_Groupid);
                record.Document_Group = dbcontext.Document_Group.FirstOrDefault(m => m.ID == id);
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
                var record = dbcontext.Documents.FirstOrDefault(m => m.ID == id);
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
                var record = dbcontext.Documents.FirstOrDefault(m => m.ID == id);
                dbcontext.Documents.Remove(record);
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