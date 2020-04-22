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
    public class Medical_Test_ProfileController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Medical_Test_Profile
        public ActionResult Index()
        {
            return View();
        }
    }
}