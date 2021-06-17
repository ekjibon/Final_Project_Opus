
using OPUSERP.OtherSales.Data.Entity.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.OtherSales.Models
{
    public class ItemPriceFixingViewModel
    {
        public int? itemId { get; set; }

        public int? itemSpecificationId { get; set; }

        public decimal? price { get; set; }

        public decimal? discount { get; set; }

        public decimal? vat { get; set; }
        

        public IEnumerable<ItemPriceFixing> itemPriceFixings { get; set; }
    }
}
