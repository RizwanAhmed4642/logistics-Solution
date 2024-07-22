
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OcdlogisticsSolution.Web.Util;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using OcdlogisticsSolution.Web.Models.Authorization;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Models;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    [Authorize]

    public class MailListController : AuthenticatedControllerEx
    {

        //public ActionResult CreateEvent()
        //{
        //    var eventType = Entities.tbl_EventType.Where(x => x.IsDeleted == false).ToList();
        //    ViewBag.Type = new SelectList(eventType, "EventTypeId", "EventTypeName");
        //    //var eventLocation = _EventLocationHandler.List.ToList();
        //    //ViewBag.Location = new SelectList(eventLocation, "EventLocationId", "EventLocationName");

        //    return View();
        //}
        //[HttpPost, ValidateAntiForgeryToken]
        //public ActionResult CreateEvent(tbl_Event model, HttpPostedFileBase image)
        //{ 
        //    model.IsDeleted = false;
        //    _EventHandler.add(model);
        //    if (image != null)
        //    {
        //        tbl_EventAttachments obj = new tbl_EventAttachments();
        //        obj.PicDetail = FileManager.SaveImage(image);
        //        obj.EventId = model.EventId;
        //        _EventAttatchmantHandler.add(obj);
        //    }
        //    return RedirectToAction("EventsList");
        //}
        //public ActionResult EditEvent(int EventId)
        //{
        //    var eventType = _EventTypeHandler.List.Where(x => x.IsDeleted == false).ToList();
        //    ViewBag.Type = new SelectList(eventType, "EventTypeId", "EventTypeName");
        //    tbl_Event obj = _EveentHandler.List.FirstOrDefault(x => x.EventId == EventId);
        //    return View(obj);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditEvent(tbl_Event model)
        //{
        //    tbl_Event Oldobj = _EveentHandler.List.FirstOrDefault(x => x.EventId == model.EventId);
        //    Oldobj.EndDate = model.EndDate;
        //    Oldobj.EndTime = model.EndTime;
        //    Oldobj.EventDetails = model.EventDetails;
        //    Oldobj.EventLocationName = model.EventLocationName;
        //    Oldobj.EventName = model.EventName;
        //    Oldobj.EventTypeId = model.EventTypeId;
        //    Oldobj.ExtraServices = model.ExtraServices;
        //    Oldobj.FoodDetails = model.FoodDetails;
        //    Oldobj.IsPaid = model.IsPaid;
        //    Oldobj.IsShownOnSite = model.IsShownOnSite;
        //    Oldobj.StartDate = model.StartDate;
        //    Oldobj.StartTime = model.StartTime;
        //    Oldobj.TicketPrice = model.TicketPrice;
        //    Oldobj.TotalPasses = model.TotalPasses;
        //    _EveentHandler.update(Oldobj);
        //    return RedirectToAction("EventsList");
        //}
        //public ActionResult Detail(int id)
        //{
        //    tbl_Event obj = _EveentHandler.List.FirstOrDefault(x => x.EventId == id);


        //    return View(obj);
        //}
        public ActionResult EmailList()
        {
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.MailList))
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

            List<tbl_SubscribeToNewsLatter> list = OcdlogisticsEntities.tbl_SubscribeToNewsLatter.Where(x => x.IsActive == true).ToList();
            JQueryDataTableParamModel jqObj = DataTablesFilter.SetDataTablesFilter(Request);

            if (jqObj.iDisplayLength != 0)
            {
                IQueryable<tbl_SubscribeToNewsLatter> filteredRecords;
                IQueryable<tbl_SubscribeToNewsLatter> allRecords = list.AsQueryable();
                //Check whether the companies should be filtered by keyword

                if (!string.IsNullOrEmpty(jqObj.sSearch))
                {

                    filteredRecords = allRecords.Where(c =>
                    c.email.ToLower().ToString().Contains(jqObj.sSearch.ToLower()) 
                    );
                }
                else
                {
                    filteredRecords = allRecords.AsQueryable();
                }

                var sortColumnIndex = Convert.ToInt32(Request.Params["iSortCol_0"]);

                Func<tbl_SubscribeToNewsLatter, string> orderingFunction = (c => sortColumnIndex == 0 ? c.email.ToString() : "");

                var sortDirection = Request.Params["sSortDir_0"]; // asc or desc
                if (sortDirection == "asc")
                    filteredRecords = filteredRecords.OrderBy(orderingFunction).AsQueryable();
                else
                    filteredRecords = filteredRecords.OrderByDescending(orderingFunction).AsQueryable();

                var displayedList = filteredRecords.Skip(jqObj.iDisplayStart).Take(jqObj.iDisplayLength)
                    .Select(s => new
                    {
                        s.email,
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
        public ActionResult Newsletter()
        {
            
            if (User.IsInRole("Admin") || CurrentUser.AspNetUserRoles.Any(x => x.AspNetRoles.tblRights.MailCreate))
            {
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Home", new { area = "" });
            }
        }
        [HttpPost]
        public async Task<ActionResult> Newsletter(NewsLetterEmailModel model)
        {
            foreach (var item in OcdlogisticsEntities.tbl_SubscribeToNewsLatter.Where(x=>x.IsActive==true))
            {
                IdentityMessage _message = new IdentityMessage();
                _message.Body = model.body;
                _message.Destination = item.email;
                _message.Subject = model.subject;
                await EmailService.SendMailAsync(_message);
            }
            return View();
        }
    }
}
