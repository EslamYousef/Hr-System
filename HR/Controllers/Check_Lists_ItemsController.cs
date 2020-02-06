using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Data.Entity.Infrastructure;
using HR.Models.Infra;
namespace HR.Controllers
{
    [Authorize]
    public class Check_Lists_ItemsController : Controller
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Check_Lists_Items
        public ActionResult Index()
        {
            var model = dbcontext.Check_Lists_Items.ToList();
            return View(model);
        } 
        public ActionResult Create()
        {

            var o = dbcontext.Check_List_Item_Groups.ToList().Select(m => new { Code = "" + m.Group_Code + "-----[" + m.Description_Group + ']', ID = m.ID }).ToList();
            ViewBag.Check_List_Item_Groups = o;
            if (o == null || o.Count() == 0)
            {

                TempData["Message"] = "Enter data first on Check List Item Groups ";
                var modelll = dbcontext.Check_Lists_Items.ToList();
                return View("index", modelll);

            }
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Check_Lists_Items.ToList();
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
            var modell = new Check_Lists_Items { Check_Code = stru.Structure_Code + count };
            return View(modell);

        }
        [HttpPost]
        public ActionResult Create(Check_Lists_Items model, string command)
        {
            try
            {
                ViewBag.Check_List_Item_Groups = dbcontext.Check_List_Item_Groups.ToList().Select(m => new { Code = "" + m.Group_Code + "-----[" + m.Description_Group + ']', ID = m.ID }).ToList();

                if (ModelState.IsValid)
                {
                    if (model.Check_List_Item_GroupsId == "0" || model.Check_List_Item_GroupsId == null)
                    {
                        ModelState.AddModelError("", "Check List Item Groups Code must enter");
                        return View(model);
                    }
                    Check_Lists_Items record = new Check_Lists_Items();
                    record.Check_Code = model.Check_Code;
                    record.Description = model.Description;
                    record.Description_Alternative = model.Description_Alternative;
                    record.Is_Mandatory = model.Is_Mandatory;
                    record.Check_List_Item_GroupsId = model.Check_List_Item_GroupsId;
                    var Question_GroupId = int.Parse(model.Check_List_Item_GroupsId);
                    
                    var  group= dbcontext.Check_List_Item_Groups.FirstOrDefault(m => m.ID == Question_GroupId);
                    record.Check_List_Item_Groups = group;
                    var item= dbcontext.Check_Lists_Items.Add(record);
                    dbcontext.SaveChanges();
                    /////
                    //group.check_items.Add(item);
                    //dbcontext.SaveChanges();

                    /////add new item to eos
                    var ESO = dbcontext.EOS_Request.Where(m => m.Check_List_Item_Groups.ID == Question_GroupId);
                    foreach (var item1 in ESO)
                    {
                        var interview = new check_list_EOS();
                        interview.item = item;
                        interview.interpolation = false;
                        interview.EOS = item1;
                        var inter = dbcontext.check_list_EOS.Add(interview);
                    }
                    dbcontext.SaveChanges();
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



        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.Check_List_Item_Groups = dbcontext.Check_List_Item_Groups.ToList().Select(m => new { Code = "" + m.Group_Code + "-----[" + m.Description_Group + ']', ID = m.ID }).ToList();
                var record = dbcontext.Check_Lists_Items.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Check List Item Groups";
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(Check_Lists_Items model)
        {
            try
            {
                ViewBag.Check_List_Item_Groups = dbcontext.Check_List_Item_Groups.ToList().Select(m => new { Code = "" + m.Group_Code + "-----[" + m.Description_Group + ']', ID = m.ID }).ToList();
                if (model.Check_Code == "0" || model.Check_Code == null)
                {
                    ModelState.AddModelError("", "Check List Item Groups Code must enter");
                    return View(model);
                }
                var record = dbcontext.Check_Lists_Items.FirstOrDefault(m => m.ID == model.ID);
                var ID_old_group = int.Parse(record.Check_List_Item_GroupsId);
                /////remove from old group//////
                //var old_group = dbcontext.Check_List_Item_Groups.FirstOrDefault(m => m.ID == ID_old_group);
                //old_group.check_items.Remove(record);
                //dbcontext.SaveChanges();


                record.Check_Code = model.Check_Code;
                record.Description = model.Description;
                record.Description_Alternative = model.Description_Alternative;
                record.Is_Mandatory = model.Is_Mandatory;
                record.Check_List_Item_GroupsId = model.Check_List_Item_GroupsId;
                var Question_GroupId = int.Parse(model.Check_List_Item_GroupsId);
                var new_group= dbcontext.Check_List_Item_Groups.FirstOrDefault(m => m.ID == Question_GroupId);
                record.Check_List_Item_Groups = new_group;
                dbcontext.SaveChanges();

                /////add to new group//////
                //new_group.check_items.Add(record);
                //dbcontext.SaveChanges();
                return RedirectToAction("index");
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
                var record = dbcontext.Check_Lists_Items.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = "There is no Check Lists Items ";
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
            var record = dbcontext.Check_Lists_Items.FirstOrDefault(m => m.ID == id);

            try
            {
                ////remove question from EOS interview////
                var answerEOS = dbcontext.check_list_EOS.Where(m => m.item.ID == record.ID);
                dbcontext.check_list_EOS.RemoveRange(answerEOS);
                dbcontext.SaveChanges();
                /////
                dbcontext.Check_Lists_Items.Remove(record);
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