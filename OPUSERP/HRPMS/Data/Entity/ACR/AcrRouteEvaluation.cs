using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.ACR
{
    [Table("AcrRouteEvaluation", Schema = "HR")]
    public class AcrRouteEvaluation : Base
    {
        public int? acrRouteId { get; set; }
        public AcrRoute acrRoute { get; set; }

        public int? acrEvaluationMasterId { get; set; }
        public AcrEvaluationMaster acrEvaluationMaster { get; set; }
    }
}
