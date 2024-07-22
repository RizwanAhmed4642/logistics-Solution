using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OcdlogisticsSolution.Web.Admin.Data
{
    public class UserRoleRightModel
    {
        public tblRights tbl_Rights { get; set; }
        public AspNetRoles AspNetRoles { get; set; }
    }


    public class EditUserRoleRightModel: UserRoleRightModel
    {
        [Required]
        public string Id { get; set; }
    }
}