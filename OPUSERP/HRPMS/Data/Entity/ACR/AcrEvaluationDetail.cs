using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.ACR
{
    [Table("AcrEvaluationDetail", Schema = "HR")]
    public class AcrEvaluationDetail : Base
    {
        public int acrEvaluationMasterId { get; set; }
        public AcrEvaluationMaster acrEvaluationMaster { get; set; }

        [Required]
        public string evaluationDetailName { get; set; }
        [Required]
        public string evaluationDetailNameBn { get; set; }        

        public string comments { get; set; }
    }
}
