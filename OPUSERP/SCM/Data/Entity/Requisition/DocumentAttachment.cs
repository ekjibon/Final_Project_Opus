using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Matrix;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.Requisition
{
    [Table("DocumentAttachments", Schema = "SCM")]
    public class DocumentAttachment:Base
    {
        public int masterId { get; set; }

        public string fileName { get; set; }

        public string fileType { get; set; }

        public string filePath { get; set; }

        public string subject { get; set; }
        public int matrixTypeId { get; set; }
        public MatrixType matrixType { get; set; }
    }
}
