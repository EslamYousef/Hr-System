using HR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models.Infra;
using HR.Models.ViewModel;
using HR.Models.Application;
using System.Web.WebPages;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,Recuirtment,RecuirtmentCards,Applications Rec")]
    public class Hiring_Check_ListController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Hiring_Check_List
        public ActionResult Edit(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var App = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Applicant_ProfileId;
                var AppId = int.Parse(App);
                ViewBag.ApplicationApp = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == AppId).Full_Name;
                ViewBag.ApplicationCode = dbcontext.Application.FirstOrDefault(a => a.ID == ID).Code;
                var record = dbcontext.Check_List_Items.Where(a => a.Required_on_application == true).ToList();
                ViewBag.Check_List_Items=record.Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
             
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
        public ActionResult Edit(FormCollection form, Check_List_Items model, string Command, string id2)
        {
            try
            {
              
                ViewBag.idemp = id2;
                var ID = int.Parse(id2);
                //var record = dbcontext.Check_List_Items.FirstOrDefault(m => m.ApplicantId == ID);

                //record.ApplicantId = int.Parse(id2);
                //record.Select = model.Select;
                //dbcontext.SaveChanges();

                var Select = form["Select"].Split(char.Parse(","));
                var ItemCode = form["ItemCode"].Split(char.Parse(","));
                var ItemDesc = form["ItemDesc"].Split(char.Parse(","));
               

                var items = new List<Check_List_Items>();
                for (var i = 0; i < ItemCode.Count(); i++)
                {
                    if (Select[i] != "" && ItemCode[i] != "" && ItemDesc[i] != "" )
                    {
                        //items.Add(new Check_List_Items { ApplicantId = int.Parse(id2), Select = Select[i].IsBool() });
                    }
                }
                if (items.Count() > 0)
                {
                    var add_items = dbcontext.Check_List_Items.AddRange(items);
                    dbcontext.SaveChanges();
                    /////////////////////////////////////
                    if (Command == "Submit")
                    {
                        return RedirectToAction("Edit", "Application", new { id = id2 });
                    }
                }
                return RedirectToAction("Index", "Application", new { id = id2 });
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