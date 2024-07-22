using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcdlogisticsSolution.DomainModels.Models
{
    public class TeamProfileMetaData
    {
        [Required]
        [MaxLength(250)]
        public string ProfileName { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Speciality { get; set; }
        [Required]
        [MaxLength(200)]
        public string Position { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(30)]
        public string ContactNumber { get; set; }
        [MaxLength(150)]
        public string Facebook { get; set; }
        [MaxLength(150)]
        public string Instagram { get; set; }
        [MaxLength(150)]
        public string Tweeter { get; set; }
        [MaxLength(150)]
        public string Pintrestr { get; set; }
        [MaxLength(150)]
        public string Youtube { get; set; }
        [MaxLength(150)]
        public string LinkedIn { get; set; }
        [MaxLength(50)]
        public string Message { get; set; }
        public string TeamImage { get; set; }
      //  [Required]
        [MaxLength(50)]
        public string FavoriteQuote { get; set; }
      
      
    }
    [MetadataType(typeof(TeamProfileMetaData))]
    public partial class tbl_TeamProfiles
    {

    }
}
