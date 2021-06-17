//using UMBRELLA.Areas.MasterData.Models.Lang;

using OPUSERP.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.MasterData.Models
{
    public class CountryViewModel
    {
        public string countryId { get; set; }
        [Required]
        public string countryCode { get; set; }

        [Required]
        public string countryName { get; set; }
        //public string countryNameBn { get; set; }
        
        public string shortName { get; set; }

        //public CountryLn fLang { get; set; }

        public IEnumerable<Country> countries { get; set; }
    }
}
