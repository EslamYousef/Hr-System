using HR.Models;
using HR.Models.Infra;
using HR.Models.SetupPayroll;
using HR.Models.Time_management;
using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Web.Helpers;

namespace HR.Controllers
{
    [Authorize]
    public class HelperController : Controller
    {

        public HelperController()
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
        }


        // GET: Helper
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetCountryfromarea(int id, string flag)
        {
            if (flag == "1")
            {
                var record = dbcontext.Area.FirstOrDefault(m => m.ID == id);
                return Json(record.Country);
            }
            else
            {
                var record = dbcontext.Area.Where(m => m.Countryid == id.ToString());
                return Json(record);
            }
        }

        //public JsonResult getCountry(int id)
        //{
        //    dbcontext.Configuration.ProxyCreationEnabled = false;
        //    var Area = dbcontext.Area.FirstOrDefault(m => m.ID == id);
        //    var ID = int.Parse(Area.Countryid);
        //    var record = dbcontext.Country.FirstOrDefault(m => m.ID == ID);
        //    return Json(record);
        //}
        //public JsonResult getarea(int id)
        //{
        //    dbcontext.Configuration.ProxyCreationEnabled = false;
        //    var state = dbcontext.the_states.FirstOrDefault(m => m.ID == id);
        //    var ID = int.Parse(state.Areaid);
        //    var record = dbcontext.Area.FirstOrDefault(m => m.ID ==ID);
        //    return Json(record);
        //}

        public JsonResult getareabycountry(int id, string flag)
        {
            if (flag == "1")
            {
                var area = dbcontext.Area.FirstOrDefault(m => m.Countryid == id.ToString());

                return Json(area);
            }
            else
            {
                var area = dbcontext.Area.Where(m => m.Countryid == id.ToString()).ToList();
                var t = area.Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                return Json(t);
            }
        }
        public JsonResult getstatebycountry(int id, string flag)
        {
            if (flag == "1")
            {
                var area = dbcontext.the_states.FirstOrDefault(m => m.Areaid == id.ToString());

                return Json(area);
            }
            else
            {
                var area = dbcontext.the_states.Where(m => m.Areaid == id.ToString()).ToList();
                var t = area.Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                return Json(t);
            }
        }
        public JsonResult getarea(string id)
        {
            var Id = int.Parse(id);
            var area = dbcontext.Area.FirstOrDefault(m => m.ID == Id);
            return Json(area);
        }

        public JsonResult GetallCountry()
        {
            var country = dbcontext.Country.ToList();
            return Json(country);
        }
        public JsonResult GetCountry(string id)
        {
            var ID = int.Parse(id);
            var country = dbcontext.Country.FirstOrDefault(m => m.ID == ID);
            return Json(country);
        }

