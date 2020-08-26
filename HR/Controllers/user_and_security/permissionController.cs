using HR.Models;
using HR.Models.user;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers.user_and_security
{
    [Authorize(Roles = "Admin")]
    public class permissionController : BaseController
    {
        // GET: permission
        Models.ApplicationDbContext dbcontext = new Models.ApplicationDbContext();
    
       
        public ActionResult create(string group_code)
        {
            try
            {
                var group = dbcontext.User_Group_Info.FirstOrDefault(m => m.User_Group_Code == group_code);
                var Per = new User_Permissions { User_Group_Code = group_code ,Modified_By=group.User_Group_Desc};
                ViewBag.module = dbcontext.permissions.Where(m => m.type_permission == type_permission.module).ToList().Select(m => new { Permission_Name= '[' + m.Permission_Name + ']', ID = m.ID }).ToList();
              //  ViewBag.submodule = dbcontext.permissions.Where(m => m.type_permission == type_permission.sub_module).ToList().Select(m => new { Permission_Name = '[' + m.Permission_Name + ']', ID = m.ID }).ToList();
                return View(Per);
            }
            catch(Exception)
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult create(FormCollection Form_)
        {
            ViewBag.module = dbcontext.permissions.Where(m => m.type_permission == type_permission.module).ToList().Select(m => new { Permission_Name = '[' + m.Permission_Name + ']', ID = m.ID }).ToList();
         //   ViewBag.submodule = dbcontext.permissions.Where(m => m.type_permission == type_permission.sub_module).ToList().Select(m => new { Permission_Name = '[' + m.Permission_Name + ']', ID = m.ID }).ToList();

            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbcontext));
            var group = Form_["UserGroupCode_"].Split(',')[0].ToString();
            var Group = dbcontext.User_Group_Info.FirstOrDefault(m => m.User_Group_Code == group);
            var All_user_assign_on_this_group = dbcontext.User_LinkTo_UserGroup.Where(m => m.User_Group_Code == group).ToList();
            try
            {
                //====delete old permission===========
                var old_per = dbcontext.group_with_permission.Where(m => m.User_Group_InfoID == Group.ID).ToList();
                foreach (var role in old_per)
                {
                    foreach (var item in All_user_assign_on_this_group)
                    {
                        userManager.RemoveFromRole(item.User_ID,role.Rolle_name);
                    }
                }
                dbcontext.group_with_permission.RemoveRange(old_per);
                dbcontext.SaveChanges();
                //=======================================================


                var Module = Form_["Module_"].Split(',');
                var Sub_module = Form_["subModule_"].Split(',');
                var Form = Form_["form_"].Split(',');
                if (Module.Count() > 0)
                {
                    for (var i = 0; i < Module.Count(); i++)
                    {
                        if (Module[i] != "")
                        {
                            foreach (var item in All_user_assign_on_this_group)
                            {
                                var user = dbcontext.Users.FirstOrDefault(m => m.Id == item.User_ID);
                                if (int.Parse(Module[i]) != 0&& int.Parse(Sub_module[i]) == 0&& int.Parse(Form[i]) == 0)
                                {
                                    var per_id=int.Parse(Module[i]);
                                    var per = dbcontext.permissions.FirstOrDefault(m => m.ID == per_id);
                                    var role = dbcontext.Roles.FirstOrDefault(m => m.Name == per.Permission_Name);
                                    var flag=  userManager.AddToRole(user.Id, role.Name);
                                    var check_per_with_gr = dbcontext.group_with_permission.Where(m => m.Rolle_name == per.Permission_Name && m.User_Group_InfoID == Group.ID).ToList();

                                    if (flag.Succeeded && check_per_with_gr.Count() == 0)
                                    {
                                        var L = dbcontext.group_with_permission.Add(new group_with_permission { Rolle_name = role.Name, User_Group_InfoID = Group.ID });
                                        dbcontext.SaveChanges();
                                    }
                                }
                                if (int.Parse(Sub_module[i])!=0&& int.Parse(Module[i]) == 0&& int.Parse(Form[i]) == 0)
                                {
                                    var per_id = int.Parse(Sub_module[i]);
                                    var per = dbcontext.permissions.FirstOrDefault(m => m.ID == per_id);
                                    var role = dbcontext.Roles.FirstOrDefault(m => m.Name == per.Permission_Name);
                                    var flag=  userManager.AddToRole(user.Id, role.Name);
                                    var check_per_with_gr = dbcontext.group_with_permission.Where(m => m.Rolle_name == per.Permission_Name && m.User_Group_InfoID == Group.ID).ToList();
                                    if (flag.Succeeded && check_per_with_gr.Count()==0)
                                    {
                                        var L = dbcontext.group_with_permission.Add(new group_with_permission { Rolle_name = role.Name, User_Group_InfoID = Group.ID });
                                        dbcontext.SaveChanges();
                                    }
                                }
                                if (int.Parse(Form[i]) != 0&&int.Parse(Module[i]) == 0&& int.Parse(Sub_module[i]) == 0)
                                {
                                    var per_id = int.Parse(Form[i]);
                                    var per = dbcontext.permissions.FirstOrDefault(m => m.ID == per_id);
                                    var role = dbcontext.Roles.FirstOrDefault(m => m.Name == per.Permission_Name);
                                    var flag=userManager.AddToRole(user.Id, role.Name);
                                    var check_per_with_gr = dbcontext.group_with_permission.Where(m => m.Rolle_name == per.Permission_Name && m.User_Group_InfoID == Group.ID).ToList();

                                    if (flag.Succeeded && check_per_with_gr.Count() == 0)
                                    {
                                        var L = dbcontext.group_with_permission.Add(new group_with_permission { Rolle_name = role.Name, User_Group_InfoID = Group.ID });
                                        dbcontext.SaveChanges();
                                    }
                                  
                                }
                               
                               
                            }
                        }
                    }
                    TempData["Message"] = "permission added successfully";
                    return RedirectToAction("edit", "groupinfo", new { id = Group.ID });
                }
                return RedirectToAction("index");

            }
            catch (Exception)
            {
                return RedirectToAction("create", new { group_code = group });
            }
        }
        //====json
        public JsonResult getsub(int id_module)
        {
            var module = dbcontext.permissions.FirstOrDefault(m => m.ID == id_module);
            var per = dbcontext.permissions.Where(m => m.type_permission == type_permission.sub_module && m.module == module.module).ToList();
            return Json(per);
        }
        public JsonResult getforms(int module,int sub_module)
        {
            var sub_Modules = dbcontext.permissions.FirstOrDefault(m => m.ID == sub_module);
            var forms = dbcontext.permissions.Where(m => m.type_permission==type_permission.forms&&m.module == sub_Modules.module && m.sub_module == sub_Modules.sub_module).ToList().Select(m => new { Permission_Name = '[' + m.Permission_Name + ']', ID = m.ID }).ToList();
            return Json(forms);
        }
        public JsonResult getalldata(string group_code)
        {
            try
            {
                dbcontext.Configuration.ProxyCreationEnabled = false;
                var Group = dbcontext.User_Group_Info.FirstOrDefault(m => m.User_Group_Code == group_code);
                var g_p = dbcontext.group_with_permission.Where(m => m.User_Group_InfoID == Group.ID).ToList();
                var list_per = new List<vm_per>();
                foreach(var item in g_p)
                {
                    var per = dbcontext.permissions.FirstOrDefault(m => m.Permission_Name == item.Rolle_name);
                    if(per.type_permission==type_permission.module)
                    {
                        list_per.Add(new vm_per { id = per.ID,Type=1,text=per.Permission_Name});
                    }
                    else if (per.type_permission == type_permission.sub_module)
                    {

                        list_per.Add(new vm_per { id=per.ID, Type=2, text = per.Permission_Name });
                    }
                    else if (per.type_permission == type_permission.forms)
                    {
                        list_per.Add(new vm_per { id = per.ID, Type=3, text = per.Permission_Name });
                    }
                }
                return Json(list_per);

            }
            catch (Exception)
            {
                return Json(null);
            }
        }
    }
    public class vm_per
    {
       public int id { get; set; }
        public string text { get; set; }
        public int Type{get;set;}
        
    }
}