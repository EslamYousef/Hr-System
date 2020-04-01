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
    public class Applicant_Attachment_ProfileController : BaseController
    {
        // GET: Applicant_Attachment_Profile
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index(string id)
        {
            var ID = int.Parse(id);
            var new_model = dbcontext.Applicant_Attachment_Profile.Where(m => m.Applicant_Profile.ID == ID).ToList();
            ViewBag.idemp = id;
            return View(new_model);
        }
        public ActionResult Create(string id)
        {


            ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.Documents = dbcontext.Documents.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
            ViewBag.idemp = id;
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Recuirtment);
            var model = dbcontext.Applicant_Attachment_Profile.ToList();
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
            var emp = dbcontext.Applicant_Profile.FirstOrDefault(m => m.ID == ID);
            var Applicant_Attachment = new Applicant_Attachment_Profile { Applicant_Profile = emp, Employee_ProfileId = emp.ID, Code = stru.Structure_Code + count.ToString(), Expiry_date = statis, Issue_date = statis };
            return View(Applicant_Attachment);

        }
        [HttpPost]
        public ActionResult Create(Applicant_Attachment_Profile model, string command, HttpPostedFileBase MyItem)
        {
            try
            {



                ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Documents = dbcontext.Documents.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var EmpObj = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == model.Applicant_Profile.ID);
                Applicant_Attachment_Profile record = new Applicant_Attachment_Profile();
                var empid = EmpObj.Code + "------" + EmpObj.Name;
                record.Employee_ProfileId = model.Employee_ProfileId == 0 ? model.Employee_ProfileId = EmpObj.ID : model.Employee_ProfileId;
                ViewBag.idemp = model.Employee_ProfileId;
                if (ModelState.IsValid)
                {

                    record.Code = model.Code;
                    record.Is_copy = model.Is_copy;
                    record.Is_required = model.Is_required;
                    record.DocumentGroup = model.DocumentGroup;
                    record.Related_to = model.Related_to;
                    record.Issued_place = model.Issued_place;
                    record.Reference_number = model.Reference_number;
                    record.Document_number = model.Document_number;
                    record.Document_description = model.Document_description;

                    record.Applicant_Profile = EmpObj;
                    record.DocumentsId = model.DocumentsId;
                    record.Documents = dbcontext.Documents.FirstOrDefault(m => m.ID == model.DocumentsId);
                    record.Issue_date = model.Issue_date;
                    record.Expiry_date = model.Expiry_date;
                    if (model.Issue_date > model.Expiry_date)
                    {
                        TempData["Message"] = HR.Resource.Personnel.IssuedatebiggerExpirydate;
                        return View(model);
                    }

                    record.Document_status = model.Document_status;
                    record.Comments = model.Comments;

                    var code = record.Code;
                    string folderpath = Server.MapPath("~/Applicant_Attachment_Files/") /*(@"c:\users\3lamya\documents\visual studio 2015\projects\systemuserfakahany\systemuserfakahany\files\")*/;
                    Directory.CreateDirectory(folderpath + code);
                    string mypath = folderpath + code;
                    string filename = Guid.NewGuid() + Path.GetExtension(MyItem.FileName);
                    MyItem.SaveAs(mypath + "/" + filename);
                    model.Attachmentfile = filename;
                    //record.Attachmentfile = model.Attachmentfile;

                    dbcontext.Applicant_Attachment_Profile.Add(record);
                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("edit", "Applicant_Profile", new { id = EmpObj.ID });
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
                ViewBag.Documents = dbcontext.Documents.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });

                var record = dbcontext.Applicant_Attachment_Profile.FirstOrDefault(m => m.ID == id);
                ViewBag.idemp = record.Applicant_Profile.ID.ToString();
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
        public ActionResult Edit(Applicant_Attachment_Profile model, string command, HttpPostedFileBase MyItem)
        {
            try
            {
                ViewBag.Employee_Profile = dbcontext.Applicant_Profile.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                ViewBag.Documents = dbcontext.Documents.ToList().Select(m => new { Code = m.Code + "------[" + m.Name + ']', ID = m.ID });
                //   ViewBag.idemp = model.Employee_ProfileId;
                var EmpObj = dbcontext.Applicant_Profile.FirstOrDefault(a => a.ID == model.Applicant_Profile.ID);

                var record = dbcontext.Applicant_Attachment_Profile.FirstOrDefault(m => m.ID == model.ID);

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
                record.Applicant_Profile = EmpObj;
                record.DocumentsId = model.DocumentsId;
                record.Documents = dbcontext.Documents.FirstOrDefault(m => m.ID == model.DocumentsId);
                record.Issue_date = model.Issue_date;
                record.Expiry_date = model.Expiry_date;
                if (model.Issue_date > model.Expiry_date)
                {
                    TempData["Message"] = HR.Resource.Personnel.IssuedatebiggerExpirydate;
                    return View(model);
                }

                record.Document_status = model.Document_status;
                record.Comments = model.Comments;



                if (MyItem != null)
                {
                    var code = record.Code;
                    string folderpath = Server.MapPath("~/Applicant_Attachment_Files/") /*(@"c:\users\3lamya\documents\visual studio 2015\projects\systemuserfakahany\systemuserfakahany\files\")*/;
                    Directory.CreateDirectory(folderpath + code);
                    string mypath = folderpath + code;
                    string filename = Guid.NewGuid() + Path.GetExtension(MyItem.FileName);
                    MyItem.SaveAs(mypath + "/" + filename);
                    model.Attachmentfile = filename;
                    record.Attachmentfile = model.Attachmentfile;
                }

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

                var record = dbcontext.Applicant_Attachment_Profile.FirstOrDefault(m => m.ID == id);
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

            var record = dbcontext.Applicant_Attachment_Profile.FirstOrDefault(m => m.ID == id);
            ViewBag.idemp = record.Applicant_Profile.ID.ToString();
            try
            {
                dbcontext.Applicant_Attachment_Profile.Remove(record);
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