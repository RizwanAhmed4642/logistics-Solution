using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace OcdlogisticsSolution.Web.Models
{
    public class ProfilePageViewModel
    {
        public OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_TeamProfiles tbl_TeamProfiles { get; set; }
        public OcdlogisticsSolution.DomainModels.Models.Entity_Models.tbl_ProfilePage tbl_ProfilePage { get; set; }
    }
}