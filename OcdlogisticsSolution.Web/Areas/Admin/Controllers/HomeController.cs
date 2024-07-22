using OcdlogisticsSolution.Business;
using OcdlogisticsSolution.Common.ViewModels;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Models;
using OcdlogisticsSolution.Web.Models.Authorization;
using OcdlogisticsSolution.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    [Authorize]
    //[RoutePrefix("Home")]
    public class HomeController : AuthenticatedControllerEx
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddFeature_Benifits()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddFeature_Benifits(HomePage_Feature_Benifits_ViewModel feature_Benifites)
        {
            if (feature_Benifites.file == null)
            {
                ModelState.AddModelError("file", "Please select icon");
                return View(feature_Benifites);
            }

            if (ModelState.IsValid)
            {
                tbl_Home_Page_Feature_Benifites benifites = new tbl_Home_Page_Feature_Benifites()
                {
                    Heading = feature_Benifites.Heading,
                    Content = feature_Benifites.Content,
                    Icon = FileManager.SaveImage(feature_Benifites.file),
                    CreateDate = DateTime.Now
                };
                await Database.Entity<tbl_Home_Page_Feature_Benifites>.AddAsync(benifites);
                return RedirectToAction("List_Features_Benefits");
            }

            return View(feature_Benifites);
        }

        public ActionResult EditFeature_Benifits(int id)
        {
            tbl_Home_Page_Feature_Benifites feature_Benifites = OcdlogisticsEntities.tbl_Home_Page_Feature_Benifites.Find(id);
            if (feature_Benifites == null)
            {
                return RedirectToAction("featureBenifitesList");
            }

            HomePage_Feature_Benifits_ViewModel homePage_Feature_Benifits_ViewModel = new HomePage_Feature_Benifits_ViewModel()
            {
                Content = feature_Benifites.Content,
                Heading = feature_Benifites.Heading,
                IconPathName = feature_Benifites.Icon,
                Id = feature_Benifites.ID
            };

            return View(homePage_Feature_Benifits_ViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditFeature_Benifits(HomePage_Feature_Benifits_ViewModel feature_Benifites)
        {
            tbl_Home_Page_Feature_Benifites tbl_feature_Benifites = null;
            if ((tbl_feature_Benifites = OcdlogisticsEntities.tbl_Home_Page_Feature_Benifites.Find(feature_Benifites.Id)) == null)
            {
                return RedirectToAction("List_Features_Benefits");
            }



            if (ModelState.IsValid)
            {
                tbl_Home_Page_Feature_Benifites benifites = new tbl_Home_Page_Feature_Benifites()
                {
                    ID = feature_Benifites.Id,
                    Heading = feature_Benifites.Heading,
                    Content = feature_Benifites.Content,
                    CreateDate = DateTime.Now,
                    Icon = feature_Benifites.IconPathName
                };

                if (feature_Benifites.file != null)
                {
                    benifites.Icon = FileManager.SaveImage(feature_Benifites.file);
                }
                else
                {
                    benifites.Icon = tbl_feature_Benifites.Icon;
                }

                await Database.Entity<tbl_Home_Page_Feature_Benifites>.UpdateAsync(benifites);
                return RedirectToAction("List_Features_Benefits");
            }

            return View(feature_Benifites);
        }

        public ActionResult HomePage_PackagesList()
        {
            return View();
        }

        public ActionResult AddHomePage_Package()
        {
            ViewBag.DurationType = new SelectList(OcdlogisticsEntities.tbl_Duration, "Id", "Name");
            return View();
        }

        public ActionResult AddFAQ()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddFAQ(FAQViewModel fAQViewModel)
        {
            if (ModelState.IsValid)
            {
                tbl_FAQ tbl_FAQ = new tbl_FAQ()
                {
                    HeadingName = fAQViewModel.Heading
                };

                await Database.Entity<tbl_FAQ>.AddAsync(tbl_FAQ);
                return RedirectToAction("ListFaq");
            }
            return View(fAQViewModel);
        }

        public ActionResult AddFAQ_Question(int id)
        {
            FAQ_QuestionViewModel fAQ_QuestionViewModel = new FAQ_QuestionViewModel();
            fAQ_QuestionViewModel.FAQId = id;
            return View(fAQ_QuestionViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddFAQ_Question(FAQ_QuestionViewModel fAQViewModel)
        {

            if (OcdlogisticsEntities.tbl_FAQ.Find(fAQViewModel.FAQId) == null)
            {
                return RedirectToAction("ListFaq");
            }

            if (ModelState.IsValid)
            {
                tbl_FAQ_Questions tbl_FAQ = new tbl_FAQ_Questions()
                {
                    Question = fAQViewModel.Question,
                    Answer = fAQViewModel.Answer,
                    FAQ_Id = fAQViewModel.FAQId
                };

                await Database.Entity<tbl_FAQ_Questions>.AddAsync(tbl_FAQ);
                return RedirectToAction("ListFaqQues", new { id = fAQViewModel.FAQId });
            }
            return View(fAQViewModel);
        }
        public ActionResult EditFAQ(int id)
        {
            var faq = OcdlogisticsEntities.tbl_FAQ.Find(id);
            if (faq == null)
            {
                return RedirectToAction("ListFaq");
            }
            FAQViewModel fAQView = new FAQViewModel()
            {
                Heading = faq.HeadingName,
                Id = faq.Id
            };
            return View(fAQView);
        }

        [HttpPost]
        public async Task<ActionResult> EditFAQ(FAQViewModel fAQViewModel)
        {
            if (ModelState.IsValid)
            {
                tbl_FAQ tbl_FAQ = new tbl_FAQ()
                {
                    Id = fAQViewModel.Id,
                    HeadingName = fAQViewModel.Heading
                };

                await Database.Entity<tbl_FAQ>.UpdateAsync(tbl_FAQ);
                return RedirectToAction("ListFaq");
            }
            return View(fAQViewModel);
        }

        public ActionResult EditFAQ_Question(int id)
        {
            var question = OcdlogisticsEntities.tbl_FAQ_Questions.Find(id);
            if (question == null)
            {
                return RedirectToAction("ListFaq");
            }

            FAQ_QuestionViewModel fAQ_QuestionViewModel = new FAQ_QuestionViewModel()
            {
                Id = question.Id,
                Answer = question.Answer,
                FAQId = question.FAQ_Id,
                Question = question.Question
            };
            return View(fAQ_QuestionViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditFAQ_Question(FAQ_QuestionViewModel fAQViewModel)
        {
            if (ModelState.IsValid)
            {
                tbl_FAQ_Questions tbl_FAQ = new tbl_FAQ_Questions()
                {
                    Id = fAQViewModel.Id,
                    Question = fAQViewModel.Question,
                    Answer = fAQViewModel.Answer,
                    FAQ_Id = fAQViewModel.FAQId
                };

                await Database.Entity<tbl_FAQ_Questions>.UpdateAsync(tbl_FAQ);
                return RedirectToAction("ListFaqQues", new { id = fAQViewModel.FAQId });
            }
            return View(fAQViewModel);
        }


        [HttpPost]
        public async Task<ActionResult> AddHomePage_Package(HomePagePackagesViewModel feature_Benifites)
        {
            if (feature_Benifites.file == null)
            {
                ModelState.AddModelError("file", "Please select icon");
                ViewBag.DurationType = new SelectList(OcdlogisticsEntities.tbl_Duration, "Id", "Name");
                return View(feature_Benifites);
            }

            if (ModelState.IsValid)
            {
                tbl_HomePage_Packages benifites = new tbl_HomePage_Packages()
                {
                    Name = feature_Benifites.Name,
                    Cost = feature_Benifites.Cost,
                    DurationType = feature_Benifites.DurationType,
                    Icon = FileManager.SaveImage(feature_Benifites.file),
                };
                await Database.Entity<tbl_HomePage_Packages>.AddAsync(benifites);
                return RedirectToAction("HomePage_PackagesList");
            }
            ViewBag.DurationType = new SelectList(OcdlogisticsEntities.tbl_Duration, "Id", "Name");
            return View(feature_Benifites);
        }

        public ActionResult EditAddHomePage_Package(int id)
        {
            tbl_HomePage_Packages feature_Benifites = OcdlogisticsEntities.tbl_HomePage_Packages.Find(id);
            if (feature_Benifites == null)
            {
                return RedirectToAction("HomePage_PackagesList");
            }

            HomePagePackagesViewModel homePage_Feature_Benifits_ViewModel = new HomePagePackagesViewModel()
            {
                Name = feature_Benifites.Name,
                Cost = feature_Benifites.Cost,
                DurationType = feature_Benifites.DurationType,
                Icon = feature_Benifites.Icon,
                Id = feature_Benifites.Id,
                 link = feature_Benifites.link
            };
            ViewBag.DurationType = new SelectList(OcdlogisticsEntities.tbl_Duration, "Id", "Name");
            return View(homePage_Feature_Benifits_ViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditAddHomePage_Package(HomePagePackagesViewModel feature_Benifites)
        {
            tbl_HomePage_Packages tbl_feature_Benifites = null;
            if ((tbl_feature_Benifites = OcdlogisticsEntities.tbl_HomePage_Packages.Find(feature_Benifites.Id)) == null)
            {
                return RedirectToAction("HomePage_PackagesList");
            }



            if (ModelState.IsValid)
            {
                tbl_HomePage_Packages benifites = new tbl_HomePage_Packages()
                {
                    Id = feature_Benifites.Id,
                    Name = feature_Benifites.Name,
                    Cost = feature_Benifites.Cost,
                    DurationType = feature_Benifites.DurationType,
                    Icon = feature_Benifites.Icon,
                    link = feature_Benifites.link
                };

                if (feature_Benifites.file != null)
                {
                    benifites.Icon = FileManager.SaveImage(feature_Benifites.file);
                }
                else
                {
                    benifites.Icon = tbl_feature_Benifites.Icon;
                }

                await Database.Entity<tbl_HomePage_Packages>.UpdateAsync(benifites);
                return RedirectToAction("HomePage_PackagesList");
            }
            ViewBag.DurationType = new SelectList(OcdlogisticsEntities.tbl_Duration, "Id", "Name");
            return View(feature_Benifites);
        }




        //[Route("FAQ")]
        public ActionResult ListFaq()
        {
            return View();
        }

        //[Route("FAQ/{id:int}/{Questions}")]
        public ActionResult ListFaqQues(int Id)
        {
            var faq = OcdlogisticsEntities.tbl_FAQ.Find(Id);
            if (faq == null)
            {
                return RedirectToAction("ListFaq");
            }

            ViewBag.Id = Id;

            return View();
        }

        public ActionResult List_Features_Benefits()
        {
            return View();
        }


        public ActionResult Delete_FAQ_Question(int Id, int faqId)
        {
            var faq = OcdlogisticsEntities.tbl_FAQ_Questions.Find(Id);
            if (faq != null)
            {
                //faq.tbl_FAQ_Questions.ToList().ForEach(x => OcdlogisticsEntities.tbl_FAQ_Questions.Remove(x));
                OcdlogisticsEntities.tbl_FAQ_Questions.Remove(faq);
            }
            OcdlogisticsEntities.SaveChanges();
            return RedirectToAction("ListFaqQues", new { id = faqId });
        }

        public ActionResult Delete_FAQ(int Id)
        {
            var faq = OcdlogisticsEntities.tbl_FAQ.Find(Id);
            if (faq != null)
            {
                faq.tbl_FAQ_Questions.ToList().ForEach(x => OcdlogisticsEntities.tbl_FAQ_Questions.Remove(x));
                OcdlogisticsEntities.tbl_FAQ.Remove(faq);
            }
            OcdlogisticsEntities.SaveChanges();
            return RedirectToAction("ListFaq");
        }
        public ActionResult Delete_Home_Packages(int Id)
        {
            var package = OcdlogisticsEntities.tbl_HomePage_Packages.Find(Id);
            if (package != null)
            {
                OcdlogisticsEntities.tbl_HomePage_Packages.Remove(package);
            }
            OcdlogisticsEntities.SaveChanges();
            return RedirectToAction("HomePage_PackagesList");
        }

        public ActionResult Delete_Feature_Benefit(int Id)
        {
            var package = OcdlogisticsEntities.tbl_Home_Page_Feature_Benifites.Find(Id);
            if (package != null)
            {
                OcdlogisticsEntities.tbl_Home_Page_Feature_Benifites.Remove(package);
            }
            OcdlogisticsEntities.SaveChanges();
            return RedirectToAction("List_Features_Benefits");
        }

       


        public JsonResult GetListFaq()
        {
            List<tbl_FAQ> list = OcdlogisticsEntities.tbl_FAQ.ToList();
            JQueryDataTableParamModel jqObj = DataTablesFilter.SetDataTablesFilter(Request);
            if (jqObj.iDisplayLength != 0)
            {
                IQueryable<tbl_FAQ> filteredRecords;
                IQueryable<tbl_FAQ> allRecords = list.AsQueryable();
                //Check whether the companies should be filtered by keyword

                if (!string.IsNullOrEmpty(jqObj.sSearch))
                {

                    filteredRecords = allRecords.Where(c =>
                    c.HeadingName.ToString().ToString().Contains(jqObj.sSearch.ToLower())
                    ||
                    Enum.GetName(typeof(Pages), c.Type).ToString().ToString().Contains(jqObj.sSearch.ToLower())
                    );
                }
                else
                {
                    filteredRecords = allRecords.AsQueryable();
                }

                var sortColumnIndex = Convert.ToInt32(Request.Params["iSortCol_0"]);

                Func<tbl_FAQ, string> orderingFunction = (c => sortColumnIndex == 1 ? c.HeadingName.ToString() : sortColumnIndex == 2 ? Enum.GetName(typeof(Pages), c.Type).ToString() : "");

                var sortDirection = Request.Params["sSortDir_0"]; // asc or desc
                if (sortDirection == "asc")
                    filteredRecords = filteredRecords.OrderBy(orderingFunction).AsQueryable();
                else
                    filteredRecords = filteredRecords.OrderByDescending(orderingFunction).AsQueryable();

                var displayedList = filteredRecords.Skip(jqObj.iDisplayStart).Take(jqObj.iDisplayLength)
                .Select(s => new
                {
                    s.Id,
                    s.HeadingName,
                    Type = Enum.GetName(typeof(Pages), s.Type)
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

        public JsonResult GetList_Features_Benifits()
        {
            List<tbl_Home_Page_Feature_Benifites> list = OcdlogisticsEntities.tbl_Home_Page_Feature_Benifites.ToList();
            JQueryDataTableParamModel jqObj = DataTablesFilter.SetDataTablesFilter(Request);
            if (jqObj.iDisplayLength != 0)
            {
                IQueryable<tbl_Home_Page_Feature_Benifites> filteredRecords;
                IQueryable<tbl_Home_Page_Feature_Benifites> allRecords = list.AsQueryable();
                //Check whether the companies should be filtered by keyword

                if (!string.IsNullOrEmpty(jqObj.sSearch))
                {

                    filteredRecords = allRecords.Where(c =>
                    c.Content.ToString().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    c.Heading.ToString().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    Enum.GetName(typeof(Pages), c.Type).ToString().Contains(jqObj.sSearch.ToLower())
                    );
                }
                else
                {
                    filteredRecords = allRecords.AsQueryable();
                }

                var sortColumnIndex = Convert.ToInt32(Request.Params["iSortCol_0"]);

                Func<tbl_Home_Page_Feature_Benifites, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Heading.ToString() : sortColumnIndex == 1 ? Enum.GetName(typeof(Pages), c.Type) : "");

                var sortDirection = Request.Params["sSortDir_0"]; // asc or desc
                if (sortDirection == "asc")
                    filteredRecords = filteredRecords.OrderBy(orderingFunction).AsQueryable();
                else
                    filteredRecords = filteredRecords.OrderByDescending(orderingFunction).AsQueryable();

                var displayedList = filteredRecords.Skip(jqObj.iDisplayStart).Take(jqObj.iDisplayLength)
                .Select(s => new
                {
                    s.ID,
                    s.Heading,
                    s.Icon,
                    s.Content,
                    Type = Enum.GetName(typeof(Pages), s.Type)
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

        public JsonResult GetListFaqQues(int id)
        {
            List<tbl_FAQ_Questions> list = OcdlogisticsEntities.tbl_FAQ.Find(id).tbl_FAQ_Questions.ToList();
            JQueryDataTableParamModel jqObj = DataTablesFilter.SetDataTablesFilter(Request);
            if (jqObj.iDisplayLength != 0)
            {
                IQueryable<tbl_FAQ_Questions> filteredRecords;
                IQueryable<tbl_FAQ_Questions> allRecords = list.AsQueryable();
                //Check whether the companies should be filtered by keyword

                if (!string.IsNullOrEmpty(jqObj.sSearch))
                {

                    filteredRecords = allRecords.Where(c =>
                    c.Question.ToString().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    c.Answer.ToString().ToString().Contains(jqObj.sSearch.ToLower())


                    );
                }
                else
                {
                    filteredRecords = allRecords.AsQueryable();
                }

                var sortColumnIndex = Convert.ToInt32(Request.Params["iSortCol_0"]);

                Func<tbl_FAQ_Questions, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Question.ToString() : "");

                var sortDirection = Request.Params["sSortDir_0"]; // asc or desc
                if (sortDirection == "asc")
                    filteredRecords = filteredRecords.OrderBy(orderingFunction).AsQueryable();
                else
                    filteredRecords = filteredRecords.OrderByDescending(orderingFunction).AsQueryable();

                var displayedList = filteredRecords.Skip(jqObj.iDisplayStart).Take(jqObj.iDisplayLength)
                .Select(s => new
                {
                    s.Id,
                    s.Question,
                    s.Answer

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

        public JsonResult GetListHomePagePackagesList()
        {
            List<tbl_HomePage_Packages> list = OcdlogisticsEntities.tbl_HomePage_Packages.ToList();
            JQueryDataTableParamModel jqObj = DataTablesFilter.SetDataTablesFilter(Request);
            if (jqObj.iDisplayLength != 0)
            {
                IQueryable<tbl_HomePage_Packages> filteredRecords;
                IQueryable<tbl_HomePage_Packages> allRecords = list.AsQueryable();
                //Check whether the companies should be filtered by keyword

                if (!string.IsNullOrEmpty(jqObj.sSearch))
                {

                    filteredRecords = allRecords.Where(c =>
                    c.Name.ToString().ToString().Contains(jqObj.sSearch.ToLower())
                    );
                }
                else
                {
                    filteredRecords = allRecords.AsQueryable();
                }

                var sortColumnIndex = Convert.ToInt32(Request.Params["iSortCol_0"]);

                Func<tbl_HomePage_Packages, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name.ToString() : "");

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
                    s.Cost,
                    s.Icon,
                    s.link,
                    Duration = s.tbl_Duration.Name,
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