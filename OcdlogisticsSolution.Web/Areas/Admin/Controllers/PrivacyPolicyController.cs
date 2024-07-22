using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    public class PrivacyPolicyController : Controller
    {
        // GET: Admin/PrivacyPolicy
        OcdlogisticsEntities entities = new OcdlogisticsEntities();
        // GET: Admin/RefundPolicies
        public ActionResult Index(string message)
        {

            ViewBag.message = message;
            Tbl_PrivacyPolicy obj = new Tbl_PrivacyPolicy();
            Tbl_PrivacyPolicy currentobj = entities.Tbl_PrivacyPolicy.FirstOrDefault();
            if (currentobj != null)
            {
                obj = currentobj;
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Index(Tbl_PrivacyPolicy model)
        {
            try
            {
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    Tbl_PrivacyPolicy Oldobj = db.Tbl_PrivacyPolicy.FirstOrDefault();
                    if (Oldobj != null)
                    {
                        Oldobj.Contents = model.Contents;

                    }
                    else
                    {

                        model.Contents = model.Contents;
                        db.Tbl_PrivacyPolicy.Add(model);
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "PrivacyPolicy", new { area = "Admin", message = "Some Error occour while updating   ..." });
            }
            return RedirectToAction("Index", "PrivacyPolicy", new { area = "Admin", message = "  Update Successfully ..." });
        }
    }
}