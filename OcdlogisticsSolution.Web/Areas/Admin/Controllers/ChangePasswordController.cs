
using OcdlogisticsSolution.Web.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    [Authorize]

    public class ChangePasswordController : AuthenticatedControllerEx
    {
        // GET: AdminDashboard/ChangePassword
        public ActionResult ChangePass(string ErrorMesage)
        {
            if (!string.IsNullOrEmpty(ErrorMesage))
            {
                ViewBag.error = ErrorMesage;
            }
            return View();
        }
    }
}