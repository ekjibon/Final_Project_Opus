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
    [Table("AcrEvaluationMaster", Schema = "HR")]
    public class AcrEvaluationMaster : Base
    {
        [Required]
        public string evaluationMasterName { get; set; }
        [Required]
        public string evaluationMasterNameBn { get; set; }
    }
}
