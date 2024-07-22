using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Models;
using OcdlogisticsSolution.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    public class SponserEducationController : Controller
    {
        // GET: Admin/SponserEducation
        // GET: Admin/Sponser
        OcdlogisticsEntities context;
        public SponserEducationController()
        {
            context = new OcdlogisticsEntities();
        }

        // GET: Admin/Solution
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSponserEducation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSponserEducation(Tbl_SponserEducation model, HttpPostedFileBase image)
        {


            if (image != null)
            {
                model.imgurl = FileManager.SaveImage(image);
            }


            context.Tbl_SponserEducation.Add(model);
            context.SaveChanges();

            return RedirectToAction("Index", "SponserEducation", new { area = "Admin" });
        }
        public JsonResult GetList()
        {
            List<Tbl_SponserEducation> list = context.Tbl_SponserEducation.ToList();
            JQueryDataTableParamModel jqObj = DataTablesFilter.SetDataTablesFilter(Request);
            if (jqObj.iDisplayLength != 0)
            {
                IQueryable<Tbl_SponserEducation> filteredRecords;
                IQueryable<Tbl_SponserEducation> allRecords = list.AsQueryable();
                //Check whether the companies should be filtered by keyword

                if (!string.IsNullOrEmpty(jqObj.sSearch))
                {

                    filteredRecords = allRecords.Where(c =>
                    c.name.ToLower().ToString().Contains(jqObj.sSearch.ToLower())

                    );
                }
                else
                {
                    filteredRecords = allRecords.AsQueryable();
                }

                var sortColumnIndex = Convert.ToInt32(Request.Params["iSortCol_0"]);

                Func<Tbl_SponserEducation, string> orderingFunction = (c => sortColumnIndex == 0 ? c.name.ToString() : "");

                var sortDirection = Request.Params["sSortDir_0"]; // asc or desc
                if (sortDirection == "asc")
                    filteredRecords = filteredRecords.OrderBy(orderingFunction).AsQueryable();
                else
                    filteredRecords = filteredRecords.OrderByDescending(orderingFunction).AsQueryable();

                var displayedList = filteredRecords.Skip(jqObj.iDisplayStart).Take(jqObj.iDisplayLength)
                    .Select(s => new
                    {
                        s.Id,
                        s.imgurl,
                        s.name


                    }).ToList();

                var Result = new
                {
                    sEcho = jqObj.sEcho,
                    iTotalRecords = allRecords.Count(),
                    iTotalDisplayRecords = filteredRecords.Count(),
                    aaData = displayedList
                };

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            else
                return null;

        }
        public PartialViewResult List()
        {
            return PartialView();
        }
        public PartialViewResult EditSponserEducation(int Id)
        {
            Tbl_SponserEducation obj = context.Tbl_SponserEducation.Find(Id);
            return PartialView(obj);
        }
        [HttpPost]
        public async Task<ActionResult> EditSponserEducation(Tbl_SponserEducation model, HttpPostedFileBase image)
        {

            using (OcdlogisticsEntities db = new OcdlogisticsEntities())
            {
                Tbl_SponserEducation oldobj = db.Tbl_SponserEducation.FirstOrDefault(x => x.Id == model.Id);
                if (oldobj != null)
                {

                    oldobj.name = model.name;


                    if (image != null)
                    {
                        oldobj.imgurl = FileManager.SaveImage(image);
                    }


                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index", "SponserEducation", new { area = "Admin" });

        }
        public async Task<ActionResult> Delete(int id)
        {

            using (OcdlogisticsEntities db = new OcdlogisticsEntities())
            {
                OcdlogisticsSolution.DomainModels.Models.Entity_Models.Tbl_SponserEducation Oldobj = db.Tbl_SponserEducation.FirstOrDefault(x => x.Id == id);
                db.Tbl_SponserEducation.Remove(Oldobj);

                await db.SaveChangesAsync();

            }
            return RedirectToAction("Index", "SponserEducation", new { area = "Admin" });
        }

    }
}