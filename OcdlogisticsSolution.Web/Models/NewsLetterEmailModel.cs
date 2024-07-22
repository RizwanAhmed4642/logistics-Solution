using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OcdlogisticsSolution.Web.Models
{
    public class NewsLetterEmailModel
    {
        public string subject{ get; set; }
      
        [AllowHtml]
        public string body{ get; set; }
    }
}