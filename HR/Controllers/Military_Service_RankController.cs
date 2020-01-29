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
    public class Military_Service_RankController : Controller
    {
        // GET: Military_Service_Rank
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {

            var model = dbcontext.Military_Service_Rank.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new Military_Service_Rank();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var model = dbcontext.Military_Service_Rank.ToList();
            if (model.Count() == 0)
            {
                modell.Code = stru + "1";
            }
            else
            {
                modell.Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Military_Service_Rank model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Military_Service_Rank record = new Military_Service_Rank();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    dbcontext.Military_Service_Rank.Add(record);
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
                var record = dbcontext.Military_Service_Rank.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Military_Service_Rank model)
        {
            try
            {
                var record = dbcontext.Military_Service_Rank.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Code = model.Code;
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
                var record = dbcontext.Military_Service_Rank.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no country";
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
            var record = dbcontext.Military_Service_Rank.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Military_Service_Rank.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "this code Is already exists";
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}