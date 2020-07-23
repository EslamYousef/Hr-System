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
    public class Public_Holiday_EventsController : BaseController
    {
        // GET: Public_Holiday_Events
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Definition_of_EOS_Interview_Questions
        public ActionResult Index()
        {
            var model = dbcontext.Public_Holiday_Events.ToList();
            return View(model);
        }
        public ActionResult Create()
        {

            //var o = dbcontext.EOS_Interview_Questions_Groups.ToList().Select(m => new { Code = "" + m.Questions_Group_Code + "-----[" + m.Description_of + ']', ID = m.ID }).ToList();
            //ViewBag.EOS_Interview_Questions_Groups = o;
            //if (o == null || o.Count() == 0)
            //{
            //    TempData["Message"] = HR.Resource.Personnel.EnterdatafirstonEOSInterviewQuestionsGroups;
            //    var modelll = dbcontext.Definition_of_EOS_Interview_Questions.ToList();
            //    return View("index", modelll);
            //}
            ViewBag.Shift_day_status_setup = dbcontext.Shift_day_status_setup.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Public_Holiday_Events.ToList();
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
            var modell = new Public_Holiday_Events { Code = stru.Structure_Code + count , Type_Holiday = Type_Holiday.Public_Holiday};
            return View(modell);

        }
        [HttpPost]
        public ActionResult Create(Public_Holiday_Events model)
        {
            try
            {
                ViewBag.Shift_day_status_setup = dbcontext.Shift_day_status_setup.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });

                if (ModelState.IsValid)
                {
                    if (model.ShiftdaystatussetupId == 0)
                    {
                        ModelState.AddModelError("", HR.Resource.Personnel.EOSInterviewQuestionsGroupsCodemustenter);
                        return View(model);
                    }
                    if (model.Type_Holiday == 0 )
                    {
                        ModelState.AddModelError("", "Please Choose from Type");
                        return View(model);
                    }
                    Public_Holiday_Events record = new Public_Holiday_Events();
                    record.Code = model.Code;
                    record.Description = model.Description;
                    record.AlternativeDescription = model.AlternativeDescription;
                    record.Type_Holiday = model.Type_Holiday;
                    record.ShiftdaystatussetupId = model.ShiftdaystatussetupId;
                     dbcontext.Public_Holiday_Events.Add(record);
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
                ViewBag.Shift_day_status_setup = dbcontext.Shift_day_status_setup.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });
                var record = dbcontext.Public_Holiday_Events.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Public_Holiday_Events model)
        {
            try
            {
                ViewBag.Shift_day_status_setup = dbcontext.Shift_day_status_setup.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });
                if (model.ShiftdaystatussetupId == 0)
                {
                    ModelState.AddModelError("", HR.Resource.Personnel.EOSInterviewQuestionsGroupsCodemustenter);
                    return View(model);
                }
                if (model.Type_Holiday == 0)
                {
                    ModelState.AddModelError("", "Please Choose from Type");
                    return View(model);
                }
                var record = dbcontext.Public_Holiday_Events.FirstOrDefault(m => m.ID == model.ID);
                record.Code = model.Code;
                record.Description = model.Description;
                record.AlternativeDescription = model.AlternativeDescription;
                record.Type_Holiday = model.Type_Holiday;
                record.ShiftdaystatussetupId = model.ShiftdaystatussetupId;
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
                var record = dbcontext.Public_Holiday_Events.FirstOrDefault(m => m.ID == id);
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
                var record = dbcontext.Public_Holiday_Events.FirstOrDefault(m => m.ID == id);
                dbcontext.Public_Holiday_Events.Remove(record);
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