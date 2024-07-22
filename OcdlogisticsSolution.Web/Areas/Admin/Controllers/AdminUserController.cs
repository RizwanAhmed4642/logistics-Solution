using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OcdlogisticsSolution.Common.ViewModels.GlobalSetingsModel;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Models;
using OcdlogisticsSolution.Web.Models.Authorization;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    [Authorize]
    public class AdminUserController : AuthenticatedControllerEx
    {


        // GET: AdminDashboard/AdminUser
        public ActionResult CreateUser()
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AlllowToCreateUser))
            {
                ViewBag.roleList = new SelectList(OcdlogisticsEntities.AspNetRoles.Where(x => x.Id != GlobalSettingsConst.ADMIN_ID && x.IsDeleted != true), "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserViewModel createUserViewModel)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AlllowToCreateUser))
            {
                ViewBag.roleList = new SelectList(OcdlogisticsEntities.AspNetRoles.Where(x => x.Id != GlobalSettingsConst.ADMIN_ID && x.IsDeleted != true), "Id", "Name");
                if (ModelState.IsValid && createUserViewModel != null)
                {
                    var applicationuser = new ApplicationUser
                    {
                        FirstName = createUserViewModel.FirstName,
                        LastName = createUserViewModel.LastName,
                        Email = createUserViewModel.Email,
                        UserName = createUserViewModel.Email,
                        Id = Guid.NewGuid().ToString()
                    };

                    List<IdentityUserRole> ValidRoles = new List<IdentityUserRole>();
                    foreach (var item in createUserViewModel.RoleIds)
                    {
                        var role = OcdlogisticsEntities.AspNetRoles.Find(item);
                        if (role != null)
                        {
                            applicationuser.Roles.Add(new IdentityUserRole
                            {
                                RoleId = item,
                                UserId = applicationuser.Id
                            });
                        }
                    }
                    try
                    {
                        var result = await UserManager.CreateAsync(applicationuser, createUserViewModel.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("AdminUserList");
                        }
                        else
                        {

                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item);
                            }
                            return View(createUserViewModel);
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var item in ex.EntityValidationErrors)
                        {
                            foreach (var error in item.ValidationErrors)
                            {
                                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                            }
                        }
                        return View(createUserViewModel);
                    }
                }
                else
                {
                    return View(createUserViewModel);
                }
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }

        }

        public ActionResult EditUser(string userId)
        {
            ViewBag.roleList = new SelectList(OcdlogisticsEntities.AspNetRoles.Where(x => x.Id != GlobalSettingsConst.ADMIN_ID && x.IsDeleted != true), "Id", "Name");
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToUpdateUser))
            {
                var user = OcdlogisticsEntities.AspNetUsers.Find(userId);
                if (user == null)
                    return RedirectToAction("Error", "Error", new { statusCode = HttpStatusCode.NotFound });

                EditViewModel editViewModel = new EditViewModel
                {
                    Id = userId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    RoleIds = user.AspNetUserRoles.Select(x => x.RoleId).ToList()
                };
                return View(editViewModel);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }

        }
        [HttpPost]
        public async Task<ActionResult> EditUser(EditViewModel user)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToUpdateUser))
            {
                ViewBag.roleList = new SelectList(OcdlogisticsEntities.AspNetRoles.Where(x => x.Id != GlobalSettingsConst.ADMIN_ID && x.IsDeleted != true), "Id", "Name");

                if (ModelState.IsValid && user != null)
                {
                    using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                    {
                        AspNetUsers olduser = db.AspNetUsers.Find(user.Id);
                        olduser.FirstName = user.FirstName;
                        olduser.LastName = user.LastName;
                        List<AspNetUserRoles> ValidRoles = new List<AspNetUserRoles>();

                        foreach (var item in user.RoleIds)
                        {
                            var role = db.AspNetRoles.Find(item);
                            if (role != null)
                            {
                                ValidRoles.Add(new AspNetUserRoles
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    AspNetRoles = role,
                                    UserId = olduser.Id
                                });
                            }
                        }

                        olduser.AspNetUserRoles = ValidRoles;

                        await db.SaveChangesAsync();
                    }
                }
                else
                {
                    return View(user);
                }
                return RedirectToAction("AdminUserList");
            }

            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToDeleteUser))
            {

                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    AspNetUsers olduser = db.AspNetUsers.Find(userId);
                    if (olduser == null)
                        return RedirectToAction("Error", "Error", new { statusCode = HttpStatusCode.NotFound });

                    olduser.IsDelete = true;
                    await db.SaveChangesAsync();
                }

                return RedirectToAction("AdminUserList");
            }

            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }

        public ActionResult AdminUserList()
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.AllowToSeeUserList))
            {
                return View(OcdlogisticsEntities.AspNetUsers.Where(x => (x.IsDelete == null || x.IsDelete == false) && x.Id != UserId));
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
        //public PartialViewResult List()
        //{
        //    return PartialView("_List");
        //}
        //public JsonResult GetList()
        //{
        //    List<AspNetUsers> list = _IdentityUserHandler.List.ToList();
        //    JQueryDataTableParamModel jqObj = DataTablesFilter.SetDataTablesFilter(Request);

        //    if (jqObj.iDisplayLength != 0)
        //    {
        //        IQueryable<AspNetUsers> filteredRecords;
        //        IQueryable<AspNetUsers> allRecords = list.AsQueryable();

        //        if (!string.IsNullOrEmpty(jqObj.sSearch))
        //        {

        //            filteredRecords = allRecords.Where(c =>
        //            c.UserName.ToLower().ToString().Contains(jqObj.sSearch.ToLower())||
        //            c.Email.ToLower().ToString().Contains(jqObj.sSearch.ToLower())||
        //            c.EmailConfirmed.ToString().Contains(jqObj.sSearch.ToLower())

        //            );
        //        }
        //        else
        //        {
        //            filteredRecords = allRecords.AsQueryable();
        //        }

        //        var sortColumnIndex = Convert.ToInt32(Request.Params["iSortCol_0"]);

        //        Func<AspNetUsers, string> orderingFunction = (c => sortColumnIndex == 0 ? c.UserName.ToString() : sortColumnIndex == 1 ? c.Email.ToString() : sortColumnIndex == 2 ? c.EmailConfirmed.ToString() : "");

        //        var sortDirection = Request.Params["sSortDir_0"]; // asc or desc
        //        if (sortDirection == "asc")
        //            filteredRecords = filteredRecords.OrderBy(orderingFunction).AsQueryable();
        //        else
        //            filteredRecords = filteredRecords.OrderByDescending(orderingFunction).AsQueryable();

        //        var displayedList = filteredRecords.Skip(jqObj.iDisplayStart).Take(jqObj.iDisplayLength)
        //            .Select(s => new
        //            {
        //                s.Id,
        //                s.UserName,
        //                s.Email,
        //                EmailConfirmed=s.EmailConfirmed == true ? "Confirmed" : "Not Confirmed"
        //            }).ToList();

        //        var Result = new
        //        {
        //            sEcho = jqObj.sEcho,
        //            iTotalRecords = allRecords.Count(),
        //            iTotalDisplayRecords = filteredRecords.Count(),
        //            aaData = displayedList
        //        };

        //        return Json(Result, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //        return null;

        //}
        //public ActionResult Delete(int id)
        //{
        //    tbl_EventType Oldobj = _EventTypeHandler.List.FirstOrDefault(x => x.EventTypeId == id);
        //    Oldobj.IsDeleted = true;
        //    _EventTypeHandler.update(Oldobj);

        //    return RedirectToAction("CreateEventType");
        //}
    }
}