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
namespace HR.Controllers
{
    [Authorize]
    public class Employee_attachment_profileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Employee_attachment_profile
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Employee_attachment_profile.Where(m => m.Employee_Profile.ID == ID).ToList();
            ViewBag.idemp = id;
            return View(new_model);
        }
        public ActionResult Create(string id)
        {

          
            ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Documents = dbcontext.Documents.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Employee_attachment_profile.ToList();
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
            //if (id != null)
            //{
            //    var ID = int.Parse(id);
            //    var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
            //    var x = emp.Employee_family_profile;
            //    return View(x);
            //}
            DateTime statis = Convert.ToDateTime("1/1/1900");
            var ID = int.Parse(id);
            var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == ID);
            var EmployeeAttachment = new Employee_attachment_profile { Employee_Profile = emp, Employee_ProfileId = emp.ID, Code = stru.Structure_Code + count.ToString(), Expiry_date = statis, Issue_date = statis };
            return View(EmployeeAttachment);

        }
        [HttpPost]
        public ActionResult Create(Employee_attachment_profile model, string command, HttpPostedFileBase MyItem)
        {
            try
            {



                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Documents = dbcontext.Documents.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var EmpObj = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.Employee_Profile.ID);
                Employee_attachment_profile record = new Employee_attachment_profile();
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == 0 ? model.Employee_ProfileId = EmpObj.ID : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                if (ModelState.IsValid)
                {
                    //  var family = int.Parse(model.Employee_ProfileId);
                    //   var emp = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == family);
                    //      var record = dbcontext.Employee_family_profile.FirstOrDefault(m => m.ID == emp.Employee_family_profile.ID);

               
                   
                    record.Code = model.Code;
                    record.Is_copy = model.Is_copy;
                    record.Is_required = model.Is_required;
                    record.DocumentGroup = model.DocumentGroup;
                    record.Related_to = model.Related_to;
                    record.Issued_place = model.Issued_place;
                    record.Reference_number = model.Reference_number;
                    record.Document_number = model.Document_number;
                    record.Document_description = model.Document_description;

                    record.Employee_Profile = EmpObj;
                    record.DocumentsId = model.DocumentsId;
                    record.Documents = dbcontext.Documents.FirstOrDefault(m => m.ID == model.DocumentsId);
                    record.Issue_date = model.Issue_date;
                    record.Expiry_date = model.Expiry_date;
                    if (model.Issue_date > model.Expiry_date)
                    {
                        TempData["Message"] = "Issue date  bigger Expiry date ";
                        return View(model);
                    }
                   
                    record.Document_status = model.Document_status;
                    record.Comments = model.Comments;

                    var fileId = 1;
                     var  File = fileId + 1;

                    string folderpath = Server.MapPath("~/files/") /*(@"c:\users\3lamya\documents\visual studio 2015\projects\systemuserfakahany\systemuserfakahany\files\")*/;
                    Directory.CreateDirectory(folderpath + File);
                    string mypath = folderpath + File;
                    string filename = Guid.NewGuid() + Path.GetExtension(MyItem.FileName);
                    MyItem.SaveAs(mypath + "/" + filename);
                    model.Attachmentfile = filename;
                    //record.Attachmentfile = model.Attachmentfile;

                    dbcontext.Employee_attachment_profile.Add(record);
                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("edit", "Employee_Profile", new { id = EmpObj.ID });
                    }
                    return RedirectToAction("Index", new { id = EmpObj.ID });
                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = "this code Is already exists";
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
                ViewBag.Documents = dbcontext.Documents.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var record = dbcontext.Employee_attachment_profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString();
                if (record != null)
                {
                    return View(record);
                }
                else
                {
                    TempData["Message"] = "There is no Employee Attachment Profile";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Employee_attachment_profile model, string command)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Employee_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Documents = dbcontext.Documents.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                //   ViewBag.idemp = model.Employee_ProfileId;
                var EmpObj = dbcontext.Employee_Profile.FirstOrDefault(a => a.ID == model.Employee_Profile.ID);

                var record = dbcontext.Employee_attachment_profile.FirstOrDefault(m => m.ID == model.ID);

                record.Code = model.Code;
                record.Is_copy = model.Is_copy;
                record.Is_required = model.Is_required;
                record.DocumentGroup = model.DocumentGroup;
                record.Related_to = model.Related_to;
                record.Issued_place = model.Issued_place;
                record.Reference_number = model.Reference_number;
                record.Document_number = model.Document_number;
                record.Document_description = model.Document_description;
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == 0 ? model.Employee_ProfileId = EmpObj.ID : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                record.Employee_Profile = EmpObj;
                record.DocumentsId = model.DocumentsId;
                record.Documents = dbcontext.Documents.FirstOrDefault(m => m.ID == model.DocumentsId);
                record.Issue_date = model.Issue_date;
                record.Expiry_date = model.Expiry_date;
                if (model.Issue_date > model.Expiry_date)
                {
                    TempData["Message"] = "Issue date  bigger Expiry date ";
                    return View(model);
                }

                record.Document_status = model.Document_status;
                record.Comments = model.Comments;

                //var fileId = 1;
                //var File = fileId + 1;

                //string folderpath = Server.MapPath("~/files/") /*(@"c:\users\3lamya\documents\visual studio 2015\projects\systemuserfakahany\systemuserfakahany\files\")*/;
                //Directory.CreateDirectory(folderpath + File);
                //string mypath = folderpath + File;
                //string filename = Guid.NewGuid() + Path.GetExtension(MyItem.FileName);
                //MyItem.SaveAs(mypath + "/" + filename);
                //model.Attachmentfile = filename;
                //record.Attachmentfile = model.Attachmentfile;

                dbcontext.SaveChanges();

                if (command == "Submit")
                {
                    return RedirectToAction("edit", "Employee_Profile", new { id = EmpObj.ID });
                }
                return RedirectToAction("index", new { id = EmpObj.ID });
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "This code Is already exists";
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            try
            {

                var record = dbcontext.Employee_attachment_profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Employee_Profile.ID.ToString();
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Employee Attachment Profile";
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

            var record = dbcontext.Employee_attachment_profile.FirstOrDefault(m => m.ID == id);
            ViewBag.idemp = record.Employee_Profile.ID.ToString();
            try
            {
                dbcontext.Employee_attachment_profile.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index", new { id = record.Employee_ProfileId });
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "You can't delete beacause it have child";
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}