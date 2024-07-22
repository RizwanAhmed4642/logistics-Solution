using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcdlogisticsSolution.Common.ViewModels
{
    public enum Pages
    {
        Home = 0,
        Education = 1
    }
    public class FAQViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Heading { get; set; }
        [Required]
        public int Type { get; set; }
    }

    public class FAQ_QuestionViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }
        [Required]
        public int? FAQId { get; set; }
    }
}