using OcdlogisticsSolution.Business;
using OcdlogisticsSolution.Common.ViewModels;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using OcdlogisticsSolution.Web.Models;
using OcdlogisticsSolution.Web.Models.Authorization;
using OcdlogisticsSolution.Web.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Areas.AdminDashboard.Controllers
{
    [Authorize]
    public class ProductController : AuthenticatedControllerEx
    {
        public ActionResult Index()
        {
            return View();

        }

        public JsonResult GetList()
        {

            List<tbl_Product> list = OcdlogisticsEntities.tbl_Product.ToList();
            JQueryDataTableParamModel jqObj = DataTablesFilter.SetDataTablesFilter(Request);

            if (jqObj.iDisplayLength != 0)
            {
                IQueryable<tbl_Product> filteredRecords;
                IQueryable<tbl_Product> allRecords = list.AsQueryable();
                //Check whether the companies should be filtered by keyword

                if (!string.IsNullOrEmpty(jqObj.sSearch))
                {

                    filteredRecords = allRecords.Where(c =>
                    c.SKU.ToLower().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    c.Name.ToLower().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    c.tbl_Color.Name.ToLower().ToString().Contains(jqObj.sSearch.ToLower()) ||
                    c.tbl_ProductType.Name.ToLower().ToString().Contains(jqObj.sSearch.ToLower())

                    );
                }
                else
                {
                    filteredRecords = allRecords.AsQueryable();
                }

                var sortColumnIndex = Convert.ToInt32(Request.Params["iSortCol_0"]);

                Func<tbl_Product, string> orderingFunction = (c => sortColumnIndex == 0 ? c.Name.ToString() : "");

                var sortDirection = Request.Params["sSortDir_0"]; // asc or desc
                if (sortDirection == "asc")
                    filteredRecords = filteredRecords.OrderBy(orderingFunction).AsQueryable();
                else
                    filteredRecords = filteredRecords.OrderByDescending(orderingFunction).AsQueryable();

                var displayedList = filteredRecords.Skip(jqObj.iDisplayStart).Take(jqObj.iDisplayLength)
                    .Select(s => new
                    {
                        s.Id,
                        s.SKU,
                        ProductName = s.Name,
                        BrandName = s.tblProduct_line_Info.FirstOrDefault().tbl_Brand.Name,
                        Category = s.tbl_ProductType.Name
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

        // GET: Admin/Product
        public ActionResult Create()
        {
            ProductViewModel model = new ProductViewModel();
            Session["Product_Process"] = 1;

            ViewBag.ProductColorId = new SelectList(OcdlogisticsEntities.tbl_Color, "Id", "Name");
            ViewBag.ProductIndustryId = new SelectList(OcdlogisticsEntities.tbl_Industry, "Id", "Name");
            ViewBag.ProductWeightUnitId = new SelectList(OcdlogisticsEntities.tbl_UnitOfWeight, "Id", "Name");
            ViewBag.ProductBrandId = new SelectList(OcdlogisticsEntities.tbl_Brand, "Id", "Name");

            ViewBag.ProductLineId = new SelectList(OcdlogisticsEntities.tbl_Product_Line, "Id", "Name");
            ViewBag.ProductTypeId = new SelectList(OcdlogisticsEntities.tbl_ProductType, "Id", "Name");
            ViewBag.ProductSubCategory = new SelectList(OcdlogisticsEntities.tbl_ProductCategory, "Id", "Name");
            ViewBag.ProductVendors = new SelectList(OcdlogisticsEntities.tbl_Vendors, "Id", "Name");
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public async Task<ActionResult> Create(ProductViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var product = new tbl_Product
                {
                    Id = Guid.NewGuid().ToString(),
                    ShortDescription = model.BriefDescription,
                    FullDescription = model.FullDescription,
                    Name = model.Name,
                    UPC = model.UPC,
                    SKU = model.PrductSkU,
                    Part = model.Part,
                    ProductWeight = model.Weight.ToString(),
                    ProductSizeWeightTypeId = model.ProductWeightUnitId,
                    ProductColorId = model.ProductColorId,
                    Quantity = model.Quantity,
                    TypeId = model.ProductTypeId,
                    SubCategId = model.ProductSubCategory,
                    CreateDate = DateTime.Now
                };


                if(!string.IsNullOrWhiteSpace(model.Id) && OcdlogisticsEntities.tbl_Product.Find(model.Id) != null)
                {
                    product.Id = model.Id;
                }

                if (!string.IsNullOrWhiteSpace(model.Ingredients))
                {
                    List<tbl_Ingredient_Info> tbl_Ingredients = new List<tbl_Ingredient_Info>();
                    var list = model.Ingredients.Split(',');
                    foreach (var item in list)
                    {
                        var obj = OcdlogisticsEntities.tblIngredients.FirstOrDefault(x => x.Name.Contains(item));
                        if (obj == null)
                        {
                            product.tbl_Ingredient_Info.Add(new tbl_Ingredient_Info()
                            {
                                Id = Guid.NewGuid().ToString(),
                                tbl_Ingredients = new tbl_Ingredients()
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    Name = item,
                                }
                            });
                        }
                        else
                        {
                            var ingredient = new tbl_Ingredient_Info()
                            {
                                Id = Guid.NewGuid().ToString(),
                                IngredientId = obj.ID
                            };
                            tbl_Ingredients.Add(ingredient);
                        }
                    }

                    product.tbl_Ingredient_Info = tbl_Ingredients;
                }
                else
                {
                    product.tbl_Ingredient_Info = null;
                }

              



                    var productLine = new tblProduct_line_Info()
                {
                    ID = Guid.NewGuid().ToString(),
                    BrandId = model.ProductBrandId,
                };

                if (model.ProductLineId != null && OcdlogisticsEntities.tbl_Product_Line.Find(model.ProductLineId) != null)
                {
                    productLine.ProductLineId = model.ProductLineId;
                }

                if (model.ProductIndustryId != null && OcdlogisticsEntities.tbl_Industry.Find(model.ProductIndustryId) != null)
                {
                    product.IndustryId = model.ProductIndustryId;
                }


                product.tblProduct_line_Info.Add(productLine);

                if (model.ImageFromWebLink != null && model.ImageFromWebLink.Where(x => x != null).Count() > 0)
                {
                    foreach (var item in model.ImageFromWebLink.Where(x => x != null))
                    {
                        product.tbl_Product_File.Add(new tbl_Product_File()
                        {
                            Id = Guid.NewGuid().ToString(),
                            tbl_File = new tbl_File()
                            {
                                Id = Guid.NewGuid().ToString(),
                                IsExternal = true,
                                Name = "External",
                                Type = "Image",
                                PathName = item.ToString()
                            }
                        });
                    }
                }


                if (model.VideoFromWebLink != null && model.VideoFromWebLink.Where(x => x != null).Count() > 0)
                {
                    foreach (var item in model.VideoFromWebLink.Where(x => x != null))
                    {
                        product.tbl_Product_File.Add(new tbl_Product_File()
                        {
                            Id = Guid.NewGuid().ToString(),
                            tbl_File = new tbl_File()
                            {
                                Id = Guid.NewGuid().ToString(),
                                IsExternal = true,
                                Name = "External",
                                Type = "Video",
                                PathName = item.ToString()
                            }
                        });
                    }
                }


                if (model.Files != null && model.Files.Where(x => x != null).Count() > 0)
                {
                    foreach (var item in model.Files.Where(x => x != null))
                    {
                        product.tbl_Product_File.Add(new tbl_Product_File()
                        {
                            Id = Guid.NewGuid().ToString(),
                            tbl_File = new tbl_File()
                            {
                                Id = Guid.NewGuid().ToString(),
                                IsExternal = false,
                                Name = Path.GetFileName(item.FileName),
                                Type = item.ContentType,
                                PathName = FileManager.SaveImage(item)
                            }
                        });
                    }
                }

                product.SupplyPrice = model.SupplyPrice;
                product.Markup = model.Markup;

                List<tbl_ProductVendorsInfo> vendorInfos = new List<tbl_ProductVendorsInfo>(); 
                foreach (var item in model.ProductVendors)
                {
                    vendorInfos.Add(new tbl_ProductVendorsInfo()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Vendor = item,
                        ProductId = product.Id
                    });
                }
                product.tbl_ProductVendorsInfo = vendorInfos;
                if (!string.IsNullOrWhiteSpace(model.Id) && OcdlogisticsEntities.tbl_Product.Find(model.Id) != null)
                {
                    await Database.Entity<tbl_Product>.UpdateAsync(product,OcdlogisticsEntities);
                }
                else
                {
                    await Database.Entity<tbl_Product>.AddAsync(product);
                 
                }

                return RedirectToAction("Index");
            }

            ViewBag.ProductColorId = new SelectList(OcdlogisticsEntities.tbl_Color, "Id", "Name");
            ViewBag.ProductIndustryId = new SelectList(OcdlogisticsEntities.tbl_Industry, "Id", "Name");
            ViewBag.ProductWeightUnitId = new SelectList(OcdlogisticsEntities.tbl_UnitOfWeight, "Id", "Name");
            ViewBag.ProductBrandId = new SelectList(OcdlogisticsEntities.tbl_Brand, "Id", "Name");

            ViewBag.ProductLineId = new SelectList(OcdlogisticsEntities.tbl_Product_Line, "Id", "Name");
            ViewBag.ProductTypeId = new SelectList(OcdlogisticsEntities.tbl_ProductCategory, "Id", "Name");
            ViewBag.ProductSubCategory = new SelectList(OcdlogisticsEntities.tbl_ProductCategory, "Id", "Name");
            ViewBag.ProductVendors = new SelectList(OcdlogisticsEntities.tbl_Vendors, "Id", "Name");
            if (!string.IsNullOrWhiteSpace(model.Id) && OcdlogisticsEntities.tbl_Product.Find(model.Id) != null)
            {
                return View("Create", model);
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult Edit(string id)
        {
            var product = OcdlogisticsEntities.tbl_Product.Find(id);
            if (product != null)
            {
                ProductViewModel viewModel = new ProductViewModel();
                viewModel.BriefDescription = product.ShortDescription;
                viewModel.FullDescription = product.FullDescription;
                viewModel.Name = product.Name;
                viewModel.UPC = product.UPC;
                viewModel.PrductSkU = product.SKU;
                viewModel.Id = product.Id;
                viewModel.Part = product.Part;
                ViewBag.Ingredients = new SelectList(OcdlogisticsEntities.tbl_Ingredients, "Id", "Name");
                viewModel.SupplyPrice = Convert.ToInt64(product.SupplyPrice);
                viewModel.Markup = Convert.ToInt64(product.Markup);
                viewModel.SellingPrice = viewModel.SupplyPrice * viewModel.SupplyPrice;
                ViewBag.ProductWeight = new SelectList(OcdlogisticsEntities.tbl_Product, "Id", "Name");
                viewModel.Weight = Convert.ToInt32(product.ProductWeight);
                ViewBag.ProductWeightUnitId = new SelectList(OcdlogisticsEntities.tbl_UnitOfWeight, "Id", "Name");
                viewModel.ProductWeightUnitId = Convert.ToInt32(product.ProductSizeWeightTypeId);
                ViewBag.ProductColorId = new SelectList(OcdlogisticsEntities.tbl_Color, "Id", "Name");
                viewModel.ProductColorId = product.ProductColorId;
                viewModel.Quantity = Convert.ToInt32(product.Quantity);
                viewModel.ProductTypeId = product.TypeId;
                viewModel.ProductSubCategory = product.SubCategId;
                ViewBag.ProductBrandId = new SelectList(OcdlogisticsEntities.tbl_Brand, "Id", "Name");
                ViewBag.ProductLineId = new SelectList(OcdlogisticsEntities.tbl_Product_Line, "Id", "Name");
                ViewBag.ProductTypeId = new SelectList(OcdlogisticsEntities.tbl_ProductType, "Id", "Name");
                ViewBag.ProductSubCategory = new SelectList(OcdlogisticsEntities.tbl_ProductCategory, "Id", "Name");
                ViewBag.ProductIndustryId = new SelectList(OcdlogisticsEntities.tbl_Industry, "Id", "Name");



                return View("Create", viewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Brands()
        {
            return View();
        }
        public ActionResult Colors()
        {
            return View();
        }
        public ActionResult AddNewBrand()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddNewBrand(BrandViewModel brandModel)
        {
            if (brandModel != null && ModelState.IsValid)
            {
                tbl_Business business = new tbl_Business()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = brandModel.Business.Name,
                    Email = brandModel.Business.Email,
                    PhoneNumber = brandModel.Business.PhoneNumber,
                    tbl_Brand = new tbl_Brand()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = brandModel.Name,
                        Description = brandModel.Description,
                        tbl_File = new tbl_File()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = Path.GetFileName(brandModel.TradeMarkImg.FileName),
                            PathName = FileManager.SaveImage(brandModel.TradeMarkImg),
                            Type = brandModel.TradeMarkImg.ContentType
                        }
                    }
                    ,
                    PointOfContractPerson = new DomainModels.Models.Entity_Models.PointOfContractPerson()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = brandModel.Business.Person.Name,
                        Email = brandModel.Business.Person.Email,
                        ContactNumber = brandModel.Business.Person.ContactNumber,
                        Designation = brandModel.Business.Person.Designation,
                    }
                };

                try
                {
                    await Database.Entity<tbl_Business>.AddAsync(business);
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var item in ex.EntityValidationErrors)
                    {
                        foreach (var error in item.ValidationErrors)
                        {
                            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                            return View(brandModel);
                        }
                    }
                }
                return RedirectToAction("Brands");
            }

            return View(brandModel);
        }

        //public ActionResult Images()
        //{
        // Session["Product_Process"] = 2;
        // if (Session["tbl_Product"] == null || !(Session["tbl_Product"] is tbl_Product))
        // return RedirectToAction("Create");

        // if (Session["Product_Images"] != null && Session["Product_Images"] is ProductFilesViewModel)
        // {
        // return View((ProductFilesViewModel)Session["Product_Images"]);
        // }

        // return View();
        //}

        //[HttpPost]
        //public ActionResult Images(ProductFilesViewModel productFiles)
        //{
        // if (Session["tbl_Product"] == null || !(Session["tbl_Product"] is tbl_Product))
        // return RedirectToAction("Create");

        // if (productFiles == null || (productFiles != null && productFiles.Files != null
        // && productFiles.Files.Count() <= 0)
        // || (productFiles.Files.Count() > 0 && productFiles.Files.Where(x => x == null).Count() > 0))
        // {
        // ModelState.AddModelError("files", "Product should have at least 1 image.");
        // return View(productFiles);
        // }


        // var product = (tbl_Product)Session["tbl_Product"];
        // foreach (var file in productFiles.Files)
        // {
        // if (file != null)
        // {
        // var fileId = Guid.NewGuid().ToString();
        // product.tbl_Product_File.Add(new tbl_Product_File()
        // {
        // Id = Guid.NewGuid().ToString(),
        // tbl_File = new tbl_File()
        // {
        // Id = fileId,
        // Name = Path.GetFileName(file.FileName),
        // Type = file.ContentType,
        // PathName = HttpUtility.UrlEncode("~/uploadimages/" + fileId + file.FileName)
        // }
        // });
        // }
        // }

        // Session["Product_Images"] = productFiles;

        // Session["tbl_Product"] = product;
        // return RedirectToAction("Pricing");
        //}


        public ActionResult ClearProductImages()
        {
            Session["Product_Images"] = null;
            return RedirectToAction("Images");
        }

        public ActionResult Pricing()
        {
            if (Session["tbl_Product"] == null || !(Session["tbl_Product"] is tbl_Product))
                return RedirectToAction("Create");

            Session["Product_Process"] = 3;
            return View();
        }

        //[HttpPost]
        //public async Task<ActionResult> Pricing(ProductPricingViewModel model)
        //{
        // if (Session["tbl_Product"] == null || !(Session["tbl_Product"] is tbl_Product))
        // return RedirectToAction("Create");


        // if (model != null && ModelState.IsValid)
        // {
        // try
        // {
        // var product = (tbl_Product)Session["tbl_Product"];
        // product.SKU = model.PrductSkU;

        // //product.CreateAsync();



        // return RedirectToAction("List");
        // }
        // catch (DbEntityValidationException ex)
        // {

        // }
        // }



        // return View(model);
        //}


        [HttpPost]
        public ActionResult GenerateUniqueSKU()
        {
            return Json(generateSKU());
        }

        private string generateSKU()
        {
            var productSKU = RandomString(10);
            while (OcdlogisticsEntities.tbl_Product.FirstOrDefault(x => x.SKU == productSKU) != null)
            {
                productSKU = RandomString(10);
            }
            return productSKU;
        }

        private static Random random = new Random();
        [NonAction]
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public ActionResult ProductLine()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Categories()
        {
            return View();
        }
        public ActionResult LoyalityRules()
        {
            return View();
        }
        public ActionResult Industries()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> UpdateIndustry(IndustryViewModel model, string UpdateField_Id)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.Values.FirstOrDefault() != null)
                {
                    var error = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault();
                    return RedirectToAction("Industries", new { UpdateError = error.ErrorMessage });
                }
            }
            else
            {
                var row = await OcdlogisticsEntities.tbl_Industry.FindAsync(UpdateField_Id);
                if (row != null)
                {
                    row.name = model.Name;
                    await Database.Entity<tbl_Industry>.UpdateAsync(row, OcdlogisticsEntities);
                    return RedirectToAction("Industries", new { SuccessMsg = $"The changes have successfully been applied." });
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> UpdateColor(ColorViewModel model, string UpdateField_Id)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.Values.FirstOrDefault() != null)
                {
                    var error = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault();
                    return RedirectToAction("Colors", new { UpdateError = error.ErrorMessage });
                }
            }
            else
            {
                var row = await OcdlogisticsEntities.tbl_Color.FindAsync(UpdateField_Id);
                if (row != null)
                {
                    row.Name = model.Name;
                    row.Value = model.Value;
                    await Database.Entity<tbl_Color>.UpdateAsync(row, OcdlogisticsEntities);
                    return RedirectToAction("Colors", new { SuccessMsg = $"The changes have successfully been applied." });
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> CreateIndustry(IndustryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.Values.FirstOrDefault() != null)
                {
                    var error = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault();
                    return RedirectToAction("Industries", new { Error = error.ErrorMessage });

                }

            }
            else
            {
                await Database.Entity<tbl_Industry>.AddAsync(new tbl_Industry() { Id = Guid.NewGuid().ToString(), name = model.Name.Trim() }); ;
            }
            return RedirectToAction("Industries");
        }

        [HttpPost]
        public async Task<ActionResult> CreateColor(ColorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.Values.FirstOrDefault() != null)
                {
                    var error = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault();
                    return RedirectToAction("Colors", new { Error = error.ErrorMessage });

                }

            }
            else
            {
                await Database.Entity<tbl_Color>.AddAsync(new tbl_Color() { Id = Guid.NewGuid().ToString(), Name = model.Name.Trim(), Value = model.Value }); ;
            }
            return RedirectToAction("Colors");
        }


        [HttpPost]
        public async Task<ActionResult> RemoveColor(string id)
        {
            using (OcdlogisticsEntities context = new OcdlogisticsEntities())
            {
                tbl_Color category = null;
                if (!string.IsNullOrEmpty(id) && (category = context.tbl_Color.Find(id)) != null)
                {
                    await Database.Entity<tbl_Color>.RemoveAsync(category, context);
                }
            }
            return RedirectToAction("Colors");
        }


        [HttpPost]
        public async Task<ActionResult> RemoveIndustry(string id)
        {
            using (OcdlogisticsEntities context = new OcdlogisticsEntities())
            {
                tbl_Industry category = null;
                if (!string.IsNullOrEmpty(id) && (category = context.tbl_Industry.Find(id)) != null)
                {
                    await Database.Entity<tbl_Industry>.RemoveAsync(category, context);
                }
            }
            return RedirectToAction("Industries");
        }
        public ActionResult SubCategories()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> CreateCategory(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.Values.FirstOrDefault() != null)
                {
                    var error = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault();
                    return RedirectToAction("Categories", new { Error = error.ErrorMessage });

                }

            }
            else
            {
                var row = OcdlogisticsEntities.tbl_ProductType.FirstOrDefault(x => x.Name.ToLower().Contains(model.Name.Trim().ToLower()));
                if (row != null)
                {
                    return RedirectToAction("Categories", new { Error = "The Category with that name is already present." });
                }
                else
                {
                    await Database.Entity<tbl_ProductType>.AddAsync(new tbl_ProductType() { Id = Guid.NewGuid().ToString(), Name = model.Name.Trim() });
                }
            }
            return RedirectToAction("Categories");
        }
        [HttpPost]
        public async Task<ActionResult> RemoveCategory(string id)
        {
            using (OcdlogisticsEntities context = new OcdlogisticsEntities())
            {
                tbl_ProductType category = null;
                if (!string.IsNullOrEmpty(id) && (category = context.tbl_ProductType.Find(id)) != null)
                {
                    await Database.Entity<tbl_ProductType>.RemoveAsync(category, context);
                }
            }
            return RedirectToAction("Categories");
        }

        [HttpPost]
        public async Task<ActionResult> CreateSubCategory(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.Values.FirstOrDefault() != null)
                {
                    var error = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault();
                    return RedirectToAction("SubCategories", new { Error = error.ErrorMessage });

                }

            }
            else
            {
                var row = OcdlogisticsEntities.tbl_ProductCategory.FirstOrDefault(x => x.Name.ToLower().Contains(model.Name.Trim().ToLower()));
                if (row != null)
                {
                    return RedirectToAction("SubCategories", new { Error = $"The Sub Category with name {model.Name} is already taken." });
                }
                else
                {
                    await Database.Entity<tbl_ProductCategory>.AddAsync(new tbl_ProductCategory() { Id = Guid.NewGuid().ToString(), Name = model.Name.Trim(), CreateDate = DateTime.Now });
                }
            }
            return RedirectToAction("SubCategories");
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductLine(ProductLineViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.Values.FirstOrDefault() != null)
                {
                    var error = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault();
                    return RedirectToAction("ProductLine", new { Error = error.ErrorMessage });

                }

            }
            else
            {
                var row = OcdlogisticsEntities.tbl_Product_Line.FirstOrDefault(x => x.Name.ToLower().Contains(model.Name.Trim().ToLower()));
                if (row != null)
                {
                    return RedirectToAction("ProductLine", new { Error = $"The product line with name {model.Name} is already taken." });
                }
                else
                {
                    await Database.Entity<tbl_Product_Line>.AddAsync(new tbl_Product_Line() { Id = Guid.NewGuid().ToString(), Name = model.Name.Trim() }); ;
                }
            }
            return RedirectToAction("ProductLine");
        }
        [HttpPost]
        public async Task<ActionResult> RemoveSubCategory(string id)
        {
            using (OcdlogisticsEntities context = new OcdlogisticsEntities())
            {
                tbl_ProductCategory category = null;
                if (!string.IsNullOrEmpty(id) && (category = context.tbl_ProductCategory.Find(id)) != null)
                {
                    await Database.Entity<tbl_ProductCategory>.RemoveAsync(category, context);
                }
            }
            return RedirectToAction("SubCategories");
        }

        [HttpPost]
        public async Task<ActionResult> RemoveProductLine(string id)
        {
            using (OcdlogisticsEntities context = new OcdlogisticsEntities())
            {
                tbl_Product_Line category = null;
                if (!string.IsNullOrEmpty(id) && (category = context.tbl_Product_Line.Find(id)) != null)
                {
                    await Database.Entity<tbl_Product_Line>.RemoveAsync(category, context);
                }
            }
            return RedirectToAction("ProductLine");
        }
        [HttpPost]
        public async Task<ActionResult> UpdateCategory(CategoryViewModel model, int EntityType, string UpdateField_Id)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.Values.FirstOrDefault() != null)
                {
                    var error = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault();
                    if (EntityType == 1)
                    {
                        return RedirectToAction("Categories", new { UpdateError = error.ErrorMessage });
                    }
                    else if (EntityType == 2)
                    {
                        return RedirectToAction("SubCategories", new { UpdateError = error.ErrorMessage });
                    }

                }

            }
            else
            {
                if (EntityType == 1)
                {
                    var row = await OcdlogisticsEntities.tbl_ProductType.FindAsync(UpdateField_Id);
                    if (row != null)
                    {
                        row.Name = model.Name;
                        await Database.Entity<tbl_ProductType>.UpdateAsync(row, OcdlogisticsEntities);
                        return RedirectToAction("Categories", new { SuccessMsg = $"The changes have successfully been applied." });
                    }
                }
                else if (EntityType == 2)
                {
                    var row = await OcdlogisticsEntities.tbl_ProductCategory.FindAsync(UpdateField_Id);
                    if (row != null)
                    {
                        row.Name = model.Name;
                        await Database.Entity<tbl_ProductCategory>.UpdateAsync(row, OcdlogisticsEntities);
                        return RedirectToAction("SubCategories", new { SuccessMsg = $"The changes have successfully been applied." });
                    }
                }


            }

            if (EntityType == 1)
            {
                return RedirectToAction("Categories");
            }
            else if (EntityType == 2)
            {
                return RedirectToAction("SubCategories");
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<ActionResult> UpdateProductLine(ProductLineViewModel model, string UpdateField_Id)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.Values.FirstOrDefault() != null)
                {
                    var error = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault();
                    return RedirectToAction("ProductLine", new { UpdateError = error.ErrorMessage });
                }
            }
            else
            {
                var row = await OcdlogisticsEntities.tbl_Product_Line.FindAsync(UpdateField_Id);
                if (row != null)
                {
                    row.Name = model.Name;
                    await Database.Entity<tbl_Product_Line>.UpdateAsync(row, OcdlogisticsEntities);
                    return RedirectToAction("ProductLine", new { SuccessMsg = $"The changes have successfully been applied." });
                }
            }

            return RedirectToAction("Index");
        }
    }

    public static class ProductEx
    {
        public async static void CreateAsync(this tbl_Product product)
        {
            await Database.Entity<tbl_Product>.AddAsync(product);
        }
    }

}