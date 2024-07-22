using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Models.Authorization
{
    public class AuthenticatedControllerEx : Controller
    {
        public string UserId
        {
            get
            {
                return User.Identity.GetUserId();
            }
        }

        public OcdlogisticsEntities OcdlogisticsEntities = new OcdlogisticsEntities();

        public AspNetUsers CurrentUser
        {
            get
            {
                return OcdlogisticsEntities.AspNetUsers.Find(UserId);
            }
        }
        ApplicationUserManager _userManager;
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
    }
}