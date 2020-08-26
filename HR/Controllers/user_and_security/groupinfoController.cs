using HR.Models;
using HR.Models.Infra;
using HR.Models.user;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.user_and_security
{
    [Authorize(Roles = "Admin")]
    public class groupinfoController : BaseController
    {
        // GET: groupinfo
        ApplicationDbContext dbcontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = dbcontext.User_Group_Info.ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            //////
            var modell = new User_Group_Info();

            var stru = dbcontext.StructureModels.FirstOrDefault(m => m.All_Models == ChModels.Personnel).Structure_Code;
            var model = dbcontext.User_Group_Info.ToList();
            if (model.Count() == 0)
            {
                modell.User_Group_Code = stru + "1";
            }
            else
            {
                modell.User_Group_Code = stru + (model.LastOrDefault().ID + 1).ToString();
            }
            /////
            return View(modell);
        }
        [HttpPost]
        public ActionResult Create(User_Group_Info model,string comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Created_By = User.Identity.Name;
                    model.Created_Date = DateTime.Now.Date;
                  var group=dbcontext.User_Group_Info.Add(model);
                    dbcontext.SaveChanges();
                    if(comment=="per")
                    {
                        return RedirectToAction("create", "permission", new { group_code = group.User_Group_Code });

                    }
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
        public ActionResult Edit(int id)
        {
            try
            {
                var record = dbcontext.User_Group_Info.FirstOrDefault(m => m.ID == id);
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
        public ActionResult Edit(User_Group_Info model,string comment)
        {
            try
            {
                var record = dbcontext.User_Group_Info.FirstOrDefault(m => m.ID == model.ID);
                record.User_Group_Desc = model.User_Group_Desc;
                record.User_Group_AltDesc = model.User_Group_AltDesc;
                record.Modified_By = User.Identity.Name;
                record.Modified_Date = DateTime.Now.Date;
                dbcontext.SaveChanges();
                if (comment == "per")
                {
                    return RedirectToAction("create", "permission", new { group_code= record.User_Group_Code });
                }
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
                var record = dbcontext.User_Group_Info.FirstOrDefault(m => m.ID == id);
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
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbcontext));
            var record = dbcontext.User_Group_Info.FirstOrDefault(m => m.ID == id);
            //====delete old permission===========
            var old_per = dbcontext.group_with_permission.Where(m => m.User_Group_InfoID == record.ID).ToList();
            var All_user_assign_on_this_group = dbcontext.User_LinkTo_UserGroup.Where(m => m.User_Group_Code == record.User_Group_Code).ToList();

            foreach (var role in old_per)
            {
                foreach (var item in All_user_assign_on_this_group)
                {
                    userManager.RemoveFromRole(item.User_ID, role.Rolle_name);
                }
            }
            dbcontext.group_with_permission.RemoveRange(old_per);
            dbcontext.SaveChanges();
            //=======================================================
            try
            {
                var link_group_with_users = dbcontext.User_LinkTo_UserGroup.Where(m => m.User_Group_Code == record.User_Group_Code).ToList();
                dbcontext.User_LinkTo_UserGroup.RemoveRange(link_group_with_users);
                dbcontext.User_Group_Info.Remove(record);
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
                return View(record);
            }
        }
        //====================================================================================
        public ActionResult link(string userid)
        {

            try
            {
                var Link = dbcontext.User_LinkTo_UserGroup.Where(m => m.User_ID == userid).ToList();
                var group = dbcontext.User_Group_Info.ToList();
                var listt = new List<SelectListItem>();
                foreach (var team in group)
                {

                    if (Link.Any(ma => ma.User_Group_Code == team.User_Group_Code && ma.User_ID == userid))
                    {
                        listt.Add(new SelectListItem
                        {
                            Text = team.User_Group_Code + "---" + team.User_Group_Desc,
                            Value = team.ID.ToString(),
                            Selected = true
                        });
                    }
                    else
                    {
                        listt.Add(new SelectListItem
                        {
                            Text = team.User_Group_Code + "---" + team.User_Group_Desc,                  
                            Value = team.ID.ToString(),
                            Selected = false
                        });
                    }

                }
                var model = new link_ViewModel { AvailableFruits = listt, userID = userid };
                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToAction("index");

            }
        }
        [HttpPost]
        public ActionResult link(link_ViewModel model, FormCollection form)
        {
            var Link = dbcontext.User_LinkTo_UserGroup.Where(m => m.User_ID == model.userID).ToList();
            var group = dbcontext.User_Group_Info.ToList();
            var listt = new List<SelectListItem>();
            foreach (var team in group)
            {

                if (Link.Any(ma => ma.User_Group_Code == team.User_Group_Code && ma.User_ID == model.userID))
                {
                    listt.Add(new SelectListItem    
                    {
                        Text = team.User_Group_Code + "---" + team.User_Group_Desc,
                        Value = team.ID.ToString(),
                        Selected = true
                    });
                }
                else
                {
                    listt.Add(new SelectListItem
                    {
                        Text = team.User_Group_Code + "---" + team.User_Group_Desc,
                        Value = team.ID.ToString(),
                        Selected = false
                    });
                }

            }
            var model2 = new link_ViewModel { AvailableFruits = listt, userID = model.userID };
            try
            {
                var user = dbcontext.Users.FirstOrDefault(m => m.Id == model.userID);
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbcontext));

                if (true)
                {
                    var Link2 = dbcontext.User_LinkTo_UserGroup.Where(m => m.User_ID == model.userID).ToList();
                    if (Link2.Count() > 0)
                    {
                        dbcontext.User_LinkTo_UserGroup.RemoveRange(Link2);
                        dbcontext.SaveChanges();
                    }
                    //====remove form UserRole table
                    foreach (var item in Link2)
                    {
                        var my_group = dbcontext.User_Group_Info.FirstOrDefault(m => m.User_Group_Code == item.User_Group_Code);
                        var per_link_with_this_group = dbcontext.group_with_permission.Where(m => m.User_Group_InfoID == my_group.ID).ToList();
                        foreach(var per in per_link_with_this_group)
                        {
                            userManager.RemoveFromRole(user.Id, per.Rolle_name);
                        }
                    }
                }
                var link = form["AvailableFruits"].Split(',');
                if (link.Count() > 1)
                {
                    foreach (var item in link)
                    {
                        if (item != "")
                        {
                            var ID = int.Parse(item);
                            var group2 = dbcontext.User_Group_Info.FirstOrDefault(m => m.ID == ID);
                            dbcontext.User_LinkTo_UserGroup.Add(new User_LinkTo_UserGroup { Created_By = User.Identity.Name, Created_Date = DateTime.Now.Date, User_ID = model.userID, User_Group_Code = group2.User_Group_Code });
                            dbcontext.SaveChanges();
                            ///add new permission  to UserRole table
                            var my_group = dbcontext.User_Group_Info.FirstOrDefault(m => m.User_Group_Code == group2.User_Group_Code);
                            var per_link_with_this_group = dbcontext.group_with_permission.Where(m => m.User_Group_InfoID == my_group.ID).ToList();
                            foreach (var per in per_link_with_this_group)
                            {
                                userManager.AddToRole(user.Id, per.Rolle_name);
                            }
                            ///
                        }
                    }
                }
                return RedirectToAction("edit_profile", "account", new { id = model.userID });
            }
            catch(Exception)
            {
                return View(model2);
            }
        }
    }
    public class link_ViewModel
    {
        public List<SelectListItem> AvailableFruits { get; set; }
        public string userID { get; set; }
    }
}