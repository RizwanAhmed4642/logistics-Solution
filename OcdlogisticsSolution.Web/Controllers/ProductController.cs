using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OcdlogisticsSolution.Common.ViewModels;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Controllers
{

    public class ProductController : Controller
    {
        public ProductController()
        {

        }
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ProductController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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
        OcdlogisticsEntities ocdlogisticsEntities = new OcdlogisticsEntities();
        // GET: Product
        public ActionResult Index()
        {
            return View(ocdlogisticsEntities.tbl_Product);
        }



      

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Panel", new { area = "Admin" });
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [AcceptVerbs(new string[] { "GET", "POST" })]
        public ActionResult IsUniqueEmail(string email)
        {
            var IsValid = UserManager.FindByEmail(email) == null;
            if (IsValid)
            {
                return Json(true);
            }
            else
            {
                return Json($"This Email {email} is already taken, please try another");
            }
        }


        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(string returnUrl, RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    // string callbackUrll = await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account");
                    ViewBag.Message = "Check your email and confirm your account, you must be confirmed "
                        + "before you can log in.";
                    //DefaultConnection
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("CreateUser", "AdminUser", new { area = "Admin", errorfromedintity = "User created successfully..." });
                    }
                    return View("Info");
                    // return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("CreateUser", "AdminUser", new { area = "Admin", errorfromedintity = "User name already exist..." });
            }
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> Login(string email, string password)
        {
            //if (Request.IsAuthenticated)
            //{
            //    RedirectToLocal(returnUrl);
            //}

            if (!ModelState.IsValid)
            {
                return Json(new { IsOk = false });
            }
            var user = await UserManager.FindByNameAsync(email);
            if (user != null)
            {
                if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    //  string callbackUrll = await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account");
                    ViewBag.errorMessage = "You must have a confirmed email to login.";
                    ViewBag.email = email;
                    return Json(new { IsOk = false });
                }
            }

            var isAutheticated = Request.IsAuthenticated;
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(email, password, true, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return Json(new { IsOk = true });

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return Json(new { IsOk = false });
            }
        }
        
    }




}


