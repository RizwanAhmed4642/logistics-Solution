using OcdlogisticsSolution.Web.Infrastructure;
using OcdlogisticsSolution.Web.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    [Authorize]
    public class PanelController : AuthenticatedControllerEx
    {
        
        public ActionResult Index()
        {
            return View();
        }
    }
}