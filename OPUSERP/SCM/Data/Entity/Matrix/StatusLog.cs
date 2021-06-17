using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Data.Entity.Stock;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Data.Entity.Matrix
{
    [Table("StatusLog", Schema = "SCM")]
    public class StatusLog:Base
    {
        public int? matrixTypeId { get; set; }
        public MatrixType matrixType { get; set; }

        public int? requisitionId { get; set; }
        public RequisitionMaster requisition { get; set; }

        public int? cSMasterId { get; set; }
        public CSMaster cSMaster { get; set; }

        public int? purchaseId { get; set; }
        public PurchaseOrderMaster purchase { get; set; }

        public int? iOUId { get; set; }
        public IOUMaster iOU { get; set; }

        public int? stockId { get; set; }
        public StockMaster stock { get; set; }

        public int? iOUPaymentMasterId { get; set; }
        public IOUPaymentMaster iOUPaymentMaster { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string remarks { get; set; }

        public int? statusInfoId { get; set; }
        public StatusInfo statusInfo { get; set; }

        public int? userId { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string empName { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string nextEmpName { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Status { get; set; }
    }
}
