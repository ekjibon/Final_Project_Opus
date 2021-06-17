using OPUSERP.POS.Data.Entity;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.POS.Models
{
    public class OfferViewModel
    {
        public int offerId { get; set; }

        public string offerName { get; set; }

        public DateTime? offerDate { get; set; }

        public int?[] itemIdall { get; set; }

        public int?[] qntall { get; set; }

        public decimal? price { get; set; }

        public decimal? discount { get; set; }

        public decimal? vat { get; set; }

        public IEnumerable<ItemPriceFixing> itemPriceFixings { get; set; }

        public IEnumerable<OfferMaster> offerMasters { get; set; }
    }
}
