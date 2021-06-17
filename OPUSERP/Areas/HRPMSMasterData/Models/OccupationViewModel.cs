using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class OccupationViewModel
    {
        public int occupationId { get; set; }
        [Required]
        public string occupationName { get; set; }
        public string occupationNameBn { get; set; }

        public string occupationShortName { get; set; }

        public OccupationLn fLang { get; set; }

        public IEnumerable<Occupation> occupations { get; set; }
    }
}
