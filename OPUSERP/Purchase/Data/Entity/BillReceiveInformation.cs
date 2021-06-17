using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Purchase.Data.Entity
{
    [Table("BillReceiveInformation", Schema = "Purchase")]
    public class BillReceiveInformation:Base
    {
        public int? relSupplierCustomerResourseId { get; set; }
        public Organization relSupplierCustomerResourse { get; set; }

        public int? purchaseOrderMasterId { get; set; }
        public PurchaseOrderMaster purchaseOrderMaster { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? billPaymentDate { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string billPaymentNo { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string billPaymentBy { get; set; }
        
        public decimal? totalAmount { get; set; }

        public decimal? dueAmount { get; set; }

        public string remarks { get; set; }

        public int? paymentModeId { get; set; }
        public PaymentMode paymentMode { get; set; }

        public decimal? cashAmount { get; set; }

        public decimal? bankAmount { get; set; }

        public int? ispaid { get; set; }
    }
}
