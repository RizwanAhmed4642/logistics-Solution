using OcdlogisticsSolution.Business;
using OcdlogisticsSolution.Common.ViewModels;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Models;
using OcdlogisticsSolution.Web.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    public class DistributionServicesController : AuthenticatedControllerEx
    {
        // GET: Admin/Distribution
        public ActionResult Index()
        {
            return View();
        }
        //Distribution Service List
        public JsonResult GetList()
        {

            List<OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_DistributionServices> list = OcdlogisticsEntities.tbl_DistributionServices.ToList();
            JQueryDataTableParamModel jqObj = DataTablesFilter.SetDataTablesFilter(Request);

            if (jqObj.iDisplayLength != 0)
            {
                IQueryable<OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_DistributionServices> filteredRecords;
                IQueryable<OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_DistributionServices> allRecords = list.AsQueryable();
                //Check whether the companies should be filtered by keyword

                if (!string.IsNullOrEmpty(jqObj.sSearch))
                {

                    filteredRecords = allRecords.Where(c =>
                    c.Name.ToLower().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    c.ShortText.ToLower().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    c.Id.ToLower().ToString().Contains(jqObj.sSearch.ToLower())|| 
                    c.tbl_Duration.Name.ToLower().ToString().Contains(jqObj.sSearch.ToLower()) 
                   

                    );
                }
                else
                {
                    filteredRecords = allRecords.AsQueryable();
                }

                var sortColumnIndex = Convert.ToInt32(Request.Params["iSortCol_0"]);

                Func<OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_DistributionServices, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name.ToString() : "");

                var sortDirection = Request.Params["sSortDir_0"]; // asc or desc
                if (sortDirection == "asc")
                    filteredRecords = filteredRecords.OrderBy(orderingFunction).AsQueryable();
                else
                    filteredRecords = filteredRecords.OrderByDescending(orderingFunction).AsQueryable();

                var displayedList = filteredRecords.Skip(jqObj.iDisplayStart).Take(jqObj.iDisplayLength)
                    .Select(s => new
                    {
                        s.Id,
                        s.Name,
                        s.ShortText,
                        s.Cost,
                        DurationType=s.tbl_Duration.Name
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

        public ActionResult Create()
        {
            DistributionServicesViewModel viewModel = new DistributionServicesViewModel();
            ViewBag.DurationType = new SelectList(OcdlogisticsEntities.tbl_Duration, "Id", "Name");
            ViewBag.Packages = new SelectList(OcdlogisticsEntities.tbl_Distribution_Packages, "Id", "Label");
            return View(viewModel);
        }

        public ActionResult Edit(string id)
        {
            var service = OcdlogisticsEntities.tbl_DistributionServices.Find(id);
            if (service != null)
            {
                DistributionServicesViewModel viewModel = new DistributionServicesViewModel();
                viewModel.Name = service.Name;
                viewModel.Cost = service.Cost;
                viewModel.Id = id;
                viewModel.ShortText = service.ShortText;
                viewModel.DurationType = service.DurationType;
                foreach (var item in service.tbl_Distribution_Packages_Info)
                {
                    viewModel.Packages.Add(item.PackageId);
                }
                ViewBag.DurationType = new SelectList(OcdlogisticsEntities.tbl_Duration, "Id", "Name");
                ViewBag.Packages = new SelectList(OcdlogisticsEntities.tbl_Distribution_Packages, "Id", "Label");
                ViewBag.Id = id;
                return View("Create", viewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(DistributionServicesViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var service = new tbl_DistributionServices()
                {
                    Id = model.Id,
                    Name = model.Name,
                    ShortText = model.ShortText,
                    Cost = model.Cost,
                    DurationType = model.DurationType,

                };
                List<tbl_Distribution_Packages_Info> packagesInfos = new List<tbl_Distribution_Packages_Info>();

                if (string.IsNullOrWhiteSpace(service.Id))
                {
                    service.Id = Guid.NewGuid().ToString();
                }

                foreach (var item in model.Packages)
                {
                    tbl_Distribution_Packages package = await OcdlogisticsEntities.tbl_Distribution_Packages.FindAsync(item);
                    if (package != null)
                    {

                        packagesInfos.Add(new tbl_Distribution_Packages_Info()
                        {
                            Id = Guid.NewGuid().ToString(),
                            DistributionId = service.Id,
                            PackageId = item
                        });
                    }
                }
                service.tbl_Distribution_Packages_Info = packagesInfos;
                if (!string.IsNullOrWhiteSpace(model.Id))
                {
                    await Database.Entity<tbl_DistributionServices>.UpdateAsync(service, OcdlogisticsEntities);
                }
                else
                {
                    await Database.Entity<tbl_DistributionServices>.AddAsync(service);
                }
                return RedirectToAction("Index");
            }
            ViewBag.DurationType = new SelectList(OcdlogisticsEntities.tbl_Duration, "Id", "Name");
            ViewBag.Packages = new SelectList(OcdlogisticsEntities.tbl_Distribution_Packages, "Id", "Label");

            return View("Create", model);
        }

        public ActionResult CreatePackage(string returnUrl)
        {
            PackageViewModel packageView = new PackageViewModel();
            ViewBag.DurationType = new SelectList(OcdlogisticsEntities.tbl_Duration, "Id", "Name");
            ViewBag.ReturnUrl = returnUrl;
            return View(packageView);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePackage(PackageViewModel packageViewModel, string returnUrl)
        {
            if (packageViewModel != null && ModelState.IsValid)
            {
                var package = new tbl_Distribution_Packages()
                {
                    Cost = packageViewModel.Cost,
                    Name = packageViewModel.Name,
                    DurationType = packageViewModel.DurationType,
                    Label = packageViewModel.Label
                };
                package.Features = packageViewModel.Features;
                package.Id = packageViewModel.Id;

                if (!string.IsNullOrWhiteSpace(packageViewModel.Id))
                {
                    await Database.Entity<tbl_Distribution_Packages>.UpdateAsync(package, OcdlogisticsEntities);
                }
                else
                {
                    package.Id = Guid.NewGuid().ToString();
                    await Database.Entity<tbl_Distribution_Packages>.AddAsync(package);
                }


                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToAction(returnUrl);
                }
                else
                {
                    return RedirectToAction("DistributionPackegeList");
                }
            }

            ViewBag.DurationType = new SelectList(OcdlogisticsEntities.tbl_Duration, "Id", "Name");
            return View(packageViewModel);
        }

        public ActionResult DistributionPackegeList()
        {
            return View();
        }
        //Distribution Packege List
        public JsonResult GetListPack()
        {

            List<OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_Distribution_Packages> list = OcdlogisticsEntities.tbl_Distribution_Packages.ToList();
            JQueryDataTableParamModel jqObj = DataTablesFilter.SetDataTablesFilter(Request);

            if (jqObj.iDisplayLength != 0)
            {
                IQueryable<OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_Distribution_Packages> filteredRecords;
                IQueryable<OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_Distribution_Packages> allRecords = list.AsQueryable();
                //Check whether the companies should be filtered by keyword

                if (!string.IsNullOrEmpty(jqObj.sSearch))
                {

                    filteredRecords = allRecords.Where(c =>
                    c.Name.ToLower().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    c.Cost.ToLower().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    c.Features.ToLower().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    c.tbl_Duration.Name.ToLower().ToString().Contains(jqObj.sSearch.ToLower())||
                    c.Label.ToLower().ToString().Contains(jqObj.sSearch.ToLower())


                    );
                }
                else
                {
                    filteredRecords = allRecords.AsQueryable();
                }

                var sortColumnIndex = Convert.ToInt32(Request.Params["iSortCol_0"]);

                Func<OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_Distribution_Packages, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name.ToString() : "");

                var sortDirection = Request.Params["sSortDir_0"]; // asc or desc
                if (sortDirection == "asc")
                    filteredRecords = filteredRecords.OrderBy(orderingFunction).AsQueryable();
                else
                    filteredRecords = filteredRecords.OrderByDescending(orderingFunction).AsQueryable();

                var displayedList = filteredRecords.Skip(jqObj.iDisplayStart).Take(jqObj.iDisplayLength)
                    .Select(s => new
                    {
                        s.Id,
                        s.Cost,
                        s.Name,
                        s.Features,
                        s.Label,
                        DurationType = s.tbl_Duration.Name
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
     
        public ActionResult EditPackage(string id)
        {
            var service = OcdlogisticsEntities.tbl_Distribution_Packages.Find(id);
            if (service != null)
            {
                PackageViewModel viewModel = new PackageViewModel();
                viewModel.Name = service.Name;
                viewModel.Cost = service.Cost;
                viewModel.Id = id;
                viewModel.Label = service.Label;
                viewModel.DurationType = service.DurationType;
                ViewBag.DurationType = new SelectList(OcdlogisticsEntities.tbl_Duration, "Id", "Name");
                viewModel.Features = service.Features;
                return View("CreatePackage", viewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}