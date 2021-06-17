using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.POS.Data.Entity
{
    [Table("PosInvoiceDetail", Schema = "POS")]
    public class PosInvoiceDetail : Base
    {
        public int? itemPriceFixingId { get; set; }
        public ItemPriceFixing itemPriceFixing { get; set; }

        public int? posInvoiceMasterId { get; set; }
        public PosInvoiceMaster posInvoiceMaster { get; set; }

        public decimal? quantity { get; set; }

    }
}
