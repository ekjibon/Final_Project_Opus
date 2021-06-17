using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Distribution.Models
{
    public class ItemPriceFixingDetailViewModel
    {
        public string name { get; set; }
        public decimal? price { get; set; }
        public decimal? VAT { get; set; }
        public decimal? discountPersent { get; set; }
    }
}
