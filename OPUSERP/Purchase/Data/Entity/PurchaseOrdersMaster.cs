
using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Purchase.Data.Entity
{

    [Table("PurchaseOrdersMasters", Schema = "Purchase")]
    public class PurchaseOrdersMaster:Base
    {
        [Column(TypeName = "nvarchar(150)")]
        public string poNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? poDate { get; set; }
        public DateTime? paymentDate { get; set; }

        //public int? supplierId { get; set; }
        //public Supplier supplier { get; set; }

        public int? relSupplierCustomerResourseId { get; set; }
        public Organization relSupplierCustomerResourse { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? deliveryDate { get; set; }

      

        [Column(TypeName = "nvarchar(150)")]
        public string rfqRef { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string remarks { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string billToLocation { get; set; }

        public int? companyId { get; set; }

       

        [Column(TypeName = "decimal(18,2)")]
        public decimal? taxPercent { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? vatPercent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? totalAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? taxAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? vatAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? netTotal { get; set; }
      

        public int? isClose { get; set; }
        public int? isStockClose { get; set; }
    }
}
