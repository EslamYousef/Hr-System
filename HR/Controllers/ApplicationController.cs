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
    [Authorize(Roles = "Admin,Recuirtment,RecuirtmentCards,Applications Rec")]
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
                        return RedirectToAction("Edit", "Applicant_Profile", new { id = record.Applicant_ProfileId });
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
                    if (Command == "Submit9")
                    {
                        return RedirectToAction("Create", "Personnel_Committee_Profile", new { id = record.ID });
                    }
                    if (Command == "Submit10")
                    {
                        return RedirectToAction("link", "Application", new { id = record.ID });
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
                //var ID = int.Parse(id);

                ViewBag.Committe_Resolution_Recuirtment = dbcontext.Committe_Resolution_Recuirtment.Where(a => a.Committe_Usage == Committe_Usage.Test).ToList().Select(m => new { Code = m.Code, ID = m.ID });
                ViewBag.Applicant_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

              
                var record = dbcontext.Application.FirstOrDefault(m => m.ID == id);
                if (record != null)
                {
                    //ViewBag.idemp = ID;
                    return View(record); }

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
        public ActionResult Edit(Application model, HttpPostedFileBase MyItem, string Command, string id2)
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
                var Personnel_Committee_Profile = dbcontext.Personnel_Committee_Profile.FirstOrDefault(a => a.ApplicantId == model.ID);
                var Hiring_Check_List = dbcontext.Hiring_Check_List.FirstOrDefault(a => a.ApplicantId == model.ID);
                
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
                    return RedirectToAction("Edit", "Applicant_Profile", new { id = record.Applicant_ProfileId , nid =record.Code});
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
                if (Personnel_Committee_Profile != null && Command == "Submit9")
                {
                    return RedirectToAction("Edit", "Personnel_Committee_Profile", new { id = record.ID });
                }
                else if (Personnel_Committee_Profile == null && Command == "Submit9")
                {
                    return RedirectToAction("Create", "Personnel_Committee_Profile", new { id = record.ID });
                }
                if (Command == "Submit10")
                {
                    return RedirectToAction("link", "Application", new { id = record.ID, tag = "0" });
                }
                //if (Hiring_Check_List != null && Command == "Submit10")
                //{
                //    return RedirectToAction("Edit", "Hiring_Check_List", new { id = record.ID });
                //}
                //else if (Hiring_Check_List == null && Command == "Submit10")
                //{
                //    return RedirectToAction("Create", "Hiring_Check_List", new { id = record.ID });
                //}


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

        public ActionResult HireProcess()
        {
            //ViewBag.idemp = id2;
            //var ID = int.Parse(id2);
            //var addmodel1 = dbcontext.Employee_Qualification_Profile.ToList();
            //var tr = 0;

            //if (addmodel1.Count() == 0)
            //{
            //    tr = 1;
            //}
            //else
            //{
            //    var te = addmodel1.LastOrDefault().ID;
            //    tr = te + 1;
            //}
            //DateTime statis = Convert.ToDateTime("1/1/1900");
            //var strus = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            //var text = new Employee_Qualification_Profile
            //{ Employee_ProfileId = emp.ID.ToString(), Code = strus.Structure_Code + tr.ToString(), Qualification_start_date = statis, Qualification_end_date = statis };
            //var e = dbcontext.Employee_Qualification_Profile.Add(text);
            //dbcontext.SaveChanges();

            //emp.Employee_Qualification_Profile = e;
            //dbcontext.SaveChanges();
            var Applicant_Profile = dbcontext.Applicant_Profile.ToList();
            var Application = dbcontext.Application.FirstOrDefault(a=>a.Applicant_ProfileId == a.Applicant_Profile.ID.ToString());
           
            var Employee_Profile = dbcontext.Employee_Profile.ToList();
            var tr = 0;
            if (Employee_Profile.Count() == 0)
            {
                tr = 1;
            }
            else
            {
                var te = Employee_Profile.LastOrDefault().ID;
                tr = te + 1;
            }
            DateTime statis = DateTime.Now;
            var strus = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
           
            var Ability = new Ability { registration_date = statis };
            var Personnel_Information = new Personnel_Information { Hire_Date = statis, Join_Date = statis, Boarding_Date = statis, Sector_Join_Date = statis, Social_Insurance_Date = statis };
            var Service_Information = new Service_Information { EOS_date = statis, Last_working_date = statis, Retired_expected_EOS = statis };

            var ab = dbcontext.Ability.Add(Ability);
            var per = dbcontext.Personnel_Information.Add(Personnel_Information);
            var ser = dbcontext.Service_Information.Add(Service_Information);
            dbcontext.SaveChanges();
            var emp = new Employee_Profile { ID = Employee_Profile.LastOrDefault().ID, Code = strus.Structure_Code + tr.ToString(), Name = Application.Applicant_Profile.Name, Full = Application.Applicant_Profile.Full, Full_Name = Application.Applicant_Profile.Full_Name, Birth_date = Application.Applicant_Profile.Birth_date, Expire_date = Application.Applicant_Profile.Expire_date, Issue_date = Application.Applicant_Profile.Issue_date, ReligionId = Application.Applicant_Profile.ReligionId.ToString(), NationalityId = Application.Applicant_Profile.NationalityId.ToString(), Active = true ,Ability=ab,Personnel_Information=per,Service_Information=ser,Surname= Application.Applicant_Profile.Surname, Sur_Name = Application.Applicant_Profile.Sur_Name };
            var e = dbcontext.Employee_Profile.Add(emp);
            dbcontext.SaveChanges();

            return RedirectToAction("index");
        }
        public ActionResult link(string id, string tag)
            {
            try
            {
                if (tag == "0")
                {
                    var ID = int.Parse(id);
                    var All_Sub = dbcontext.Check_List_Items.Where(m => m.Name_of_educational_qualification_ns == id &&m.Required_on_application==true ).ToList();
                    var mainqulifications = dbcontext.Check_List_Items.Where(a=>a.Required_on_application==true).ToList();
                    var listt = new List<SelectListItem>();
                    foreach (var team in mainqulifications)
                    {
                        //if (mainqulifications.Any(ma => ma.ID == team.Name_of_educational_qualification.ID))
                        if (All_Sub.Any(ma => ma.ID == team.ID))
                        {
                            listt.Add(new SelectListItem
                            {
                                Text = team.Code + "-----" + team.Name,
                                Value = team.ID.ToString(),
                                Selected = true
                            });
                        }
                        else
                        {
                            listt.Add(new SelectListItem
                            {
                                Text = team.Code + "-----" + team.Name
                                                     ,
                                Value = team.ID.ToString(),
                                Selected = false
                            });
                        }

                    }
                    var model = new Assign_subqulifications_ViewModel { AvailableFruits = listt, nameID = id };
                    return View(model);
                }
                else
                {
                    var model = new Assign_subqulifications_ViewModel { AvailableFruits = Getitems(), nameID = id };
                    return View(model);
                }


            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult link(Assign_subqulifications_ViewModel model)
        {
            var id = int.Parse(model.nameID);
            var name = dbcontext.Application.FirstOrDefault(m => m.ID == id);
            if (true)
            {
                var ID = int.Parse(model.nameID);
                var sub = dbcontext.Check_List_Items.ToList();
                if (sub != null)
                {
                    foreach (var item in sub)
                    {
                        item.Name_of_educational_qualification_ns = null;
                        item.Name_of_educational_qualification = null;
                        dbcontext.SaveChanges();
                    }
                }

            }
            if (model.SelectedFruits != null)
            {
                foreach (var item in model.SelectedFruits)
                {
                    var ID = int.Parse(item);
                    var sub = dbcontext.Check_List_Items.FirstOrDefault(m => m.ID == ID);
                    sub.Name_of_educational_qualification_ns = name.ID.ToString();
                    sub.Name_of_educational_qualification = name;
                    dbcontext.SaveChanges();
                }
            }
            return RedirectToAction("index");
        }

        private IList<SelectListItem> Getitems()
        {
            var mainqulifications = dbcontext.Check_List_Items.Where(a=>a.Required_on_application==true).ToList();
            var listt = new List<SelectListItem>();
            foreach (var team in mainqulifications)
            {
                listt.Add(new SelectListItem
                {
                    Text = team.Code + "-----" + team.Name
                                             ,
                    Value = team.ID.ToString()
                });
            }
            return listt;

        }




    }
}