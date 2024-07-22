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
    public class EducationBannerController : Controller
    {
        // GET: Admin/EducationBanner
        OcdlogisticsEntities entities = new OcdlogisticsEntities();

        public ActionResult EditEduc(string message)
        {
            ViewBag.message = message;
            Tbl_EducationBanner obj = new Tbl_EducationBanner();
            Tbl_EducationBanner currentobj = entities.Tbl_EducationBanner.FirstOrDefault();
            if (currentobj != null)
            {
                obj = currentobj;
            }
            return View(obj);
        }
        [HttpPost]
        public async Task<ActionResult> EditEduc(Tbl_EducationBanner model, HttpPostedFileBase EmpFile1, HttpPostedFileBase EmpFile2)
        {
            try
            {
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    Tbl_EducationBanner Oldobj = db.Tbl_EducationBanner.FirstOrDefault();
                    if (Oldobj != null)
                    {
                        Oldobj.BannerHeading = model.BannerHeading;
                        Oldobj.BannerContent = model.BannerContent;
                        Oldobj.ImgHeading = model.ImgHeading;
                        Oldobj.ImgContent = model.ImgContent;

                        if (EmpFile1 != null)
                        {
                            Oldobj.BannerImg = FileManager.SaveImage(EmpFile1);
                        }
                        if (EmpFile2 != null)
                        {
                            Oldobj.Img = FileManager.SaveImage(EmpFile2);
                        }
                    }


                    else
                    {


                        model.BannerHeading = model.BannerHeading;
                        model.BannerContent = model.BannerContent;
                        model.BannerImg = model.BannerImg;
                        model.Img = model.Img;

                        if (EmpFile1 != null)
                        {
                            model.BannerImg = FileManager.SaveImage(EmpFile1);
                        }
                        if (EmpFile2 != null)
                        {
                            model.Img = FileManager.SaveImage(EmpFile2);
                        }

                        db.Tbl_EducationBanner.Add(model);
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                return RedirectToAction("EditEduc", "EducationBanner", new { area = "Admin", message = "Some Error occour while updating   ..." });
            }
            return RedirectToAction("EditEduc", "EducationBanner", new { area = "Admin", message = "  Update Successfully ..." });
        }
    }
}