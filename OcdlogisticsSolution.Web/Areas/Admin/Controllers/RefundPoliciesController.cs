using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    public class RefundPoliciesController : Controller
    {
        OcdlogisticsEntities entities = new OcdlogisticsEntities();
        // GET: Admin/RefundPolicies
        public ActionResult Index(string message)
        {

            ViewBag.message = message;
            Tbl_RefundPolicy obj = new Tbl_RefundPolicy();
            Tbl_RefundPolicy currentobj = entities.Tbl_RefundPolicy.FirstOrDefault();
            if (currentobj != null)
            {
                obj = currentobj;
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Index(Tbl_RefundPolicy model)
        {
            try
            {
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    Tbl_RefundPolicy Oldobj = db.Tbl_RefundPolicy.FirstOrDefault();
                    if (Oldobj != null)
                    {
                        Oldobj.Contents = model.Contents;
                     
                    }
                    else
                    {

                        model.Contents = model.Contents;
                        db.Tbl_RefundPolicy.Add(model);
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "RefundPolicies", new { area = "Admin", message = "Some Error occour while updating   ..." });
            }
            return RedirectToAction("Index", "RefundPolicies", new { area = "Admin", message = "  Update Successfully ..." });
        }
    }
}