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
    [Table("SalesReturnInvoiceMaster", Schema = "OSales")]
    public class SalesReturnInvoiceMaster:Base
    {
        public int? relSupplierCustomerResourseId { get; set; }
        public RelSupplierCustomerResourse relSupplierCustomerResourse { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? invoiceDate { get; set; }
        public DateTime? paymentDate { get; set; }

        public string invoiceNumber { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? totalAmount { get; set; }
        public decimal? VATOnTotal { get; set; }
        public decimal? DiscountOnTotal { get; set; }
        public decimal? NetTotal { get; set; }

        public int? isClose { get; set; } //1=Paid, 0 = have Due
        public int? isStockClose { get; set; } //1 = full stock, 0 = Due
        public int? isPayClose { get; set; }
        public string remarks { get; set; }
        public int? companyId { get; set; }
        public Company company { get; set; }
        public int? storeId { get; set; }
        public Store store { get; set; }


    }
}
