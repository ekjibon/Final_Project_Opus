using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.POS.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Purchase.Data.Entity
{
    [Table("TransferMaster", Schema = "Purchase")]
    public class TransferMaster : Base
    {
        public int? companyId { get; set; }
        public Company company { get; set; }

        public int? storeFromId { get; set; }
        public Store storeFrom { get; set; }

        public int? storeToId { get; set; }
        public Store storeTo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? transferDate { get; set; }
        [MaxLength(30)]
        public string transferNO { get; set; }        

        public string remarks { get; set; }

        public int? isStockClose { get; set; }
        

    }
}
