using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    public class TermsConditionController : Controller
    {
        // GET: Admin/TermsCondition
        OcdlogisticsEntities entities = new OcdlogisticsEntities();
        // GET: Admin/RefundPolicies
        public ActionResult Index(string message)
        {

            ViewBag.message = message;
            TermsConditions obj = new TermsConditions();
            TermsConditions currentobj = entities.TermsConditions.FirstOrDefault();
            if (currentobj != null)
            {
                obj = currentobj;
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Index(TermsConditions model)
        {
            try
            {
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    TermsConditions Oldobj = db.TermsConditions.FirstOrDefault();
                    if (Oldobj != null)
                    {
                        Oldobj.Contents = model.Contents;

                    }
                    else
                    {

                        model.Contents = model.Contents;
                        db.TermsConditions.Add(model);
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "TermsCondition", new { area = "Admin", message = "Some Error occour while updating   ..." });
            }
            return RedirectToAction("Index", "TermsCondition", new { area = "Admin", message = "  Update Successfully ..." });
        }
    }
}