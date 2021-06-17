using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Data.Entity.Matrix
{
    [Table("ApprovalLog", Schema = "SCM")]
    public class ApprovalLog:Base
    {
        public int masterId { get; set; }

        public int? matrixTypeId { get; set; }
        public MatrixType matrixType { get; set; }

        public int userId { get; set; }

        public int? nextApproverId { get; set; }

        public int sequenceNo { get; set; }

        public int isActive { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string notes { get; set; }
    }
}
