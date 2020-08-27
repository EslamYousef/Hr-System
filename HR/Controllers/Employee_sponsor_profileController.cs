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
    [Authorize(Roles = "Admin,personnel,personnelCards,Employee Profile")]
    public class Employee_sponsor_profileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_sponsor_profile
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Employee_sponsor_profile.Where(m => m.Employee_Profile.ID == ID).ToList();
            var record = dbcontext.Employee_sponsor_profile.FirstOrDefault(m => m.ID == ID);
            ViewBag.idemp = id;

            return View(new_model);
        }
        public ActionResult Create(string id)
        {
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Sponsor = dbcontext.Sponsor.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;

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
            var ID = int.Parse(id);
            var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
            var EmployeeSponsor = new Employee_sponsor_profile { Employee_Profile = emp, Employee_ProfileId = emp.ID/*, Code = stru.Structure_Code + count.ToString()*/, Birth_date =statis2,Issue_date=statis2};
            return View(EmployeeSponsor);

        }
        [HttpPost]
        public ActionResult Create(Employee_sponsor_profile model , string command)
        {
            try
            {
              
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Sponsor = dbcontext.Sponsor.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                ViewBag.idemp = model.Employee_ProfileId;

                //if (ModelState.IsValid)
                //{
                    var EmpObj = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.Employee_Profile.ID);

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
                    if (command == "Submit")
                    {
                        return RedirectToAction("edit", "Employee_Profile", new { id = EmpObj.ID });//int.Parse(record.Employee_ProfileId)
                    }
                    return RedirectToAction("Index", new { id = EmpObj.ID });
                //}
                //else
                //{
                //    return View(model);
                //}
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
                ViewBag.idemp = record.Employee_Profile.ID.ToString();



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
        public ActionResult Edit(Employee_sponsor_profile model ,string command)
        {
            try
            {
                ViewBag.idemp = model.Employee_ProfileId;
                var EmpObj = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.Employee_Profile.ID);

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
                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Employee_Profile", new { id = EmpObj.ID });
                }
                return RedirectToAction("index", new { id = EmpObj.ID });
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
                ViewBag.idemp = record.Employee_Profile.ID.ToString();

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
            ViewBag.idemp = record.Employee_Profile.ID.ToString();

            try
            {
                dbcontext.Employee_sponsor_profile.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index", new { id = record.Employee_ProfileId });
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