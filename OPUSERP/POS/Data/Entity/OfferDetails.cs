using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.POS.Data.Entity
{
    [Table("OfferDetails", Schema = "POS")]
    public class OfferDetails : Base
    {
        public int? itemPriceFixingId { get; set; }
        public ItemPriceFixing itemPriceFixing { get; set; }

        public decimal? quantity { get; set; }

        public int? offerMasterId { get; set; }
        public OfferMaster offerMaster { get; set; }
    }
}
