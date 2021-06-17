using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class AwardViewModel
    {
        [Required]
        public string awardId { get; set; }
        public string awardName { get; set; }
        public string awardNameBn { get; set; }
        
        public string awardShortName { get; set; }

        public AwardLn fLang { get; set; }

        public IEnumerable<Award> awards { get; set; }
    }
}
