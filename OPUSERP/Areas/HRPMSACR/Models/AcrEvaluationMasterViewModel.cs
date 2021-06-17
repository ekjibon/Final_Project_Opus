using OPUSERP.Areas.HRPMSACR.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.ACR;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSACR.Models
{
    public class AcrEvaluationMasterViewModel
    {
        public int acrEvaMasterId { get; set; }
        public string evaluationMasterName { get; set; }
        public string evaluationMasterNameBn { get; set; }

        public AcrEvaluationMasterLn fLang { get; set; }

        public IEnumerable<AcrEvaluationMaster> acrEvaluationMasters { get; set; }

        public AcrEvaluationMaster acrEvaluationMaster { get; set; }
    }
}
