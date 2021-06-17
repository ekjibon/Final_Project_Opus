using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.SCM.Data.Entity.MasterData;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.Requisition
{
    [Table("DocumentAttachmentDetail", Schema = "SCM")]
    public class DocumentAttachmentDetail: Base
    {
       public int? requisitionDetailId { get; set; }
       public RequisitionDetail requisitionDetail { get; set; }

        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }

        public string fileName { get; set; }

        public string fileType { get; set; }

        public string filePath { get; set; }

       
    }
}
