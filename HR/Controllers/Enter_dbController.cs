using HR.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public  class Enter_dbController : Controller
    {
        // GET: Enter_db

       
       
    }
    public class db_VM
    {
        public string server { get; set; }
        public string db { get; set; }
        public string userid { get; set; }
        public string pass { get; set; }
    }
}