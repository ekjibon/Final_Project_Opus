//using UMBRELLA.Areas.MasterData.Models.Lang;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OPUSERP.Data.Entity.Master;

namespace OPUSERP.Areas.MasterData.Models
{
    public class DistrictViewModel
    {      
        [Display(Name = "Division")]
        public int divisionId { get; set; }

        public int districtId { get; set; }

        [Required]
        [Display(Name = "District Code")]
        public string districtCode { get; set; }

        [Required]
        [Display(Name = "District Name")]
        public string districtName { get; set; }
        public string districtNameBn { get; set; }

        public string shortName { get; set; }

        //public DistrictLn fLang { get; set; }

        public IEnumerable<District> districts { get; set; }
        public IEnumerable<Division> divisions { get; set; }
        public IEnumerable<Country> countries { get; set; }
    }
}
