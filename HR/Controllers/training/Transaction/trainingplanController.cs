using HR.Controllers.training.setup;
using HR.Models;
using HR.Models.Infra;
using HR.Models.Training.transaction;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.training.Transaction
{
    public class trainingplanController : BaseController
    {
        // GET: trainingplan
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var courses = dbcontext.TrainingPlan_Header.ToList();
            return View(courses);
        }
        public ActionResult Create()
        {
            try
            {
                ViewBag.type = dbcontext.TrainingType_Header.ToList().Select(m => new { Code = m.TrainingType_Code + "-[" + m.TrainingType_Desc + ']', ID = m.ID });
                ViewBag.classification = dbcontext.CourseClassification.ToList().Select(m => new { Code = m.CourseClassification_Code + "-[" + m.CourseClassification_Desc + ']', ID = m.ID });
                ViewBag.courses = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });
                ViewBag.instructors = dbcontext.Instructor.ToList().Select(m => new { Code = m.InstructorID + "-[" + m.Instructor_FullName + ']', ID = m.ID });

                var new_header = new TrainingPlan_Header { Year = DateTime.Now.Year };
                var new_Details = new TrainingPlan_Detail { From_Date = DateTime.Now.Date, To_Date = DateTime.Now.Date };
                var new_plan = new VM_trainingplan { Course_status = Course_status.planed, TrainingPlan_Header = new_header, TrainingPlan_Detail = new_Details };
                return View(new_plan);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult create(VM_trainingplan model, FormCollection form)
        {
            try
            {
                ViewBag.type = dbcontext.TrainingType_Header.ToList().Select(m => new { Code = m.TrainingType_Code + "-[" + m.TrainingType_Desc + ']', ID = m.ID });
                ViewBag.classification = dbcontext.CourseClassification.ToList().Select(m => new { Code = m.CourseClassification_Code + "-[" + m.CourseClassification_Desc + ']', ID = m.ID });
                ViewBag.courses = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });
                ViewBag.instructors = dbcontext.Instructor.ToList().Select(m => new { Code = m.InstructorID + "-[" + m.Instructor_FullName + ']', ID = m.ID });
                //======================
                model.TrainingPlan_Header.Created_By = User.Identity.Name;
                model.TrainingPlan_Header.Created_Date = DateTime.Now.Date;
                if(int.Parse(model.TrainingPlan_Header.TrainingType_Code)>0)
                {
                    var ID =int.Parse(model.TrainingPlan_Header.TrainingType_Code);
                    var Tr = dbcontext.TrainingType_Header.FirstOrDefault(m => m.ID == ID);
                    model.TrainingPlan_Header.TrainingType_des = Tr.TrainingType_Code + "-" + Tr.TrainingType_Desc;
                }
                //====
                var St_ = new status {created_by=User.Identity.Name,created_bydate=DateTime.Now.Date, statu = check_status.created };
                var status=dbcontext.status.Add(St_);
                dbcontext.SaveChanges();
                //====
                model.TrainingPlan_Header.statu = check_status.created;
                model.TrainingPlan_Header.status_ID = status.ID;
                model.TrainingPlan_Header.status_ = status;
                var Hheader = dbcontext.TrainingPlan_Header.Add(model.TrainingPlan_Header);
                dbcontext.SaveChanges();
                //======================
                var Classification = form["Classification__"].Split(',');
                var course = form["course__"].Split(',');
                var center = form["center__"].Split(',');
                var bransh = form["bransh__"].Split(',');
                var instructor = form["instr__"].Split(',');
                var from = form["fromD"].Split(',');
                var to = form["toD"].Split(',');
                var num_days = form["num_D"].Split(',');
                var cost_per_person = form["cost_P"].Split(',');
                var num_of_participant = form["num_part"].Split(',');
                var total_coast = form["tot_"].Split(',');
                var status_co = form["course_status__"].Split(',');
                var status_co_text = form["course_status__text"].Split(',');

                for (var i = 0; i < Classification.Length; i++)
                {
                    if (Classification[i] != "")
                    {
                        var CL_id = int.Parse(Classification[i]);
                        var class_ = dbcontext.CourseClassification.FirstOrDefault(m => m.ID == CL_id);
                        //===
                        var CO_id = int.Parse(course[i]);
                        var course_ = dbcontext.Cours.FirstOrDefault(m => m.ID == CO_id);
                        //===
                        var CE_id = int.Parse(center[i]);
                        var Center_ = dbcontext.Course_Centers.FirstOrDefault(m => m.ID == CE_id);
                        //===
                        var B_id = int.Parse(bransh[i]);
                        var Branch_ = dbcontext.TrainingCenters_Branch.FirstOrDefault(m => m.ID == B_id);
                        //===
                        var plan_details = new TrainingPlan_Detail { CourseClassification_Code = class_.ID.ToString(), classification_des = class_.CourseClassification_Code + '-' + class_.CourseClassification_Desc,
                            Course_Code = course_.ID.ToString(), course_des = course_.Course_Code + '-' + course_.Course_Desc,
                            TrainingCenters_Code = Center_.ID.ToString(), center_des = Center_.TrainingCenters_Code + '-' + Center_.center_des,
                            Branch_Code = Branch_.ID.ToString(), bransh_des = Branch_.Branch_Code + '-' + Branch_.Branch_Desc,
                            From_Date = Convert.ToDateTime(from[i]), To_Date = Convert.ToDateTime(to[i]),
                            Number_Of_Days = (Int16)int.Parse(num_days[i]), Cost_Per_Person = (Int16)int.Parse(cost_per_person[i]), Total_Cost = (Int16)int.Parse(total_coast[i]), Number_Of_Participant = (Int16)int.Parse(num_of_participant[i]),
                            Course_Status = (Int16)int.Parse(status_co[i]), stat_course = status_co_text[i], Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date,
                            Year = Hheader.Year, TrainingType_Code = Hheader.TrainingType_Code, TrainingPlan_HeaderID = Hheader.ID
                        };
                        if (instructor[i] == "")
                        {
                            plan_details.InstructorID = "0";
                            plan_details.instructor_des = null;
                        }
                        else
                        {
                            var inst_id = int.Parse(instructor[i]);

                            if (inst_id == 0)
                            {
                                plan_details.InstructorID = "0";
                                plan_details.instructor_des = null;
                            }
                            else
                            {

                                var inst_ = dbcontext.Instructor.FirstOrDefault(m => m.ID == inst_id);
                                plan_details.InstructorID = inst_.ID.ToString();
                                plan_details.instructor_des = inst_.Instructor_FullName;

                            }
                        }
                        var D = dbcontext.TrainingPlan_Detail.Add(plan_details);
                        dbcontext.SaveChanges();
                        //======================================================================
                        ///=========add cost header===============
                        var Cource_cen_header = new Course_CostElement_Header { Cost_Per_Person = 0, details_id = D.ID, CourseClassification_Code = D.CourseClassification_Code, Branch_Code = D.Branch_Code, Year = D.Year, TrainingType_Code = D.TrainingType_Code, Number_Of_Days = D.Number_Of_Days, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Course_Code = D.Course_Code, TrainingCenters_Code = D.TrainingCenters_Code };
                        var H = dbcontext.Course_CostElement_Header.Add(Cource_cen_header);
                        dbcontext.SaveChanges();
                        ///=======================================
                        //======================================================================
                        //======================================================================
                        ///=========add trainee header===============
                        var trainee_header = new TrainingParticipants_Header { TrainingPlan_HeaderID = Hheader.ID, Cost_Per_Person = 0, details_id = D.ID, CourseClassification_Code = D.CourseClassification_Code, Branch_Code = D.Branch_Code, Year = D.Year, TrainingType_Code = D.TrainingType_Code, Number_Of_Days = D.Number_Of_Days, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Course_Code = D.Course_Code, TrainingCenters_Code = D.TrainingCenters_Code };
                        var T_H = dbcontext.TrainingParticipants_Header.Add(trainee_header);
                        dbcontext.SaveChanges();
                        ///=======================================
                        //======================================================================
                        ///=========add result header===============
                        var result_header = new TrainingCourseResult_Header { From_Date = D.From_Date, To_Date = D.To_Date, details_id = D.ID, CourseClassification_Code = D.CourseClassification_Code, Branch_Code = D.Branch_Code, Year = D.Year, TrainingType_Code = D.TrainingType_Code, Number_Of_Days = D.Number_Of_Days, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Course_Code = D.Course_Code, TrainingCenters_Code = D.TrainingCenters_Code };
                        var R_H = dbcontext.TrainingCourseResult_Header.Add(result_header);
                        dbcontext.SaveChanges();
                        ///=======================================
                        //======================================================================
                        //======================================================================
                        ///=========add evalution header===============
                        var evalution_header = new TrainingCourceEvaluation_Header { Evaluation_Type = (Int16)used_for.center, details_ID = D.ID, CourseClassification_Code = D.CourseClassification_Code, Branch_Code = D.Branch_Code, Year = D.Year, TrainingType_Code = D.TrainingType_Code, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Course_Code = D.Course_Code, TrainingCenters_Code = D.TrainingCenters_Code };
                        var evalution_H = dbcontext.TrainingCourceEvaluation_Header.Add(evalution_header);
                        dbcontext.SaveChanges();
                        ///=======================================


                    }
                }
                return RedirectToAction("index");


            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public ActionResult edit(int id)
        {
            try
            {
                ViewBag.type = dbcontext.TrainingType_Header.ToList().Select(m => new { Code = m.TrainingType_Code + "-[" + m.TrainingType_Desc + ']', ID = m.ID });
                ViewBag.classification = dbcontext.CourseClassification.ToList().Select(m => new { Code = m.CourseClassification_Code + "-[" + m.CourseClassification_Desc + ']', ID = m.ID });
                ViewBag.courses = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });
                ViewBag.instructors = dbcontext.Instructor.ToList().Select(m => new { Code = m.InstructorID + "-[" + m.Instructor_FullName + ']', ID = m.ID });

                var Header = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == id);
                var Details = new TrainingPlan_Detail { From_Date = DateTime.Now.Date, To_Date = DateTime.Now.Date };
                var new_plan = new VM_trainingplan { Course_status = Course_status.planed, TrainingPlan_Detail = Details, TrainingPlan_Header = Header };
                return View(new_plan);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult edit(VM_trainingplan model, FormCollection form)
        {
            try
            {
                ViewBag.type = dbcontext.TrainingType_Header.ToList().Select(m => new { Code = m.TrainingType_Code + "-[" + m.TrainingType_Desc + ']', ID = m.ID });
                ViewBag.classification = dbcontext.CourseClassification.ToList().Select(m => new { Code = m.CourseClassification_Code + "-[" + m.CourseClassification_Desc + ']', ID = m.ID });
                ViewBag.courses = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });
                ViewBag.instructors = dbcontext.Instructor.ToList().Select(m => new { Code = m.InstructorID + "-[" + m.Instructor_FullName + ']', ID = m.ID });
                //====

                var header = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == model.TrainingPlan_Header.ID);
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == header.status_ID);
                if(sta.statu==check_status.Approved || sta.statu == check_status.Rejected || sta.statu == check_status.Closed|| sta.statu == check_status.Recervied || sta.statu == check_status.Canceled)
                {
                    TempData["message"] =HR.Resource.training.status_message;
                    return RedirectToAction("index");
                }
                header.Year = model.TrainingPlan_Header.Year;
                header.TrainingType_Code = model.TrainingPlan_Header.TrainingType_Code;
                header.Total_Estimated_Cost = model.TrainingPlan_Header.Total_Estimated_Cost;
                header.Approved_Budget_Amount = model.TrainingPlan_Header.Approved_Budget_Amount;
                header.Deviation = (int?)(header.Approved_Budget_Amount - header.Total_Estimated_Cost);
                dbcontext.SaveChanges();
                //======================

                //======================
                var new_data = form["new_data"].Split(',');
                var Classification = form["Classification__"].Split(',');
                var course = form["course__"].Split(',');
                var center = form["center__"].Split(',');
                var bransh = form["bransh__"].Split(',');
                var instructor = form["instr__"].Split(',');
                var from = form["fromD"].Split(',');
                var to = form["toD"].Split(',');
                var num_days = form["num_D"].Split(',');
                var cost_per_person = form["cost_P"].Split(',');
                var num_of_participant = form["num_part"].Split(',');
                var total_coast = form["tot_"].Split(',');
                var status_co = form["course_status__"].Split(',');
                var status_co_text = form["course_status__text"].Split(',');
                var HH = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == model.TrainingPlan_Header.ID);
                for (var i = 0; i < Classification.Length; i++)
                {
                    if (Classification[i] != "" && new_data[i] == "1")
                    {
                        var CL_id = int.Parse(Classification[i]);
                        var class_ = dbcontext.CourseClassification.FirstOrDefault(m => m.ID == CL_id);
                        //===
                        var CO_id = int.Parse(course[i]);
                        var course_ = dbcontext.Cours.FirstOrDefault(m => m.ID == CO_id);
                        //===
                        var CE_id = int.Parse(center[i]);
                        var Center_ = dbcontext.Course_Centers.FirstOrDefault(m => m.ID == CE_id);
                        //===
                        var B_id = int.Parse(bransh[i]);
                        var Branch_ = dbcontext.TrainingCenters_Branch.FirstOrDefault(m => m.ID == B_id);
                        //===
                        var plan_details = new TrainingPlan_Detail
                        {
                            CourseClassification_Code = class_.ID.ToString(),
                            classification_des = class_.CourseClassification_Code + '-' + class_.CourseClassification_Desc,
                            Course_Code = course_.ID.ToString(),
                            course_des = course_.Course_Code + '-' + course_.Course_Desc,
                            TrainingCenters_Code = Center_.ID.ToString(),
                            center_des = Center_.TrainingCenters_Code + '-' + Center_.center_des,
                            Branch_Code = Branch_.ID.ToString(),
                            bransh_des = Branch_.Branch_Code + '-' + Branch_.Branch_Desc,
                            From_Date = Convert.ToDateTime(from[i]),
                            To_Date = Convert.ToDateTime(to[i]),
                            Number_Of_Days = (Int16)int.Parse(num_days[i]),
                            Cost_Per_Person = (Int16)int.Parse(cost_per_person[i]),
                            Total_Cost = (Int16)int.Parse(total_coast[i]),
                            Number_Of_Participant = (Int16)int.Parse(num_of_participant[i]),
                            Course_Status = (Int16)int.Parse(status_co[i]),
                            stat_course = status_co_text[i],
                            Created_By = User.Identity.Name,
                            Created_Date = DateTime.Now.Date,
                            Year = HH.Year,
                            TrainingType_Code = HH.TrainingType_Code,
                            TrainingPlan_HeaderID = HH.ID
                        };
                        if (instructor[i] == "")
                        {
                            plan_details.InstructorID = "0";
                            plan_details.instructor_des = null;
                        }
                        else
                        {
                            var inst_id = int.Parse(instructor[i]);

                            if (inst_id == 0)
                            {
                                plan_details.InstructorID = "0";
                                plan_details.instructor_des = null;
                            }
                            else
                            {

                                var inst_ = dbcontext.Instructor.FirstOrDefault(m => m.ID == inst_id);
                                plan_details.InstructorID = inst_.ID.ToString();
                                plan_details.instructor_des = inst_.Instructor_FullName;

                            }
                        }
                        var D = dbcontext.TrainingPlan_Detail.Add(plan_details);
                        dbcontext.SaveChanges();
                        //======================================================================
                        ///=========add cost header===============
                        var Cource_cen_header = new Course_CostElement_Header { Cost_Per_Person = 0, details_id = D.ID, CourseClassification_Code = D.CourseClassification_Code, Branch_Code = D.Branch_Code, Year = D.Year, TrainingType_Code = D.TrainingType_Code, Number_Of_Days = D.Number_Of_Days, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Course_Code = D.Course_Code, TrainingCenters_Code = D.TrainingCenters_Code };
                        var H = dbcontext.Course_CostElement_Header.Add(Cource_cen_header);
                        dbcontext.SaveChanges();
                        //======================================================================
                        //======================================================================
                        ///=========add trainee header===============
                        var trainee_header = new TrainingParticipants_Header { TrainingPlan_HeaderID = header.ID, Cost_Per_Person = 0, details_id = D.ID, CourseClassification_Code = D.CourseClassification_Code, Branch_Code = D.Branch_Code, Year = D.Year, TrainingType_Code = D.TrainingType_Code, Number_Of_Days = D.Number_Of_Days, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Course_Code = D.Course_Code, TrainingCenters_Code = D.TrainingCenters_Code };
                        var T_H = dbcontext.TrainingParticipants_Header.Add(trainee_header);
                        dbcontext.SaveChanges();
                        ///=====================================================================
                        //======================================================================
                        ///=========add result header===============
                        var result_header = new TrainingCourseResult_Header { From_Date = D.From_Date, To_Date = D.To_Date, details_id = D.ID, CourseClassification_Code = D.CourseClassification_Code, Branch_Code = D.Branch_Code, Year = D.Year, TrainingType_Code = D.TrainingType_Code, Number_Of_Days = D.Number_Of_Days, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Course_Code = D.Course_Code, TrainingCenters_Code = D.TrainingCenters_Code };
                        var R_H = dbcontext.TrainingCourseResult_Header.Add(result_header);
                        dbcontext.SaveChanges();
                        ///=====================================================================
                        //======================================================================
                        ///=========add evalution header===============
                        var evalution_header = new TrainingCourceEvaluation_Header { Evaluation_Type = (Int16)used_for.course, details_ID = D.ID, CourseClassification_Code = D.CourseClassification_Code, Branch_Code = D.Branch_Code, Year = D.Year, TrainingType_Code = D.TrainingType_Code, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Course_Code = D.Course_Code, TrainingCenters_Code = D.TrainingCenters_Code };
                        var evalution_H = dbcontext.TrainingCourceEvaluation_Header.Add(evalution_header);
                        dbcontext.SaveChanges();
                        ///=======================================

                    }
                }
                //====

                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public ActionResult Edit_Details(int id)
        {
            ViewBag.type = dbcontext.TrainingType_Header.ToList().Select(m => new { Code = m.TrainingType_Code + "-[" + m.TrainingType_Desc + ']', ID = m.ID });
            ViewBag.classification = dbcontext.CourseClassification.ToList().Select(m => new { Code = m.CourseClassification_Code + "-[" + m.CourseClassification_Desc + ']', ID = m.ID });
            ViewBag.courses = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });
            ViewBag.instructors = dbcontext.Instructor.ToList().Select(m => new { Code = m.InstructorID + "-[" + m.Instructor_FullName + ']', ID = m.ID });
            var Details = dbcontext.TrainingPlan_Detail.FirstOrDefault(m => m.ID == id);
            var new_D = new VM_plan { TrainingPlan_Detail = Details, Course_status = (Course_status)Details.Course_Status };


            return View(new_D);
        }
        [HttpPost]
        public ActionResult Edit_Details(VM_plan model, FormCollection form, string command)
        {
            try
            {
                ViewBag.type = dbcontext.TrainingType_Header.ToList().Select(m => new { Code = m.TrainingType_Code + "-[" + m.TrainingType_Desc + ']', ID = m.ID });
                ViewBag.classification = dbcontext.CourseClassification.ToList().Select(m => new { Code = m.CourseClassification_Code + "-[" + m.CourseClassification_Desc + ']', ID = m.ID });
                ViewBag.courses = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });
                ViewBag.instructors = dbcontext.Instructor.ToList().Select(m => new { Code = m.InstructorID + "-[" + m.Instructor_FullName + ']', ID = m.ID });
                var Details = dbcontext.TrainingPlan_Detail.FirstOrDefault(m => m.ID == model.TrainingPlan_Detail.ID);
                ////==============
                var header = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == model.TrainingPlan_Detail.TrainingPlan_HeaderID);
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == header.status_ID);
                if (sta.statu == check_status.Approved || sta.statu == check_status.Rejected || sta.statu == check_status.Closed || sta.statu == check_status.Recervied || sta.statu == check_status.Canceled)
                {
                    if (command == "cost")
                    {
                        return RedirectToAction("coast", new { details_id = Details.ID });
                    }
                    else if (command == "employee")
                    {
                        return RedirectToAction("trainee", new { details_id = Details.ID });
                    }
                    else if (command == "result")
                    {
                        return RedirectToAction("result", new { details_id = Details.ID });
                    }
                    else if (command == "evalution")
                    {
                        return RedirectToAction("evalution", new { details_id = Details.ID });

                    }
                    else
                    {
                        TempData["message"] = HR.Resource.training.status_message;
                        return RedirectToAction("index");
                    }
                }
                //=================
                Details.CourseClassification_Code = model.TrainingPlan_Detail.CourseClassification_Code;
                if (Details.CourseClassification_Code != null)
                {
                    var ID_CLA = int.Parse(model.TrainingPlan_Detail.CourseClassification_Code);
                    var Cl = dbcontext.CourseClassification.FirstOrDefault(m => m.ID == ID_CLA);
                    Details.classification_des = Cl.CourseClassification_Code + "-" + Cl.CourseClassification_Desc;
                }
                Details.Course_Code = model.TrainingPlan_Detail.Course_Code;
                if (Details.Course_Code != null)
                {
                    var ID_cour = int.Parse(model.TrainingPlan_Detail.Course_Code);
                    var Cl = dbcontext.Cours.FirstOrDefault(m => m.ID == ID_cour);
                    Details.course_des = Cl.Course_Code + "-" + Cl.Course_Desc;
                }
                Details.InstructorID = model.TrainingPlan_Detail.InstructorID;
                if (Details.InstructorID != null)
                {
                    var ID_cour = int.Parse(model.TrainingPlan_Detail.InstructorID);
                    var Cl = dbcontext.Instructor.FirstOrDefault(m => m.ID == ID_cour);
                    Details.instructor_des = Cl.ID + "-" + Cl.Instructor_FullName;
                }



                Details.Number_Of_Days = model.TrainingPlan_Detail.Number_Of_Days;
                Details.Number_Of_Participant = model.TrainingPlan_Detail.Number_Of_Participant;
                Details.Total_Cost = model.TrainingPlan_Detail.Total_Cost;
                Details.Cost_Per_Person = model.TrainingPlan_Detail.Cost_Per_Person;
                Details.Course_Status = model.TrainingPlan_Detail.Course_Status;
                Details.From_Date = Convert.ToDateTime(model.TrainingPlan_Detail.From_Date);
                Details.To_Date = Convert.ToDateTime(model.TrainingPlan_Detail.To_Date);
                Details.Course_Status = (Int16)model.Course_status.GetHashCode();
                Details.stat_course = model.Course_status.ToString();
                var center = form["center_"].Split(',');
                var bransh = form["Bransh_"].Split(',');
                if (center.Count() > 1)
                {
                    Details.TrainingCenters_Code = center[1];
                    var ID_cour = int.Parse(Details.TrainingCenters_Code);
                    var Cl = dbcontext.Course_Centers.FirstOrDefault(m => m.ID == ID_cour);
                    Details.center_des = Cl.TrainingCenters_Code + "-" + Cl.center_des;
                }
                if (bransh.Count() > 1)
                {

                    Details.Branch_Code = bransh[1];

                    var ID_cour = int.Parse(Details.Branch_Code);
                    var Cl = dbcontext.TrainingCenters_Branch.FirstOrDefault(m => m.ID == ID_cour);
                    Details.bransh_des = Cl.Branch_Code + "-" + Cl.Branch_Desc;
                }

                dbcontext.SaveChanges();

                if (command == "cost")
                {
                    return RedirectToAction("coast", new { details_id = Details.ID });
                }
                else if (command == "employee")
                {
                    return RedirectToAction("trainee", new { details_id = Details.ID });
                }
                else if (command == "result")
                {
                    return RedirectToAction("result", new { details_id = Details.ID });
                }
                else if (command == "evalution")
                {
                    return RedirectToAction("evalution", new { details_id = Details.ID });

                }
                return RedirectToAction("edit", new { id = Details.TrainingPlan_HeaderID });
            }
            catch (Exception e)
            {
                return View(model);

            }

        }
        public ActionResult coast(int details_id)
        {
            ViewBag.costs = dbcontext.CostElement.ToList().Select(m => new { Code = m.CostElement_Code + "-[" + m.CostElement_Desc + ']', ID = m.ID });
            var details = dbcontext.TrainingPlan_Detail.FirstOrDefault(m => m.ID == details_id);
            return View(details);
        }
        [HttpPost]
        public ActionResult coast(TrainingPlan_Detail model, FormCollection form)
        {
            try
            {
                ViewBag.costs = dbcontext.CostElement.ToList().Select(m => new { Code = m.CostElement_Code + "-[" + m.CostElement_Desc + ']', ID = m.ID });

                ////==============
                var header = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == model.TrainingPlan_HeaderID);
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == header.status_ID);
                if (sta.statu == check_status.Approved || sta.statu == check_status.Rejected || sta.statu == check_status.Closed || sta.statu == check_status.Recervied || sta.statu == check_status.Canceled)
                {
                    TempData["message"] = HR.Resource.training.status_message;
                    return RedirectToAction("index");
                }
                //=================
                var details = dbcontext.TrainingPlan_Detail.FirstOrDefault(m => m.ID == model.ID);
                var total_amnount = 0;
                //================================delete old=====================
                var old_total_price = 0;
                var old_cost_header = dbcontext.Course_CostElement_Header.FirstOrDefault(m => m.details_id == model.ID);
                
                if (old_cost_header != null)
                {
                    var old_cost_details = dbcontext.Course_CostElement_Detail.Where(m => m.Course_CostElement_HeaderID == old_cost_header.ID).ToList();
                
                    dbcontext.Course_CostElement_Detail.RemoveRange(old_cost_details);
                    dbcontext.SaveChanges();

                }
                //================================add new========================
                var cost = form["center_id"].Split(',');
                var Amount = form["amount"].Split(',');
                var H = new Course_CostElement_Header();
                for (var i = 0; i < cost.Length; i++)
                {
                    if (cost[i] != "")
                    {
                        var ID = int.Parse(cost[i]);
                        var Co_ = dbcontext.CostElement.FirstOrDefault(m => m.ID == ID);
                        var D = new Course_CostElement_Detail { TrainingPlan_HeaderID = details.ID, CourseClassification_Code = details.CourseClassification_Code, Branch_Code = details.Branch_Code, Year = details.Year, TrainingType_Code = details.TrainingType_Code, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Course_Code = details.Course_Code, TrainingCenters_Code = details.TrainingCenters_Code, CostElement_Code = Co_.ID.ToString(), cost_des = Co_.CostElement_Code + "-" + Co_.CostElement_Desc, Course_CostElement_HeaderID = old_cost_header.ID, Amount = int.Parse(Amount[i].ToString()) };
                        dbcontext.Course_CostElement_Detail.Add(D);
                        dbcontext.SaveChanges();
                        total_amnount += int.Parse(Amount[i].ToString());
                        //===
                    }
                }
                var new_details = dbcontext.TrainingPlan_Detail.FirstOrDefault(m => m.ID == model.ID);
                var new_header = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == new_details.TrainingPlan_HeaderID);
                new_header.Total_Estimated_Cost = new_header.Total_Estimated_Cost - new_details.Total_Cost;
                new_details.Cost_Per_Person = total_amnount;
                new_details.Total_Cost = total_amnount * new_details.Number_Of_Participant;
                new_header.Total_Estimated_Cost = new_header.Total_Estimated_Cost + new_details.Total_Cost;
                new_header.Deviation =(int?)(new_header.Approved_Budget_Amount - new_header.Total_Estimated_Cost);
                //================================
                var Hheader = dbcontext.Course_CostElement_Header.FirstOrDefault(m => m.ID == old_cost_header.ID);
                Hheader.Cost_Per_Person = total_amnount;
                dbcontext.SaveChanges();
                //================================

                return RedirectToAction("Edit_Details", new { id = model.ID });
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
        public ActionResult trainee(int details_id)
        {
            ViewBag.employee = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
            var details = dbcontext.TrainingPlan_Detail.FirstOrDefault(m => m.ID == details_id);
            return View(details);
        }
        [HttpPost]
        public ActionResult trainee(TrainingPlan_Detail model, FormCollection form)
        {
            try
            {
                ////==============
                var header = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == model.TrainingPlan_HeaderID);
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == header.status_ID);
                if (sta.statu == check_status.Approved || sta.statu == check_status.Rejected || sta.statu == check_status.Closed || sta.statu == check_status.Recervied || sta.statu == check_status.Canceled)
                {
                    TempData["message"] = HR.Resource.training.status_message;
                    return RedirectToAction("index");
                }
                //=================
                ViewBag.employee = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
                var details = dbcontext.TrainingPlan_Detail.FirstOrDefault(m => m.ID == model.ID);
                var total_trainee = 0;
                //================================delete old=====================
                var old_trainee_header = dbcontext.TrainingParticipants_Header.FirstOrDefault(m => m.details_id == model.ID);
                if (old_trainee_header != null)
                {
                    var old_trainee_details = dbcontext.TrainingParticipants_Detail.Where(m => m.TrainingParticipants_HeaderID == old_trainee_header.ID).ToList();
                    dbcontext.TrainingParticipants_Detail.RemoveRange(old_trainee_details);
                    dbcontext.SaveChanges();

                }
                //================================add new========================
                //====================delete old result for old trainee==========
                var header_result = dbcontext.TrainingCourseResult_Header.FirstOrDefault(m => m.details_id == model.ID);
                if (header_result != null)
                {
                    var result_details = dbcontext.TrainingCourseResult_Detail.Where(m => m.header_ID == header_result.ID).ToList();
                    dbcontext.TrainingCourseResult_Detail.RemoveRange(result_details);
                    dbcontext.SaveChanges();

                }
                //===============================================================    
                var cost = form["center_id"].Split(',');
                for (var i = 0; i < cost.Length; i++)
                {
                    if (cost[i] != "")
                    {
                        var ID = int.Parse(cost[i]);
                        var Co_ = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
                        var D = new TrainingParticipants_Detail { TrainingPlan_HeaderID = details.ID, CourseClassification_Code = details.CourseClassification_Code, Branch_Code = details.Branch_Code, Year = details.Year, TrainingType_Code = details.TrainingType_Code, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Course_Code = details.Course_Code, TrainingCenters_Code = details.TrainingCenters_Code, Employee_Code = Co_.ID.ToString(), emp_des = Co_.Code + "-" + Co_.Full_Name, TrainingParticipants_HeaderID = old_trainee_header.ID };
                        dbcontext.TrainingParticipants_Detail.Add(D);
                        dbcontext.SaveChanges();
                        //==================add new result for this employee==================
                        var new_result = new TrainingCourseResult_Detail { Attended_Days = 0, Absent_Days = 0, Comments = "", Grade = 0, header_ID = header_result.ID, CourseClassification_Code = details.CourseClassification_Code, Branch_Code = details.Branch_Code, Year = details.Year, TrainingType_Code = details.TrainingType_Code, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Course_Code = details.Course_Code, TrainingCenters_Code = details.TrainingCenters_Code, Employee_Code = Co_.ID.ToString(), emp_des = Co_.Code + "-" + Co_.Full_Name };
                        dbcontext.TrainingCourseResult_Detail.Add(new_result);
                        dbcontext.SaveChanges();
                        //===================================================================
                        total_trainee += 1;
                        //===
                    }
                }
                var new_details = dbcontext.TrainingPlan_Detail.FirstOrDefault(m => m.ID == model.ID);
                var new_header = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == new_details.TrainingPlan_HeaderID);
                new_header.Total_Estimated_Cost = new_header.Total_Estimated_Cost - new_details.Total_Cost;
                new_details.Number_Of_Participant = (Int16)total_trainee;
                new_details.Total_Cost = new_details.Cost_Per_Person * new_details.Number_Of_Participant;
                new_header.Total_Estimated_Cost = new_header.Total_Estimated_Cost + new_details.Total_Cost;
                new_header.Deviation = (int?)(new_header.Approved_Budget_Amount - new_header.Total_Estimated_Cost);
                dbcontext.SaveChanges();
                ////================================
                //var Hheader = dbcontext.Course_CostElement_Header.FirstOrDefault(m => m.ID == old_cost_header.ID);
                //Hheader.pa = total_amnount;
                //dbcontext.SaveChanges();
                ////================================

                return RedirectToAction("Edit_Details", new { id = model.ID });
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

        public ActionResult evalution(int details_id)
        {
            ViewBag.employee = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
            ViewBag.element = dbcontext.TrainingCourceEvaluationElement.ToList().Select(m => new { Code = m.Element_Code + "-[" + m.Element_Desc + ']', ID = m.ID });

            var evalution_H = dbcontext.TrainingCourceEvaluation_Header.FirstOrDefault(m => m.details_ID == details_id);
            var D = dbcontext.TrainingPlan_Detail.FirstOrDefault(m => m.ID == evalution_H.details_ID);
            var add_model = new VM_evalution { TrainingPlan_Detail = D, TrainingCourceEvaluation_Header = evalution_H, evalution_type = (used_for)evalution_H.Evaluation_Type };
            return View(add_model);
        }
        [HttpPost]
        public ActionResult evalution(VM_evalution model, FormCollection form)
        {
            ViewBag.employee = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { Code = m.Code + "-[" + m.Full_Name + ']', ID = m.ID });
            ViewBag.element = dbcontext.TrainingCourceEvaluationElement.ToList().Select(m => new { Code = m.Element_Code + "-[" + m.Element_Desc + ']', ID = m.ID });
            ////==============
            var header = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == model.TrainingPlan_Detail.TrainingPlan_HeaderID);
            var sta = dbcontext.status.FirstOrDefault(m => m.ID == header.status_ID);
            if (sta.statu == check_status.Closed || sta.statu == check_status.Canceled)
            {
               if(sta.statu == check_status.Closed)
                {
                    TempData["message"] = HR.Resource.training.status_message1;
                    return RedirectToAction("index");
                }
               else if(sta.statu == check_status.Canceled)
                {
                    TempData["message"] = HR.Resource.training.status_message2;
                    return RedirectToAction("index");
                }
               
            }
            //=================
            try
            {
                var details = dbcontext.TrainingPlan_Detail.FirstOrDefault(m => m.ID == model.TrainingPlan_Detail.ID);
                var evalution_H = dbcontext.TrainingCourceEvaluation_Header.FirstOrDefault(m => m.ID == model.TrainingCourceEvaluation_Header.ID);
              
                evalution_H.Evaluation_Type = (Int16)model.evalution_type.GetHashCode();
                evalution_H.Employee_Code = model.TrainingCourceEvaluation_Header.Employee_Code;
                if(evalution_H.Employee_Code=="0"|| evalution_H.Employee_Code==null|| evalution_H.Employee_Code=="")
                {
                    return View(model);
                }
                    dbcontext.SaveChanges();
                //===================================
                //============delete old=============
                var old_evalution_details = dbcontext.TrainingCourceEvaluation_Details.Where(m => m.header_id == evalution_H.ID).ToList();
                dbcontext.TrainingCourceEvaluation_Details.RemoveRange(old_evalution_details);
                dbcontext.SaveChanges();
                //====================================
                //============add new=================
                var element = form["center_id__"].Split(',');
                var grade = form["G_id__"].Split(',');
                var y_n = form["Y_id__"].Split(',');
                var percentage = form["P__"].Split(',');
                for (var i = 0; i < element.Length; i++)
                {
                    if (element[i] != "")
                    {
                        var ID = int.Parse(element[i]);
                        var E_ = dbcontext.TrainingCourceEvaluationElement.FirstOrDefault(m => m.ID == ID);

                        var D = new TrainingCourceEvaluation_Details { Element_Code = E_.ID.ToString(), Employee_Code = model.TrainingCourceEvaluation_Header.Employee_Code, element_des = E_.Element_Code + "-" + E_.Element_Desc, header_id = model.TrainingCourceEvaluation_Header.ID, CourseClassification_Code = details.CourseClassification_Code, Branch_Code = details.Branch_Code, Year = details.Year, TrainingType_Code = details.TrainingType_Code, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Course_Code = details.Course_Code, TrainingCenters_Code = details.TrainingCenters_Code };
                        if (grade[i] != "" && grade[i] != "0")
                        {
                            D.Grade_Result = (Int16)int.Parse(grade[i]);
                            D.grade = ((vm_G)int.Parse(grade[i])).ToString();

                        }
                        else if (y_n[i] != "" && y_n[i] != "0")
                        {
                            D.YesOrNo_Result = (Int16)int.Parse(y_n[i]);
                            D.yes_or_no = ((y_nvm)int.Parse(y_n[i])).ToString();
                        }
                        else if (percentage[i] != "")
                        {
                            D.NumOrPercentage_Result = int.Parse(percentage[i]);
                        }
                        dbcontext.TrainingCourceEvaluation_Details.Add(D);
                        dbcontext.SaveChanges();

                    }
                }
                return RedirectToAction("Edit_Details", new { id = model.TrainingPlan_Detail.ID });
            }
            catch(Exception)
            {
                return View(model);
            }
        }
        public ActionResult result(int details_id)
        {
            ViewBag.type = dbcontext.TrainingType_Header.ToList().Select(m => new { Code = m.TrainingType_Code + "-[" + m.TrainingType_Desc + ']', ID = m.ID });
            ViewBag.classification = dbcontext.CourseClassification.ToList().Select(m => new { Code = m.CourseClassification_Code + "-[" + m.CourseClassification_Desc + ']', ID = m.ID });
            ViewBag.courses = dbcontext.Cours.ToList().Select(m => new { Code = m.Course_Code + "-[" + m.Course_Desc + ']', ID = m.ID });
            ViewBag.center = dbcontext.Course_Centers.ToList().Select(m => new { Code = m.TrainingCenters_Code + "-[" + m.center_des + ']', ID = m.ID });
            ViewBag.Bransh = dbcontext.TrainingCenters_Branch.ToList().Select(m => new { Code = m.Branch_Code + "-[" + m.Branch_Desc + ']', ID = m.ID });
            //=====================
            var R_header = dbcontext.TrainingCourseResult_Header.FirstOrDefault(m => m.details_id == details_id);
            return View(R_header);
        }
        [HttpPost]
        public ActionResult result(TrainingCourseResult_Header model, FormCollection form)
        {
            try
            {
                var header = dbcontext.TrainingCourseResult_Header.FirstOrDefault(m => m.ID == model.ID);
                ////==============
                var main_Details = dbcontext.TrainingPlan_Detail.FirstOrDefault(m => m.ID == model.details_id);
                var main_header = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == main_Details.TrainingPlan_HeaderID);
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == main_header.status_ID);
                if (sta.statu == check_status.Closed || sta.statu == check_status.Canceled)
                {
                    if (sta.statu == check_status.Closed)
                    {
                        TempData["message"] = HR.Resource.training.status_message1;
                        return RedirectToAction("index");
                    }
                    else if (sta.statu == check_status.Canceled)
                    {
                        TempData["message"] = HR.Resource.training.status_message2;
                        return RedirectToAction("index");
                    }
                }
                header.From_Date = model.From_Date;
                header.To_Date = model.To_Date;
                header.Number_Of_Days = model.Number_Of_Days;
                dbcontext.SaveChanges();
                //===============================================
                var details = dbcontext.TrainingCourseResult_Detail.Where(m => m.header_ID == header.ID).ToList();

                dbcontext.TrainingCourseResult_Detail.RemoveRange(details);
                dbcontext.SaveChanges();
                //===============================================
                var emp_id = form["emp_id__"].Split(',');
                var attend = form["Attended_Days__"].Split(',');
                var absent = form["absent__"].Split(',');
                var grade = form["grade__"].Split(',');
                var comment = form["Comment__"].Split(',');

                for (var i = 0; i < emp_id.Length; i++)
                {
                    if (emp_id[i] != "")
                    {
                        var ID = int.Parse(emp_id[i]);
                        var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
                        var result = new TrainingCourseResult_Detail { header_ID = header.ID, CourseClassification_Code = header.CourseClassification_Code, Branch_Code = header.Branch_Code, Year = header.Year, TrainingType_Code = header.TrainingType_Code, Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, Course_Code = header.Course_Code, TrainingCenters_Code = header.TrainingCenters_Code, Employee_Code = emp.ID.ToString(), emp_des = emp.Code + "-" + emp.Full_Name };
                        if (attend[i] != "")
                        {
                            result.Attended_Days = int.Parse(attend[i]);
                        }
                        if (absent[i] != "")
                        {
                            result.Absent_Days = int.Parse(absent[i]);
                        }
                        if (grade[i] != "")
                        {
                            result.Grade = int.Parse(grade[i]);
                        }
                        if (comment[i] != "")
                        {
                            result.Comments = comment[i];
                        }
                        dbcontext.TrainingCourseResult_Detail.Add(result);
                        dbcontext.SaveChanges();
                        //===
                    }
                }
                return RedirectToAction("Edit_Details", new { id = header.details_id });

            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public ActionResult delete(int id)
        {
            try
            {
                var model = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public ActionResult delete_method(int id)
        {
           
            //=================
            var plan_header = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == id);
            var plan_details = dbcontext.TrainingPlan_Detail.Where(m => m.TrainingPlan_HeaderID == plan_header.ID).ToList();
            var sta = dbcontext.status.FirstOrDefault(m => m.ID == plan_header.status_ID);

            if (sta.statu == check_status.Approved || sta.statu == check_status.Rejected || sta.statu == check_status.Closed || sta.statu == check_status.Recervied || sta.statu == check_status.Canceled)
            {
                TempData["message"] = HR.Resource.training.status_message;
                return RedirectToAction("index");
            }
            try
            {
                //=====================================================================================
                foreach (var Details in plan_details)
                {
                    var header_plan = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == Details.TrainingPlan_HeaderID);
                    var header_cost = dbcontext.Course_CostElement_Header.FirstOrDefault(m => m.details_id == Details.ID);
                    var deatils_cost = dbcontext.Course_CostElement_Detail.Where(m => m.Course_CostElement_HeaderID == header_cost.ID).ToList();
                    var header_trainee = dbcontext.TrainingParticipants_Header.FirstOrDefault(m => m.details_id == Details.ID);
                    var details_trainee = dbcontext.TrainingParticipants_Detail.Where(m => m.TrainingParticipants_HeaderID == header_trainee.ID).ToList();
                    var header_result = dbcontext.TrainingCourseResult_Header.FirstOrDefault(m => m.details_id == Details.ID);
                    var details_result = dbcontext.TrainingCourseResult_Detail.Where(m => m.header_ID == header_result.ID).ToList();
                    var header_evalution = dbcontext.TrainingCourceEvaluation_Header.FirstOrDefault(m => m.details_ID == Details.ID);
                    var details_Evalution = dbcontext.TrainingCourceEvaluation_Details.Where(m => m.header_id == header_evalution.ID);
                    var status = dbcontext.status.FirstOrDefault(m => m.ID == header_plan.status_ID);
                    //=====
                    dbcontext.Course_CostElement_Header.Remove(header_cost);
                    dbcontext.Course_CostElement_Detail.RemoveRange(deatils_cost);
                    //=====
                    dbcontext.TrainingParticipants_Header.Remove(header_trainee);
                    dbcontext.TrainingParticipants_Detail.RemoveRange(details_trainee);
                    //=====
                    dbcontext.TrainingCourseResult_Header.Remove(header_result);
                    dbcontext.TrainingCourseResult_Detail.RemoveRange(details_result);
                    //=====
                    dbcontext.TrainingCourceEvaluation_Header.Remove(header_evalution);
                    dbcontext.TrainingCourceEvaluation_Details.RemoveRange(details_Evalution);
                    //=====
                    dbcontext.status.Remove(status);
                    //=====
                    dbcontext.SaveChanges();
                }
                //=======================================================================================
                dbcontext.TrainingPlan_Header.Remove(plan_header);
                dbcontext.TrainingPlan_Detail.RemoveRange(plan_details);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (Exception)
            {
                return View(plan_header);
            }
        }
        public ActionResult delete_details(int id)
        {
            try
            {
                var model = dbcontext.TrainingPlan_Detail.FirstOrDefault(m => m.ID == id);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        [ActionName("delete_details")]
        public ActionResult delete_details_(int id)
        {
            ////==============
            var D = dbcontext.TrainingPlan_Detail.FirstOrDefault(m => m.ID == id);
            var headerr = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == D.TrainingPlan_HeaderID);
            var sta = dbcontext.status.FirstOrDefault(m => m.ID == headerr.status_ID);
            if (sta.statu == check_status.Approved || sta.statu == check_status.Rejected || sta.statu == check_status.Closed || sta.statu == check_status.Recervied || sta.statu == check_status.Canceled)
            {

                TempData["message"] = HR.Resource.training.status_message;
                return RedirectToAction("index");
            }
            //=================
            var Details = dbcontext.TrainingPlan_Detail.FirstOrDefault(m => m.ID == id);
            var header_plan = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == Details.TrainingPlan_HeaderID);
            var header_cost = dbcontext.Course_CostElement_Header.FirstOrDefault(m => m.details_id == Details.ID);
            var deatils_cost = dbcontext.Course_CostElement_Detail.Where(m => m.Course_CostElement_HeaderID == header_cost.ID).ToList();
            var header_trainee = dbcontext.TrainingParticipants_Header.FirstOrDefault(m => m.details_id == Details.ID);
            var details_trainee = dbcontext.TrainingParticipants_Detail.Where(m => m.TrainingParticipants_HeaderID == header_trainee.ID).ToList();
            var header_result = dbcontext.TrainingCourseResult_Header.FirstOrDefault(m => m.details_id == Details.ID);
            var details_result = dbcontext.TrainingCourseResult_Detail.Where(m => m.header_ID == header_result.ID).ToList();
            var header_evalution = dbcontext.TrainingCourceEvaluation_Header.FirstOrDefault(m => m.details_ID == Details.ID);
            var details_Evalution = dbcontext.TrainingCourceEvaluation_Details.Where(m => m.header_id == header_evalution.ID);
           
            var header = Details.TrainingPlan_HeaderID;
            //====
            header_plan.Total_Estimated_Cost = header_plan.Total_Estimated_Cost - Details.Total_Cost;
            header_plan.Deviation =(int?) (header_plan.Approved_Budget_Amount - header_plan.Total_Estimated_Cost);
            dbcontext.SaveChanges();
            //=====
            

            try
            {
                dbcontext.TrainingPlan_Detail.Remove(Details);
                //=====
                dbcontext.Course_CostElement_Header.Remove(header_cost);
                dbcontext.Course_CostElement_Detail.RemoveRange(deatils_cost);
                //=====
                dbcontext.TrainingParticipants_Header.Remove(header_trainee);
                dbcontext.TrainingParticipants_Detail.RemoveRange(details_trainee);
                //=====
                dbcontext.TrainingCourseResult_Header.Remove(header_result);
                dbcontext.TrainingCourseResult_Detail.RemoveRange(details_result);
                //=====
                dbcontext.TrainingCourceEvaluation_Header.Remove(header_evalution);
                dbcontext.TrainingCourceEvaluation_Details.RemoveRange(details_Evalution);
                //=====
                dbcontext.SaveChanges();
                return RedirectToAction("edit", new { id = header });
            }
            catch (Exception)
            {
                return View(Details);
            }
        }

        public ActionResult status(int id)
        {
            ViewBag.header_id = id;
            var hearder = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == id);
            var my_model = dbcontext.status.FirstOrDefault(m => m.ID == hearder.status_ID);
         
            if (my_model.approved_by == null)
                my_model.approved_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            if (my_model.Rejected_by == null)
                my_model.Rejected_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            if (my_model.return_to_reviewby == null)
                my_model.return_to_reviewdate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            if (my_model.cancaled_by == null)
                my_model.cancaled_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            if (my_model.closed_by == null)
                my_model.closed_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            if (my_model.Recervied_by == null)
                my_model.Recervied_bydate = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            return View(my_model);
        }
        [HttpPost]
        public ActionResult status(status model, int header_id2)
        {
            ViewBag.header_id = header_id2;
            var record = dbcontext.TrainingPlan_Header.FirstOrDefault(m => m.ID == header_id2);
            if (model.statu == check_status.Approved)
            {

                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.status_ID);
                sta.approved_by = User.Identity.Name;
                sta.approved_bydate = model.approved_bydate;
                sta.statu = check_status.Approved;
                dbcontext.SaveChanges();
                record.Status = (Int16)check_status.Approved;
                record.statu = check_status.Approved;
                dbcontext.SaveChanges();
            }
            else if (model.statu == check_status.Closed)
            {
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.status_ID);

                sta.closed_by = User.Identity.Name;
                sta.closed_bydate = model.closed_bydate;
                sta.statu = check_status.Closed;
                dbcontext.SaveChanges();
                record.Status = (Int16)check_status.Closed;
                record.statu = check_status.Closed;
                dbcontext.SaveChanges();
               

            }
            else if (model.statu == check_status.Canceled)
            {
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.status_ID);

                sta.cancaled_by = User.Identity.Name;
                sta.cancaled_bydate = model.cancaled_bydate;
                sta.statu = check_status.Canceled;
                dbcontext.SaveChanges();
                record.Status = (Int16)check_status.Canceled;
                record.statu = check_status.Canceled;
                dbcontext.SaveChanges();

            }
            else if (model.statu == check_status.Recervied)
            {
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.status_ID);

                sta.Recervied_by = User.Identity.Name;
                sta.Recervied_bydate = model.Recervied_bydate;
                sta.statu = check_status.Recervied;
                dbcontext.SaveChanges();
                record.Status = (Int16)check_status.Recervied;
                record.statu = check_status.Recervied;
                dbcontext.SaveChanges();
                

            }
            else if (model.statu == check_status.Rejected)
            {
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.status_ID);
                sta.Rejected_by = User.Identity.Name;
                sta.Rejected_bydate = model.approved_bydate;
                sta.statu = check_status.Rejected;
                dbcontext.SaveChanges();
                record.Status = (Int16)check_status.Rejected;
                record.statu = check_status.Rejected;
                dbcontext.SaveChanges();

            }
            else if (model.statu == check_status.Return_To_Review)
            {
                var sta = dbcontext.status.FirstOrDefault(m => m.ID == record.status_ID);
                sta.return_to_reviewby = User.Identity.Name;
                sta.return_to_reviewdate = model.approved_bydate;
                sta.statu = check_status.Return_To_Review;
                dbcontext.SaveChanges();
                record.Status = (Int16)check_status.Return_To_Review;
                record.statu = check_status.Return_To_Review;
                dbcontext.SaveChanges();

            }

            return RedirectToAction("index");
        }
        //====================ajax============
        public JsonResult getcenters(string courses_id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var centers = dbcontext.Course_Centers.Where(m => m.Course_Code == courses_id).ToList().Select(m => new { Code = m.TrainingCenters_Code + "-[" + m.center_des + ']', ID = m.ID }).ToList();
            return Json(centers);
        }
        public JsonResult getbranshes(int center_id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var centers = dbcontext.Course_Centers.Where(m => m.ID == center_id).FirstOrDefault();
            var branshes = dbcontext.TrainingCenters_Branch.Where(m => m.TrainingCenters_Code == centers.TrainingCenters_Code).ToList().Select(m => new { Code = m.Branch_Code + "-[" + m.Branch_Desc + ']', ID = m.ID }).ToList();
            return Json(branshes);
        }
        public JsonResult getallcontent(int header_id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var headers = dbcontext.TrainingPlan_Detail.Where(m => m.TrainingPlan_HeaderID == header_id).ToList();
            return Json(headers);
        }
        public JsonResult getallcost(int details_id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var cost_H = dbcontext.Course_CostElement_Header.FirstOrDefault(m => m.details_id == details_id);
            var coast_D = dbcontext.Course_CostElement_Detail.Where(m => m.Course_CostElement_HeaderID == cost_H.ID).ToList();
            return Json(coast_D);
        }
        public JsonResult getalltrainee(int details_id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var trainee_H = dbcontext.TrainingParticipants_Header.FirstOrDefault(m => m.details_id == details_id);
            var trainee_D = dbcontext.TrainingParticipants_Detail.Where(m => m.TrainingParticipants_HeaderID == trainee_H.ID).ToList();
            return Json(trainee_D);
        }
        public JsonResult getallresult(int header_id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var result_D = dbcontext.TrainingCourseResult_Detail.Where(m => m.header_ID == header_id).ToList();
            return Json(result_D);
        }
        public JsonResult gettype(int element_id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var element = dbcontext.TrainingCourceEvaluationElement.FirstOrDefault(m => m.ID == element_id).Element_Type;
            return Json(element);
        }
        public JsonResult getallevalution(int header_id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var result_D = dbcontext.TrainingCourceEvaluation_Details.Where(m => m.header_id == header_id).ToList();
            return Json(result_D);
        }
    }
    public class VM_trainingplan
    {
        public TrainingPlan_Header TrainingPlan_Header { get; set; }
        public TrainingPlan_Detail TrainingPlan_Detail { get; set; }
        public Course_status Course_status { get; set; }
    }
    public enum Course_status
    {
        planed = 1,
        Released = 2,
        Canceled = 3,
        Hold = 4,
        Finished = 5
    }
    public class VM_plan
    {
        public Course_status Course_status { get; set; }
        public TrainingPlan_Detail TrainingPlan_Detail { get; set; }
    }
    public class VM_evalution
    {
        public TrainingCourceEvaluation_Header TrainingCourceEvaluation_Header { get; set; }
        public TrainingPlan_Detail TrainingPlan_Detail { get; set; }
        public used_for evalution_type { get; set; }

    }
    public enum vm_G
    {
        excellent=1,
        verygood=2,
        good=3,
        middle=4,
        low=5,
    }
    public enum y_nvm
    {
        Yes=1,
        No=2,
    }

}