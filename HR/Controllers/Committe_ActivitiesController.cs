using HR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models.Infra;
using HR.Models.ViewModel;
using HR.Models.All_Table_Commitee_Resolution;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,Recuirtment,RecuirtmentCards,Committe Resolution Rec")]
    public class Committe_ActivitiesController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Committe_Activities
        public ActionResult Create(string id)
        {
            var ID = int.Parse(id);
            ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(a => a.ID == ID).Code;
            ViewBag.idemp = id;

            //var ID = int.Parse(id);
            var com = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(m => m.ID == ID);
            var Committe_Activities = new Committe_Activities { Committe_Resolution_Recuirtment = com, ID = com.ID };
            return View(Committe_Activities);

        }
        [HttpPost]
        public ActionResult Create(FormCollection form, Committe_Activities model, string command, string id2)
        {
            try
            {

                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(a => a.ID == model.ID).Code;
                ViewBag.idemp = id2;

                var SubjectCode = form["SubjectCode"].Split(char.Parse(","));
                var StartDate = form["StartDate"].Split(char.Parse(","));
                var EndDate = form["EndDate"].Split(char.Parse(","));
                var SubjectDescription = form["SubjectDescription"].Split(char.Parse(","));

                var items = new List<Committe_Activities>();
                for (var i = 0; i < SubjectCode.Count(); i++)
                {
                    if (SubjectCode[i] != "" && SubjectDescription[i] != "" && StartDate[i] != "" && EndDate[i] != "")
                    {
                        items.Add(new Committe_Activities { Committe_Resolution_RecuirtmentId = int.Parse(id2), ActivitySerialNumber = SubjectCode[i], Committe_Resolution_Status =SubjectDescription[i], Planned_Date = DateTime.Parse(StartDate[i]), Actual_Date = DateTime.Parse(EndDate[i]) });
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.Committe_Activities.AddRange(items);
                    dbcontext.SaveChanges();
                    /////////////////////////////////////
                    if (command == "Submit")
                    {
                        return RedirectToAction("Edit", "Committe_Resolution_Recuirtment", new { id = id2 });
                    }
                }
                return RedirectToAction("Index", "Committe_Resolution_Recuirtment", new { id = id2 });
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
        public ActionResult Edit(string id)
        {
            try
            {
                var ID = int.Parse(id);

                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(a => a.ID == ID).Code;
                var record = dbcontext.Committe_Activities.FirstOrDefault(m => m.Committe_Resolution_RecuirtmentId == ID);
              
                if (record != null)
                {
                    ViewBag.idemp = ID;
                    return View(record);

                }
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
        public ActionResult Edit(FormCollection form, Committe_Activities model, string command, string id2)
        {
            try
            {
                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(a => a.ID == model.ID).Code;
                ViewBag.idemp = id2;
                var ID = int.Parse(id2);
                var record = dbcontext.Committe_Activities.Where(m => m.Committe_Resolution_RecuirtmentId == ID);
                dbcontext.Committe_Activities.RemoveRange(record);
                dbcontext.SaveChanges();

                var SubjectCode = form["SubjectCode"].Split(char.Parse(","));
                var StartDate = form["StartDate"].Split(char.Parse(","));
                var EndDate = form["EndDate"].Split(char.Parse(","));
                var SubjectDescription = form["SubjectDescription"].Split(char.Parse(","));

                var items = new List<Committe_Activities>();
                for (var i = 0; i < SubjectCode.Count(); i++)
                {
                    if (SubjectCode[i] != "" && SubjectDescription[i] != "" && StartDate[i] != "" && EndDate[i] != "")
                    {
                        items.Add(new Committe_Activities { Committe_Resolution_RecuirtmentId = int.Parse(id2), ActivitySerialNumber = SubjectCode[i], Committe_Resolution_Status = SubjectDescription[i], Planned_Date = DateTime.Parse(StartDate[i]), Actual_Date = DateTime.Parse(EndDate[i]) });
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.Committe_Activities.AddRange(items);
                    dbcontext.SaveChanges();
                    /////////////////////////////////////
                    if (command == "Submit")
                    {
                        return RedirectToAction("Edit", "Committe_Resolution_Recuirtment", new { id = id2 });
                    }
                }
                return RedirectToAction("Index", "Committe_Resolution_Recuirtment", new { id = id2 });
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
    }
}