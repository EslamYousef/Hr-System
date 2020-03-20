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
    [Authorize]
    public class Job_level_classController : BaseController
    {
        // GET: Job_level_class

        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Country
        public ActionResult Index()
        {
            var model = dbcontext.Job_level_class.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            //////
            var modell = new Job_level_class();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Organization).Structure_Code;
            var model = dbcontext.Job_level_class.ToList();
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
        public ActionResult Create(Job_level_class model,string command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Job_level_class record = new Job_level_class();
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
                    var Job_level_class= dbcontext.Job_level_class.Add(record);
                    dbcontext.SaveChanges();
                    //////
                    //var special = new Special_Allwonce_History();
                    //special.selectedID = Job_level_class.ID;
                    //special.Job_level_class = Job_level_class;
                    //special.type_allowance = type_allowance.job_level_class;
                    //var spe = dbcontext.Special_Allwonce_History.Add(special);
                    //dbcontext.SaveChanges();
                    /////
                    if (command == "Submit")
                    {
                        return RedirectToAction("allowance", new { id = Job_level_class.ID ,type=type_allowance.job_level_class});
                    };
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
                var record = dbcontext.Job_level_class.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Job_level_class model,string command)
        {
            try
            {
                var record = dbcontext.Job_level_class.FirstOrDefault(m => m.ID == model.ID);
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
                var spe = dbcontext.Special_Allwonce_History.FirstOrDefault(m => m.selectedID == record.ID&&m.type_allowance==type_allowance.job_level_class);
                if (command == "Submit")
                {
                    return RedirectToAction("allowance", new { id = record.ID,type=1 });
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
                var record = dbcontext.Job_level_class.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Job_level_class.FirstOrDefault(m => m.ID == id);
            try
            {
             
                dbcontext.Job_level_class.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(record);
            }
            catch (Exception e)
            {
                return View(record);
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var record = dbcontext.Job_level_class.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Details(Job_level_class model, string command)
        {
            if (command == "edit")
            {
                return RedirectToAction("Edit", new { id = model.ID });
            }
            else if (command == "delete")
            {
                return RedirectToAction("Delete", new { id = model.ID });
            }
            else if (command == "back")
            {
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult allowance(string id,type_allowance type)
        {
            try
            {
                var ID = int.Parse(id);
                var list = new List<special_allowance_job_level_class>();
                if((int)type==2)
                {
                    var model1 = dbcontext.special_allowance_job_level_grade.Where(m => m.Job_level_gradeID == ID).ToList();
                    foreach(var item in model1)
                    {
                        list.Add(new special_allowance_job_level_class { Year = item.Year, Allowance_amount = item.Allowance_amount, Month = item.Month, Percentage = item.Percentage, new_basic_sallary = item.new_basic_sallary, pervious_basic = item.pervious_basic });
                    }
                   
                }
               else if ((int)type == 1)
                {
                    var model1 = dbcontext.special_allowance_job_level_class.Where(m => m.Job_level_classID == ID).ToList();
                    foreach (var item in model1)
                    {
                        list.Add(new special_allowance_job_level_class { Year = item.Year, Allowance_amount = item.Allowance_amount, Month = item.Month, Percentage = item.Percentage, new_basic_sallary = item.new_basic_sallary, pervious_basic = item.pervious_basic });
                    }

                }
                else if ((int)type == 3)
                {
                    var model1 = dbcontext.special.Where(m => m.job_level_setupID == ID).ToList();
                    foreach (var item in model1)
                    {
                        list.Add(new special_allowance_job_level_class { Year = item.Year, Allowance_amount = item.Allowance_amount, Month = item.Month, Percentage = item.Percentage, new_basic_sallary = item.new_basic_sallary, pervious_basic = item.pervious_basic });
                    }

                }

                TempData["TYPE"] = (int)type;
                TempData["ID"] = ID;
                return View(list);
            }
            catch(Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult allowance(FormCollection form,int type,int ID)
        {
            try
            {
                TempData["TYPE"] = type;
                TempData["ID"] = ID;
                if (type == 1)
                {
                    var specialold = dbcontext.special_allowance_job_level_class.Where(m => m.Job_level_classID == ID);
                    dbcontext.special_allowance_job_level_class.RemoveRange(specialold);
                    dbcontext.SaveChanges();
                }
                else if (type == 2)
                {
                    var specialold = dbcontext.special_allowance_job_level_grade.Where(m => m.Job_level_gradeID == ID);
                    dbcontext.special_allowance_job_level_grade.RemoveRange(specialold);
                    dbcontext.SaveChanges();
                }
                else if (type == 3)
                {
                    var specialold = dbcontext.special.Where(m => m.job_level_setupID == ID);
                    dbcontext.special.RemoveRange(specialold);
                    dbcontext.SaveChanges();
                }
                var Year = form["Year"].Split(char.Parse(","));
                var Month = form["Month"].Split(char.Parse(","));
                var Percentage = form["Percentage"].Split(char.Parse(","));
                var Allowance_amount = form["Allowance_amount1"].Split(char.Parse(","));
                var pervious_basic = form["pervious_basic"].Split(char.Parse(","));
                var new_basic_sallary = form["new_basic_sallary"].Split(char.Parse(","));
                for(var i=0;i<Year.Count();i++)
                {
                    if (type == 1)
                    {
                        var new_record = new special_allowance_job_level_class();
                        if (Year[i] != "" || Month[i] != "" || Percentage[i] != "" || pervious_basic[i] != "" || new_basic_sallary[i] != "")
                        {
                            new_record.Year = float.Parse(Year[i]);
                            new_record.Month = float.Parse(Month[i]);
                            new_record.Percentage = float.Parse(Percentage[i]);
                            new_record.pervious_basic = float.Parse(pervious_basic[i]);
                            new_record.new_basic_sallary = float.Parse(new_basic_sallary[i]);
                            new_record.Allowance_amount = float.Parse(Allowance_amount[i]);
                            new_record.Job_level_classID = ID;
                            dbcontext.special_allowance_job_level_class.Add(new_record);
                            dbcontext.SaveChanges();
                        }
                    }
                    else if (type == 2)
                    {
                        var new_record = new special_allowance_job_level_grade();
                        if (Year[i] != "" || Month[i] != "" || Percentage[i] != "" || pervious_basic[i] != "" || new_basic_sallary[i] != "")
                        {
                            new_record.Year = float.Parse(Year[i]);
                            new_record.Month = float.Parse(Month[i]);
                            new_record.Percentage = float.Parse(Percentage[i]);
                            new_record.pervious_basic = float.Parse(pervious_basic[i]);
                            new_record.new_basic_sallary = float.Parse(new_basic_sallary[i]);
                            new_record.Allowance_amount = float.Parse(Allowance_amount[i]);
                            new_record.Job_level_gradeID = ID;
                            dbcontext.special_allowance_job_level_grade.Add(new_record);
                            dbcontext.SaveChanges();
                        }
                    }
                    else if (type == 3)
                    {
                        var new_record = new special();
                        if (Year[i] != "" || Month[i] != "" || Percentage[i] != "" || pervious_basic[i] != "" || new_basic_sallary[i] != "")
                        {
                            new_record.Year = float.Parse(Year[i]);
                            new_record.Month = float.Parse(Month[i]);
                            new_record.Percentage = float.Parse(Percentage[i]);
                            new_record.pervious_basic = float.Parse(pervious_basic[i]);
                            new_record.new_basic_sallary = float.Parse(new_basic_sallary[i]);
                            new_record.Allowance_amount = float.Parse(Allowance_amount[i]);
                            new_record.job_level_setupID = ID;
                            dbcontext.special.Add(new_record);
                            dbcontext.SaveChanges();
                        }
                    }
                }

               
              if(type==2)
                    return RedirectToAction("index", "Job_level_grade");
              else if (type==3)
                    return RedirectToAction("index", "job_level_setup");
              else
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                TempData["TYPE"] = type;
                TempData["ID"] = ID;
                return View();
            }
        }


    }
}