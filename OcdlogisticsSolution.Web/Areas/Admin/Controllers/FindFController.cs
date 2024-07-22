using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    public class FindFController : Controller
    {
        OcdlogisticsEntities entities = new OcdlogisticsEntities();
        // GET: Admin/FindF
        //public ActionResult EditFindF(int Id)
        //{

        //    //  ViewBag.message = Message;
        //    Tbl_FindF obj = new Tbl_FindF();
        //    Tbl_FindF currentobj = entities.Tbl_FindF.FirstOrDefault();
        //    if (currentobj != null)
        //    {
        //        obj = currentobj;
        //    }
        //    return View(obj);
        //}
        //[HttpPost]
        //public async Task<ActionResult> EditFindF(Tbl_FindF model, HttpPostedFileBase image, HttpPostedFileBase image1)
        //{

        //    using (OcdlogisticsEntities db = new OcdlogisticsEntities())
        //    {
        //        Tbl_FindF oldobj = db.Tbl_FindF.FirstOrDefault();
        //        if (oldobj != null)
        //        {

        //            oldobj.BoxHeading = model.BoxHeading;
        //            oldobj.BoxContent = model.BoxContent;
        //            if (image != null)
        //            {
        //                oldobj.ImgPath = FileManager.SaveImage(image);
        //            }
        //            if (image1 != null)
        //            {
        //                oldobj.VedioPath = FileManager.SaveImage(image);
        //            }
        //            oldobj.BoxButonText = model.BoxButonText;
        //            oldobj.ButtonSideText = model.ButtonSideText;
        //            oldobj.MapBelowText = model.MapBelowText;
        //            oldobj.OCDTeamHeading = model.OCDTeamHeading;
        //            oldobj.OCDTeamContent = model.OCDTeamContent;
        //            oldobj.PartnerHeading = model.PartnerHeading;
        //        }
        //        else
        //        {
        //            model.Id = model.Id;
        //            model.BoxHeading = model.BoxHeading;
        //            model.BoxContent = model.BoxContent;
        //            if (image != null)
        //            {
        //                model.ImgPath = FileManager.SaveImage(image);
        //            }
        //            if (image1 != null)
        //            {
        //                model.VedioPath = FileManager.SaveImage(image);
        //            }
        //            model.BoxButonText = model.BoxButonText;
        //            model.ButtonSideText = model.ButtonSideText;
        //            model.MapBelowText = model.MapBelowText;
        //            model.OCDTeamHeading = model.OCDTeamHeading;
        //            model.OCDTeamContent = model.OCDTeamContent;
        //            model.PartnerHeading = model.PartnerHeading;
        //            db.Tbl_FindF.Add(model);

        //        }
        //        await db.SaveChangesAsync();
        //    }
        //    return RedirectToAction("EditFindF", "FindF", new { area = "Admin" });
        //}


    }
}