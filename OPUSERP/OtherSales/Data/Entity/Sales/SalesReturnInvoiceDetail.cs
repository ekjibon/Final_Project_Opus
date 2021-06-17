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
    [Table("SalesReturnInvoiceDetail", Schema = "OSales")]
    public class SalesReturnInvoiceDetail:Base
    {
        public int? itemPriceFixingId { get; set; }
        public ItemPriceFixing itemPriceFixing { get; set; }

        public int? salesReturnInvoiceMasterId { get; set; }
        public SalesReturnInvoiceMaster salesReturnInvoiceMaster { get; set; }

        public int? salesInvoiceDetailId { get; set; }
        public SalesInvoiceDetail salesInvoiceDetail { get; set; }

        public decimal? quantity { get; set; }

    }
}
