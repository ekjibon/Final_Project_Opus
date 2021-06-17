using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Data.Entity.MasterData
{
    [Table("DisItemPriceFixingDetails", Schema = "Distribution")]
    public class DisItemPriceFixingDetails : Base
    {
        

        public decimal? price { get; set; }

        public decimal? discountPersent { get; set; }

        public decimal? VAT { get; set; }

        public byte[] barCodeImage { get; set; }

        [MaxLength(50)]
        public string barCode { get; set; }



      
      
        public int? disItemPriceFixingId { get; set; }
        public DisItemPriceFixing disItemPriceFixing { get; set; }
        public int? distributorTypeId { get; set; }
        public DistributorType distributorType { get; set; }
    }
}
