using HR.Models;
using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class Reports_RecuirtmentController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Reports_Recuirtment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Recuirtment()
        {
            try
            {
                var model = new Report_RecuirtmentVM();
                var Applicant_Profile = dbcontext.Applicant_Profile.ToList();

                ViewBag.Full = Applicant_Profile.Select(m => new { Code = m.Code + "------[" + m.Full + ']', ID = m.ID });
                ViewBag.Full_Name = Applicant_Profile.Select(m => new { Code = m.Code + "------[" + m.Full_Name + ']', ID = m.ID });
                ViewBag.code = Applicant_Profile.Select(m => new { m.Code });
                //ViewBag.names = Applicant_Profile.Select(m => new { m.name });
                ViewBag.nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                //ViewBag.parent = Applicant_Profile.Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                //ViewBag.sub_class = dbcontext.Job_title_sub_class.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                //ViewBag.chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ViewBag.Religion = dbcontext.Religion.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Recuirtment(Report_RecuirtmentVM record, FormCollection model)
        {
            try
            {
                List<Applicant_Profile> jobForReport = new List<Applicant_Profile>();
                /////////////////////////////////////
                var Applicant_Profile = dbcontext.Applicant_Profile.ToList();
                ViewBag.Full = Applicant_Profile.Select(m => new { Code = m.Code + "------[" + m.Full + ']', ID = m.ID });
                ViewBag.Full_Name = Applicant_Profile.Select(m => new { Code = m.Code + "------[" + m.Full_Name + ']', ID = m.ID });
                ViewBag.code = Applicant_Profile.Select(m => new { m.Code });
                //ViewBag.names = Applicant_Profile.Select(m => new { m.name });
                ViewBag.nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                //ViewBag.parent = Applicant_Profile.Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                //ViewBag.sub_class = dbcontext.Job_title_sub_class.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                //ViewBag.chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ViewBag.Religion = dbcontext.Religion.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.job_setup = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                /////////////////////////////////////
                var nationality = model["nationality"].Split(char.Parse(","));
                var Full_Name = model["Full_Name"].Split(char.Parse(","));
                ///  var name = model["name"].Split(char.Parse(","));
                var Full = model["Full"].Split(char.Parse(","));
                var Blood_group = model["Blood_group"].Split(char.Parse(","));
                var Religion = model["Religion"].Split(char.Parse(","));
                //var gender = model["gender_list"].Split(char.Parse(","));
                ///     var sub_class = model["sub_class"].Split(char.Parse(","));///////
                //var parmanet = model["parmet_List"].Split(char.Parse(","));
                //var validity = model["validity_list"].Split(char.Parse(","));
                var List_Display = model["List_Display"].Split(char.Parse(","));

                ////////parent//////
                foreach (var item in Full_Name)
                {
                    if (item != "")
                    {
                        var parentID = int.Parse(item);
                        var newRecord = Applicant_Profile.FirstOrDefault(m => m.ID == parentID);
                        if (newRecord != null && !jobForReport.Contains(newRecord))
                        {
                            jobForReport.Add(newRecord);
                        }

                    }
                }
                /////job level//////
                foreach (var item in Full)
                {
                    //  var joblevelID = int.Parse(item);
                    if (item != "")
                    {
                        var newRecord = Applicant_Profile.FirstOrDefault(m => m.ID.ToString() == item);
                        if (newRecord != null && !jobForReport.Contains(newRecord))
                        {
                            jobForReport.Add(newRecord);
                        }

                    }
                }
                /////organiztion chart//////
                //foreach (var item in chart)
                //{
                //    //  var joblevelID = int.Parse(item);
                //    if (item != "")
                //    {
                //        var newRecord = Applicant_Profile.FirstOrDefault(m => m.Organization_unit_codeID == item);
                //        if (newRecord != null && !jobForReport.Contains(newRecord))
                //        {
                //            jobForReport.Add(newRecord);
                //        }

                //    }
                //}
                /////nationality//////
                foreach (var item in nationality)
                {


                    if (item != "")
                    {
                        var newRecord = Applicant_Profile.FirstOrDefault(m => m.NationalityId.ToString() == item);
                        if (newRecord != null && !jobForReport.Contains(newRecord))
                        {
                            jobForReport.Add(newRecord);
                        }
                    }
                }
                /////working_system//////
                foreach (var item in Blood_group)
                {
                    if (item != "")
                    {
                        var workingsystemID = int.Parse(item);
                        var newRecord = Applicant_Profile.FirstOrDefault(m => m.Blood_group.GetHashCode() == workingsystemID);
                        if (newRecord != null && !jobForReport.Contains(newRecord))
                        {
                            jobForReport.Add(newRecord);
                        }
                    }
                }
                /////validity//////
                //foreach (var item in validity)
                //{
                //    if (item != "")
                //    {
                //        var validityID = int.Parse(item);
                //        var newRecord = Alljobs.FirstOrDefault(m => m.validity.GetHashCode() == validityID);
                //        if (newRecord != null && !jobForReport.Contains(newRecord))
                //        {
                //            jobForReport.Add(newRecord);
                //        }
                //    }
                //}
                ///////parment//////
                //foreach (var item in parmanet)
                //{
                //    if (item != "")
                //    {
                //        var parmentID = int.Parse(item);
                //        var newRecord = Alljobs.FirstOrDefault(m => m.parment.GetHashCode() == parmentID);
                //        if (newRecord != null && !jobForReport.Contains(newRecord))
                //        {
                //            jobForReport.Add(newRecord);
                //        }
                //    }
                //}
                ///////gender//////
                ////foreach (var item in gender)
                ////{
                ////    if (item != "")
                ////    {
                ////        var genderID = int.Parse(item);
                ////        var newRecord = Alljobs.FirstOrDefault(m => m.gender.GetHashCode() == genderID);
                ////        if (newRecord != null && !jobForReport.Contains(newRecord))
                ////        {
                ////            jobForReport.Add(newRecord);
                ////        }
                ////    }
                ////}
                /////////////fromage//////
                //var newRecord2 = Alljobs.FirstOrDefault(m => m.from_age == record.fromAge);
                //if (newRecord2 != null && !jobForReport.Contains(newRecord2))
                //{
                //    jobForReport.Add(newRecord2);
                //}
                /////////////toage////////
                //newRecord2 = Alljobs.FirstOrDefault(m => m.to_age == record.toAge);
                //if (newRecord2 != null && !jobForReport.Contains(newRecord2))
                //{
                //    jobForReport.Add(newRecord2);
                //}
                /////////////numslot////////
                //newRecord2 = Alljobs.FirstOrDefault(m => m.num_slots == record.numSlot);
                //if (newRecord2 != null && !jobForReport.Contains(newRecord2))
                //{
                //    jobForReport.Add(newRecord2);
                //}
                var flag1 = new Boolean[15];
                for (var i = 0; i < 15; i++)
                {
                    flag1[i] = false;
                }
                foreach (var item in List_Display)
                {
                    if (item != "")
                    {
                        var index = int.Parse(item);
                        flag1[index] = true;
                    }
                }
                /////////////////////////
                //////////////////////////
                //////////////////////////
                //foreach (var item in jobForReport) { if (item.parent_job != null) { var id = int.Parse(item.parent_job); item.parent_job = Alljobs.FirstOrDefault(m => m.ID == id).name; } }
                var report = new ReportRc_VM { jobs = jobForReport, my_list = flag1 };
                return View("viewReport", report);
            }
            catch (Exception)
            {
                return View(record);
            }
        }
        /// <summary>
        /// ////Report to job  
        /// </summary>
        /// <param name="record"> jobs</param>
        /// <param name="flag"> list of display column</param>
        /// <returns></returns>
        public ActionResult viewReport(List<Applicant_Profile> record, Boolean[] flag)
        {
            var model = new ReportRc_VM { jobs = record, my_list = flag };
            try
            {

                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }
        }

    }
}