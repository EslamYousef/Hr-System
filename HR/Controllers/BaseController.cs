using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;
namespace HR.Controllers
{
    [HandleError]
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["lang"] != null)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["lang"].ToString());
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            }
        }
    }
}