        public JsonResult Getstate(string id)
        {
            var ID = int.Parse(id);
            var state = dbcontext.the_states.FirstOrDefault(m => m.ID == ID);
            return Json(state);

        }
        public JsonResult getallter(string id)
        {
            var ID = int.Parse(id);
            var ter = dbcontext.Territories.Where(m => m.the_statesid == id).ToList();
            var t = ter.Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            return Json(t);


        }
        public JsonResult getter(string id)
        {
            var ID = int.Parse(id);
            var ter = dbcontext.Territories.FirstOrDefault(m => m.ID == ID);
            return Json(ter);
        }
        public JsonResult getallCity(string id)
        {
            var ID = int.Parse(id);
            var ter = dbcontext.cities.Where(m => m.Territoriesid == id).ToList();
            var t = ter.Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            return Json(t);

        }
        public JsonResult Getcity(string id)
        {
            var ID = int.Parse(id);
            var ter = dbcontext.cities.FirstOrDefault(m => m.ID == ID);
            return Json(ter);

        }
        public JsonResult Getmain(string id)
        {
            var ID = int.Parse(id);
            var record = dbcontext.Main_Educate_body.FirstOrDefault(m => m.ID == ID);
            return Json(record);
        }
        public JsonResult Gettitle(string id)
        {
            var ID = int.Parse(id);
            var record = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == ID);
            return Json(record);
        }
        public JsonResult getallQuli(string id)
        {
            var ID = int.Parse(id);
            var record = dbcontext.Name_of_educational_qualification.Where(m => m.Educate_Titleid == ID).ToList().Select(m => new { Code = m.Code + "----" + m.Name, ID = m.ID });
            return Json(record);
        }
        public JsonResult Getspacialquli(string id)
        {
            var ID = int.Parse(id);
            var record = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == ID);
            return Json(record);
        }
        public JsonResult GetDocument_Group(string id)
        {
            var ID = int.Parse(id);
            var record = dbcontext.Document_Group.FirstOrDefault(m => m.ID == ID);
            return Json(record);
        }
        public JsonResult Getcurrenyname(string id)
        {
            var ID = int.Parse(id);
            var currency = dbcontext.Currency.FirstOrDefault(m => m.ID == ID);
            return Json(currency);
        }
        public JsonResult Getairport(string id)
        {
            var ID = int.Parse(id);
            var recors = dbcontext.Air_ports.FirstOrDefault(m => m.ID == ID);
            return Json(recors);
        }
        public JsonResult Getauthority_type(string id)
        {
            var ID = int.Parse(id);
            var recors = dbcontext.Authority_Type.FirstOrDefault(m => m.ID == ID);
            return Json(recors);
        }
        public JsonResult GetSkill_group(string id)
        {
            var ID = int.Parse(id);
            var recors = dbcontext.Skill_group.FirstOrDefault(m => m.ID == ID);
            return Json(recors);
        }
        public JsonResult GetRisks_type(string id)
        {
            var ID = int.Parse(id);
            var recors = dbcontext.Risks_Type.FirstOrDefault(m => m.ID == ID);
            return Json(recors);
        }
        public JsonResult Get_job_title_sub_class(string id)
        {
            var ID = int.Parse(id);
            var recors = dbcontext.Job_title_class.FirstOrDefault(m => m.ID == ID);
            return Json(recors);
        }
        public JsonResult GetunitLevelcode(string id)
        {
            var ID = int.Parse(id);
            var recors = dbcontext.Organization_Unit_Level.FirstOrDefault(m => m.ID == ID);
            return Json(recors);
        }
        public JsonResult Getunitschemacode(string id)
        {
            var ID = int.Parse(id);
            var recors = dbcontext.Organization_Unit_Schema.FirstOrDefault(m => m.ID == ID);
            return Json(recors);
        }
        public JsonResult Getbudgetclass(string id)
        {
            var ID = int.Parse(id);
            var recors = dbcontext.Budget_class.FirstOrDefault(m => m.ID == ID);
            return Json(recors);
        }
        public JsonResult testdate(string from, string to)
        {
            DateTime fromdate = Convert.ToDateTime(from);
            DateTime todate = Convert.ToDateTime(to);
            if (DateTime.Compare(fromdate, todate) > 0)
                return Json(false);
            else
                return Json(true);

        }
        public JsonResult Get_type_code(string id)
        {
            var ID = int.Parse(id);
            var recors = dbcontext.Checktype.FirstOrDefault(m => m.ID == ID);
            return Json(recors);
        }
        public JsonResult get_check_request_status(string id)
        {
            var ID = int.Parse(id);
            var recors = dbcontext.check_request_status.FirstOrDefault(m => m.ID == ID);
            return Json(recors);
        }
        public JsonResult Getrequest(DateTime from, DateTime to)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var req = dbcontext.check_Request.Where(m => DateTime.Compare(m.Request_date, from) >= 0 && DateTime.Compare(m.Request_date, to) <= 0).ToList();
                foreach (var item in req)
                {
                    item.date = item.Request_date.ToShortDateString().ToString();

                }
                foreach (var item in req)
                {
                    var ID = int.Parse(item.ChecktypeID);
                    item.Checktype = dbcontext.Checktype.FirstOrDefault(m => m.ID == ID);
                    var ID_ = int.Parse(item.check_request_change_statusID);
                    item.check_request_change_status = dbcontext.check_request_change_status.FirstOrDefault(m => m.ID == ID_);
                    var hh = int.Parse(item.check_request_change_status.check_request_statusID);
                    item.check_request_change_status.check_Request = dbcontext.check_request_status.FirstOrDefault(m => m.ID == hh);
                }

                return Json(req);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult Getalllrequest()
        {
            try
            {
                var request = dbcontext.check_Request.ToList();

                var check_type = dbcontext.Checktype.ToList();
                var checkRequestStatues = dbcontext.check_request_change_status.ToList();

                var employeeRecord = from e in request
                                     join d in check_type on e.ChecktypeID equals d.ID.ToString()
                                     join i in checkRequestStatues on e.check_request_change_statusID equals i.ID.ToString()
                                     select new CheckRequestVM
                                     {
                                         iD = e.ID,
                                         amount = e.amount,
                                         checkNumber = e.check_number,
                                         requestDate = e.Request_date.ToString("dd/MM/yyyy"),
                                         requestNumber = e.Request_number,
                                         checkType = d.Name,
                                     };
                var t = employeeRecord.ToList();
                return Json(employeeRecord, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult getjobclass(string id)
        {
            var ID = int.Parse(id);
            var record = dbcontext.Job_level_class.FirstOrDefault(m => m.ID == ID);
            return Json(record);
        }
        public JsonResult getjobgrade(string id)
        {
            var ID = int.Parse(id);
            var record = dbcontext.Job_level_gradee.FirstOrDefault(m => m.ID == ID);
            return Json(record);
        }
        public JsonResult Get_Education(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var record = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == ID);
                return Json(record);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult getjobreport(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var record = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == ID);
                return Json(record);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult get_num_of_free_jobs(int id)
        {
            try
            {

                var job_titles = dbcontext.job_title_cards.Where(m => m.ID == id).ToList();
                var count = 0;
                foreach (var item in job_titles)
                {
                    count = count + item.number_vacant;
                }
                var model = new manpoweritems { count = count };
                return Json(model);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult Creat_slots(int number)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            try
            {
                var slots = new List<Slots>();
                for (var item = 0; item < number; item++)
                {
                    slots.Add(new Slots());
                }
                var slot = dbcontext.Slots.AddRange(slots);
                dbcontext.SaveChanges();
                return Json(slot);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult quli(int name, int speci, int grade)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var n = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == name);
            var s = dbcontext.Qualification_Major.FirstOrDefault(m => m.ID == speci);
            var g = dbcontext.GradeEducate.FirstOrDefault(m => m.ID == grade);
            var model = new quliVM { name = n, Qualification_Major = s, GradeEducate = g };
            return Json(model);
        }
        public JsonResult Risks(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Risks.FirstOrDefault(m => m.ID == ID);
            return Json(model);
        }

        public JsonResult skill(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Skill.FirstOrDefault(m => m.ID == ID);
            return Json(model);
        }

        public JsonResult reponsiblities(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Responsibilities.FirstOrDefault(m => m.ID == ID);
            return Json(model);
        }

        public JsonResult requirments(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Requirments.FirstOrDefault(m => m.ID == ID);
            return Json(model);
        }
        public JsonResult job_level_setup(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == ID);
            return Json(model);
        }
        public JsonResult ALL_job_level_setup()
        {

            var model = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            return Json(model);
        }
        public JsonResult all_organization_unit()
        {
            var model = dbcontext.Organization_Unit_Level.ToList();
            return Json(model);
        }
        public JsonResult job_title_card(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == ID);
            return Json(model);
        }
        public JsonResult job_sub_class(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Job_title_sub_class.FirstOrDefault(m => m.ID == ID);
            return Json(model);
        }
        public JsonResult nationality(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Nationality.FirstOrDefault(m => m.ID == ID);
            return Json(model);
        }

        //public JsonResult GetMedical_Medicine_ClassficationfromMedicine(int id, string flag)
        //{
        //    if (flag == "1")
        //    {
        //        var record = dbcontext.Medicine.FirstOrDefault(m => m.ID == id);
        //        return Json(record.Medical_Medicine_ClassficationId);
        //    }
        //    else
        //    {
        //        var record = dbcontext.Medicine.Where(m => m.Medical_Medicine_ClassficationId == id.ToString());
        //        return Json(record);
        //    }
        //}

        public JsonResult GetMedical_Medicine_Classfication(string id)
        {
            var ID = int.Parse(id);
            var MedicalMedicineClassfication = dbcontext.Medical_Medicine_Classfication.FirstOrDefault(m => m.ID == ID);
            return Json(MedicalMedicineClassfication);
        }
        public JsonResult GetMedicalDoctorsLevel(string id)
        {
            var ID = int.Parse(id);
            var MedicalDoctorsLevel = dbcontext.Medical_Doctors_Level.FirstOrDefault(m => m.ID == ID);
            return Json(MedicalDoctorsLevel);
        }
        public JsonResult GetMedicalServiceGroup(string id)
        {
            var ID = int.Parse(id);
            var MedicalServiceGroup = dbcontext.Medical_Service_Group.FirstOrDefault(m => m.ID == ID);
            return Json(MedicalServiceGroup);
        }
        public JsonResult GetMedicalServiceClassification(string id)
        {
            var ID = int.Parse(id);
            var MedicalServiceClassification = dbcontext.Medical_Service_Classification.FirstOrDefault(m => m.ID == ID);
            return Json(MedicalServiceClassification);
        }
        public JsonResult GetMedicalContributionsAllocationsEntry(string id)
        {
            var ID = int.Parse(id);
            var recors = dbcontext.Medical_Contributions_Allocations_Entry.FirstOrDefault(m => m.ID == ID);
            return Json(recors);
        }
        public JsonResult GetMedicalServiceClasslification(string id)
        {
            var ID = int.Parse(id);
            var recors = dbcontext.Medical_Service_Classification.FirstOrDefault(m => m.ID == ID);
            return Json(recors);
        }
        public JsonResult GetMedicalServicebyMedicalServiceClasslification(int id, string flag)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            if (flag == "1")
            {
                var recode = dbcontext.Medical_Service.FirstOrDefault(m => m.Medical_Service_ClassificationId == id.ToString());

                return Json(recode);
            }
            else
            {
                var area = dbcontext.Medical_Service.Where(m => m.Medical_Service_ClassificationId == id.ToString()).ToList();
                foreach (var item in area)
                {
                    var ID = int.Parse(item.Medical_Service_ClassificationId);
                    item.Medical_Service_Classification = dbcontext.Medical_Service_Classification.FirstOrDefault(m => m.ID == ID);
                }
                dbcontext.SaveChanges();
                var t = area.Select(m => new { my = m.Medical_Service_Classification, Code = m.Service_Code + "------[" + m.Name + ']', ID = m.ID });
                return Json(t);
            }
        }

        public JsonResult GetOrganization_unit_type(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var ID = int.Parse(id);
            var model = dbcontext.Organization_Unit_Type.FirstOrDefault(m => m.ID == ID);

            var le = dbcontext.Organization_Unit_Level.FirstOrDefault(m => m.ID == model.Organization_Unit_LevelID);

            var sc = dbcontext.Organization_Unit_Schema.FirstOrDefault(m => m.ID == model.Organization_Unit_SchemaID);
            var m1 = new unvm { code1 = le.Code, name1 = le.Name, code2 = sc.Code, name2 = sc.Name };
            return Json(m1);

        }
        public class unvm
        {
            public string code1 { get; set; }
            public string name1 { get; set; }
            public string code2 { get; set; }
            public string name2 { get; set; }
        }

        public JsonResult Get_organization_unit_chart(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
            return Json(model);

        }
        public JsonResult GetMedicalServiceee(string id)
        {
            var ID = int.Parse(id);
            var modesl = dbcontext.Medical_Service.FirstOrDefault(m => m.ID == ID);
            return Json(modesl.Name);

        }
        public JsonResult GetEOSInterviewQuestionsGroups(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var ID = int.Parse(id);
            var modesl = dbcontext.EOS_Interview_Questions_Groups.FirstOrDefault(m => m.ID == ID);
            return Json(modesl);
        }
        public JsonResult GetCheck_List_Item_Groups(string id)
        {
            var ID = int.Parse(id);
            var modesl = dbcontext.Check_List_Item_Groups.FirstOrDefault(m => m.ID == ID);
            return Json(modesl);

        }
        public JsonResult GetContactMethods(string id)
        {
            var ID = int.Parse(id);
            var modesl = dbcontext.Contact_methods.FirstOrDefault(m => m.ID == ID);
            return Json(modesl);

        }
        public JsonResult GetReligion(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Religion.FirstOrDefault(m => m.ID == ID);
            return Json(model);

        }
        public JsonResult GetNationality(string id)
        {
            var ID = int.Parse(id);
            var model = dbcontext.Nationality.FirstOrDefault(m => m.ID == ID);
            return Json(model);

        }
        public JsonResult getcitybyTerritories(int id, string flag)
        {
            if (flag == "1")
            {
                var Territories = dbcontext.cities.FirstOrDefault(m => m.Territoriesid == id.ToString());

                return Json(Territories);
            }
            else
            {
                var Territories = dbcontext.cities.Where(m => m.Territoriesid == id.ToString()).ToList();
                var t = Territories.Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                return Json(t);
            }
        }

        public JsonResult Getcite(string id)
        {
            var ID = int.Parse(id);
            var city = dbcontext.cities.FirstOrDefault(m => m.ID == ID);
            return Json(city);

        }
        public JsonResult GetCurrency(string id)
        {
            var ID = int.Parse(id);
            var Currency = dbcontext.Currency.FirstOrDefault(m => m.ID == ID);
            return Json(Currency);

        }
        public JsonResult getallPost(string id)
        {
            var ID = int.Parse(id);
            var city = dbcontext.postcode.Where(m => m.citiesid == id).ToList();
            var t = city.Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            return Json(t);

        }
        public JsonResult Getpost(string id)
        {
            var ID = int.Parse(id);
            var ter = dbcontext.postcode.FirstOrDefault(m => m.ID == ID);
            return Json(ter);

        }
        [HttpPost]
        public JsonResult GetEmployee(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var ID = int.Parse(id);
            var Employee = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
            return Json(Employee);

        }
        public JsonResult GetApplicant(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var ID = int.Parse(id);
            var Employee = dbcontext.Applicant_Profile.FirstOrDefault(m => m.ID == ID);
            return Json(Employee);

        }
        public JsonResult CheckAddressEmployee(string id)
        {
            var ID = int.Parse(id);
            var emps = (from emp in dbcontext.Employee_Profile
                        join add in dbcontext.Employee_Address_Profile on emp.ID.ToString() equals add.Employee_ProfileId
                        where emp.ID == ID
                        select new { namex = add.Transportation_method, nameb = add.countryid }
                     ).FirstOrDefault();
            return Json(emps);

        }
        public JsonResult GetDataByIdAppendPublicHolidays(string id)
        {
            var ID = int.Parse(id);
            dbcontext.Append_Public_Holidays_Dates.Where(a => a.Public_Holidays_DatesId == ID);
            var AppendPublicHoliday = (from AppendHoilday in dbcontext.Append_Public_Holidays_Dates
                                       join PublicHoliday in dbcontext.Public_Holidays_Dates on AppendHoilday.Public_Holidays_DatesId equals PublicHoliday.ID
                                       where AppendHoilday.Public_Holidays_DatesId == ID
                                       select new { id = AppendHoilday.ID, fromdate = AppendHoilday.Fromdate, todate = AppendHoilday.Todate });
            //dbcontext.Append_Public_Holidays_Dates.FirstOrDefault(m => m.ID == ID);
            return Json(AppendPublicHoliday);
        }
        public JsonResult GetDataByJobTitle(string id)
        {
            var ID = int.Parse(id);
            var all_level = dbcontext.job_level_setup.ToList();
            var all_jobs = dbcontext.job_title_cards.Where(a => a.ID == ID).ToList();
            var all_units = dbcontext.Organization_Chart.ToList();
            var AppendPublicHoliday = from A in all_jobs
                                      join Baa in all_level on A.joblevelsetupID equals Baa.ID.ToString()
                                      join c in all_units on A.Organization_unit_codeID equals c.ID.ToString()
                                      select new job_title_cards { name = A.name, Code = A.Code, job_level_setup = Baa, Organization_Chart = c };

            //dbcontext.Append_Public_Holidays_Dates.Where(a => a.Public_Holidays_DatesId == ID);
            //var AppendPublicHoliday = (from joblevel in dbcontext.job_level_setup
            //                           join jobtitle in dbcontext.job_title_cards on joblevel.ID equals int.Parse(jobtitle.joblevelsetupID)
            //                           where jobtitle.joblevelsetupID == id
            //                           select new { id = joblevel.ID, names = jobtitle.name, Names = joblevel.Name });
            //dbcontext.Append_Public_Holidays_Dates.FirstOrDefault(m => m.ID == ID);
            return Json(AppendPublicHoliday.ToList());
        }
        public JsonResult GetDataBysa()
        {
            var all_level = dbcontext.salary_code.Where(a => a.SourceEntry == 1).ToList();
            var all_jobs = dbcontext.ExtendedFields_Header.ToList();
            var AppendPublicHoliday = from A in all_level
                                      join Baa in all_jobs on A.ExtendedFields_Code equals Baa.ID.ToString()
                                      select new SalaryCode_ExtendedFields_VM { ID = A.ID, SalaryCodeID = A.SalaryCodeID, SalaryCodeDesc = A.SalaryCodeDesc, CodeGroupType = A.CodeGroupType, CodeValueType = A.CodeValueType /*, ExtendedFields_Code = Baa.ExtendedFields_Code , ExtendedFields_Description = Baa.ExtendedFields_Desc*/};
            return Json(AppendPublicHoliday.ToList());
        }
        public JsonResult GetDataBys()
        {
            var salary_code = dbcontext.salary_code.Where(a => a.SourceEntry == 1).ToList();
            return Json(salary_code);
        }
        public JsonResult GetDataByIdAppendCommitee_Agenda(string id)
        {
            var ID = int.Parse(id);
            var AppendCommitee_Agenda = dbcontext.Commitee_Agenda.Where(a => a.Committe_Resolution_RecuirtmentId == ID).ToList().Select(m => new { id = m.ID, fromdate = m.Start_Date, todate = m.End_Date });
            return Json(AppendCommitee_Agenda);
        }
        public JsonResult GetDataByIdAppendLinked_to_Testing(string id)
        {
            var ID = int.Parse(id);
            var Linked_to_Testing = dbcontext.Linked_to_Testing.Where(a => a.Committe_Resolution_RecuirtmentId == ID).ToList().Select(m => new { id = m.ID, fromdate = m.Expected_Start_Date, todate = m.Expected_End_Date });
            return Json(Linked_to_Testing);
        }
        public JsonResult GetDataByIdAppendCommitee_Activities(string id)
        {
            var ID = int.Parse(id);
            var Committe_Activities = dbcontext.Committe_Activities.Where(a => a.Committe_Resolution_RecuirtmentId == ID).ToList().Select(m => new { id = m.ID, fromdate = m.Planned_Date, todate = m.Actual_Date });
            return Json(Committe_Activities);
        }
        public JsonResult GetDataByIdAppendMedical_Test_Profile(string id)
        {
            var ID = int.Parse(id);
            var Medical_Test_Profile = dbcontext.Medical_Test_Profile.Where(a => a.ApplicantId == ID).ToList().Select(m => new { id = m.ID, fromdate = m.Test_Date });
            return Json(Medical_Test_Profile);
        }
        public JsonResult GetEmployeeRecodes(string id)
        {
            var ID = int.Parse(id);
            var Employee = dbcontext.Employee_Recodes_Group.FirstOrDefault(m => m.ID == ID);
            return Json(Employee);

        }
        public JsonResult GetEducatecategory(string id)
        {
            var ID = int.Parse(id);
            var Educatecategory = dbcontext.Educate_category.FirstOrDefault(m => m.ID == ID);
            return Json(Educatecategory);
        }
        public JsonResult GetEducateTitle(string id)
        {
            var ID = int.Parse(id);
            var EducateTitle = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == ID);
            return Json(EducateTitle);
        }
        public JsonResult GetMainEducatebody(string id)
        {
            var ID = int.Parse(id);
            var MainEducatebody = dbcontext.Main_Educate_body.FirstOrDefault(m => m.ID == ID);
            return Json(MainEducatebody);
        }
        public JsonResult GetSubeducationalbody(string id)
        {
            var ID = int.Parse(id);
            var Subeducationalbody = dbcontext.Sub_educational_body.FirstOrDefault(m => m.ID == ID);
            return Json(Subeducationalbody);
        }
        public JsonResult getSubeducationalbyMainEducate(int id, string flag)
        {
            if (flag == "1")
            {
                var area = dbcontext.Sub_educational_body.FirstOrDefault(m => m.Main_Educate_bodyid == id);

                return Json(area);
            }
            else
            {
                var area = dbcontext.Sub_educational_body.Where(m => m.Main_Educate_bodyid == id).ToList();
                var t = area.Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                return Json(t);
            }
        }
        public JsonResult GetNameofeducationalqualification(string id)
        {
            var ID = int.Parse(id);
            var Nameofeducationalqualification = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == ID);
            return Json(Nameofeducationalqualification);
        }

        public JsonResult getNameeducationalbyEducatetitle(int id, string flag)
        {
            if (flag == "1")
            {
                var area = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.Educate_Titleid == id);

                return Json(area);
            }
            else
            {
                var area = dbcontext.Name_of_educational_qualification.Where(m => m.Educate_Titleid == id).ToList();
                var t = area.Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                return Json(t);
            }
        }

        public JsonResult GetQualificationMajor(string id)
        {
            var ID = int.Parse(id);
            var QualificationMajor = dbcontext.Qualification_Major.FirstOrDefault(m => m.ID == ID);
            return Json(QualificationMajor);
        }

        public JsonResult getQualificationMajorbyNameofeducational(int id, string flag)
        {
            if (flag == "1")
            {
                var area = dbcontext.Qualification_Major.FirstOrDefault(m => m.Name_of_educational_qualificationid == id);

                return Json(area);
            }
            else
            {
                var area = dbcontext.Qualification_Major.Where(m => m.Name_of_educational_qualificationid == id).ToList();
                var t = area.Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                return Json(t);
            }
        }
        public JsonResult GradeEducate(string id)
        {
            var ID = int.Parse(id);
            var GradeEducate = dbcontext.GradeEducate.FirstOrDefault(m => m.ID == ID);
            return Json(GradeEducate);
        }
        public JsonResult getgroupinterview(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var model = dbcontext.EOS_Interview_Questions_Groups.FirstOrDefault(m => m.ID == ID);
                return Json(model);
            }
            catch (Exception e)
            { return Json(false); }
        }
        public JsonResult GetJobtitle(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;

            var ID = int.Parse(id);
            var Jobtitle = dbcontext.job_title_cards.FirstOrDefault(m => m.ID == ID);
            //var joblevelsetupID = int.Parse(Jobtitle.joblevelsetupID);
            //Jobtitle.job_level_setup = dbcontext.job_level_setup.FirstOrDefault(a=>a.ID == joblevelsetupID);
            //Jobtitle.Slots = dbcontext.Slots.Where(a => a.job_title_cards.ID == Jobtitle.ID).ToList();
            //var Organization_unit = int.Parse(Jobtitle.Organization_unit_codeID);
            //Jobtitle.Organization_Chart = dbcontext.Organization_Chart.FirstOrDefault(a => a.ID == Organization_unit);


            return Json(Jobtitle);
        }



        public JsonResult Getjoblevel(string id)
        {
            var ID = int.Parse(id);
            var joblevel = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == ID);
            return Json(joblevel);
        }
        public JsonResult GetWorklocation(string id)
        {
            var ID = int.Parse(id);
            var worklocation = dbcontext.work_location.FirstOrDefault(m => m.ID == ID);
            return Json(worklocation);
        }

        public JsonResult GetJobSolts(string id, string did)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var ii = int.Parse(did);
            var ID = int.Parse(id);
            var Slots = dbcontext.Slots.Where(m => m.job_title_cards.ID == ID && ((m.EmployeeID == null || m.EmployeeID == "0") || m.EmployeeID == did)).Where(m => m.slot_type == slot_type.Active).ToList().Select(m => new { Code = m.slot_code + "------[" + m.slot_description + ']', ID = m.ID });
            return Json(Slots);
        }
        public JsonResult GetSolt(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            //       var ii = int.Parse(did);
            var ID = int.Parse(id);
            var Slots = dbcontext.Slots.FirstOrDefault(m => m.ID == ID);
            return Json(Slots);
        }

        public JsonResult GetOrganizationChart(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;

            var ID = int.Parse(id);
            var OrganizationChart = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
            return Json(OrganizationChart);
        }
        public JsonResult GetExternalcompaines(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;

            var ID = int.Parse(id);
            var Externalcompaines = dbcontext.External_compaines.FirstOrDefault(m => m.ID == ID);
            return Json(Externalcompaines);
        }
        public JsonResult GetRejectionReasons(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;

            var ID = int.Parse(id);
            var RejectionReasons = dbcontext.Rejection_Reasons.FirstOrDefault(m => m.ID == ID);
            return Json(RejectionReasons);
        }
        public JsonResult GetContactmethods(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;

            var ID = int.Parse(id);
            var Contactmethods = dbcontext.Contact_methods.FirstOrDefault(m => m.ID == ID);
            return Json(Contactmethods);
        }
        public JsonResult GetContract_Type(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;

            var ID = int.Parse(id);
            var Contract_Type = dbcontext.Contract_Type.FirstOrDefault(m => m.ID == ID);
            return Json(Contract_Type);
        }

        public JsonResult GetAirports(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;

            var ID = int.Parse(id);
            var Airports = dbcontext.Air_ports.FirstOrDefault(m => m.ID == ID);
            return Json(Airports);
        }
        public JsonResult all_organization_chart()
        {
            var model = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "------[" + m.unit_Description + ']', ID = m.ID });
            return Json(model);
        }

        public JsonResult Getlevel(string id)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;

                var ID = int.Parse(id);
                var organization_chart = dbcontext.Organization_Chart.FirstOrDefault(m => m.ID == ID);
                var organiztion_unit_id = organization_chart.unit_type_codeID.ToString();
                var unit_ID = int.Parse(organiztion_unit_id);
                var unit = dbcontext.Organization_Unit_Type.FirstOrDefault(m => m.ID == unit_ID);
                var ii = dbcontext.job_level_setup.ToList();
                var list_level = new List<job_level_setup>();
                foreach (var item in ii)
                {
                    if (item.Organization_Unit_Type != null)
                    {
                        if (item.Organization_Unit_Type.Contains(unit))
                        {
                            list_level.Add(item);
                        }
                    }
                }
                //var level_setup = dbcontext.job_level_setup.ToList().Where(m => m.Organization_Unit_Type.Contains(unit)).ToList();
                return Json(list_level.Select(m => new { Code = m.Code + "--[" + m.Name + ']', ID = m.ID }));
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
        public JsonResult GetSubscriptionSyndicate(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;

            var ID = int.Parse(id);
            var SubscriptionSyndicate = dbcontext.Subscription_Syndicate.FirstOrDefault(m => m.ID == ID);
            return Json(SubscriptionSyndicate);
        }

        public JsonResult GetEmployeefamilyprofile(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;

            var ID = int.Parse(id);
            var Employeefamilyprofile = dbcontext.Employee_family_profile.FirstOrDefault(m => m.ID == ID);
            return Json(Employeefamilyprofile);
        }

        public JsonResult getfamilybyemployee(string id /*string flag*/)
        {
            int ID2 = int.Parse(id);
            var family = dbcontext.Employee_family_profile.FirstOrDefault(m => m.ID == ID2);

            return Json(family);

        }
        public JsonResult GetSponsor(string id)
        {
            var ID = int.Parse(id);
            var Sponsor = dbcontext.Sponsor.FirstOrDefault(m => m.ID == ID);
            return Json(Sponsor);
        }

        public JsonResult GetDocuments(string id)
        {
            var ID = int.Parse(id);
            var Documents = dbcontext.Documents.FirstOrDefault(m => m.ID == ID);
            var ID2 = int.Parse(Documents.Document_Groupid);
            Documents.Document_Group = dbcontext.Document_Group.FirstOrDefault(m => m.ID == ID2);
            return Json(Documents);
        }
        public JsonResult GetDataById(string id)
        {
            var ID = int.Parse(id);
            var newmodel = dbcontext.Position_Information.FirstOrDefault(m => m.ID == ID);
            return Json(newmodel);
        }
        public JsonResult GetDataByIdcontract(string id)
        {
            var ID = int.Parse(id);
            var newmodel = dbcontext.Employee_contract_profile.FirstOrDefault(m => m.ID == ID);
            return Json(newmodel);
        }
        public JsonResult GetDataByIdsubscription(string id)
        {
            var ID = int.Parse(id);
            var newmodel = dbcontext.Employee_subscription_syndicate_profile.FirstOrDefault(m => m.ID == ID);
            return Json(newmodel);
        }
        public JsonResult GetDataByIdsubscriptions(string id)
        {
            var ID = int.Parse(id);
            var newmodel = dbcontext.Applicant_Subscription_Syndicate_Profile.FirstOrDefault(m => m.ID == ID);
            return Json(newmodel);
        }

        public JsonResult GetDataByIdmilitary(string id)
        {
            var ID = int.Parse(id);
            var newmodel = dbcontext.Employee_military_service_calling.FirstOrDefault(m => m.ID == ID);
            return Json(newmodel);
        }
        public JsonResult GetDataByIdQualification(string id)
        {
            var ID = int.Parse(id);
            var newmodel = dbcontext.Employee_Qualification_Profile.FirstOrDefault(m => m.ID == ID);
            return Json(newmodel);
        }
        public JsonResult GetDataByIdQualificationApp(string id)
        {
            var ID = int.Parse(id);
            var newmodel = dbcontext.Applicant_Qualification_Profile.FirstOrDefault(m => m.ID == ID);
            return Json(newmodel);
        }
        public JsonResult GetDataByIdExperience(string id)
        {
            var ID = int.Parse(id);
            var newmodel = dbcontext.Employee_experience_profile.FirstOrDefault(m => m.ID == ID);
            return Json(newmodel);
        }
        public JsonResult GetDataByIdExperiences(string id)
        {
            var ID = int.Parse(id);
            var newmodel = dbcontext.Applicant_Previous_Experiences_Profile.FirstOrDefault(m => m.ID == ID);
            return Json(newmodel);
        }
        public JsonResult GetShiftdaystatussetup(string id)
        {
            var ID = int.Parse(id);
            var modesl = dbcontext.Shift_day_status_setup.FirstOrDefault(m => m.ID == ID);
            return Json(modesl);
        }
        public JsonResult GetPublic_Holiday_Events(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;

            var ID = int.Parse(id);
            var Public_Holiday_Events = dbcontext.Public_Holiday_Events.FirstOrDefault(m => m.ID == ID);
            return Json(Public_Holiday_Events);
        }
        public JsonResult GetSubjectSetup(string id)
        {
            var ID = int.Parse(id);
            var Committe_subjects = dbcontext.Committe_subjects.FirstOrDefault(m => m.ID == ID);
            return Json(Committe_subjects);
        }

        public JsonResult getempbyunit(int id)
        {
            try
            {

                var list = dbcontext.Organization_Chart.ToList().Select(m => new { Code = m.Code + "->" + m.unit_Description, ID = m.ID }).ToList();

                //var list = dbcontext.Position_Information.Where(m => m.Organization_ChartId == id.ToString()).Select(m=>new { m.Employee_Profile}).ToList().Select(m => new { Code = m.Employee_Profile.Code, ID = m.Employee_Profile.ID }).ToList();

                return Json(list);
            }
            catch (Exception)
            {
                return Json(null);
            }
        }
        public JsonResult getempbylevel(int id)
        {
            try
            {
                var list = dbcontext.job_level_setup.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID }).ToList();
                //var list = dbcontext.Position_Information.Where(m => m.job_level_setup.ID == id).Select(m => new { m.Employee_Profile }).ToList().Select(m => new { Code = m.Employee_Profile.Code, ID = m.Employee_Profile.ID }).ToList();

                return Json(list);

            }
            catch (Exception)
            {
                return Json(null);
            }
        }
        public JsonResult getempbynationality(int id)
        {
            try
            {
                var list = dbcontext.Nationality.ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID }).ToList();
                //var LIST = dbcontext.Employee_Profile.Where(m=>m.NationalityId==id.ToString()).ToList().Select(m => new { Code = m.Code + "->" + m.Name, ID = m.ID });
                return Json(list);
            }
            catch (Exception)
            {
                return Json(null);
            }
        }
        public JsonResult getallemp()
        {
            try
            {
                var LIST = dbcontext.Employee_Profile.Where(m => m.Active == true).ToList().Select(m => new { code = m.Code, Name = m.Name, ID = m.ID }).ToList();
                return Json(LIST);
            }
            catch (Exception)
            {
                return Json(null);
            }
        }
        public JsonResult getemp(int type, string[] id)
        {
            try
            {


                var y = new List<uoi>();
                var em = new List<Employee_Profile>();
                if (type == 2)
                {
                    foreach (var item in id)
                    {

                        var i = dbcontext.Position_Information.Where(m => m.Organization_ChartId == item).ToList();
                        foreach (var item2 in i)
                        {
                            int ID = int.Parse(item2.Employee_ProfileId);
                            em.Add(dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID && m.Active == true));
                        }
                        var ii = em.Distinct().ToList().Select(m => new { Name = m.Name, code = m.Code, ID = m.ID }).Distinct();
                        em = new List<Employee_Profile>();
                        foreach (var o in ii)
                        {
                            y.Add(new uoi { Name = o.Name, ID = o.ID, code = o.code });
                        }

                    }

                    return Json(y);
                }
                else if (type == 3)
                {
                    foreach (var item in id)
                    {

                        var i = (dbcontext.Employee_Profile.Where(m => m.NationalityId == item && m.Active == true).ToList().Select(m => new { Name = m.Name, code = m.Code, ID = m.ID }).Distinct());
                        foreach (var o in i)
                        {
                            y.Add(new uoi { Name = o.Name, ID = o.ID, code = o.code });
                        }
                    }
                    return Json(y);

                }
                else if (type == 4)
                {
                    foreach (var item in id)
                    {
                        var unit = dbcontext.Organization_Chart.Where(m => m.worklocationid == item).ToList();
                        var jobs = new List<job_title_cards>();
                        foreach (var item4 in unit)
                        {
                            var ID = item4.ID.ToString();
                            jobs.AddRange(dbcontext.job_title_cards.Where(m => m.Organization_unit_codeID == ID).ToList().ToList());


                        }
                        var slot = new List<Slots>();
                        foreach (var item7 in jobs)
                        {

                            slot.AddRange(dbcontext.Slots.Where(m => m.job_title_cardsID == item7.ID && m.EmployeeID != null && m.EmployeeID != ""));
                        }
                        foreach (var item2 in slot)
                        {

                            int ID = int.Parse(item2.EmployeeID);
                            var i = (dbcontext.Employee_Profile.Where(m => m.ID == ID && m.Active == true).ToList().Select(m => new { Name = m.Name, code = m.Code, ID = m.ID }).Distinct());

                            y.Add(new uoi { Name = i.First().Name, ID = i.First().ID, code = i.First().code });

                        }


                    }
                    var t = y.GroupBy(x => x.ID).Select(yy => yy.First());
                    return Json(t);

                }
                else if (type == 6)
                {

                    foreach (var item in id)
                    {
                        var jobs = dbcontext.job_title_cards.Where(m => m.joblevelsetupID == item).ToList();



                        var slot = new List<Slots>();
                        foreach (var item7 in jobs)
                        {

                            slot.AddRange(dbcontext.Slots.Where(m => m.job_title_cardsID == item7.ID && m.EmployeeID != null && m.EmployeeID != ""));
                        }
                        var real_slot = new List<Slots>();
                        foreach (var item2 in slot)
                        {
                            if (item2.EmployeeID != null || item2.EmployeeID != "0")
                            {


                                int ID = int.Parse(item2.EmployeeID);
                                var i = (dbcontext.Employee_Profile.Where(m => m.ID == ID && m.Active == true).ToList().Select(m => new { Name = m.Name, code = m.Code, ID = m.ID }).Distinct());
                                y.Add(new uoi { Name = i.First().Name, ID = i.First().ID, code = i.First().code });
                            }

                        }


                    }
                    var t = y.GroupBy(x => x.ID).Select(yy => yy.First());
                    return Json(t);

                }
                ////costcenter
                //else if (type == 5)
                //{
                //    foreach (var item in id)
                //    {
                //        var Slots = dbcontext.job_title_cards.Where(m => m.Organization_Chart == item).ToList().Select(m => m.Slots).ToList();

                //        var real_slot = new List<Slots>();
                //        foreach (var item2 in Slots)
                //        {

                //            real_slot.AddRange(item2.Where(m => m.EmployeeID != null || m.EmployeeID != "0").ToList());
                //        }
                //        foreach (var item3 in real_slot)
                //        {

                //            int ID = int.Parse(item3.EmployeeID);
                //            var i = (dbcontext.Employee_Profile.Where(m => m.ID == ID && m.Active == true).ToList().Select(m => new { Name = m.Name, code = m.Code, ID = m.ID }).Distinct());
                //            y.Add(new uoi { Name = i.First().Name, ID = i.First().ID, code = i.First().code });
                //        }

                //    }
                //    return Json(y);

                //}
                //else if (type == 3)
                //{
                //    foreach (var item in id)
                //    {
                //        var ID = int.Parse(item);
                //        var units = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == ID).Organization_Unit_TypeID.ToList() ;

                //        var list = dbcontext.Position_Information.Where(m => m.job_level_setup.ID == ID).Select(m => new { m.Employee_Profile }).ToList().Select(m => new { Code = m.Employee_Profile.Code+"->"+m.Employee_Profile.Name, ID = m.Employee_Profile.ID }).ToList().Distinct();
                //        foreach(var i in units)
                //        {i.

                //            var list = dbcontext.Position_Information.Where(m => m.Organization_ChartId == i.).Select(m => new { m.Employee_Profile }).ToList().Select(m => new { Code = m.Employee_Profile.Code + "->" + m.Employee_Profile.Name, ID = m.Employee_Profile.ID }).ToList().Distinct();

                //        }
                //        foreach (var item2 in list)
                //        {
                //            items.Insert(0, (new SelectListItem
                //            {
                //                Text = item2.Code,
                //                Value = item2.ID.ToString(),

                //            }));
                //        }
                //    }

                //}

                return Json(null);
            }
            catch (Exception e)
            {
                return Json(null);
            }

        }
        public JsonResult getjob(int id)
        {
            var jobs = dbcontext.job_title_cards.Where(m => m.Organization_unit_codeID == id.ToString()).ToList().Select(m => new { Code = m.Code + "-[" + m.name + ']', ID = m.ID }); ;
            return Json(jobs);
        }
        public JsonResult GetTest(string id)
        {
            var ID = int.Parse(id);
            var Test = dbcontext.Test.FirstOrDefault(m => m.ID == ID);
            return Json(Test);
        }
        public JsonResult GetCommitteResolutionRecuirtment(string id)
        {
            var ID = int.Parse(id);
            var CommitteResolutionRecuirtment = dbcontext.Committe_Resolution_Recuirtment.Where(a => a.Committe_Usage == Committe_Usage.Test).FirstOrDefault(a => a.ID == ID);
            return Json(CommitteResolutionRecuirtment);
        }
        public JsonResult GetWeekendsetup(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var ID = int.Parse(id);
            var Weekend_setup = dbcontext.Weekend_setup.FirstOrDefault(m => m.ID == ID);
            return Json(Weekend_setup);
        }
        public JsonResult GetDataByIdAppendBusiness_Test_Profile(string id)
        {
            var ID = int.Parse(id);
            var Business_Test_Profile = dbcontext.Business_Test_Profile.Where(a => a.ApplicantId == ID).ToList().Select(m => new { id = m.ID, fromdate = m.Test_Date });
            return Json(Business_Test_Profile);
        }
        public JsonResult GetCommitteResolutionRecuirtmentPer(string id)
        {
            var ID = int.Parse(id);
            var CommitteResolutionRecuirtment = dbcontext.Committe_Resolution_Recuirtment.Where(a => a.Committe_Usage == Committe_Usage.Personnel).FirstOrDefault(a => a.ID == ID);
            return Json(CommitteResolutionRecuirtment);
        }
        public JsonResult GetCommitteActivitiesByCommitteResolutionRecuirtment(int id, string flag)
        {
            if (flag == "1")
            {
                var Committe_Activities = dbcontext.Committe_Activities.FirstOrDefault(m => m.Committe_Resolution_RecuirtmentId == id);

                return Json(Committe_Activities);
            }
            else
            {
                var Committe_Activities = dbcontext.Committe_Activities.Where(m => m.Committe_Resolution_RecuirtmentId == id).ToList();
                var t = Committe_Activities.Select(m => new { Code = m.ActivitySerialNumber, ID = m.ID });
                return Json(t);
            }
        }
        public JsonResult GetDataByIdAppendPersonnel_Committee_Profile(string id)
        {
            var ID = int.Parse(id);
            var Personnel_Committee_Profile = dbcontext.Personnel_Committee_Profile.Where(a => a.ApplicantId == ID).ToList().Select(m => new { id = m.ID, fromdate = m.Committee_Approval_Date });
            return Json(Personnel_Committee_Profile);
        }
        public JsonResult GetDataByIdPositionRec(string id)
        {
            var ID = int.Parse(id);
            var newmodel = dbcontext.Position_Information_Rec.FirstOrDefault(m => m.ApplicantId == ID);
            return Json(newmodel);
        }
        public JsonResult GetCostCenterCategory(string id)
        {
            var ID = int.Parse(id);
            var CostCenterCategory = dbcontext.CostCenterCategory.FirstOrDefault(m => m.ID == ID);
            return Json(CostCenterCategory);
        }
        public JsonResult GetBank(string id)
        {
            var ID = int.Parse(id);
            var Bank = dbcontext.Bank.FirstOrDefault(m => m.ID == ID);
            return Json(Bank);
        }
        public JsonResult GetExtendedFields_Header(string id)
        {
            var ID = int.Parse(id);
            var ExtendedFields_Header = dbcontext.ExtendedFields_Header.FirstOrDefault(m => m.ID == ID);
            return Json(ExtendedFields_Header);
        }
        public JsonResult GetExtendedFieldsHeader()
        {
            var ExtendedFields_Header = dbcontext.ExtendedFields_Header.ToList();
            return Json(ExtendedFields_Header);
        }
        public JsonResult GetSalaryCodeGroup_Header(string id)
        {
            var ID = int.Parse(id);
            var SalaryCodeGroup_Header = dbcontext.SalaryCodeGroup_Header.FirstOrDefault(m => m.ID == ID);
            return Json(SalaryCodeGroup_Header);
        }
        public JsonResult GetDebit(string id)
        {
            var ID = int.Parse(id);
            var Debit = dbcontext.GL_AccountSetup.Where(a => a.AccountType == 1 || a.AccountType == 3).FirstOrDefault(m => m.ID == ID);
            return Json(Debit);
        }
        public JsonResult GetCredit(string id)
        {
            var ID = int.Parse(id);
            var Credit = dbcontext.GL_AccountSetup.Where(a => a.AccountType == 2 || a.AccountType == 3).FirstOrDefault(m => m.ID == ID);
            return Json(Credit);
        }
        public JsonResult GetChecktype()
        {
            var Checktype = dbcontext.Checktype.ToList();
            return Json(Checktype);
        }
        public JsonResult salaryitem()
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var item = dbcontext.salary_code.ToList();
            return Json(item);
        }
        public JsonResult GetEmployees()
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var Employee = dbcontext.Employee_Profile.Where(a => a.Active == true).ToList();
            return Json(Employee);
        }
        public JsonResult GetManualPaymentTypes_Header()
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var item = dbcontext.ManualPaymentTypes_Header.ToList();
            return Json(item);
        }
        public JsonResult GetExtendedFieldsHeaderByManualPaymentTypes(int id)
        {
            var ManualPaymentTypes_Header = dbcontext.ManualPaymentTypes_Header.FirstOrDefault(m => m.ID == id);
            var code = int.Parse(ManualPaymentTypes_Header.ExtendedFields_Code);
            var Ex = dbcontext.ExtendedFields_Header.FirstOrDefault(a => a.ID == code);
            return Json(Ex);
        }
        public JsonResult GetExtendedFieldsDetailsByExtendedFieldsHeader(string id)
        {
            var Checktype = dbcontext.ExtendedFields_Details.Where(a => a.ExtendedFields_Code == id).ToList();
            return Json(Checktype);
        }
        public JsonResult GetManualPaymentTypes_DetailByManualPaymentTypesHeader(string id)
        {
            var Checktype = dbcontext.ManualPaymentTypes_Detail.Where(a => a.PaymentTypeCode == id).ToList();
            return Json(Checktype);
        }
        public JsonResult GetManualPaymentTransactionEntry_Detail(string id)
        {
            var ID = double.Parse(id);
            var Checktype = dbcontext.ManualPaymentTransactionEntry_Detail.Where(a => a.TransactionNumber == id).ToList();
            return Json(Checktype);
        }
        public JsonResult GetManualPaymentTypes_HeaderinEdit(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var item = dbcontext.ManualPaymentTransactionEntry.Where(a => a.ID == id).ToList();
            return Json(item);
        }
        public JsonResult AllManualMass(string id, string did)
        {
            int ID = int.Parse(id);
            //var ManualPaymentTransactionEntry = dbcontext.ManualPaymentTransactionEntry.Where(a => a.ID == ID).ToList();
            var ManualPaymentTypes_Detail = dbcontext.ManualPaymentTypes_Detail.Where(a => a.PaymentTypeCode == id).ToList();
            var ManualPaymentTransactionEntry_Detail = dbcontext.ManualPaymentTransactionEntry_Detail.Where(a => a.TransactionNumber == id).ToList();
            if (ManualPaymentTransactionEntry_Detail.Count() == 0)
            {
                var Checktype = dbcontext.ManualPaymentTypes_Detail.Where(a => a.PaymentTypeCode == id).ToList();
                return Json(Checktype);
            }
            else
            {
                var model = (from a in ManualPaymentTransactionEntry_Detail
                             join b in ManualPaymentTypes_Detail on a.SalaryCodeID equals b.Salarycodedescription
                             where a.SalaryCodeID == b.Salarycodedescription
                             orderby (a.ID)
                             select new { SalaryCodeID = b.SalaryCodeID, Salarycodedescription = b.Salarycodedescription, Type = b.Type, ValueType = b.ValueType, Value = a.Value });
                return Json(model);
            }

        }

        public JsonResult AllManual(string id, string did)
        {
            int ID = int.Parse(id);
            //var ManualPaymentTransactionEntry = dbcontext.ManualPaymentTransactionEntry.Where(a => a.ID == ID).ToList();
            var ManualPaymentTypes_Detail = dbcontext.ManualPaymentTypes_Detail.Where(a => a.PaymentTypeCode == did).ToList();
            var ManualPaymentTransactionEntry_Detail = dbcontext.ManualPaymentTransactionEntry_Detail.Where(a => a.TransactionNumber == id).ToList();
            if (ManualPaymentTransactionEntry_Detail.Count() == 0)
            {
                var Checktype = dbcontext.ManualPaymentTypes_Detail.Where(a => a.PaymentTypeCode == did).ToList();
                return Json(Checktype);
            }
            else
            {
                var model = (from a in ManualPaymentTransactionEntry_Detail
                             join b in ManualPaymentTypes_Detail on a.SalaryCodeID equals b.Salarycodedescription
                             where a.SalaryCodeID == b.Salarycodedescription
                             orderby (a.ID)
                             select new { SalaryCodeID = b.SalaryCodeID, Salarycodedescription = b.Salarycodedescription, Type = b.Type, ValueType = b.ValueType, Value = a.Value });
                return Json(model);
            }
        }
        public JsonResult GetPayrollTransactionJournalSetup(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var item = dbcontext.PayrollTransactionJournalSetup.FirstOrDefault(a => a.ID == id);
            return Json(item);
        }
        public JsonResult Getsalary_code(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var item = dbcontext.salary_code.Where(a => a.SourceEntry == 2).FirstOrDefault(a => a.ID == id);
            return Json(item);
        }
        public JsonResult GetExtendedFieldsBySalaryCode(int id)
        {
            var salary_code = dbcontext.salary_code.FirstOrDefault(m => m.ID == id);
            var code = int.Parse(salary_code.ExtendedFields_Code);
            var Ex = dbcontext.ExtendedFields_Header.FirstOrDefault(a => a.ID == code);
            return Json(Ex);
        }
        public JsonResult Getsalarycode(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var item = dbcontext.salary_code.Where(a => a.SourceEntry == 2).FirstOrDefault(a => a.ID == id);
            return Json(item);
        }
        public JsonResult GetEmployee_Payroll_Transactions_ExtendedFieldsDetail(int id)
        {
            var code = dbcontext.Employee_Payroll_Transactions.FirstOrDefault(a => a.ID == id).TransactionNumber;
            var Checktype = dbcontext.Employee_Payroll_Transactions_ExtendedFieldsDetail.Where(a => a.TransactionNumber == code).ToList();
            return Json(Checktype);
        }
        public JsonResult GetSalaryCodeGroupHeader(int id)
        {
            var Checktype = dbcontext.SalaryCodeGroup_Header.Where(a => a.GroupPurpose == 2).FirstOrDefault(a => a.ID == id);
            return Json(Checktype);
        }
        public JsonResult GetSalaryCodeGroup_Detail(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;

            var SalaryCodeGroup_Detail = dbcontext.SalaryCodeGroup_Detail.Where(a => a.CodeGroupID == id).ToList();
            var salary = dbcontext.salary_code.ToList();
            var total = from a in SalaryCodeGroup_Detail
                        join b in salary on a.SalaryCodeID equals b.SalaryCodeID
                        where a.SalaryCodeID == b.SalaryCodeID
                        select new { salarycode = b.SalaryCodeID, salaryDes = b.SalaryCodeDesc, codegrouptype = b.CodeGroupType, codevaluetype = b.CodeValueType, defaultvalue = a.DefaultValue };
            return Json(total);
        }
        public JsonResult GetPayrollPeriodSetup(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var item = dbcontext.PayrollPeriodSetup.ToList();
            return Json(item);
        }
        public JsonResult GetEmployee_Profile_Groups(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var ID = int.Parse(id);
            var Employee_Profile_Groups = dbcontext.Employee_Profile_Groups.ToList();
            return Json(Employee_Profile_Groups);
        }
        public JsonResult GetBusiness_Trip(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var item = dbcontext.Business_Trip.FirstOrDefault(a => a.Code == id);
            return Json(item);
        }
        public JsonResult GetsalarycodeTime(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var item = dbcontext.salary_code.FirstOrDefault(a => a.ID == id);
            return Json(item);
        }
        public JsonResult GetTemCodeByEmployee_Shift_schedule()
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var model = new Shiftscheduletemplate();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
            var model_ = dbcontext.Shiftscheduletemplate.ToList();
            if (model_.Count() == 0)
            {
                model.TemplateCode_Shifts = stru + "1";
            }
            else
            {
                model.TemplateCode_Shifts = stru + (model_.LastOrDefault().ID + 1).ToString();
            }
            return Json(model);
        }
        public JsonResult GetTemplate(int id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            var item = dbcontext.Template.FirstOrDefault(a => a.ID == id);
            return Json(item);
        }
        public JsonResult GetTempSchByTemplate(string id)
        {
            dbcontext.Configuration.ProxyCreationEnabled = false;
            //var ID = int.Parse(id);
            var Shiftscheduletemplate = dbcontext.Shiftscheduletemplate.Where(a => a.TemplateCode_Shifts == id).ToList();
            return Json(Shiftscheduletemplate);
        }
        public JsonResult GetShift_setup(int id)
        {
            var item = dbcontext.Shift_setup.FirstOrDefault(a => a.ID == id);
            return Json(item);
        }
        public JsonResult GetDataforTimeManagement_EmployeeTimeAttendanceTransaction(string id)
        {
            var PositionInformation = dbcontext.Position_Information.Where(a => a.Primary_Position == true && a.Employee_ProfileId == id).ToList();
            var worklocation = dbcontext.work_location.ToList();
            var Shiftsetup = dbcontext.Shift_setup.ToList();
            var model = (from a in PositionInformation
                         join b in worklocation on a.Default_location_descId equals b.ID.ToString()
                         join c in Shiftsetup on a.Shift_setupId equals c.ID.ToString()
                         select new { work_location = b.Name, Shiftsetup = c.Name, working_system = a.working_system });
            return Json(model);
        }
        public JsonResult GetShiftdaystatus(int id)
        {
            var item = dbcontext.Shiftdaystatus.FirstOrDefault(a => a.ID == id);
            return Json(item);
        }
        public JsonResult GetfullmonthforTimeManagement(int year, int month, string shift, string location, int emp)
        {
            var Shift_setup = dbcontext.Shift_setup.FirstOrDefault(a => a.Name == shift);
            var work_location = dbcontext.work_location.FirstOrDefault(a => a.Name == location);
            var Shiftdaystatus = dbcontext.Shiftdaystatus.FirstOrDefault(a => a.ID == work_location.Defaultdaystatuscode);
            var EmployeeProfile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == emp);
            var Weekendsetup = dbcontext.Weekend_setup.FirstOrDefault(a => a.ID == EmployeeProfile.Weekendcode);
            var Shiftdaystatuss = dbcontext.Shiftdaystatus.FirstOrDefault(a => a.ID == Weekendsetup.ShiftdaystatussetupId);

            var ret = new List<DateTime>();

            var timemanagement = new List<Vm_TimeManagement_EmployeeTimeAttendanceTransaction>();

            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                ret.Add(new DateTime(year, month, i));
            }
            foreach (var o in ret)
            {
                var weeks = o.DayOfWeek.ToString();
                var Name = "";
                var Code = "";
                var Saturday = "Saturday";
                var Sunday = "Sunday";
                var Monday = "Monday";
                var Tuesday = "Tuesday";
                var Wednesday = "Wednesday";
                var Thursday = "Thursday";
                var Friday = "Friday";
                int EmployeeSta;
                if (Weekendsetup.Saturday == true && Saturday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                }
                else if (Weekendsetup.Sunday == true && Sunday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                }
                else if (Weekendsetup.Monday == true && Monday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                }
                else if (Weekendsetup.Tuesday == true && Tuesday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                }
                else if (Weekendsetup.Wednesday == true && Wednesday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                }
                else if (Weekendsetup.Thursday == true && Thursday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                }
                else if (Weekendsetup.Friday == true && Friday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatus.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                }
                else
                {
                    Name = Shiftdaystatus.Name;
                    Code = Shiftdaystatus.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.WeekEnd);
                }

                timemanagement.Add(new Vm_TimeManagement_EmployeeTimeAttendanceTransaction
                {
                    week = o.DayOfWeek.ToString(),
                    AttendDate = o.ToShortDateString(),
                    Start_time = Shift_setup.Start_time,
                    End_time = Shift_setup.End_time,
                    worklocationcode = Code,
                    worklocationDes = Name,
                    LocationCode = work_location.Code,
                    LocationDescription = work_location.Name,
                    ShiftCode = Shift_setup.Code,
                    ShiftDescription = Shift_setup.Name,
                    working_system = working_system.Day,
                    EmployeeStat = EmployeeSta,
                    ID = o.Day
                });
            }

            return Json(timemanagement);
        }
        public TimeSpan? FROM_TIME { get; set; }
        public TimeSpan? END_TIME { get; set; }

        public JsonResult GetApplyDefaultforTimeManagement(int year, int month, string shift, string location, int emp)
        {

            var Employee_Shift_schedule = dbcontext.Employee_Shift_schedule.Where(a => a.Use_As_Default == true).FirstOrDefault(a => a.Employee_ProfileID == emp);
            var Schedule_Details = dbcontext.Schedule_Details.Where(a => a.Employee_Shift_scheduleID == Employee_Shift_schedule.ID).ToList();
            var sch = new List<uoi>();

            for (int c = 0; c < Schedule_Details.Count(); c++)
            {
                sch.Add(new uoi { date = Schedule_Details[c].From_date });
            }

            var Shift_setup = dbcontext.Shift_setup.FirstOrDefault(a => a.Name == shift);
            var work_location = dbcontext.work_location.FirstOrDefault(a => a.Name == location);
            var Shiftdaystatus = dbcontext.Shiftdaystatus.FirstOrDefault(a => a.ID == work_location.Defaultdaystatuscode);
            var EmployeeProfile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == emp);
            var Weekendsetup = dbcontext.Weekend_setup.FirstOrDefault(a => a.ID == EmployeeProfile.Weekendcode);
            var Shiftdaystatuss = dbcontext.Shiftdaystatus.FirstOrDefault(a => a.ID == Weekendsetup.ShiftdaystatussetupId);

            var ret = new List<DateTime>();

            var timemanagement = new List<Vm_TimeManagement_EmployeeTimeAttendanceTransaction>();

            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                ret.Add(new DateTime(year, month, i));
            }
            foreach (var o in ret)
            {
                var weeks = o.DayOfWeek.ToString();
                var Name = "";
                var Code = "";
                var Saturday = "Saturday";
                var Sunday = "Sunday";
                var Monday = "Monday";
                var Tuesday = "Tuesday";
                var Wednesday = "Wednesday";
                var Thursday = "Thursday";
                var Friday = "Friday";
                int EmployeeSta;
                var ShiftName = "";
                var ShiftCode = "";
                if (Weekendsetup.Saturday == true && Saturday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                else if (Weekendsetup.Sunday == true && Sunday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                else if (Weekendsetup.Monday == true && Monday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                else if (Weekendsetup.Tuesday == true && Tuesday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                else if (Weekendsetup.Wednesday == true && Wednesday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                else if (Weekendsetup.Thursday == true && Thursday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                else if (Weekendsetup.Friday == true && Friday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatus.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                else
                {
                    Name = Shiftdaystatus.Name;
                    Code = Shiftdaystatus.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.WeekEnd);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                for (int d = 0; d < sch.Count(); d++)
                {
                    if (o.ToShortDateString() == sch[d].date.ToShortDateString())
                    {
                        var ShID = Schedule_Details[d].ShiftdaystatusID.ToString();
                        var s = dbcontext.Shiftdaystatus.FirstOrDefault(a => a.ID.ToString() == ShID);
                        Code = s.Code;
                        Name = s.Name;
                        var shif = Schedule_Details[d].Shift_setupID.ToString();
                        var sh = dbcontext.Shift_setup.FirstOrDefault(a => a.ID.ToString() == shif);
                        ShiftCode = sh.Code;
                        ShiftName = sh.Name;
                        FROM_TIME = Schedule_Details[d].From;
                        END_TIME = Schedule_Details[d].To;
                        //AttendDate = sch[d].date.ToShortDateString();
                    }
                }
                timemanagement.Add(new Vm_TimeManagement_EmployeeTimeAttendanceTransaction
                {
                    week = o.DayOfWeek.ToString(),
                    AttendDate = o.ToShortDateString(),
                    Start_time = FROM_TIME,
                    End_time = END_TIME,
                    worklocationcode = Code,
                    worklocationDes = Name,
                    LocationCode = work_location.Code,
                    LocationDescription = work_location.Name,
                    ShiftCode = ShiftCode,
                    ShiftDescription = ShiftName,
                    working_system = working_system.Day,
                    EmployeeStat = EmployeeSta,
                    ID = o.Day
                });
            }

            return Json(timemanagement);
        }
        public JsonResult GetVacationsSetup(int id)
        {
            var Vacations = dbcontext.Vacations_Setup.FirstOrDefault(a => a.ID == id);
            return Json(Vacations);
        }
        public JsonResult GetLeavesBalance(int id)
        {
            var record = dbcontext.LeavesBalance.Where(a => a.VacCode == id).ToList();
            return Json(record);
        }
        public JsonResult Balance(DateTime id, int Vac , DateTime Start)
        {

            var record = dbcontext.LeavesBalance.Where(a => a.BalanceStartDate == id && a.VacCode == Vac).FirstOrDefault();
            var BalanceStart = record.BalanceStartDate;
            int BalanceStartDate = Convert.ToDateTime(BalanceStart).Year;

            var LeavesRequestMaster = dbcontext.LeavesRequestMaster.Where(a => a.VacCode == record.VacCode && a.LastOrder == true).FirstOrDefault();
            var LeavesRequestMasters = dbcontext.LeavesRequestMaster.Where(a => a.VacCode == record.VacCode && a.Approved == 1 && a.year == BalanceStartDate && a.LastOrder == true).ToList();
            var VacationsSetup = dbcontext.Vacations_Setup.FirstOrDefault(a => a.ID == Vac);

            if (VacationsSetup.TrackMonthlyAccrualBalance == true)
            {
                var Bal = record.Balance / 12;
                int Sta = Convert.ToDateTime(Start).Month;
                int year = Convert.ToDateTime(Start).Year;

                var RemainDays = Bal * Sta;
                var LeavesRequestMas = dbcontext.LeavesRequestMaster.Where(a => a.VacCode == record.VacCode && a.Approved == 1 && a.year == year).ToList();
              
                var AllDays = new List<double?>();
                if (LeavesRequestMas != null)
                {
                    for (int i = 0; i < LeavesRequestMas.Count; i++)
                    {
                        var Rem= LeavesRequestMas[i].TotalDays;
                        var Day =  AllDays.LastOrDefault();
                        if (Day == null)
                        {
                            Day = Rem ;
                            AllDays.Add(Day);
                        }
                        else
                        {
                            var All = Day + Rem;
                            AllDays.Add(All);
                        }
                    }
                    var RemainDaysss = RemainDays - AllDays.LastOrDefault();
                    if (AllDays.LastOrDefault() != 0)
                    {
                        return Json(RemainDaysss);
                    }
                }
                return Json(RemainDays);

            }
            if (LeavesRequestMasters != null)
            {
                for (int i = 0; i < LeavesRequestMasters.Count; i++)
                {
                    int year = LeavesRequestMasters[i].year;
                    if (year == BalanceStartDate)
                    {
                        var RemainDays = LeavesRequestMasters[i].RemainDays;
                        return Json(RemainDays);
                    }
                }
            }
                var RemainDay = record.Balance;
                return Json(RemainDay);
        }


        public JsonResult GetLeaveRequestFormVacationSetup(int year, int month, string shift, string location, int emp, string from, string to)
        {

            var Employee_Shift_schedule = dbcontext.Employee_Shift_schedule.Where(a => a.Use_As_Default == true).FirstOrDefault(a => a.Employee_ProfileID == emp);
            var Schedule_Details = dbcontext.Schedule_Details.Where(a => a.Employee_Shift_scheduleID == Employee_Shift_schedule.ID).ToList();
            var sch = new List<uoi>();

            for (int c = 0; c < Schedule_Details.Count(); c++)
            {
                sch.Add(new uoi { date = Schedule_Details[c].From_date });
            }

            var Shift_setup = dbcontext.Shift_setup.FirstOrDefault(a => a.Name == shift);
            var work_location = dbcontext.work_location.FirstOrDefault(a => a.Name == location);
            var Shiftdaystatus = dbcontext.Shiftdaystatus.FirstOrDefault(a => a.ID == work_location.Defaultdaystatuscode);
            var EmployeeProfile = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == emp);
            var Weekendsetup = dbcontext.Weekend_setup.FirstOrDefault(a => a.ID == EmployeeProfile.Weekendcode);
            var Shiftdaystatuss = dbcontext.Shiftdaystatus.FirstOrDefault(a => a.ID == Weekendsetup.ShiftdaystatussetupId);

            var ret = new List<DateTime>();

            var timemanagement = new List<Vm_TimeManagement_EmployeeTimeAttendanceTransaction>();

            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                ret.Add(new DateTime(year, month, i));
            }
            foreach (var o in ret)
            {
                var weeks = o.DayOfWeek.ToString();
                var Name = "";
                var Code = "";
                var Saturday = "Saturday";
                var Sunday = "Sunday";
                var Monday = "Monday";
                var Tuesday = "Tuesday";
                var Wednesday = "Wednesday";
                var Thursday = "Thursday";
                var Friday = "Friday";
                int EmployeeSta;
                var ShiftName = "";
                var ShiftCode = "";
                if (Weekendsetup.Saturday == true && Saturday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                else if (Weekendsetup.Sunday == true && Sunday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                else if (Weekendsetup.Monday == true && Monday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                else if (Weekendsetup.Tuesday == true && Tuesday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                else if (Weekendsetup.Wednesday == true && Wednesday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                else if (Weekendsetup.Thursday == true && Thursday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatuss.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                else if (Weekendsetup.Friday == true && Friday == weeks)
                {
                    Name = Shiftdaystatuss.Name;
                    Code = Shiftdaystatus.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.OnDuty);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                else
                {
                    Name = Shiftdaystatus.Name;
                    Code = Shiftdaystatus.Code;
                    EmployeeSta = Convert.ToInt32(EmployeeStatus.WeekEnd);
                    FROM_TIME = Shift_setup.Start_time;
                    END_TIME = Shift_setup.End_time;
                    ShiftCode = Shift_setup.Code;
                    ShiftName = Shift_setup.Name;
                }
                for (int d = 0; d < sch.Count(); d++)
                {
                    if (o.ToShortDateString() == sch[d].date.ToShortDateString())
                    {
                        var ShID = Schedule_Details[d].ShiftdaystatusID.ToString();
                        var s = dbcontext.Shiftdaystatus.FirstOrDefault(a => a.ID.ToString() == ShID);
                        Code = s.Code;
                        Name = s.Name;
                        var shif = Schedule_Details[d].Shift_setupID.ToString();
                        var sh = dbcontext.Shift_setup.FirstOrDefault(a => a.ID.ToString() == shif);
                        ShiftCode = sh.Code;
                        ShiftName = sh.Name;
                        FROM_TIME = Schedule_Details[d].From;
                        END_TIME = Schedule_Details[d].To;
                        //AttendDate = sch[d].date.ToShortDateString();
                    }
                }
                timemanagement.Add(new Vm_TimeManagement_EmployeeTimeAttendanceTransaction
                {
                    week = o.DayOfWeek.ToString(),
                    AttendDate = o.ToShortDateString(),
                    Start_time = FROM_TIME,
                    End_time = END_TIME,
                    worklocationcode = Code,
                    worklocationDes = Name,
                    LocationCode = work_location.Code,
                    LocationDescription = work_location.Name,
                    ShiftCode = ShiftCode,
                    ShiftDescription = ShiftName,
                    working_system = working_system.Day,
                    EmployeeStat = EmployeeSta,
                    ID = o.Day
                });
            }

            return Json(timemanagement);
        }

    }
}