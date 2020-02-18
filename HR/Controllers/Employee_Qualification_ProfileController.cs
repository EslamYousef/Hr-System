using HR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models.Infra;
using HR.Models.ViewModel;

namespace HR.Controllers
{
    [Authorize]
    public class Employee_Qualification_ProfileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        // GET: Employee_Qualification_Profile
        public ActionResult Index()
        {
            var employee = dbcontext.Employee_Profile.ToList();
            var qualification = dbcontext.Employee_Qualification_Profile.ToList();
            var model = from a in employee
                        join b in qualification on a.Employee_Qualification_Profile.ID equals b.ID
                        select new Employee_Qualification_VM
                        {
                            fullname = a.Full_Name,
                            code = a.Code,
                            EmployeeId = a.ID,
                            Employee_Qualification_Profile = b
                        };
            return View(model);
        }
        public ActionResult Create(string id)
        {

            ViewBag.Educate_category = dbcontext.Educate_category.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Educate_Title = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Main_Educate_body = dbcontext.Main_Educate_body.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Sub_educational_body = dbcontext.Sub_educational_body.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Name_of_educational_qualification = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Qualification_Major = dbcontext.Qualification_Major.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.GradeEducate = dbcontext.GradeEducate.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Employee_Qualification_Profile.ToList();
            var count = 0;
            if (model.Count() == 0)
            {
                count = 1;
            }
            else
            {
                var te = model.LastOrDefault().ID;
                count = te + 1;
            }
            if (id != null)
            {
                var ID = int.Parse(id);
                var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
                var x = emp.Employee_Qualification_Profile;
                return View(x);
            }
            DateTime statis = Convert.ToDateTime("1/1/1900");
            var EmployeeQualification = new Employee_Qualification_Profile { Code = stru.Structure_Code + count, Qualification_start_date = statis, Qualification_end_date = statis, Allowance_value = 0.0 };
            //   var EmployeeQualification = new Employee_Qualification_Profile();
            return View(EmployeeQualification);

        }
        [HttpPost]
        public ActionResult Create(Employee_Qualification_Profile model, string command)
        {
            try
            {


                ViewBag.Educate_category = dbcontext.Educate_category.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Educate_Title = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Main_Educate_body = dbcontext.Main_Educate_body.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Sub_educational_body = dbcontext.Sub_educational_body.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Name_of_educational_qualification = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Qualification_Major = dbcontext.Qualification_Major.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.GradeEducate = dbcontext.GradeEducate.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                if (ModelState.IsValid)
                {
                    if (model.Educate_categoryId == "0" || model.Educate_categoryId == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Personnel.EducatecategoryCodemustenter);
                        return View(model);
                    }
                    if (model.Educate_TitleId == "0" || model.Educate_TitleId == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Personnel.EducateTitleCodemustenter);
                        return View(model);
                    }
                    if (model.Main_Educate_bodyId == "0" || model.Main_Educate_bodyId == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Personnel.MainEducatebodyCodemustenter);
                        return View(model);
                    }
                    if (model.Sub_educational_bodyId == "0" || model.Sub_educational_bodyId == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Personnel.SubeducationalbodyCodemustenter);
                        return View(model);
                    }
                    if (model.Name_of_educational_qualificationId == "0" || model.Name_of_educational_qualificationId == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Personnel.NameofeducationalqualificationCodemustenter);
                        return View(model);
                    }
                    if (model.Qualification_MajorId == "0" || model.Qualification_MajorId == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Personnel.QualificationMajorCodemustenter);
                        return View(model);
                    }
                    if (model.GradeEducateId == "0" || model.GradeEducateId == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Personnel.GradeEducateCodemustenter);
                        return View(model);
                    }
                    if (model.Employee_ProfileId == "0" || model.Employee_ProfileId == null)
                    {
                        ModelState.AddModelError("", HR.Resource.Personnel.EmployeeProfileCodemustenter);
                        return View(model);
                    }
                    var prof = int.Parse(model.Employee_ProfileId);
                    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == prof);
                    var record = dbcontext.Employee_Qualification_Profile.FirstOrDefault(m => m.ID == emp.Employee_Qualification_Profile.ID);
                    record.Related_to_job = model.Related_to_job;
                    record.Qualification_start_date = model.Qualification_start_date;
                    record.Qualification_end_date = model.Qualification_end_date;
                    if (model.Qualification_start_date > model.Qualification_end_date)
                    {
                        TempData["Message"] = HR.Resource.Personnel.QualificationstartdatebiggerQualificationenddate;
                        return View(model);
                    }
                    record.Years = model.Years;
                    record.Months = model.Months;
                    record.Extra_education_years = model.Extra_education_years;
                    record.Allowance_value = model.Allowance_value;
                    record.Employee_ProfileId = model.Employee_ProfileId;
                    var Employee_ProfileId = int.Parse(model.Employee_ProfileId);
                    record.Educate_categoryId = model.Educate_categoryId;
                    var Educate_categoryId = int.Parse(model.Educate_categoryId);
                    record.Educate_category = dbcontext.Educate_category.FirstOrDefault(m => m.ID == Educate_categoryId);
                    record.Educate_TitleId = model.Educate_TitleId;
                    var Educate_TitleId = int.Parse(model.Educate_TitleId);
                    record.Educate_Title = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == Educate_TitleId);
                    record.Main_Educate_bodyId = model.Main_Educate_bodyId;
                    var Main_Educate_bodyId = int.Parse(model.Main_Educate_bodyId);
                    record.Main_Educate_body = dbcontext.Main_Educate_body.FirstOrDefault(m => m.ID == Main_Educate_bodyId);
                    record.Sub_educational_bodyId = model.Sub_educational_bodyId;
                    var Sub_educational_bodyId = int.Parse(model.Sub_educational_bodyId);
                    record.Sub_educational_body = dbcontext.Sub_educational_body.FirstOrDefault(m => m.ID == Sub_educational_bodyId);
                    record.Name_of_educational_qualificationId = model.Name_of_educational_qualificationId;
                    var Name_of_educational_qualificationId = int.Parse(model.Name_of_educational_qualificationId);
                    record.Name_of_educational_qualification = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == Name_of_educational_qualificationId);
                    record.Qualification_MajorId = model.Qualification_MajorId;
                    var Qualification_MajorId = int.Parse(model.Qualification_MajorId);
                    record.Qualification_Major = dbcontext.Qualification_Major.FirstOrDefault(m => m.ID == Qualification_MajorId);
                    record.GradeEducateId = model.GradeEducateId;
                    var GradeEducateId = int.Parse(model.GradeEducateId);
                    record.GradeEducate = dbcontext.GradeEducate.FirstOrDefault(m => m.ID == GradeEducateId);

                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(record.Employee_ProfileId) });
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
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
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Educate_category = dbcontext.Educate_category.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Educate_Title = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Main_Educate_body = dbcontext.Main_Educate_body.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Sub_educational_body = dbcontext.Sub_educational_body.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Name_of_educational_qualification = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Qualification_Major = dbcontext.Qualification_Major.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.GradeEducate = dbcontext.GradeEducate.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Employee_Qualification_Profile.FirstOrDefault(m => m.ID == id);
                if (record != null)
                {
                    return View(record);
                }
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
        public ActionResult Edit(Employee_Qualification_Profile model, string command)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Educate_category = dbcontext.Educate_category.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Educate_Title = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Main_Educate_body = dbcontext.Main_Educate_body.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Sub_educational_body = dbcontext.Sub_educational_body.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Name_of_educational_qualification = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Qualification_Major = dbcontext.Qualification_Major.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.GradeEducate = dbcontext.GradeEducate.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });


                var EmpObj = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.ID);

                var record = dbcontext.Employee_Qualification_Profile.FirstOrDefault(m => m.ID == model.ID);
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                var empl = record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = EmpObj.ID.ToString() : model.Employee_ProfileId;

                //if (model.Employee_ProfileId == "0" || model.Employee_ProfileId == null)
                //{
                //    ModelState.AddModelError("", "Employee Profile Code must enter");
                //    return View(model);
                //}
                if (model.Educate_categoryId == "0" || model.Educate_categoryId == null)
                {
                    ModelState.AddModelError("", HR.Resource.Personnel.EducatecategoryCodemustenter);
                    return View(model);
                }
                if (model.Educate_TitleId == "0" || model.Educate_TitleId == null)
                {
                    ModelState.AddModelError("", HR.Resource.Personnel.EducateTitleCodemustenter);
                    return View(model);
                }
                if (model.Main_Educate_bodyId == "0" || model.Main_Educate_bodyId == null)
                {
                    ModelState.AddModelError("", HR.Resource.Personnel.MainEducatebodyCodemustenter);
                    return View(model);
                }
                if (model.Sub_educational_bodyId == "0" || model.Sub_educational_bodyId == null)
                {
                    ModelState.AddModelError("", HR.Resource.Personnel.SubeducationalbodyCodemustenter);
                    return View(model);
                }
                if (model.Name_of_educational_qualificationId == "0" || model.Name_of_educational_qualificationId == null)
                {
                    ModelState.AddModelError("", HR.Resource.Personnel.NameofeducationalqualificationCodemustenter);
                    return View(model);
                }
                if (model.Qualification_MajorId == "0" || model.Qualification_MajorId == null)
                {
                    ModelState.AddModelError("", HR.Resource.Personnel.QualificationMajorCodemustenter);
                    return View(model);
                }
                if (model.GradeEducateId == "0" || model.GradeEducateId == null)
                {
                    ModelState.AddModelError("", HR.Resource.Personnel.GradeEducateCodemustenter);
                    return View(model);
                }
              
                record.Code = model.Code;
                record.Related_to_job = model.Related_to_job;
                record.Qualification_start_date = model.Qualification_start_date;
                record.Qualification_end_date = model.Qualification_end_date;
                if (model.Qualification_start_date > model.Qualification_end_date)
                {
                    TempData["Message"] = HR.Resource.Personnel.QualificationstartdatebiggerQualificationenddate;
                    return View(model);
                }
                record.Years = model.Years;
                record.Months = model.Months;
                record.Extra_education_years = model.Extra_education_years;
                record.Allowance_value = model.Allowance_value;

                record.Educate_categoryId = model.Educate_categoryId;
                var Educate_categoryId = int.Parse(model.Educate_categoryId);
                record.Educate_category = dbcontext.Educate_category.FirstOrDefault(m => m.ID == Educate_categoryId);
                record.Educate_TitleId = model.Educate_TitleId;
                var Educate_TitleId = int.Parse(model.Educate_TitleId);
                record.Educate_Title = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == Educate_TitleId);
                record.Main_Educate_bodyId = model.Main_Educate_bodyId;
                var Main_Educate_bodyId = int.Parse(model.Main_Educate_bodyId);
                record.Main_Educate_body = dbcontext.Main_Educate_body.FirstOrDefault(m => m.ID == Main_Educate_bodyId);
                record.Sub_educational_bodyId = model.Sub_educational_bodyId;
                var Sub_educational_bodyId = int.Parse(model.Sub_educational_bodyId);
                record.Sub_educational_body = dbcontext.Sub_educational_body.FirstOrDefault(m => m.ID == Sub_educational_bodyId);
                record.Name_of_educational_qualificationId = model.Name_of_educational_qualificationId;
                var Name_of_educational_qualificationId = int.Parse(model.Name_of_educational_qualificationId);
                record.Name_of_educational_qualification = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == Name_of_educational_qualificationId);
                record.Qualification_MajorId = model.Qualification_MajorId;
                var Qualification_MajorId = int.Parse(model.Qualification_MajorId);
                record.Qualification_Major = dbcontext.Qualification_Major.FirstOrDefault(m => m.ID == Qualification_MajorId);
                record.GradeEducateId = model.GradeEducateId;
                var GradeEducateId = int.Parse(model.GradeEducateId);
                record.GradeEducate = dbcontext.GradeEducate.FirstOrDefault(m => m.ID == GradeEducateId);

                dbcontext.SaveChanges();

                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Employee_Profile", new { id = int.Parse(record.Employee_ProfileId) });
                }
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
    }
}