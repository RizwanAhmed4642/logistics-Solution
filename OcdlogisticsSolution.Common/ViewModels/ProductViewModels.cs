using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OcdlogisticsSolution.Common.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        public string ProductTypeId { get; set; }

        public string ProductLineId { get; set; }

        [Required]
        public string ProductBrandId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string Ingredients { get; set; }


        [Required]
        public int Weight { get; set; }
        [Required]
        public int ProductWeightUnitId { get; set; }
        [Required]
        public string ProductColorId { get; set; }

        public string ProductIndustryId { get; set; }
        [Required]
        public string ProductSubCategory { get; set; }
        [Required]
        public List<string> ProductVendors { get; set; }


        public List<Uri> ImageFromWebLink { get; set; }
        public List<Uri> VideoFromWebLink { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 20)]
        public string BriefDescription { get; set; }
        [StringLength(20000, MinimumLength = 20)]
        public string FullDescription { get; set; }


        //Images Panel Open There

        public IEnumerable<HttpPostedFileBase> Files { get; set; }

        //Pricing Model Open There


        [Required]
        public long SupplyPrice { get; set; }
        [Required]
        public long Markup { get; set; }
        public double SellingPrice { get; set; }
        [Required]
        [UniqueProductSKU]
        public string PrductSkU { get; set; }
        public string UPC { get; set; }
        public string Part { get; set; }
        //[Required]
        //public string Threshold { get; set; }
        //[Required]
        //public long ReorderAmount { get; set; }
    }


    public class BrandViewModel
    {
        public BrandViewModel()
        {
            this.Business = new BusinessViewModel();
        }
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public HttpPostedFileBase TradeMarkImg { get; set; }
        [Required]
        [StringLength(20000, MinimumLength = 20)]
        public string Description { get; set; }
        public BusinessViewModel Business { get; set; }

    }

    public class BusinessViewModel
    {
        public BusinessViewModel()
        {
            this.Person = new PointOfContractPerson();
        }
        public int ID { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public PointOfContractPerson Person { get; set; }
    }

    public class PointOfContractPerson
    {
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string ContactNumber { get; set; }
        [Required]
        public string Designation { get; set; }
    }

    public class CategoryViewModel
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }
    }

    public class IndustryViewModel
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }
    }

    public class ColorViewModel
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }
    }

    public class ProductLineViewModel
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }
    }

    public class UniqueProductSKUAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;
            using (OcdlogisticsEntities entities = new OcdlogisticsEntities())
            {
                var item = entities.tbl_Product.FirstOrDefault(x => x.SKU == value.ToString());
                return item == null;
            }
        }
    }

}