

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using OcdlogisticsSolution.Web.Models.Authorization;
using OcdlogisticsSolution.Web.Admin.Data;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Models;
using OcdlogisticsSolution.Common.ViewModels.GlobalSetingsModel;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    [Authorize]
    public class UserRolesAndRightsController : AuthenticatedControllerEx
    {


        public ActionResult CreateRoleRight()
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToCreateRolesAndRights == true))
            {
                UserRoleRightModel userRoleRightModel = new UserRoleRightModel()
                {
                    AspNetRoles = new OcdlogisticsSolution.DomainModels.Models.Entity_Models.AspNetRoles(),
                    tbl_Rights = new OcdlogisticsSolution.DomainModels.Models.Entity_Models.tblRights()
                };

                return View(userRoleRightModel);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }


        }
        [HttpPost]
        public async Task<ActionResult> CreateRoleRight(UserRoleRightModel model)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToCreateRolesAndRights == true))
            {

                Guid id = Guid.NewGuid();
                model.AspNetRoles.Id = id.ToString();
                AspNetRoles role = model.AspNetRoles;
                role.tblRights = model.tbl_Rights;

                if (model != null && model.AspNetRoles != null)
                {
                    if (string.IsNullOrEmpty(model.AspNetRoles.Name))
                        return View(model);


                    if (OcdlogisticsEntities.AspNetRoles.Where(x => x.Name.ToLower().Contains(model.AspNetRoles.Name.ToLower())).Count() > 0)
                    {
                        return View(model);
                    }



                    OcdlogisticsEntities.AspNetRoles.Add(role);
                    var isAffected = await OcdlogisticsEntities.SaveChangesAsync();
                }


                return RedirectToAction("List");
            }

            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
        public ActionResult EditUserRoleRight(string id)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToUpdateRolesAndRights))
            {


                EditUserRoleRightModel userRoleRightModel = new EditUserRoleRightModel()
                {
                    AspNetRoles = OcdlogisticsEntities.AspNetRoles.FirstOrDefault(x => x.Id == id),
                    tbl_Rights = OcdlogisticsEntities.tblRights.FirstOrDefault(x => x.RoleId == id),
                    Id = id
                };

                return View(userRoleRightModel);
            }

            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditUserRoleRight(EditUserRoleRightModel model)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToUpdateRolesAndRights))
            {
                if (ModelState.IsValid && model != null)
                {

                    using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                    {
                        var role = db.AspNetRoles.Find(model.Id);
                        if (role != null)
                        {
                            role.tblRights.EventsList = model.tbl_Rights.EventsList;
                            role.tblRights.CreateEvent = model.tbl_Rights.CreateEvent;
                            role.tblRights.Delete = model.tbl_Rights.Delete;
                            role.tblRights.EditEvent = model.tbl_Rights.EditEvent;
                            role.tblRights.AllowToSeeUserList = model.tbl_Rights.AllowToSeeUserList;
                            role.tblRights.AlllowToCreateUser = model.tbl_Rights.AlllowToCreateUser;
                            role.tblRights.AllowToDeleteUser = model.tbl_Rights.AllowToDeleteUser;
                            role.tblRights.AllowToUpdateUser = model.tbl_Rights.AllowToUpdateUser;
                            role.tblRights.AllowToSeeEventTypeList = model.tbl_Rights.AllowToSeeEventTypeList;
                            role.tblRights.AlllowToCreateEventType = model.tbl_Rights.AlllowToCreateEventType;
                            role.tblRights.AllowToDeleteEventType = model.tbl_Rights.AllowToDeleteEventType;
                            role.tblRights.AllowToUpdateEventType = model.tbl_Rights.AllowToUpdateEventType;
                            role.tblRights.MailList = model.tbl_Rights.MailList;
                            role.tblRights.MailCreate = model.tbl_Rights.MailCreate;
                            role.tblRights.MembershipCreate = model.tbl_Rights.MembershipCreate;
                            role.tblRights.MembershipEdit = model.tbl_Rights.MembershipEdit;
                            role.tblRights.MembershipList = model.tbl_Rights.MembershipList;
                            role.tblRights.MembershipDelete = model.tbl_Rights.MembershipDelete;
                            role.tblRights.SocialHandlersEdit = model.tbl_Rights.SocialHandlersEdit;
                            role.tblRights.TeamProfileCreate = model.tbl_Rights.TeamProfileCreate;
                            role.tblRights.TeamProfileEdit = model.tbl_Rights.TeamProfileEdit;
                            role.tblRights.TeamProfileList = model.tbl_Rights.TeamProfileList;
                            role.tblRights.TeamProfileDelete = model.tbl_Rights.TeamProfileDelete;
                            role.tblRights.AllowToCreateRolesAndRights = model.tbl_Rights.AllowToCreateRolesAndRights;
                            role.tblRights.AllowToUpdateRolesAndRights = model.tbl_Rights.AllowToUpdateRolesAndRights;
                            role.tblRights.AllowToDeleteRolesAndRights = model.tbl_Rights.AllowToDeleteRolesAndRights;
                            role.tblRights.AllowToListRolesAndRights = model.tbl_Rights.AllowToListRolesAndRights;

                            role.Name = model.AspNetRoles.Name;
                        }

                        await db.SaveChangesAsync();
                    }
                    return RedirectToAction("List");
                }
                else
                {
                    return View(model);
                }
            }

            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteRole(string id)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToDeleteRolesAndRights))
            {
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    var role = db.AspNetRoles.Find(id);
                    if (role != null)
                    {
                        role.IsDeleted = true;
                    }
                    var roles = role.AspNetUserRoles.ToList();
                    foreach (var item in roles)
                    {
                        db.AspNetUserRoles.Remove(item);
                    }

                    await db.SaveChangesAsync();
                }

                return RedirectToAction("List");
            }

            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }

        }



        public ActionResult List()
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToListRolesAndRights))
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

            List<AspNetRoles> list = OcdlogisticsEntities.AspNetRoles.Where(x => (x.IsDeleted == null || x.IsDeleted == false) && x.Id != GlobalSettingsConst.ADMIN_ID).ToList();

            JQueryDataTableParamModel jqObj = DataTablesFilter.SetDataTablesFilter(Request);

            if (jqObj.iDisplayLength != 0)
            {
                IQueryable<AspNetRoles> filteredRecords;
                IQueryable<AspNetRoles> allRecords = list.AsQueryable();
                //Check whether the companies should be filtered by keyword

                if (!string.IsNullOrEmpty(jqObj.sSearch))
                {

                    filteredRecords = allRecords.Where(c =>
                    c.Name.ToLower().ToString().Contains(jqObj.sSearch.ToLower())

                    );
                }
                else
                {
                    filteredRecords = allRecords.AsQueryable();
                }

                var sortColumnIndex = Convert.ToInt32(Request.Params["iSortCol_0"]);

                Func<AspNetRoles, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name.ToString() : "");

                var sortDirection = Request.Params["sSortDir_0"]; // asc or desc
                if (sortDirection == "asc")
                    filteredRecords = filteredRecords.OrderBy(orderingFunction).AsQueryable();
                else
                    filteredRecords = filteredRecords.OrderByDescending(orderingFunction).AsQueryable();

                var displayedList = filteredRecords.Skip(jqObj.iDisplayStart).Take(jqObj.iDisplayLength)
                    .Select(s => new
                    {
                        s.Id,
                        s.Name
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
    }
}