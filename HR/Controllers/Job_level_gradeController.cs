using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize(Roles = "Admin,Organization,OrganizationSetup,job level")]
    public class Job_level_gradeController : BaseController
    {
        // GET: Job_level_grade
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Country
        public ActionResult Index()
        {
            var model = dbcontext.Job_level_gradee.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new Job_level_grade();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Organization).Structure_Code;
            var model = dbcontext.Job_level_gradee.ToList();
            if (model.Count() == 0)
            {
                modell.Code = stru + "1";
            }
            else
            {
                modell.Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(Job_level_grade model,string command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Job_level_grade record = new Job_level_grade();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Code = model.Code;
                    record.max_annual_increase_percentage = model.max_annual_increase_percentage;
                    record.max_basic_salary = model.max_basic_salary;
                    record.max_incentive_amount = model.max_incentive_amount;
                    record.max_incentive_percentage = model.max_incentive_percentage;
                    record.max_monthly_allowance = model.max_monthly_allowance;
                    record.mid_basic_salary = model.mid_basic_salary;
                    record.min_basic_salary = model.min_basic_salary;
                    record.min_working_years = model.min_working_years;
                    record.representation_allowance_value = model.representation_allowance_value;
                    record.dedicated_allowence = model.dedicated_allowence;
                  var Job_level_grade=  dbcontext.Job_level_gradee.Add(record);
                    dbcontext.SaveChanges();
                    //////
                    //var special = new Special_Allwonce_History();
                    //special.selectedID = Job_level_grade.ID;
                    //special.Job_level_grade = Job_level_grade;
                    //special.type_allowance = type_allowance.job_levle_grade;
                    //var spe = dbcontext.Special_Allwonce_History.Add(special);
                    //dbcontext.SaveChanges();
                    /////
                    if (command == "Submit")
                    {
                        return RedirectToAction("allowance", "Job_level_class", new { id = record.ID,type=type_allowance.job_levle_grade });
                    }
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
                var record = dbcontext.Job_level_gradee.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Job_level_grade model,string command)
        {
            try
            {
                var record = dbcontext.Job_level_gradee.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Code = model.Code;
                record.max_annual_increase_percentage = model.max_annual_increase_percentage;
                record.max_basic_salary = model.max_basic_salary;
                record.max_incentive_amount = model.max_incentive_amount;
                record.max_incentive_percentage = model.max_incentive_percentage;
                record.max_monthly_allowance = model.max_monthly_allowance;
                record.mid_basic_salary = model.mid_basic_salary;
                record.min_basic_salary = model.min_basic_salary;
                record.min_working_years = model.min_working_years;
                record.representation_allowance_value = model.representation_allowance_value;
                record.dedicated_allowence = model.dedicated_allowence;
                dbcontext.SaveChanges();
                var spe = dbcontext.Special_Allwonce_History.FirstOrDefault(m => m.selectedID == record.ID&&m.type_allowance==type_allowance.job_levle_grade);
                if (command == "Submit")
                {
                    return RedirectToAction("allowance", "Job_level_class", new { id = record.ID,type=type_allowance.job_levle_grade });
                };
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
                var record = dbcontext.Job_level_gradee.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Job_level_gradee.FirstOrDefault(m => m.ID == id);
            try
            {
            
                dbcontext.Job_level_gradee.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            try
            {
                var record = dbcontext.Job_level_gradee.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Details(Job_level_grade model, string command)
        {
            if(command=="edit")
            {
                return RedirectToAction("Edit", new { id = model.ID });
            }
            else if(command=="delete")
            {
                return RedirectToAction("Delete", new { id=model.ID});
            }
            else if(command=="back")
            {
                return RedirectToAction("index");
            }
            if (command == "Submit")
            {
                return RedirectToAction("allowance", "Job_level_class", new { id = model.ID, type = type_allowance.job_levle_grade });
            }
            else
            {
                return View();
            }
        }


    }
}