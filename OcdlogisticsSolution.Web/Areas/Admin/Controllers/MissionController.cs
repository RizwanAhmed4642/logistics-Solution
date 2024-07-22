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
    public class MissionController : Controller
    {
        // GET: Admin/Mission
        OcdlogisticsEntities entities = new OcdlogisticsEntities();

        public ActionResult EditMission(string message)
        {
            ViewBag.message = message;
            Tbl_Misson obj = new Tbl_Misson();
            Tbl_Misson currentobj = entities.Tbl_Misson.FirstOrDefault();
            if (currentobj != null)
            {
                obj = currentobj;
            }
            return View(obj);
        }
        [HttpPost]
        public async Task<ActionResult> EditMission(Tbl_Misson model, HttpPostedFileBase EmpFile1, HttpPostedFileBase EmpFile2)
        {
            try
            {
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    Tbl_Misson Oldobj = db.Tbl_Misson.FirstOrDefault();
                    if (Oldobj != null)
                    {
                        Oldobj.Heading = model.Heading;
                        Oldobj.Contents = model.Contents;
                        Oldobj.BelowHeading = model.BelowHeading;
                        Oldobj.BlowContents = model.BlowContents;

                        if (EmpFile1 != null)
                        {
                            Oldobj.ImgUrl = FileManager.SaveImage(EmpFile1);
                        }
                        if (EmpFile2 != null)
                        {
                            Oldobj.VedioUrl = FileManager.SaveImage(EmpFile2);
                        }
                    }


                    else
                    {


                        model.Heading = model.Heading;
                        model.Contents = model.Contents;
                        model.BelowHeading = model.BelowHeading;
                        model.BlowContents = model.BlowContents;

                        if (EmpFile1 != null)
                        {
                            model.ImgUrl = FileManager.SaveImage(EmpFile1);
                        }
                        if (EmpFile2 != null)
                        {
                            model.VedioUrl = FileManager.SaveImage(EmpFile2);
                        }

                        db.Tbl_Misson.Add(model);
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                return RedirectToAction("EditMission", "Mission", new { area = "Admin", message = "Some Error occour while updating   ..." });
            }
            return RedirectToAction("EditMission", "Mission", new { area = "Admin", message = "  Update Successfully ..." });
        }
    }
}