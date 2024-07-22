
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Infrastructure;


namespace OcdlogisticsSolution.Controllers
{
    public class EmailSubscriptionController : Controller
    {
        OcdlogisticsEntities _entities;
        public EmailSubscriptionController()
        {
            _entities = new OcdlogisticsEntities();
        }
        [HttpPost]
        [ValidateGoogleCaptcha]
        public ActionResult AddSubscriptionEmail(string data)
        {
            tbl_SubscribeToNewsLatter email = new tbl_SubscribeToNewsLatter();
            email.email = data;
            email.IsActive = true;
            email.SubscribeNewslatterId = Guid.NewGuid().ToString();
            if (!string.IsNullOrEmpty(data) && _entities.tbl_SubscribeToNewsLatter.FirstOrDefault(x => x.email.ToLower() == data.ToLower()) == null)
            {
                _entities.tbl_SubscribeToNewsLatter.Add(email);
                _entities.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.ToString());
        }
        public PartialViewResult ReCaptcha()
        {
            return PartialView("_ReCaptcha");
        }
    }
}