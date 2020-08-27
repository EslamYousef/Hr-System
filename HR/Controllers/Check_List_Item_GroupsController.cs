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
    [Authorize(Roles = "Admin,personnel,personnelSetup,Check List Item Groups")]
    public class Check_List_Item_GroupsController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Check_List_Item_Groups
        public ActionResult Index()
        {
            var model = dbcontext.Check_List_Item_Groups.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.Check_List_Item_Groups.ToList();
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

            var modell = new Check_List_Item_Groups { Group_Code = stru.Structure_Code + count };
            return View(modell);

        }
        [HttpPost]
        public ActionResult Create(Check_List_Item_Groups model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Check_List_Item_Groups record = new Check_List_Item_Groups();
                    record.Group_Code = model.Group_Code;
                    record.Description_Alternative = model.Description_Alternative;
                    record.Description_Group = model.Description_Group;
                    if (model.The_Purpose == 0)
                    {
                        TempData["Message"] = HR.Resource.Personnel.PleaseChoosefromThePurpose; 
                        return View(model);
                    }
                    else
                    {
                        record.The_Purpose = model.The_Purpose;
                       dbcontext.Check_List_Item_Groups.Add(record);
                        dbcontext.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException)
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
                var record = dbcontext.Check_List_Item_Groups.FirstOrDefault(m => m.ID == id);

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
        public ActionResult Edit(Check_List_Item_Groups model)
        {
            try
            {
                var record = dbcontext.Check_List_Item_Groups.FirstOrDefault(m => m.ID == model.ID);
                record.Group_Code = model.Group_Code;
                record.Description_Alternative = model.Description_Alternative;
                record.Description_Group = model.Description_Group;
                if (model.The_Purpose == 0)
                {
                    TempData["Message"] = HR.Resource.Personnel.PleaseChoosefromThePurpose;
                    return View(model);
                }
                else
                {
                    record.The_Purpose = model.The_Purpose;
                    dbcontext.SaveChanges();

                    return RedirectToAction("Index");
                }

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
                var record = dbcontext.Check_List_Item_Groups.FirstOrDefault(m => m.ID == id);
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
            var record = dbcontext.Check_List_Item_Groups.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.Check_List_Item_Groups.Remove(record);
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