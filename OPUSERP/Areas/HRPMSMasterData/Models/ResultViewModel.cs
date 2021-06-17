using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class ResultViewModel
    {
        public int resultId { get; set; }
        [Required]
        public string resultName { get; set; }
        public string resultNameBn { get; set; }

        public string resultShortName { get; set; }

        [Required]
        public decimal resultMaxValue { get; set; }

        public ResultInfoLn fLang { get; set; }
        public IEnumerable<Result> resultslist { get; set; }
    }
}
