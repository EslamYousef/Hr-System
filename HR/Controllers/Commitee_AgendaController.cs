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
    [Authorize]
    public class Commitee_AgendaController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Commitee_Agenda
        public ActionResult Create(string id)
        {
            var ID = int.Parse(id);
            ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(a => a.ID == ID).Code;
            ViewBag.Committe_subjects = dbcontext.Committe_subjects.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;
         
            //var ID = int.Parse(id);
            var com = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(m => m.ID == ID);
            var Commitee_Agenda = new Commitee_Agenda { Committe_Resolution_Recuirtment = com, ID = com.ID };
            return View(Commitee_Agenda);

        }
        [HttpPost]
        public ActionResult Create(FormCollection form, Commitee_Agenda model, string command,string id2)
        {
            try
            {

                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(a => a.ID == model.ID).Code;
                ViewBag.Committe_subjects = dbcontext.Committe_subjects.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = id2;

                var SubjectCode = form["SubjectCode"].Split(char.Parse(","));
                var SubjectDescription = form["SubjectDescription"].Split(char.Parse(","));
                var StartDate = form["StartDate"].Split(char.Parse(","));
                var EndDate = form["EndDate"].Split(char.Parse(","));
                var Notes = form["Notes"].Split(char.Parse(","));

                var items = new List<Commitee_Agenda>();
                for (var i = 0; i < SubjectCode.Count(); i++)
                {
                    if (SubjectCode[i] != "" && SubjectDescription[i] != "" && StartDate[i] != "" && EndDate[i] != "")
                    {
                        items.Add(new Commitee_Agenda { Committe_Resolution_RecuirtmentId= int.Parse(id2) , SubjectCode =int.Parse(SubjectCode[i]), SubjectDescription = SubjectDescription[i], Start_Date = DateTime.Parse(StartDate[i]), End_Date = DateTime.Parse(EndDate[i]), Notes = Notes[i] ,});
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.Commitee_Agenda.AddRange(items);
                    dbcontext.SaveChanges();
                    /////////////////////////////////////
                    if (command == "Submit")
                    {
                        return RedirectToAction("Edit", "Committe_Resolution_Recuirtment", new { id =  id2});
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
                ViewBag.Committe_subjects = dbcontext.Committe_subjects.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Commitee_Agenda.FirstOrDefault(m => m.Committe_Resolution_RecuirtmentId == ID);

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
        public ActionResult Edit(FormCollection form, Commitee_Agenda model, string command,string id2)
        {
            try
            {
                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(a => a.ID == model.ID).Code;
                ViewBag.Committe_subjects = dbcontext.Committe_subjects.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = id2;
                var ID = int.Parse(id2);
                var record = dbcontext.Commitee_Agenda.Where(m => m.Committe_Resolution_RecuirtmentId == ID);
                dbcontext.Commitee_Agenda.RemoveRange(record);
                dbcontext.SaveChanges();

                var SubjectCode = form["SubjectCode"].Split(char.Parse(","));
                var SubjectDescription = form["SubjectDescription"].Split(char.Parse(","));
                var StartDate = form["StartDate"].Split(char.Parse(","));
                var EndDate = form["EndDate"].Split(char.Parse(","));
                var Notes = form["Notes"].Split(char.Parse(","));

                var items = new List<Commitee_Agenda>();
                for (var i = 0; i < SubjectCode.Count(); i++)
                {
                    if (SubjectCode[i] != "" && SubjectDescription[i] != "" && StartDate[i] != "" && EndDate[i] != "")
                    {
                        items.Add(new Commitee_Agenda { Committe_Resolution_RecuirtmentId = int.Parse(id2), SubjectCode = int.Parse(SubjectCode[i]), SubjectDescription = SubjectDescription[i], Start_Date = DateTime.Parse(StartDate[i]), End_Date = DateTime.Parse(EndDate[i]), Notes = Notes[i], });
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.Commitee_Agenda.AddRange(items);
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