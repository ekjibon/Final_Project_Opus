using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class TravelPurposeViewModel
    {
        public int travelPurposeId { get; set; }
        [Required]
        public string purposeName { get; set; }
        public string purposeNameBn { get; set; }

        public string purposeShortName { get; set; }

        public TravelPurposeLn fLang { get; set; }

        public IEnumerable<TravelPurpose> travelPurposes { get; set; }
    }
}
