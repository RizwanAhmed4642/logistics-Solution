using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Models;


namespace OcdlogisticsSolution.Web.Controllers
{
    public class HomeController : Controller
    {
        OcdlogisticsEntities _entities;
        public HomeController()
        {
            _entities = new OcdlogisticsEntities();
        }
    
        public ActionResult Index()
        {
            return View();
        }
        //Home Controller
      
        public ActionResult Distribution()
        {
            var services = _entities.tbl_DistributionServices;
            return View(services);
        }

        public ActionResult Refundpolicy()
        {
            Tbl_RefundPolicy objabout = _entities.Tbl_RefundPolicy.FirstOrDefault();
            return View(objabout);
        }
        public ActionResult TurmsConditions()
        {
            TermsConditions objabout = _entities.TermsConditions.FirstOrDefault();
            return View(objabout);
        }
        public ActionResult PrivacyPolicy()
        {
            Tbl_PrivacyPolicy objabout = _entities.Tbl_PrivacyPolicy.FirstOrDefault();
            return View(objabout);
        }
        public ActionResult FindF()
        {
            //Tbl_FindF findf = _entities.Tbl_FindF.FirstOrDefault();
            return Redirect("http://logistics.ocdtechsolutions.com/ocd/find-f.html");
        }
        public async Task<ActionResult> Packages(string id)
        {
            var service = await _entities.tbl_DistributionServices.FindAsync(id);
            if (service != null)
            {
                ViewBag.ServiceId = id;
                return View(service.tbl_Distribution_Packages_Info);
            }
            else
            {
                return RedirectToAction("Distribution");
            }
        }
        public PartialViewResult OurMission()
        {
            Tbl_Misson SliderList = _entities.Tbl_Misson.FirstOrDefault();
            return PartialView(SliderList);
            //return PartialView();

        }
     
        public PartialViewResult OurSolution()
        {
            List<Tbl_OurSolution> SliderList = _entities.Tbl_OurSolution.ToList();
            return PartialView(SliderList);
         
        }
        public PartialViewResult slider()
        {
            List<tbl_Slider> SliderList = _entities.tbl_Slider.Where(x => x.Isdeleted == false & x.Isactive == true).ToList();
            return PartialView(SliderList);
        }
        public PartialViewResult Sponser()
        {
            List<Tbl_Sponser> SliderList = _entities.Tbl_Sponser.ToList();
            return PartialView(SliderList);
        }
        public PartialViewResult SponserEducation()
        {
            List<Tbl_SponserEducation> SliderList = _entities.Tbl_SponserEducation.ToList();
            return PartialView(SliderList);
        }

        public ActionResult Partner()
        {
            tbl_PartnerCms objabout = _entities.tbl_PartnerCms.FirstOrDefault();
            return View(objabout);
        }
        public ActionResult About()
        {
            tbl_AboutUs objabout = _entities.tbl_AboutUs.FirstOrDefault();
            return View(objabout);
        }
 
        public ActionResult Login()
        {

            return View();
        }
        
        public ActionResult Contact()
        {
           Tbl_ContactUs obj= _entities.Tbl_ContactUs.FirstOrDefault();
            ViewBag.Message = "Your contact page.";

            return View(obj);
        }
        public ActionResult Ourteam()
        {
            OurTeamPageViewModel _OurTeamPageViewModel = new OurTeamPageViewModel
            {
                tbl_TeamProfilesList = _entities.tbl_TeamProfiles.Where(x => x.IsDeleted == false & x.IsOnDisplay == true).ToList(),
                tbl_OurTeamPage = _entities.tbl_OurTeamPage.FirstOrDefault()
            };
            return View(_OurTeamPageViewModel);
           // return View();
        }
       
        public new ActionResult Profile(int profId)
        {
            ProfilePageViewModel profilePageViewModel = new ProfilePageViewModel
            {

                tbl_TeamProfiles = _entities.tbl_TeamProfiles.FirstOrDefault(x => x.ProfileId == profId),
                tbl_ProfilePage = _entities.tbl_ProfilePage.FirstOrDefault()
            };
            return View(profilePageViewModel);
           
        }

        public PartialViewResult SocialLinksHeader()
        {
            tbl_SocialHandler obj = _entities.tbl_SocialHandler.FirstOrDefault();
            return PartialView(obj);
        }
        public PartialViewResult SocialLinksFooter()
        {
            tbl_SocialHandler obj = _entities.tbl_SocialHandler.FirstOrDefault();
            return PartialView(obj);
        }
        [ChildActionOnly]
        public ActionResult _FAQ_Questions(int Id)
        {
            var faqs = _entities.tbl_FAQ.Find(Id);
            if (faqs == null)
                return null;
            return PartialView(faqs.tbl_FAQ_Questions);
        }
        public ActionResult Product()
        {
            return View();
        }
        public ActionResult Education()
        {
            return View();
        }
        public PartialViewResult EduBanne()
        {
            Tbl_EducationBanner obj = _entities.Tbl_EducationBanner.FirstOrDefault();
            return PartialView(obj);
           // return PartialView();
        }
        

        [ChildActionOnly]
        public ActionResult HomePage_Fetures_Benifits(Common.ViewModels.Pages page = Common.ViewModels.Pages.Home)
        {
            var feature_Benifites = _entities.tbl_Home_Page_Feature_Benifites.Where(x => (Common.ViewModels.Pages)x.Type == page).ToList();
            return PartialView(feature_Benifites);
        }

        [ChildActionOnly]
        public ActionResult HomePage_Packages()
        {
            var homePage_Packages = _entities.tbl_HomePage_Packages.ToList();
            return PartialView(homePage_Packages);
        }

        [ChildActionOnly]
        public ActionResult _FAQ(Common.ViewModels.Pages page = Common.ViewModels.Pages.Home)
        {
            var faqs = _entities.tbl_FAQ.Where(x => (Common.ViewModels.Pages)x.Type == page).ToList();
            return PartialView(faqs);
        }

    }
}