

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;
using OcdlogisticsSolution.Web.Models.Authorization;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Util;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    [Authorize]

    public class SocialHandlerController : AuthenticatedControllerEx
    {
        // GET: AdminDashboard/Event

        public ActionResult EditSocialHandler(string message)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToUpdateGlobalSettings))
            {
                tbl_SocialHandler Oldobj = OcdlogisticsEntities.tbl_SocialHandler.FirstOrDefault();
                ViewBag.message = message;
                return View(Oldobj);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
        [HttpPost]
        public async Task<ActionResult> EditTeamProfile(tbl_SocialHandler model, HttpPostedFileBase header, HttpPostedFileBase footer)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToUpdateGlobalSettings))
            {
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
            {
                try
                {

                    tbl_SocialHandler Oldobj = db.tbl_SocialHandler.FirstOrDefault();
                    if (Oldobj != null)
                    {
                        Oldobj.Email = model.Email;
                        Oldobj.Facebook = model.Facebook;
                        Oldobj.Instagram = model.Instagram;
                        Oldobj.LinkedIn = model.LinkedIn;
                        Oldobj.Phone = model.Phone;
                        Oldobj.Pintrest = model.Pintrest;
                        Oldobj.Snapchat = model.Snapchat;
                        Oldobj.TextMessage = model.TextMessage;
                        Oldobj.Twitter = model.Twitter;
                        Oldobj.Youtube = model.Youtube;
                        Oldobj.Adress = model.Adress;
                        Oldobj.AmercianExpress = model.AmercianExpress;
                        Oldobj.BusinessName = model.BusinessName;
                        Oldobj.Discover = model.Discover;
                        Oldobj.MasterCard = model.MasterCard;
                        Oldobj.Norten = model.Norten;
                        Oldobj.Paypal = model.Paypal;
                        Oldobj.SSl = model.SSl;
                        Oldobj.VisaBit = model.VisaBit;
                        Oldobj.CompanyName = model.CompanyName;
                        Oldobj.CusinessSlogan = model.CusinessSlogan;
                        Oldobj.LegalBusinessName = model.LegalBusinessName;
                        Oldobj.PrimerColor = model.PrimerColor;
                        Oldobj.PrimerColor2 = model.PrimerColor2;
                        Oldobj.PrimerColor3 = model.PrimerColor3;
                        Oldobj.SecondaryColor = model.SecondaryColor;
                        Oldobj.SecondaryColor2 = model.SecondaryColor2;
                        Oldobj.SecondaryColor3 = model.SecondaryColor3;
                        Oldobj.SecondaryColor4 = model.SecondaryColor4;
                        Oldobj.SecondaryColor5 = model.SecondaryColor5;
                        Oldobj.Tiktok = model.Tiktok;

                        Oldobj.TitalBarColor = model.TitalBarColor;
                        Oldobj.TitalBarFont = model.TitalBarFont;
                        Oldobj.TitalBarSize = model.TitalBarSize;

                        Oldobj.HeaderColor = model.HeaderColor;
                        Oldobj.HeaderFont = model.HeaderFont;
                        Oldobj.HeaderSize = model.HeaderSize;

                        Oldobj.BodyColor = model.BodyColor;
                        Oldobj.BodyFont = model.BodyFont;
                        Oldobj.BodySize = model.BodySize;

                        Oldobj.SubHeaderColor = model.SubHeaderColor;
                        Oldobj.SubHeaderFont = model.SubHeaderFont;
                        Oldobj.SubHeaderSize = model.SubHeaderSize;

                        Oldobj.BodyContentColor = model.BodyContentColor;
                        Oldobj.BodyContentFont = model.BodyContentFont;
                        Oldobj.BodyContentSize = model.BodyContentSize;

                        Oldobj.SloganColor = model.SloganColor;
                        Oldobj.SloganFont = model.SloganFont;
                        Oldobj.SloganSize = model.SloganSize;

                        Oldobj.QuoteColor = model.QuoteColor;
                        Oldobj.QuoteFont = model.QuoteFont;
                        Oldobj.QuoteSize = model.QuoteSize;

                        Oldobj.WebsiteColor = model.WebsiteColor;
                        Oldobj.WebsiteFont = model.WebsiteFont;
                        Oldobj.WebsiteSize = model.WebsiteSize;

                        if (header != null)
                        {
                            Oldobj.HeaderImage110 = FileManager.SaveImage(header);
                        }
                        if (footer != null)
                        {
                            Oldobj.FooterImage80 = FileManager.SaveImage(footer);
                        }

                    }
                    else
                    {
                        if (header != null)
                        {
                            model.HeaderImage110 = FileManager.SaveImage(header);
                        }
                        if (footer != null)
                        {
                            model.FooterImage80 = FileManager.SaveImage(footer);
                        }

                        if (Oldobj == null)
                        {
                            model.UserId = CurrentUser.Id;
                            db.tbl_SocialHandler.Add(model);
                        }

                    }

                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return RedirectToAction("EditSocialHandler", "SocialHandler", new { area = "Admin", message = "Some Error occour while updating social handler ..." });
                }
                return RedirectToAction("EditSocialHandler", "SocialHandler", new { area = "Admin", message = "Social Handler Update Successfully ..." });
            }
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }

    }
}
