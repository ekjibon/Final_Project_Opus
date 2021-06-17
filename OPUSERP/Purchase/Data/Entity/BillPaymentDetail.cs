using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Purchase.Data.Entity
{
    [Table("BillPaymentDetail", Schema = "Purchase")]
    public class BillPaymentDetail:Base
    {
        public int? billPaymentMasterId { get; set; }
        public BillPaymentMaster billPaymentMaster { get; set; }

        public int? billReceiveInformationId { get; set; }
        public BillReceiveInformation billReceiveInformation { get; set; }

        //public int? purchaseOrderMasterId { get; set; }
        //public PurchaseOrdersMaster purchaseOrderMaster { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? date { get; set; }
    }
}
