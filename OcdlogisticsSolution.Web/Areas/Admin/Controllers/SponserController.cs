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
    public class SponserController : Controller
    {
        // GET: Admin/Sponser
        OcdlogisticsEntities context;
        public SponserController()
        {
            context = new OcdlogisticsEntities();
        }

        // GET: Admin/Solution
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSponser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSponser(Tbl_Sponser model, HttpPostedFileBase image)
        {


            if (image != null)
            {
                model.Img = FileManager.SaveImage(image);
            }
          

            context.Tbl_Sponser.Add(model);
            context.SaveChanges();

            return RedirectToAction("Index", "Sponser", new { area = "Admin" });
        }
        public JsonResult GetList()
        {
            List<Tbl_Sponser> list = context.Tbl_Sponser.ToList();
            JQueryDataTableParamModel jqObj = DataTablesFilter.SetDataTablesFilter(Request);
            if (jqObj.iDisplayLength != 0)
            {
                IQueryable<Tbl_Sponser> filteredRecords;
                IQueryable<Tbl_Sponser> allRecords = list.AsQueryable();
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

                Func<Tbl_Sponser, string> orderingFunction = (c => sortColumnIndex == 0 ? c.name.ToString() : "");

                var sortDirection = Request.Params["sSortDir_0"]; // asc or desc
                if (sortDirection == "asc")
                    filteredRecords = filteredRecords.OrderBy(orderingFunction).AsQueryable();
                else
                    filteredRecords = filteredRecords.OrderByDescending(orderingFunction).AsQueryable();

                var displayedList = filteredRecords.Skip(jqObj.iDisplayStart).Take(jqObj.iDisplayLength)
                    .Select(s => new
                    {
                        s.Id,
                        s.Img,
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
        public PartialViewResult EditSponser(int Id)
        {
            Tbl_Sponser obj = context.Tbl_Sponser.Find(Id);
            return PartialView(obj);
        }
        [HttpPost]
        public async Task<ActionResult> EditSponser(Tbl_Sponser model, HttpPostedFileBase image)
        {

            using (OcdlogisticsEntities db = new OcdlogisticsEntities())
            {
                Tbl_Sponser oldobj = db.Tbl_Sponser.FirstOrDefault(x => x.Id == model.Id);
                if (oldobj != null)
                {

                    oldobj.name = model.name;
                   

                    if (image != null)
                    {
                        oldobj.Img = FileManager.SaveImage(image);
                    }
                  

                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index", "Sponser", new { area = "Admin" });

        }
        public async Task<ActionResult> Delete(int id)
        {

            using (OcdlogisticsEntities db = new OcdlogisticsEntities())
            {
                OcdlogisticsSolution.DomainModels.Models.Entity_Models.Tbl_Sponser Oldobj = db.Tbl_Sponser.FirstOrDefault(x => x.Id == id);
                db.Tbl_Sponser.Remove(Oldobj);

                await db.SaveChangesAsync();

            }
            return RedirectToAction("Index", "Sponser", new { area = "Admin" });
        }

    }
}