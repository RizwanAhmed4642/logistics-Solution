using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(HttpStatusCode? statusCode)
        {
            return View();
        }
    }
}