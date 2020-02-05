using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models.Infra;
using HR.Models.ViewModel;
using HR.Models;
using System.Data.Entity.Infrastructure;
namespace HR.Controllers
{
    public class Employee_sponsor_profileController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_sponsor_profile
        public ActionResult Index()
        {
            var model = dbcontext.Employee_sponsor_profile.ToList();
            return View(model);
        }
        public ActionResult Create(string id)
        {
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Sponsor = dbcontext.Sponsor.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

            //var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            //var model = dbcontext.Employee_sponsor_profile.ToList();
            //var count = 0;
            //if (model.Count() == 0)
            //{
            //    count = 1;
            //}
            //else
            //{
            //    var te = model.LastOrDefault().ID;
            //    count = te + 1;
            //}
            //if (id != null)
            //{
            //    var ID = int.Parse(id);
            //    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
            //    var x = emp.s;
            //    return View(x);
            //}
            DateTime statis2 = Convert.ToDateTime("1/1/1900");
            var EmployeeSponsor = new Employee_sponsor_profile { Birth_date=statis2,Issue_date=statis2};
            return View(EmployeeSponsor);

        }
        [HttpPost]
        public ActionResult Create(Employee_sponsor_profile model)
        {
            try
            {
              
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Sponsor = dbcontext.Sponsor.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });


                if (ModelState.IsValid)
                {
                    var prof = model.Employee_ProfileId;
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == prof);
                    //           var record = dbcontext.Employee_sponsor_profile.FirstOrDefault(m => m.ID == emp.spon.ID);
                    Employee_sponsor_profile record = new Employee_sponsor_profile();
                    record.Residence_Id = model.Residence_Id;
                    record.Issue_date = model.Issue_date;
                    record.Issue_place = model.Issue_place;
                    record.Birth_date = model.Birth_date;
                    if (model.Birth_date > model.Issue_date)
                    {
                        TempData["Message"] = "Birth date  bigger Issue date ";
                        return View(model);
                    }
                    record.Owner = model.Owner;
                    record.Job = model.Job;
                    record.Nationality = model.Nationality;
                    record.Religin = model.Religin;
                    record.Employee_ProfileId = model.Employee_ProfileId;
                    record.SponsorId = model.SponsorId;
                    record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.Employee_ProfileId);
                    record.Sponsor = dbcontext.Sponsor.FirstOrDefault(m => m.ID == model.SponsorId);

                    dbcontext.Employee_sponsor_profile.Add(record);
                    dbcontext.SaveChanges();
                  
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException e)
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

                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Sponsor = dbcontext.Sponsor.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });


                var record = dbcontext.Employee_sponsor_profile.FirstOrDefault(m => m.ID == id);
                


                if (record != null)
                {
                    return View(record);
                }
                else
                {
                    TempData["Message"] = "There is no Employee Sponsor Profile";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Employee_sponsor_profile model)
        {
            try
            {

           //     var prof = int.Parse(model.Employee_ProfileId);
                //   var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == prof);
                var record = dbcontext.Employee_sponsor_profile.FirstOrDefault(m => m.ID == model.ID);
                var emp = record.Employee_Profile;
                record.Residence_Id = model.Residence_Id;
                record.Issue_date = model.Issue_date;
                record.Issue_place = model.Issue_place;
                record.Birth_date = model.Birth_date;
                if (model.Birth_date > model.Issue_date)
                {
                    TempData["Message"] = "Birth date  bigger Issue date ";
                    return View(model);
                }
                record.Owner = model.Owner;
                record.Job = model.Job;
                record.Nationality = model.Nationality;
                record.Religin = model.Religin;
                record.Employee_ProfileId = model.Employee_ProfileId;
                record.SponsorId = model.SponsorId;
                record.Employee_Profile = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == model.Employee_ProfileId);
                record.Sponsor = dbcontext.Sponsor.FirstOrDefault(m => m.ID == model.SponsorId);
                dbcontext.SaveChanges();

                return RedirectToAction("index");
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = "This code Is already exists";
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            try
            {

                var record = dbcontext.Employee_sponsor_profile.FirstOrDefault(m => m.ID == id);
           
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Employee Sponsor Profile";
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

            var record = dbcontext.Employee_sponsor_profile.FirstOrDefault(m => m.ID == id);
            
            try
            {
                dbcontext.Employee_sponsor_profile.Remove(record);
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
                return View();
            }
        }
    }
}