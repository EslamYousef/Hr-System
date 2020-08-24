using HR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HR.Controllers
{
    [Authorize]
    [HandleError(View = "Error")]
    public class HomeController : BaseController
    {

     
        public ActionResult Index()

        {
           
            return View();
          
        }
        public ActionResult ChangeLanguage(string lang)
        {
            new Langmanger().SetLanguage(lang);
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles ="Admin")]
        public ActionResult setting()
        {
            return View();
        }

        public ActionResult GetAllBasicModule()
        {

            return View();

        }
      























    }
}