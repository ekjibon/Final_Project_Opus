
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.OtherSales.Data.Entity.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Distribution.Models
{
    public class ItemPriceFixingViewModel
    {
        public int? itemId { get; set; }

        public int? itemSpecificationId { get; set; }

        public decimal? price { get; set; }

        public decimal? discount { get; set; }

        public decimal? vat { get; set; }

        public int? disitempricefixingId { get; set; }
        public decimal?[] priceall { get; set; }

        public decimal?[] discountall { get; set; }

        public decimal?[] vatall { get; set; }
        public int?[] distributortypeIdall { get; set; }


        public IEnumerable<DisItemPriceFixing> itemPriceFixings { get; set; }
        public IEnumerable<DistributorType> distributorTypes { get; set; }
    }
}
