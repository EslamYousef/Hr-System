using HR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models.Infra;
using HR.Models.Application;

namespace HR.Controllers
{
    [Authorize]
    public class Application_StatusController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Application_Status
        public ActionResult Index()
        {
            return View();
        }
    }
}