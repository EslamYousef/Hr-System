using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HR.Models.Langmanger;
using System.Web.Mvc;
using HR.Controllers;

namespace HR.Models
{
    public class MyController : BaseController
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string lang = null;
            HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "")
                {
                    lang = userLang;
                }
                else
                {
                    lang = Langmanger.GetDefaultLanguage();
                }
            }
            new Langmanger().SetLanguage(lang);
            return base.BeginExecuteCore(callback, state);
        }
    }
}