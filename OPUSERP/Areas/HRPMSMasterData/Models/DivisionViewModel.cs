using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class DivisionViewModel
    {
        public int divisionId { get; set; }
        [Display(Name = "Country")]
        public int countryId { get; set; }

        [Required]
        [Display(Name = "Division Code")]
        public string divisionCode { get; set; }

        [Required]
        [Display(Name = "Division Name")]
        public string divisionName { get; set; }
        public string divisionNameBn { get; set; }

        public string shortName { get; set; }

        public DivisionLn fLang { get; set; }

        public IEnumerable<Division> divisions { get; set; }
        public IEnumerable<Country> countries { get; set; }
    }
}
