using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;


namespace OcdlogisticsSolution.Web.Models
{
    public class OurTeamPageViewModel
    {
        public OurTeamPageViewModel()
        {
            tbl_OurTeamPage = new tbl_OurTeamPage();
            tbl_TeamProfilesList = new List<tbl_TeamProfiles>();
        }
        public tbl_OurTeamPage tbl_OurTeamPage { get; set; }
        public List<tbl_TeamProfiles> tbl_TeamProfilesList { get; set; }
    }
}