
using Form.ViewModels;
using HR;
using HR.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Company.Controllers
{
   
    public class SecurityController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _context;

        public SecurityController()
        {
            _context = new ApplicationDbContext();
        }

        public SecurityController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _context = new ApplicationDbContext();
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToAction("Index", "Home");
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "خطأ في اسم المستخدم أو كلمة المرور");
                        return View(model);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<ActionResult> UserProfile()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var user = await UserManager.FindByIdAsync(userId);

                var model = new ProfileEditViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    ImagePath = user.ImagePath,
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserProfile(ProfileEditViewModel model, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = User.Identity.GetUserId();
                var user = await UserManager.FindByIdAsync(userId);

                user.Email = model.Email;
                user.UserName = model.UserName;

                //if (file != null && file.ContentLength > 0)
                //{
                //    var supportedTypes = new[] { "jpg", "jpeg", "png" };
                //    var fileName = Path.GetExtension(file.FileName);
                //    if (fileName != null)
                //    {
                //        var fileExt = fileName.Substring(1);
                //        if (!supportedTypes.Contains(fileExt))
                //        {
                //            TempData["Message"] = "صورة غير مدعومة. فقط الصور بالمسارات (jpg, jpeg, png) مدعومة";
                //            return View(model);
                //        }
                //        var photoName = Guid.NewGuid().ToString("N") + "." + fileExt;
                //        var photo = Server.MapPath("~/Images/Users/") + photoName;
                //        file.SaveAs(photo);
                //        user.ImagePath = "/Images/Users/" + photoName;
                //    }
                //}

                if (!string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(model.NewPassword) && model.NewPassword == model.ConfirmPassword)
                {
                    var result = await UserManager.ChangePasswordAsync(userId, model.Password, model.NewPassword);
                    if (!result.Succeeded)
                    {
                        AddErrors(result);
                        return View(model);
                    }
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                var result1 = await UserManager.UpdateAsync(user);
                if (!result1.Succeeded)
                {
                    AddErrors(result1);
                    return View(model);
                }

                TempData["Message1"] = "تم تعديل الصفحة الشخصية بنجاح.";
                return RedirectToAction("UserProfile", "Security");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }

                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }

            base.Dispose(disposing);
        }

    }
}