using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.IOU
{
    [Table("IOUMaster", Schema = "SCM")]
    public class IOUMaster:Base
    {
        [Column(TypeName = "nvarchar(120)")]
        public string iouNo { get; set; }

        public DateTime? iouDate { get; set; }

        public DateTime? expectedDeliveryDate { get; set; }

        //public int? requisitionId { get; set; }
        //public RequisitionMaster requisition { get; set; }

        [Column(TypeName = "nvarchar(120)")]
        public string attentionTo { get; set; }

        public int? attentionToId { get; set; }

        public int? iouStatus { get; set; }
        [NotMapped]
        public string iouCurrentStatus { get; set; }

        [NotMapped]
        public decimal? iouValue { get; set; }

        public int? userId { get; set; }

        public int? projectId { get; set; }
        public Project project { get; set; }

        [NotMapped]
        public string procurementPerson { get; set; }
        [NotMapped]
        public string approverName { get; set; }
        [NotMapped]
        public RequisitionMaster requisition { get; set; }

        [NotMapped]
        public string projectName { get; set; }
        
    }
}
