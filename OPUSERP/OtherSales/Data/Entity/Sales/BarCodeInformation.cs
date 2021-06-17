using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Data.Entity.Sales
{
    [Table("BarCodeInformation", Schema = "OSales")]
    public class BarCodeInformation : Base
    {
        public int? itemPriceId { get; set; }
        public ItemPriceFixing itemPrice { get; set; }

        public byte[] barCodeImage { get; set; }

        [MaxLength(50)]
        public string barCode { get; set; }
    }
}
