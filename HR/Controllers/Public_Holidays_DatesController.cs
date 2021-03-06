﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using HR.Reposatory.Evalutions.reposatory;
using HR.Reposatory.Evalutions.IReposatory;
using HR.Models.Infra;
using System.Data.Entity.Infrastructure;
using HR.Models.ViewModel;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,Vacations,VacationsSetup,Public Holidays Dates")]
    public class Public_Holidays_DatesController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private readonly IStructure reposatorystructure;
        private readonly IPublic_Holidays_Dates reposatoryPublicHolidaysDates;
        private readonly IAppend_Public_Holidays_Dates reposatoryAppend_Public_Holidays_Dates;
        private readonly IPublic_Holiday_Events reposatoryPublic_Holiday_EventsCs;

        // GET: Public_Holidays_Dates
        public Public_Holidays_DatesController()
        {
            reposatoryPublicHolidaysDates = new Public_Holidays_DatesCs(new ApplicationDbContext());
            reposatoryAppend_Public_Holidays_Dates = new Append_Public_Holidays_DatesCs(new ApplicationDbContext());
            reposatoryPublic_Holiday_EventsCs = new Public_Holiday_EventsCs(new ApplicationDbContext());

            reposatorystructure = new Structure(new ApplicationDbContext());
        }
        public ActionResult Index()
        {
            try
            {
                var model = reposatoryPublicHolidaysDates.GetAll();
                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return View(model);
                }
            }
            catch (Exception)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return View();
            }
        }
        public ActionResult Create()
        {
            ViewBag.Public_Holiday_Events = db.Public_Holiday_Events.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });
            ViewBag.locations = db.work_location.ToList().Select(m=>new {ID=m.ID,Code=m.Code+"-->"+m.Name });
            try
            {
                var stru = reposatorystructure.find(ChModels.Personnel).Structure_Code;
                var list = reposatoryPublicHolidaysDates.GetAll();
                var model = new Public_Holidays_Dates();
                if (list.Count() == 0)
                {
                    model.Holidays_Code = stru + "1";
                }
                else
                {
                    model.Holidays_Code = stru+(list.LastOrDefault().ID + 1).ToString();
                }
                return View(model);
            }
            catch (Exception e)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(FormCollection form, Public_Holidays_Dates model ,string command)
        {
            ViewBag.Public_Holiday_Events = db.Public_Holiday_Events.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });
            ViewBag.locations = db.work_location.ToList().Select(m => new { ID = m.ID, Code = m.Code + "-->" + m.Name });

            try
            {
                if (ModelState.IsValid)
                {
                    var flag = reposatoryPublicHolidaysDates.AddOne(model);
                    var loca =new List<work_location>();
                    if (flag == null)
                    {
                        TempData["Message"] = HR.Resource.pers_2.Faild;
                        return View();
                    }
                    else
                    {
                        var ID_5 = form["ID_5"].Split(char.Parse(","));
                        var Fromdate = form["Fromdate"].Split(char.Parse(","));
                        var Todate = form["Todate"].Split(char.Parse(","));
                        var Notes = form["Notes"].Split(char.Parse(","));
                        var Locations = form["locationdown"].Split(char.Parse(",")); 
                        var items = new Append_Public_Holidays_Dates();
                      
                        for(var i=0;i<Locations.Count();i++)
                        {
                            if (Locations[i] != "")
                            {
                                var ID_loc = int.Parse(Locations[i]);
                                var locati = db.work_location.FirstOrDefault(m => m.ID == ID_loc);
                             
                                locati.Public_Holidays_DatesID = flag.ID;
                                db.SaveChanges();
                            }
                        }
                   
                        for (var i = 0; i < ID_5.Count(); i++)
                        {
                           
                            if (ID_5[i] != "")
                            {
                                
                                var obj = reposatoryPublic_Holiday_EventsCs.Find(int.Parse(ID_5[i]));
                                if (obj != null)
                                {
                                    items = new Append_Public_Holidays_Dates { Fromdate = DateTime.Parse(Fromdate[i]), Todate = DateTime.Parse(Todate[i]), Notes = Notes[i], Public_Holiday_EventsId = obj.ID, Public_Holidays_DatesId = flag.ID };
                                    var app = reposatoryAppend_Public_Holidays_Dates.AddOne(items);
                                    if (app == null)
                                    {
                                        TempData["Message"] = HR.Resource.pers_2.Faild;
                                        return View(model);
                                    }
                                }
                               
                                else
                                {
                                    TempData["Message"] = HR.Resource.pers_2.Faild;
                                    return View(model);
                                }
                            }
                        }
                        if (command == "Submit")
                        {
                            return RedirectToAction("link", "Public_Holidays_Dates", new { id = flag.ID });
                        }
                        TempData["Message"] = HR.Resource.pers_2.addedSuccessfully;
                        return RedirectToAction("index");
                    }
                }            
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return View(model);
                }        
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                var ty = db.work_location.ToList();

                ViewBag.Public_Holiday_Events = db.Public_Holiday_Events.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });
                var recode = db.Public_Holidays_Dates.FirstOrDefault(a => a.ID == id);
                if (recode != null)
                {
                    IEnumerable<SelectListItem> categoryList =
                                 from category in ty
                                 select new SelectListItem
                                 {
                                     Text = category.Code+"->"+category.Name,
                                     Value = category.ID.ToString(),
                                     Selected = recode.work_location.Contains(category)
                                 };
                    recode.selectedlocations = categoryList;
                    ViewBag.f = recode.Append_Public_Holidays_Dates.Count();
                    return View(recode);
                }
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
        [HttpPost]
        public ActionResult Edit(Public_Holidays_Dates model, FormCollection form)
        {
            try
            {
                ViewBag.locations = db.work_location.ToList().Select(m => new { ID = m.ID, Code = m.Code + "-->" + m.Name });

                ViewBag.Public_Holiday_Events = db.Public_Holiday_Events.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });
                var record = db.Public_Holidays_Dates.FirstOrDefault(a => a.ID == model.ID);

                record.Holidays_Code = model.Holidays_Code;
                record.Holiday_description = model.Holiday_description;
                record.Holiday_alternative_description = model.Holiday_alternative_description;
                record.Holidaysyear = model.Holidaysyear;
                db.SaveChanges();
                ///////////delete old Append_beneficiary_Family///////
                // var all_items = model.Append_beneficiary_Family;
                db.Append_Public_Holidays_Dates.RemoveRange(record.Append_Public_Holidays_Dates);
                db.SaveChanges();
                /////////////////add new Append_beneficiary_Family////////
                var ID_5 = form["ID_5"].Split(char.Parse(","));
                var Fromdate = form["Fromdate"].Split(char.Parse(","));
                var Todate = form["Todate"].Split(char.Parse(","));
                var Notes = form["Notes"].Split(char.Parse(","));
                var Locations = form["locationdown"].Split(char.Parse(","));
                var items = new Append_Public_Holidays_Dates();
                var locations = db.work_location.Where(m => m.Public_Holidays_DatesID == record.ID);
                record.work_location = null;
                db.SaveChanges();
                //foreach (var item in locations)
                //{
                //    item.Public_Holidays_DatesID = null;
                //    db.SaveChanges();
                //}
                for (var i = 0; i < Locations.Count(); i++)
                {
                    if (Locations[i] != "")
                    {
                        var ID_loc = int.Parse(Locations[i]);
                        var locati = db.work_location.FirstOrDefault(m => m.ID == ID_loc);

                        locati.Public_Holidays_DatesID = model.ID;
                        db.SaveChanges();
                    }
                }

                for (var i = 0; i < ID_5.Count(); i++)
                {
                    if (ID_5[i] != "")
                    {
                        var obj = reposatoryPublic_Holiday_EventsCs.Find(int.Parse(ID_5[i]));
                        if (obj != null)
                        {
                            items = new Append_Public_Holidays_Dates { Fromdate = DateTime.Parse(Fromdate[i]), Todate = DateTime.Parse(Todate[i]), Notes = Notes[i], Public_Holiday_EventsId = obj.ID, Public_Holidays_DatesId = record.ID };
                            var app = reposatoryAppend_Public_Holidays_Dates.AddOne(items);
                            if (app == null)
                            {
                                TempData["Message"] = HR.Resource.pers_2.Faild;
                                return View(model);
                            }
                       
                        }
                        else
                        {
                            TempData["Message"] = HR.Resource.pers_2.addedSuccessfully;
                            return RedirectToAction("index");
                        }
                    }
                }
                TempData["Message"] = HR.Resource.pers_2.addedSuccessfully;
                return RedirectToAction("index");
              
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var Public_Holidays_Dates = db.Public_Holidays_Dates.FirstOrDefault(m => m.ID == id);
                var Append_Public_Holidays_Dates = db.Append_Public_Holidays_Dates.FirstOrDefault(m => m.ID == Public_Holidays_Dates.ID);
                var work_location = db.work_location.FirstOrDefault(m => m.Public_Holidays_DatesID == Public_Holidays_Dates.ID);

                var model = reposatoryPublicHolidaysDates.Find(id);
                if (Public_Holidays_Dates != null)
                { return View(Public_Holidays_Dates); }
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return RedirectToAction("index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_method(int id)
        {
            try
            {
                var Public_Holidays_Dates = db.Public_Holidays_Dates.FirstOrDefault(m => m.ID == id);
                var Append_Public_Holidays_Dates = db.Append_Public_Holidays_Dates.FirstOrDefault(m => m.ID == Public_Holidays_Dates.ID);
                //var flags = reposatoryPublicHolidaysDates.Remove(id);
                for (int i = 0; i < Public_Holidays_Dates.work_location.Count; i++)
                {
                    var work_location = db.work_location.FirstOrDefault(m => m.Public_Holidays_DatesID == Public_Holidays_Dates.ID);
                    work_location.Public_Holidays_DatesID = null;
                    db.SaveChanges();
                }
               

          
                var flag = db.Public_Holidays_Dates.Remove(Public_Holidays_Dates);
                
                db.SaveChanges();

                if (Append_Public_Holidays_Dates != null)
                {
                    db.Append_Public_Holidays_Dates.Remove(Append_Public_Holidays_Dates);
                }
               
                db.SaveChanges();

                if (flag != null) { TempData["Message"] = HR.Resource.pers_2.removesuccessfully; return RedirectToAction("index"); }
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return View("index");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }


    }
}