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
    public class AcrEvaluationValueViewModel
    {
        public int acrEvaValueId { get; set; }
        public int acrInitiateId { get; set; }
        public int acrEvaluationDetailId { get; set; }

        public int? marks { get; set; }   

        public AcrEvaluationValue acrEvaluationValue { get; set; }

        public IEnumerable<AcrEvaluationValue> acrEvaluationValues { get; set; }

        public IEnumerable<AcrEvaluationDetail> acrEvaluationDetails { get; set; }
    }
}
