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
    public class Applicant_Qualification_ProfileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Applicant_Qualification_Profile
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Applicant_Qualification_Profile.Where(m => m.Applicant_Profile.ID == ID).ToList();
            var record = dbcontext.Applicant_Qualification_Profile.FirstOrDefault(m => m.ID == ID);
            ViewBag.idemp = id;
            return View(new_model);
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
            ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Recuirtment);
            var model = dbcontext.Applicant_Qualification_Profile.ToList();
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

            var ID = int.Parse(id);
            var emp = dbcontext.Applicant_Profile.FirstOrDefault(m => m.ID == ID);
            DateTime statis = Convert.ToDateTime("1/1/1900");//emp.ID.ToString()
            var ApplicantQualification = new Applicant_Qualification_Profile { Applicant_Profile = emp, Employee_ProfileId = emp.ID.ToString(), Code = stru.Structure_Code + count.ToString(), Qualification_start_date = statis, Qualification_end_date = statis, Allowance_value = 0.0 };
            ViewBag.iod = ApplicantQualification.Applicant_Profile.ID;
            return View(ApplicantQualification);

        }
        [HttpPost]
        public ActionResult Create(Applicant_Qualification_Profile model, string command,int ID2)
        {
            try
            {
                ViewBag.iod =ID2;
                ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Educate_category = dbcontext.Educate_category.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Educate_Title = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Main_Educate_body = dbcontext.Main_Educate_body.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Sub_educational_body = dbcontext.Sub_educational_body.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Name_of_educational_qualification = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Qualification_Major = dbcontext.Qualification_Major.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.GradeEducate = dbcontext.GradeEducate.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var EmpObj = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == ID2);

                Applicant_Qualification_Profile record = new Applicant_Qualification_Profile();
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = EmpObj.ID.ToString() : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                record.Applicant_Profile = EmpObj;

                record.Code = model.Code;
                if (model.Educate_categoryId == 0) { model.Educate_categoryId = null; }
                if (model.Educate_TitleId == 0) { model.Educate_TitleId = null; }
                if (model.Main_Educate_bodyId == 0) { model.Main_Educate_bodyId = null; }
                if (model.Sub_educational_bodyId == 0) { model.Sub_educational_bodyId = null; }
                if (model.Name_of_educational_qualificationId == 0) { model.Name_of_educational_qualificationId = null; }
                if (model.Qualification_MajorId == 0) { model.Qualification_MajorId = null; }
                if (model.GradeEducateId == 0) { model.GradeEducateId = null; }
                //if (ModelState.IsValid)
                //{
                    //if (model.Educate_categoryId == "0" || model.Educate_categoryId == null)
                    //{
                    //    ModelState.AddModelError("", HR.Resource.Personnel.EducatecategoryCodemustenter);
                    //    return View(model);
                    //}
                    //if (model.Educate_TitleId == "0" || model.Educate_TitleId == null)
                    //{
                    //    ModelState.AddModelError("", HR.Resource.Personnel.EducateTitleCodemustenter);
                    //    return View(model);
                    //}
                    //if (model.Main_Educate_bodyId == "0" || model.Main_Educate_bodyId == null)
                    //{
                    //    ModelState.AddModelError("", HR.Resource.Personnel.MainEducatebodyCodemustenter);
                    //    return View(model);
                    //}
                    //if (model.Sub_educational_bodyId == "0" || model.Sub_educational_bodyId == null)
                    //{
                    //    ModelState.AddModelError("", HR.Resource.Personnel.SubeducationalbodyCodemustenter);
                    //    return View(model);
                    //}
                    //if (model.Name_of_educational_qualificationId == "0" || model.Name_of_educational_qualificationId == null)
                    //{
                    //    ModelState.AddModelError("", HR.Resource.Personnel.NameofeducationalqualificationCodemustenter);
                    //    return View(model);
                    //}
                    //if (model.Qualification_MajorId == "0" || model.Qualification_MajorId == null)
                    //{
                    //    ModelState.AddModelError("", HR.Resource.Personnel.QualificationMajorCodemustenter);
                    //    return View(model);
                    //}
                    //if (model.GradeEducateId == "0" || model.GradeEducateId == null)
                    //{
                    //    ModelState.AddModelError("", HR.Resource.Personnel.GradeEducateCodemustenter);
                    //    return View(model);
                    //}
                    //if (model.Employee_ProfileId == 0 || model.Employee_ProfileId == null)
                    //{
                    //    ModelState.AddModelError("", HR.Resource.Personnel.EmployeeProfileCodemustenter);
                    //    return View(model);
                    //}
                    //   var prof = int.Parse(model.Employee_ProfileId);
                    //     var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == prof);
                    //             var record = dbcontext.Employee_Qualification_Profile.FirstOrDefault(m => m.ID == emp.Employee_Qualification_Profile.ID);

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
                    //record.Employee_ProfileId = model.Employee_ProfileId;
                    //var Employee_ProfileId = int.Parse(model.Employee_ProfileId);
                    record.Educate_categoryId = model.Educate_categoryId;
                    record.Educate_TitleId = model.Educate_TitleId;
                    record.Main_Educate_bodyId = model.Main_Educate_bodyId;
                    record.Sub_educational_bodyId = model.Sub_educational_bodyId;
                    record.Name_of_educational_qualificationId = model.Name_of_educational_qualificationId;
                    record.Qualification_MajorId = model.Qualification_MajorId;
                    record.GradeEducateId = model.GradeEducateId;
                    dbcontext.Applicant_Qualification_Profile.Add(record);
                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("edit", "Applicant_Profile", new { id = EmpObj.ID });
                    }
                    return RedirectToAction("Index", new { id = EmpObj.ID });
                //}

                //else
                //{
                //    return View(model);
                //}

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
                ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Educate_category = dbcontext.Educate_category.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Educate_Title = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Main_Educate_body = dbcontext.Main_Educate_body.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Sub_educational_body = dbcontext.Sub_educational_body.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Name_of_educational_qualification = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Qualification_Major = dbcontext.Qualification_Major.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.GradeEducate = dbcontext.GradeEducate.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                var record = dbcontext.Applicant_Qualification_Profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Applicant_Profile.ID.ToString();

                if (record != null)
                {
                    ViewBag.iod = record.Applicant_Profile.ID;
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
        public ActionResult Edit(Applicant_Qualification_Profile model, string command,int ID2)
        {
            try
            {
                if (model.Educate_categoryId == 0) { model.Educate_categoryId = null; }
                if (model.Educate_TitleId == 0) { model.Educate_TitleId = null; }
                if (model.Main_Educate_bodyId == 0) { model.Main_Educate_bodyId = null; }
                if (model.Sub_educational_bodyId == 0) { model.Sub_educational_bodyId = null; }
                if (model.Name_of_educational_qualificationId == 0) { model.Name_of_educational_qualificationId = null; }
                if (model.Qualification_MajorId == 0) { model.Qualification_MajorId = null; }
                if (model.GradeEducateId == 0) { model.GradeEducateId = null; }
                ViewBag.iod = ID2;

                ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Educate_category = dbcontext.Educate_category.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Educate_Title = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Main_Educate_body = dbcontext.Main_Educate_body.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Sub_educational_body = dbcontext.Sub_educational_body.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Name_of_educational_qualification = dbcontext.Name_of_educational_qualification.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Qualification_Major = dbcontext.Qualification_Major.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.GradeEducate = dbcontext.GradeEducate.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });


                var EmpObj = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == ID2);

                var record = dbcontext.Applicant_Qualification_Profile.FirstOrDefault(m => m.ID == model.ID);
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == null ? model.Employee_ProfileId = EmpObj.ID.ToString() : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                record.Applicant_Profile = EmpObj;
                //if (model.Employee_ProfileId == "0" || model.Employee_ProfileId == null)
                //{
                //    ModelState.AddModelError("", "Employee Profile Code must enter");
                //    return View(model);
                //}
                //if (model.Educate_categoryId == "0" || model.Educate_categoryId == null)
                //{
                //    ModelState.AddModelError("", HR.Resource.Personnel.EducatecategoryCodemustenter);
                //    return View(model);
                //}
                //if (model.Educate_TitleId == "0" || model.Educate_TitleId == null)
                //{
                //    ModelState.AddModelError("", HR.Resource.Personnel.EducateTitleCodemustenter);
                //    return View(model);
                //}
                //if (model.Main_Educate_bodyId == "0" || model.Main_Educate_bodyId == null)
                //{
                //    ModelState.AddModelError("", HR.Resource.Personnel.MainEducatebodyCodemustenter);
                //    return View(model);
                //}
                //if (model.Sub_educational_bodyId == "0" || model.Sub_educational_bodyId == null)
                //{
                //    ModelState.AddModelError("", HR.Resource.Personnel.SubeducationalbodyCodemustenter);
                //    return View(model);
                //}
                //if (model.Name_of_educational_qualificationId == "0" || model.Name_of_educational_qualificationId == null)
                //{
                //    ModelState.AddModelError("", HR.Resource.Personnel.NameofeducationalqualificationCodemustenter);
                //    return View(model);
                //}
                //if (model.Qualification_MajorId == "0" || model.Qualification_MajorId == null)
                //{
                //    ModelState.AddModelError("", HR.Resource.Personnel.QualificationMajorCodemustenter);
                //    return View(model);
                //}
                //if (model.GradeEducateId == "0" || model.GradeEducateId == null)
                //{
                //    ModelState.AddModelError("", HR.Resource.Personnel.GradeEducateCodemustenter);
                //    return View(model);
                //}

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
                record.Educate_TitleId = model.Educate_TitleId;
                record.Main_Educate_bodyId = model.Main_Educate_bodyId;
                record.Sub_educational_bodyId = model.Sub_educational_bodyId;
                record.Name_of_educational_qualificationId = model.Name_of_educational_qualificationId;
                record.Qualification_MajorId = model.Qualification_MajorId;
                record.GradeEducateId = model.GradeEducateId;

                dbcontext.SaveChanges();

                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Applicant_Profile", new { id = EmpObj.ID });
                }
                return RedirectToAction("index", new { id = EmpObj.ID });
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

                var record = dbcontext.Applicant_Qualification_Profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Applicant_Profile.ID.ToString();
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

            var record = dbcontext.Applicant_Qualification_Profile.FirstOrDefault(m => m.ID == id);
            ViewBag.idemp = record.Applicant_Profile.ID.ToString();
            try
            {
                dbcontext.Applicant_Qualification_Profile.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index", new { id = record.Employee_ProfileId });
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.youcannotdeletethisRow;
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}