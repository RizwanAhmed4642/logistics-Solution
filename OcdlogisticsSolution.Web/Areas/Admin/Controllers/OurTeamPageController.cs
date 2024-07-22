using OcdlogisticsSolution.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using OcdlogisticsSolution.Web.Models.Authorization;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    [Authorize]
    public class OurTeamPageController : AuthenticatedControllerEx
    {

        public ActionResult EditOurTeamPage(string message)
        {
            
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.SocialHandlersEdit))
            {
                ViewBag.message = message;
                tbl_OurTeamPage obj = new tbl_OurTeamPage();
                tbl_OurTeamPage currentobj = OcdlogisticsEntities.tbl_OurTeamPage.FirstOrDefault();
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
        public async Task<ActionResult> EditOurTeamPage(tbl_OurTeamPage model, HttpPostedFileBase Backimg, HttpPostedFileBase seminar, HttpPostedFileBase expert, HttpPostedFileBase bannerimg)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.SocialHandlersEdit))
            {
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    try
                    {
                        tbl_OurTeamPage Oldobj = db.tbl_OurTeamPage.FirstOrDefault();
                        if (Oldobj != null)
                        {
                            // Oldobj.BackGroungdColor = model.BackGroungdColor;
                            Oldobj.Expertimgtext = model.Expertimgtext;
                            Oldobj.semenarimgtext = model.semenarimgtext;
                            Oldobj.BannerImgHeading = model.BannerImgHeading;
                            Oldobj.BannerImgText = model.BannerImgText;
                            Oldobj.OurTeamPageQuote = model.OurTeamPageQuote;
                            Oldobj.BannerImgColor = model.BannerImgColor;
                            Oldobj.ContentImgColor = model.ContentImgColor;
                            Oldobj.FootrImgColor = model.FootrImgColor;

                            if (Backimg != null)
                            {
                              
                                Oldobj.OurTeamBackimg = FileManager.SaveImage(Backimg);
                            }
                            if (seminar != null)
                            {
                                Oldobj.SeminarImage = FileManager.SaveImage(seminar);
                            }
                            if (expert != null)
                            {
                                Oldobj.ExpertImage = FileManager.SaveImage(expert);
                            }
                            if (bannerimg != null)
                            {
                                Oldobj.BannerImgColor = null;
                                Oldobj.Bannerimg = FileManager.SaveImage(bannerimg);
                            }

                        }
                        else
                        {
                            if (Backimg != null)
                            {
                                model.OurTeamBackimg = FileManager.SaveImage(Backimg);
                            }
                            if (seminar != null)
                            {
                                model.SeminarImage = FileManager.SaveImage(seminar);
                            }
                            if (expert != null)
                            {
                                model.ExpertImage = FileManager.SaveImage(expert);
                            }
                            //if (bannerimg != null)
                            //{
                            //    model.Bannerimg = FileManager.SaveImage(bannerimg);
                            //}

                            db.tbl_OurTeamPage.Add(model);
                        }
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("EditOurTeamPage", "OurTeamPage", new { area = "Admin", message = "Some Error occour while updating our team page ..." });
                    }

                   await db.SaveChangesAsync();
                }
                return RedirectToAction("EditOurTeamPage", "OurTeamPage", new { area = "Admin", message = "Team Page Update Successfully ..." });

            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }

    }
}
