using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseOrder
{
    [Table("PurchaseOrderMaster", Schema = "SCM")]
    public class PurchaseOrderMaster:Base
    {
        [Column(TypeName = "nvarchar(100)")]
        public string poNo { get; set; }

        public DateTime? poDate { get; set; }

        public int? csMasterId { get; set; }
        public CSMaster cSMaster { get; set; }

        public DateTime? deliveryDate { get; set; }

        public int? supplierId { get; set; }
        public Organization supplier { get; set; }

        public int? poStatus { get; set; }

        public int? deliveryModeId { get; set; }
        public DeliveryMode deliveryMode { get; set; }

        public int? paymentModeId { get; set; }
        public PaymentMode paymentMode { get; set; }

        public int? paymentTermsId { get; set; }
        public PaymentTerms paymentTerms { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string billToLocation { get; set; }

        public int? isCustomise { get; set; }

        public int? receiveStatus { get; set; }

        public int? userId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? savingAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? savingPercent { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string remarks { get; set; }

        //new add on 12/12/2020       

        public DateTime? paymentDate { get; set; }
        [MaxLength(150)]
        public string rfqRef { get; set; }
        public decimal? taxPercent { get; set; }
        public decimal? vatPercent { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? taxAmount { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? netTotal { get; set; }
        public int? isClose { get; set; }
        public int? isStockClose { get; set; }
        [MaxLength(50)]
        public string poType { get; set; } //Like = CS, Purchase

        [NotMapped]
        public decimal? poValue { get; set; }

        [NotMapped]
        public int? printStatus { get; set; }
    }
}
