using Microsoft.AspNet.Identity;
using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcdlogisticsSolution.Web.Models.CustomValidation
{
  public  class UniqueEmail:ValidationAttribute
    {
        private OcdlogisticsEntities _entities;

        public UniqueEmail()
        {
            _entities = new OcdlogisticsEntities();
        }
        public override bool IsValid(object value)
        {
            if (value == null ||  string.IsNullOrEmpty(value.ToString()))
                return false;
            var IsValid = _entities.AspNetUsers.FirstOrDefault(x=>x.Email.ToLower()== value.ToString().ToLower()) == null;
            if (IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
