


using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Util;
using OcdlogisticsSolution.Web.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    [Authorize]
    public class ProfilePageController : AuthenticatedControllerEx
    {

        OcdlogisticsEntities entities = new OcdlogisticsEntities();
        public ActionResult EditProfilePage(string message)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.SocialHandlersEdit))
            {
                ViewBag.message = message;
                tbl_ProfilePage obj = new tbl_ProfilePage();
                tbl_ProfilePage currentobj = entities.tbl_ProfilePage.FirstOrDefault();
                if (currentobj != null)
                {
                    obj = currentobj;
                }
                return View(obj);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfilePage(tbl_ProfilePage model, HttpPostedFileBase seminar, HttpPostedFileBase expert, HttpPostedFileBase backimg1, HttpPostedFileBase backimg2, HttpPostedFileBase backimg3)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.SocialHandlersEdit))
            {

                try
                {
                    using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                    {
                        tbl_ProfilePage Oldobj = db.tbl_ProfilePage.FirstOrDefault();
                        if (Oldobj != null)
                        {
                            Oldobj.BackGroungdColorbody = model.BackGroungdColorbody;
                            Oldobj.BackGroungdColorBanner = model.BackGroungdColorBanner;
                            Oldobj.BackGroungdColorFootr = model.BackGroungdColorFootr;
                            Oldobj.ExpertImageText = model.ExpertImageText;
                            Oldobj.SeminarImageText = model.SeminarImageText;
                            Oldobj.ProfilePageQuote = model.ProfilePageQuote;

                            if (seminar != null)
                            {
                                Oldobj.ProfilePageSeminarImage = FileManager.SaveImage(seminar);
                            }
                            if (expert != null)
                            {
                                Oldobj.ProfilePageExpertImage = FileManager.SaveImage(expert);
                            }
                            if (backimg1 != null)
                            {
                                Oldobj.BackGroungdColorBanner = null;
                                Oldobj.bckimg1 = FileManager.SaveImage(backimg1);
                            }
                            if (backimg2 != null)
                            {
                                Oldobj.BackGroungdColorbody = null;
                                Oldobj.bckimg2 = FileManager.SaveImage(backimg2);
                            }
                            if (backimg3 != null)
                            {
                                Oldobj.BackGroungdColorFootr = null;
                                Oldobj.bckimg3 = FileManager.SaveImage(backimg3);
                            }

                        }
                        else
                        {
                            if (seminar != null)
                            {
                                model.ProfilePageSeminarImage = FileManager.SaveImage(seminar);
                            }
                            if (expert != null)
                            {
                                model.ProfilePageExpertImage = FileManager.SaveImage(expert);
                            }
                            if (backimg1 != null)
                            {
                                Oldobj.bckimg1 = FileManager.SaveImage(backimg1);
                            }
                            if (backimg2 != null)
                            {
                                Oldobj.bckimg2 = FileManager.SaveImage(backimg2);
                            }
                            if (backimg3 != null)
                            {
                                Oldobj.bckimg3 = FileManager.SaveImage(backimg3);
                            }
                            db.tbl_ProfilePage.Add(model);
                        }

                       await db.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("EditProfilePage", "ProfilePage", new { area = "Admin", message = "Some Error occour while updating our profile page ..." });
                }
                return RedirectToAction("EditProfilePage", "ProfilePage", new { area = "Admin", message = "Profile Page Update Successfully ..." });
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }

    }
}
