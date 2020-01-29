using HR.Models;
using HR.Models.Infra;
using HR.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class Name_of_educational_qualificationController : Controller
    {
        // GET: Name_of_educational_qualification
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = dbcontext.Name_of_educational_qualification.ToList();
            return View(model);
        }

        public ActionResult Create(string id)
        {

            var modell = new Name_of_educational_qualification();
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Basic).Structure_Code;
            var modelll = dbcontext.Name_of_educational_qualification.ToList();
            var Code = "";
            if (modelll.Count() == 0)
            {
                Code = stru + "1";
            }
            else
            {
                Code = stru + (modelll.LastOrDefault().ID + 1).ToString();
            }


            ViewBag.titleee = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "----" + m.Name, ID = m.ID });
            if (id != null)
            {
                var ID = int.Parse(id);
                var Educate_level = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == ID);
                var model = new Name_of_educational_qualification {Code=Code, Educate_Title=Educate_level,Educate_Titleid=Educate_level.ID};
                return View(model);
            }
            var mmodel = new Name_of_educational_qualification();
            mmodel.Code = Code;
            return View(mmodel);
        }
        [HttpPost]
        public ActionResult Create(Name_of_educational_qualification model,string command)
        {
            try
            {
                ViewBag.titleee = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "----" + m.Name, ID = m.ID });

                if (ModelState.IsValid)
                {
                    Name_of_educational_qualification record = new Name_of_educational_qualification();
                    record.Name = model.Name;
                    record.Description = model.Description;
                    record.Educate_Titleid = model.Educate_Titleid;
                    record.Code = model.Code;
                    record.Educate_Title = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == model.Educate_Titleid);
                   var Name= dbcontext.Name_of_educational_qualification.Add(record);
                    dbcontext.SaveChanges();
                    if (command == "Submit")
                    {
                        return RedirectToAction("Create", "QualificationMajor", new { id = Name.ID });
                    }
                    if (command == "sub")
                    {
                        return RedirectToAction("link", "Name_of_educational_qualification", new { id = Name.ID });
                    }
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "this code Is already exists";
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }
        }

        public ActionResult link (string id,string tag)
        {
            try
            {
                if (tag == "0")
                {

                    var All_Sub = dbcontext.Sub_educational_body.Where(m => m.Name_of_educational_qualification_IDD == id).ToList();
                    var mainqulifications = dbcontext.Sub_educational_body.ToList();
                    var listt = new List<SelectListItem>();
                    foreach (var team in mainqulifications)
                    {
                        if (All_Sub.Any(ma => ma.ID == team.ID))
                        {
                            listt.Add(new SelectListItem
                            {
                                Text = team.Name + "-----------------------------------------------------------------" + team.Main_Educate_body.Name
                                                     ,
                                Value = team.ID.ToString(),
                                Selected = true
                            });
                        }
                        else
                        {
                            listt.Add(new SelectListItem
                            {
                                Text = team.Name + "-----------------------------------------------------------------" + team.Main_Educate_body.Name
                                                     ,
                                Value = team.ID.ToString(),
                                Selected = false
                            });
                        }

                    }
                    var model = new Assign_subqulifications_ViewModel { AvailableFruits = listt ,nameID=id};
                    return View(model);
                }
                else
                {
                    var model = new Assign_subqulifications_ViewModel { AvailableFruits = Getitems(),nameID=id };
                    return View(model);
                }
               
                
            }
            catch(Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult link(Assign_subqulifications_ViewModel model)
        {
            var id = int.Parse(model.nameID);
            var name = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == id);
            if(true)
            {
                var ID = int.Parse(model.nameID);
                var sub = dbcontext.Sub_educational_body.Where(m => m.Name_of_educational_qualification_IDD == ID.ToString()).ToList();
                if (sub !=null)
                {
                    foreach (var item in sub)
                    {
                        item.Name_of_educational_qualification_IDD = null;
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
                    var sub = dbcontext.Sub_educational_body.FirstOrDefault(m => m.ID == ID);
                    sub.Name_of_educational_qualification_IDD = name.ID.ToString();
                    sub.Name_of_educational_qualification = name;
                    dbcontext.SaveChanges();
                }
            }
            return RedirectToAction("index");
        }

        private IList<SelectListItem> Getitems()
        {
            var mainqulifications = dbcontext.Sub_educational_body.ToList();
            var listt = new List<SelectListItem>();
            foreach (var team in mainqulifications)
            {
                listt.Add(new SelectListItem { Text = team.Name+"-----------------------------------------------------------------"+team.Main_Educate_body.Name
                                             , Value = team.ID.ToString()});
            }
            return listt;

        }
    
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.titleee = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "----" + m.Name, ID = m.ID });
                var record = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no country";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Name_of_educational_qualification model,string command)
        {
            try
            {
                ViewBag.titleee = dbcontext.Educate_Title.ToList().Select(m => new { Code = m.Code + "----" + m.Name, ID = m.ID });
                var record = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == model.ID);
                record.Name = model.Name;
                record.Description = model.Description;
                record.Educate_Titleid = model.Educate_Titleid;
                record.Code = model.Code;
                record.Educate_Title = dbcontext.Educate_Title.FirstOrDefault(m => m.ID == model.Educate_Titleid);
                dbcontext.SaveChanges();
                if (command == "sub")
                {
                    return RedirectToAction("link", "Name_of_educational_qualification", new { id = record.ID,tag="0" });
                }
                if (command == "Submit1")
                {
                    return RedirectToAction("Create", "QualificationMajor", new { id = record.ID });
                }
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = "this code Is already exists";
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "there is no data";
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
            var record = dbcontext.Name_of_educational_qualification.FirstOrDefault(m => m.ID == id);

            try
            {
                 dbcontext.Name_of_educational_qualification.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
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