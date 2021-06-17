using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class TrainingInstituteViewModel
    {
        public int trnInstituteId { get; set; }
        [Required]
        public string trainingInstituteName { get; set; }
        public string trainingInstituteNameBn { get; set; }

        public string trainingInstituteShortName { get; set; }

        public TrainigInstituteLn fLang { get; set; }

        public IEnumerable<TrainingInstitute> trainingInstitutes { get; set; }
    }
}
