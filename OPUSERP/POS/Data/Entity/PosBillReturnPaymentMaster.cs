using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.POS.Data.Entity
{
    [Table("PosBillReturnPaymentMaster", Schema = "POS")]
    public class PosBillReturnPaymentMaster : Base
    {      
        public int? PosCustomerId { get; set; }
        public PosCustomer PosCustomer { get; set; }

        public int? companyId { get; set; }
        public Company company { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? billPaymentDate { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string billPaymentNo { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string billPaymentBy { get; set; }
        [MaxLength(100)]
        public string billNumber { get; set; }
        [MaxLength(100)]
        public string receivedNotes { get; set; }

        public int? isPartialReceived { get; set; }

        public decimal? totalAmount { get; set; }

        public string remarks { get; set; }

        public int? paymentModeId { get; set; }
        public PaymentMode paymentMode { get; set; }

        public decimal? cashAmount { get; set; }
        public decimal? bankAmount { get; set; }
       
        public int? storeId { get; set; }
        public Store store { get; set; }


    }
}
