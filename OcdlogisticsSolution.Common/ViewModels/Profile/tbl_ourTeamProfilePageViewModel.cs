using OcdlogisticsSolution.Common.ViewModels.MetaDataTypes;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcdlogisticsSolution.Common.ViewModels.Profile
{
  public  class tbl_ourTeamProfilePageViewModel
    {
        public DomainModels.Models.Entity_Models.tbl_ProfilePage tbl_ProfilePage { get; set; }
        public DomainModels.Models.Entity_Models.tbl_TeamProfiles tbl_TeamProfiles { get; set; }
    }
}
