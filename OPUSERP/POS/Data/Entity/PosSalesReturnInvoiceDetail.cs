using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.POS.Data.Entity
{
    [Table("PosSalesReturnInvoiceDetail", Schema = "POS")]
    public class PosSalesReturnInvoiceDetail:Base
    {
        public int? itemPriceFixingId { get; set; }
        public ItemPriceFixing itemPriceFixing { get; set; }

        public int? salesReturnInvoiceMasterId { get; set; }
        public PosSalesReturnInvoiceMaster salesReturnInvoiceMaster { get; set; }

        public int? salesInvoiceDetailId { get; set; }
        public PosInvoiceDetail salesInvoiceDetail { get; set; }
        public decimal? quantity { get; set; }

    }
}
