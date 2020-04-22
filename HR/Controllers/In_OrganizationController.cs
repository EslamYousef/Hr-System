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
    public class In_OrganizationController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: In_Organization
        public ActionResult Create(string id)
        {
            var ID = int.Parse(id);
            ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(a => a.ID == ID).Code;
            ViewBag.job_title_cards = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
            ViewBag.idemp = id;

            //var ID = int.Parse(id);
            var com = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(m => m.ID == ID);
            var In_Organization = new In_Organization { Committe_Resolution_Recuirtment = com, ID = com.ID };
            return View(In_Organization);

        }
        [HttpPost]
        public ActionResult Create(FormCollection form, In_Organization model, string command, string id2)
        {
            try
            {

                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(a => a.ID == model.ID).Code;
                ViewBag.job_title_cards = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.idemp = id2;

                var JobCode = form["JobCode"].Split(char.Parse(","));
                var JobDescription = form["JobDescription"].Split(char.Parse(","));
                var CadreCode = form["CadreCode"].Split(char.Parse(","));
                var CadreDescription = form["CadreDescription"].Split(char.Parse(","));
                var OrgCode = form["OrgCode"].Split(char.Parse(","));
                var OrgDescription = form["OrgDescription"].Split(char.Parse(","));

                var items = new List<In_Organization>();
                for (var i = 0; i < JobCode.Count(); i++)
                {
                    if (JobCode[i] != "" && JobDescription[i] != "" && CadreCode[i] != "" && CadreDescription[i] != "")
                    {
                        items.Add(new In_Organization { Committe_Resolution_RecuirtmentId = int.Parse(id2), Job_Code = JobCode[i], Job_Description = JobDescription[i], Cadre_Code = CadreCode[i], Cadre = CadreDescription[i], Org_Unit_Code = OrgCode[i], Org_Unit_Description = OrgDescription[i] });
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.In_Organization.AddRange(items);
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
                ViewBag.job_title_cards = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                var record = dbcontext.In_Organization.FirstOrDefault(m => m.Committe_Resolution_RecuirtmentId == ID);

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
        public ActionResult Edit(FormCollection form, In_Organization model, string command, string id2)
        {
            try
            {
                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.FirstOrDefault(a => a.ID == model.ID).Code;
                ViewBag.job_title_cards = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.idemp = id2;
                var ID = int.Parse(id2);
                var record = dbcontext.In_Organization.Where(m => m.Committe_Resolution_RecuirtmentId == ID);
                dbcontext.In_Organization.RemoveRange(record);
                dbcontext.SaveChanges();

                var JobCode = form["JobCode"].Split(char.Parse(","));
                var JobDescription = form["JobDescription"].Split(char.Parse(","));
                var CadreCode = form["CadreCode"].Split(char.Parse(","));
                var CadreDescription = form["CadreDescription"].Split(char.Parse(","));
                var OrgCode = form["OrgCode"].Split(char.Parse(","));
                var OrgDescription = form["OrgDescription"].Split(char.Parse(","));

                var items = new List<In_Organization>();
                for (var i = 0; i < JobCode.Count(); i++)
                {
                    if (JobCode[i] != "" && JobDescription[i] != "" && CadreCode[i] != "" && CadreDescription[i] != "")
                    {
                        items.Add(new In_Organization { Committe_Resolution_RecuirtmentId = int.Parse(id2), Job_Code = JobCode[i], Job_Description = JobDescription[i], Cadre_Code = CadreCode[i], Cadre = CadreDescription[i], Org_Unit_Code = OrgCode[i], Org_Unit_Description = OrgDescription[i] });
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.In_Organization.AddRange(items);
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