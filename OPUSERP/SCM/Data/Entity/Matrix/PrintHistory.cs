using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Data.Entity.Stock;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.Matrix
{
    [Table("PrintHistory", Schema = "SCM")]
    public class PrintHistory:Base
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

        public int? printStatus { get; set; }

        public DateTime? printDate { get; set; }
    }
}
