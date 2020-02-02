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
    public class job_title_cardController : Controller
    {
        // GET: job_title_card
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult index()
        {
            var model = dbcontext.job_title_cards.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            try
            {
                ////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////
                ViewBag.level_Set_up = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.parent_jobbb = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.subclass = dbcontext.Job_title_sub_class.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                var model = new job_title_cards();
                model.check_status = Models.Infra.check_status.created;
                model.gender = Models.Infra.gender.Both;
                model.validity = Models.Infra.validity.valid;
                model.working_system = Models.Infra.working_system.Day;
                model.parment = Models.Infra.parment.permanent;
                model.from_age = 1;
                model.to_age = 2;
                ////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////


                //////
                

                var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Organization).Structure_Code;
                var modell = dbcontext.job_title_cards.ToList();
                if (modell.Count() == 0)
                {
                    model.Code = stru + "1";
                }
                else
                {
                    model.Code = stru + (modell.LastOrDefault().ID + 1).ToString();
                }
                /////
                return View(model);
            }
            catch (Exception e)
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult Create(FormCollection form, job_title_cards model, string command)
        {
            try
            {
                ////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////
                ViewBag.level_Set_up = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.parent_jobbb = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.subclass = dbcontext.Job_title_sub_class.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////
                if (ModelState.IsValid)
                {
                    var record = new job_title_cards();
                    record.Code = model.Code;
                    record.name = model.name;
                    record.Description = model.Description;
                    record.gender = model.gender;
                    record.parent_job = model.parent_job;
                    record.validity = model.validity;
                    record.working_system = model.working_system;
                    record.num_slots = model.num_slots;
                    record.parment = model.parment;
                    record.job_Summery = model.job_Summery;
                    record.Job_alt_summery = model.Job_alt_summery;
                    record.from_age = model.from_age;
                    record.to_age = model.to_age;
                    if (model.from_age > model.to_age)
                    {
                        TempData["Message"] = "Frome age bigger than to age";
                     
                        return View(model);
                    }
                    record.check_status = model.check_status;
                    record.Default_job_title_sub_classID = model.Default_job_title_sub_classID;
                    /////////////////////
                    record.Organization_unit_codeID = model.Organization_unit_codeID;
                    var IDorg = int.Parse(model.Organization_unit_codeID);
                    record.Organization_Chart = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == IDorg);
                    //////////////////////
                    List<Job_title_sub_class> sub = new List<Job_title_sub_class>();
                    var idd = int.Parse(record.Default_job_title_sub_classID);
                    var s = dbcontext.Job_title_sub_class.FirstOrDefault(m => m.ID == idd);
                    sub.Add(s);
                    List<string> subid = new List<string>();
                    subid.Add(s.ID.ToString());
                    record.Job_title_sub_class = sub;
                    record.Job_title_sub_classid = subid;
                    ///////////////////
                    var ID = int.Parse(model.joblevelsetupID);
                    record.joblevelsetupID = model.joblevelsetupID;//////////////
                    record.job_level_setup = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == ID);
                    ///////////////////
                    ///////////////////
                    var IDD = int.Parse(model.nationalityID);
                    record.nationalityID = model.nationalityID;
                    record.Nationality = dbcontext.Nationality.FirstOrDefault(m => m.ID == IDD);

                    //////////////////
                    //var jobdetails = new Job_Details();
                    //var quli = new qulification_job();
                    //var qulii=dbcontext.qulification_job.Add(quli);
                    //dbcontext.SaveChanges();
                    //jobdetails.qulification_job = new List<qulification_job>();
                    //jobdetails.qulification_job.Add(qulii);
                    //var skil = new skills_job();
                    //var skill = dbcontext.skills_job.Add(skil);
                    //dbcontext.SaveChanges();
                    //jobdetails.skill = new List<skills_job>();
                    //jobdetails.skill.Add(skill);
                    //jobdetails.Risks = new List<Risks>();
                    //jobdetails.Responsibilities = new List<Responsibilities>();
                    //jobdetails.Requirments = new List<Requirments>();
                    //var details=dbcontext.Job_Details.Add(jobdetails);
                    //dbcontext.SaveChanges();
                    ///////////////////
                    //////////////////
                    //record.Job_Details = details;
                    //record.Job_DetailsID = details.ID.ToString();
                    record.Job_Details = new Job_Details();

                    ////////////////////////////////////////////////////////
                    //////////////////////save slots///////////////////////
                    //////////////////////////////////////////////////////
                    var job_level = form["job_level"].Split(char.Parse(","));
                    var slotcode__ = form["slotcode__"].Split(char.Parse(","));
                    var organization = form["organization"].Split(char.Parse(","));
                    var type = form["type"].Split(char.Parse(","));
                    var status = form["status"].Split(char.Parse(","));
                    var slots = new List<Slots>();
                    var hir = 0;
                   
                    var vacant = 0;
                    for (var iii = 0; iii < job_level.Count(); iii++)
                    {

                        if (job_level[iii] != "" && organization[iii] != "" && type[iii] != "" && status[iii] != "")
                        {
                            var IDlevel = int.Parse(job_level[iii]);
                            var le = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == IDlevel);
                            var IDorganization = int.Parse(organization[iii]);
                            var organization2 = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == IDorganization);

                            var slot = new Slots
                            {
                                EmployeeID = "0",
                                EmployeeName = "empty",
                                slot_code = slotcode__[iii],
                                slot_description = record.Description,
                                job_level_setup = le,
                                joblevelsetupID = le.ID.ToString(),
                                Organization_Chart__ = organization2,
                                check_status = (check_status)int.Parse(status[iii]),
                                slot_type = (slot_type)(int.Parse(type[iii])),
                                Employee_Profile = null,
                                
                            };
                            
                                hir = hir + 1;
                            
                                vacant = 0;
                            
                            var ss = dbcontext.Slots.Add(slot);
                            dbcontext.SaveChanges();
                            slots.Add(ss);
                        }
                    }
                    record.Slots = slots;
                    record.number_hired = hir;
                    record.number_vacant = vacant;
                    var card = dbcontext.job_title_cards.Add(record);
                    dbcontext.SaveChanges();
                    //foreach(var item in slots)
                    //{
                    //    item.job_title_cards = card;
                    //    dbcontext.SaveChanges();
                    //}
                    ///////////////////
                    if (command == "Submit")
                    {
                        // TempData["model"] = modelll;
                        return RedirectToAction("Link", new { id = card.ID });
                    }
                    if (command == "Submit2")
                    {
                        // TempData["model"] = modelll;
                        return RedirectToAction("Details", new { id = card.ID });
                    }
                }
                else
                {
                  
                    return View(model);
                }
                }
            catch (DbUpdateException)
            {
                
                TempData["Message"] = "this code already in Database";
                return View(model);
            }
            catch (Exception e)
            {
                
                return View(model);
            }
            return RedirectToAction("index");
        }

        public ActionResult edit(string id)
        {
            try
            {
                ////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////
                ViewBag.level_Set_up = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.parent_jobbb = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.subclass = dbcontext.Job_title_sub_class.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////
                var ID = int.Parse(id);
                var model = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == ID);
              
                return View(model);
            }

            catch (Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult edit(FormCollection form, job_title_cards model, string command)
        {
            ////////////////////////////////////////////////////////////////////////
            ViewBag.level_Set_up = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.parent_jobbb = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
            ViewBag.subclass = dbcontext.Job_title_sub_class.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
            ////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////
            try
            {
               
                ViewBag.num = model.num_slots;
                var record = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == model.ID);
                record.Code = model.Code;
                record.name = model.name;
                record.Description = model.Description;
                record.gender = model.gender;
                record.parent_job = model.parent_job;
                record.validity = model.validity;
                record.working_system = model.working_system;
                record.num_slots = model.num_slots;
                record.parment = model.parment;
                record.job_Summery = model.job_Summery;
                record.Job_alt_summery = model.Job_alt_summery;
                record.from_age = model.from_age;
                record.to_age = model.to_age;
                if (model.from_age > model.to_age)
                {
                    TempData["Message"] = "Frome age bigger than to age";
                    return View(model);
                }
                record.check_status = model.check_status;
                record.Default_job_title_sub_classID = model.Default_job_title_sub_classID;
                //////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////
                //if (model.Job_title_sub_class.Count < 1)
                //{
                //    List<Job_title_sub_class> sub = new List<Job_title_sub_class>();
                //    var idd = int.Parse(record.Default_job_title_sub_classID);
                //    var s = dbcontext.Job_title_sub_class.FirstOrDefault(m => m.ID == idd);
                //    sub.Add(s);
                //    List<string> subid = new List<string>();
                //    subid.Add(s.ID.ToString());
                //    record.Job_title_sub_class = sub;
                //    record.Job_title_sub_classid = subid;
                //}
                var sub = record.Job_title_sub_class;
                bool flag = false;
                if (sub != null)
                {
                    foreach (var item in sub)
                    {
                        if (item.ID.ToString() == model.Default_job_title_sub_classID)
                        {
                            record.Default_job_title_sub_classID = model.Default_job_title_sub_classID;
                            flag = true;
                            break;
                        }
                    }
                    if (flag == false)
                    {
                        record.Default_job_title_sub_classID = model.Default_job_title_sub_classID;
                        var II = int.Parse(model.Default_job_title_sub_classID);
                        var new_sub = dbcontext.Job_title_sub_class.FirstOrDefault(m => m.ID ==II );
                        record.Job_title_sub_class.Add(new_sub);
                     //   record.Job_title_sub_classid.Add(new_sub.ID.ToString());
                        dbcontext.SaveChanges();
                    }
                }
                else
                {
                    List<Job_title_sub_class> subb = new List<Job_title_sub_class>();
                    var idd = int.Parse(record.Default_job_title_sub_classID);
                    var s = dbcontext.Job_title_sub_class.FirstOrDefault(m => m.ID == idd);
                    subb.Add(s);
                    List<string> subid = new List<string>();
                    subid.Add(s.ID.ToString());
                    record.Job_title_sub_class = subb;
                    record.Job_title_sub_classid = subid;
                }
                    //////////////////////////////////////////////////////////
                    //////////////////////////////////////////////////////////
                    //////////////////////////////////////////////////////////
                    var ID = int.Parse(model.joblevelsetupID);
                record.joblevelsetupID = model.joblevelsetupID;//////////////
                record.job_level_setup = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == ID);
                ///////////////////
                ///////////////////
                var IDD = int.Parse(model.nationalityID);
                record.nationalityID = model.nationalityID;
                record.Nationality = dbcontext.Nationality.FirstOrDefault(m => m.ID == IDD);
                ///////////////////
                /////////////////////
                record.Organization_unit_codeID = model.Organization_unit_codeID;
                var IDorg = int.Parse(model.Organization_unit_codeID);
                record.Organization_Chart = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == IDorg);
                //////////////////////
                ///////////////////
                //var jobdetails = new Job_Details();
                //var quli = new qulification_job();
                //var qulii=dbcontext.qulification_job.Add(quli);
                //dbcontext.SaveChanges();
                //jobdetails.qulification_job = new List<qulification_job>();
                //jobdetails.qulification_job.Add(qulii);
                //var skil = new skills_job();
                //var skill = dbcontext.skills_job.Add(skil);
                //dbcontext.SaveChanges();
                //jobdetails.skill = new List<skills_job>();
                //jobdetails.skill.Add(skill);
                //jobdetails.Risks = new List<Risks>();
                //jobdetails.Responsibilities = new List<Responsibilities>();
                //jobdetails.Requirments = new List<Requirments>();
                //var details=dbcontext.Job_Details.Add(jobdetails);
                //dbcontext.SaveChanges();
                ///////////////////
                //////////////////
                //record.Job_Details = details;
                //record.Job_DetailsID = details.ID.ToString();
                var old_id = form["id"].Split(char.Parse(","));
                var type = form["type"].Split(char.Parse(","));
                int co = 0;
                for (var i= 0;i < model.Slots.Count();i++)
                {
                    var boolo=old_id.Contains(model.Slots[i].ID.ToString());
                    if(boolo==false)
                    {
                        var slot = dbcontext.Slots.FirstOrDefault(m => m.ID == model.Slots[i].ID);
                        var job = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == slot.job_title_cards.ID);
                        job.number_hired = job.number_hired + 1;
                        job.number_vacant = job.number_vacant - 1;
                        dbcontext.SaveChanges();
                        dbcontext.Slots.Remove(slot);
                        dbcontext.SaveChanges();
                        co = co + 1;
                    }
                }
                for(var i=0;i<co;i++)
                {
                    var id =int.Parse(old_id[i]);
                    var slot = dbcontext.Slots.FirstOrDefault(m => m.ID == id);
                    slot.slot_type = (slot_type)(int.Parse(type[i]));
                }



                var job_level = form["job_level"].Split(char.Parse(","));
                var slotcode__ = form["slotcode__"].Split(char.Parse(","));
                var organization = form["organization"].Split(char.Parse(","));
                var status = form["status"].Split(char.Parse(","));
                var slots = new List<Slots>();
                var hired = 0;
                var vacant = 0;
                for (var iii = 0; iii < job_level.Count(); iii++)
                {

                    if (job_level[iii] != "" && organization[iii] != "" && type[iii] != "" && status[iii] != "")
                    {
                        var IDlevel = int.Parse(job_level[iii]);
                        var le = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == IDlevel);
                        var IDorganization = int.Parse(organization[iii]);
                        var organization2 = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == IDorganization);

                        var slot = new Slots
                        {
                            EmployeeID = "0",
                            EmployeeName = "empty",
                            slot_code = slotcode__[iii],
                            slot_description = record.Description,
                            job_level_setup = le,
                            joblevelsetupID = le.ID.ToString(),
                            Organization_Chart__=organization2,
                            check_status = (check_status)int.Parse(status[iii]),
                            slot_type = (slot_type)(int.Parse(type[iii])),
                            Employee_Profile = null,
                        };
                        hired++;
                        
                        var ss = dbcontext.Slots.Add(slot);
                        dbcontext.SaveChanges();
                        slots.Add(ss);
                    }
                }
                record.Slots.AddRange(slots);
                record.number_hired += hired;
                dbcontext.SaveChanges();
                 foreach (var item in slots)
                {
                    item.job_title_cards = record;
                    dbcontext.SaveChanges();
                }
                if (command == "Submit")
                {
                    // TempData["model"] = modelll;
                    return RedirectToAction("Link", new { id = model.ID });
                }
                if (command == "Submit2")
                {
                    // TempData["model"] = modelll;
                    return RedirectToAction("Details", new { id = model.ID });
                }
            }

            catch (DbUpdateException e)
            {
              
                TempData["Message"] = "this code already in Database";
                return View(model);
            }
            catch (Exception e)
            {
             
                return View(model);
            }
            return RedirectToAction("index");
        }
        public ActionResult Link(int id)
        {
            job_title_cards modell = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == id);
            try
            {

                var all_unit = dbcontext.Job_title_sub_class.ToList();
                ViewBag.id = modell.ID;
                List<job_title_sub_classs_VM> mm = new List<job_title_sub_classs_VM>();
                foreach (var team in all_unit)
                {
                    if (modell.Job_title_sub_class.Any(ma => ma.ID == team.ID))
                    {
                        mm.Add(new job_title_sub_classs_VM { Job_title_sub_class=team,selected=true});
                    }
                    else
                    {
                        mm.Add(new job_title_sub_classs_VM { Job_title_sub_class = team, selected = false });
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
                var record = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == id);
                List<Job_title_sub_class> organization = new List<Job_title_sub_class>();
                List<string> orgid = new List<string>();
                for (var item = 0; item < record.Job_title_sub_class.Count(); item++)
                {
                    record.Job_title_sub_class[item] = null;
                    dbcontext.SaveChanges();
                }
                record.Job_title_sub_class = null;
                dbcontext.SaveChanges();
                if (form.AllKeys.Count() > 0)
                {
                    var checkedd = form["Selected"].Split(char.Parse(","));
                    foreach (var item in checkedd)
                    {
                        var ID = int.Parse(item);
                        var org = dbcontext.Job_title_sub_class.FirstOrDefault(m => m.ID == ID);
                        organization.Add(org);
                        orgid.Add(org.ID.ToString());
                    }
                    record.Job_title_sub_class = organization;
                    record.Job_title_sub_classid = orgid;
                    dbcontext.SaveChanges();
                }
                else
                {

                    record.Job_title_sub_classid = null;
                    dbcontext.SaveChanges();
                }
                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return RedirectToAction("link");
            }
        }


        public ActionResult Details(int id)
        {
            try
            {
                var model = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == id);
                ViewBag.id = model.ID;
                ViewBag.quliname = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.qulispeci = dbcontext.Qualification_Major.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.qulidegree = dbcontext.GradeEducate.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.skill = dbcontext.Skill.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.risk = dbcontext.Risks.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.respon = dbcontext.Responsibilities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.requi = dbcontext.Requirments.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record =model.Job_Details ;
               // record.job_title_cards = model;
              //  record.job_title_cardsID = model.ID.ToString();
                return View(model);
            }
            catch(Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Details(FormCollection form,string id)
        {
            try
            {
                ViewBag.quliname = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.qulispeci = dbcontext.Qualification_Major.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.qulidegree = dbcontext.GradeEducate.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.skill = dbcontext.Skill.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.risk = dbcontext.Risks.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.respon = dbcontext.Responsibilities.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.requi = dbcontext.Requirments.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                ViewBag.id = id;
                var ID = int.Parse(id);
                var model = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == ID);
                //ViewBag.quliname = dbcontext.Name_of_educational_qualification.ToList();
                //ViewBag.qulispeci = dbcontext.Qualification_Major.ToList();
                //ViewBag.qulidegree = dbcontext.GradeEducate.ToList();
                //ViewBag.skill = dbcontext.Skill.ToList();
                //ViewBag.risk = dbcontext.Risks.ToList();
                //ViewBag.respon = dbcontext.Responsibilities.ToList();
                //ViewBag.requi = dbcontext.Requirments.ToList();
                ////////////////////////
                ////////////////////////
                var record1 = model.Job_Details;
                if (model.Job_Details != null)
                {
                    model.Job_Details = null;
                    model.Job_DetailsID = null;
                    dbcontext.SaveChanges();
                    //dbcontext.Job_Details.Remove(record1);
                    //dbcontext.SaveChanges();
                }

                var record = new Job_Details();
                ////////////////////
                ///////////////////
                if (record1 != null)
                {
                    for (var item = 0; item < record1.qulification_job.Count(); item++)
                    {
                        var quli5 = record1.qulification_job[item];
                        record1.qulification_job[item] = null;
                        dbcontext.qulification_job.Remove(quli5);
                        dbcontext.SaveChanges();
                    }
                    record1.qulification_job = null;
                    record1.qulification_jobID = null;
                    dbcontext.SaveChanges();
                    ///////
                    //////
                    //////
                    for (var item = 0; item < record1.Risks.Count(); item++)
                    {
                        var quli1 = record1.Risks[item];
                        record1.Risks[item] = null;
                        dbcontext.SaveChanges();
                    }
                    record1.Risks = null;
                    record1.Risksid = null;
                    dbcontext.SaveChanges();
                    ////////
                    ///////
                    //////
                    for (var item = 0; item < record1.skill.Count(); item++)
                    {
                        var quli2 = record1.skill[item];
                        record1.skill[item] = null;
                        dbcontext.skills_job.Remove(quli2);
                        dbcontext.SaveChanges();
                    }
                    record1.skill = null;
                    record1.skillID = null;
                    dbcontext.SaveChanges();
                    ////////
                    ///////
                    //////
                    for (var item = 0; item < record1.Responsibilities.Count(); item++)
                    {
                        var quli3 = record1.Requirments[item];
                        record1.Responsibilities[item] = null;
                        dbcontext.SaveChanges();
                    }
                    record1.Responsibilities = null;
                    record1.responsibilitiesID = null;
                    dbcontext.SaveChanges();
                    /////////
                    ///////////
                    /////////
                    for (var item = 0; item < record1.Requirments.Count(); item++)
                    {
                        var quli4 = record1.Requirments[item];
                        record1.Requirments[item] = null;
                        dbcontext.SaveChanges();
                    }
                    record1.Requirments = null;
                    record1.requirmentID = null;
                    dbcontext.SaveChanges();
                }
                List<qulification_job> qulification_job = new List<qulification_job>();
                List<string> qulification_job_id = new List<string>();
              
                    var name = form["s1"].Split(char.Parse(","));
              
                    var major = form["s11"].Split(char.Parse(","));
                    var grade = form["s111"].Split(char.Parse(","));
                 
                    for (var item=0;item<name.Count();item++)
                    {
                    if (name[item] != ""&&grade[item]!=""&& major[item]!="")
                    {
                        var quli = new qulification_job();
                        var IDd = int.Parse(name[item]);
                            var org = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == IDd);
                            quli.Name_of_educational_qualification = org;
                            quli.Name_of_educational_qualificationID = org.ID.ToString();

                            var IDd1 = int.Parse(major[item]);
                            var majorr = dbcontext.Qualification_Major.FirstOrDefault(m => m.ID == IDd1);
                            quli.Qualification_Major = majorr;
                            quli.Qualification_MajorID = majorr.ID.ToString();

                            var IDd2 = int.Parse(grade[item]);
                            var gradee = dbcontext.GradeEducate.FirstOrDefault(m => m.ID == IDd2);
                            quli.GradeEducate = gradee;
                            quli.GradeEducateID = gradee.ID.ToString();
                            quli.required = true;

                            var quli_job = dbcontext.qulification_job.Add(quli);
                            dbcontext.SaveChanges();
                            qulification_job.Add(quli_job);
                            qulification_job_id.Add(quli_job.ID.ToString());
                        }
                    }
                  
                
                record.qulification_job = qulification_job;
                record.qulification_jobID = qulification_job_id;
                dbcontext.SaveChanges();
                //////////////////////
                //////////////////////
                /////////////////////
              
                List<Risks> Risks = new List<Risks>();
                List<string> Risks_id = new List<string>();
               

                    var risk = form["risk"].Split(char.Parse(","));
                   
                    //var quli = new qulification_job();
                    for (var item = 0; item < risk.Count(); item++)
                    {
                        if (risk[item]!="")
                        {
                            var IDd = int.Parse(risk[item]);
                            var org = dbcontext.Risks.FirstOrDefault(m => m.ID == IDd);
                            Risks.Add(org);
                            Risks_id.Add(org.ID.ToString());
                        }
                    }

            
                record.Risks = Risks;
                record.Risksid = Risks_id;
                dbcontext.SaveChanges();
                ///////////////////
                ///////////////////
                ///////////////////
              
                List<skills_job> Skill = new List<skills_job>();
                List<string> Skill_id = new List<string>();
               
                    var Skill1 = form["newskill"].Split(char.Parse(","));

                    //var quli = new qulification_job();
                    for (var item = 0; item < Skill1.Count(); item++)
                    {
                        if (Skill1[item] != "")
                        {
                            var IDd = int.Parse(Skill1[item]);
                            var org = dbcontext.Skill.FirstOrDefault(m => m.ID == IDd);
                            var s = dbcontext.skills_job.Add(new skills_job { skill = org, skillid = org.ID.ToString(), required = true });
                            dbcontext.SaveChanges();
                            Skill.Add(s);
                            Skill_id.Add(s.ID.ToString());
                        }
                    }

                record.skill = Skill;
                record.skillID = Skill_id;
                dbcontext.SaveChanges();
                /////////////////////////////
                ////////////////////////////
                ////////////////////////////
               
                List<Responsibilities> res = new List<Responsibilities>();
                List<string> res_id = new List<string>();
                
                    var respon = form["respon"].Split(char.Parse(","));

                    //var quli = new qulification_job();
                    for (var item = 0; item < respon.Count(); item++)
                    {
                        if (respon[item] != "")
                        {
                            var IDd = int.Parse(respon[item]);
                            var org = dbcontext.Responsibilities.FirstOrDefault(m => m.ID == IDd);
                            res.Add(org);
                            res_id.Add(org.ID.ToString());
                        }
                    }

                
                record.Responsibilities = res;
                record.responsibilitiesID = res_id;
                dbcontext.SaveChanges();
                //////////////////////////////////
                //////////////////////////////////
                ///////////////////////////////////
             
                List<Requirments> Requirments = new List<Requirments>();
                List<string> Requirments_id = new List<string>();
                
                    var r = form["requirment"].Split(char.Parse(","));

                    //var quli = new qulification_job();
                    for (var item = 0; item < r.Count(); item++)
                    {
                        if (r[item] != "")
                        {
                            var IDd = int.Parse(r[item]);
                            var org = dbcontext.Requirments.FirstOrDefault(m => m.ID == IDd);
                            Requirments.Add(org);
                            Requirments_id.Add(org.ID.ToString());
                        }
                    }
                    
                record.Requirments = Requirments;
                record.requirmentID = Requirments_id;
                dbcontext.SaveChanges();
                /////////////////////////////////
                /////////////////////////////////
                ////////////////////////////////
                var rec=  dbcontext.Job_Details.Add(record);
                dbcontext.SaveChanges();
              
                model.Job_Details = rec;
                model.Job_DetailsID = rec.ID.ToString();
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch(Exception e)
            {
                return RedirectToAction("Details");
            }
        }
        public ActionResult Remove(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == ID);
                return View(model);
            }
            catch(Exception e)
            {
                return View();
            }
        }
        [ActionName("Remove")]
        [HttpPost]
        public ActionResult  method_Remove(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == ID);
            
          
            var quli = model.Job_Details.qulification_job;
            var skill = model.Job_Details.skill;
            var details = model.Job_Details;
            var co = model.Slots.Count();
            List<int> lll = new List<int>();
            try
            {
                for (var item=0;item<model.Slots.Count();item++)
                {
                    lll.Add(model.Slots[item].ID);
                }
                for (var item=0;item<co;item++)
                {
                    var i = lll[item];
                    var s=dbcontext.Slots.FirstOrDefault(m=>m.ID== i);
                    if (s.joblevelsetupID == model.joblevelsetupID)
                    {
                        dbcontext.Slots.Remove(s);
                       // dbcontext.SaveChanges();
                    }
                    else if(s.job_level_setup==null)
                    {
                        dbcontext.Slots.Remove(s);
                        //dbcontext.SaveChanges();
                    }
                    else
                    {
                        dbcontext.Slots.Remove(s);
                        //dbcontext.SaveChanges();
                    }
                }
                dbcontext.SaveChanges();
                for (var ii = 0; ii < model.Job_title_sub_class.Count(); ii++)
                {
                    model.Job_title_sub_class[ii] = null;
                    dbcontext.SaveChanges();
                }

                dbcontext.job_title_cards.Remove(model);
                dbcontext.SaveChanges();

               
              
                dbcontext.qulification_job.RemoveRange(quli);
                dbcontext.skills_job.RemoveRange(skill);
                dbcontext.SaveChanges();
                ////////////////////////
                ///////////////////////
                //////////////////////
                dbcontext.Job_Details.Remove(details);
                dbcontext.SaveChanges();
             
                return RedirectToAction("index");
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = "You can't delete beacause it have child";
                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToAction("Remove");
            }
        }
        public JsonResult getchart(string id)
        {
            var ID = int.Parse(id);
            var m = dbcontext.Organization_Chart.FirstOrDefault(mM=>mM.ID==ID);
            return Json(m);
        }
    }
   
}