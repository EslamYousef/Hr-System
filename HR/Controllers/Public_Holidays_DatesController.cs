using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using HR.Reposatory.Evalutions.reposatory;
using HR.Reposatory.Evalutions.IReposatory;
using HR.Models.Infra;

namespace HR.Controllers
{
    public class Public_Holidays_DatesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private readonly IStructure reposatorystructure;
        private readonly IPublic_Holidays_Dates reposatoryPublicHolidaysDates;
        // GET: Public_Holidays_Dates
        public Public_Holidays_DatesController()
        {
            reposatoryPublicHolidaysDates = new Public_Holidays_DatesCs(new ApplicationDbContext());
            reposatorystructure = new Structure(new ApplicationDbContext());
        }
        public ActionResult Index()
        {
            try
            {
                var model = reposatoryPublicHolidaysDates.GetAll();
                if (model !=null)
                {
                    return View();
                }
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild; return View();
                }
            }
            catch (Exception)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return View();
            }
        }
        public ActionResult Create()
        {
            ViewBag.Public_Holiday_Events = db.Public_Holiday_Events.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });
            try
            {
                var stru = reposatorystructure.find(ChModels.Personnel).Structure_Code;
                var list = reposatoryPublicHolidaysDates.GetAll();
                var model = new Public_Holidays_Dates();
                if (list.Count()==0)
                {
                    model.Holidays_Code = stru + "1";
                }
                else
                {
                    model.Holidays_Code = (list.LastOrDefault().ID + 1).ToString();
                }
                return View(model);
            }
            catch (Exception e)
            {
                TempData["Message"] = HR.Resource.pers_2.Faild;
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Create(FormCollection form, Public_Holidays_Dates model)
        {
            ViewBag.Public_Holiday_Events = db.Public_Holiday_Events.ToList().Select(m => new { Code = "" + m.Code + "-----[" + m.Description + ']', ID = m.ID });
            try
            {
                if (ModelState.IsValid)
                {
                    var flag = reposatoryPublicHolidaysDates.AddOne(model);

                    if (flag)
                    {
                        TempData["Message"] = HR.Resource.pers_2.addedSuccessfully;
                        return RedirectToAction("index");
                    }
                    else
                    {
                        TempData["Message"] = HR.Resource.pers_2.Faild;
                        return View(model);
                    }
                }
                else
                {
                    TempData["Message"] = HR.Resource.pers_2.Faild;
                    return View(model);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }





        }
}