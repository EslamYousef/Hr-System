using HR.Models;
using HR.Models.Infra;
using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
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
                var req = dbcontext.check_Request.ToList();
                foreach (var item in req)
                {
                    item.date = item.Request_date.ToString("dd/MM/yyyy");
                }
                return Json(req);
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
        public JsonResult get_num_of_free_jobs(string id)
        {
            try
            {
                var ID = int.Parse(id);
                var record = dbcontext.job_level_setup.FirstOrDefault(m => m.ID == ID);
                var job_titles = dbcontext.job_title_cards.Where(m => m.joblevelsetupID == record.ID.ToString()).ToList();
                var count = 0;
                foreach(var item in job_titles)
                {
                    count = count + item.number_vacant;
                }
                var model = new manpoweritems { count = count, job_level_setup = record };
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

            var model = dbcontext.job_level_setup.ToList();
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
            var ID = int.Parse(id);
            var model = dbcontext.Organization_Unit_Type.FirstOrDefault(m => m.ID == ID);
            return Json(model);

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
        public JsonResult GetEmployee(string id)
        {
           
            var ID = int.Parse(id);
            var Employee = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
            return Json(Employee);
            dbcontext.Configuration.ProxyCreationEnabled = false;
        }

        public JsonResult CheckAddressEmployee(string id)
        {
            var ID = int.Parse(id);
            var emps = (from emp in dbcontext.Employee_Profile
                        join add in dbcontext.Employee_Address_Profile on emp.ID.ToString() equals add.Employee_ProfileId
                        where emp.ID == ID
                        select new {namex= add.Transportation_method ,nameb=add.countryid} 
                     ).FirstOrDefault();
            return Json(emps);

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
            var Slots = dbcontext.Slots.Where(m => m.job_title_cards.ID == ID && ((m.EmployeeID == null|| m.EmployeeID == "0") || m.EmployeeID == did)).Where(m=>m.slot_type == slot_type.Active).ToList().Select(m => new { Code = m.slot_code + "------[" + m.slot_description + ']', ID = m.ID });
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
            var RejectionReasons = dbcontext.Rejection_Reasons.FirstOrDefault(m => m.ID == ID );
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
            var model = dbcontext.Organization_Chart.ToList();
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
                foreach(var item in ii)
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
            catch(Exception e)
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
            dbcontext.Configuration.ProxyCreationEnabled = false;
            int ID2 = int.Parse(id);
                var family = dbcontext.Employee_family_profile.FirstOrDefault(m => m.ID == ID2);
           
                return Json(family);
            
        }














    }

}