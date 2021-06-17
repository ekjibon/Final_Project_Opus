using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.SCM.Data.Entity.MasterData;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.Requisition
{
    [Table("RFQDocumentAttachmentDetail", Schema = "SCM")]
    public class RFQDocumentAttachmentDetail: Base
    {
        public int? rFQPriceMasterId { get; set; }
        public RFQPriceMaster rFQPriceMaster { get; set; }

        public string fileName { get; set; }

        public string fileType { get; set; }

        public string filePath { get; set; }

       
    }
}
