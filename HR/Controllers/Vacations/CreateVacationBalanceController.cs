using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models.Infra;
using HR.Models.ViewModel;
using HR.Models;
using System.Data.Entity.Infrastructure;
using HR.Models.Vacations;

namespace HR.Controllers.Vacations
{
    [Authorize]
    public class CreateVacationBalanceController : BaseController
    {
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: CreateVacationBalance
        public ActionResult Create()
        {
            var Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();
         
            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel);
            var model = dbcontext.LeavesBalance.ToList();
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
            var modell = new LeavesBalance { Serial_LB = stru.Structure_Code + count };
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(LeavesBalance model)
        {
            try
            {
                var Vacations_Setup = dbcontext.Vacations_Setup.ToList().Select(m => new { Code = "" + m.LeaveTypeCode + "-----[" + m.LeaveTypeNameEnglish + ']', ID = m.ID }).ToList();
                if (model.VacCode == 0)
                {
                    ModelState.AddModelError("", "Vacations Setup Code must enter");
                    return View(model);
                }
                if (ModelState.IsValid)
                {
                    LeavesBalance record = new LeavesBalance();
                    record.Serial_LB = model.Serial_LB;
                    if (model.EmployeeID == null)
                    {
                        model.EmployeeID = 0;
                        record.EmployeeID = model.EmployeeID;
                    }
                    else
                    {

                    }
                    record.Serial_LB = model.Serial_LB;
                    record.VacCode = model.VacCode;
                    record.Serial_LB = model.Serial_LB;



                    dbcontext.LeavesBalance.Add(record);
                    dbcontext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException e)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }

        }
    }
}