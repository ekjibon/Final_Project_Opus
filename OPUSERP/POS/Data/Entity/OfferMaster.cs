using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.POS.Data.Entity
{
    [Table("OfferMaster", Schema = "POS")]
    public class OfferMaster : Base
    {
        [MaxLength(250)]
        public string offerName { get; set; }
        [MaxLength(100)]
        public string barCode { get; set; }

        public byte[] barCodeImage { get; set; }

        public decimal? price { get; set; }

        public decimal? discountPersent { get; set; }

        public decimal? VAT { get; set; }

        public DateTime? endDate { get; set; }

        [NotMapped]
        public string ImageUrl { get; set; }
        [NotMapped]
        public string itemName { get; set; }
    }
}
