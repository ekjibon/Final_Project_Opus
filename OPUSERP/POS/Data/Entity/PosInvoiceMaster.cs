using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.POS.Data.Entity
{
    [Table("PosInvoiceMaster", Schema = "POS")]
    public class PosInvoiceMaster : Base
    {
        public int? posCustomerId { get; set; }
        public PosCustomer posCustomer { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? invoiceDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? paymentDate { get; set; }
        [MaxLength(100)]
        public string invoiceNumber { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? totalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? VATOnTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountOnTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? NetTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? givenAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? change { get; set; }

        public int? isClose { get; set; } //1=Paid, 0 = have Due

        public int? isStockClose { get; set; } //1 = full stock, 0 = Due
        public int? storeId { get; set; }
        public Store store { get; set; }
    }
}
