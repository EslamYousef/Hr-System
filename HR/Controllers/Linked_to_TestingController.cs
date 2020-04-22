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
    public class Linked_to_TestingController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Linked_to_Testing
        public ActionResult Create(string id)
        {
            var ID = int.Parse(id);
            ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(a => a.ID == ID).Code;
            ViewBag.Test = dbcontext.Test.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;

            //var ID = int.Parse(id);
            var com = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(m => m.ID == ID);
            var Linked_to_Testing = new Linked_to_Testing { Committe_Resolution_Recuirtment = com, ID = com.ID };
            return View(Linked_to_Testing);

        }
        [HttpPost]
        public ActionResult Create(FormCollection form, Linked_to_Testing model, string command, string id2)
        {
            try
            {

                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(a => a.ID == model.ID).Code;
                ViewBag.Test = dbcontext.Test.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = id2;

                var TestCode = form["TestCode"].Split(char.Parse(","));
                var TestDescription = form["TestDescription"].Split(char.Parse(","));
                var ExpectedStart_Date = form["ExpectedStart_Date"].Split(char.Parse(","));
                var ExpectedEnd_Date = form["ExpectedEnd_Date"].Split(char.Parse(","));
                var PassMark = form["PassMark"].Split(char.Parse(","));
                var FullMark = form["FullMark"].Split(char.Parse(","));

                var items = new List<Linked_to_Testing>();
                for (var i = 0; i < TestCode.Count(); i++)
                {
                    if (TestCode[i] != "" && TestDescription[i] != "" && ExpectedStart_Date[i] != "" && ExpectedEnd_Date[i] != "" && PassMark[i] != "" && FullMark[i] != "")
                    {
                        items.Add(new Linked_to_Testing { Committe_Resolution_RecuirtmentId = int.Parse(id2), TestCode = TestCode[i], TestDescription = TestDescription[i], Expected_Start_Date = DateTime.Parse(ExpectedStart_Date[i]), Expected_End_Date = DateTime.Parse(ExpectedEnd_Date[i]), Pass_Mark = int.Parse(PassMark[i]), Full_Mark = int.Parse(FullMark[i]) });
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.Linked_to_Testing.AddRange(items);
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
                ViewBag.Test = dbcontext.Test.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Linked_to_Testing.FirstOrDefault(m => m.Committe_Resolution_RecuirtmentId == ID);

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
        public ActionResult Edit(FormCollection form, Linked_to_Testing model, string command, string id2)
        {
            try
            {
                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(a => a.ID == model.ID).Code;
                ViewBag.Test = dbcontext.Test.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.idemp = id2;
                var ID = int.Parse(id2);
                var record = dbcontext.Linked_to_Testing.Where(m => m.Committe_Resolution_RecuirtmentId == ID);
                dbcontext.Linked_to_Testing.RemoveRange(record);
                dbcontext.SaveChanges();

                var TestCode = form["TestCode"].Split(char.Parse(","));
                var TestDescription = form["TestDescription"].Split(char.Parse(","));
                var ExpectedStart_Date = form["ExpectedStart_Date"].Split(char.Parse(","));
                var ExpectedEnd_Date = form["ExpectedEnd_Date"].Split(char.Parse(","));
                var PassMark = form["PassMark"].Split(char.Parse(","));
                var FullMark = form["FullMark"].Split(char.Parse(","));

                var items = new List<Linked_to_Testing>();
                for (var i = 0; i < TestCode.Count(); i++)
                {
                    if (TestCode[i] != "" && TestDescription[i] != "" && ExpectedStart_Date[i] != "" && ExpectedEnd_Date[i] != "" && PassMark[i] != "" && FullMark[i] != "")
                    {
                        items.Add(new Linked_to_Testing { Committe_Resolution_RecuirtmentId = int.Parse(id2), TestCode = TestCode[i], TestDescription = TestDescription[i], Expected_Start_Date = DateTime.Parse(ExpectedStart_Date[i]), Expected_End_Date = DateTime.Parse(ExpectedEnd_Date[i]), Pass_Mark = int.Parse(PassMark[i]), Full_Mark = int.Parse(FullMark[i]) });
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.Linked_to_Testing.AddRange(items);
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