using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OcdlogisticsSolution.Common.ViewModels
{
    public class HomePagePackagesViewModel
    {
        public string Icon { get; set; }
        public int Id { get; set; }
        public HttpPostedFileBase file { get; set; }
        [Required]
        public string Cost { get; set; }
        [Required]
        public Nullable<int> DurationType { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Url)]
        public string link { get; set; }
    }
}