using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.Stock
{
    [Table("OpeningStock", Schema = "SCM")]
    public class OpeningStock : Base
    {
        public int? itemSpecificationId { get; set; }
        public ItemSpecification itemSpecification { get; set; }

        public decimal? openingQty { get; set; }
        public decimal? openingRate { get; set; }
        public decimal? openingValue { get; set; }
        public DateTime? balanceUpTo { get; set; }


    }
}
