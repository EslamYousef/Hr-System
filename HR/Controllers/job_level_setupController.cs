using HR.Models;
using HR.Models.Infra;
using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class job_level_setupController : Controller
    {
        // GET: job_level_setup
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Create()
        {
            try
            {
                var model = new job_level_setup();
                ViewBag.level_code=dbcontext.Job_level_class.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.level_grade=dbcontext.Job_level_gradee.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.eduction = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.report = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });



                //////
                //var modell = new job_level_setup();

                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Organization).Structure_Code;
                var modelll = dbcontext.job_level_setup.ToList();
                if (modelll.Count() == 0)
                {
                    model.Code = stru + "1";
                }
                else
                {
                    model.Code = stru + (modelll.LastOrDefault().ID + 1).ToString();
                }
                /////

                return View(model);

            }
            catch(Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create(job_level_setup model,string command,FormCollection form)
        {
            try
            {
                ViewBag.eduction = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.level_code = dbcontext.Job_level_class.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.level_grade = dbcontext.Job_level_gradee.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.report = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                if (ModelState.IsValid)
                {
                   var record = new job_level_setup();
                    record.Code = model.Code;
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Job_level_class__ID = model.Job_level_class__ID;
                    var IDclass = int.Parse(model.Job_level_class__ID);
                    record.Job_level_class = dbcontext.Job_level_class.FirstOrDefault(m => m.ID == IDclass);
                    record.Job_level_gradeI__D = model.Job_level_gradeI__D;
                    var IDgrade = int.Parse(model.Job_level_gradeI__D);
                    record.Job_level_grade = dbcontext.Job_level_gradee.FirstOrDefault(m => m.ID == IDgrade);
                    record.report_job_levelID = model.report_job_levelID;
                    //var IDreport = int.Parse(model.report_job_levelID);
                    //record.report_job_level = dbcontext.Job_level_grade.FirstOrDefault(m => m.ID == IDreport);
                    ////////////////////////////
                    record.Sequence_number = model.Sequence_number;
                    record.max_annual_increase_percentage = model.max_annual_increase_percentage;
                    record.max_basic_salary = model.max_basic_salary;
                    record.max_incentive_amount = model.max_incentive_amount;
                    record.max_incentive_percentage = model.max_incentive_percentage;
                    record.max_monthly_allowance = model.max_monthly_allowance;
                    record.mid_basic_salary = model.mid_basic_salary;
                    record.min_basic_salary = model.min_basic_salary;
                    record.min_working_years = model.min_working_years;
                    record.dedicated_allowence = model.dedicated_allowence;
                    record.representation_allowance_value = model.representation_allowance_value;
                    /////////////////////////////
                    record.Calculate_added_experience_years = model.Calculate_added_experience_years;
                    record.Calculate_added_military_years = model.Calculate_added_military_years;
                    record.Calculate_extra_qualification_years = model.Calculate_extra_qualification_years;
                    ////////////////////////////
                    record.Notes = model.Notes;
                    //////////////////////////////////
                    /////////////////////////////////
                    List<string> educationid = new List<string>();
                    if (form.AllKeys.Count() > 24)//////////////////////////////////////ممكن تبقى احسن من كده
                    {
                        var idedu = form["codeid"].Split(char.Parse(","));
                        var num = form["num"].Split(char.Parse(","));
                        IList<job_level_education> job_level_education = new List<Models.ViewModel.job_level_education>();
                        for (var item = 0; item < idedu.Count(); item++)
                        {
                            var id = int.Parse(idedu[item]);
                            var modell = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == id);
                            job_level_education.Add(new job_level_education { Educate_Title = modell, number_of_years_requires = int.Parse(num[item]) });

                        }

                        var job_edu = dbcontext.job_level_education.AddRange(job_level_education).ToList();
                        dbcontext.SaveChanges();
                        foreach(var item in job_edu)
                        {
                            educationid.Add(item.ID.ToString());
                        }
                        record.job_level_education = job_edu;
                        record.job_level_educationID = educationid;
                    }
                    ////////////////////////////////
                    ////////////////////////////////
                    record.Organization_Unit_Type = new List<Organization_Unit_Type>();
                    record.Organization_Unit_TypeID = new List<string>();
                   var modelll=dbcontext.job_level_setup.Add(record);
                    dbcontext.SaveChanges();
                    //////
                    var special = new Special_Allwonce_History();
                    special.selectedID = modelll.ID;
                    special.job_level_setup = modelll;
                    special.type_allowance = type_allowance.job_level_card;
                    var spe = dbcontext.Special_Allwonce_History.Add(special);
                    dbcontext.SaveChanges();
                    /////
                    if (command== "Submit")
                    {
                        TempData["model"] = modelll;
                        return RedirectToAction("Link",new { id=modelll.ID});
                    }
                    if (command == "Submit2")
                    {
                        return RedirectToAction("allowance", "Job_level_class", new { id = spe.ID, type = type_allowance.job_level_card });
                    };
                    return RedirectToAction("index");
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
                return View();
            }
        }

        public ActionResult Edit(string id)
        {
            try
            {
                var ID = int.Parse(id);
                ViewBag.eduction = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.report = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var model = dbcontext.job_level_setup.FirstOrDefault(m=>m.ID==ID);
                ViewBag.level_code = dbcontext.Job_level_class.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.level_grade = dbcontext.Job_level_gradee.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                return View(model);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Edit(job_level_setup model, string command,FormCollection form)
        {
            try
            {
                ViewBag.eduction = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.level_code = dbcontext.Job_level_class.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.level_grade = dbcontext.Job_level_gradee.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.report = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                
               
                    var record = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == model.ID);
                model.job_level_education = record.job_level_education;
                model.job_level_educationID = record.job_level_educationID;
                    record.Code = model.Code;
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Job_level_class__ID = model.Job_level_class__ID;
                    var IDclass = int.Parse(model.Job_level_class__ID);
                    record.Job_level_class = dbcontext.Job_level_class.FirstOrDefault(m => m.ID == IDclass);
                    record.Job_level_gradeI__D = model.Job_level_gradeI__D;
                    var IDgrade = int.Parse(model.Job_level_gradeI__D);
                    record.Job_level_grade = dbcontext.Job_level_gradee.FirstOrDefault(m => m.ID == IDgrade);
                    record.report_job_levelID = model.report_job_levelID;
                    //var IDreport = int.Parse(model.report_job_levelID);
                    //record.report_job_level = dbcontext.Job_level_grade.FirstOrDefault(m => m.ID == IDreport);
                    ////////////////////////////
                    record.Sequence_number = model.Sequence_number;
                    record.max_annual_increase_percentage = model.max_annual_increase_percentage;
                    record.max_basic_salary = model.max_basic_salary;
                    record.max_incentive_amount = model.max_incentive_amount;
                    record.max_incentive_percentage = model.max_incentive_percentage;
                    record.max_monthly_allowance = model.max_monthly_allowance;
                    record.mid_basic_salary = model.mid_basic_salary;
                    record.min_basic_salary = model.min_basic_salary;
                    record.min_working_years = model.min_working_years;
                    record.dedicated_allowence = model.dedicated_allowence;
                    record.representation_allowance_value = model.representation_allowance_value;
                    /////////////////////////////
                    record.Calculate_added_experience_years = model.Calculate_added_experience_years;
                    record.Calculate_added_military_years = model.Calculate_added_military_years;
                    record.Calculate_extra_qualification_years = model.Calculate_extra_qualification_years;
                    ////////////////////////////
                    record.Notes = model.Notes;
                    //////////////////////////////////
                    /////////////////////////////////
                    var jobs=record.job_level_education.ToList();
                    var count = jobs.Count();
                    for (var item = 0;item<count;item++)
                    {
                        record.job_level_education[item] = null;
                        dbcontext.SaveChanges();
                    }
                    dbcontext.job_level_education.RemoveRange(jobs);
                    dbcontext.SaveChanges();
                    ///////////////////////////////
                    ///////////////////////////////
                    if (form.AllKeys.Count()>24)///////////////////////////////////ممكن تبقى احسن من كده
                    {
                        var idedu = form["codeid"].Split(char.Parse(","));
                        var num = form["num"].Split(char.Parse(","));
                        IList<job_level_education> job_level_education = new List<Models.ViewModel.job_level_education>();
                        for (var item = 0; item < idedu.Count(); item++)
                        {
                            var id = int.Parse(idedu[item]);
                            var modell = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == id);
                            job_level_education.Add(new job_level_education { Educate_Title = modell, number_of_years_requires = int.Parse(num[item]) });

                        }
                        var job_edu = dbcontext.job_level_education.AddRange(job_level_education).ToList();
                        dbcontext.SaveChanges();
                        record.job_level_education = job_edu;
                    }
                    ////////////////////////////////
                    ////////////////////////////////
                    ////////////////////////////////
                    //record.Organization_Unit_Type = new List<Organization_Unit_Type>();
                    //record.Organization_Unit_TypeID = new List<string>();
                    //var modelll = dbcontext.job_level_setup.Add(record);
                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("Link", new { id = model.ID });
                    }

                    var spe = dbcontext.Special_Allwonce_History.FirstOrDefault(m => m.selectedID == record.ID&& m.type_allowance==type_allowance.job_level_card);
                    if (command == "Submit2")
                    {
                        return RedirectToAction("allowance", "Job_level_class", new { id = spe.ID, type = type_allowance.job_level_card });
                    };
                    return RedirectToAction("index");
                
              
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
        public ActionResult Link(int id)
        {
            job_level_setup modell = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == id);
            try
            {

                    var all_unit = dbcontext.Organization_Unit_Type.ToList();
                    ViewBag.id = modell.ID;
                    List<organization_type_vm> mm = new List<organization_type_vm>();
                    foreach (var team in all_unit)
                    {
                        if (modell.Organization_Unit_Type.Any(ma => ma.ID == team.ID))
                        {
                            mm.Add(new organization_type_vm { Educate_Title = team, selected = true, job_level_setup_id = modell.ID });

                        }
                        else
                        {
                            mm.Add(new organization_type_vm { Educate_Title = team, selected = false, job_level_setup_id = modell.ID });
                        }
                    }
                
                    return View(mm);
            }
            catch (Exception e)
            {
                return View();
            }

           
        }
        [HttpPost]

        public ActionResult Link(FormCollection form, int id)
        {
            try
            {
                ViewBag.id = id;
                var record = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == id);
                List<Organization_Unit_Type> organization = new List<Organization_Unit_Type>();
                List<string> orgid = new List<string>();
                for (var item = 0; item < record.Organization_Unit_Type.Count(); item++)
                {
                    record.Organization_Unit_Type[item] = null;
                    dbcontext.SaveChanges();
                }
                record.Organization_Unit_TypeID = null;
                dbcontext.SaveChanges();
                if (form.AllKeys.Count()>0)
                {
                    var checkedd = form["Selected"].Split(char.Parse(","));
                    foreach (var item in checkedd)
                    {
                        var ID = int.Parse(item);
                        var org = dbcontext.Organization_Unit_Type.FirstOrDefault(m => m.ID == ID);
                        organization.Add(org);
                        orgid.Add(org.ID.ToString());
                    }
                    record.Organization_Unit_Type = organization;
                    record.Organization_Unit_TypeID = orgid;
                    dbcontext.SaveChanges();
                }
                else
                {
                    
                    record.Organization_Unit_TypeID = null;
                    dbcontext.SaveChanges();
                }
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult index()
        {
            var model = dbcontext.job_level_setup.ToList();
            return View(model);
        }
        public ActionResult Delete(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var reco = dbcontext.job_level_setup.FirstOrDefault(m=>m.ID==ID);
                return View(reco);
            }
            catch(Exception e)
            {
                return View();
            }
        }
        [ActionName("Delete")]
        [HttpPost]
        public ActionResult methodDelete(string id)
        {
            var ID = int.Parse(id);
            var record = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == ID);
            var child = dbcontext.job_level_setup.Where(m => m.report_job_levelID == record.ID.ToString());
            if(child.Count()>0)
            {
                TempData["Message"] = "You can't delete beacause it have child";
                return View(record);
            }
            try
            {
                var slots = dbcontext.Slots.Where(m => m.joblevelsetupID == id).ToList();
                //foreach(var item in slots)
                //{
                //    if(item.job_title_cards==null)
                //    {
                //        dbcontext.Slots.Remove(item);
                //    }
                //    else
                //    {
                //        if(item.job_title_cards.joblevelsetupID!=id)
                //        {
                //            item.job_level_setup = null;
                //        }
                //    }
                  
                //}
                dbcontext.SaveChanges();
                var speci = dbcontext.Special_Allwonce_History.Where(m => m.selectedID == record.ID); 
                dbcontext.Special_Allwonce_History.RemoveRange(speci);
                dbcontext.SaveChanges();
               if(record.job_level_education!=null)
                {
                    dbcontext.job_level_education.RemoveRange(record.job_level_education);
                    dbcontext.SaveChanges();
                }
               if(record.Organization_Unit_Type!=null)
                {
                    record.Organization_Unit_Type = null;
                    dbcontext.SaveChanges();
                }
                dbcontext.job_level_setup.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = "You can't delete beacause it have child";
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        public ActionResult details(string id)
        {
            var ID = int.Parse(id);
            ViewBag.level_code = dbcontext.Job_level_class.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.level_grade = dbcontext.Job_level_gradee.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.report = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

            var record = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == ID);
            return View(record);
        }
    }
}