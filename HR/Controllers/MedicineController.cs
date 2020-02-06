using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;

namespace HR.Controllers
{
    [Authorize]
    public class MedicineController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Medicine
        public ActionResult Index()
        {
            var model = dbcontext.Medicine.ToList();
            return View(model);
        }
        public ActionResult Create(string id)
        {
            ViewBag.Medical_Medicine_Classfication = dbcontext.Medical_Medicine_Classfication.ToList().Select(m => new { Code = +m.Code + "-----[" + m.Name + ']', ID = m.ID });
            if (id !=null)
            {
                var ID = int.Parse(id);
                var Medical_Medicine_Classfication = dbcontext.Medical_Medicine_Classfication.FirstOrDefault(m => m.ID == ID);
                var model = new Medicine { Medical_Medicine_Classfication = Medical_Medicine_Classfication, Medical_Medicine_ClassficationId = Medical_Medicine_Classfication.ID.ToString() };
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Medicine model,string command)
        {
            try
            {
                ViewBag.Medical_Medicine_Classfication = dbcontext.Medical_Medicine_Classfication.ToList().Select(m => new { Code = +m.Code + "-----[" + m.Name + ']', ID = m.ID });

                if (ModelState.IsValid)
                {
                    if (model.Medical_Medicine_ClassficationId == "0" || model.Medical_Medicine_ClassficationId==null)
                    {
                        ModelState.AddModelError("", "Medical_Medicine_Classfication Code must enter");
                        return View(model);
                    }
                    Medicine record = new Medicine();
                  record.Code = model.Code;
                    record.Name = model.Name;
                    record.Description = model.Description;               
                    record.Is_Foreign = model.Is_Foreign;
                    record.price = model.price;
                    record.Indication = model.Indication;
                    record.Precaution_Warnings = model.Precaution_Warnings;
                    record.Note = model.Note;
                    record.Usual_Dosage = model.Usual_Dosage;
                    record.Contraindication = model.Contraindication;
                    record.Medical_Medicine_ClassficationId = model.Medical_Medicine_ClassficationId;
                    var Medical_Medicine_ClassficationId = int.Parse(model.Medical_Medicine_ClassficationId);
                    record.Medical_Medicine_Classfication = dbcontext.Medical_Medicine_Classfication.FirstOrDefault(m => m.ID == Medical_Medicine_ClassficationId);
                    dbcontext.Medicine.Add(record);
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
                ViewBag.Medical_Medicine_Classfication = dbcontext.Medical_Medicine_Classfication.ToList().Select(m => new { Code = +m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Medicine.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Medical Medicine Classfication";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Medicine model)
        {
            try
            {
                ViewBag.Medical_Medicine_Classfication = dbcontext.Medical_Medicine_Classfication.ToList().Select(m => new { Code = +m.Code + "------[" + m.Name + ']', ID = m.ID });
                if (model.Medical_Medicine_ClassficationId == "0" || model.Medical_Medicine_ClassficationId == null)
                {
                    ModelState.AddModelError("", "Country Code must enter");
                    return View(model);
                }
                var record = dbcontext.Medicine.FirstOrDefault(m => m.ID == model.ID);
                record.Code = model.Code;
                record.Name = model.Name;
                record.Description = model.Description;
                record.Is_Foreign = model.Is_Foreign;
                record.price = model.price;
                record.Indication = model.Indication;
                record.Precaution_Warnings = model.Precaution_Warnings;
                record.Note = model.Note;
                record.Usual_Dosage = model.Usual_Dosage;
                record.Contraindication = model.Contraindication;
                record.Medical_Medicine_ClassficationId = model.Medical_Medicine_ClassficationId;
                var Medical_Medicine_ClassficationId = int.Parse(model.Medical_Medicine_ClassficationId);
                record.Medical_Medicine_Classfication = dbcontext.Medical_Medicine_Classfication.FirstOrDefault(m => m.ID == Medical_Medicine_ClassficationId);

                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "This code Is already exists";
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.Medicine.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Medical Medicine Classfication";
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
            var record = dbcontext.Medicine.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Medicine.Remove(record);
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