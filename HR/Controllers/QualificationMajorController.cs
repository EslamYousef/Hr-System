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
    public class QualificationMajorController : Controller
    {
        // GET: QualificationMajor
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = dbcontext.Qualification_Major.ToList();
            return View(model);
        }

        public ActionResult Create(string id)
        {
            var modell = new Qualification_Major();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var modelll = dbcontext.Qualification_Major.ToList();
            var Code = "";
            if (modelll.Count() == 0)
            {
                Code = stru + "1";
            }
            else
            {
                Code = stru + (modelll.LastOrDefault().ID + 1).ToString();
            }

            ViewBag.titleee = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "----" + m.Name, ID = m.ID });
            ViewBag.name = new List<Name_of_educational_qualification>();
            if (id != null)
            {
                var ID = int.Parse(id);
                var name = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == ID);
                var model = new Qualification_Major {Code=Code, Name_of_educational_qualification=name,Name_of_educational_qualificationid=name.ID,Educate_Titleid=name.Educate_Title.ID  };
             
                    ViewBag.name = dbcontext.Name_of_educational_qualification.Where(m => m.Educate_Titleid == model.Educate_Titleid);
               
                return View(model);
            }
            var mmodel = new Qualification_Major();
            mmodel.Code = Code;
            return View(mmodel);
        }
        [HttpPost]
        public ActionResult Create(Qualification_Major model)
        {
            try
            {
                ViewBag.titleee = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "----" + m.Name, ID = m.ID });
                if (model.Educate_Titleid > 0 )
                {
                    ViewBag.name = dbcontext.Name_of_educational_qualification.Where(m => m.Educate_Titleid == model.Educate_Titleid).ToList().Select(m => new { Code = m.Code + "----" + m.Name, ID = m.ID });
                }
                else
                {
                    ViewBag.name = new List<Name_of_educational_qualification>();
                }


                if (ModelState.IsValid)
                {
                    if (model.Name_of_educational_qualificationid <= 0)
                    {
                        ModelState.AddModelError("", "State Code must enter");
                        return View(model);
                    }
                    Qualification_Major record = new Qualification_Major();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Name_of_educational_qualificationid = model.Name_of_educational_qualificationid;
                    record.Code = model.Code;
                    record.Name_of_educational_qualification = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == model.Name_of_educational_qualificationid);
                    record.Educate_Titleid = model.Educate_Titleid;
                    dbcontext.Qualification_Major.Add(record);
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
                ViewBag.titleee = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "----" + m.Name, ID = m.ID });
                var record = dbcontext.Qualification_Major.FirstOrDefault(m => m.ID == id);
                ViewBag.name = dbcontext.Name_of_educational_qualification.Where(m => m.Educate_Titleid == record.Educate_Titleid).ToList().Select(m => new { Code = m.Code + "----" + m.Name, ID = m.ID });

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
        public ActionResult Edit(Qualification_Major model)
        {
            ViewBag.titleee = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "----" + m.Name, ID = m.ID });
            if (model.Educate_Titleid > 0)
            {
                ViewBag.name = dbcontext.Name_of_educational_qualification.Where(m => m.Educate_Titleid == model.Educate_Titleid).ToList().Select(m => new { Code = m.Code + "----" + m.Name, ID = m.ID });
            }
            else
            {
                ViewBag.name = new List<Name_of_educational_qualification>();
            }
            try
            {
                if (model.Name_of_educational_qualificationid <= 0)
                {
                    ModelState.AddModelError("", "State Code must enter");
                    return View(model);
                }
                var record = dbcontext.Qualification_Major.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Name_of_educational_qualificationid = model.Name_of_educational_qualificationid;
                record.Educate_Titleid = model.Educate_Titleid;
                record.Code = model.Code;
                record.Name_of_educational_qualification = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == model.Name_of_educational_qualificationid);
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
                var record = dbcontext.Qualification_Major.FirstOrDefault(m => m.ID == id);
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
            try
            {
                var record = dbcontext.Qualification_Major.FirstOrDefault(m => m.ID == id);
                dbcontext.Qualification_Major.Remove(record);
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