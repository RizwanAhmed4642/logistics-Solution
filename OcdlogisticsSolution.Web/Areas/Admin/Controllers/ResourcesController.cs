using OcdlogisticsSolution.Common.ViewModels.ResourcesModel;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Models;
using OcdlogisticsSolution.Web.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    [Authorize]
    public class ResourcesController : AuthenticatedControllerEx
    {

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {
            ViewBag.ResourcessTypeId = new SelectList(OcdlogisticsEntities.tbl_ResourcesType, "Id", "Name");

            ResourceViewModel resourceViewModel = new ResourceViewModel();
            return View(resourceViewModel);
        }


        [HttpPost]
        public ActionResult Create(ResourceViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                Session["resourceViewModel"] = model;
                return RedirectToAction("Schedule");
                //using (OcdlogisticsEntities entities = new OcdlogisticsEntities())
                //{
                // entities.tbl_Resources.Add(new tbl_Resources()
                // {
                // Id = Guid.NewGuid().ToString(),
                // Name = resourceViewModel.Name,
                // ResourcessTypeId = resourceViewModel.ResourcessTypeId,
                // IsActive = resourceViewModel.IsActive,
                // UserId = UserId
                // });
                // await entities.SaveChangesAsync();
                //}
            }
            ViewBag.ResourcessTypeId = new SelectList(OcdlogisticsEntities.tbl_ResourcesType, "Id", "Name");
            return View(model);
        }

        public ActionResult Schedule()
        {
            if (Session["resourceViewModel"] == null)
                return RedirectToAction("Create");

            ResourceSheduleViewModel resourceSheduleViewModel = new ResourceSheduleViewModel();
            foreach (var item in OcdlogisticsEntities.tbl_WeekDay.Where(x => x.IsVisible))
            {
                var weekdayViewModel = new WeekdayViewModel(item.Id, item.Name);
                resourceSheduleViewModel.Weekdays.Add(weekdayViewModel);
            }


            return View(resourceSheduleViewModel);
        }

        //[HttpPost]
        //public ActionResult Schedule()
        //{
        //    //if (Session["resourceViewModel"] == null)
        //    // return RedirectToAction("Create");


        //    ResourceSheduleViewModel resourceSheduleViewModel = new ResourceSheduleViewModel();
        //    foreach (var item in OcdlogisticsEntities.tbl_WeekDay.Where(x => x.IsVisible))
        //    {
        //        var weekdayViewModel = new WeekdayViewModel(item.Id, item.Name);
        //        resourceSheduleViewModel.Weekdays.Add(weekdayViewModel);
        //    }

        //    return View(resourceSheduleViewModel);
        //}

        public ActionResult SpecialHours()
        {
            SpecialHoursViewModel model = new SpecialHoursViewModel();
            return View(model);
        }
    }




}