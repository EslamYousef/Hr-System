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
    public class TestController : BaseController
    {
        // GET: Test
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.Test.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.counter = 0;
            ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });

            var modell = new Test();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Recuirtment).Structure_Code;
            var model = dbcontext.Test.ToList();
            if (model.Count() == 0)
            {
                modell.Code = stru + "1";
            }
            else
            {
                modell.Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            return View(modell);

        }

        [HttpPost]
        public ActionResult Create(HR.Models.Test model, FormCollection form, IEnumerable<HttpPostedFileBase> MyItem)
        {
            if (ModelState.IsValid)
            {
                Test record = new Test();

                var CODE = form["CODE11"].Split(char.Parse(","));
                var questionS = form["question11"].Split(char.Parse(","));
                var answerS = form["answer11"].Split(char.Parse(","));

                List<Questions> Questions = new List<Questions>();
                List<int> QuestionsID = new List<int>();

                var test = dbcontext.Test.Add(model);
                dbcontext.SaveChanges();
                var ques = new Questions();
                var s = MyItem.ToList();
                for (int w = 0; w < s.Count; w++)
                {
                    if (CODE[w] != "" && questionS[w] != "" && answerS[w] != "")
                    {
                        if (s[w] == null)
                        {
                            ques.Attachmentfile = null;
                        }
                        else
                        {
                            string filename = Guid.NewGuid() + Path.GetExtension(s[w].FileName);
                            s[w].SaveAs(Path.Combine(Server.MapPath("/Testfiles"), filename));
                            ques.Attachmentfile = filename;
                        }
                        ques.Code = CODE[w];
                        ques.Question = questionS[w];
                        ques.Standart_Question = answerS[w];
                        ques.TestId = model.ID;
                        var q = dbcontext.Questions.Add(ques);
                        dbcontext.SaveChanges();
                        Questions.Add(ques);
                        QuestionsID.Add(q.ID);
                        dbcontext.SaveChanges();
                        model.Questions = Questions;
                        model.QuestionsID = QuestionsID;
                        dbcontext.SaveChanges();
                    }
                }

                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Edit(string id)
        {
            try
            {
                ViewBag.counter = 0;
                ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                var ID = int.Parse(id);
                var test = dbcontext.Test.FirstOrDefault(m => m.ID == ID);

                return View(test);
            }
            catch (Exception e)
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult Edit(Models.Test tests, FormCollection form, IEnumerable<HttpPostedFileBase> MyItem)
        {
            try
            {
                ViewBag.job_desc = dbcontext.job_title_cards.ToList().Select(m => new { Code = m.Code + "------[" + m.name + ']', ID = m.ID });
                var record = dbcontext.Test.FirstOrDefault(m => m.ID == tests.ID);

                var test = dbcontext.Questions.Where(m => m.TestId == tests.ID);
                dbcontext.Questions.RemoveRange(test);
                dbcontext.SaveChanges();
                /////////////////////////
                var CODE = form["CODE11"].Split(char.Parse(","));
                var questionS = form["question11"].Split(char.Parse(","));
                var answerS = form["answer11"].Split(char.Parse(","));

                List<Questions> Questions = new List<Questions>();
                List<int> QuestionsID = new List<int>();

                var ques = new Questions();
                var s = MyItem.ToList();
                for (int w = 0; w < s.Count; w++)
                {
                    if (CODE[w] != "" && questionS[w] != "" && answerS[w] != "")
                    {
                        if (s[w] != null)
                        {
                            string filename = Guid.NewGuid() + Path.GetExtension(s[w].FileName);
                            s[w].SaveAs(Path.Combine(Server.MapPath("/Testfiles"), filename));
                            ques.Attachmentfile = filename;
                        }

                        ques.Code = CODE[w];
                        ques.Question = questionS[w];
                        ques.Standart_Question = answerS[w];
                        ques.TestId = tests.ID;
                        var q = dbcontext.Questions.Add(ques);
                        dbcontext.SaveChanges();
                        Questions.Add(ques);
                        QuestionsID.Add(q.ID);
                        dbcontext.SaveChanges();
                        tests.Questions = Questions;
                        tests.QuestionsID = QuestionsID;
                        dbcontext.SaveChanges();
                    }
                }

                record.Code = tests.Code;
                record.Description = tests.Description;
                record.Full_mark = tests.Full_mark;
                record.Pass_mark = tests.Pass_mark;
                record.Test_type = tests.Test_type;
                record.Name = tests.Name;
                record.job_descId = tests.job_descId;
                //record.Questions = Questions;
                //record.QuestionsID = QuestionsID;
                dbcontext.SaveChanges();

                return RedirectToAction("index");
            }
            catch (Exception e)
            {
                return View(tests);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {

                var record = dbcontext.Test.FirstOrDefault(m => m.ID == id);
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

            var record = dbcontext.Test.FirstOrDefault(m => m.ID == id);
            var Questions = dbcontext.Questions.Where(m => m.TestId == id);

            try
            {
                dbcontext.Questions.RemoveRange(Questions);
                dbcontext.SaveChanges();
                dbcontext.Test.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
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