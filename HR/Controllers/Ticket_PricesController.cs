using HR.Models;
using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    [Authorize]
    public class Ticket_PricesController : BaseController
    {
        // GET: Ticket_Prices
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        // GET: Country
        public ActionResult Index()
        {
            var model = dbcontext.TicketPrice.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.airport = dbcontext.Air_ports.ToList();


            return View();
        }
        [HttpPost]
        public ActionResult Create(TicketPrice model,FormCollection form)
        {
            try
            {
                ViewBag.airport = dbcontext.Air_ports.ToList();
                if (ModelState.IsValid)
                {   
                  
                    var fromair = form["from"].Split(char.Parse(","));
                    var to = form["to"].Split(char.Parse(","));
                    var codefromair = form["codefrom"].Split(char.Parse(","));
                    var codeto = form["codeto"].Split(char.Parse(","));
                    var fromdate = form["fromdate"].Split(char.Parse(","));
                    var todate = form["todate"].Split(char.Parse(","));
                    var price = form["price"].Split(char.Parse(","));
                    var class_type = form["class_type"].Split(char.Parse(","));

                    for (var i=0;i<fromair.Length;i++)
                    {
                        TicketPrice record = new TicketPrice();
                        if (class_type[i] == "1")
                        {
                            record.classtype = ClassType.Economy;
                        }
                       else  if (class_type[i] == "2")
                        {
                            record.classtype = ClassType.Premium_economy;
                        }
                       else if (class_type[i] == "3")
                        {
                            record.classtype = ClassType.Business;
                        }
                       else if (class_type[i] == "4")
                        {
                            record.classtype = ClassType.First;
                        }
                        var fromair1 = int.Parse(codefromair[i]);
                        var toair = int.Parse(codeto[i]);
                        var fromdate1 = Convert.ToDateTime(fromdate[i]);
                        var todate1 = Convert.ToDateTime(todate[i]);
                        var pri = double.Parse(price[i]);
                        record.From_air_port_Idd = fromair1;
                        record.To_air_port_Idd = toair;
                        record.From_Date = fromdate1;
                        record.TO_Date = todate1;
                        record.Price =pri;
                        record.From_Air_port = dbcontext.Air_ports.FirstOrDefault(m => m.ID == record.From_air_port_Idd);
                        record.To_Air_port = dbcontext.Air_ports.FirstOrDefault(m => m.ID == record.To_air_port_Idd);
                        dbcontext.TicketPrice.Add(record);
                        dbcontext.SaveChanges();
                    }
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            {
                return View(model);
            }

        }
        public ActionResult Edit(int id)
        {
            try
            {
                var record = dbcontext.TicketPrice.FirstOrDefault(m => m.ID == id);
                ViewBag.airport = dbcontext.Air_ports.ToList();
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = HR.Resource.Basic.thereisnodata;
                    return View();
                }
            }
            catch
            (Exception e)
            { return View(); }
        }
        [HttpPost]
        public ActionResult Edit(TicketPrice model)
        {
            try
            {
                ViewBag.airport = dbcontext.Air_ports.ToList();
                if (DateTime.Compare(model.From_Date, model.TO_Date) > 0)
                {
                    TempData["Message"] = HR.Resource.Basic.FromDateLaterthanTodate;
                    return View(model);
                }
                else if (model.From_air_port_Idd <1)
                {
                    TempData["Message"] = HR.Resource.Basic.youmustenterFromAir;
                    return View(model);
                }

                else if (model.To_air_port_Idd <1 )
                {
                    TempData["Message"] = HR.Resource.Basic.youmustentertoAir1;
                    return View(model);
                }
                var record = dbcontext.TicketPrice.FirstOrDefault(m => m.ID == model.ID);
                record.From_air_port_Idd = model.From_air_port_Idd;
                record.To_air_port_Idd = model.To_air_port_Idd;
                record.From_Date = model.From_Date;
                record.TO_Date = model.TO_Date;
                record.Price = model.Price;
                record.classtype = model.classtype;
                record.From_Air_port = dbcontext.Air_ports.FirstOrDefault(m => m.ID == record.From_air_port_Idd);
                record.To_Air_port = dbcontext.Air_ports.FirstOrDefault(m => m.ID == record.To_air_port_Idd);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.thiscodeIsalreadyexists;
                return View(model);
            }
            catch (Exception e)
            { return View(model); }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var record = dbcontext.TicketPrice.FirstOrDefault(m => m.ID == id);
                if (record != null)
                { return View(record); }
                else
                {
                    TempData["Message"] = HR.Resource.Basic.thereisnodata;
                    return View();
                }

            }
            catch (Exception e)
            {
                return View();
            }

        }
        [ActionName("Delete")]
        [HttpPost]
        public ActionResult Deletemethod(int id)
        {
            var record = dbcontext.TicketPrice.FirstOrDefault(m => m.ID == id);
            try
            {
                dbcontext.TicketPrice.Remove(record);
                dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            catch (DbUpdateException)
            {
                TempData["Message"] = HR.Resource.Basic.youcannotdeletethisRow;
                return View(record);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}