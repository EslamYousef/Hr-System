using HR.Migrations;
using HR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        // GET: Test
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Create()
        {
            ViewBag.counter = 0;
            return View();
        }

        [HttpPost]
        public ActionResult Create(HR.Models.Test model, FormCollection form, HttpPostedFileBase[] file)
        {
            if (ModelState.IsValid)
            {

                var CODE = form["CODE11"].Split(char.Parse(","));
                var questionS = form["question11"].Split(char.Parse(","));
                var answerS = form["answer11"].Split(char.Parse(","));
                ///////////////////////////////////////////////////
                ///////////////////////////////////////////////////
                List<Questions> Questions = new List<Questions>();
                List<int> QuestionsID = new List<int>();
                var Saved_File_id=0;
                for (var i = 0; i < CODE.Length; i++)
                {
                    Saved_File_id = 0;
                    if (CODE[i] != "")
                    {
                        if (file[i] != null)
                        {
                            byte[] uploadedFile = new byte[file[i].InputStream.Length];

                            Files myfile = new Files();

                            file[i].InputStream.Read(uploadedFile, 0, uploadedFile.Length);
                            myfile.File = uploadedFile;
                            //myfile.testid = test.ID;
                            myfile.filename = Path.GetFileName(file[i].FileName);
                            var Saved = dbcontext.Files.Add(myfile);
                            dbcontext.SaveChanges();
                            Saved_File_id = Saved.id;
                          
                        }
                        var ques = new Questions();
                        ques.Code = CODE[i];
                        ques.Question = questionS[i];
                        ques.Standart_Question = answerS[i];
                        if(Saved_File_id!=0)
                        {
                            ques.Filesid = Saved_File_id.ToString();
                            ques.Files = dbcontext.Files.FirstOrDefault(m => m.id == Saved_File_id);
                        }
                        var q = dbcontext.Questions.Add(ques);
                        dbcontext.SaveChanges();

                     

                        Questions.Add(ques);
                        QuestionsID.Add(q.ID);


                    }
                }
                model.Questions = Questions;
                model.QuestionsID = QuestionsID;
                var test = dbcontext.Test.Add(model);
                dbcontext.SaveChanges();
                ///////////////////////////////////////////////////
                ///////////////////////////////////////////////////
               
                ///////////////////////////////////////////////////
                ///////////////////////////////////////////////////
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }

        //public ActionResult Edit(string id)
        //{
        //    try
        //    {
        //        var ID = int.Parse(id);
        //        var test = dbcontext.Test.FirstOrDefault(m => m.ID == ID);
              
        //        foreach (var item in test.Questions)
        //        {
        //            HttpPostedFileBase[] files = new HttpPostedFileBase[]();

        //            files.InputStream.wr(item.Files.File, 0, item.Files.File.Length));
        //        }

               
        //        return View(test);
        //    }catch(Exception e)
        //    {
        //        return View();
        //    }

        //}
        //[HttpPost]
        //public ActionResult Edit(Models.Test mytest, FormCollection form, HttpPostedFileBase[] file)
        //{
        //    try
        //    {
                
        //        var test = dbcontext.Test.FirstOrDefault(m => m.ID == mytest.ID);
        //        var Question = test.Questions;
        //       // dbcontext.Questions.RemoveRange(ques);
        //         /////////////////////////
        //        /////////////////////////
        //        var CODE = form["CODE11"].Split(char.Parse(","));
        //        var questionS = form["question11"].Split(char.Parse(","));
        //        var answerS = form["answer11"].Split(char.Parse(","));
        //        ///////////////////////////////////////////////////
        //        ///////////////////////////////////////////////////
        //        List<Questions> Questions = new List<Questions>();
        //        List<int> QuestionsID = new List<int>();
        //        var Saved_File_id = 0;
        //        for (var i = 0; i < CODE.Length; i++)
        //        {
        //            if (CODE[i] != "")
        //            {

        //                if (file[i] != null)
        //                {
        //                    byte[] uploadedFile = new byte[file[i].InputStream.Length];
        //                    Files myfile = new Files();
        //                    file[i].InputStream.Read(uploadedFile, 0, uploadedFile.Length);
        //                    myfile.File = uploadedFile;
        //                    myfile.filename = Path.GetFileName(file[i].FileName);
        //                    Saved_File_id = dbcontext.Files.Add(myfile).id;
        //                    dbcontext.SaveChanges();
        //                }
        //                var ques = new Questions();
        //                ques.Code = CODE[i];
        //                ques.Question = questionS[i];
        //                ques.Standart_Question = answerS[i];
        //                if (Saved_File_id != 0)
        //                {
        //                    ques.Filesid = Saved_File_id.ToString();
        //                    ques.Files = dbcontext.Files.FirstOrDefault(m => m.id == Saved_File_id);
        //                }
        //                var q = dbcontext.Questions.Add(ques);
        //                dbcontext.SaveChanges();
        //                Questions.Add(ques);
        //                QuestionsID.Add(q.ID);
        //            }
        //        }
            
        //        test.Code = mytest.Code;
        //        test.Description = mytest.Description;
        //        test.Full_mark = mytest.Full_mark;
        //        test.Pass_mark = mytest.Pass_mark;
        //        test.Test_type = mytest.Test_type;
        //        test.Questions = Questions;
        //        test.QuestionsID = QuestionsID;
        //        dbcontext.SaveChanges();
        //        dbcontext.Questions.RemoveRange(Question);
        //        dbcontext.SaveChanges();
        //        foreach (var item in Question)
        //        {
        //            dbcontext.Files.Remove(item.Files);
        //            dbcontext.SaveChanges();
        //        }
        //        ///////////////////////////////////////////////////
        //        ///////////////////////////////////////////////////
        //        return RedirectToAction("index");

        //    }
        //    catch(Exception e)
        //    {
        //        return View(mytest);
        //    }
        //}
        public FileContentResult FileDownload(int id)
        {
            byte[] fileData;
            var record = dbcontext.Files.FirstOrDefault(m => m.id == id);
            fileData = (byte[])record.File.ToArray();
            return File(fileData, "text", record.filename);
        }
    }
}