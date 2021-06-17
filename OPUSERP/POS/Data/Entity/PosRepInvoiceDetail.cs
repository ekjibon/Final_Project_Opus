using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.POS.Data.Entity
{
    [Table("PosRepInvoiceDetail", Schema = "POS")]
    public class PosRepInvoiceDetail: Base
    {
        public int? itemPriceFixingId { get; set; }
        public ItemPriceFixing itemPriceFixing { get; set; }

        public int? posRepInvoiceMasterId { get; set; }
        public PosRepInvoiceMaster posRepInvoiceMaster { get; set; }

        public decimal? quantity { get; set; }
    }
}
