using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Data.Entity.Sales
{
    public class BillReturnPaymentMaster : Base
    {
        //public int? supplierId { get; set; }
        //public Supplier supplier { get; set; }

        public int? relSupplierCustomerResourseId { get; set; }
        public RelSupplierCustomerResourse relSupplierCustomerResourse { get; set; }

        public int? companyId { get; set; }
        public Company company { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? billPaymentDate { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string billPaymentNo { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string billPaymentBy { get; set; }

        //[Column(TypeName = "nvarchar(150)")]
        //public string paymentMode { get; set; }

        public string billNumber { get; set; }

        public string receivedNotes { get; set; }

        public int? isPartialReceived { get; set; }

        public decimal? totalAmount { get; set; }

        public string remarks { get; set; }
        public int? paymentModeId { get; set; }
        public PaymentMode paymentMode { get; set; }
        public decimal? cashAmount { get; set; }
        public decimal? bankAmount { get; set; }
        //public int? saveStatusId { get; set; }
        //public SaveStatus saveStatus { get; set; }
        public int? storeId { get; set; }
        public Store store { get; set; }


    }
}
