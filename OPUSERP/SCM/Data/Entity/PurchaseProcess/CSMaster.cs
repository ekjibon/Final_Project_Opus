using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseProcess
{
    [Table("CSMaster", Schema = "SCM")]
    public class CSMaster:Base
    {
        public int? requisitionId { get; set; }
        public RequisitionMaster requisition { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string csNo { get; set; }

        public DateTime? csDate { get; set; }

        public int? csStatus { get; set; }

        public DateTime? expectedDeliveryDate { get; set; }

        [StringLength(200)]
        public string rfqRefNo { get; set; }

        public int? userId { get; set; }

        public int? procurementTypeId { get; set; }
        public ProcurementType procurementType { get; set; }

        public int? procurementValueId { get; set; }
        public ProcurementValue procurementValue { get; set; }
        public int? rFQMasterId { get; set; }
        public RFQMaster rFQMaster { get; set; }

        [NotMapped]
        public string procurementPerson { get; set; }

        [NotMapped]
        public string approverName { get; set; }

    }
}
