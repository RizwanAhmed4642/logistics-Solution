using OcdlogisticsSolution.DomainModels.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcdlogisticsSolution.Common.ViewModels.ResourcesModel
{
    public class ResourceViewModel
    {
        //public string Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [Required]
        public long ResourcessTypeId { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
        //public string UserId { get; set; }
    }

    public class SpecialHoursViewModel
    {
        public DateTime Date { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public bool IsClosed { get; set; }
    }
    public class ResourcesTypeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

    }
    public class ResourceSheduleViewModel
    {
        public ResourceSheduleViewModel()
        {
            this.Weekdays = new List<WeekdayViewModel>();
        }
        //public string Id { get; set; }
        public List<WeekdayViewModel> Weekdays { get; set; }

        public string UserId { get; set; }
    }

    public class WeekdayViewModel
    {
        public WeekdayViewModel(int id, string name)
        {
            this.WeekDayId = id;
            this.Name = name;
        }
        public int WeekDayId { get; set; }
        public string Name { get; set; }
        public string Open { get; set; }
        public string Close { get; set; }
        public bool IsEmployeeAvailable { get; set; }
    }

    public class ResourcesSpecialHourViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime DateTo { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsClose { get; set; }
    }
}
