
using OcdlogisticsSolution.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor;

using System.Threading.Tasks;
using OcdlogisticsSolution.Web.Models.Authorization;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Models;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    [Authorize]
    public class SliderController : AuthenticatedControllerEx
    {
        // GET: AdminDashboard/Event

        public ActionResult Index()
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToUpdateSlider))
            {
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
        // GET: AdminDashboard/Slider
        public PartialViewResult Slider()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<ActionResult> Slider(tbl_Slider model, HttpPostedFileBase image)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToUpdateSlider))
            {
                if (image != null)
                {
                    model.SliderImage = FileManager.SaveImage(image);
                }
                model.SliderId = Guid.NewGuid().ToString();
                model.UserId = CurrentUser.Id;
                model.Isdeleted = false;
                model.Isactive = true;
                OcdlogisticsEntities.tbl_Slider.Add(model);
                await OcdlogisticsEntities.SaveChangesAsync();
                return RedirectToAction("Index", "Slider", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
        public PartialViewResult List()
        {
            return PartialView();
        }
        public JsonResult GetList()
        {
            List<tbl_Slider> list = OcdlogisticsEntities.tbl_Slider.Where(x => x.Isdeleted == false).ToList();
            JQueryDataTableParamModel jqObj = DataTablesFilter.SetDataTablesFilter(Request);
            if (jqObj.iDisplayLength != 0)
            {
                IQueryable<tbl_Slider> filteredRecords;
                IQueryable<tbl_Slider> allRecords = list.AsQueryable();
                //Check whether the companies should be filtered by keyword

                if (!string.IsNullOrEmpty(jqObj.sSearch))
                {

                    filteredRecords = allRecords.Where(c =>
                    c.SliderContent.ToLower().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    c.Isactive.ToString().Contains(jqObj.sSearch.ToLower())
                    );
                }
                else
                {
                    filteredRecords = allRecords.AsQueryable();
                }

                var sortColumnIndex = Convert.ToInt32(Request.Params["iSortCol_0"]);

                Func<tbl_Slider, string> orderingFunction = (c => sortColumnIndex == 0 ? c.SliderHeading.ToString() : "");

                var sortDirection = Request.Params["sSortDir_0"]; // asc or desc
                if (sortDirection == "asc")
                    filteredRecords = filteredRecords.OrderBy(orderingFunction).AsQueryable();
                else
                    filteredRecords = filteredRecords.OrderByDescending(orderingFunction).AsQueryable();

                var displayedList = filteredRecords.Skip(jqObj.iDisplayStart).Take(jqObj.iDisplayLength)
                    .Select(s => new
                    {
                        s.SliderId,
                        s.SliderHeading,
                        Isactive = s.Isactive == true ? "Active" : "Inactive",
                        s.SliderImage
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
        public PartialViewResult EditSlider(string SliderId)
        {
            tbl_Slider obj = OcdlogisticsEntities.tbl_Slider.FirstOrDefault(x => x.Isdeleted == false & x.SliderId == SliderId);
            return PartialView(obj);
        }
        [HttpPost]
        public async Task<ActionResult> EditSlider(tbl_Slider model, HttpPostedFileBase image)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToUpdateSlider))
            {
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    tbl_Slider oldobj = db.tbl_Slider.FirstOrDefault(x => x.SliderId == model.SliderId);
                    if (oldobj != null)
                    {
                        oldobj.UserId = CurrentUser.Id;
                        oldobj.SliderHeading = model.SliderHeading;
                        oldobj.SliderContent = model.SliderContent;
                        if (image != null)
                        {
                            oldobj.SliderImage = FileManager.SaveImage(image);
                        }
                        oldobj.Isactive = model.Isactive;

                        await db.SaveChangesAsync();
                    }
                }
                return RedirectToAction("Index", "Slider", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
        public async Task<ActionResult> Delete(string id)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToUpdateSlider))
            {
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    tbl_Slider oldobj = db.tbl_Slider.FirstOrDefault(x => x.SliderId == id);
                    if (oldobj != null)
                    {
                        oldobj.UserId = CurrentUser.Id;
                        oldobj.Isdeleted = true;
                    }
                    await db.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
    }
}