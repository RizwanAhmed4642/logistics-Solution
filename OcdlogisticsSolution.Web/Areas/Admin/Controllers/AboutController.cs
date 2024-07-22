using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    public class AboutController : Controller
    {
        OcdlogisticsEntities entities = new OcdlogisticsEntities();

        public ActionResult Editabout(string message)
        {
            ViewBag.message = message;
            tbl_AboutUs obj = new tbl_AboutUs();
            tbl_AboutUs currentobj = entities.tbl_AboutUs.FirstOrDefault();
            if (currentobj != null)
            {
                obj = currentobj;
            }
            return View(obj);
        }
        [HttpPost]
        public async Task<ActionResult> EditAbout(tbl_AboutUs model, HttpPostedFileBase EmpFile1, HttpPostedFileBase EmpFile2, HttpPostedFileBase EmpFile3, HttpPostedFileBase EmpFile4, HttpPostedFileBase EmpFile5, HttpPostedFileBase EmpFile6, HttpPostedFileBase EmpFile7, HttpPostedFileBase EmpFile8, HttpPostedFileBase EmpFile9, HttpPostedFileBase EmpFile10, HttpPostedFileBase EmpFile11, HttpPostedFileBase EmpFile12)
        {
            try
            {
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    tbl_AboutUs Oldobj = db.tbl_AboutUs.FirstOrDefault();
                    if (Oldobj != null)
                    {
                        Oldobj.SliderOverlayColor = model.SliderOverlayColor;
                        Oldobj.SliderOverlayHeading = model.SliderOverlayHeading;
                        Oldobj.SliderOverlayContent = model.SliderOverlayContent;
                        Oldobj.MainSliderBelowHeading = model.MainSliderBelowHeading;
                        Oldobj.VideoHeading = model.VideoHeading;
                        Oldobj.VideoContent = model.VideoContent;
                        Oldobj.VideoContentFooter = model.VideoContentFooter;
                        Oldobj.AttentionDetailHeading = model.AttentionDetailHeading;
                        Oldobj.AttentionDetailContent = model.AttentionDetailContent;
                        Oldobj.AttentíonDetailFooter = model.AttentíonDetailFooter;
                        Oldobj.FirstFullScreenBelowHeading = model.FirstFullScreenBelowHeading;
                        Oldobj.FirstFullScreenBelowContent = model.FirstFullScreenBelowContent;
                        Oldobj.FirstFullScreenBelowFooter = model.FirstFullScreenBelowFooter;
                        Oldobj.FirstFullScreenBelowSecondHeading = model.FirstFullScreenBelowSecondHeading;
                        Oldobj.FirstFullScreenBelowSecondContent = model.FirstFullScreenBelowSecondContent;
                        Oldobj.FirstFullScreenBelowSecondFooter = model.FirstFullScreenBelowSecondFooter;
                        Oldobj.SecondFullScreenBelowHeading = model.SecondFullScreenBelowHeading;
                        Oldobj.SecondFullScreenBelowContent = model.SecondFullScreenBelowContent;
                        Oldobj.SecondFullScreenbelowfooter = model.SecondFullScreenbelowfooter;
                        Oldobj.SecondFullScreenbelowSecondHeading = model.SecondFullScreenbelowSecondHeading;
                        Oldobj.SecondFullScreenbelowSecondContent = model.SecondFullScreenbelowSecondContent;
                        Oldobj.SecondFullScreenbelowSecondFooter = model.SecondFullScreenbelowSecondFooter;
                        Oldobj.ThirdFullScreenBelowFirstLayoverContent = model.ThirdFullScreenBelowFirstLayoverContent;
                        Oldobj.ThirdFullScreenBelowFirstLayoverColor = model.ThirdFullScreenBelowFirstLayoverColor;
                        Oldobj.ThirdFullScreenBelowSecondLayoverColor = model.ThirdFullScreenBelowSecondLayoverColor;
                        Oldobj.ThirdFullScreenBelowFooterText = model.ThirdFullScreenBelowFooterText;
                        Oldobj.ThirdFullScreenBelowSecondLayoverContent = model.ThirdFullScreenBelowSecondLayoverContent;
                        if (EmpFile1 != null)
                        {
                            Oldobj.MainSliderImg = FileManager.SaveImage(EmpFile1);
                        }
                        if (EmpFile2 != null)
                        {
                            Oldobj.VideoPath = FileManager.SaveImage(EmpFile2);
                        }

                        if (EmpFile3 != null)
                        {
                            Oldobj.AttentíonDetailImgPath = FileManager.SaveImage(EmpFile3);
                        }
                        if (EmpFile4 != null)
                        {
                            Oldobj.FirstFullScreenImgPath = FileManager.SaveImage(EmpFile4);

                        }

                        if (EmpFile5 != null)
                        {
                            Oldobj.FirstFullScreenBelowImgPath = FileManager.SaveImage(EmpFile5);

                        }

                        if (EmpFile6 != null)
                        {
                            Oldobj.FirstFullScreenBelowSecondImgPath = FileManager.SaveImage(EmpFile6);

                        }

                        if (EmpFile7 != null)
                        {
                            Oldobj.SecondFullScreenImgPath = FileManager.SaveImage(EmpFile7);

                        }

                        if (EmpFile8 != null)
                        {
                            Oldobj.SecondFullScreenbelowImgPath = FileManager.SaveImage(EmpFile8);

                        }

                        if (EmpFile9 != null)
                        {
                            Oldobj.SecondFullScreenbelowSecondImgPath = FileManager.SaveImage(EmpFile9);

                        }
                        if (EmpFile10 != null)
                        {
                            Oldobj.ThirdFullScreenImg = FileManager.SaveImage(EmpFile10);

                        }


                        if (EmpFile11 != null)
                        {
                            Oldobj.ThirdFullScreenBelowImg = FileManager.SaveImage(EmpFile11);

                        }
                        if (EmpFile12 != null)
                        {
                            Oldobj.FullScreenBelowSecondImg = FileManager.SaveImage(EmpFile12);

                        }
                    }
                    else
                    {

                        model.SliderOverlayColor = model.SliderOverlayColor;
                        model.SliderOverlayHeading = model.SliderOverlayHeading;
                        model.SliderOverlayContent = model.SliderOverlayContent;
                        model.MainSliderBelowHeading = model.MainSliderBelowHeading;
                        model.VideoHeading = model.VideoHeading;
                        model.VideoContent = model.VideoContent;
                        model.VideoContentFooter = model.VideoContentFooter;
                        model.AttentionDetailHeading = model.AttentionDetailHeading;
                        model.AttentionDetailContent = model.AttentionDetailContent;
                        model.AttentíonDetailFooter = model.AttentíonDetailFooter;
                        model.FirstFullScreenBelowHeading = model.FirstFullScreenBelowHeading;
                        model.FirstFullScreenBelowContent = model.FirstFullScreenBelowContent;
                        model.FirstFullScreenBelowFooter = model.FirstFullScreenBelowFooter;
                        model.FirstFullScreenBelowSecondHeading = model.FirstFullScreenBelowSecondHeading;
                        model.FirstFullScreenBelowSecondContent = model.FirstFullScreenBelowSecondContent;
                        model.FirstFullScreenBelowSecondFooter = model.FirstFullScreenBelowSecondFooter;
                        model.SecondFullScreenBelowHeading = model.SecondFullScreenBelowHeading;
                        model.SecondFullScreenBelowContent = model.SecondFullScreenBelowContent;
                        model.SecondFullScreenbelowfooter = model.SecondFullScreenbelowfooter;
                        model.SecondFullScreenbelowSecondHeading = model.SecondFullScreenbelowSecondHeading;
                        model.SecondFullScreenbelowSecondContent = model.SecondFullScreenbelowSecondContent;
                        model.SecondFullScreenbelowSecondFooter = model.SecondFullScreenbelowSecondFooter;
                        model.ThirdFullScreenBelowFirstLayoverContent = model.ThirdFullScreenBelowFirstLayoverContent;
                        model.ThirdFullScreenBelowFirstLayoverColor = model.ThirdFullScreenBelowFirstLayoverColor;
                        model.ThirdFullScreenBelowSecondLayoverColor = model.ThirdFullScreenBelowSecondLayoverColor;
                        model.ThirdFullScreenBelowFooterText = model.ThirdFullScreenBelowFooterText;
                        model.ThirdFullScreenBelowSecondLayoverContent = model.ThirdFullScreenBelowSecondLayoverContent;

                        if (EmpFile1 != null)
                        {
                            model.MainSliderImg = FileManager.SaveImage(EmpFile1);
                        }
                        if (EmpFile2 != null)
                        {
                            model.VideoPath = FileManager.SaveImage(EmpFile2);
                        }

                        if (EmpFile3 != null)
                        {
                            model.AttentíonDetailImgPath = FileManager.SaveImage(EmpFile3);
                        }
                        if (EmpFile4 != null)
                        {
                            model.FirstFullScreenImgPath = FileManager.SaveImage(EmpFile4);

                        }
                        if (EmpFile5 != null)
                        {
                            model.FirstFullScreenBelowImgPath = FileManager.SaveImage(EmpFile5);

                        }

                        if (EmpFile6 != null)
                        {
                            model.FirstFullScreenBelowSecondImgPath = FileManager.SaveImage(EmpFile6);

                        }

                        if (EmpFile7 != null)
                        {
                            model.SecondFullScreenImgPath = FileManager.SaveImage(EmpFile7);

                        }

                        if (EmpFile8 != null)
                        {
                            model.SecondFullScreenbelowImgPath = FileManager.SaveImage(EmpFile6);

                        }

                        if (EmpFile9 != null)
                        {
                            model.SecondFullScreenbelowSecondImgPath = FileManager.SaveImage(EmpFile9);

                        }
                        if (EmpFile10 != null)
                        {
                            model.ThirdFullScreenImg = FileManager.SaveImage(EmpFile10);

                        }


                        if (EmpFile11 != null)
                        {
                            model.ThirdFullScreenBelowImg = FileManager.SaveImage(EmpFile11);

                        }
                        if (EmpFile12 != null)
                        {
                            model.FullScreenBelowSecondImg = FileManager.SaveImage(EmpFile12);

                        }
                        db.tbl_AboutUs.Add(model);

                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex )
            {
                throw;
                return RedirectToAction("Editabout", "About", new { area = "Admin", message = "Some Error occour while updating   ..." });
            }
            return RedirectToAction("Editabout", "About", new { area = "Admin", message = "  Update Successfully ..." });
        }
    }
}