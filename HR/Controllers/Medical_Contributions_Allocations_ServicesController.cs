using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using HR.Models;
using HR.Models.Infra;

namespace HR.Controllers
{
    [Authorize]
    public class Medical_Contributions_Allocations_ServicesController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Medical_Contributions_Allocations_Services
        public ActionResult Index()
        {
            var model = dbcontext.Medical_Contributions_Allocations_Services.ToList();
            return View(model);
        }
        public ActionResult Create(string id)
        {
            ViewBag.ContributionAllocations = dbcontext.Medical_Contributions_Allocations_Entry.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
            ViewBag.Classifiaction = dbcontext.Medical_Service_Classification.ToList().Select(m => new { Code = m.Classification_Code + "-----[" + m.Description + ']', ID = m.ID });
            ViewBag.Services = dbcontext.Medical_Service.ToList().Select(m => new { Code = +m.Service_Code + "-----[" + m.Name + ']', ID = m.ID });

            //if (id != null)
            //{
            //    var ID = int.Parse(id);
            //    var ContributionAllocations_Entry = dbcontext.Medical_Contributions_Allocations_Entry.FirstOrDefault(m => m.ID == ID);

            //    var model = new Medical_Contributions_Allocations_Services { Medical_Contributions_Allocations_EntryId = ContributionAllocations_Entry.ID.ToString()};
            //    return View(model);
            //}
            //var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Medical);
            //var model = dbcontext.Medical_Contributions_Allocations_Services.ToList();
            //var count = 0;
            //if (model.Count() == 0)
            //{
            //    count = 1;
            //}
            //else
            //{
            //    var te = model.LastOrDefault().ID;
            //    count = te + 1;
            //}

            //var model_ = new Medical_Contributions_Allocations_Services { Disease_Code = stru.Structure_Code + count };
            //return View(model_);
            return View();
        }
        [HttpPost]
        public ActionResult Create(Medical_Contributions_Allocations_Services model)
        {
            try
            {
                ViewBag.ContributionAllocations = dbcontext.Medical_Contributions_Allocations_Entry.ToList().Select(m => new { Code = m.Code + "-----[" + m.Name + ']', ID = m.ID });
                ViewBag.Classifiaction = dbcontext.Medical_Service_Classification.ToList().Select(m => new { Code = m.Classification_Code + "-----[" + m.Description + ']', ID = m.ID });
                ViewBag.Services = dbcontext.Medical_Service.ToList().Select(m => new { Code = +m.Service_Code + "-----[" + m.Name + ']', ID = m.ID });
                if (ModelState.IsValid)
                {
                    Medical_Contributions_Allocations_Services record = new Medical_Contributions_Allocations_Services();
                    record.Service_CodeId = model.Service_CodeId;
                    record.Classification_CodeId = model.Classification_CodeId;
                    record.Medical_Contributions_Allocations_EntryId = model.Medical_Contributions_Allocations_EntryId;

                    var ID = int.Parse(model.Medical_Contributions_Allocations_EntryId);
                    record.Medical_Contributions_Allocations_Entry = dbcontext.Medical_Contributions_Allocations_Entry.FirstOrDefault(m => m.ID == ID);

                    var ID2 = int.Parse(model.Service_CodeId);
                    record.Medical_Service = dbcontext.Medical_Service.FirstOrDefault(m => m.ID == ID2);

                    var ID3 = int.Parse(model.Classification_CodeId);
                    record.Medical_Service_Classification = dbcontext.Medical_Service_Classification.FirstOrDefault(m => m.ID == ID3);
                    var Contributions_Allocations = dbcontext.Medical_Contributions_Allocations_Services.Add(record);
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
    }
}