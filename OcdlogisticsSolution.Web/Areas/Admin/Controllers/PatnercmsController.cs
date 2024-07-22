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
    public class PatnercmsController : Controller
    {
        // GET: Admin/Patnercms
        OcdlogisticsEntities entities = new OcdlogisticsEntities();
        public ActionResult EditPartner(string message)
        {
            ViewBag.message = message;
            tbl_PartnerCms obj = new tbl_PartnerCms();
            tbl_PartnerCms currentobj = entities.tbl_PartnerCms.FirstOrDefault();
            if (currentobj != null)
            {
                obj = currentobj;
            }
            return View(obj);
        }
        [HttpPost]
        public async Task<ActionResult> EditPartner(tbl_PartnerCms model, HttpPostedFileBase EmpFile1)
        {
            try
            {
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    tbl_PartnerCms Oldobj = db.tbl_PartnerCms.FirstOrDefault();
                    if (Oldobj != null)
                    {
                        Oldobj.PartnerHeading = model.PartnerHeading;
                        Oldobj.PartnerContent = model.PartnerContent;
                      
                        if (EmpFile1 != null)
                        {
                            Oldobj.BackGroundImg = FileManager.SaveImage(EmpFile1);
                        }
                    
                    }


                    else
                    {
                        model.PartnerHeading = model.PartnerHeading;
                        model.PartnerContent = model.PartnerContent;


                        if (EmpFile1 != null)
                        {
                            model.BackGroundImg = FileManager.SaveImage(EmpFile1);
                        }
                     
                        db.tbl_PartnerCms.Add(model);
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                return RedirectToAction("EditPartner","Patnercms", new { area = "Admin", message = "Some Error occour while updating   ..." });
            }
            return RedirectToAction("EditPartner", "Patnercms", new { area = "Admin", message = "  Update Successfully ..." });
        }
    }
}