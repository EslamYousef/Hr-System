﻿using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;


namespace HR.Controllers
{
    [Authorize(Roles = "Admin,Recuirtment,RecuirtmentSetup,Check list item Rec")]
    public class Check_List_ItemsController : BaseController
    {
        // GET: Check_List_Items
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Country
        public ActionResult Index()
        {
            var model = dbcontext.Check_List_Items.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            var modell = new Check_List_Items();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Recuirtment).Structure_Code;
            var model = dbcontext.Check_List_Items.ToList();
            if (model.Count() == 0)
            {
                modell.Code = stru + "1";
            }
            else
            {
                modell.Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Check_List_Items model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Check_List_Items record = new Check_List_Items();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    record.Required_on_application = model.Required_on_application;
                   dbcontext.Check_List_Items.Add(record);
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
                var record = dbcontext.Check_List_Items.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no Data";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Check_List_Items model)
        {
            try
            {
                var record = dbcontext.Check_List_Items.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Code = model.Code;
                record.Required_on_application = model.Required_on_application;
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
                var record = dbcontext.Check_List_Items.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Check_List_Items.FirstOrDefault(m => m.ID == id);

            try
            {
                dbcontext.Check_List_Items.Remove(record);
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
                return View(record);
            }
        }
    }
}