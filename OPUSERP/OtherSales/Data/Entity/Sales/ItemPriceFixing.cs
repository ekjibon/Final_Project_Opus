using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Data.Entity.Sales
{
    [Table("ItemPriceFixing", Schema = "OSales")]
    public class ItemPriceFixing:Base
    {
        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }

        public decimal? price { get; set; }

        public decimal? discountPersent { get; set; }

        public decimal? VAT { get; set; }

        public byte[] barCodeImage { get; set; }

        [MaxLength(50)]
        public string barCode { get; set; }

        public int? isActive { get; set; }  // 0 = In Active, 1 = Active

        [NotMapped]
        public string ImageUrl { get; set; }
        [NotMapped]
        public string itemName { get; set; }
    }
}
