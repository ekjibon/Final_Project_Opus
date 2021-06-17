using OPUSERP.SCM.Data.Entity.MasterData;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Data.Entity.Matrix
{
    [Table("ApprovalMatrix", Schema = "SCM")]
    public class ApprovalMatrix:Base
    {
        public int? projectId { get; set; }
        public Project project { get; set; }

        public int? matrixTypeId { get; set; }
        public MatrixType matrixType { get; set; }

        public int? userId { get; set; }

        public int? nextApproverId { get; set; }

        public int? approverTypeId { get; set; }
        public ApproverType approverType { get; set; }

        public int isActive { get; set; }
        public int? isLeave { get; set; }

        public int sequenceNo { get; set; }
    }
}
