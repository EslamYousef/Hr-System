using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;

namespace HR.Controllers
{
    public class Sings_SymptomController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Sings_Symptom
        public ActionResult Index()
        {
            var model = dbcontext.Sings_Symptom.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Sings_Symptom model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Sings_Symptom recode = new Sings_Symptom();
                    recode.Sing_Code = model.Sing_Code;
                    recode.Sing_Name = model.Sing_Name;
                    recode.Notes = model.Notes;
                    dbcontext.Sings_Symptom.Add(recode);
                    dbcontext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }      
            }
            catch(DbUpdateException)
            {
                TempData["Message"] = "this code Is already exists";
                return View(model);
            }
            catch(Exception e)
            {
                return View(model);

            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                var recode = dbcontext.Sings_Symptom.FirstOrDefault(m => m.ID == id);
                if (recode !=null)
                {
                    return View(recode);
                }
                else
                {
                    TempData["Message"] = "There is no Sings Symptom";
                    return View();
                }
            }
            catch (Exception e)
            {

                return View();
            }
        }
        [HttpPost]
        public ActionResult Edit(Sings_Symptom model)
        {
            try
            {
                var recode = dbcontext.Sings_Symptom.FirstOrDefault(m => m.ID == model.ID);
                recode.Sing_Code = model.Sing_Code;
                recode.Sing_Name = model.Sing_Name;
                recode.Notes = model.Notes;
                dbcontext.SaveChanges();
                return RedirectToAction("Index");
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
        public ActionResult Delete(int id)
        {
            try
            {
                var recode = dbcontext.Sings_Symptom.FirstOrDefault(m => m.ID == id);
                if (recode !=null)
                {
                    return View(recode);
                }
                else
                {
                    TempData["Message"] = "There is no Sings Symptom"; ;
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
            var recode = dbcontext.Sings_Symptom.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Sings_Symptom.Remove(recode);
                dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "You can't delete beacause it have child";
                return View(recode);
            }
            catch(Exception e)
            {
                return View();
            }
        }
    }
}