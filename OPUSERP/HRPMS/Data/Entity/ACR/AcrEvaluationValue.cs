using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.ACR
{
    [Table("AcrEvaluationValue", Schema = "HR")]
    public class AcrEvaluationValue : Base
    {       
        public int acrInitiateId { get; set; }
        public AcrInitiate acrInitiate { get; set; }

        public int acrEvaluationDetailId { get; set; }
        public AcrEvaluationDetail acrEvaluationDetail { get; set; }

        public int? marks { get; set; }
    }
}
