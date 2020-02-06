using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Arabic()
        {

            {
                Session["lang"] = "ar-EG";
                return Redirect(Request.UrlReferrer.ToString());
            }

        }

        public ActionResult English()
        {
            Session["lang"] = "en-US";
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}