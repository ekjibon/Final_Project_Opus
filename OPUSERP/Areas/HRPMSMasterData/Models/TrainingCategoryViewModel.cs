using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Areas.HRPMSMasterData.Models.Lang;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class TrainingCategoryViewModel
    {
        public int trnCategoryId { get; set; }
        [Required]
        public string trainingCategoryName { get; set; }
        public string trainingCategoryNameBn { get; set; }
        
        public string trainingCategoryShortName { get; set; }

        public TrainingCategoryLn fLang { get; set; }

        public IEnumerable<TrainingCategory> trainingCategories { get; set; }
    }
}
