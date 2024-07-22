using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using OcdlogisticsSolution.Web.Models.Authorization;
using OcdlogisticsSolution.Web.Util;
using OcdlogisticsSolution.Web.Models;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using System.Data.Entity.Validation;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    [Authorize]

    public class TeamProfileController : AuthenticatedControllerEx
    {

        public ActionResult CreateTeamProfile()
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.TeamProfileCreate))
            {
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> CreateTeamProfile(OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_TeamProfiles model, HttpPostedFileBase image)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.TeamProfileCreate))
            {
                model.IsDeleted = false;
                if (image != null)
                {
                    model.TeamImage = FileManager.SaveImage(image);
                }
                OcdlogisticsEntities.tbl_TeamProfiles.Add(model);

                try
                {
                    await OcdlogisticsEntities.SaveChangesAsync();
                }
                catch (DbEntityValidationException ex)
                {

                    throw;
                }
               

                return RedirectToAction("TeamProfileList", "TeamProfile", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
        public ActionResult EditTeamProfile(int TeamProfileId)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.TeamProfileEdit))
            {

                OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_TeamProfiles obj = OcdlogisticsEntities.tbl_TeamProfiles.FirstOrDefault(x => x.ProfileId == TeamProfileId);
                return View(obj);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> EditTeamProfile(OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_TeamProfiles model, HttpPostedFileBase image)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.TeamProfileEdit))
            {
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_TeamProfiles Oldobj = db.tbl_TeamProfiles.FirstOrDefault(x => x.ProfileId == model.ProfileId);
                    Oldobj.ContactNumber = model.ContactNumber;
                    Oldobj.Email = model.Email;
                    Oldobj.IsOnDisplay = model.IsOnDisplay;
                    Oldobj.Position = model.Position;
                    Oldobj.ProfileName = model.ProfileName;
                    Oldobj.ShortDescription = model.ShortDescription;
                    Oldobj.Speciality = model.Speciality;
                    Oldobj.Facebook = model.Facebook;
                    Oldobj.Instagram = model.Instagram;
                    Oldobj.Tweeter = model.Tweeter;
                    Oldobj.Pintrestr = model.Pintrestr;
                    Oldobj.Youtube = model.Youtube;
                    Oldobj.LinkedIn = model.LinkedIn;
                    Oldobj.Message = model.Message;
                    Oldobj.Bio = model.Bio;
                    Oldobj.Credentials = model.Credentials;
                    Oldobj.Experience = model.Experience;
                    Oldobj.MyWork = model.MyWork;
                    Oldobj.PassionandHobbies = model.PassionandHobbies;
                    Oldobj.FavoriteQuote = model.FavoriteQuote;
                    if (image != null)
                    {
                        Oldobj.TeamImage = FileManager.SaveImage(image);
                    }
                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (DbEntityValidationException ex)
                    {

                        throw;
                    }
                 
                    return RedirectToAction("TeamProfileList", "TeamProfile", new { area = "Admin" });
                }
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
        public ActionResult Detail(int id)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.TeamProfileList))
            {
                OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_TeamProfiles obj = OcdlogisticsEntities.tbl_TeamProfiles.FirstOrDefault(x => x.ProfileId == id);
                return View(obj);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
        public ActionResult TeamProfileList()
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.TeamProfileList))
            {
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
        public JsonResult GetList()
        {

            List<OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_TeamProfiles> list = OcdlogisticsEntities.tbl_TeamProfiles.Where(x => x.IsDeleted == false).ToList();
            JQueryDataTableParamModel jqObj = DataTablesFilter.SetDataTablesFilter(Request);

            if (jqObj.iDisplayLength != 0)
            {
                IQueryable<OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_TeamProfiles> filteredRecords;
                IQueryable<OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_TeamProfiles> allRecords = list.AsQueryable();
                //Check whether the companies should be filtered by keyword

                if (!string.IsNullOrEmpty(jqObj.sSearch))
                {

                    filteredRecords = allRecords.Where(c =>
                    c.ProfileName.ToLower().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    c.Position.ToLower().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    c.ContactNumber.ToLower().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    c.TeamImage.ToLower().ToString().Contains(jqObj.sSearch.ToLower())

                    );
                }
                else
                {
                    filteredRecords = allRecords.AsQueryable();
                }

                var sortColumnIndex = Convert.ToInt32(Request.Params["iSortCol_0"]);

                Func<OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_TeamProfiles, string> orderingFunction = (c => sortColumnIndex == 0 ? c.ProfileName.ToString() : "");

                var sortDirection = Request.Params["sSortDir_0"]; // asc or desc
                if (sortDirection == "asc")
                    filteredRecords = filteredRecords.OrderBy(orderingFunction).AsQueryable();
                else
                    filteredRecords = filteredRecords.OrderByDescending(orderingFunction).AsQueryable();

                var displayedList = filteredRecords.Skip(jqObj.iDisplayStart).Take(jqObj.iDisplayLength)
                    .Select(s => new
                    {
                        s.ProfileId,
                        s.ProfileName,
                        s.ContactNumber,
                        s.Position,
                        s.TeamImage
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
        public async Task<ActionResult> Delete(int id)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.TeamProfileDelete))
            {

                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_TeamProfiles Oldobj = db.tbl_TeamProfiles.FirstOrDefault(x => x.ProfileId == id);
                    Oldobj.IsDeleted = true;

                    await db.SaveChangesAsync();

                }
                return RedirectToAction("TeamProfileList", "TeamProfile", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
    }
}
