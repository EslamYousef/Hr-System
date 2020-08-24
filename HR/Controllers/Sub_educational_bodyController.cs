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
    [Authorize(Roles = "Admin,Basic,BasicSetup,Qulifications")]
    public class Sub_educational_bodyController : BaseController
    {
        // GET: Sub_educational_body
        ApplicationDbContext dbcontext = new ApplicationDbContext();
      
        public ActionResult Index()
        {
            var model = dbcontext.Sub_educational_body.ToList();
            return View(model);
        }

        public ActionResult Create(string id)
        {
            var modell = new Sub_educational_body();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var modelll = dbcontext.Sub_educational_body.ToList();
            var Code = "";
            if (modelll.Count() == 0)
            {
                Code = stru + "1";
            }
            else
            {
                Code = stru + (modelll.LastOrDefault().ID + 1).ToString();
            }

            ViewBag.main = dbcontext.Main_Educate_body.ToList().Select(m=>new { Code=m.Code+"----"+m.Name,ID=m.ID});
            if (id != null)
            {
                var ID = int.Parse(id);
                var Main_Educate_body = dbcontext.Main_Educate_body.FirstOrDefault(m => m.ID == ID);
                var model = new Sub_educational_body {Code=Code, Main_Educate_body=Main_Educate_body,Main_Educate_bodyid=Main_Educate_body.ID};
                return View(model);
            }
            var mmodel = new Sub_educational_body();
            mmodel.Code = Code;
            return View(mmodel);
        }
        [HttpPost]
        public ActionResult Create(Sub_educational_body model)
        {
            try
            {
                ViewBag.main = dbcontext.Main_Educate_body.ToList().Select(m => new { Code = m.Code + "----" + m.Name, ID = m.ID });

                if (ModelState.IsValid)
                {
                    Sub_educational_body record = new Sub_educational_body();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Main_Educate_bodyid = model.Main_Educate_bodyid;
                    record.Code = model.Code;
                    record.Main_Educate_body = dbcontext.Main_Educate_body.FirstOrDefault(m => m.ID == model.Main_Educate_bodyid);
                    dbcontext.Sub_educational_body.Add(record);
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
                ViewBag.main = dbcontext.Main_Educate_body.ToList().Select(m => new { Code = m.Code + "----" + m.Name, ID = m.ID });
                var record = dbcontext.Sub_educational_body.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no country";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Sub_educational_body model)
        {
            try
            {
                ViewBag.main = dbcontext.Main_Educate_body.ToList().Select(m => new { Code = m.Code + "----" + m.Name, ID = m.ID });
                var record = dbcontext.Sub_educational_body.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Main_Educate_bodyid = model.Main_Educate_bodyid;
                record.Code = model.Code;
                record.Main_Educate_body = dbcontext.Main_Educate_body.FirstOrDefault(m => m.ID == model.Main_Educate_bodyid);
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
                var record = dbcontext.Sub_educational_body.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no data";
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
            var record = dbcontext.Sub_educational_body.FirstOrDefault(m => m.ID == id);
            try
            {
               dbcontext.Sub_educational_body.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "you can't delete it";
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}