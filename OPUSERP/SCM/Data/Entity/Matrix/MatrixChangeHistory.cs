using OPUSERP.SCM.Data.Entity.Requisition;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Data.Entity.Matrix
{
    [Table("MatrixChangeHistory", Schema = "SCM")]
    public class MatrixChangeHistory : Base
    {
        public int requisitionMasterId { get; set; }
        public RequisitionMaster requisitionMaster { get; set; }

        public int? matrixTypeId { get; set; }
        public MatrixType matrixType { get; set; }

        public int userId { get; set; }

        public int? nextApproverId { get; set; }

        

        [Column(TypeName = "nvarchar(500)")]
        public string notes { get; set; }
    }
}
