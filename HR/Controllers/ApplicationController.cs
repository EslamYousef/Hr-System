using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;
using HR.Models.Infra;
using HR.Models.ViewModel;
using System.IO;
using HR.Models.Application;

namespace HR.Controllers
{
    [Authorize]
    public class ApplicationController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Application
        public ActionResult Index()
        {
            var Applicant_Profile = dbcontext.Applicant_Profile.ToList();
            var Applications = dbcontext.Application.ToList();
            var model = from a in Applicant_Profile
                        join b in Applications on a.ID.ToString() equals b.Applicant_ProfileId
                        select new Applicant_VM
                        {
                            FullNameAr = a.Full,
                            FullNameEn = a.Full_Name,
                            Code = a.Code,
                            ApplicantId = a.ID,
                            Application = b
                        };
            return View(model);
        
        }
        public ActionResult Create(string id)
        {
            ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.Where(a => a.Committe_Usage == Committe_Usage.Test).ToList().Select(m => new { Code = m.Code, ID = m.ID });
            ViewBag.Applicant_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

            DateTime statis = DateTime.Now;
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Recuirtment);
            var model = dbcontext.Application.ToList();
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
            var Applicant = new Application { Code = stru.Structure_Code + count };
            return View(Applicant);
        }
        [HttpPost]
        public ActionResult Create(Application model, HttpPostedFileBase MyItem, string Command)
        {
            try
            {
                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.Where(a => a.Committe_Usage == Committe_Usage.Test).ToList().Select(m => new { Code = m.Code , ID = m.ID });
                ViewBag.Applicant_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                if (model.Applicant_ProfileId == "0" || model.Applicant_ProfileId == null)
                {
                    ModelState.AddModelError("", "Applicant Profile must enter");
                    return View(model);
                }

                Application record = new Application();

                if (ModelState.IsValid)
                {
                    record.Code = model.Code;
                    record.Applicant_ProfileId = model.Applicant_ProfileId;
                    record.CommiteeTestNo = model.CommiteeTestNo;
                    var emp = dbcontext.Application.Add(record);
                    dbcontext.SaveChanges();

                    if (Command == "Submit")
                    {
                        return RedirectToAction("Create", "Contract_Information", new { id = record.ID });
                    }

                    if (Command == "Submit2")
                    {
                        return RedirectToAction("Create", "Position_Information", new { id = record.ID });
                    }
                    if (Command == "Submit3")
                    {
                        return RedirectToAction("Create", "Basic_Salary_Calculation_Result", new { id = record.ID });
                    }
                    if (Command == "Submit4")
                    {
                        return RedirectToAction("Create", "Hiring_Information", new { id = record.ID });
                    }
                    if (Command == "Submit5")
                    {
                        return RedirectToAction("Edit", "Applicant_Profile", new { id = record.Applicant_Profile.ID });
                    }
                    if (Command == "Submit6")
                    {
                        return RedirectToAction("Create", "Application_Status", new { id = record.ID });
                    }
                    if (Command == "Submit7")
                    {
                        return RedirectToAction("Create", "Business_Test_Profile", new { id = record.ID });
                    }
                    if (Command == "Submit8")
                    {
                        return RedirectToAction("Create", "Medical_Test_Profile", new { id = record.ID });
                    }
                    //if (MyItem == null)
                    //{
                    //    emp.EmpProfileIMG = null;

                    //}
                    //else if (MyItem.FileName != null)
                    //{
                    //    var code = record.Code;
                    //    string folderpath = Server.MapPath("~/RecIMGFiles/") ;
                    //    Directory.CreateDirectory(folderpath + code);
                    //    string mypath = folderpath + code;
                    //    string filename = Guid.NewGuid() + Path.GetExtension(MyItem.FileName);
                    //    MyItem.SaveAs(mypath + "/" + filename);

                    //    model.EmpProfileIMG = filename;
                    //    emp.EmpProfileIMG = model.EmpProfileIMG;
                    //}
                    dbcontext.SaveChanges();
                    ///////////////////////////////////////////////////////////////////////////
                }
        

                return RedirectToAction("Index");
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
                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.Where(a => a.Committe_Usage == Committe_Usage.Test).ToList().Select(m => new { Code = m.Code, ID = m.ID });
                ViewBag.Applicant_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var record = dbcontext.Application.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(Application model, HttpPostedFileBase MyItem, string Command)
        {
            try
            {
                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.Where(a => a.Committe_Usage == Committe_Usage.Test).ToList().Select(m => new { Code = m.Code, ID = m.ID });
                ViewBag.Applicant_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var record = dbcontext.Application.FirstOrDefault(m => m.ID == model.ID);

                record.Code = model.Code;
                record.CommiteeTestNo = model.CommiteeTestNo;
                dbcontext.SaveChanges();
                //if (MyItem != null)
                //{
                //    var code = model.Code;
                //    string folderpath = Server.MapPath("~/RecIMGFiles/");
                //    Directory.CreateDirectory(folderpath + code);
                //    string mypath = folderpath + code;
                //    string filename = Guid.NewGuid() + Path.GetExtension(MyItem.FileName);
                //    MyItem.SaveAs(mypath + "/" + filename);
                //    model.EmpProfileIMG = filename;
                //    record.EmpProfileIMG = model.EmpProfileIMG;
                //}
                var ContractInformation = dbcontext.Contract_Information.FirstOrDefault(a => a.ApplicantId == model.ID);
                var Position_Information = dbcontext.Position_Information_Rec.FirstOrDefault(a => a.ApplicantId == model.ID);
                var Basic_Salary_Calculation_Result = dbcontext.Basic_Salary_Calculation_Result.FirstOrDefault(a => a.ApplicantId == model.ID);
                var Hiring_Information = dbcontext.Hiring_Information.FirstOrDefault(a => a.ApplicantId == model.ID);
                var Application_Status = dbcontext.Application_Status.FirstOrDefault(a => a.ApplicantId == model.ID);
                var Business_Test_Profile = dbcontext.Business_Test_Profile.FirstOrDefault(a => a.ApplicantId == model.ID);
                var Medical_Test_Profile = dbcontext.Medical_Test_Profile.FirstOrDefault(a => a.ApplicantId == model.ID);

                if (ContractInformation != null && Command == "Submit")
                {
                    return RedirectToAction("Edit", "Contract_Information", new { id = record.ID });
                }
                else if (ContractInformation == null && Command == "Submit")
                {
                    return RedirectToAction("Create", "Contract_Information", new { id = record.ID });
                }
                if (Position_Information != null && Command == "Submit2")
                {
                    return RedirectToAction("Edit", "Position_Information", new { id = record.ID });
                }
                else if (Position_Information == null && Command == "Submit2")
                {
                    return RedirectToAction("Create", "Position_Information", new { id = record.ID });
                }
                if (Basic_Salary_Calculation_Result != null && Command == "Submit3")
                {
                    return RedirectToAction("Edit", "Basic_Salary_Calculation_Result", new { id = record.ID });
                }
                else if (Basic_Salary_Calculation_Result == null && Command == "Submit3")
                {
                    return RedirectToAction("Create", "Basic_Salary_Calculation_Result", new { id = record.ID });
                }
                if (Hiring_Information != null && Command == "Submit4")
                {
                    return RedirectToAction("Edit", "Hiring_Information", new { id = record.ID });
                }
                else if (Hiring_Information == null && Command == "Submit4")
                {
                    return RedirectToAction("Create", "Hiring_Information", new { id = record.ID });
                }             
                if (Command == "Submit5")
                {
                    return RedirectToAction("Edit", "Applicant_Profile", new { id = record.Applicant_Profile.ID });
                }
                if (Application_Status != null && Command == "Submit6")
                {
                    return RedirectToAction("Edit", "Application_Status", new { id = record.ID });
                }
                else if (Application_Status == null && Command == "Submit6")
                {
                    return RedirectToAction("Create", "Application_Status", new { id = record.ID });
                }
                if (Business_Test_Profile != null && Command == "Submit7")
                {
                    return RedirectToAction("Edit", "Business_Test_Profile", new { id = record.ID });
                }
                else if (Business_Test_Profile == null && Command == "Submit7")
                {
                    return RedirectToAction("Create", "Business_Test_Profile", new { id = record.ID });
                }
                if (Medical_Test_Profile != null && Command == "Submit8")
                {
                    return RedirectToAction("Edit", "Medical_Test_Profile", new { id = record.ID });
                }
                else if (Medical_Test_Profile == null && Command == "Submit8")
                {
                    return RedirectToAction("Create", "Medical_Test_Profile", new { id = record.ID });
                }




                if (Command == "Submit5")
                {
                    return RedirectToAction("Edit", "Applicant_Profile", new { id = record.ID });
                }
                if (Command == "Submit6")
                {
                    return RedirectToAction("Create", "Application_Status", new { id = record.ID });
                }
                if (Command == "Submit7")
                {
                    return RedirectToAction("Create", "Business_Test_Profile", new { id = record.ID });
                }
                if (Command == "Submit8")
                {
                    return RedirectToAction("Create", "Medical_Test_Profile", new { id = record.ID });
                }
                return RedirectToAction("index");
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }







    }
}