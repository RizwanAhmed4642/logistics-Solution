using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcdlogisticsSolution.Common.ViewModels
{
    public class DistributionServicesViewModel
    {
        public DistributionServicesViewModel()
        {
            this.Packages = new List<string>();
        }
        public string Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 4)]
        public string Name { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 4)]
        public string ShortText { get; set; }
        [Required]
        public string Cost { get; set; }
        [Required]
        public int? DurationType { get; set; }
        [Required]
        public List<string> Packages { get; set; }
    }

    public class PackageViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Cost { get; set; }
        [Required]
        public Nullable<int> DurationType { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Label { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Features { get; set; }
    }

}
