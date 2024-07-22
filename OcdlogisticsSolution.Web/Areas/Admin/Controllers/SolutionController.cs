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
    public class SolutionController : Controller
    {
        OcdlogisticsEntities context;
        public SolutionController()
        {
            context = new OcdlogisticsEntities();
        }

        // GET: Admin/Solution
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSolution()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSolution(Tbl_OurSolution model, HttpPostedFileBase image, HttpPostedFileBase image1)
        {

         
            if (image != null)
            {
                model.Img = FileManager.SaveImage(image);
            }
            if (image1 != null)
            {
                model.Logo = FileManager.SaveImage(image1);
            }
           
            context.Tbl_OurSolution.Add(model);
            context.SaveChanges();

            return RedirectToAction("Index", "Solution", new { area = "Admin" });
        }
           public JsonResult GetList()
        {
            List<Tbl_OurSolution> list = context.Tbl_OurSolution.ToList();
            JQueryDataTableParamModel jqObj = DataTablesFilter.SetDataTablesFilter(Request);
            if (jqObj.iDisplayLength != 0)
            {
                IQueryable<Tbl_OurSolution> filteredRecords;
                IQueryable<Tbl_OurSolution> allRecords = list.AsQueryable();
                //Check whether the companies should be filtered by keyword

                if (!string.IsNullOrEmpty(jqObj.sSearch))
                {

                    filteredRecords = allRecords.Where(c =>
                    c.ButtonText.ToLower().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    c.ImgText.ToString().Contains(jqObj.sSearch.ToLower())
                    );
                }
                else
                {
                    filteredRecords = allRecords.AsQueryable();
                }

                var sortColumnIndex = Convert.ToInt32(Request.Params["iSortCol_0"]);

                Func<Tbl_OurSolution, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Id.ToString() : "");

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
                        s.ImgText,
                        s.Logo,
                        s.ButtonText,
                        s.ButtonUrl,
                        s.ButtonColor
                        

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
        public PartialViewResult EditSolution(int Id)
        {
            Tbl_OurSolution obj = context.Tbl_OurSolution.Find(Id);
            return PartialView(obj);
        }
        [HttpPost]
        public async Task<ActionResult> EditSolution(Tbl_OurSolution model, HttpPostedFileBase image, HttpPostedFileBase img1)
        {
           
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                Tbl_OurSolution oldobj = db.Tbl_OurSolution.FirstOrDefault(x => x.Id == model.Id);
                    if (oldobj != null)
                    {
                       
                        oldobj.ButtonColor = model.ButtonColor;
                        oldobj.ButtonText = model.ButtonText;
                        oldobj.ButtonUrl = model.ButtonUrl;
                        oldobj.ImgText = model.ImgText;
                    
                        if (image != null)
                        {
                            oldobj.Img = FileManager.SaveImage(image);
                        }
                    if (img1 != null)
                    {
                        oldobj.Logo = FileManager.SaveImage(img1);
                    }


                    await db.SaveChangesAsync();
                    }
                }
                return RedirectToAction("Index", "Solution", new { area = "Admin" });
           
        }

        public async Task<ActionResult> Delete(int id)
        {
          
                using (OcdlogisticsEntities db = new OcdlogisticsEntities())
                {
                    OcdlogisticsSolution.DomainModels.Models.Entity_Models.Tbl_OurSolution Oldobj = db.Tbl_OurSolution.FirstOrDefault(x => x.Id == id);
                db.Tbl_OurSolution.Remove(Oldobj);

                    await db.SaveChangesAsync();

                }
                return RedirectToAction("Index", "Solution", new { area = "Admin" });
            }
          
        }
    }
