using HR.Models;
using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers 
{
   
    public class Reports_organizationController : BaseController
    {
        // GET: Reports_organization
        ApplicationDbContext dbcontext = new ApplicationDbContext(); 
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Organization,OrganizationReport,jobs title_report")]
        public ActionResult job_title()
        {
            try
            {
                var model = new Report_JobVM();
                var Alljobs = dbcontext.job_title_cards.ToList();

                ViewBag.jobs = Alljobs.Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.codes = Alljobs.Select(m => new { m.Code });
                ViewBag.names = Alljobs.Select(m => new { m.name });
                ViewBag.nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.parent = Alljobs.Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.sub_class = dbcontext.Job_title_sub_class.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ViewBag.job_setup =dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                return View(model);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult job_title(Report_JobVM record, FormCollection model)
        {
            try
            {
                List<job_title_cards> jobForReport = new List<job_title_cards>();
                /////////////////////////////////////
                var Alljobs = dbcontext.job_title_cards.ToList();
                ViewBag.jobs = Alljobs.Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                ViewBag.codes = Alljobs.Select(m => new { m.Code });
                ViewBag.names = Alljobs.Select(m => new { m.name });


                ViewBag.nationality = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
              
                ViewBag.parent = Alljobs.Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
         //       ViewBag.sub_class = dbcontext.Job_title_sub_class.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.chart = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
                ViewBag.job_setup = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                /////////////////////////////////////
                var nationality = model["nationality"].Split(char.Parse(","));
                var job_setup = model["job_setup"].Split(char.Parse(","));
              ///  var name = model["name"].Split(char.Parse(","));
                var working_system = model["working_system_List"].Split(char.Parse(","));
                var chart = model["chart"].Split(char.Parse(","));
                var parent = model["parent"].Split(char.Parse(","));
                //var gender = model["gender_list"].Split(char.Parse(","));
                ///     var sub_class = model["sub_class"].Split(char.Parse(","));///////
                var parmanet = model["parmet_List"].Split(char.Parse(","));
                var validity = model["validity_list"].Split(char.Parse(","));
                var List_Display = model["List_Display"].Split(char.Parse(","));

                ////////parent//////
                foreach(var item in parent)
                {
                    if (item != "" )
                    {
                        var parentID = int.Parse(item);
                        var newRecord = Alljobs.FirstOrDefault(m => m.ID == parentID);
                        if(newRecord!=null && !jobForReport.Contains(newRecord))
                        {
                            jobForReport.Add(newRecord);
                        }
                       
                    }
                }
                /////job level//////
                foreach (var item in job_setup)
                {
                    //  var joblevelID = int.Parse(item);
                    if (item != "")
                    {
                        var newRecord = Alljobs.FirstOrDefault(m => m.joblevelsetupID == item);
                        if(newRecord!=null && !jobForReport.Contains(newRecord))
                        {
                            jobForReport.Add(newRecord);
                        }
                      
                    }
                }
                /////organiztion chart//////
                foreach (var item in chart)
                {
                    //  var joblevelID = int.Parse(item);
                    if (item != "")
                    {
                        var newRecord = Alljobs.FirstOrDefault(m => m.Organization_unit_codeID == item);
                        if(newRecord!= null && !jobForReport.Contains(newRecord))
                        {
                            jobForReport.Add(newRecord);
                        }
                       
                    }
                }
                /////nationality//////
                foreach (var item in nationality)
                {
                    

                    if (item != "")
                    {
                        var newRecord = Alljobs.FirstOrDefault(m => m.nationalityID == item);
                        if (newRecord != null && !jobForReport.Contains(newRecord))
                        {
                            jobForReport.Add(newRecord);
                        }
                    }
                }
                /////working_system//////
                foreach (var item in working_system)
                {
                    if (item != "")
                    {
                        var workingsystemID = int.Parse(item);
                        var newRecord = Alljobs.FirstOrDefault(m => m.working_system.GetHashCode() == workingsystemID);
                        if(newRecord != null && !jobForReport.Contains(newRecord))
                        {
                            jobForReport.Add(newRecord);
                        }
                    }
                }
                /////validity//////
                foreach (var item in validity)
                {
                    if (item != "")
                    {
                        var validityID = int.Parse(item);
                        var newRecord = Alljobs.FirstOrDefault(m => m.validity.GetHashCode() == validityID);
                        if (newRecord != null && !jobForReport.Contains(newRecord))
                        {
                            jobForReport.Add(newRecord);
                        }
                    }
                }
                /////parment//////
                foreach (var item in parmanet)
                {
                    if (item != "")
                    {
                        var parmentID = int.Parse(item);
                        var newRecord = Alljobs.FirstOrDefault(m => m.parment.GetHashCode() == parmentID);
                        if (newRecord != null && !jobForReport.Contains(newRecord))
                        {
                            jobForReport.Add(newRecord);
                        }
                    }
                }
                /////gender//////
                //foreach (var item in gender)
                //{
                //    if (item != "")
                //    {
                //        var genderID = int.Parse(item);
                //        var newRecord = Alljobs.FirstOrDefault(m => m.gender.GetHashCode() == genderID);
                //        if (newRecord != null && !jobForReport.Contains(newRecord))
                //        {
                //            jobForReport.Add(newRecord);
                //        }
                //    }
                //}
                ///////////fromage//////
                var newRecord2 = Alljobs.FirstOrDefault(m => m.from_age== record.fromAge);
                if (newRecord2 != null && !jobForReport.Contains(newRecord2))
                {
                    jobForReport.Add(newRecord2);
                }
                ///////////toage////////
                 newRecord2 = Alljobs.FirstOrDefault(m => m.to_age == record.toAge);
                if (newRecord2 != null && !jobForReport.Contains(newRecord2))
                {
                    jobForReport.Add(newRecord2);
                }
                ///////////numslot////////
                newRecord2 = Alljobs.FirstOrDefault(m => m.num_slots == record.numSlot);
                if (newRecord2 != null && !jobForReport.Contains(newRecord2))
                {
                    jobForReport.Add(newRecord2);
                }
                var flag1=new Boolean[15];
                for(var i=0;i<15;i++)
                {
                    flag1[i] = false;
                }
                foreach(var item in List_Display)
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
                foreach (var item in jobForReport) { if (item.parent_job != null) { var id = int.Parse(item.parent_job); item.parent_job = Alljobs.FirstOrDefault(m => m.ID == id).name; } }
                var report = new reportJobVm { jobs = jobForReport, my_list = flag1 };
                return View("viewReport",report);
            }
            catch(Exception)
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
        public ActionResult viewReport(List<job_title_cards> record,Boolean[]flag)
        {
            var model = new reportJobVm { jobs = record, my_list = flag };
            try
            {
             
                return View(model);
            }
            catch(Exception)
            {
                return View(model);
            }
        }
        [Authorize(Roles = "Admin,Organization,OrganizationReport,organization chart_report")]
        public ActionResult Chart()
        {
            try
            {
                var Allchart = dbcontext.Organization_Chart.ToList();
                ViewBag.userUnitCode = Allchart.Select(m => new { code=m.User_unit_code});
                ViewBag.unitDescription = Allchart.Select(m => new { code=m.unit_Description });
                ViewBag.parent = Allchart.Select(m => new { code = m.unit_Description, id = m.ID });
                ViewBag.unitTypeCode = dbcontext.Organization_Unit_Type.ToList().Select(m => new { code = m.Name, id = m.ID });
                ViewBag.unitLevelCode = dbcontext.Organization_Unit_Level.ToList().Select(m => new { code = m.Name, id = m.ID });
                ViewBag.unitSchema = dbcontext.Organization_Unit_Schema.ToList().Select(m => new { code = m.Name, id = m.ID });
                ViewBag.location = dbcontext.work_location.ToList().Select(m => new { code = m.Name, id = m.ID });
                var Model = new OrganizationChartReportVM();
                return View(Model);

            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Chart(OrganizationChartReportVM record, FormCollection model)
        {
            try
            {
                var Allchart = dbcontext.Organization_Chart.ToList();
                ViewBag.userUnitCode = Allchart.Select(m => new { code = m.User_unit_code });
                ViewBag.unitDescription = Allchart.Select(m => new { code = m.unit_Description });
                ViewBag.parent = Allchart.Select(m => new { code = m.unit_Description, id = m.ID });
                ViewBag.unitTypeCode = dbcontext.Organization_Unit_Type.ToList().Select(m => new { code = m.Name, id = m.ID });
                ViewBag.unitLevelCode = dbcontext.Organization_Unit_Level.ToList().Select(m => new { code = m.Name, id = m.ID });
                ViewBag.unitSchema = dbcontext.Organization_Unit_Schema.ToList().Select(m => new { code = m.Name, id = m.ID });
                ViewBag.location = dbcontext.work_location.ToList().Select(m => new { code = m.Name, id = m.ID });

                /////////////////////////////////////////////
                ////////////////////////////////////////////
                var organizationChartList = new List<Organization_Chart>();
                var display = new Boolean[12];
                var report = new reportVM ();
                var userUnit = model["userUnit"].Split(char.Parse(","));
                var unitDescription = model["unitDescription"].Split(char.Parse(","));
                var parent = model["parent"].Split(char.Parse(","));
                var UnitType = model["UnitType"].Split(char.Parse(","));
                var unitlevel = model["unitlevel"].Split(char.Parse(","));
                var unitSchema = model["unitSchema"].Split(char.Parse(","));
                var loaction = model["loaction"].Split(char.Parse(","));
                var check_status = model["check_status"].Split(char.Parse(","));
                var List_Display = model["List_Display"].Split(char.Parse(","));
                /////////////////////////////////////////////
                /////////////////////////////////////////////
                for (var i=0;i<12;i++) { display[i] =false; }
                foreach (var item in userUnit) { if (item != "") { var newRecord = Allchart.FirstOrDefault(m => m.User_unit_code == item);if (newRecord != null && !organizationChartList.Contains(newRecord)) { organizationChartList.Add(newRecord);}}}
                foreach (var item in unitDescription) { if (item != "") { var newRecord = Allchart.FirstOrDefault(m => m.unit_Description == item); if (newRecord != null && !organizationChartList.Contains(newRecord)) { organizationChartList.Add(newRecord); } } }
                foreach (var item in parent) { if (item != "") {  var newRecord = Allchart.FirstOrDefault(m => m.parent == item); if (newRecord != null && !organizationChartList.Contains(newRecord)) { organizationChartList.Add(newRecord); } } }
                foreach (var item in UnitType) { if (item != "") { var id = int.Parse(item); var newRecord = Allchart.FirstOrDefault(m => m.unit_type_code.ID == id); if (newRecord != null && !organizationChartList.Contains(newRecord)) { organizationChartList.Add(newRecord); } } }
                foreach (var item in unitlevel) { if (item != "") { int unit_id = int.Parse(item); var newRecord = Allchart.FirstOrDefault(m => m.unit_type_code.Organization_Unit_LevelID == unit_id); if (newRecord != null && !organizationChartList.Contains(newRecord)) { organizationChartList.Add(newRecord); } } }
                foreach (var item in unitSchema) { if (item != "") { int schemaID = int.Parse(item); var newRecord = Allchart.FirstOrDefault(m => m.unit_type_code.Organization_Unit_SchemaID == schemaID); if (newRecord != null && !organizationChartList.Contains(newRecord)) { organizationChartList.Add(newRecord); } } }
                foreach (var item in loaction) { if (item != "") { var id = int.Parse(item); var newRecord = Allchart.FirstOrDefault(m => m.work_location.ID == id); if (newRecord != null && !organizationChartList.Contains(newRecord)) { organizationChartList.Add(newRecord); } } }
                foreach (var item in check_status) { if (item != "") { var id = int.Parse(item); var newRecord = Allchart.FirstOrDefault(m => m.unit_status.GetHashCode() == id); if (newRecord != null && !organizationChartList.Contains(newRecord)) { organizationChartList.Add(newRecord); } } }
                foreach (var item in List_Display) { if (item != "") {var index = int.Parse(item);display[index] = true;}}
                foreach (var item in organizationChartList) { if (item.parent != "0") { var id =int.Parse(item.parent); item.parent = Allchart.FirstOrDefault(m => m.ID == id).unit_Description; } }
                report.listDisplay = display;report.Organization_Chart = organizationChartList;
                return View("chartReport", report);
            }
            catch(Exception)
            {
                return View(record);
            }
        }
        
    }
}