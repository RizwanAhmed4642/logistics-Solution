using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OcdlogisticsSolution.Common.ViewModels
{
    public class HomePage_Feature_Benifits_ViewModel
    {
        public int Id { get; set; }
        public HttpPostedFileBase file { get; set; }
        public string IconPathName { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 3)]
        public string Heading { get; set; }
        [StringLength(1000, MinimumLength = 3)]
        [Required]
        public string Content { get; set; }
    }
}
