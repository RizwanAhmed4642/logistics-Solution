using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Areas.AdminDashboard.Controllers
{
    public class DefaultController : Controller
    {
        // GET: AdminDashboard/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}