using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    public class ContactController : Controller
    {
        // GET: Admin/Contact
        OcdlogisticsEntities entities = new OcdlogisticsEntities();
        // GET: Admin/RefundPolicies
        public ActionResult EditContact(string message)
        {

            ViewBag.message = message;
            Tbl_ContactUs obj = new Tbl_ContactUs();
            Tbl_ContactUs currentobj = entities.Tbl_ContactUs.FirstOrDefault();
            if (currentobj != null)
            {
                obj = currentobj;
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> EditContact(Tbl_ContactUs model, HttpPostedFileBase image1, HttpPostedFileBase image2)
        {
            try
            {
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    Tbl_ContactUs Oldobj = db.Tbl_ContactUs.FirstOrDefault();
                    if (Oldobj != null)
                    {
                        Oldobj.Heading = model.Heading;
                        Oldobj.Contents = model.Contents;
                        if (image1 != null)
                        {
                            Oldobj.BannerImg = FileManager.SaveImage(image1);
                        }
                        if (image2 != null)
                        {
                            Oldobj.BodyImg = FileManager.SaveImage(image2);
                        }
                    }
                    else
                    {

                        model.Heading = model.Heading;
                        model.Contents = model.Contents;
                        if (image1 != null)
                        {
                            model.BannerImg = FileManager.SaveImage(image1);
                        }
                        if (image2 != null)
                        {
                            model.BodyImg = FileManager.SaveImage(image2);
                        }
                        db.Tbl_ContactUs.Add(model);
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                return RedirectToAction("EditContact", "Contact", new { area = "Admin", message = "Some Error occour while updating   ..." });
            }
            return RedirectToAction("EditContact", "Contact", new { area = "Admin", message = "  Update Successfully ..." });
        }
    }
}