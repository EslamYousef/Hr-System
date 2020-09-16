using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using HR.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;

namespace HR.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext dbcontext = new ApplicationDbContext();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _userManager= userManager;
        }
      
        public async Task<ActionResult> edit_profile(string id)
        {
            try
            {
                var cuurent_ID = User.Identity.GetUserId();
                ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "--[" + m.Full_Name + ']', ID = m.ID }).ToList();

                if (id != null) ///يفتح
                {
                    var Login_User1 = await UserManager.FindByIdAsync(id.ToString());
                    edit user_edit1 = new edit();
                    user_edit1.name = Login_User1.UserName;
                    user_edit1.mail = Login_User1.Email;
                    user_edit1.image_profile = Login_User1.ImagePath;
                    user_edit1.company_name = Login_User1.company_name;
                    user_edit1.employee_o = (int)Login_User1.employee_o;
                    user_edit1.active = Login_User1.active;
                    user_edit1.id = Login_User1.Id;
                    ViewBag.flag = true;
                    return View(user_edit1);
                }
                else
                {
                    var userId = User.Identity.GetUserId();
                    var Login_User = await UserManager.FindByIdAsync(userId);
                    edit user_edit = new edit();
                    user_edit.id = Login_User.Id;
                    user_edit.name = Login_User.UserName;
                    user_edit.mail = Login_User.Email;
                    user_edit.image_profile = Login_User.ImagePath;
                    user_edit.company_name = Login_User.company_name;
                    user_edit.active = Login_User.active;
                   
                    return View(user_edit);
                }

            }
            catch (Exception e)
            {
                return RedirectToAction("index","Home");

            }
        }
        [HttpPost]
        public async  Task<ActionResult> edit_profile(edit model, HttpPostedFileBase file,string Command)
        {
            var cuurent_ID = User.Identity.GetUserId();
            ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "--[" + m.Full_Name + ']', ID = m.ID }).ToList();
            var userId =model.id;
            var Login_User = await UserManager.FindByIdAsync(userId);
            model.image_profile = Login_User.ImagePath;
          //  model.company_name = Login_User.company_name;
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "ERROR.";
                if(cuurent_ID!=model.id)
                {
                    return RedirectToAction("edit_profile",new { id=model.id});
                }
                else
                {
                    return View(model);
                }
               
            }
            try
            {
              

                Login_User.UserName = model.name;
                Login_User.Email = model.mail;
                Login_User.company_name = model.company_name;
                Login_User.active = model.active;
                if (model.employee_o != 0)
                {
                    var id_em = model.employee_o;
                    Login_User.employee_name = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == id_em).Full_Name;
                    Login_User.employee_o =model.employee_o;
                }
                else
                {
                    if (cuurent_ID != model.id)
                    {
                        return RedirectToAction("edit_profile","account", new { id = model.id });
                    }
                }
                if (model.image_profile!=null)
                Login_User.ImagePath = model.image_profile;

                if (file != null && file.ContentLength > 0)
                {
                    var supportedTypes = new[] { "jpg", "jpeg", "png" };
                    var fileName = Path.GetExtension(file.FileName);
                    if (fileName != null)
                    {
                        var fileExt = fileName.Substring(1);
                        if (!supportedTypes.Contains(fileExt))
                        {
                            TempData["Message"] = "Invalid image type. Only the following types (jpg, jpeg, png) are supported.";
                            if (cuurent_ID != model.id)
                            {
                                return RedirectToAction("edit_profile", new { id = model.id });
                            }
                            else
                            {
                                return View(model);
                            }
                        }
                        var photoName = Guid.NewGuid().ToString("N") + "." + fileExt;
                        var photo = Server.MapPath("~/Images/Users/") + photoName;
                        file.SaveAs(photo);
                        Login_User.ImagePath = "/Images/Users/" + photoName;
                    }
                }
                if (!string.IsNullOrEmpty(model.password) && !string.IsNullOrEmpty(model.new_password) && model.new_password == model.confirmpassword)
                {
                    var result = await UserManager.ChangePasswordAsync(userId, model.password, model.new_password);
                    if (!result.Succeeded)
                    {
                        AddErrors(result);
                        if (cuurent_ID != model.id)
                        {
                            return RedirectToAction("edit_profile", new { id = model.id });
                        }
                        else
                        {
                            return View(model);
                        }
                    }
                    await SignInManager.SignInAsync(Login_User, isPersistent: false, rememberBrowser: false);
                }
                else if(!string.IsNullOrEmpty(model.new_password) && model.new_password != model.confirmpassword)
                {
                    TempData["Message"] = "new password not matching";
                    if (cuurent_ID != model.id)
                    {
                        return RedirectToAction("edit_profile", new { id = model.id });
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    TempData["Message"] = "password not updated";
                }
                var result1 = await UserManager.UpdateAsync(Login_User);
                if (!result1.Succeeded)
                {
                    AddErrors(result1);
                    if (cuurent_ID != model.id)
                    {
                        return RedirectToAction("edit_profile", new { id = model.id });
                    }
                    else
                    {
                        return View(model);
                    }
                }
                TempData["msg"] = "Profile Changes Saved !";
                if (Command == "link")
                {
                    return RedirectToAction("link", "groupinfo", new { userid = Login_User.Id });
                }
                else if (cuurent_ID != model.id)
                {
                    return RedirectToAction("all", "Account");
                }
                else
                {
                    return RedirectToAction("index","Home");
                }

            }
            catch (Exception e)
            {
                TempData["Message"] = "ERROR.";
                return View(model);

            }
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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            ApplicationDbContext db = new ApplicationDbContext();
            var users=db.Users.ToList();
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    {
                      var user=  await UserManager.FindByNameAsync(model.UserName);
                        if(user.active==false)
                        {
                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                            return View("Lockout");
                        }
                        else
                        {
                            return RedirectToLocal(returnUrl);
                        }
                        
                     
                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }



        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "--[" + m.Full_Name + ']', ID = m.ID }).ToList();

            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult all()
        {
            var C_ID = User.Identity.GetUserId();         
            var users = dbcontext.Users.Where(m=>m.Id!= C_ID).ToList();
            return View(users);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model,string Command)
        {
            ViewBag.employee = dbcontext.Employee_Profile.ToList().Select(m => new { Code = "" + m.Code + "--[" + m.Full_Name + ']', ID = m.ID }).ToList();
            if (ModelState.IsValid)
            {
                var name_em = "";
                if(model.employee!=null)
                {
                    var id_em = int.Parse(model.employee);
                    name_em = dbcontext.Employee_Profile.FirstOrDefault(m => m.ID == id_em).Full_Name;
                }
                else
                {
                    return View(model);
                }
                var user = new ApplicationUser {active=model.active,employee_name= name_em, employee_o=int.Parse(model.employee),company_name=model.company_name,UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    if(Command=="link")
                    {
                        return RedirectToAction("link", "groupinfo",new { userid = user.Id});
                    }
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
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
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
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

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}