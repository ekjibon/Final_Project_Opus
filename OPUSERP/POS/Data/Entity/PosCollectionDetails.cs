using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.POS.Data.Entity
{
    [Table("PosCollectionDetail", Schema = "POS")]
    public class PosCollectionDetail : Base
    {
        public int? posCollectionMasterId { get; set; }
        public PosCollectionMaster posCollectionMaster { get; set; }

        public int? posInvoiceMasterId { get; set; }
        public PosInvoiceMaster posInvoiceMaster { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? collectionDate { get; set; }

        public decimal? Amount { get; set; }

        public string remarks { get; set; }
    }
}
